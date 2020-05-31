using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Waiter : MonoBehaviour
{
    AIPath aIPath;
    public GameObject pathEffect;
    public float speedEffect = 5f;
    public float speedApparition = 1f;
    public Transform startWaiter;
    bool visible = false;
    bool drawingPath = false;
    SpriteRenderer sprite;
    Color color;

    List<Vector3> path;

    private void Start()
    {
        aIPath = GetComponent<AIPath>();
        sprite = GetComponent<SpriteRenderer>();
        color = sprite.color;
        sprite.color = new Color(0,0,0,0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine("Appear");
            visible = true;
        }
        if (visible)
        {
            if (!drawingPath)
            {
                DrawPath();
            }
        }
    }

    public void CallWaiter()
    {
        transform.position = startWaiter.position;
        Appear();
    }

    void DrawPath()
    {
        List<Vector3> buffer = new List<Vector3>();
        aIPath.GetRemainingPath(buffer, out bool stale);
        if (!stale)
        {
            drawingPath = true;
            pathEffect.SetActive(true);
            path = new List<Vector3>();
            Vector2 directionOld = Vector2.zero;

            for (int i = 1; i < buffer.Count; i++) //Simplify the path
            {
                Vector2 directionNew = new Vector2(buffer[i - 1].x - buffer[i].x, buffer[i - 1].y - buffer[i].y);
                if (directionNew != directionOld)
                {
                    path.Add(buffer[i - 1]);
                    directionOld = directionNew;
                }
            }
            path.Add(aIPath.destination);
            StartCoroutine("MoveEffectAlongPath", transform.position);

        }
    }

    IEnumerator MoveEffectAlongPath(Vector3 startPosition)
    {
        pathEffect.transform.position = startPosition;
        for(int i = 0; i < path.Count; i++)
        {
            float distance;
            while ((distance = Dst(pathEffect.transform.position, path[i])) > 0.1f)
            {
                Vector3 direction = new Vector3(path[i].x - pathEffect.transform.position.x, path[i].y - pathEffect.transform.position.y, 0);
                Vector3 velocity = direction * speedEffect * Time.deltaTime;
                Debug.Log("Position : " + pathEffect.transform.position + "Vecteur : " + direction + 
                          " Velocity : " + velocity + " Distance : " + distance + " Target : " + path[i] + " Index : " + i);
                pathEffect.transform.Translate(velocity, Space.World);
                yield return null;
            }
        }
        drawingPath = false;
        pathEffect.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if (path != null)
        {
            foreach (Vector3 waypoints in path)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(waypoints, Vector3.one / 4f);
            }
        }

    }

    private float Dst(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt(Mathf.Pow(b.x - a.x, 2) + Mathf.Pow(b.y - a.y, 2));
    }

    IEnumerator Appear()
    {
        transform.position = startWaiter.position;
        Color temp = color;
        temp.a = 0;
        while(sprite.color.a < 1)
        {
            temp.a += speedApparition;
            sprite.color = temp;
            yield return null;
        }
        temp.a = 1;
        sprite.color = temp;
    }

    IEnumerator Disappear()
    {
        Color temp = color;
        temp.a = 1;
        while (sprite.color.a > 0)
        {
            temp.a -= speedApparition;
            sprite.color = temp;
            yield return null;
        }
        temp.a = 0;
        sprite.color = temp;
    }

}
