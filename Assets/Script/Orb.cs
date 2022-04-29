using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Orb : MonoBehaviour
{
    public Dialogue dialogue;
    private Image deusImg;
    [SerializeField] private Sprite deusSprite;
    [SerializeField] private Sprite tutorialSprite;

    float speedMelee = 0;
    float speedDistance = 0;
    float distancePlayerMelee = 0;
    float distancePlayerDistance = 0;

    private bool wantSkipDialogue;
    private bool canSkipDialogue;

    private void Update()
    {
        if (wantSkipDialogue && canSkipDialogue)
        {
            GameObject[] enemyTab = GameObject.FindGameObjectsWithTag("Enemy");

            FindObjectOfType<DialogueManager>().EndDialogue();
            if (GameManager.gameState == GameManager.GameState.Tuto && GameManager.tutorialState == GameManager.TutorialState.Interaction)
            {
                Debug.Log("Update Tuto");
                GameManager.UpdateTutorial();
                OrbePowerManagement.actualOrb++;
            }
            else if (GameManager.gameState != GameManager.GameState.Tuto)
            {
                Debug.Log("On est en jeu");
                GameManager.gameState = GameManager.GameState.InGame;
                //TotemTracker.Instance.StopTracker();
            }

            foreach (GameObject enemy in enemyTab)
            {
                EnemyIA enemyIA = enemy.GetComponent<EnemyIA>();
                if (enemyIA.isDistanceAttack)
                {
                    enemyIA.MovSpeed = speedDistance;
                    enemyIA.DistancePlayer = distancePlayerDistance;
                }
                else
                {
                    enemyIA.MovSpeed = speedMelee;
                    enemyIA.DistancePlayer = distancePlayerMelee;
                }
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject.GetComponent<CircleCollider2D>());

            deusImg = GameObject.FindGameObjectWithTag("DeusImage").GetComponent<Image>();
            deusImg.sprite = deusSprite;

            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

            if (gameObject.CompareTag("Fertilite"))
                PlayerAttack.Instance.hasFertilitySkill = true;
            else if (gameObject.CompareTag("Thunder"))
                PlayerAttack.Instance.hasThunderSkill = true;
            else if (gameObject.CompareTag("Fire"))
                PlayerAttack.Instance.hasRaySkill = true;
            else if (gameObject.CompareTag("Forge"))
                PlayerAttack.Instance.hasHammerSkill = true;

            PlayerUI.Instance.StartAffichageUITutorial(tutorialSprite);

            StartCoroutine(StopDialogue());

        }
    }

    private IEnumerator StopDialogue()
    {
        if (GameManager.gameState != GameManager.GameState.Tuto)
            GameManager.gameState = GameManager.GameState.Paused;
        
        GameObject[] enemyTab = GameObject.FindGameObjectsWithTag("Enemy");
        speedMelee = 0;
        speedDistance = 0;
        distancePlayerMelee = 0;
        distancePlayerDistance = 0;

        foreach (GameObject enemy in enemyTab)
        {
            EnemyIA enemyIA = enemy.GetComponent<EnemyIA>();
            if (enemyIA.isDistanceAttack)
            {
                speedDistance = enemyIA.MovSpeed;
                distancePlayerDistance = enemyIA.DistancePlayer;
            }
            else
            {
                speedMelee = enemyIA.MovSpeed;
                distancePlayerMelee = enemyIA.DistancePlayer;
            }

            enemyIA.MovSpeed = 0;
            enemyIA.DistancePlayer = 0;
        }

        yield return new WaitForSeconds(5);
        canSkipDialogue = true;
    }

    public void SkipDialogue(InputAction.CallbackContext context)
    {
        if (context.started)
            wantSkipDialogue = true;
        else if (context.canceled)
            wantSkipDialogue = false;
    }
}
