using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    [Header("References")]
    public GameObject shatter;
    public Image healthBar;

    [Header("Settings")]
    public float maxHealth;
    public float regen = 0;
    // Enemy Death sound or Explosion sound
    public string explosionSound;

    // Self Variables
    private float currentHealth;
    public bool isHurt = false;
    public bool isPlayer = false;

    void Start()
    {
        currentHealth = maxHealth;
        if (regen != 0) InvokeRepeating("Regenerate", 0.0f, 1f / regen);
    }

    // Function to regenerate health
    void Regenerate()
    {
        if (currentHealth < maxHealth) currentHealth++;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            GameObject effect = Instantiate(shatter, transform.position, transform.rotation);

            // Play death audio
            FindObjectOfType<AudioManager>().Play(explosionSound);

            Destroy(effect, 5f);

            if (isPlayer)
            {
                SceneManager.LoadScene(2);
            }
        }
        // Update Healthbar every frame when regeneration is active
        if (regen != 0) healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void hurt(int damage)
    {
        isHurt = true;
        currentHealth -= damage;
        // Only update Healthbar when damaged to save resources
        if (regen == 0) healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void regenLevelUp()
    {
        regen += 5;
    }

    public void healthLevelUp()
    {
        maxHealth += 50;
        currentHealth = maxHealth;
    }
}
