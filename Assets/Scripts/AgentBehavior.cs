using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class AgentBehavior : MonoBehaviour
{
    [Header("Associated objects :")]
    public AIPath aIPath;
    public AIDestinationSetter destinationSetter;
    public Transform targetPoint;
    public Ingredient carriedIngredient;
    public Image ingredientIcon;
    public Image gaugeIcon;

    public void OnValidate()
    {
        ingredientIcon.enabled = false;
        gaugeIcon.enabled = false;
        aIPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
    }

    public void moveToDestination(Vector3 worldPosition)
    {
        targetPoint.transform.position = worldPosition;
        destinationSetter.target = targetPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
    }
}
