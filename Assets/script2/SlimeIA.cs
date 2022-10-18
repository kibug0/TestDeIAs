
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum states{idle, walk, attack}
public enum Enimigo{Fujao, atacante}
public class SlimeIA : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 targetCenter;
    public Animator animator;
    public SpriteRenderer sprite;

    

    //Variaveis internas
    public float walkDistance;
    public float areaRange;
    public Vector2 randomWalk;
    public GameObject player;
    
    public Enimigo tipo;
    public Vector2 movement;
    public float distanceToPlayer;
    
    public float timerMin = 0.5f;
    public float timerMax = 0.8f;

    public Vector2 extraSpeed;
    public Vector2 dir;

    public states _states = states.idle;
    private float _timer = 0f;
    private Vector2 _playerDirection;
    private Vector2 _playerDir;
    public bool _Bateu;

    //variaveis de calibracao
    public float walkSpeed = 10f;
    public float offsetSpeed = 2f;
    public float offsetDist = 0.5f;

    public float distAttack = 0.1f;

    public float distanceToWalk;

    public float distanceToRun;
    public float attackSpeed = 4.9f;
    

    // Start is called before the first frame update
    void Start()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }

        player = GameObject.FindWithTag("Player");

        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        _timer = Random.Range(timerMin, timerMax);

        _playerDirection =  player.transform.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var slimes = GameObject.FindGameObjectsWithTag("Slime");

        extraSpeed = Vector2.zero;

        foreach (var sl in slimes)
        {
            if (sl != this)
            {
                float dd = Vector2.Distance(sl.transform.position, this.transform.position);

                if (dd <= offsetDist)
                {
                    dir = this.transform.position - sl.transform.position;

                    extraSpeed += dir.normalized * AttackSpeed(dd);
                }
            }
        }

        distanceToPlayer = Vector2.Distance(player.transform.position,this.transform.position);

        animator.SetFloat("x", _playerDir.x);
        animator.SetFloat("y", _playerDir.y);

        switch(tipo)
        {
            case Enimigo.atacante:
                PensamentoAtacante();
                break;

            case Enimigo.Fujao:
                PensamentoFujao();
                break;
        }
        

        _timer -= Time.deltaTime;
    }

    public void PensamentoAtacante()
    {
        switch(_states)
        {
            case states.idle:
                if(distanceToPlayer < distanceToWalk)
                    _states = states.walk;

                if(_timer<0)
                {
                    randomWalk = new Vector2(Random.Range(targetCenter.x - areaRange, targetCenter.x + areaRange), Random.Range(targetCenter.y - areaRange, targetCenter.y + areaRange));
                    _timer = Random.Range(timerMin, timerMax);
                }

                break;

            case states.walk:

                if(_timer<0)
                {
                    _playerDirection =  player.transform.position - this.transform.position;
                    if(distanceToPlayer > distanceToWalk){
                        _states = states.idle;
                    }
                    

                    _timer = Random.Range(timerMin, timerMax);
                }
                if (distanceToPlayer < distAttack)
                {
                    _playerDir = (player.transform.position - this.transform.position);
                    _states = states.attack;
                    _timer = (distanceToPlayer + distAttack) / attackSpeed;
                }
                
                break;


            case states.attack:
            if (_timer < 0)
                {
                    _states = states.idle;
                    _timer = Random.Range(0.5f, 1f);
                }
                break;
        }

    }

    public void PensamentoFujao()
    {
        switch(_states)
        {
            case states.idle:
                if(distanceToPlayer < distanceToWalk)
                    _states = states.walk;

                if(_timer<0)
                {
                    randomWalk = new Vector2(Random.Range(targetCenter.x - areaRange, targetCenter.x + areaRange), Random.Range(targetCenter.y - areaRange, targetCenter.y + areaRange));
                    _timer = Random.Range(timerMin, timerMax);
                }

                break;

            case states.walk:

                if(_timer<0)
                {
                    _playerDirection =  player.transform.position - this.transform.position;
                    _timer = Random.Range(timerMin, timerMax);
                    if(!_Bateu)
                    {
                        if(distanceToPlayer > distanceToWalk)
                        {
                            _states = states.idle;
                            _timer = 1f;
                        }
                    }
                    else if(distanceToPlayer>distanceToRun)
                    {
                        _Bateu = false;
                        _states = states.idle;
                    }
                }
                if (distanceToPlayer < distAttack)
                {
                    _playerDir = (player.transform.position - this.transform.position);
                    _states = states.attack;
                    _timer = (distanceToPlayer + distAttack) / attackSpeed;
                }
                
                break;


            case states.attack:
            if (_timer < 0)
                {
                    _states = states.walk;
                    _Bateu = true;
                    _timer = Random.Range(0.5f, 1f);
                }
                break;
        }

    }

    void FixedUpdate()
    {
        switch(tipo)
        {
            case Enimigo.atacante:
                MovimentoAtacante();
                break;

            case Enimigo.Fujao:
                MovimentoFujao();
                break;
        }
    }

    public void MovimentoAtacante()
    {
        

        switch(_states)
        {
            case states.idle:
                //animator.SetBool("isAttacking", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isIdle", true);
                
                //rb.MovePosition(rb.position + randomWalk.normalized * walkSpeed * Time.fixedDeltaTime);
                break;

            case states.walk:
                //animator.SetBool("isAttacking", false);
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalking", true);
                rb.MovePosition(rb.position + _playerDirection.normalized * walkSpeed * Time.fixedDeltaTime);
                break;


            case states.attack:
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalking", false);
                //animator.SetBool("isAttacking", true);
                rb.MovePosition(rb.position + (_playerDir.normalized * attackSpeed * Time.fixedDeltaTime));
                break;
        }
    }

    public void MovimentoFujao()
    {
        

        switch(_states)
        {
            case states.idle:
                //animator.SetBool("isAttacking", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isIdle", true);
                
                //rb.MovePosition(rb.position + randomWalk.normalized * walkSpeed * Time.fixedDeltaTime);
                break;

            case states.walk:
                //animator.SetBool("isAttacking", false);
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalking", true);
                if(_Bateu)
                {
                    rb.MovePosition(rb.position - _playerDirection.normalized * walkSpeed * Time.fixedDeltaTime);
                }
                else
                {
                    rb.MovePosition(rb.position + _playerDirection.normalized * walkSpeed * Time.fixedDeltaTime);
                }
                break;


            case states.attack:
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalking", false);
                //animator.SetBool("isAttacking", true);
                rb.MovePosition(rb.position + (_playerDir.normalized * attackSpeed * Time.fixedDeltaTime));
                break;
        }
    }

    public float AttackSpeed(float _d)
    {
        return 2 * offsetSpeed + (offsetDist - _d)/ offsetDist;
    }

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(player.transform.position, distanceToRun);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.transform.position, distAttack);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(player.transform.position, distanceToWalk);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, offsetDist);
    }
}
