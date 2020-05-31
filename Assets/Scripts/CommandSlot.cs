using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandSlot : MonoBehaviour
{
    public int commandSlotId;
    public Image mealIcon;
    public Image durationGauge;
    public GameLoop gameLoop;
    public Command command;

    public void Start()
    {
        gameLoop = FindObjectOfType<GameLoop>();
    }

    // Update is called once per frame
    void Update()
    {
        if(command != null)
        {
            durationGauge.fillAmount = command.actualTime / command.timeToPerform;
        }
    }

    public void attributeCommand()
    {

    }
}
