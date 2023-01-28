using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShellDrop : MonoBehaviour
{
    public float speed;
    public GameObject impactEffect;
    public float explosionRadius = 5;
    public float explosionPower = 10;

    public float lifeTime;
    public int damageToGive;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

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
        Destroy(effect, 3f);

        // Checks for enemies in the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider obj in colliders)
        {
            // Adds an explosive force to rigidbodies
            if (obj.TryGetComponent(out Rigidbody rigidbody))
            {
                Vector3 dir = (obj.transform.position - transform.position).normalized;
                rigidbody.AddForce(dir * explosionPower, ForceMode.Impulse);
            }


            if (obj.CompareTag("Enemy"))
            {
                obj.GetComponent<HealthController>().hurt(damageToGive);
            }
        }

        // Play explosion audio
        FindObjectOfType<AudioManager>().Play("MortarExplosion");

        // Destroy the bullet
        Destroy(gameObject);
    }
}

