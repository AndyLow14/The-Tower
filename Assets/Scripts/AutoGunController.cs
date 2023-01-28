using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGunController : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;
    public BulletController bullet;
    public string enemyTag = "Enemy";
    public Canvas healthBar;

    [Header("Gun Specs")]
    public float bulletSpeed = 20;
    public float timeBetweenShots = 0.1f;
    public float bulletSpread = 5;

    [Header("Shooting Specs")]
    public float range = 15f;
    public float rotateSpeed = 1;

    // Self variables
    private Transform target;
    private float shotCounter;
    public bool isFiring;

    // Start is called before the first frame update
    void Start()
    {
        // Search for the nearest target and repeat every .5s or scan twice every second
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void LateUpdate()
    {
        healthBar.transform.LookAt(Camera.main.transform);
    }

    void UpdateTarget()
    {
        // Create an array storing all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // Calculate the distance of all enemies from the turret and find the closest enemy
        foreach (GameObject enemy in enemies)
        {
            Ray ray = new Ray(enemy.transform.position, enemy.transform.forward);
            // Fire a ray to detect for obstacles between the enemy and turret
            if (Physics.Raycast(ray, out var laserHitObject, range))
            {
                if (!laserHitObject.collider.CompareTag("Tower"))
                {
                    continue;
                }

            }
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            isFiring = false;
            return;
        }
        else
        {
            // Rotates towards the closest target smoothly
            Vector3 targetDir = target.position - transform.position;

            float step = rotateSpeed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);

            transform.rotation = Quaternion.LookRotation(newDir);

            isFiring = true;
        }

        if (isFiring)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;

                // Adds random bullet spread
                Vector3 oriAngle = firePoint.eulerAngles;

                // Create a new random offset angle
                Vector3 offsetAngle = new Vector3(oriAngle.x + Random.Range(-bulletSpread, bulletSpread), oriAngle.y + Random.Range(-bulletSpread, bulletSpread), oriAngle.z);
                firePoint.eulerAngles = offsetAngle;

                // Instantiate as a BulletController object so this bullet would have all properties of BulletController
                BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
                newBullet.speed = bulletSpeed;

                //Reset back to the original angle
                firePoint.eulerAngles = oriAngle;
            }
        }
        else
        {
            shotCounter = 0;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
