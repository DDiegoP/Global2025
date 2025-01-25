using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LimpiameEstaComponent : GameComponent
{
    private List<GameObject> allFoam;

    private void createFoam()
    {
        // Creamos la lista instanciando prefabs en el canvas -> posiciones aleatorias?

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        createFoam();
    }

    public void gameFinished()
    {
        
    }
}
