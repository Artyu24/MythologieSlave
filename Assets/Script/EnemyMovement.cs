using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float distancePlayer = 1;
    [SerializeField] private float speed = 5;
    public float DistancePlayer { get => distancePlayer; set => distancePlayer = value; }
    public float Speed { get => speed; set => speed = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
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
            }
        }

        if(betterDistance > distancePlayer)
            rb.MovePosition(rb.position + enemyDirection.normalized * Time.fixedDeltaTime * speed);
    }
}
