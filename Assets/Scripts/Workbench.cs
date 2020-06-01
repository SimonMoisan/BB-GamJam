using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Workbench : Furniture
{
    public RecipeSteps recipeStep;
    public Image progressionGauge;
    public Image iconOutputIngredient;

    private void Start()
    {
        if(progressionGauge != null && iconOutputIngredient != null)
        {
            progressionGauge.enabled = false;
            iconOutputIngredient.enabled = false;
        }
        else
        {
            Debug.Log("Missing objects on furniture");
        }
        
    }

    private void Update()
    {
        if(agentUsingFurniture != null)
        {
            progressionGauge.fillAmount = (agentUsingFurniture as ChefBehavior).workingTimer / (agentUsingFurniture as ChefBehavior).workingDuration;
        }
    }
}
