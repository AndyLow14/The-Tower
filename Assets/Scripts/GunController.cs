using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    [Header("References")]
    public BulletController bullet;
    public Transform firePoint;
    public LineRenderer laser;
    public Canvas healthBar;

    [Header("Gun Specs")]
    public float bulletSpeed = 20;
    public float timeBetweenShots = 0.1f;

    // Self variables
    private float shotCounter;
    private Rigidbody gunBody;
    private Camera mainCamera;
    private bool isFiring;

    // Start is called before the first frame update
    void Start()
    {
        gunBody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
    }

    void LateUpdate()
    {
        healthBar.transform.LookAt(Camera.main.transform);
    }

    // Update is called once per frame
    void Update()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out var hitInfo, Mathf.Infinity))
        {

            Vector3 pointToLook = cameraRay.GetPoint(hitInfo.distance);
            //Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            // Check if something is blocking the laser / bullet path
            Vector3 bias = new Vector3(.05f, .05f, 0);
            laser.SetPosition(0, firePoint.position + bias);

            Ray laserRay = new Ray(firePoint.position + bias, firePoint.forward);
            if (Physics.Raycast(laserRay, out var laserHitObject, Mathf.Infinity))
            {
                laser.SetPosition(1, laserHitObject.point + bias);
            }
            else
            {
                laser.SetPosition(1, pointToLook + bias);
            }

            // Draw a line between the muzzle and mouse pointer



            transform.LookAt(pointToLook);
        }

        if (isFiring)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;

                // Instantiate as a BulletController object so this bullet would have all properties of BulletController
                BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
                newBullet.speed = bulletSpeed;
            }
        }
        else
        {
            shotCounter = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isFiring = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isFiring = false;
        }

    }
}
