using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float respawnTime = 10f;
    public Vector3 spawnCenterOffset;

    GameObject currentPowerup;

    private void Start()
    {
        SpawnPowerUp();
    }

    public void WaitAndRespawn()
    {
        StartCoroutine(StartWaitRespawn());
    }

    public void SpawnPowerUp()
    {
        currentPowerup = GameObject.Instantiate<GameObject>(powerUpPrefab, transform.position + spawnCenterOffset, powerUpPrefab.transform.rotation);
        PowerUp powerUp = currentPowerup.GetComponent<PowerUp>();
        powerUp.onDestroy.AddListener(WaitAndRespawn);
    }

    IEnumerator StartWaitRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        SpawnPowerUp();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position + spawnCenterOffset, 1.0f);
    }
}
