using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyController : MonoBehaviour
{
    [Header("References")]
    public GunController theTower;
    public Canvas healthBar;

    // For the enemies gun
    public Transform firePoint;
    public BulletController bullet;
    public AutoGunController enemyGun;


    [Header("Shooting Enemy Specs")]
    public float moveSpeed;

    // Self variables
    private Rigidbody enemyBody;
    private CapsuleCollider enemyBox;
    private HealthController hc;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody>();
        enemyBox = GetComponent<CapsuleCollider>();
        theTower = FindObjectOfType<GunController>();
        hc = GetComponent<HealthController>();
        healthBar.enabled = false;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void LateUpdate()
    {
        if (hc.isHurt) healthBar.transform.LookAt(Camera.main.transform);
    }

    void FixedUpdate()
    {
        // Only start moving forward if the enemy is grounded
        if (IsGrounded() && !enemyGun.isFiring) enemyBody.velocity = (transform.forward * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // Only show the healthBar when enemies have been hurt once
        if (hc.isHurt)
        {
            healthBar.enabled = true;
        }
        // Stop the enemy when the gun is firing
        Vector3 pointToLook = theTower.transform.position;
        transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

    }

    // To check if enemy is grounded fire a raycast downwards to the plane
    private bool IsGrounded()
    {
        return Physics.Raycast(enemyBox.bounds.center, Vector3.down, enemyBox.bounds.extents.y + .01f);
    }

}
