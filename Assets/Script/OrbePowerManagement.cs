using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbePowerManagement : MonoBehaviour
{
    [SerializeField] private GameObject[] listDeusOrbe = new GameObject[4];
    public static GameObject[] listOrbeFinal = new GameObject[4];

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        bool isDone = true;
        
        foreach (GameObject orbe in listDeusOrbe)
        {
            if (orbe == null)
            {
                break;
            }
            while (isDone)
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
