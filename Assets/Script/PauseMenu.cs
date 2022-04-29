using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool isPaused;
    float speedMelee = 0; 
    float speedDistance = 0; 
    float distancePlayerMelee = 0; 
    float distancePlayerDistance = 0;

    public void TogglePause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isPaused)
            {
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

                isPaused = true;
                GameManager.gameState = GameManager.GameState.Paused;
            }
            else
            {
                GameObject[] enemyTab = GameObject.FindGameObjectsWithTag("Enemy");
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

                isPaused = false;
                if (GameManager.tutorialState != GameManager.TutorialState.End)
                {
                    GameManager.gameState = GameManager.GameState.Tuto;
                }
                else if (GameManager.isInBossFight == true)
                {
                    GameManager.gameState = GameManager.GameState.FinalBoss;
                }
                else
                {
                    GameManager.gameState = GameManager.GameState.InGame;
                }
            }
        }

        pauseMenu.SetActive(isPaused);
        
    }
}
