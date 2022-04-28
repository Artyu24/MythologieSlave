using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotemTracker : MonoBehaviour {

    private GameObject actualTotem;
    private bool display;
    [SerializeField] float angleOffset = 0;

    public static TotemTracker Instance { get; private set; }

    public Sprite trackerSprite;

    private void Awake() {
        Instance = this;
    }

    void Update() {
       if(display) {
            Vector3 distance = (actualTotem.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position);

            float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg + angleOffset;

            distance = distance.normalized;
            Vector3 worldPos = GameObject.FindGameObjectWithTag("Player").transform.position + distance * 1.5f;
            transform.position = Camera.main.WorldToScreenPoint(worldPos);

            transform.eulerAngles = new Vector3(0,0,angle);
       } 
    }

    public void StartTracker(GameObject totem) {
        actualTotem = totem;
        display = true;

        transform.gameObject.GetComponent<Image>().sprite = trackerSprite;
    }

    public void StopTracker() {
        actualTotem = null;
        display = false;

        transform.gameObject.GetComponent<Image>().sprite = null;
    }
}
