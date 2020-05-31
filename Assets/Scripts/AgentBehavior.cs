using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AgentBehavior : MonoBehaviour
{
    [Header("Associated objects :")]
    public AIPath aIPath;
    public AIDestinationSetter destinationSetter;
    public Transform targetPoint;
    public Furniture interactWithFurniture;
    public ParticleSystem visualPath;

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

    private void Update()
    {
        DrawPath();
    }

    void DrawPath()
    {
        List<Vector3> buffer = new List<Vector3>();
        bool stale;
        aIPath.GetRemainingPath(buffer, out stale);
        if (!stale)
        {
            List<Vector3> waypoints = new List<Vector3>();
            Vector2 directionOld = Vector2.zero;

            for (int i = 1; i < buffer.Count; i++) //Simplify the path
            {
                Vector2 directionNew = new Vector2(buffer[i - 1].x - buffer[i].x, buffer[i - 1].y - buffer[i].y);
                if (directionNew != directionOld)
                {
                    waypoints.Add(buffer[i - 1]);
                    directionOld = directionNew;
                }
            }

            foreach(Vector3 waypoint in waypoints)
            {
                //Draw something to show pass
            }

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
    }
}
