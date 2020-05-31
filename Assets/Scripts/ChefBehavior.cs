using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefBehavior : AgentBehavior
{
    [Header("Agents stats :")]
    public float failFactor; //percentage chance to do the wrong thing

    [Header("Associated objects :")]
    public ChefActor actor;
    public Recipe recipeToDo;
    public RecipeSteps currentStep;
    public int recipeStepIndex;
    public Furniture furnitureToInteractWith;

    private void Update()
    {
        if(actor.chefState == ChefState.GoToFurniture)
        {
            destinationSetter.target = targetPoint;
            if(transform == targetPoint)
            {
                actor.chefState = ChefState.Working;
            }
        }
    }

    public void addNewRecipe(Recipe recipe)
    {
        recipeStepIndex = 0;
        recipeToDo = recipe;
        currentStep = recipeToDo.recipeSteps[0];

        //Find furniture to use at the start of the recipe : finding first ingredient in the good fridge
        furnitureToInteractWith = findFurniture(currentStep.workbenchUsed, currentStep.ingredientOutput);
        while (furnitureToInteractWith == null)
        {
            actor.chefState = ChefState.Idle;
            furnitureToInteractWith = findFurniture(currentStep.workbenchUsed);
        }
        targetPoint = furnitureToInteractWith.accessPoint;
        actor.chefState = ChefState.GoToFurniture;
    }

    public void doNextRecipeStep()
    {
        recipeStepIndex++;
        currentStep = recipeToDo.recipeSteps[recipeStepIndex];

        //Find furniture to use for the next step of a recipe
        furnitureToInteractWith = findFurniture(currentStep.workbenchUsed);
        while (furnitureToInteractWith == null)
        {
            actor.chefState = ChefState.Idle;
            furnitureToInteractWith = findFurniture(currentStep.workbenchUsed);
            Debug.Log("Waiting for furniture");
        }
        targetPoint = furnitureToInteractWith.accessPoint;
        actor.chefState = ChefState.GoToFurniture;
    }

    public void interactWithFurniture()
    {
        FurnitureType type = furnitureToInteractWith.furnitureType;
        switch(type)
        {
            case FurnitureType.CuttingTable:

                break;
            case FurnitureType.DeepFryer:

                break;
            case FurnitureType.FoodDisplayer:

                break;
            case FurnitureType.Fridge:

                break;
            case FurnitureType.FryPan:

                break;
            case FurnitureType.SeasonTable:

                break;
        }
        actor.chefState = ChefState.Working;
    }

    //Find the furniture required to do the current recipe's step, if this step require to find an ingredient, it will be a parameter
    public Furniture findFurniture(FurnitureType furnitureType, Ingredient ingredient = null)
    {
        Furniture[] surrondingFurnitures = FindObjectsOfType<Furniture>();
        for (int i = 0; i < surrondingFurnitures.Length; i++)
        {
            if (surrondingFurnitures[i].furnitureType == furnitureType && !surrondingFurnitures[i].isUsed)
            {
                if(ingredient == null)
                {
                    return surrondingFurnitures[i];
                }
                else if((surrondingFurnitures[i] as Fridge).ingredient == ingredient)
                {
                    return surrondingFurnitures[i];
                }
            }
        }
        return null;
    }
}
