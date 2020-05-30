using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe step")]
public class RecipeSteps : ScriptableObject
{
    //Ingredients required to make this step
    public Ingredient[] ingredientsInput;

    //Can be an ingredient
    public Ingredient ingredientOutput;

    public WorkbenchType workbenchUsed;
}
