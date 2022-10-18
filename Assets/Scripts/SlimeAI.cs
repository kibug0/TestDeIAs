using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    enum States
    {
        idle,
        walk,
        attack
    }

    #region Variaveis Internas
    private Rigidbody2D _rb;
    private States _state = States.idle;

    private Vector2 _movement;
    private Vector2 _playerDirection;
    private Vector2 _extraSpeed;
    private Vector2 _directionBetweenSlimes;
    private Vector2 _speed = Vector2.zero;

    private float _distanceToPlayer;
    private float _timer = 0f;

    private Animator _anim;
    
    #endregion

    #region Variaveis de Calibracao
    [Header("Player")]
    [SerializeField] private GameObject playerObj;

    [Header("Walk")]
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float minResponseWalkTime = 0.5f;
    [SerializeField] private float maxResponseWalkTime = 0.3f;

    [Header("Attack")]
    [SerializeField] private float attackSpeed = 10f;
    [SerializeField] private float distanceAttack = 2f;
    [SerializeField] private float resetAttackTimer = 1f;
    [SerializeField] private float attackDistanceOffset = 0.1f;

    [Header("Detection")]
    [SerializeField] private float detectionDistance = 6f;

    [Header("Counter Slimes")]
    [SerializeField] private float offsetDistance = 0.5f;
    [SerializeField] private float offsetSpeed = 1f;
    #endregion

    // //////////////////////////////////////////////////////////////////////////////////////////////////////

    #region Unity Methods
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        _playerDirection = (playerObj.transform.position - this.transform.position).normalized;
        _extraSpeed = Vector2.zero;
    }

    private void Update()
    {
        var slimes = GameObject.FindGameObjectsWithTag("Slime");

        _extraSpeed = Vector2.zero;
        foreach (var slime in slimes)
        {
            if (slime != this)
            {
                float distanceSlimes = Vector2.Distance(slime.transform.position, this.transform.position);
                if (distanceSlimes <= offsetDistance)
                {
                    _directionBetweenSlimes = this.transform.position - slime.transform.position;
                    _extraSpeed += _directionBetweenSlimes.normalized * AttackSpped(distanceSlimes);
                }
            }
        }

        _distanceToPlayer = Vector2.Distance(playerObj.transform.position, this.transform.position);

        CheckState();

        _timer -= Time.deltaTime;

        AnimSlime();
    }

    private void FixedUpdate()
    {
        DoState();
    }
    #endregion

    // /////////////////////////////////////////////////////////////////////////////////////////////////

    #region Methods
    private void CheckState()
    {
        switch (_state)
        {
            case States.idle:
                if (_distanceToPlayer < detectionDistance && _timer < 0)
                {
                    _state = States.walk;
                }

                break;

            case States.walk:
                if (_timer < 0)
                {
                    _playerDirection = playerObj.transform.position - this.transform.position;
                    _timer = Random.Range(minResponseWalkTime, maxResponseWalkTime);

                    if (_distanceToPlayer >= detectionDistance)
                    {
                        _state = States.idle;
                        //_timer = 1f;
                    }
                }
                
                if (_distanceToPlayer < distanceAttack)
                {
                    _playerDirection = playerObj.transform.position - this.transform.position;
                    print("attack");
                    _state = States.attack;
                    _timer = (_distanceToPlayer + attackDistanceOffset) / attackSpeed; // t = d / v
                }
                break;

            case States.attack:
                if (_timer < 0)
                {
                    _state = States.idle;
                    _timer = resetAttackTimer;
                }
                break;
        }

        print("estado slime: " + _state);
    }

    private void DoState()
    {
        switch (_state)
        {
            case States.idle:
                _speed = Vector2.zero;
                break;

            case States.walk:
                _speed = (_playerDirection.normalized * walkSpeed + _extraSpeed) * Time.fixedDeltaTime;
                break;

            case States.attack:
                _extraSpeed = Vector2.zero;
                _speed = (_playerDirection.normalized * attackSpeed) * Time.fixedDeltaTime;
                break;
        }

        _rb.MovePosition(_rb.position + _speed);
    }

    private float AttackSpped(float distance)
    {
        return 2 * offsetSpeed + (offsetDistance - distance) / offsetDistance;
    }

    private void AnimSlime()
    {
        if (_speed != Vector2.zero)
            _anim.SetBool("isWalking", true);

        else
            _anim.SetBool("isWalking", false);

        _anim.SetFloat("x", _speed.x);
        _anim.SetFloat("y", _speed.y);
    }
    #endregion
}
