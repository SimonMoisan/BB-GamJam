using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChefState { Idle, Working, GoToFurniture, Waiting, Deliver }
public class ChefActor : Actor
{
    public ChefState chefState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<AgentBehavior>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetWorking(bool working)
    {
        anim.SetBool("Working", working);
    }

    public void SetHolding(bool holding)
    {
        anim.SetBool("Holding", holding);
    }

    public void SetDrag(bool drag)
    {
        anim.SetBool("Drag", drag);
    }
}
