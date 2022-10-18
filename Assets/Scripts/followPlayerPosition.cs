using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayerPosition : MonoBehaviour
{
    private Transform player;

    private float speed = 0f;
    private float stopdist = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) > stopdist)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
