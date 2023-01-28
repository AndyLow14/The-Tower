using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject meleeEnemy;
    public GameObject shootingEnemy;
    public GameObject giantMeleeEnemy;
    public GameObject shopManager;
    public Text waveCountdownText;
    public Text waveNumberText;

    [Header("Settings")]
    public float countdown = 5f;
    public float timeBetweenWaves = 10f;
    public float radius = 5;
    public float minRange;
    public float maxRange;

    // Self Variables
    private int waveNumber = 1;

    void Update()
    {
        if (countdown <= 0)
        {
            StartCoroutine(spawnWave());
            // Time between waves increases as enemies increase
            waveNumberText.text = "Wave " + waveNumber.ToString();
            timeBetweenWaves++;
            countdown = timeBetweenWaves;

            // Add money for wave
            shopManager.GetComponent<ShopManager>().AddMoney(100);
        }
        countdown -= Time.deltaTime;
        waveCountdownText.text = countdown.ToString("F2");
    }

    IEnumerator spawnWave()
    {
        // Select Rally Point at a random x and z coordinate within minRange and maxRange from the tower
        float angle = Random.Range(0, 2f * Mathf.PI);
        float range = Random.Range(minRange, maxRange);

        float x = Mathf.Cos(angle) * range;
        float z = Mathf.Sin(angle) * range;

        Vector3 groupSpawnPoint = new Vector3(x, 25, z);

        for (int i = 0; i < waveNumber * 2; i++)
        {
            spawnEnemy(meleeEnemy, groupSpawnPoint);
            spawnEnemy(shootingEnemy, groupSpawnPoint);
            yield return new WaitForSeconds(.5f);
        }

        waveNumber++;

        // Every 5th round spawn a giant boss
        if (waveNumber % 5 == 0)
        {
            spawnEnemy(giantMeleeEnemy, groupSpawnPoint);
        }

        // Increase the radius of Rally Point to accomodate more enemies
        radius += 0.5f;
    }

    void spawnEnemy(GameObject enemy, Vector3 spawnPoint)
    {
        // Randomly spawn enemies in a YxY square around the Rally Point
        float randX = Random.Range(-1f * radius, 1f * radius);
        float randZ = Random.Range(-1f * radius, 1f * radius);

        Vector3 offset = new Vector3(randX, 0, randZ);

        Instantiate(enemy, spawnPoint + offset, Quaternion.identity);
    }

}
