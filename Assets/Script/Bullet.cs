using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    public float GetSpeed { get => speed; set => speed = value; }

    private int damage;
    public int GetDamage { get => damage; set => damage = value; }

    private Vector2 bulletDirection = Vector2.zero;
    private GameObject enemyTarget;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        TargetAnEnemy();
    }

    private void FixedUpdate()
    {
        if (enemyTarget != null)
        {
            bulletDirection = enemyTarget.transform.position - transform.position;
            rb.MovePosition(rb.position + bulletDirection.normalized * Time.fixedDeltaTime * speed);
        }
        else
            TargetAnEnemy();
    }

    private void TargetAnEnemy()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        float betterDistance = 99999999999;
        bulletDirection = Vector2.zero;

        foreach (GameObject enemy in enemyList)
        {
            float enemyToAllies = Vector2.Distance(transform.position, enemy.transform.position);

            if (betterDistance > enemyToAllies && enemy.GetComponent<EnemyHealth>().GetPotentialLife > 0)
            {
                betterDistance = enemyToAllies;
                bulletDirection = enemy.transform.position - transform.position;
                enemyTarget = enemy;
            }
        }

        if (enemyList.Length == 0)
        {
            Destroy(gameObject);
        }
        else if(enemyTarget != null)
        {
            Debug.Log(enemyTarget.GetComponent<EnemyHealth>().GetPotentialLife);
            enemyTarget.GetComponent<EnemyHealth>().TakePotentialDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
