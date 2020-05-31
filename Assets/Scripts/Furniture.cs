using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FurnitureType { DeepFryer, CuttingTable, SeasonTable, FryPan, Fridge, FoodDisplayer }
public class Furniture : MonoBehaviour
{
    public bool isUsed;
    public FurnitureType furnitureType;

    [Header("Associated objects :")]
    public Transform accessPoint;
    public AgentBehavior agentUsingFurniture;
}
