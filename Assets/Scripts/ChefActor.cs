using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChefState { Idle, Working, GoToFurniture, Waiting, Deliver }
public class ChefActor : Actor
{
    public ChefState chefState;
    public bool isFailling;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<AgentBehavior>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        gameManager.actorSelecter = this;
    }
}
