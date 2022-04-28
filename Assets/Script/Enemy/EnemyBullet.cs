using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed;
    public float GetSpeed { get => speed; set => speed = value; }

    private int damage;
    public int GetDamage { get => damage; set => damage = value; }

    private Vector2 bulletDirection = Vector2.zero;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject[] getAllAllies = GameObject.FindGameObjectsWithTag("Player");

        float betterDistance = 99999999999;
        Vector2 enemyDirection = Vector2.zero;

        foreach (GameObject allies in getAllAllies)
        {
            float bulletToEnemy = Vector2.Distance(transform.position, allies.transform.position);

            if (betterDistance > bulletToEnemy)
            {
                betterDistance = bulletToEnemy;
                bulletDirection = allies.transform.position - transform.position;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + bulletDirection.normalized * Time.fixedDeltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
