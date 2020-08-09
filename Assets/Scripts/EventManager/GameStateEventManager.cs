using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateEventManager : EventManager
{
    public UnityEvent eventGameOver;
    // Start is called before the first frame update
    void Start()
    {
        eventGameOver.AddListener(GameOver);
    }

    private void GameOver()
    {
        //TODO
    }
}
