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

    [Header("Enemy Tutorial Spawn")]
    [SerializeField] private List<Transform> enemyTutorialAutoAttackSpawn = new List<Transform>();
    [SerializeField] private List<Transform> enemyTutorialDeusAttackSpawn = new List<Transform>();
    [SerializeField] private EnemyIA tutoEnemy;
    [SerializeField] private float distanceBtwPlayerTutorial = 1.3f;
    [SerializeField] private float enemySpeedTutorial = 1;
    public static int nbrEnemyTuto;

    [Header("Orbe Spawn Tutorial")] 
    [SerializeField] private Transform orbeSpawn;

    [Header("End Tutorial")] 
    [SerializeField] private BoxCollider2D entranceOfForest;

    [Header("List of Event by Time")]
    [SerializeField] private List<float> minutesPerEventList = new List<float>();

    [Header("Enemy")] 
    [SerializeField] private AnimationCurve enemySpawnEvolution;
    [SerializeField] private float enemyToKill = 20;
    [SerializeField] private float delayStartBtwEachEnemy = 1;
    [SerializeField] private float delayEndBtwEachEnemy = 0.5f;
    public static int enemyKill;
    public static Vector3 lastEnemyKillPos;

    [Header("Chest")] 
    [SerializeField] private GameObject silverChest;
    [SerializeField] private GameObject goldChest;
    private int nbrSilverChest;


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
                    player = Instantiate(player, playerTutorialSpawn.position, Quaternion.identity);
                }
                else if (tutorialState == TutorialState.AutoAttack)
                {
                    StartCoroutine(WaitBeforeSpawn());
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
                        EnemyIA enemy = Instantiate(tutoEnemy, enemySpawnPos.position, Quaternion.identity);
                        enemy.DistancePlayer = distanceBtwPlayerTutorial;
                        enemy.MovSpeed = enemySpeedTutorial;
                    }
                }
                else if (tutorialState == TutorialState.GoToForest)
                {
                    entranceOfForest.isTrigger = true;
                }
            }
        }
        else if (gameState == GameState.InitGame && GameObject.FindGameObjectWithTag("PlayerSpawn") != null)
        {
            player.transform.position = GameObject.FindGameObjectWithTag("PlayerSpawn").transform.position;
            gameState =  GameState.InGame;
        }
        else if (gameState == GameState.InGame)
        {
            timeInGame += Time.deltaTime;

            EnemySpawnManager.timeBtwEachSpawn = Mathf.Lerp(delayStartBtwEachEnemy, delayEndBtwEachEnemy, enemySpawnEvolution.Evaluate(timeInGame));

            for(int i = 0; i < minutesPerEventList.Count;)
            {
                if (timeInGame > minutesPerEventList[i] && minutesPerEventList[i] != 0)
                {
                    minutesPerEventList[i] = 0;
                    //Appeler fonction de spawn de la boule de dieu dans les temples
                }
            }

            if (enemyKill >= enemyToKill)
            {
                enemyToKill = enemyKill + enemyToKill * 1.5f;
                if (nbrSilverChest == 3)
                {
                    nbrSilverChest = 0;
                    Instantiate(goldChest, lastEnemyKillPos, Quaternion.identity);
                }
                else
                {
                    nbrSilverChest++;
                    Instantiate(silverChest, lastEnemyKillPos, Quaternion.identity);
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
                isInitTutoDone = false;
                //Go to the end of the forest
                break;
            case TutorialState.GoToForest:
                tutorialState = TutorialState.End;
                gameState = GameState.InitGame;
                //Switch GameScene
                break;
            default:
                break;
        }
    }

    private IEnumerator WaitBeforeSpawn()
    {
        yield return new WaitForSeconds(2);
        nbrEnemyTuto = enemyTutorialAutoAttackSpawn.Count;
        foreach (Transform enemySpawnPos in enemyTutorialAutoAttackSpawn)
        {
            EnemyIA enemy = Instantiate(tutoEnemy, enemySpawnPos.position, Quaternion.identity);
            enemy.DistancePlayer = distanceBtwPlayerTutorial;
            enemy.MovSpeed = enemySpeedTutorial;
        }
    }

    public enum GameState
    {
        Tuto,
        InitGame,
        InGame,
        FinalBoss,
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
