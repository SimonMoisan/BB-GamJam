using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{

    bool mouseOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.canAct && mouseOver && Input.GetMouseButton(0))
        {
            GameManager.canAct = false;
            ChooseAction();
        }
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
