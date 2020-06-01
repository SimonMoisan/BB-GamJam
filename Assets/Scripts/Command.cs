using UnityEngine;
using UnityEngine.UI;

public class Command : MonoBehaviour
{
    public int difficulty;
    public float timeToPerform;
    public float actualTime;
    public Recipe recipe;
    public CommandSlot commandSlot;
    public GameLoop gameLoop;

    private void Awake()
    {
        gameLoop = FindObjectOfType<GameLoop>();
        actualTime = timeToPerform;
    }

    public void Update()
    {
        if (actualTime <= 0)
        {
            commandExpired();
        }
        else
        {
            actualTime -= Time.deltaTime;
        }
    }

    public void commandExpired()
    {
        gameLoop.actualCommands.Remove(this);
        gameLoop.commandsFailed++;

        //Reset command slot
        commandSlot.selector.enabled = false;
        commandSlot.mealIcon.sprite = null;
        commandSlot.mealIcon.enabled = false;
        commandSlot.durationGauge.enabled = false;

        Destroy(gameObject);
    }
}
