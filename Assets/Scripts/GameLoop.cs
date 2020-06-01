using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    [Header("Game parameter :")]
    public float gameDuration;
    public float gameTimer;
    public bool gameIsRunning;
    public float actualMoney;
    public float maxMoney;
    public int[] moneyJalons; //Bronze, Silver, Gold, etc...
    public int commandsSuccesful;
    public int commandsFailed;

    public float changePortraitDuration;
    public float changerPortraitTimer;
    public bool portraitIsChanged;


    [Header("Command parameter :")]
    public Command[] possibleCommands; //Contains prefabs of command
    public List<Command> actualCommands;
    public CommandSlot[] commandSlots;
    public float delayBtwCommand;
    public float delayBtwCommandTimer;
    public float randomDelayFactor;

    [Header("Associated objects :")]
    public Transform commandParent;
    public GameManager gameManager;
    public Image moneyGauge;
    public GameObject handcloak;

    public int indexGordon; //0: satisfait, 1: neutre, 2: tilted, 3: colère
    public Sprite[] gordonImages;
    public Image gordonPortrait;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameTimer = gameDuration;
        delayBtwCommandTimer = 5;
        startGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameIsRunning)
        {
            timerManagement();
        }
        moneyGauge.fillAmount = actualMoney / maxMoney;

        if(changerPortraitTimer <= 0)
        {
            portraitIsChanged = false;
            gordonPortrait.sprite = gordonImages[1];
        }
        else
        {
            changerPortraitTimer -= Time.deltaTime;
        }

        handcloak.transform.Rotate(new Vector3(0, 0, (-360 / gameDuration) * Time.deltaTime)); 
    }

    public void startGame()
    {
        gameIsRunning = true;
    }

    public void endGame()
    {
        gameIsRunning = false;
        if (actualMoney < moneyJalons[0])
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            SceneManager.LoadScene("Win");
        }
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
            //If max number of command not reached, add command to a command slot
            if(actualCommands.Count < commandSlots.Length)
            {
                Command newCommandPrefab = chooseRandomCommand();
                Command commandGO = Instantiate(newCommandPrefab, commandParent);

                //Add command to its command slot and vice versa
                actualCommands.Add(commandGO);
                for (int i = 0; i < commandSlots.Length; i++)
                {
                    if(commandSlots[i].command == null)
                    {
                        CommandSlot usedCommandSlot = commandSlots[actualCommands.Count - 1];
                        usedCommandSlot.command = commandGO;
                        commandGO.commandSlot = usedCommandSlot;

                        //Set up sprite and gauge
                        usedCommandSlot.mealIcon.sprite = commandGO.recipe.mealSprite;
                        usedCommandSlot.mealIcon.enabled = true;
                        usedCommandSlot.durationGauge.enabled = true;

                        //Display command slot
                        usedCommandSlot.enabled = true;

                        //Enable selector if an agent is selected
                        if (gameManager.cookerSelected != null)
                        {
                            usedCommandSlot.selector.enabled = true;
                        }

                        delayBtwCommandTimer = delayBtwCommand + Random.Range(-randomDelayFactor, randomDelayFactor);
                        break;
                    }
                }             
            }
            else
            {
                delayBtwCommandTimer = 0;
            }
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

    public void changePortrait(int idPortrait)
    {
        gordonPortrait.sprite = gordonImages[idPortrait];
        portraitIsChanged = true;
        changerPortraitTimer = changePortraitDuration;
    }
}
