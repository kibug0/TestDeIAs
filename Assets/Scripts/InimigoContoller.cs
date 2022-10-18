using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoController : MonoBehaviour
{
    public BarraDeVida barraDeVida;
    public int inimigoVidaMax;
    public int inimigoVidaAtual;
    public float speed;
    public float defineSpeed;
    public int attackPower;

    public float knockBackX;
    public float knockBackY;


    protected float confuseTime;
    public float startConfuseTime;

    protected bool recovering;
    protected float recoveryCount;
    public float recoveryTime;
    public float colorHitSec;

    public Rigidbody2D rgdb;
    public SpriteRenderer spriteInimigo;
    public Animator anim;

    protected int olhandoDirecao = 1;


    // Start is called before the first frame update
    void Awake()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Jogador>().TomarDano(attackPower);
            Debug.Log("encostou");

            confuseTime = startConfuseTime;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Jogador>().TomarDano(attackPower);
            Debug.Log("encostou");

            confuseTime = startConfuseTime;
        }
    }

    public void InimigoTomarDano(int dano)
    {

        if (!recovering)
        {
            rgdb.AddForce(new Vector2(knockBackX * -olhandoDirecao, knockBackY), ForceMode2D.Impulse);

            StartCoroutine(ColorHit());

            confuseTime = startConfuseTime;

            inimigoVidaAtual -= dano;

            barraDeVida.DefineVida(inimigoVidaAtual);

            if (inimigoVidaAtual <= 0)
            {
                Morrer();
            }
        }
    }

    IEnumerator ColorHit()
    {
        spriteInimigo.color = Color.red;
        yield return new WaitForSeconds(colorHitSec);
        spriteInimigo.color = Color.white;
    }

    void Morrer()
    {
        Destroy(gameObject, 0.3f);
    }
}
