using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabaYaga : MonoBehaviour
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
        delay += Time.fixedDeltaTime;

        if (delay > timeBtwAttack)
        {
            delay = 0;
            EnemyBullet bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            bullet.GetDamage = Damage;
            bullet.GetSpeed = BulletSpeed;
        }
    }
}
