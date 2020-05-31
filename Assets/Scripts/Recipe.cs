using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe")]
public class Recipe : ScriptableObject
{
    public RecipeSteps[] recipeSteps;
    public Ingredient[] ingredients; //All ingredients required to make this recipe
    public Sprite mealSprite;
}
