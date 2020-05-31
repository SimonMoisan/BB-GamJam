using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    public float interactionTime;
    public bool isUsed;

    [Header("Associated objects :")]
    public Transform interactionPoint;
    public AgentBehavior agentUsingFurniture;
}
