using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D cc;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    { 
        if(collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<Jogador>().GanharVida(20);

            sr.enabled = false;
            cc.enabled = false;
            Destroy(gameObject, 0f);
        }
    }
}
