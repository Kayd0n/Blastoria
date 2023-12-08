using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlock : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float powerUpChance = 0.3f;

    public void Explode()
    {
        if (powerUpPrefab != null && Random.value < powerUpChance) {
            Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
