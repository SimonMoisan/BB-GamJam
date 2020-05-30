using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AgentBehavior : MonoBehaviour
{
    public AIPath aIPath;
    public AIDestinationSetter destinationSetter;
    public Transform targetPoint;

    public void OnValidate()
    {
        aIPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
    }

    public void moveToDestination(Vector3 worldPosition)
    {
        targetPoint.transform.position = worldPosition;
        destinationSetter.target = targetPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(worldPosition);
            targetPoint.transform.position = worldPosition;
            destinationSetter.target = targetPoint;
        }
    }
}
