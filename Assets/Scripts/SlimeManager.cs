// Criado por: Henrique Batista de Assis
// Data: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManager : MonoBehaviour
{
    [Header("Slime Containers")]
    [SerializeField] private GameObject slimeGreenContainer;
    [SerializeField] private GameObject slimeBlueContainer;
    
    private void Update()
    {
        SpawnSlime();
    }

    private void SpawnSlime()
    {
        if (Input.GetButtonDown("F1"))
        {
            slimeGreenContainer.SetActive(true);
            slimeBlueContainer.SetActive(false);
        }

        if (Input.GetButtonDown("F2"))
        {
            slimeGreenContainer.SetActive(false);
            slimeBlueContainer.SetActive(true);
        }

        if (Input.GetButtonDown("F3"))
        {
            slimeGreenContainer.SetActive(true);
            slimeBlueContainer.SetActive(true);
        }
    }
}
