using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rei : InimigoController
{
    public float reiSpeed;
    public float reiSpeedDefinitive;
    public Vector2 moveDirection;
    public float charge;

    private Transform player;
    private Vector2 target;

    public Transform groundCheckUp;
    public Transform groundCheckWall;
    public Transform groundCheckDown;
    public float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;
    private bool touchUp;
    private bool touchDown;
    private bool touchWall;
    private bool goingUp = true;
    private bool facingRight = true;

    public float timer;
    public float timerMax;
    public float timerMin;
    public float timeBtwAttack;

    public GameObject DistAttack;
    public Transform DistAttackPoint;

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(timerMin, timerMax);
        rgdb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteInimigo = GetComponent<SpriteRenderer>();
        inimigoVidaAtual = inimigoVidaMax;

        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        touchUp = Physics2D.OverlapCircle(groundCheckUp.position, groundCheckRadius, groundLayer);
        touchDown = Physics2D.OverlapCircle(groundCheckDown.position, groundCheckRadius, groundLayer);
        touchWall = Physics2D.OverlapCircle(groundCheckWall.position, groundCheckRadius, groundLayer);

        Move();

        if(timer <= 0)
        {
            GetsugaTenchou();
        }
        else
        {
            timer -= Time.deltaTime;
        }

        target = player.position - transform.position;

        target.Normalize();
    }



    void Move()
    {
        rgdb.velocity = reiSpeed * moveDirection;

        if(touchUp && goingUp)
        {
            ChangeDirection();
        }
        else if(touchDown && !goingUp)
        {
            ChangeDirection();
        }

        if (touchWall)
        {
            if (facingRight)
            {
                Flip();
            }
            else if(!facingRight)
            {
                Flip();
            }
        }
    }
    
    

    void GetsugaTenchou()
    {
        timeBtwAttack = Random.Range(timerMin, timerMax);
        if (timer <= 0)
        {

            reiSpeed = 0.1f; 
            rgdb.velocity = target * reiSpeed;
            Instantiate(DistAttack, DistAttackPoint.position, Quaternion.identity);
            timer = timeBtwAttack;
            reiSpeed = reiSpeedDefinitive;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void AttackDirect()
    {
        target = player.position - transform.position;

        target.Normalize();

        rgdb.velocity = target * charge;
    }

    void ChangeDirection()
    {
        goingUp = !goingUp;
        moveDirection.y *= -1;

    }
    
    void Flip()
    {
        facingRight = !facingRight;
        moveDirection.x *= -1;
        transform.Rotate(0, 180, 0);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckUp.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckDown.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckWall.position, groundCheckRadius);
    }
}
