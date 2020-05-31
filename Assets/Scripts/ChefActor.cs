using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChefState { Idle, Working, GoToFurniture }
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        gameManager.actorSelecter = this;
    }
}
