using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Slider slider;

    public void DefineVidaMax(int vida)
    {
        slider.maxValue = vida;
        slider.value = vida;
    }

    public void DefineVida(int vida)
    {
        slider.value = vida;
    }
}
