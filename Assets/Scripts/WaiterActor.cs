using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WaiterActor : Actor
{

    [Header("Path")]
    public GameObject pathA;
    public GameObject pathB;
    AIPath aIPath;
    [SerializeField]
    bool processingPath = false;
    public GameObject pathChoosen;
    public int index;

    [Header("Entry")]
    public float speedApparition = 1f;
    public Transform startWaiter;
    [SerializeField]
    bool visible = false;
    SpriteRenderer sprite;
    Color color;

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<AgentBehavior>();
        gameManager = FindObjectOfType<GameManager>();
        aIPath = GetComponent<AIPath>();
        sprite = GetComponent<SpriteRenderer>();
        color = sprite.color;
        sprite.color = new Color(0, 0, 0, 0); //Hidding the character
    }

    // Update is called once per frame
    void Update()
    {
        /************** Temporary **************/
        if (Input.GetKey(KeyCode.Space) && !processingPath)
        {
            CallWaiter();
        }
        /***************************************/
        if (processingPath && aIPath.reachedDestination)
        {
            index++;
            if (index < pathChoosen.transform.childCount)
            {
                Vector3 target = pathChoosen.transform.GetChild(index).position;
                MoveToTarget(target);
            }
            else
            {
                processingPath = false;
                Debug.Log("You have arrived");
                StartCoroutine("Disappear");
            }
        }
    }

    public void CallWaiter()
    {
        visible = true;
        processingPath = true;
        index = 0;
        StartCoroutine("Appear");
        pathChoosen = (Random.Range(0, 2) == 0) ? pathA : pathB;
        Vector3 target = pathChoosen.transform.GetChild(index).position;
        MoveToTarget(target);
    }

    private void MoveToTarget(Vector3 target)
    {
        if (target != null)
        {
            agent.moveToDestination(target);
        }
        else
        {
            Debug.LogWarning("Child not found in path object");
        }
    }

    private IEnumerator Appear()
    {
        transform.position = startWaiter.position;
        Color temp = color;
        temp.a = 0;
        while (sprite.color.a < 1)
        {
            temp.a += speedApparition;
            sprite.color = temp;
            yield return null;
        }
        temp.a = 1;
        sprite.color = temp;
    }

    private IEnumerator Disappear()
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



/*
private float Dst(Vector3 a, Vector3 b)
  {
      return Mathf.Pow(b.x - a.x, 2) + Mathf.Pow(b.y - a.y, 2);
  }
private void DrawPath()
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

private IEnumerator MoveEffectAlongPath(Vector3 startPosition)
{
    pathEffect.transform.position = startPosition;
    for (int i = 0; i < path.Count; i++)
    {
        float distance;
        while ((distance = Dst(pathEffect.transform.position, path[i])) > 0.1f * 0.1f)
        {
            Vector3 direction = new Vector3(path[i].x - pathEffect.transform.position.x, path[i].y - pathEffect.transform.position.y, 0);
            Vector3 velocity = direction * speedEffect * Time.deltaTime;
            pathEffect.transform.Translate(velocity, Space.World);
            yield return null;
        }
    }
    drawingPath = false;
    pathEffect.SetActive(false);
}
*/
