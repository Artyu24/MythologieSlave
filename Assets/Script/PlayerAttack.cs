using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {


    [Header("Competence 1")]

    private bool isShooting;
    private float delay;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform spawnBulletPoint;

    private float bulletSpeed = 5;
    private int bulletDamage = 50;

    [Header("Competence 2")]
    public int cooldownTwo;
    private float timerTwo;
    [HideInInspector]
    public bool isInCooldownTwo;



    [Header("Competence 4")]
    public float rayDistance;
    public List<GameObject> rays;

    void Start() {
    }

    void Update() {
        Debug.DrawLine(transform.position,transform.position + transform.right * 100,Color.red);

        Attack(0);
    }

    void FixedUpdate()
    {
        //SHOOT
        delay += Time.fixedDeltaTime;

        if (isShooting && delay >= 0.25f)
        {
            delay = 0;
            Bullet actualBullet = Instantiate(bullet, spawnBulletPoint.position, Quaternion.identity);
            actualBullet.GetSpeed = bulletSpeed;
            actualBullet.GetDamage = bulletDamage;
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
            isShooting = true;
        else if (context.canceled)
            isShooting = false;
    }

    void Attack(int attackID) {
        Vector3 beginTop = rays[0].transform.position;
        Vector3 beginBottom = rays[1].transform.position;
        Vector3 endTop = rays[0].transform.position + transform.right * rayDistance;
        Vector3 endBottom = rays[1].transform.position + transform.right * rayDistance;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> ennemies = new List<GameObject>();

        foreach(GameObject enemy in enemies) {
            Vector3 enemyPos = enemy.transform.position;

            if(enemyPos.x >= beginTop.x && enemyPos.x <= endTop.x) {
                if(enemyPos.y <= beginTop.y && enemyPos.y >= endBottom.y)
                    ennemies.Add(enemy);
            }
        }

        // (Vector2 origin, Vector2 direction, float distance);
    }


}
