using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoVoador : InimigoController
{
    public float stopDistance;
    public float enemyDetect;

    private Transform target;

    bool direita = true;

    // Start is called before the first frame update
    void Start()
    {
        rgdb = GetComponent<Rigidbody2D>();
        spriteInimigo = GetComponent<SpriteRenderer>();

        inimigoVidaAtual = inimigoVidaMax;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(transform.position, target.position) < enemyDetect)
        {
            if (confuseTime <= 0)
            {
                speed = defineSpeed;
            }
            else
            {
                speed = 0;
                confuseTime -= Time.deltaTime;
            }

            if (recovering)
            {
                recoveryCount += Time.deltaTime;
                if (recoveryCount >= recoveryTime)
                {
                    recoveryCount = 0;
                    recovering = false;
                }
            }

            Mover();
        }
    }

    public void Mover()
    {
        if ((target.position.x > transform.position.x && !direita) || (target.position.x < transform.position.x && direita))
        {
            Flip();
        }

        if (Vector2.Distance(transform.position, target.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        }
    }

    void Flip()
    {
        olhandoDirecao *= -1;
        direita = !direita;
        transform.Rotate(0f, 180f, 0f);
    }

}
