using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    AgentBehavior agent;
    bool mouseOver;

    Animator anim;
    bool facingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<AgentBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = agent.aIPath.desiredVelocity;
        Vector3 scale = transform.localScale;

        anim.SetFloat("Velocity", Mathf.Abs(velocity.x) + Mathf.Abs(velocity.y));
        if (velocity.x >= 0.01f && !facingRight)
        {
            facingRight = true;
            scale.x *= -1;
            transform.localScale = scale;
        } else if (velocity.x <= -0.01f && facingRight)
        {
            facingRight = false;
            scale.x *= -1;
            transform.localScale = scale;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            agent.moveToDestination(worldPosition);
        }
        /*
        if (GameManager.canAct && mouseOver && Input.GetMouseButton(0))
        {
            GameManager.canAct = false;
            ChooseAction();
        }
        */
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }

    void ChooseAction()
    {
        //Show Menu
        
    }
}
