using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ChefBehavior cookerSelected;
    public ChefBehavior[] cookers;
    public AgentBehavior[] waiters;
    public GameLoop gameLoop;
    public Canvas cursor;
    public Image cheerCooldownGauge;
    public Image dragCooldownGauge;
    public bool dragModeAcivated;
    public Camera camera;

    [Header("Action timers :")]
    public float cheerCooldown;
    public float cheerTimer;
    public float dragCooldown;
    public float dragTimer;

    [Header("Mood values :")]
    public float moodShootPenalty;
    public float moodDragPenalty;
    public float moodCheerBonus;

    private void Start()
    {
        cookers = FindObjectsOfType<ChefBehavior>();
        gameLoop = FindObjectOfType<GameLoop>();
        camera = FindObjectOfType<Camera>();
    }

    public void Update()
    {
        if (cheerTimer == 0)
        {
            cheerCooldownGauge.fillAmount = 0;
        }
        else
        {
            cheerCooldownGauge.fillAmount = cheerTimer / cheerCooldown;
        }
        
        if (dragTimer == 0)
        {
            dragCooldownGauge.fillAmount = 0;
        }
        else
        {
            dragCooldownGauge.fillAmount = dragTimer / dragCooldown;
        }

        if(cheerTimer <= 0)
        {
            cheerTimer = 0;
        }
        else
        {
            cheerTimer -= Time.deltaTime;
        }

        if (dragTimer <= 0)
        {
            dragTimer = 0;
        }
        else
        {
            dragTimer -= Time.deltaTime;
        }

        //Cursor follow mouse
        Vector3 newPosition = new Vector3(camera.ScreenToWorldPoint(Input.mousePosition).x, camera.ScreenToWorldPoint(Input.mousePosition).y, 0);
        cursor.transform.position = newPosition;
    }

    public void shootAction()
    {
        if(cookerSelected != null && (cookerSelected.actor.chefState == ChefState.Working || cookerSelected.actor.chefState == ChefState.GoToFurniture || cookerSelected.actor.chefState == ChefState.Deliver))
        {
            Debug.Log("Shoot");
            Audio.AudioManager.Play("Slap");
            gameLoop.changePortrait(3);
            cookerSelected.actor.SetSlap();
            //Can do action
            if(cookerSelected.actualMood > 0)
            {
                if(cookerSelected.actor.chefState == ChefState.Working)
                {
                    cookerSelected.correctBehavior();
                }
                else if(cookerSelected.carriedIngredient is Meal && (cookerSelected.carriedIngredient as Meal).mealQuality == MealQuality.Failed)
                {
                    cookerSelected.trashMeal();
                }


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
                cookerSelected.actor.gameObject.SetActive(false);
                //Go to pause
            }
            
            //resetSelection();
        }
    }

    public void cheerAction()
    {
        if (cookerSelected != null && cheerTimer <= 0)
        {
            if (cookerSelected.actualMood + moodCheerBonus < cookerSelected.moodMax)
            {
                cookerSelected.actualMood += moodCheerBonus;
            }
            else
            {
                cookerSelected.actualMood = cookerSelected.moodMax;
            }
            //resetSelection();
            cheerTimer = cheerCooldown;
        }
    }

    public void enableDragMode()
    {
        if(dragTimer <= 0)
        {
            dragModeAcivated = true;
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
