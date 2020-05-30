using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : ScriptableObject
{
    public RecipeSteps[] recipeSteps;
    public Ingredient[] ingredients; //All ingredients required to make this recipe
}
