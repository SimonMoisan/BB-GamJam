﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : MonoBehaviour
{
    public int difficulty;
    public float timeToPerform;
    public float actualTime;
    public Recipe recipe;

    public GameLoop gameLoop;

    private void Awake()
    {
        gameLoop = FindObjectOfType<GameLoop>();
        actualTime = timeToPerform;
    }

    public void Update()
    {
        if (actualTime <= 0)
        {
            commandExpired();
        }
        else
        {
            actualTime -= Time.deltaTime;
        }
    }

    public void commandExpired()
    {
        gameLoop.actualCommands.Remove(this);
        gameLoop.commandsFailed++;
        Destroy(gameObject);
    }
}
