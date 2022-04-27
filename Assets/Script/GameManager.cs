using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player Spawn")]
    [SerializeField] private GameObject playerTutorialSpawn;
    [SerializeField] private GameObject playerGameSpawn;

    [Header("Time Event Deus Power Spawn")]
    [SerializeField] private List<float> minutesPerEvent = new List<float>();

    [Header("Enemy Tutorial Spawn")]
    [SerializeField] private List<Transform> enemyTutorialAutoAttackSpawn = new List<Transform>();
    [SerializeField] private List<Transform> enemyTutorialDeusAttackSpawn = new List<Transform>();
    [SerializeField] private GameObject tutoEnemy;

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
        if (gameState == GameState.InGame)
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
                //Spawn Enemy for auto attack
                break;
            case TutorialState.AutoAttack:
                tutorialState = TutorialState.Interaction;
                //Spawn Deus Orbe
                break;
            case TutorialState.Interaction:
                tutorialState = TutorialState.DeusAttack;
                //Spawn Enemy For test compétence
                break;
            case TutorialState.DeusAttack:
                tutorialState = TutorialState.End;
                gameState = GameState.InGame;
                //Switch GameScene
                break;
            default:
                break;
        }
    }

    private void SpawnEnemyTutorialAutoAttack()
    {
        foreach (Transform enemySpawnPos in enemyTutorialAutoAttackSpawn)
        {
            //EnemyMovement enemy = Instantiate()
        }
    }

    private void SpawnEnemyTutorialDeusAttack()
    {

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
        End
    };

}
