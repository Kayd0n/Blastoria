using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static GameObject playerPrefab;
    public static GameObject InstantiatePlayer(Vector3 position, bool isPlayerOne = true)
    {
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

        if (playerPrefab == null)
        {
#if UNITY_EDITOR
                playerPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player.prefab");
#else
            playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
#endif
            if (playerPrefab == null)
            {
                Debug.LogError("Player prefab not found");
                return null;
            }
        }

        Vector3 clipped = new Vector3(Mathf.RoundToInt(position.x), position.y, Mathf.RoundToInt(position.z));
        GameObject player = Instantiate(playerPrefab, clipped, Quaternion.identity);

        player.GetComponent<PlayerMove>().speed = 5;
        player.GetComponent<PlayerMove>().isPlayerOne = isPlayerOne;
        PlayerBehaviour behaviour = player.GetComponent<PlayerBehaviour>();
        if (behaviour == null)
        {
            behaviour = player.AddComponent<PlayerBehaviour>();
        }
        return player;
    }
}
