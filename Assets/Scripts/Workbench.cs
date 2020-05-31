using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Workbench : Furniture
{
    public RecipeSteps recipeStep;
    public Image progressionGauge;
    public Image iconOutputIngredient;

    private void Start()
    {
        progressionGauge.enabled = false;
        iconOutputIngredient.enabled = false;
    }

    private void Update()
    {
        if(agentUsingFurniture != null)
        {
            progressionGauge.fillAmount = (agentUsingFurniture as ChefBehavior).workingTimer / (agentUsingFurniture as ChefBehavior).workingDuration;
        }
    }
}
