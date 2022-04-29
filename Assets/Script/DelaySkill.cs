using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaySkill : MonoBehaviour {

    private float timer;

    public float maxTimer;

    public bool follow;

    void Update() {

        if (follow)
            transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;

        timer += Time.deltaTime;

        if (timer >= maxTimer)
            Destroy(transform.gameObject);
            
    }
}
