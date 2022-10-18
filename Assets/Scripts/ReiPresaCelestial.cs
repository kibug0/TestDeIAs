using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiPresaCelestial : MonoBehaviour
{
    public float speed;
    public int dano;
    public Rigidbody2D rb;
    private Transform player;
    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);

    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        Destruir();
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {

        if (hit.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }

        if (hit.CompareTag("Player"))
        {
            Jogador jogador = hit.GetComponent<Jogador>();
            {
                if (jogador != null)
                {
                    jogador.TomarDano(dano);
                }
            }
            Destroy(gameObject);
        }

        if (hit.CompareTag("Espada"))
        {
            Destroy(gameObject);
        }

    }

    private void Destruir()
    {

        Destroy(gameObject, 5f);
    }
}
