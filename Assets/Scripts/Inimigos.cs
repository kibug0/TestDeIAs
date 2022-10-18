using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigos : MonoBehaviour
{
    public InimigoVoador inimigoVoador;
    public InimigoPatrulheiro inimigoPatrulheiro;

    // Start is called before the first frame update
    void Start()
    {
        inimigoVoador = GetComponent<InimigoVoador>();
        inimigoPatrulheiro = GetComponent<InimigoPatrulheiro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
