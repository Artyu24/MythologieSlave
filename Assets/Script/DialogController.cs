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
    public string[] Content;
    public bool NeedAnswer;
    public string[] Answers;
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
    public GameObject nextObj;
    public GameObject dialogObj;
    public DialogArray dialogArray;
    public bool finish = false;
    public GameObject answerObj;
    public Text textAnswer;
    public GameObject arrowObj;
    public Dialog currentDialog;

    private bool nextPage;
    private int index = 0;

    private int answer = 0;

    private bool hasReturnToMainMenu;

    // Events
    public event EventHandler<OnDialogEndArgs> OnDialogEnd;
    public class OnDialogEndArgs : EventArgs
    {
        public Dialog dialog;
        public GameObject actualPlayer;
        public Vector3 position;
        public GameObject obj;
        public int answerIndex;
    }

    void Start() {
    }

    public void OnInteract(InputAction.CallbackContext e) { // APPELEZ QUAND LE JOUEUR APPUIE SUR E

        if (e.started) {
            if (isInDialog)
                AccelerateDialog(0.01f); // Faire en sorte de d�celerrer   

            if (nextPage && isInDialog && !finish) {
                nextPage = false;
                nextObj.SetActive(false);
                StartCoroutine(ShowText(currentDialog.Content[index], currentDialog.Content.Length));
            }

            if (finish)  {
                if (answer >= 0) { // LE JOUEUR A UN CHOIX A FAIRE
                    OnDialogEndArgs args = new OnDialogEndArgs { dialog = null, actualPlayer = null, position = Vector3.zero, obj = null, answerIndex = -1 };
                    
                    EndDialog();

                    OnDialogEnd?.Invoke(this, args); // Call only if OnDialogEnd is null ; you should write if(OnDialogEnd != null) ....

                }
                else { // LE JOUEUR NA PAS DE CHOIX A FAIRE
                    if (!answerObj.activeSelf) {

                        EndDialog(); 
                    }
                }

            }
        }

    }

    public void OnNext(InputAction.CallbackContext e) { // Appel� quand le joueur change vers le bas de choix de r�ponse
        if (e.started) {
            if (answerObj.activeSelf && isInDialog) {
                if (answer < currentDialog.Answers[0].Split('\n').Length) {
                    arrowObj.SetActive(true);
                    arrowObj.transform.localPosition = new Vector3(arrowObj.transform.localPosition.x, 15, 0);
                    answer++;
                }
            }
        }
    }

    public void OnPrevious(InputAction.CallbackContext e) { // Appel� quand le joueur change vers le haut de choix de r�ponse 
        if (e.started) {
            if (answerObj.activeSelf && isInDialog) {
                if (answer >= 1) {
                    arrowObj.SetActive(true);
                    arrowObj.transform.localPosition = new Vector3(arrowObj.transform.localPosition.x, 66, 0);
                    answer--;
                }
            }
        }
    }

    void AccelerateDialog(float accelerate) {
        this.speed = accelerate;
    }

    public IEnumerator ShowText(string displayText, int length) {
        nextPage = false;
        dialogObj.SetActive(true);
        text.gameObject.SetActive(true);
        answerObj.SetActive(false);
        textAnswer.gameObject.SetActive(false);
        arrowObj.SetActive(false);

        for (int i = 1; i < displayText.Length; i++) {
            yield return new WaitForSeconds(speed);
            text.text = displayText.Substring(0, i);
        }

        if (length > 1 && index < length) {
            nextPage = true;
            index += 1;
            nextObj.SetActive(true);
        }

        if ((length > 1 && index == length) || (length == 1)) {
            if (currentDialog.NeedAnswer) {
                answerObj.SetActive(true);
                textAnswer.gameObject.SetActive(true);
                arrowObj.SetActive(true);

                textAnswer.text = currentDialog.Answers[0];
            }

            finish = true;

        }

    }

    public void EndDialog() {
        dialogObj.SetActive(false);
        if (nextObj != null)
            nextObj.SetActive(false);

        text.gameObject.SetActive(false);
        answerObj.SetActive(false);
        textAnswer.gameObject.SetActive(false);
        arrowObj.SetActive(false);
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