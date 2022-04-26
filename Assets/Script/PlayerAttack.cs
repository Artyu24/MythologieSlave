using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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


    [Header("Competence 3")]
    public GameObject hammerPrefab;
    public float spawnRange;
    public int spawnMin;
    public int spawnMax;
    private int maxSpawn;
    private int actualSpawn;


    [Header("Competence 4")]
    public float rayDistance;
    public List<GameObject> rays;
    public int damageRay;

    public static PlayerAttack Instance { get; set; }

    private void Awake() {
        Instance = this;
    }
    void Start() {}

    void Update() {
        Debug.DrawLine(transform.position,transform.position + transform.right * 100,Color.red);

    }

    void FixedUpdate() {
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

    public void OnSkillHammer(InputAction.CallbackContext e) {
        if(e.performed) {
            Debug.Log("skill");
            maxSpawn = Random.Range(spawnMin, spawnMax);
            SpawnHammer();
            
        }
    }

    private IEnumerator WaitToSpawnHammer() {
        yield return new WaitForSeconds(3f);

        Debug.Log("entered");
        
        actualSpawn++;

        if(actualSpawn < maxSpawn)
            SpawnHammer();
    }

    private void SpawnHammer() {
        Vector3 beginRandom = transform.position - transform.right * spawnRange;
        Vector3 endRandom = transform.position + transform.right * spawnRange;

        Vector3 shieldBegin = transform.position - transform.right * 1.5f;
        Vector3 shieldEnd = transform.position + transform.right * 1.5f;

        beginRandom.y = Camera.main.transform.position.y + Camera.main.orthographicSize;
        endRandom.y = Camera.main.transform.position.y + Camera.main.orthographicSize;

        float spawnX = Random.Range(beginRandom.x,endRandom.x);

        Instantiate(hammerPrefab,new Vector3(spawnX,beginRandom.y - 0.5f,beginRandom.z),Quaternion.Euler(0,0,-180));

        StartCoroutine(WaitToSpawnHammer());

        Debug.Log("entered02");
    }


}
