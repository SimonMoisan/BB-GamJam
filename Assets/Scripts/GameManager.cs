using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ChefBehavior cookerSelected;
    public ChefBehavior[] cookers;
    public AgentBehavior[] waiters;
    public GameLoop gameLoop;

    private void Start()
    {
        cookers = FindObjectsOfType<ChefBehavior>();
        gameLoop = FindObjectOfType<GameLoop>();
    }

    private void Update()
    {
        
    }

    public void shootAction()
    {
        if(cookerSelected != null && cookerSelected.actor.chefState == ChefState.Working)
        {

            resetSelection();
        }
    }

    public void cheerAction()
    {
        if (cookerSelected != null)
        {

            resetSelection();
        }
    }

    public void selectCooker(ChefBehavior chefSelected)
    {
        cookerSelected = chefSelected;

        chefSelected.selector.enabled = true;
        for (int i = 0; i < cookers.Length; i++)
        {
            if(cookers[i] != chefSelected)
            {
                cookers[i].selector.enabled = false;
            }
        }

        for (int i = 0; i < gameLoop.commandSlots.Length; i++)
        {
            if(gameLoop.commandSlots[i].command != null)
            {
                gameLoop.commandSlots[i].selector.enabled = true;
            }
        }
    }

    public void resetSelection()
    {
        //Deselect cooker
        cookerSelected.selector.enabled = false;
        cookerSelected = null;
        //Deselec commands
        for (int i = 0; i < gameLoop.commandSlots.Length; i++)
        {
            gameLoop.commandSlots[i].selector.enabled = false;
        }
    }
}
