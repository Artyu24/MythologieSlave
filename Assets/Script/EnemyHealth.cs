using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

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
            if (GameManager.tutorialState == GameManager.TutorialState.AutoAttack || GameManager.tutorialState == GameManager.TutorialState.DeusAttack)
            {
                GameManager.nbrEnemyTuto--;
                if (GameManager.nbrEnemyTuto == 0)
                {
                    GameManager.UpdateTutorial();
                }
            }
        }
    }

    public void TakePotentialDamage(int potentialDamage)
    {
        potentialLife -= potentialDamage;
    }
}
