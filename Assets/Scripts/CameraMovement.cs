using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Specs")]
    public LayerMask checkLayer;
    public float rotateAngle = 45f;
    public float zoomAmountRMB = .5f;
    public float movementStep = .5f;

    // Self Variables
    private Camera cam;
    private Vector3 origin = new Vector3(0, 0, 0);
    private Vector3 oriPosition;
    private float oriCamOrthographicSize;

    public void Start()
    {
        cam = GetComponent<Camera>();
        oriPosition = transform.position;
        oriCamOrthographicSize = cam.orthographicSize;
    }

    public void MoveHorizontal(bool left)
    {
        // Tells the camera to move left or right
        float dir = 1;

        if (!left) dir *= -1;

        transform.RotateAround(origin, Vector3.up, rotateAngle * dir * Time.deltaTime);
        oriPosition = transform.position;
    }

    public void Zoom()
    {
        transform.position = oriPosition;

        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            Ray ray = this.transform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            Vector3 desiredPosition;

            if (Physics.Raycast(ray, out hit, 1000, checkLayer))
            {
                desiredPosition = hit.point;
            }
            else
            {
                desiredPosition = transform.position;
            }

            float distance = Vector3.Distance(desiredPosition, transform.position);
            Vector3 direction = Vector3.Normalize(desiredPosition - transform.position) * (distance * zoomAmountRMB);

            transform.position += direction;
        }
    }

    void Update()
    {
        // Only allow camera orbiting and zooming one at a time
        if (Input.GetKey(KeyCode.A) && !Input.GetMouseButton(1))
        {
            MoveHorizontal(true);
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetMouseButton(1))
        {
            MoveHorizontal(false);
        }
        else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Zoom();
        }
    }
}
