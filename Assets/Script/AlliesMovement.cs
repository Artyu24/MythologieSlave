using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliesMovement : MonoBehaviour {

    private Rigidbody2D rb;

    public GameObject bulletPrefab;

    public float detectionRange;
    public float speed = 5;
    private bool hasSeeEnemy;

    private float timer;
    public float attackSpeed;
    public float attackReload;

    private Vector3 direction;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).position;

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        float nearbyDistance = float.MaxValue;
        GameObject targetEnemy = null;

        foreach (GameObject enemy in allEnemies) {
            float distance = Vector3.Distance(transform.position,enemy.transform.position);
               
            if(distance <= detectionRange && distance <= nearbyDistance) {
                nearbyDistance = distance;
                targetEnemy = enemy;
                hasSeeEnemy = true;
            }
        }

        if(hasSeeEnemy) {
            timer += Time.deltaTime;

            if(timer >= attackReload) {
                timer = 0;

                direction = (targetEnemy.transform.position - transform.position).normalized;

                GameObject bullet = Instantiate(bulletPrefab, transform.position + direction * 0.3f , Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = attackSpeed * direction;
            }
        }
        else {
            timer = 0f;
        }

        if(direction != Vector3.zero)
        {
            Debug.DrawLine(transform.position,transform.position + direction * 5,Color.red);
        }


        
    }
}
