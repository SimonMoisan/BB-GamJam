using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Workbench : Furniture
{
    public int numberInputSlots;

    [Header("Associated Object")]
    public Ingredient[] ingredientInputSlots;
    public Ingredient ingredientOutput;
    public Meal mealOutput;
}
