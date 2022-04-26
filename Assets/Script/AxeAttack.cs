using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAttack : MonoBehaviour
{
    private float speed;
    public float Speed { get => speed; set => speed = value; }

    private int damage;
    public int Damage { get => damage; set => damage = value; }

    private Vector2 bulletDirection = Vector2.zero;
    private GameObject enemyTarget;
    private Rigidbody2D rb;

    private List<EnemyHealth> enemyStrikeList = new List<EnemyHealth>();

    private int nbrEnemyStrike;
    private int nbrEnemyStrikeMax;
    public int NbrEnemyStrikeMax { get => nbrEnemyStrikeMax; set => nbrEnemyStrikeMax = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        nbrEnemyStrike = nbrEnemyStrikeMax;

        TargetAnEnemy();
    }

    private void FixedUpdate()
    {
        if (enemyTarget != null)
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 10);
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
        enemyTarget = null;

        foreach (GameObject enemy in enemyList)
        {
            float enemyToAllies = Vector2.Distance(transform.position, enemy.transform.position);

            if (betterDistance > enemyToAllies && enemy.GetComponent<EnemyHealth>().GetPotentialLife > 0 && !enemyStrikeList.Contains(enemy.GetComponent<EnemyHealth>()))
            {
                betterDistance = enemyToAllies;
                bulletDirection = enemy.transform.position - transform.position;
                enemyTarget = enemy;
            }
        }

        if (enemyList.Length == 0)
        {
            ResetAxeAttack();
        }
        else if (enemyTarget != null)
        {
            enemyTarget.GetComponent<EnemyHealth>().TakePotentialDamage(damage);
        }
        else
        {
            ResetAxeAttack();
        }
    }

    private void ResetAxeAttack()
    {
        GameObject player = null;
        GameObject[] alliesTab = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject allies in alliesTab)
        {
            if (allies.GetComponent<PlayerController>())
                player = allies;
        }

        player.GetComponent<CompetenceOne>().IsActive = false;
        enemyStrikeList.Clear();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage);

            nbrEnemyStrike--;
            enemyStrikeList.Add(enemyHealth);

            if(nbrEnemyStrike == 0)
                ResetAxeAttack();
            else
                TargetAnEnemy();
        }
    }
}
