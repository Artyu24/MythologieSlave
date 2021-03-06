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

    public int life, maxLife = 100, potentialLife;
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
                GameManager.lastEnemyKillPos = gameObject.transform.position;
            }
            Destroy(gameObject);
        }
    }

    public void TakePotentialDamage(int potentialDamage)
    {
        potentialLife -= potentialDamage;
    }

    private void SetHealth()
    {
        slider.gameObject.SetActive(life < maxLife);

        slider.maxValue = maxLife;
        slider.value = life;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, slider.normalizedValue);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Laser") {
            TakeDamage(PlayerAttack.Instance.damageRay);
        }
    }

}
