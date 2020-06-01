using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [Header("Actor caracteristics :")]
    public int id;
    public float stopDuration;
    public float stopTimer;
    [Header("Actor states :")]
    public bool mouseOver;
    public bool facingRight;
    public bool canMove;
    [Header("Associated objects :")]
    public Animator anim;
    public AgentBehavior agent;
    public GameManager gameManager;
    

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
        Orientate();
    }

    private void Move()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        agent.moveToDestination(worldPosition);
    }

    private void Orientate()
    {
        Vector3 velocity = agent.aIPath.desiredVelocity;
        Vector3 scale = transform.localScale;

        anim.SetFloat("Velocity", Mathf.Abs(velocity.x) + Mathf.Abs(velocity.y));
        if (velocity.x >= 0.01f && !facingRight)
        {
            facingRight = true;
            scale.x *= -1;
            transform.localScale = scale;
        }
        else if (velocity.x <= -0.01f && facingRight)
        {
            facingRight = false;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
