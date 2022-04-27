using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Uduino;

public class PlayerAttack : MonoBehaviour {


    [Header("Competence 1")]

    private bool isShooting;
    private float delay;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform spawnBulletPoint;

    private float bulletSpeed = 5;
    private int bulletDamage = 50;

    [Header("Competence 2")]
    public GameObject allyPrefab;
    public int cooldownTwo;
    private float timerTwo;
    [HideInInspector]
    public bool isInCooldownTwo;
    private int spawnAllies;
    public int maxSpawnAllies = 1;
    public float spawnAlliesDelay;
    public float spawnAlliesRange;


    [Header("Competence 3")]
    public GameObject hammerPrefab;
    public float spawnRange;
    public int spawnMin;
    public int spawnMax;
    public float spawnDelay;
    private int maxSpawn;
    private int actualSpawn;


    [Header("Competence 4")]
    public float rayDistance;
    public List<GameObject> rays;
    public int damageRay;

    public static PlayerAttack Instance { get; set; }


    private int RED = 7;
    private int GREEN = 6;
    private int BLUE = 5;

    private void Awake() {
        Instance = this;

        UduinoManager.Instance.pinMode(7, PinMode.Output);
        UduinoManager.Instance.pinMode(6, PinMode.Output);
        UduinoManager.Instance.pinMode(5, PinMode.Output);
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


    public void OnSkillSpawn(InputAction.CallbackContext e) {
        if(e.performed) {

            Debug.Log("debug arduino");

            UduinoManager.Instance.analogWrite(RED,/*(int) Scepter.Instance.spell2.r*/ 0);
            UduinoManager.Instance.analogWrite(GREEN,255);
            UduinoManager.Instance.analogWrite(BLUE, /*(int)Scepter.Instance.spell2.b*/ 0);

            SpawnAllies();
        }
    }

    public void SpawnAllies() {
        Vector3 beginRandom = transform.position - transform.right * spawnAlliesRange;
        Vector3 endRandom = transform.position + transform.right * spawnAlliesRange;

        Vector3 shieldBegin = transform.position - transform.right * 1.5f;
        Vector3 shieldEnd = transform.position + transform.right * 1.5f;

        float spawnX = Random.Range(beginRandom.x, endRandom.x);
        float spawnY = Random.Range(transform.position.y - 5, transform.position.y + 5);

        while (spawnX >= shieldBegin.x && spawnX <= shieldEnd.x) 
            spawnX = Random.Range(beginRandom.x, endRandom.x);
        
        while (spawnY >= (transform.position.y - 1) && spawnY <= (transform.position.y + 1))
            spawnY = Random.Range(transform.position.y - 5, transform.position.y + 5);

        Instantiate(allyPrefab, new Vector3(spawnX, spawnY, 0f), Quaternion.identity);

        StartCoroutine(WaitToSpawnAllies());
    }

    public IEnumerator WaitToSpawnAllies() {
        yield return new WaitForSeconds(spawnAlliesDelay);

        spawnAllies++;

        if (spawnAllies < maxSpawnAllies)
            SpawnAllies();
    }


    #region "Hammer Skill"

    public void OnSkillHammer(InputAction.CallbackContext e) {
        if(e.performed) {
            maxSpawn = Random.Range(spawnMin, spawnMax);
            SpawnHammer();
        }
    }

    private IEnumerator WaitToSpawnHammer() {
        yield return new WaitForSeconds(spawnDelay);
        
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

        while(spawnX >= shieldBegin.x && spawnX <= shieldEnd.x) {
            spawnX = Random.Range(beginRandom.x, endRandom.x);
        }

        Instantiate(hammerPrefab,new Vector3(spawnX,beginRandom.y - 0.5f,beginRandom.z),Quaternion.Euler(0,0,-180));

        StartCoroutine(WaitToSpawnHammer());
    }

    #endregion
}
