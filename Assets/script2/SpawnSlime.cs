using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlime : MonoBehaviour
{
    public GameObject slimeF1, slimeF2;

    public Transform spawnPointF1, spawnPointF2;


    //spawn slimes using the spawnpoints by pressing F1 and F2
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Instantiate(slimeF1, spawnPointF1.position, spawnPointF1.rotation);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            Instantiate(slimeF2, spawnPointF2.position, spawnPointF2.rotation);
        }
    }

}
