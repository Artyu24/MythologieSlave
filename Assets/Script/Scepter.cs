using System.Collections;
using System.Collections.Generic;
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

    private List<int> gyroXDatas;
    private List<int> gyroZDatas;

    private float timer;
    private bool hasFinishCheck = true;

    public static Scepter Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    void Start() {
        joycons = JoyconManager.Instance.j;

        
    }

    void Update() {

        if (joycons.Count > 0) {

            Joycon joycon = joycons[1];

            //   accel = new Vector3((int)joycon.GetAccel().x, (int)joycon.GetAccel().y, (int)joycon.GetAccel().z);
            accel = joycon.GetAccel();
            gyro = joycon.GetGyro();

            if ((int)joycon.GetAccel().x < 0 && (int)joycon.GetAccel().x <= accelSpeed && hasFinishCheck) {
                Debug.Log("attack 01");
                hasFinishCheck = false;
                StartCoroutine(WaitNextCheckData());
            }

            Debug.Log("gyroX: " + (int)joycon.GetGyro().x + " gyroZ: " + (int)joycon.GetGyro().z );


            if(hasFinishCheck && gyroXDatas.Count > 0) {
                int sumX = 0,sumZ = 0;

                foreach(int xData in gyroXDatas) 
                    sumX += xData;

                int averageX = sumX / gyroXDatas.Count;

                foreach (int zData in gyroZDatas)
                    sumZ += zData;

                int averageZ = sumZ / gyroZDatas.Count;

                gyroXDatas.Clear();
                gyroZDatas.Clear();
            }
        }
    }

    private IEnumerator WaitNextCheckData() {
        yield return null;

        Joycon joycon = joycons[1];

        gyroXDatas.Add((int)joycon.GetGyro().x);
        gyroZDatas.Add((int)joycon.GetGyro().z);

        timer += Time.deltaTime;

        if (timer < 1f)
            StartCoroutine(WaitNextCheckData());
        else {
            timer = 0;
            hasFinishCheck = true;
        }
    }

   
}
