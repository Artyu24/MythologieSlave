using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healSlider;
    [SerializeField] private Slider shieldSlider;
    public Color Low, High;

    public int life, shield, maxLife = 1000, maxShield = 200;
    public float GetLife { get => life; }

    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject victoryMenu;

    private void Awake()
    {
        GameObject healthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar");
        GameObject shieldBar = GameObject.FindGameObjectWithTag("PlayerShieldBar");

        healSlider = healthBar?.GetComponent<Slider>();
        shieldSlider = shieldBar?.GetComponent<Slider>();
        
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
                deathMenu.SetActive(true);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
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
        if(healSlider == null)
            return;

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
        if(shieldSlider == null) {
            return;
        }

        shieldSlider.gameObject.SetActive(shield > 0);
        shieldSlider.maxValue = maxShield;
        shieldSlider.value = shield;
    }

    public void VICTORY()
    {
        
    }
}
