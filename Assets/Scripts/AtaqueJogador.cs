using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueJogador : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange;
    public int attackPower;

    public Transform jogadaDeEspada;
    public GameObject espada;

    public float attackRateMelee;
    public float attackRateRanged;
    float nextMeleeAttackTime = 0f;
    float nextRangedAttackTime = 0f;

    public LayerMask inimigos;

    // Update is called once per frame
    void Update()
    {

        if (Time.time >= nextMeleeAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ataque();
                nextMeleeAttackTime = Time.time + 1f / attackRateMelee;
            }
        }

        if (Time.time >= nextRangedAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                AtaqueDistante();
                nextRangedAttackTime = Time.time + 1f / attackRateRanged;
            }
        }
    }

    void Ataque()
    {
        animator.SetTrigger("Ataque");

        Collider2D[] hitInimigos = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, inimigos); ;
        
        foreach(Collider2D inimigo in hitInimigos)
        {
            inimigo.GetComponent<InimigoController>().InimigoTomarDano(attackPower);
        }

    }

    void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void AtaqueDistante()
    {
        animator.SetTrigger("AtaqueDist");
        Instantiate(espada, jogadaDeEspada.position, jogadaDeEspada.rotation);
    }
}
