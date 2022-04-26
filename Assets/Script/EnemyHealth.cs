using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int life, maxLife = 100, potentialLife;
    public float GetLife { get => life;}
    public float GetPotentialLife { get => potentialLife; }


    private void Awake()
    {
        life = maxLife;
        potentialLife = maxLife;
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakePotentialDamage(int potentialDamage)
    {
        potentialLife -= potentialDamage;
    }
}
