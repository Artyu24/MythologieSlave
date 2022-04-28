using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public Color Low, High;
    [SerializeField] private Vector3 offset;

    private int life, maxLife = 100, potentialLife;
    public float GetLife { get => life;}
    public float GetPotentialLife { get => potentialLife; }

    private void Awake()
    {
        life = maxLife;
        potentialLife = maxLife;
        SetHealth();
    }

    private void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        SetHealth();
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
            else
            {
                GameManager.enemyKill++;
            }
        }
    }

    public void TakePotentialDamage(int potentialDamage)
    {
        potentialLife -= potentialDamage;
    }

    private void SetHealth()
    {
        slider.gameObject.SetActive(life < maxLife);
        slider.value = life;
        slider.maxValue = maxLife;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, slider.normalizedValue);
    }
}
