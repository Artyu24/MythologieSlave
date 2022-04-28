using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemTracker : MonoBehaviour {

    private GameObject actualTotem;
    private bool display;


    void Update() {
       if(display) {
            transform.LookAt(actualTotem.transform);

       } 
    }

    public void StartTracker(GameObject totem) {
        actualTotem = totem;
        display = true;
    }
}
