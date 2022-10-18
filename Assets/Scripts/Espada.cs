using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{
    public float speed;
    public int dano;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        Destruir();
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {

        if (hit.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }

        if (hit.CompareTag("Inimigo"))
        {
            InimigoController inimigocontroller = hit.GetComponent<InimigoController>();
            {
                if( inimigocontroller != null)
                {
                    inimigocontroller.InimigoTomarDano(dano);
                }
            }
            Destroy(gameObject);
        }

    }




    private void Destruir()
    {
        
        Destroy(gameObject, 2f);
    }
}
