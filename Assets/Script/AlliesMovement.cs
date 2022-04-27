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

    

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
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
                Vector3 direction = (transform.position - targetEnemy.transform.position).normalized;

                Debug.Log("direction: " + direction);

                GameObject bullet = Instantiate(bulletPrefab, transform.position + direction, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(attackSpeed,0,0);
            }
        }
        else {
            timer = 0f;
        }



        
    }
}
