using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BombBehaviour : MonoBehaviour
{
    [SerializeField] public GameObject blockPrefabReference;
    public static GameObject bombPrefab;

    public static GameObject InstantiateBomb(Vector3 position, int force)
    {
        // Raycast current position to check if there is a wall
        {
            RaycastHit hit;
            if (Physics.Raycast(position, Vector3.down, out hit, 1))
            {
                if (hit.collider.gameObject.tag == "wall" || hit.collider.gameObject.tag == "indestructible_wall")
                {
                    Debug.Log("wall at " + hit.collider.gameObject.transform.position + " can't place bomb here");
                    return null;
                }
            }
        }

        if (bombPrefab == null)
        {
#if UNITY_EDITOR
                bombPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Bomb.prefab");
#else
            bombPrefab = Resources.Load<GameObject>("Prefabs/Bomb");
#endif
            if (bombPrefab == null)
            {
                Debug.LogError("Bomb prefab not found");
            }
        }

        Vector3 clipped = new Vector3(Mathf.RoundToInt(position.x), position.y, Mathf.RoundToInt(position.z));
        GameObject bomb = Instantiate(bombPrefab, clipped, Quaternion.identity);

        BombBehaviour behaviour = bomb.GetComponent<BombBehaviour>();
        if (behaviour == null)
        {
            behaviour = bomb.AddComponent<BombBehaviour>();
        }
        behaviour.SetForce(force);
        return bomb;
    }

    private int _force;

    public void SetForce(int force)
    {
        _force = force;
    }

    void OnDestroy()
    {
        Vector3 scale = blockPrefabReference.transform.localScale;
        Vector3[] directions = { new Vector3(1 * scale.x, 0, 0), new Vector3(-1 * scale.x, 0, 0), new Vector3(0, 0, 1 * scale.z), new Vector3(0, 0, -1 * scale.z) };

        for (int i = 0; i < directions.Length; ++i)
        {
            ExplosionBehaviour.ExplosionSpread(transform.position, directions[i], _force);
        }
    }
}
