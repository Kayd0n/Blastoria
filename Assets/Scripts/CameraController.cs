using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject map;
    public float heightMultiplier = 1.2f;
    private int mapSize;

    void Start()
    {
        mapSize = map.GetComponent<Map>().mapSize;
        transform.position = new Vector3(mapSize / 2, mapSize * heightMultiplier, mapSize / 2);
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
