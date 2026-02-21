using UnityEngine;

public class Enemy_Respawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] respawnPoints;
    [SerializeField] private float cooldown=2f;
    [Space]
    [SerializeField]private float cooldownDecreaseRate =.05F;
    [SerializeField] private float minimumCooldown = .7f;

    [Header("Spawn limit")]
    [SerializeField] private int maxEnemiesToSpawn = 10;

    private float timer;

    private Transform player;

     private int spawnedCount = 0;

    private void Awake()
    {
        player = FindFirstObjectByType<Player>().transform;
    }

    private bool stopSpawing;

    public void StopSpawning()
    {
        stopSpawing = true;
    }

    private void Update()
    {
        if (stopSpawing)return;

        if (spawnedCount >= maxEnemiesToSpawn)
            return;
        
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = cooldown;
            CreateNewEnemy();
            spawnedCount++;
            cooldown=Mathf.Max(cooldown - cooldownDecreaseRate, minimumCooldown);
        }
    }

    private void CreateNewEnemy()
    {
        int respawnPointIndex = Random.Range(0, respawnPoints.Length);
        Vector3 spawnPoint = respawnPoints[respawnPointIndex].position;

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);

        bool createdOnTheRight = newEnemy.transform.position.x > player.transform.position.x;

        if(createdOnTheRight)
        {
            newEnemy.GetComponent<Entity>().Flip();
        }
    }
}
