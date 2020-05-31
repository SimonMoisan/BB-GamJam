using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MealQuality { Perfect, Failed }
public class Meal : Ingredient
{
    public MealQuality mealQuality;

    public Recipe recipe; //Actual recipe of this meal

    public List<Ingredient> ingredientsInMeal;

    public void Start()
    {
        mealQuality = MealQuality.Perfect;
    }

    //Function called to add an ingredient to the meal, if the ingredient is wrong, it will fail the recipe and the meal will be wasted
    public void addIngredient(Ingredient ingredientToAdd)
    {
        ingredientsInMeal.Add(ingredientToAdd);
        for (int i = 0; i < recipe.ingredients.Length; i++)
        {
            if(recipe.ingredients[i].name == ingredientToAdd.name) //Ingredient is in the recipe
            {
                return;
            }
        }
        mealQuality = MealQuality.Failed;
    }
}
