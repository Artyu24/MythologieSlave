using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbePowerManagement : MonoBehaviour
{
    [SerializeField] private GameObject[] listDeusOrbe = new GameObject[4];
    public static GameObject[] listOrbeFinal = new GameObject[4];

    [SerializeField] private GameObject[] totemsSpawnPointTab = new GameObject[4];
    [SerializeField] public static int actualOrb = 0;

    public static OrbePowerManagement Instance { get; private set; }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    private void Start()
    {
        bool isDone = false;
        
        foreach (GameObject orbe in listDeusOrbe)
        {
            isDone = false;
            if (orbe != null)
            {
                while (!isDone)
                {
                    isDone = false;
                    int i = Random.Range(0, 4);
                    if (listOrbeFinal[i] == null)
                    {
                        isDone = true;
                        listOrbeFinal[i] = orbe;
                    }
                }
            }
        }
    }

    public void SpawnOrbe()
    {
        totemsSpawnPointTab = GameObject.FindGameObjectsWithTag("OrbSpawn");

        GameObject[] alliesTab = GameObject.FindGameObjectsWithTag("Player");
        GameObject player = null;
        foreach (GameObject allies in alliesTab)
        {
            if (allies.GetComponent<PlayerHealth>())
            {
                player = allies;
                break;
            }
        }

        float betterDistance = 99999999999;
        GameObject spawnPointNearest = null;

        foreach (GameObject spawnPoint in totemsSpawnPointTab)
        {
            float bulletToEnemy = Vector2.Distance(player.transform.position, spawnPoint.transform.position);

            if (betterDistance > bulletToEnemy)
            {
                betterDistance = bulletToEnemy;
                spawnPointNearest = spawnPoint;
            }
        }

        bool isDone = false;
        while (!isDone)
        {
            int i = Random.Range(0, 4);
            if (totemsSpawnPointTab[i] != spawnPointNearest)
            {
                isDone = true;
                Instantiate(listOrbeFinal[actualOrb], totemsSpawnPointTab[i].transform.position, Quaternion.identity);
                actualOrb++;
                //TotemTracker.Instance.StartTracker(totemsSpawnPointTab[i]);
            }
        }

    }

}
