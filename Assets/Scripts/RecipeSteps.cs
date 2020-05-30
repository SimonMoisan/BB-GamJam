using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeSteps : ScriptableObject
{
    //Ingredients required to make this step
    public Ingredient[] ingredientsInput;

    //Can be an ingredient or a meal if its the final step
    public Ingredient ingredientOutput;
    public Meal meal;

    public float duration;
    public WorkbenchType workbenchUsed;
}
