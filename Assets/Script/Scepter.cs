using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scepter : MonoBehaviour {


    private List<Joycon> joycons;

    [SerializeField]
    private float accelSpeed;

    void Start() {
        joycons = JoyconManager.Instance.j;
    }

    void Update() {

        if (joycons.Count > 0) {

            Joycon joycon = joycons[0];

            if (joycon.GetAccel().x < 0 && joycon.GetAccel().x <= accelSpeed) {
                Debug.Log("attack");
            }
        }
    }
}
