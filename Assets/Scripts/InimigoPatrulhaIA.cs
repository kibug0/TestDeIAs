using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoPatrulhaIA : MonoBehaviour
{

    public Rigidbody2D inimigoRgdb;

    public float speed;

    public Vector2 offsetWall;
    public LayerMask paredeLayer;

    public Vector2 offsetGround;
    public LayerMask chaoLayer;

    private RaycastHit2D paredeD;
    private RaycastHit2D paredeE;
    private RaycastHit2D chaoD;
    private RaycastHit2D chaoE;

    private int direction = 1;

    private bool facingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChecagemDeSuperficies();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        inimigoRgdb.velocity = new Vector2(speed * direction, inimigoRgdb.velocity.y);
    }

    private void ChecagemDeSuperficies()
    {
        paredeD = Physics2D.Raycast(new Vector2(transform.position.x + offsetWall.x, transform.position.y + offsetWall.y), Vector2.right, 1f, paredeLayer);
        Debug.DrawRay(new Vector2(transform.position.x + offsetWall.x, transform.position.y + offsetWall.y), Vector2.right, Color.green);

        if (paredeD.collider != null)
        {
            direction = -1;
        }

        paredeE = Physics2D.Raycast(new Vector2(transform.position.x - offsetWall.x, transform.position.y + offsetWall.y), Vector2.left, 1f, paredeLayer);
        Debug.DrawRay(new Vector2(transform.position.x - offsetWall.x, transform.position.y + offsetWall.y), Vector2.left, Color.green);

        if (paredeE.collider != null)
        {
            direction = 1;
        }

        chaoD = Physics2D.Raycast(new Vector2(transform.position.x + offsetGround.x, transform.position.y + offsetGround.y), Vector2.down, 0.1f, chaoLayer);
        Debug.DrawRay(new Vector2(transform.position.x + offsetGround.x, transform.position.y + offsetGround.y), Vector2.down, Color.red);

        if (chaoD.collider == null)
        {
            direction = -1;
        }

        chaoE = Physics2D.Raycast(new Vector2(transform.position.y - offsetGround.x, transform.position.y + offsetGround.y), Vector2.down, 0.1f, chaoLayer);
        Debug.DrawRay(new Vector2(transform.position.x - offsetGround.x, transform.position.y + offsetGround.y), Vector2.down, Color.red);

        if (chaoE.collider == null)
        {
            direction = 1;
        }

        if (facingRight && direction == -1 || !facingRight && direction == 1)
            Flip();

        void Flip()
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
