using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement configuration")]
    [SerializeField] public float panSpeed = 30f;
    [SerializeField] public float panBoarderThickness = 10f;
    [SerializeField] public float scrollSpeed = 2f;
    [SerializeField] public float minX = -80f;
    [SerializeField] public float maxX = 40f;
    [SerializeField] public float minY = 0f;
    [SerializeField] public float maxY = 80f;


    private bool doMovement = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        if (!doMovement)
        {
            return;
        }

        if (doMovement)
        {
            if ((Input.GetKey("z") || Input.mousePosition.y >= Screen.height - panBoarderThickness) && transform.position.y < maxY)
            {
                transform.Translate(Vector3.up * panSpeed * Time.deltaTime, Space.World);
            }

            if ((Input.GetKey("s") || Input.mousePosition.y <= panBoarderThickness) && transform.position.y >= minY)
            {
                transform.Translate(Vector3.down * panSpeed * Time.deltaTime, Space.World);
            }

            if ((Input.GetKey("q") || Input.mousePosition.x <= panBoarderThickness) && transform.position.x >= minX)
            {
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            }

            if ((Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBoarderThickness) && transform.position.x < maxX)
            {
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;
        transform.position = pos;
    }
}