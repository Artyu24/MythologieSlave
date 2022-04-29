using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoss : MonoBehaviour
{
    [SerializeField] private GameObject totemOne, totemTwo, totemThree, totemFour;
    private int totemActif;

    [SerializeField] private GameObject boss;
    [SerializeField] private Transform spawnBossPoint;

    public static ActivateBoss Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void InitFight()
    {
        totemOne.GetComponent<TotemToActivate>().ResetTotem();
        totemTwo.GetComponent<TotemToActivate>().ResetTotem();
        totemThree.GetComponent<TotemToActivate>().ResetTotem();
        totemFour.GetComponent<TotemToActivate>().ResetTotem();
        totemActif = 0;
    }

    public void ActivateATotem()
    {
        totemActif++;
        if (totemActif == 5)
        {
            Instantiate(boss, spawnBossPoint.position, Quaternion.identity);
        }
    }
}
