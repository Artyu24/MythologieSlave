using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabaYaga : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private EnemyBullet enemyBullet;

    [Header("Shoot")]
    private float shootDelay;

    [SerializeField] private float timeBtwShoot = 0.5f;
    [SerializeField] private float cooldownShoot = 3;

    private int bulletLaunch;
    private bool isInCooldownShoot;

    [SerializeField] private float bulletSpeed = 5;
    [SerializeField] private int damage = 8;
    public int Damage { get => damage; set => damage = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }

    [Header("Spawn Enemy")]
    private float spawnDelay;
    [SerializeField] private float timeBtwSpawn = 15;
    [SerializeField] private Transform[] spawnPointTab;
    [SerializeField] private GameObject enemyMelee, enemyDistance;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.zero;

        if (!isInCooldownShoot)
            shootDelay += Time.fixedDeltaTime;
        spawnDelay += Time.fixedDeltaTime;

        if (bulletLaunch == 3)
        {
            bulletLaunch = 0;
            StartCoroutine(CoolDownShoot());
        }

        if (shootDelay > timeBtwShoot)
        {
            shootDelay = 0;
            bulletLaunch++;
            EnemyBullet bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            bullet.GetDamage = Damage;
            bullet.GetSpeed = BulletSpeed;
        }

        if (spawnDelay > timeBtwSpawn)
        {
            spawnDelay = 0;

            List<GameObject> listOfEnemy = new List<GameObject>();

            for (int i = 0; i < 3; i++)
            {
                listOfEnemy.Add(enemyMelee);
            }

            for (int i = 0; i < 2; i++)
            {
                listOfEnemy.Add(enemyDistance);
            }

            foreach (Transform spawnPoint in spawnPointTab)
            {
                int i = Random.Range(0, listOfEnemy.Count);
                Instantiate(listOfEnemy[i], spawnPoint.position, Quaternion.identity);
                listOfEnemy.Remove(listOfEnemy[i]);
            }
        }
    }

    private IEnumerator CoolDownShoot()
    {
        isInCooldownShoot = true;
        yield return new WaitForSeconds(cooldownShoot);
        isInCooldownShoot = false;
    }
}
