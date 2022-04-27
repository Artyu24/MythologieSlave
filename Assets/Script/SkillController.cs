using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour {

    private List<Joycon> joycons;

    public Vector3 gyro;

    void Start() {
        joycons = JoyconManager.Instance.j;
    }

    void Update() {
        if(joycons.Count > 0) {
            Joycon joycon = joycons[0];
            gyro = new Vector3((int)joycon.GetGyro().x, (int)joycon.GetGyro().y, (int)joycon.GetGyro().z);

            /*
            X = Devant  / derriere
            Z = Droite Gauche
            Y = haut / bas
           */
            if(joycon.GetGyro().x <= -8.0f && joycon.GetGyro().x < 0f) {
                Debug.Log("attack laser");
                return;
            }

            if (joycon.GetGyro().z <= -8.0f && joycon.GetGyro().z < 0f) {
                Debug.Log("attack foudre");
                return;
            }

        }
    }
}
