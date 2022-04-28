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

    //public void SpawnTotem() {
    //    GameObject player = GameObject.FindGameObjectWithTag("Player");

    //    if(totemsSpawn.Count > 1) {
    //        float closestDistance = 0;
    //        GameObject closestTotem = null;


    //        foreach(GameObject totem in totemsSpawn) {
    //            float distance = Vector3.Distance(player.transform.position, totem.transform.position);

    //            if(closestDistance == 0 || distance < closestDistance)  {
    //                closestDistance = distance;
    //                closestTotem = totem;
    //            }
    //        }

    //        if(closestTotem != null ) {
    //            totemsSpawn.Remove(closestTotem);

    //            int totemIndex = Random.Range(0, totemsSpawn.Count);

    //            GameObject totem = Instantiate(totemPrefab);
    //            totem.transform.parent = totemsSpawn[totemIndex].transform;
    //            totem.transform.localPosition = Vector3.zero;

    //            GameObject orb = Instantiate(listOrbeFinal[possessOrb]);
    //            orb.transform.parent = totem.transform;
    //            orb.transform.localPosition = new Vector3(0, 3.2f, 0);

    //            TotemTracker.Instance.StartTracker(totem);
    //        }



    //    }
    //    else {

    //        GameObject totem = Instantiate(totemPrefab);
    //        totem.transform.parent = totemsSpawn[0].transform;
    //        totem.transform.localPosition = Vector3.zero;

    //        GameObject orb = Instantiate(listOrbeFinal[possessOrb]);
    //        orb.transform.parent = totem.transform;
    //        orb.transform.localPosition = new Vector3(0, 3.2f, 0);

    //        TotemTracker.Instance.StartTracker(totem);
    //    }
    //}
}
