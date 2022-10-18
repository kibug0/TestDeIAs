using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    public float Speed;
    public float Pulo;
    public float DoubleJumpMultiplicador;
    public float recoveryTime;

    public int facingDirection = 1; //direita = 1 /esquerda = -1
    public bool facingRight = true;

    public BarraDeVida barraDeVida;
    public int vidaMax;
    public int vidaAtual;

    private bool canMove = true;
    private bool recovering;
    private float recoveryCount;
    public float colorHitSec;

    public bool Pulando;
    public bool DoubleJump;

    public SpriteRenderer personagem;
    public Rigidbody2D rgdb;
    public Animator animator;

    public Transform groundCheckP;
    public Transform groundCheckPn;

    // Start is called before the first frame update
    void Start()
    {
        rgdb = GetComponent<Rigidbody2D>();
        vidaAtual = vidaMax;
        barraDeVida.DefineVidaMax(vidaMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Move();
            Jump();
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

    }

    void Move()
    {

        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        //transform.position += movement * Time.deltaTime * Speed;

        float movement = Input.GetAxis("Horizontal");
        rgdb.velocity = new Vector2(movement * Speed, rgdb.velocity.y);

        if (Input.GetAxis("Horizontal") > 0f && !facingRight)
        {
            Flip();
            
        }
        if (Input.GetAxis("Horizontal") < 0f && facingRight)
        {
            Flip();
            
        }
        if (Input.GetAxis("Horizontal") == 0f)
        {

        }

    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if (EstaNoChao())
            {
                rgdb.AddForce(new Vector2(0f, Pulo), ForceMode2D.Impulse);
                DoubleJump = true;
            }
            else
            {
                if(DoubleJump)
                {
                    rgdb.AddForce(new Vector2(0f, Pulo * DoubleJumpMultiplicador), ForceMode2D.Impulse);
                    DoubleJump = false;
                }
            }

        }
    }
    public bool EstaNoChao()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(groundCheckP.position, Vector2.down, personagem.bounds.size.y / 2, LayerMask.GetMask("Plataforma"));
        RaycastHit2D hit2Dn = Physics2D.Raycast(groundCheckPn.position, Vector2.down, personagem.bounds.size.y / 2, LayerMask.GetMask("Plataforma"));
        return hit2D.collider != null || hit2Dn.collider != null;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundCheckP.position, Vector2.down * personagem.bounds.size / 2);
        Gizmos.DrawRay(groundCheckPn.position, Vector2.down * personagem.bounds.size / 2);
    }

    void Flip()
    {
        facingDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void TomarDano(int dano)
    {
        if (!recovering)
        {
            vidaAtual -= dano;
            StartCoroutine(ColorHit());
            Knockback();

            barraDeVida.DefineVida(vidaAtual);

            recovering = true;

            if (vidaAtual <= 0)
            {
                Morrer();
            }
        }

    }

    IEnumerator ColorHit()
    {
        personagem.color = Color.red;
        yield return new WaitForSeconds(colorHitSec);
        personagem.color = Color.white;
    }

    IEnumerator Freeze()
    {
        canMove = false;

        yield return new WaitForSeconds(0.5f);

        canMove = true;
    }

    void Knockback()
    {
        rgdb.AddForce(new Vector2(100 * -facingDirection, 120), ForceMode2D.Impulse);

        StartCoroutine("Freeze");
    }

    public void GanharVida(int cura)
    {
        vidaAtual += cura;

        barraDeVida.DefineVida(vidaAtual);

        if(vidaAtual >= vidaMax)
        {
            vidaAtual = vidaMax;
        }
    }

    public void Morrer()
    {
        vidaAtual = 0;
        barraDeVida.DefineVida(vidaAtual);
        StartCoroutine(ColorHit());

        GetComponent<AtaqueJogador>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 1f);

        GameController.instance.ShowGameOver();
    }
}
