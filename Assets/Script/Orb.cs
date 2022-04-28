using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orb : MonoBehaviour
{
    //public enum OrbType
    //{
    //    FEU,
    //    FERTILITE,
    //    FORGERON,
    //    FOUDRE
    //}

    //public OrbType orb;
    //public string dialogName;


    //public void UnlockOrb() {
    //    switch(orb) {
    //        case OrbType.FEU:
    //            PlayerAttack.Instance.hasThunderSkill = true;
    //            break;

    //        case OrbType.FERTILITE:
    //            PlayerAttack.Instance.hasFertilitySkill = true;
    //            break;

    //        case OrbType.FORGERON:
    //            PlayerAttack.Instance.hasHammerSkill = true;
    //            break;

    //        case OrbType.FOUDRE:
    //            PlayerAttack.Instance.hasRaySkill = true;
    //            break;
    //    }
    //}

    public Dialogue dialogue;
    private Image deusImg;
    [SerializeField] private Sprite deusSprite;

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

            StartCoroutine(StopDialogue());

        }
    }

    private IEnumerator StopDialogue()
    {
        if (GameManager.gameState != GameManager.GameState.Tuto)
            GameManager.gameState = GameManager.GameState.Paused;
        
        GameObject[] enemyTab = GameObject.FindGameObjectsWithTag("Enemy");
        float speedMelee = 0;
        float speedDistance = 0;
        float distancePlayerMelee = 0;
        float distancePlayerDistance = 0;

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

        yield return new WaitForSeconds(3);
        
        FindObjectOfType<DialogueManager>().EndDialogue();
        if (GameManager.gameState == GameManager.GameState.Tuto && GameManager.tutorialState == GameManager.TutorialState.Interaction)
        {
            GameManager.UpdateTutorial();
            OrbePowerManagement.actualOrb++;
        }
        else
        {
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
