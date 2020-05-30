using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [Header("Game parameter :")]
    public float gameDuration;
    public float gameTimer;
    public bool gameIsRunning;
    public int money;
    public int commandsSuccesful;
    public int commandsFailed;

    [Header("Command parameter :")]
    public Command[] possibleCommands; //Contains prefabs of command
    public List<Command> actualCommands;
    public float delayBtwCommand;
    public float delayBtwCommandTimer;
    public float randomDelayFactor;

    [Header("Associated objects :")]
    public Transform commandParent;

    // Start is called before the first frame update
    void Start()
    {
        gameTimer = gameDuration;
        delayBtwCommandTimer = delayBtwCommand;
        startGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameIsRunning)
        {
            timerManagement();
        }
    }

    public void startGame()
    {
        gameIsRunning = true;
    }

    public void endGame()
    {
        gameIsRunning = false;
    }

    public void timerManagement()
    {
        //Timer global of the game
        if(gameTimer <= 0)
        {
            endGame();
        }
        else
        {
            gameTimer -= Time.deltaTime;
        }


        //Timer for new commands
        if (delayBtwCommandTimer <= 0)
        {
            Command newCommandPrefab = chooseRandomCommand();
            Command commandGO = Instantiate(newCommandPrefab, commandParent);
            actualCommands.Add(commandGO);
            delayBtwCommandTimer = delayBtwCommand + Random.Range(-randomDelayFactor, randomDelayFactor);
        }
        else
        {
            delayBtwCommandTimer -= Time.deltaTime;
        }
    }

    public Command chooseRandomCommand()
    {
        int randomIndex = Random.Range(0, possibleCommands.Length);
        return possibleCommands[randomIndex];
    }
}
