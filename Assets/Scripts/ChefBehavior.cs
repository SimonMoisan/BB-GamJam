﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefBehavior : AgentBehavior
{
    [Header("Agents stats :")]
    public float failFactor; //percentage chance to do the wrong thing
    public float workingDuration;
    public float workingTimer;

    [Header("Associated objects :")]
    public ChefActor actor;
    public Recipe recipeToDo;
    public RecipeSteps currentStep;
    public int recipeStepIndex;
    public Furniture furnitureToInteractWith;
    public Ingredient carriedIngredient;

    private void Update()
    {
        //Go to a furniture
        if(actor.chefState == ChefState.GoToFurniture && actor.canMove)
        {
            destinationSetter.target = targetPoint;
            if(aIPath.desiredVelocity.x == 0 && aIPath.desiredVelocity.y == 0 && aIPath.reachedDestination) //Is in front of the furniture
            {
                workingDuration = currentStep.duration;
                workingTimer = workingDuration;
                actor.chefState = ChefState.Working;
                actor.SetWorking(true);
            }
        }

        //Work on a furniture
        else if(actor.chefState == ChefState.Working)
        {
            if(workingTimer <= 0) //Finish work and go to next step
            {
                actor.SetWorking(false);
                actor.chefState = ChefState.Idle;
                furnitureToInteractWith.isUsed = false;

                //Reset timer and duration
                workingDuration = 0;
                workingTimer = 0;
                carriedIngredient = currentStep.ingredientOutput; //Get new ingredient at the end of this step
                actor.SetHolding(true);

                //Go to next step of the recipe
                if(carriedIngredient is Meal)
                {
                    deliverMeal();
                }
                else if(recipeStepIndex < recipeToDo.recipeSteps.Length - 1)
                {
                    recipeStepIndex++;
                    doNextRecipeStep();
                }
            }
            else
            {
                workingTimer -= Time.deltaTime;
            }
        }

        //Go to a delivering chariot
        else if(actor.chefState == ChefState.Deliver)
        {
            destinationSetter.target = targetPoint;
            if (aIPath.desiredVelocity.x == 0 && aIPath.desiredVelocity.y == 0 && aIPath.reachedDestination) //Is in front of the furniture
            {
                FoodDisplayer chariot = (furnitureToInteractWith as FoodDisplayer);
                chariot.mealsToServe.Add(carriedIngredient as Meal);
                chariot.isUsed = false;
                carriedIngredient = null;
                actor.SetHolding(false);
                actor.chefState = ChefState.Idle;
            }
        }

        //Waiting for a furniture to get free
        else if(actor.chefState == ChefState.Waiting)
        {
            if (carriedIngredient is Meal)
            {
                deliverMeal();
            }
            else
            {
                //If Ingredient isn't null and the workbenchUsed isn't a fridge it causes problem
                if (currentStep.workbenchUsed == FurnitureType.Fridge)
                {
                    furnitureToInteractWith = findFurniture(currentStep.workbenchUsed, currentStep.ingredientOutput);
                }
                else
                {
                    furnitureToInteractWith = findFurniture(currentStep.workbenchUsed);
                }

                if (furnitureToInteractWith != null)
                {
                    targetPoint = furnitureToInteractWith.accessPoint;
                    furnitureToInteractWith.isUsed = true;
                    destinationSetter.target = targetPoint;
                    actor.chefState = ChefState.GoToFurniture;
                }
            }
        }
    }

    public void addNewRecipe(Recipe recipe)
    {
        recipeStepIndex = 0;
        recipeToDo = recipe;
        currentStep = recipeToDo.recipeSteps[0];

        furnitureToInteractWith = findFurniture(currentStep.workbenchUsed, currentStep.ingredientOutput);
        if (furnitureToInteractWith == null)
        {
            actor.chefState = ChefState.Waiting;
        }
        else
        {
            targetPoint = furnitureToInteractWith.accessPoint;
            furnitureToInteractWith.isUsed = true;
            destinationSetter.target = targetPoint;
            actor.chefState = ChefState.GoToFurniture;
        }
    }

    public void doNextRecipeStep()
    {
        currentStep = recipeToDo.recipeSteps[recipeStepIndex];

        //Find furniture to use for the next step of a recipe
        furnitureToInteractWith = findFurniture(currentStep.workbenchUsed);
        if (furnitureToInteractWith == null)
        {
            actor.chefState = ChefState.Waiting;
        }
        else
        {
            targetPoint = furnitureToInteractWith.accessPoint;
            furnitureToInteractWith.isUsed = true;
            destinationSetter.target = targetPoint;
            actor.chefState = ChefState.GoToFurniture;
        }
    }

    public void deliverMeal()
    {
        furnitureToInteractWith = findFurniture(FurnitureType.FoodDisplayer);
        if (furnitureToInteractWith == null) //FoodDisplayer is full or used
        {
            actor.chefState = ChefState.Waiting;
        }
        else
        {
            targetPoint = furnitureToInteractWith.accessPoint;
            furnitureToInteractWith.isUsed = true;
            destinationSetter.target = targetPoint;
            actor.chefState = ChefState.Deliver;
        }
    }

    //Find the furniture required to do the current recipe's step, if this step require to find an ingredient, it will be a parameter
    public Furniture findFurniture(FurnitureType furnitureType, Ingredient ingredient = null)
    {
        Furniture[] surrondingFurnitures = FindObjectsOfType<Furniture>();
        for (int i = 0; i < surrondingFurnitures.Length; i++)
        {
            if (surrondingFurnitures[i].furnitureType == furnitureType && !surrondingFurnitures[i].isUsed)
            {
                //Case : Find delivering chariot 
                if (surrondingFurnitures[i].furnitureType == FurnitureType.FoodDisplayer)
                {
                    FoodDisplayer chariot = surrondingFurnitures[i] as FoodDisplayer;
                    if (chariot.mealsToServe.Count < chariot.capacity)
                    {
                        return surrondingFurnitures[i];
                    }
                }
                //Case : Find a workbench
                else if (ingredient == null)
                {
                    return surrondingFurnitures[i];
                }
                //Case : Find a frige with the right ingredient
                else if((surrondingFurnitures[i] as Fridge).ingredient == ingredient)
                {
                    return surrondingFurnitures[i];
                }
            }
        }
        return null;
    }
}
