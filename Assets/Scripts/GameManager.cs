using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ChefBehavior cookerSelected;
    public ChefBehavior[] cookers;
    public AgentBehavior[] waiters;
    public GameLoop gameLoop;
    public Canvas cursor;

    [Header("Action timers :")]
    public float cheerCooldown;
    public float cheerTimer;
    public float dragCooldown;
    public float dragTimer;

    [Header("Mood values :")]
    public int moodShootPenalty;
    public int moodDragPenalty;
    public int moodCheerBonus;

    private void Start()
    {
        cookers = FindObjectsOfType<ChefBehavior>();
        gameLoop = FindObjectOfType<GameLoop>();
    }

    public void Update()
    {
        
    }

    public void shootAction()
    {
        if(cookerSelected != null && cookerSelected.actor.chefState == ChefState.Working)
        {
            Debug.Log("Shoot");
            //Can do action
            if(cookerSelected.actualMood > 0)
            {
                cookerSelected.correctBehavior();
                //Remove mood
                if(cookerSelected.actualMood >= moodShootPenalty)
                {
                    cookerSelected.actualMood -= moodShootPenalty;
                }
                else
                {
                    cookerSelected.actualMood = 0;
                }
            }
            else
            {
                //Go to pause
            }
            
            resetSelection();
        }
    }

    public void cheerAction()
    {
        if (cookerSelected != null)
        {
            if (cookerSelected.actualMood + moodCheerBonus < cookerSelected.moodMax)
            {
                cookerSelected.actualMood += moodCheerBonus;
            }
            else
            {
                cookerSelected.actualMood = cookerSelected.moodMax;
            }
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
