using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoss : MonoBehaviour
{
    [SerializeField] private GameObject totemOne, totemTwo, totemThree, totemFour;
    private int totemActif;

    [SerializeField] private GameObject boss;
    [SerializeField] private Transform spawnBossPoint;

    public void ActivateATotem()
    {
        totemActif++;
        if (totemActif == 4)
        {
            Instantiate(boss, spawnBossPoint.position, Quaternion.identity);
        }
    }
}
