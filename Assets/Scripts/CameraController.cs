using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement configuration")]
    [SerializeField] public float panSpeed = 30f;
    [SerializeField] public float panBoarderThickness = 10f;
    [SerializeField] public float scrollSpeed = 2f;
    [SerializeField] public float minX = -80f;
    [SerializeField] public float maxX = 40f;
    [SerializeField] public float minZ = 0f;
    [SerializeField] public float maxZ = 80f;

    [Header("Zoom configuration")]
    [SerializeField] public float minZoom = 10f;
    [SerializeField] public float maxZoom = 80f;

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
            if ((Input.GetKey("z") || Input.mousePosition.y >= Screen.height - panBoarderThickness) && transform.position.z < maxZ)
            {
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
            }

            if ((Input.GetKey("s") || Input.mousePosition.y <= panBoarderThickness) && transform.position.z >= minZ)
            {
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
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
        pos.y -= scroll * scrollSpeed * Time.deltaTime * 1000;
        pos.y = Mathf.Clamp(pos.y, minZoom, maxZoom);
        transform.position = pos;
    }
}