using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoPatrulheiro : InimigoController
{
    public float distance;

    bool direita = true;

    public Transform groundCheck;

    public SpriteRenderer inimigo;


    void Start()
    {
        rgdb = GetComponent<Rigidbody2D>();
        spriteInimigo = GetComponent<SpriteRenderer>();

        inimigoVidaAtual = inimigoVidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(confuseTime <= 0)
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

        Movimento();

    }

    void Movimento()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, distance, LayerMask.GetMask("Plataforma"));

        if (ground.collider == false)
        {
            if (direita == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                direita = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                direita = true;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundCheck.position, Vector2.down * distance);

    }

}