using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [Header("Competence 2")]
    public int cooldownTwo;
    private float timerTwo;
    [HideInInspector]
    public bool isInCooldownTwo;


    void Start() {
        
    }

    void Update() {
        
        if(isInCooldownTwo) {
            timerTwo += Time.deltaTime;

            if(timerTwo >= cooldownTwo) {
                isInCooldownTwo = false;
                timerTwo = 0.0f;
            }
        }
    }

    void Attack(int attackID) {

    }

   
}
