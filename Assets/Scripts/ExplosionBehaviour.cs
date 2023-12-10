using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExplosionBehaviour : MonoBehaviour
{
    private static GameObject explosionPrefab;

    private static GameObject InstantiateExplosion(Vector3 position)
    {
        if (explosionPrefab == null)
        {
#if UNITY_EDITOR
                explosionPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Explosion.prefab");
#else
            explosionPrefab = Resources.Load<GameObject>("Prefabs/Explosion");
#endif
            if (explosionPrefab == null)
            {
                Debug.LogError("Explosion prefab not found");
            }
        }

        Vector3 clipped = new Vector3(Mathf.RoundToInt(position.x), position.y, Mathf.RoundToInt(position.z));
        GameObject explosion = Instantiate(explosionPrefab, clipped, Quaternion.identity);

        ExplosionBehaviour behaviour = explosion.GetComponent<ExplosionBehaviour>();
        if (behaviour == null)
        {
            behaviour = explosion.AddComponent<ExplosionBehaviour>();
        }
        return explosion;
    }

    public static void ExplosionSpread(Vector3 position, Vector3 direction, int force)
    {
        ExplosionBehaviour.InstantiateExplosion(position);
        for (int i = 0; i < force; ++i)
        {
            RaycastHit hit;
            if (Physics.Raycast(position, direction, out hit, (i + 1)))
            {
                if (hit.collider.gameObject.CompareTag("indestructible_wall"))
                {
                    Debug.Log("indestructible_wall at " + hit.collider.gameObject.transform.position);
                    break;
                }
                if (hit.collider.gameObject.CompareTag("wall"))
                {
                    Debug.Log("wall at " + hit.collider.gameObject.transform.position);
                    DestructibleBlock destructibleBlock = hit.collider.gameObject.GetComponent<DestructibleBlock>();
                    if (destructibleBlock != null)
                    {
                        destructibleBlock.Explode();
                    }
                    break;
                }
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("player at " + hit.collider.gameObject.transform.position);
                    PlayerMove player = hit.collider.gameObject.GetComponent<PlayerMove>();
                    if (player != null)
                    {
                        player.Die(player.gameObject);
                    }
                    break;
                }
                Debug.Log("Something at " + hit.collider.gameObject.transform.position + " named " + hit.collider.gameObject.name);
            }
            ExplosionBehaviour.InstantiateExplosion(position + direction * (i + 2));
            Debug.Log("Nothing at " + (position + direction * (i + 1)));
            Debug.DrawRay(position, direction * (i + 1), Color.red, 10);
        }
    }
}
