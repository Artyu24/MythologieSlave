using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public Color Low, High;

    private int life, maxLife = 1000;
    public float GetLife { get => life; }

    private void Awake()
    {
        GameObject healthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar");
        slider = healthBar.GetComponent<Slider>();
        life = maxLife;
        SetHealth();
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
        SetHealth();
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
        slider.maxValue = maxLife;
        slider.value = life;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, slider.normalizedValue);
    }
}
