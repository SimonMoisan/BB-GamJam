using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandSlot : MonoBehaviour
{
    public int commandSlotId;
    public Image mealIcon;
    public Image durationGauge;
    public Image selector;
    public GameLoop gameLoop;
    public GameManager gameManager;
    public Command command;

    public void Start()
    {
        gameLoop = FindObjectOfType<GameLoop>();
        gameManager = FindObjectOfType<GameManager>();
        if(selector != null)
        {
            selector.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(command != null)
        {
            durationGauge.fillAmount = command.actualTime / command.timeToPerform;
        }
    }

    public void attributeCommand()
    {
        Debug.Log("Clicked");
        if(command != null && gameManager.cookerSelected != null && gameManager.cookerSelected.recipeToDo == null)
        {
            gameManager.cookerSelected.addNewRecipe(command.recipe);
            //Deselect cooker
            gameManager.cookerSelected.selector.enabled = false;
            gameManager.cookerSelected = null;
            //Deselec commands
            for (int i = 0; i < gameLoop.commandSlots.Length; i++)
            {
                gameLoop.commandSlots[i].selector.enabled = false;
            }
        }
    }
}
