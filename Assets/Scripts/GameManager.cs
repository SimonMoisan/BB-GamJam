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

    public void shootAction(int actorId)
    {
        cookerSelected = null;
    }

    public void slapeAction(int actorId)
    {
        cookerSelected = null;
    }

    public void stopAction(int actorId)
    {
        cookerSelected = null;
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
}
