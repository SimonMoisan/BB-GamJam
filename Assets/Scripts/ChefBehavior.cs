using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChefBehavior : AgentBehavior
{
    [Header("Agents stats :")]
    [Range(0, 99)] public int failPercentage; //percentage chance to do the wrong thing
    public float workingDuration;
    public float workingTimer;
    public bool isFailling;
    public int moodMax;
    public int actualMood;

    [Header("Associated objects chef :")]
    public ChefActor actor;
    public Recipe recipeToDo;
    public RecipeSteps currentStep;
    public int recipeStepIndex;
    public Furniture furnitureToInteractWith;
    public GameManager gameManager;
    public Image selector;

    private void Start()
    {
        actualMood = moodMax;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        //Go to a furniture
        if(actor.chefState == ChefState.GoToFurniture && actor.canMove)
        {
            destinationSetter.target = targetPoint;
            if (transform.position == targetPoint.position) //Is in front of the furniture
            {
                
                //Set up workbench gauge and icon
                if(furnitureToInteractWith as Workbench != null)
                {
                    (furnitureToInteractWith as Workbench).iconOutputIngredient.sprite = currentStep.ingredientOutput.icon;
                    (furnitureToInteractWith as Workbench).iconOutputIngredient.enabled = true;
                    (furnitureToInteractWith as Workbench).progressionGauge.enabled = true;
                }

                //See if the work (only for workbench and for step with potential fail) will success or fail
                if(furnitureToInteractWith is Workbench && currentStep.wrongIngrdientOutput.Length > 0)
                {
                    int failRatio = Random.Range(0, 100);
                    if (failRatio < failPercentage)
                    {
                        isFailling = true;
                        //Set up timer
                        workingDuration = currentStep.wrongDuration;
                        workingTimer = workingDuration;
                    }
                    else
                    {
                        isFailling = false;
                        //Set up timer
                        workingDuration = currentStep.duration;
                        workingTimer = workingDuration;
                    }
                }

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
                furnitureToInteractWith.agentUsingFurniture = null;

                //Reset timer and duration
                workingDuration = 0;
                workingTimer = 0;
                actor.SetHolding(true);
                //Get new ingredient at the end of this step (fail or succes)
                if(isFailling)
                {
                    carriedIngredient = currentStep.wrongIngrdientOutput[0];
                }
                else
                {
                    carriedIngredient = currentStep.ingredientOutput;
                }
                
                ingredientIcon.sprite = carriedIngredient.icon;
                ingredientIcon.enabled = true;
                gaugeIcon.enabled = true;

                //Reset workbench gauge and icon
                if ((furnitureToInteractWith as Workbench) != null)
                {
                    (furnitureToInteractWith as Workbench).iconOutputIngredient.sprite = null;
                    (furnitureToInteractWith as Workbench).iconOutputIngredient.enabled = false;
                    (furnitureToInteractWith as Workbench).progressionGauge.enabled = false;
                }

                //Go to next step of the recipe
                if (carriedIngredient is Meal)
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
            if (transform.position == targetPoint.position) //Is in front of the furniture
            {
                FoodDisplayer chariot = (furnitureToInteractWith as FoodDisplayer);
                chariot.mealsToServe.Add((carriedIngredient as Meal));
                chariot.isUsed = false;
                //Remove carried ingredient
                carriedIngredient = null;
                ingredientIcon.sprite = null;
                ingredientIcon.enabled = false;
                gaugeIcon.enabled = false;
                actor.SetHolding(false);
                
                //Reset recipe and recipe step
                recipeToDo = null;
                recipeStepIndex = 0;
                currentStep = null;
                
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
                    actor.chefState = ChefState.GoToFurniture;
                }
            }
        }

        //Select agent
        if(Input.GetMouseButtonDown(0))
        {
            gameManager.selectCooker(this);
        }

        //Drag agent
        if(Input.GetMouseButtonDown(1))
        {

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
            furnitureToInteractWith.agentUsingFurniture = this;
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
            furnitureToInteractWith.agentUsingFurniture = this;
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
            furnitureToInteractWith.agentUsingFurniture = this;
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
                else if ((surrondingFurnitures[i] as Workbench) != null && ingredient == null)
                {
                    return surrondingFurnitures[i];
                }
                //Case : Find a frige with the right ingredient
                else if(surrondingFurnitures[i].furnitureType == FurnitureType.Fridge && (surrondingFurnitures[i] as Fridge).ingredient == ingredient)
                {
                    return surrondingFurnitures[i];
                }
            }
        }
        return null;
    }

    //Reverse behavior
    public void correctBehavior()
    {
        isFailling = !isFailling;
    }
}
