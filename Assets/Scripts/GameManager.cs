using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Actor actorSelecter;
    public Actor[] agents;
    public AgentBehavior[] waiters;

    private void Awake()
    {
        agents = FindObjectsOfType<Actor>();
    }

    private void Update()
    {
        
    }

    public void shootAction(int actorId)
    {
        actorSelecter = null;
    }

    public void slapeAction(int actorId)
    {
        actorSelecter = null;
    }

    public void stopAction(int actorId)
    {
        actorSelecter = null;
    }
}
