using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarController : MonoBehaviour
{

    [Header("References")]
    public MortarShellFire fireShell;
    public MortarShellDrop dropShell;
    public Transform firePoint;

    [Header("Mortar Specs")]
    public float shellSpeed = 20;
    public float timeBetweenShots = 0.5f;

    // Self variables
    private float shotCounter;
    private Camera mainCamera;
    private bool isFiring;
    private int delay = 1;
    private Vector3 dropFromSky;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }

    IEnumerator dropDelay()
    {
        yield return new WaitForSeconds(delay);
        MortarShellDrop newDropShell = Instantiate(dropShell, dropFromSky, firePoint.rotation) as MortarShellDrop;
    }

    // Update is called once per frame
    void Update()
    {

        if (isFiring)
        {
            // Fire the shell straight up and out of screen
            MortarShellFire newFireShell = Instantiate(fireShell, firePoint.position, firePoint.rotation) as MortarShellFire;
            newFireShell.speed = shellSpeed;
            isFiring = false;

            // Determine where to drop the shell after the delay
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(cameraRay, out var hitInfo, Mathf.Infinity))
            {
                Vector3 pointToDrop = cameraRay.GetPoint(hitInfo.distance);
                dropFromSky = new Vector3(pointToDrop.x, pointToDrop.y + 20, pointToDrop.z);
                StartCoroutine(dropDelay());
            }


        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            isFiring = true;
        }
    }
}
