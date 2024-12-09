using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject groundPrefab;
    public GameObject treePrefab;
    public GameObject enemyPrefab;
    public float tileSpacing = 1.1f;

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                // Randomly decide to place a wall or floor
                GameObject toInstantiate = Random.Range(0, 10) < 2 ? treePrefab : groundPrefab;

                // Occasionally place an enemy
                if (Random.Range(0, 20) == 0)
                {
                    toInstantiate = enemyPrefab;
                }

                // Instantiate the object
                Vector3 position = new Vector3(x * tileSpacing, 0, z * tileSpacing);
                Instantiate(toInstantiate, position, Quaternion.identity);
            }
        }
    }
} 