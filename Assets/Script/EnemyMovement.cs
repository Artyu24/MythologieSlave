using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 5;

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
            float enemyToAllies = Vector2.Distance(transform.position, allies.transform.position);

            Debug.Log(allies.name + " : " + enemyToAllies);
            //Debug.Log("better distance : " + betterDistance);

            if (betterDistance > enemyToAllies)
            {
                betterDistance = enemyToAllies;
                enemyDirection = allies.transform.position - transform.position;
            }
        }

        rb.MovePosition(rb.position + enemyDirection.normalized * Time.fixedDeltaTime * speed);
    }
}
