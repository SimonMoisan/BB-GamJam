using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorkbenchType{ }
public class Workbench : MonoBehaviour
{
    public int numberInputSlots;
    public WorkbenchType type;

    [Header("Associated Object")]
    public Ingredient[] ingredientInputSlots;
    public Ingredient ingredientOutput;


}
