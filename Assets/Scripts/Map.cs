using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int numberOfPlayers = 2;
    public GameObject destructibleBlockPrefab;
    public GameObject indestructibleBlockPrefab;
    public int mapSize = 10;
    public float destructibleDensity = 0.5f;
    public Vector3[] spawnPoints;

    void Start()
    {
        GenerateMap();
        PlaceSpawnPoints();
        // BombBehaviour.InstantiateBomb(GetSpawnPoint(0), 8);
        PlayerBehaviour.InstantiatePlayer(GetSpawnPoint(0));
        PlayerBehaviour.InstantiatePlayer(GetSpawnPoint(1), false);
        // PlayerBehaviour.InstantiatePlayer(GetSpawnPoint(1));
    }

    void GenerateMap()
    {
        mapSize += mapSize % 2 == 0 ? 1 : 0;
        for (int x = -1; x <= mapSize; x++)
        {
            for (int z = -1; z <= mapSize; z++)
            {
                bool isBorder = x == -1 || z == -1 || x == mapSize || z == mapSize;
                bool isSpawnCorner = (x < 3 && z < 3) || (x < 3 && z > mapSize - 3) || (x > mapSize - 3 && z < 3) || (x > mapSize - 3 && z > mapSize - 3);
                if (isBorder || (x % 2 == 1 && z % 2 == 1))
                {
                    Instantiate(indestructibleBlockPrefab, new Vector3(x, 0, z), Quaternion.identity);
                }
                else if (!isSpawnCorner && Random.value < destructibleDensity)
                {
                    Instantiate(destructibleBlockPrefab, new Vector3(x, 0, z), Quaternion.identity);
                }
            }
        }
    }

    void PlaceSpawnPoints()
    {
        numberOfPlayers = Mathf.Clamp(numberOfPlayers, 2, 4);
        spawnPoints = new Vector3[numberOfPlayers];

        spawnPoints[0] = new Vector3(0, 0, 0); // bottom left
        spawnPoints[1] = new Vector3(0, 0, mapSize - 1); // top left
        if (numberOfPlayers > 2)
        {
            spawnPoints[2] = new Vector3(mapSize - 1, 0, 0); // bottom right
        }
        if (numberOfPlayers > 3)
        {
            spawnPoints[3] = new Vector3(mapSize - 1, 0, mapSize - 1); // top right
        }
    }

    public Vector3 GetSpawnPoint(int playerIndex)
    {
        if (playerIndex >= 0 && playerIndex < spawnPoints.Length)
        {
            return spawnPoints[playerIndex];
        }
        else
        {
            return Vector3.zero;
        }
    }
}
