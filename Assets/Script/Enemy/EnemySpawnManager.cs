using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPointList = new List<Transform>();

    [SerializeField] private GameObject enemyMelee, enemyDistance;

    private float delay;
    private float timeBtwEachSpawn = 1f;
    public float TimeBtwEachSpawn { get => timeBtwEachSpawn; set => timeBtwEachSpawn = value; }

    private void Update()
    {
        if (GameManager.gameState == GameManager.GameState.InGame)
        {
            delay += Time.deltaTime;

            if (delay > timeBtwEachSpawn)
            {
                delay = 0;
                int i = Random.Range(0, spawnPointList.Count);
                int y = Random.Range(0, 10);
                if (y <= 6)
                {
                    Instantiate(enemyMelee, spawnPointList[i].transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(enemyDistance, spawnPointList[i].transform.position, Quaternion.identity);
                }
            }
        }
    }
}
