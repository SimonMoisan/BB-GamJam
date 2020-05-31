using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WaiterActor : Actor
{
    public AIPath aIPath;

    // Start is called before the first frame update
    void Start()
    {
        aIPath = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
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

            foreach (Vector3 waypoint in waypoints)
            {
                //Draw something to show pass
            }
        }
    }
}
