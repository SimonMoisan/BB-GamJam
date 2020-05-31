using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe step")]
public class RecipeSteps : ScriptableObject
{
    //Ingredients required to make this step
    public Ingredient[] ingredientsInput;

    //Alternate ingredient if action failling;
    public Ingredient[] wrongIngrdientOutput;

    //Can be an ingredient or a meal
    public Ingredient ingredientOutput;

    public FurnitureType workbenchUsed;
}
