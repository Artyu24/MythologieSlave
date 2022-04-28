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

    private void Awake() {
        Instance = this;
    }

    void Start() {
        joycons = JoyconManager.Instance.j;
    }

    void Update() {

        if (joycons.Count > 0) {

            Joycon joycon = joycons[0];

            //   accel = new Vector3((int)joycon.GetAccel().x, (int)joycon.GetAccel().y, (int)joycon.GetAccel().z);
            accel = joycon.GetAccel();
            gyro = joycon.GetGyro();

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

                    // Decide sort
                    if(Mathf.Abs(averageY) > Mathf.Abs(averageX) && 
                        Mathf.Abs(averageY) > Mathf.Abs(averageZ) && 
                        averageY < 0) {
                        Debug.Log("Tonnerre !");
                        Attack(0);
                    }
                    else if(Mathf.Abs(averageX) > Mathf.Abs(averageY) && 
                        Mathf.Abs(averageX) > Mathf.Abs(averageZ) &&
                        averageX > 0) {
                        Attack(4);
                    }
                    else if(Mathf.Abs(averageZ) > Mathf.Abs(averageY) &&
                        Mathf.Abs(averageZ) > Mathf.Abs(averageX)) {
                        if(averageZ < 0) {
                            Debug.Log("fertilité");
                            Attack(2);
                        }
                        else {
                            Debug.Log("marteau");
                            Attack(3);
                        }
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

    private void Attack(int id) {
        switch(id) {
            case 0: // Lancer Hache
                PlayerAttack.Instance.isShooting = true;
                break;

            case 1:
                break;

            case 2:
                break;

            case 4: // Laser
                PlayerAttack.Instance.OnLaserSkill();
                break;

        }
    }

    

   
}
