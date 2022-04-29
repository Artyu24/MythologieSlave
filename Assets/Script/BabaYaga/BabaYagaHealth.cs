using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class BabaYagaHealth : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public Color Low, High;
    [SerializeField] private Vector3 offset;

    public int life, maxLife = 100, potentialLife;
    public float GetLife { get => life;}
    public float GetPotentialLife { get => potentialLife; }

    private bool isWellSpawn;

    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        life = maxLife;
        potentialLife = maxLife;
        SetHealth();
        StartCoroutine(IsWellSpawn());
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
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in players)
            {
                if (player.GetComponent<PlayerHealth>())
                {
                    player.GetComponent<PlayerHealth>().VICTORY();
                }
            }
            Destroy(gameObject);
        }
    }

    public void TakePotentialDamage(int potentialDamage)
    {
        potentialLife -= potentialDamage;
        if (potentialLife <= 0)
        {
            StartCoroutine(ResetPotentialLife());
        }
    }

    private IEnumerator ResetPotentialLife()
    {
        yield return new WaitForSeconds(1.3f);
        potentialLife = life;
    }

    private void SetHealth()
    {

        if (slider == null)
            return;

        slider.gameObject.SetActive(life < maxLife);

        slider.maxValue = maxLife;
        slider.value = life;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, slider.normalizedValue);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Laser") {
            TakeDamage(PlayerAttack.Instance.damageRay);
        }

        if(collision.tag == "AllyBullet") {
            TakeDamage(PlayerAttack.Instance.allyDamage);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Barriere") && !isWellSpawn)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator IsWellSpawn()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        isWellSpawn = true;
    }
}
