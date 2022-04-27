using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    [Header("Player Spawn")] 
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerTutorialSpawn;
    [SerializeField] private Transform playerGameSpawn;

    [Header("Time Event Deus Power Spawn")]
    [SerializeField] private List<float> minutesPerEvent = new List<float>();

    [Header("Enemy Tutorial Spawn")]
    [SerializeField] private List<Transform> enemyTutorialAutoAttackSpawn = new List<Transform>();
    [SerializeField] private List<Transform> enemyTutorialDeusAttackSpawn = new List<Transform>();
    [SerializeField] private EnemyMovement tutoEnemy;
    [SerializeField] private float distanceBtwPlayerTutorial = 1.3f;
    [SerializeField] private float enemySpeedTutorial = 1;
    public static int nbrEnemyTuto;

    [Header("Orbe Spawn")] 
    [SerializeField] private Transform orbeSpawn;

    private static bool isInitTutoDone;

    private float timeInGame;

    public static TutorialState tutorialState;
    public static GameState gameState;
    public static TutorialState GetTutorialState { get => tutorialState;}
    public static GameState GetGameState { get => gameState; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        gameState = GameState.Tuto;
        tutorialState = TutorialState.Mouvement;
    }

    private void Update()
    {
        if (gameState == GameState.Tuto)
        {
            if (!isInitTutoDone)
            {
                isInitTutoDone = true;
                if (tutorialState == TutorialState.Mouvement)
                {
                    Instantiate(player, playerTutorialSpawn.position, Quaternion.identity);
                }
                else if (tutorialState == TutorialState.AutoAttack)
                {
                    nbrEnemyTuto = enemyTutorialAutoAttackSpawn.Count;
                    foreach (Transform enemySpawnPos in enemyTutorialAutoAttackSpawn)
                    {
                        EnemyMovement enemy = Instantiate(tutoEnemy, enemySpawnPos.position, Quaternion.identity);
                        enemy.DistancePlayer = distanceBtwPlayerTutorial;
                        enemy.Speed = enemySpeedTutorial;
                    }
                }
                else if (tutorialState == TutorialState.Interaction)
                {
                    Instantiate(OrbePowerManagement.listOrbeFinal[0], orbeSpawn.position, Quaternion.identity);
                }
                else if (tutorialState == TutorialState.DeusAttack)
                {
                    nbrEnemyTuto = enemyTutorialDeusAttackSpawn.Count;
                    foreach (Transform enemySpawnPos in enemyTutorialDeusAttackSpawn)
                    {
                        EnemyMovement enemy = Instantiate(tutoEnemy, enemySpawnPos.position, Quaternion.identity);
                        enemy.DistancePlayer = distanceBtwPlayerTutorial;
                        enemy.Speed = enemySpeedTutorial;
                    }
                }
            }
        }
        else if (gameState == GameState.InGame)
        {
            timeInGame += Time.deltaTime;

            for(int i = 0; i < minutesPerEvent.Count;)
            {
                if (timeInGame > minutesPerEvent[i] && minutesPerEvent[i] != 0)
                {
                    minutesPerEvent[i] = 0;
                    //Appeler fonction de spawn de la boule de dieu dans les temples
                }
            }
        }
    }

    public static void UpdateTutorial()
    {
        switch (tutorialState)
        {
            case TutorialState.Mouvement:
                tutorialState = TutorialState.AutoAttack;
                isInitTutoDone = false;
                //Spawn Enemy for test
                break;
            case TutorialState.AutoAttack:
                tutorialState = TutorialState.Interaction;
                isInitTutoDone = false;
                //Spawn Deus Orbe
                break;
            case TutorialState.Interaction:
                tutorialState = TutorialState.DeusAttack;
                isInitTutoDone = false;
                //Spawn Enemy For test Deus Attack
                break;
            case TutorialState.DeusAttack:
                tutorialState = TutorialState.GoToForest;
                //Go to the end of the forest
                break;
            case TutorialState.GoToForest:
                tutorialState = TutorialState.End;
                gameState = GameState.InGame;
                //Switch GameScene
                break;
            default:
                break;
        }
    }

    private IEnumerator WaitBeforeSpawn()
    {
        yield return new WaitForSeconds(2);
        isInitTutoDone = false;
    }

    public enum GameState
    {
        Tuto,
        InGame,
        Paused,
        End
    };

    public enum TutorialState
    {
        Mouvement,
        AutoAttack,
        Interaction,
        DeusAttack,
        GoToForest,
        End
    };

}
