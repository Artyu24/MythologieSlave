using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour {


    void Update() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance > PlayerAttack.Instance.radiusArea)
            Destroy(transform.gameObject); 
    }
}
