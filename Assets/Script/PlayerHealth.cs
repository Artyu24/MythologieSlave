using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healSlider;
    [SerializeField] private Slider shieldSlider;
    public Color Low, High;

    private int life, shield, maxLife = 1000, maxShield = 200;
    public float GetLife { get => life; }

    private void Awake()
    {
        GameObject healthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar");
        GameObject shieldBar = GameObject.FindGameObjectWithTag("PlayerShieldBar");
        healSlider = healthBar.GetComponent<Slider>();
        shieldSlider = shieldBar.GetComponent<Slider>();
        life = maxLife;
        SetHealth();
        SetShield();
    }

    public void TakeDamage(int damage)
    {
        if (shield > 0)
        {
            if (shield <= damage)
            {
                damage -= shield;
                shield = 0;
            }
            else
            {
               shield -= damage;
            }
        }

        if (shield == 0)
        {
            life -= damage;
            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }
        SetHealth();
        SetShield();
    }

    public void AddLife(int heal)
    {
        life += heal;
        if (life > maxLife)
            life = maxLife;

        SetHealth();
    }

    private void SetHealth()
    {
        healSlider.maxValue = maxLife;
        healSlider.value = life;

        healSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, healSlider.normalizedValue);
    }
    public void AddShield()
    {
        shield = maxShield;
        SetShield();
    }

    private void SetShield()
    {
        shieldSlider.gameObject.SetActive(shield > 0);
        shieldSlider.maxValue = maxShield;
        shieldSlider.value = shield;
    }
}
