﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espinhos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Jogador>().Morrer();
        }
        if (other.gameObject.CompareTag("Inimigo"))
        {
            other.gameObject.GetComponent<InimigoController>().InimigoTomarDano(1000);
        }
        
    }
}