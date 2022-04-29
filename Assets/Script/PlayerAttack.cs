using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Uduino;

public class PlayerAttack : MonoBehaviour {

    [Header("AutoAttack")]
    public bool isAutoShooting;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform spawnBulletPoint;
    private float bulletDelay;
    private float bulletSpeed = 5;
    private int bulletDamage = 50;


    [Header("Competence 1")]
    public bool hasThunderSkill;
    public bool isShooting;
    private float delay;
    public bool isAxeShooting;
    private float axeDelay;
    [SerializeField] private AxeAttack axe;
    [SerializeField] private Transform spawnAxePoint;
    public float axeSpeed = 5;
    public int axeDamage = 50;
    public int nbrEnemyStrike = 20;

    private bool isActive;
    public bool IsActive { get => isActive; set => isActive = value; }


    [Header("Competence 2")]
    public bool hasFertilitySkill;
    public GameObject allyPrefab;

    public int cooldownFertility;
    private float timerFertility;
    public bool startCooldownFertility;

    [HideInInspector]
    private int spawnAllies;
    public int maxSpawnAllies = 1;
    public float spawnAlliesDelay;
    public float spawnAlliesRange;
    public int allyDamage;


    [Header("Competence 3")]
    public bool hasHammerSkill;
    public GameObject hammerPrefab;
    public int maxTimerHammer;
    public Vector2 hammerSpeed;
    private float timer;
    private float spawnDelay = 0.3f;
    private bool rightDir = true;
    public float radiusArea;
    public int hammerAttack;

    public int cooldownHammer;
    private float timerHammer;
    public bool startCooldownHammer;


    [Header("Competence 4")]
    public bool hasRaySkill;
    public float rayDistance;
    public int damageRay;
    public GameObject rayPrefab;

    public int cooldownRay;
    private float timerRay;
    public bool startCooldownRay;


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


    void Update() {
        Debug.DrawLine(transform.position,transform.position + transform.right * 100,Color.red);

        if(startCooldownFertility) {
            timerFertility += Time.deltaTime;

            if(timerFertility >= cooldownFertility) {
                timerFertility = 0;
                startCooldownFertility = false;
                if (transform.gameObject.TryGetComponent<PlayerController>(out PlayerController controller))
                    controller.animator.SetBool("IsAttacking", false);
            }
        }

        if (startCooldownHammer) {
            timerHammer += Time.deltaTime;

            if (timerHammer >= cooldownHammer) {
                timerHammer = 0;
                startCooldownHammer = false;
                if (transform.gameObject.TryGetComponent<PlayerController>(out PlayerController controller))
                    controller.animator.SetBool("IsAttacking", false);
            }
        }

        if (startCooldownRay) {
            timerRay += Time.deltaTime;

            if (timerRay >= cooldownRay) {
                timerRay = 0;
                startCooldownRay = false;
                if (transform.gameObject.TryGetComponent<PlayerController>(out PlayerController controller))
                {
                    Debug.Log("change");
                    controller.animator.SetBool("IsAttacking", false);
                }
            }
        }


    }

     void FixedUpdate() {
        //SHOOT
        if (!isActive)
        {
            axeDelay += Time.fixedDeltaTime;
            bulletDelay += Time.fixedDeltaTime;
        }

        if (isAutoShooting && bulletDelay >= 0.25f)
        {
            bulletDelay = 0;
            Bullet actualBullet = Instantiate(bullet, spawnBulletPoint.position, Quaternion.identity);
            actualBullet.GetSpeed = bulletSpeed;
            actualBullet.GetDamage = bulletDamage;
        }

        if (isAxeShooting && axeDelay >= 1f && hasThunderSkill)
        {
            axeDelay = 0;
            isActive = true;

            if (transform.gameObject.TryGetComponent<PlayerController>(out PlayerController controller))
                controller.animator.SetBool("IsAttacking", true);

            AxeAttack axeObject = Instantiate(axe, spawnAxePoint.position, Quaternion.identity);
            axeObject.Speed = axeSpeed;
            axeObject.Damage = axeDamage;
            axeObject.NbrEnemyStrikeMax = nbrEnemyStrike;
        }
    }

    public void AutoShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
            isAutoShooting = true;
        else if (context.canceled)
            isAutoShooting = false;
    }

    public void AxeShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
            isAxeShooting = true;
        else if (context.canceled)
            isAxeShooting = false;
    }

    //Upgrade damage when Item Get
    public void UpgradeDamage(int damage)
    {
        bulletDamage += damage;
        axeDamage += damage;
        allyDamage += damage;
        damageRay += damage;
        hammerAttack += damage;
    }

    public void OnSkillSpawn() {

        if (!hasFertilitySkill && !startCooldownFertility)
            return;

        Debug.Log("debug arduino");

        UduinoManager.Instance.analogWrite(RED,/*(int) Scepter.Instance.spell2.r*/ 0);
        UduinoManager.Instance.analogWrite(GREEN,255);
        UduinoManager.Instance.analogWrite(BLUE, /*(int)Scepter.Instance.spell2.b*/ 0);
        
        startCooldownFertility = true;

        if (transform.gameObject.TryGetComponent<PlayerController>(out PlayerController controller))
            controller.animator.SetBool("IsAttacking", true);

        SpawnAllies();
        
    }

    public void SpawnAllies() {
        
        GameObject ally = Instantiate(allyPrefab, transform.GetChild(0).position, Quaternion.identity);

        ally.transform.parent = transform;

        StartCoroutine(WaitToSpawnAllies());
    }

    public IEnumerator WaitToSpawnAllies() {
        yield return new WaitForSeconds(spawnAlliesDelay);

        spawnAllies++;

        if (spawnAllies < maxSpawnAllies)
            SpawnAllies();     

    }


    #region "Hammer Skill"

    public void OnSkillHammer() {

        if (!hasHammerSkill && !startCooldownHammer)
            return;


        if (transform.gameObject.TryGetComponent<PlayerController>(out PlayerController controller))
            controller.animator.SetBool("IsAttacking", true);

        SpawnHammer();
        
    }

    private IEnumerator WaitToSpawnHammer() {
        yield return new WaitForSeconds(spawnDelay);

        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            if(distance <= radiusArea) {
                if(enemy.TryGetComponent<EnemyHealth>(out EnemyHealth health))  {
                    health.TakeDamage(hammerAttack);
                }
            }
        }

        timer += spawnDelay;

        if (timer < maxTimerHammer)
        {
            SpawnHammer();
        }
        else
            timer = 0;
    }

    private void SpawnHammer() {
        GameObject hammer = Instantiate(hammerPrefab,transform.position,Quaternion.Euler(0,0,-180));

        hammerSpeed.x = Random.Range(1, radiusArea);

        Vector3 force = rightDir ? transform.right * hammerSpeed.x : transform.right * -1 * hammerSpeed.x;
        force.y = hammerSpeed.y;

        hammer.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

        rightDir = !rightDir;
        StartCoroutine(WaitToSpawnHammer());
    }


    public void OnLaserSkill() {
        if (!hasRaySkill && !startCooldownRay)
            return;

      //  if (transform.gameObject.TryGetComponent<PlayerController>(out PlayerController controller))
        //    controller.animator.SetBool("IsAttacking", true);

        GameObject ray = Instantiate(rayPrefab, transform.position,Quaternion.identity);

        ray.transform.parent = transform;
    }

    #endregion
}
