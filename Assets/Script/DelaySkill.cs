using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaySkill : MonoBehaviour {

    private float timer;

    public float maxTimer;

    void Update() {
        timer += Time.deltaTime;

        if (timer >= maxTimer)
            Destroy(transform.gameObject);
            
    }
}
