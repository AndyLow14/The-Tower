using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShellFire : MonoBehaviour
{
    public float speed;
    public GameObject impactEffect;

    public float lifeTime;
    public int damageToGive;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        
        // Maximum lifetime of bullets
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

    }
}
