using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    #region Internas
    Rigidbody2D _rb;

    Vector2 _movement;
    Vector2 _posicaoOrgiem = new Vector2(-16, -11);
    Vector2 _posicaoFinal = new Vector2(10, 10);
    Vector2 _cohesion;
    Vector2 _currentSpeed;

    float _perceptionDistanceSqr;
    #endregion

    #region Calibracao
    [Header("Boss Movement")]
    [Range(0f, 10f)] [SerializeField] private float speedWalk = 3f;
    [Range(1f, 5f)] [SerializeField] private float smoothDamp = 1f;

    [Header("Detection")]
    [Range(0f, 10f)] [SerializeField] private float perceptionDistance = 5f;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        var ang = Random.Range(0, 2 * Mathf.PI);
        _movement.x = Mathf.Cos(ang);
        _movement.y = Mathf.Sin(ang);

        _perceptionDistanceSqr = Mathf.Pow(perceptionDistance, 2);
    }

    private void Update()
    {
        //Reflect();
        Perception();
    }

    void FixedUpdate()
    {
        Move();
    }
    #endregion

    #region Methods
    private void Move()
    {
        _rb.MovePosition(_rb.position + _movement * speedWalk * Time.fixedDeltaTime);
    }

    //private void Reflect()
    //{
    //    var nextX = _rb.position.x + _movement.x * speedWalk * Time.fixedDeltaTime;
    //    var nextY = _rb.position.y + _movement.y * speedWalk * Time.fixedDeltaTime;

    //    if (nextX <= _posicaoOrgiem.x || nextX >= _posicaoFinal.x)
    //    {
    //        _movement.x = -_movement.x;
    //    }

    //    if (nextY <= _posicaoOrgiem.y || nextY >= _posicaoFinal.y)
    //    {
    //        _movement.y = -_movement.y;
    //    }
    //}

    private void Perception()
    {
        _cohesion = Vector2.zero;
        int numeroBosses = 0;
        var bosses = GameObject.FindGameObjectsWithTag("Boss");

        foreach (var boss in bosses)
        {
            if (boss != this)
            {
                Vector2 distance = boss.transform.position - this.transform.position;

                float distSqr = Vector2.SqrMagnitude(distance);
                if (distSqr <= _perceptionDistanceSqr)
                {
                    _cohesion += distance;
                    numeroBosses++;
                }
            }
        }

        if (numeroBosses > 0)
        {
            _cohesion /= numeroBosses;
            _cohesion = _cohesion.normalized;
        }

        _movement = Vector2.SmoothDamp(_movement, _cohesion, ref _currentSpeed, smoothDamp);
        _movement = _movement.normalized;
    }
    #endregion

    #region Events
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _movement = Vector2.Reflect(_movement, collision.contacts[0].normal);
    }
    #endregion
}
