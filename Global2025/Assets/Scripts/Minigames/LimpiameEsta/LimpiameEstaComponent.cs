using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LimpiameEstaComponent : GameComponent
{
    private List<GameObject> allFoam;
    private ScoreComponent _scoreComponent;
    [SerializeField]
    private Timer score;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = GetComponent<Timer>();
    }

    public void gameFinished()
    {
        
    }
}
