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

    public Color spell1;
    public Color spell2;
    public Color spell3;
    public Color spell4;

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

            if (joycon.GetAccel().x < 0 && joycon.GetAccel().x <= accelSpeed) {
                Debug.Log("attack");
            }
        }
    }

    public void OnScrollLeft(InputAction.CallbackContext e) {
        if(e.started) {
            Debug.Log("left");
        }
    }

    public void OnScrollRight(InputAction.CallbackContext e)  {
        if(e.started) {
            Debug.Log("right");

            actualSpellID++;

            if (actualSpellID >= spellsImage.Count)
                actualSpellID = spellsImage.Count - 1;

            foreach (Image image in spellsImage)
                image.gameObject.SetActive(false);

            spellsImage[actualSpellID].gameObject.SetActive(true);


        }
    }
}
