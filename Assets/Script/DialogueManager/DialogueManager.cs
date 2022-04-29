using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public GameObject canSkip;

    public Animator animator;

    //Cree une liste ranger dans l ordre d apparition les objets present
    public Queue<string> sentences;


    private void Start()
    {
        sentences = new Queue<string>();
    }

    /// <summary>
    /// Permet de faire apparaitre la boite de dialogue et d afficher le message avec le nom du PNJ
    /// </summary>
    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    /// <summary>
    /// Permet de passer au texte suivant avec le bouton continue
    /// Ferme la boite de dialogue si il n existe plus aucun texte
    /// </summary>
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        StartCoroutine(CanSkipText());
    }

    /// <summary>
    /// Permet d afficher le texte au fur et a mesure
    /// </summary>
    private IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    /// <summary>
    /// Permet de fermer la boite de dialogue quand il ne reste aucun texte
    /// </summary>
    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }

    private IEnumerator CanSkipText()
    {
        Debug.Log("alo");
        canSkip.SetActive(false);
        yield return new WaitForSeconds(5);
        Debug.Log("aluile");
        canSkip.SetActive(true);
    }
}
