using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliesMovement : MonoBehaviour {

    private Rigidbody2D rb;

    public float distancePlayer = 1;
    public float speed = 5;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
     /*   GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        float betterDistance = 99999999999;
        Vector2 enemyDirection = Vector2.zero;

        foreach (GameObject enemy in allEnemies) {
            float bulletToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            if (betterDistance > bulletToEnemy) {
                betterDistance = bulletToEnemy;
                enemyDirection = enemy.transform.position - transform.position;
            }
        }

        if (betterDistance > distancePlayer)
            rb.MovePosition(rb.position + enemyDirection.normalized * Time.fixedDeltaTime * speed);

    */
    }
}
