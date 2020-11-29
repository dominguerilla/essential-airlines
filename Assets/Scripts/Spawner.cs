using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool spawnCustomersOnStart = false;

    [Header("Spawn Prefabs")]
    public GameObject customerPrefab;
    public int minNumberOfCustomers = 4;
    public int maxNumberOfCustomers = 12;
    public GameObject restaurantPrefab;

    [Header("Debug Cube")]
    public Vector3 cubeOffset;
    public Vector3 cubeSize = new Vector3(1, 1, 1);
    public Color cubeColor;

    private void Start()
    {
        if (spawnCustomersOnStart)
        {
            SpawnCustomers(Random.Range(minNumberOfCustomers, maxNumberOfCustomers));
        }
    }

    public void SpawnCustomers(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 position = GetRandomOffset();
            GameObject.Instantiate<GameObject>(customerPrefab, transform.position + position, customerPrefab.transform.rotation);
        }
    }

    Vector3 GetRandomOffset()
    {
        float x = Random.Range(-cubeSize.x/2, cubeSize.x/2);
        float z = Random.Range(-cubeSize.z/2, cubeSize.z/2);
        return new Vector3(x, 0, z);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = cubeColor;
        Gizmos.DrawCube(transform.position + cubeOffset, cubeSize);
    }
}
