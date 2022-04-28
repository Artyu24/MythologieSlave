using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Dialog {

    public int id;
    public string Author;
    public string Name;
    public string Sprite;
    public string[] Content;
    public bool isFinish;
}

[System.Serializable]
public class DialogArray {
    public Dialog[] dialogs;
}

public class DialogController : MonoBehaviour {

    public float speed;
    public bool isInDialog;
    public Text text;
    public Text authorName;
    public Image godImage;
    public GameObject nextObj;
    public GameObject dialogObj;
    public DialogArray dialogArray;
    public bool finish = false;
    public Dialog currentDialog;

    private bool nextPage;
    private int index = 0;

    private int answer = 0;

    private bool hasReturnToMainMenu;

    public TextAsset dialogFile;

    public static DialogController Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    void Start() {
        dialogArray = JsonUtility.FromJson<DialogArray>(dialogFile.text);
    }

    public void OnInteract(InputAction.CallbackContext e) { // APPELEZ QUAND LE JOUEUR APPUIE SUR E

        if (e.started) {
            if (finish)  {
                EndDialog();
                Destroy(GameObject.FindGameObjectWithTag("Totem"));

            }
        }

    }

    public IEnumerator ShowText(string displayText, int length) {
        nextPage = false;
        dialogObj.SetActive(true);
        text.gameObject.SetActive(true);

        authorName.text = currentDialog.Author;

        Debug.Log("spriteName " + Resources.Load<Sprite>(currentDialog.Sprite));

        godImage.sprite = Resources.Load<Sprite>(currentDialog.Sprite);

        for (int i = 1; i < displayText.Length; i++) {
            yield return new WaitForSeconds(speed);
            text.text = displayText.Substring(0, i);
        }

        finish = true;

    }

    public void EndDialog() {
        dialogObj.SetActive(false);
        if (nextObj != null)
            nextObj.SetActive(false);

        text.gameObject.SetActive(false);
        nextPage = false;
        index = 0;
        answer = 0;
        text.text = "";
        isInDialog = false;
        finish = false;
        currentDialog.isFinish = true;

    }

    public Dialog GetDialogByName(string name) {
        foreach (var dialog in dialogArray.dialogs) {
            if (dialog.Name == name)
                return dialog;
        }

        return null;
    }

}