using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlock : MonoBehaviour
{
    public GameObject explosionEffectPrefab;
    public GameObject powerUpPrefab;
    public float powerUpChance = 0.3f;
    public string onCollideTag = "Explosion";

    public void Explode()
    {
        if (explosionEffectPrefab != null) {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }
        if (powerUpPrefab != null && Random.value < powerUpChance) {
            Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(onCollideTag)) {
            Explode();
        }
    }
}
