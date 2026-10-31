
using UnityEngine;

public class GolemSpawner : MonoBehaviour
{
    public GameObject golemPrefab;
    public Transform player;
    public GameMenuUI gameMenuUI;

    public float spawnInterval = 3f;
    public int maxGolems = 6;
    public float spawnDistance = 8f;

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (gameMenuUI == null || !gameMenuUI.IsPlaying)
            return;

        if (golemPrefab == null || player == null)
            return;

        if (Time.time < nextSpawnTime)
            return;

        nextSpawnTime = Time.time + spawnInterval;
        SpawnGolem();
    }

    void SpawnGolem()
    {
        GolemHealth[] golems =
            FindObjectsByType<GolemHealth>(
                FindObjectsSortMode.None
            );

        if (golems.Length >= maxGolems)
            return;

        for (int i = 0; i < 10; i++)
        {
            float x = Random.Range(-spawnDistance, spawnDistance);
            float z = Random.Range(-spawnDistance, spawnDistance);

            if (Random.Range(0, 2) == 0)
                x = Random.value < 0.5f
                    ? -spawnDistance : spawnDistance;
            else
                z = Random.value < 0.5f
                    ? -spawnDistance : spawnDistance;

            Vector3 spawnPosition = new Vector3(x, 1.25f, z);

            Vector3 direction = spawnPosition - player.position;
            direction.y = 0;

            if (direction.magnitude < 4f)
                continue;

            Instantiate(
                golemPrefab,
                spawnPosition,
                Quaternion.identity
            );

            break;
        }
    }
}
