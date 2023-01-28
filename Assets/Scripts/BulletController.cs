using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("References")]
    public GameObject impactEffect;

    [Header("Bullet Stats")]
    public float speed;
    public int damageToGive;

    [Header("Settings")]
    public float lifeTime;
    public string enemyToHit;
    public string gunType = "Normal";

    void Start()
    {
        if (gunType == "Sniper")
        {
            FindObjectOfType<AudioManager>().Play("Sniper");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Maximum lifetime of bullets
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter(Collision other)
    {
        // Destroy the effect after 2 seconds
        GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);

        // If there is collision between the bullet and enemy
        if (other.gameObject.CompareTag(enemyToHit))
        {
            // Different bullet types given to towers and enemies cannot friendly fire
            if (enemyToHit == "Enemy")
            {
                other.gameObject.GetComponent<HealthController>().hurt(damageToGive);
            }
            else if (enemyToHit == "Tower")
            {
                other.gameObject.GetComponentInParent<HealthController>().hurt(damageToGive);
            }
        }

        // Destroy the bullet
        Destroy(gameObject);
    }
}
