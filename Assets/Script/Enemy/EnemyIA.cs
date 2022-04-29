using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool isDistanceAttack;

    private PlayerHealth alliesHealth;

    [SerializeField] private EnemyBullet enemyBullet;

    private float delay;
    [SerializeField] private float timeBtwAttack = 3;

    [SerializeField] private float distancePlayer = 1;
    [SerializeField] private float movSpeed = 5;
    [SerializeField] private float bulletSpeed = 2;
    [SerializeField] private int damage = 5;

    public float DistancePlayer { get => distancePlayer; set => distancePlayer = value; }
    public float MovSpeed { get => movSpeed; set => movSpeed = value; }
    public int Damage { get => damage; set => damage = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.zero;
        GameObject[] getAllAllies = GameObject.FindGameObjectsWithTag("Player");

        float betterDistance = 99999999999;
        Vector2 enemyDirection = Vector2.zero;

        foreach (GameObject allies in getAllAllies)
        {
            float bulletToEnemy = Vector2.Distance(transform.position, allies.transform.position);

            if (betterDistance > bulletToEnemy)
            {
                betterDistance = bulletToEnemy;
                enemyDirection = allies.transform.position - transform.position;
                alliesHealth = allies.GetComponent<PlayerHealth>();
            }
        }

        if (betterDistance >= distancePlayer)
        {
            rb.MovePosition(rb.position + enemyDirection.normalized * Time.fixedDeltaTime * movSpeed);
            delay = 0;
        }
        else
        {
            delay += Time.fixedDeltaTime;

            if (delay > timeBtwAttack)
            {
                delay = 0;

                if (!isDistanceAttack)
                {
                    alliesHealth.TakeDamage(Damage);
                }
                else
                {
                    EnemyBullet bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    bullet.GetDamage = Damage;
                    bullet.GetSpeed = BulletSpeed;
                }
            }
        }
    }
}
