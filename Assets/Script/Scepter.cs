using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Scepter : MonoBehaviour {


    private List<Joycon> joycons;

    [SerializeField]
    private float accelSpeed;

    public int actualSpellID;
    public List<Image> spellsImage;

    public Vector3 accel;
    public Vector3 gyro;

    List<Vector3> gyroSave = new List<Vector3>();

    private float timer;
    private bool hasFinishCheck = true;

    [Header("Config")]
    [SerializeField] float _mesureTime = 1f;
    [SerializeField] float _magnitudeThreshold = 2f;

    Coroutine _coroutine;

    public static Scepter Instance { get; private set; }


    private int actualSkillId = 1;

    public Image weel;

    public List<Sprite> orbs = new List<Sprite>();


    private void Awake() {
        Instance = this;
    }

    void Start() {
        joycons = JoyconManager.Instance.j;

        
    }

    void Update() {

        if (joycons != null && joycons.Count > 0) {

            Joycon joycon = joycons[0];

            if (_coroutine == null && joycon.GetAccel().magnitude > _magnitudeThreshold)  {
                _coroutine = StartCoroutine(WaitNextCheckData());
                IEnumerator WaitNextCheckData() {
                    Joycon joycon = joycons[0];

                    gyroSave = new List<Vector3>();
                    while (timer < _mesureTime) {
                        timer += Time.deltaTime;
                        gyroSave.Add(joycon.GetAccel());
                        yield return null;
                    }

                    timer = 0;
                    float averageX = gyroSave.Select(i => i.x).Average();
                    float averageY = gyroSave.Select(i => i.y).Average();
                    float averageZ = gyroSave.Select(i => i.z+1).Average();

                    Debug.Log("averageX : " + averageX + " averageY: " + averageY + " averageZ: " + averageZ);

                    
                    if(Mathf.Abs(averageX) > Mathf.Abs(averageY) && 
                        Mathf.Abs(averageX) > Mathf.Abs(averageZ)) {
                        Debug.Log("laser");
                        Attack(actualSkillId);
                    }
                    else {
                        Debug.Log("Je sais pas");
                    }

                    _coroutine = null;
                    yield break;
                }


            }
            

           
        }
    }

    public void OnSkillMenu(InputAction.CallbackContext e) {
        if (e.started && orbs.Count > 0) {

            actualSkillId++;
            Debug.Log("right");
            if (actualSkillId >= orbs.Count)
                actualSkillId = 0;

            weel = GameObject.FindGameObjectWithTag("WEEL").GetComponent<Image>();
            weel.gameObject.SetActive(true);
            weel.enabled = true;
            weel.sprite = orbs[actualSkillId];
        }
    }


    private void Attack(int id) {
        switch(id) {
            case 0: // Lancer Hache
                PlayerAttack.Instance.isShooting = true;
                break;

            case 1:
                PlayerAttack.Instance.OnSkillSpawn();
                break;

            case 2:
                PlayerAttack.Instance.OnSkillHammer();
                break;

            case 3: // Laser
                PlayerAttack.Instance.OnLaserSkill();
                break;

        }
    }

    

   
}
