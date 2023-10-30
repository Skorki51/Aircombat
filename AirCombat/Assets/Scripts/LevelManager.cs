using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private Vector3 spawnPosition;
    public int playerPoints = 0;

    [SerializeField] private List<GameObject> enemiesPrefabs = new List<GameObject>();
    private int randomEnemy = 0;
    private float randomXSpawnRange = 0f;
    private bool enemyCanSpawn = true;
    private EnemyController[] spawnedEnemies;



    [SerializeField] private List<GameObject> itemsPrefabs = new List<GameObject>();
    [SerializeField] private int chanceToSpawnLife = 0;
    [SerializeField] private int chanceToSpawnUpgrade = 0;
    private int randomReasult = 0;
    private bool itemCanSpawn = true;
    [SerializeField] private float delayBetweenSpawnEnemy = 2.5f;
    [SerializeField] private int maxEnemyAmount = 5;
    [SerializeField] private float delayBetweenSpawnItem = 5f;
    [SerializeField] private Text playerPointsTextBox;

    [SerializeField] private int difficultyStepOne = 10;
    private int difficultyStepOneSave = 0;
    [SerializeField] private int difficultyStepTwo = 5;
    private int difficultyStepTwoSave = 0;
    void Start()
    {
        instance = this;
        difficultyStepOneSave = difficultyStepOne;
        difficultyStepTwoSave = difficultyStepTwo;
        playerPointsTextBox.text = playerPoints.ToString();
    }

    private void Update()
    {
        spawnedEnemies = FindObjectsOfType<EnemyController>();

        if (spawnedEnemies.Length < maxEnemyAmount && enemyCanSpawn)
        {
            enemyCanSpawn = false;
            StartCoroutine(EnemySpawn());
        }

        if (itemCanSpawn)
        {
            itemCanSpawn = false;
            StartCoroutine(ItemsSpawner());
        }

        if (playerPoints >= difficultyStepOne)
        {
            difficultyStepOne = difficultyStepOneSave + difficultyStepOne;
            ChangeDifficultyOne();
        }
        if (playerPoints >= difficultyStepTwo)
        {
            difficultyStepTwo = difficultyStepTwoSave + difficultyStepTwo;
            ChangeDifficultyTwo();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    private IEnumerator ItemsSpawner()
    {
        randomReasult = Random.Range(0, 101);
        if (randomReasult <= chanceToSpawnLife)
        {
            randomXSpawnRange = Random.Range(-8f, 8f);
            spawnPosition = new Vector3(randomXSpawnRange, 5.5f, 0);
            Instantiate(itemsPrefabs[0], spawnPosition, Quaternion.Euler(0, 0, 0));
        }
        else if (randomReasult <= chanceToSpawnLife + chanceToSpawnUpgrade)
        {
            randomXSpawnRange = Random.Range(-8f, 8f);
            spawnPosition = new Vector3(randomXSpawnRange, 5.5f, 0);
            Instantiate(itemsPrefabs[1], spawnPosition, Quaternion.Euler(0, 0, 0));
        }
        yield return new WaitForSeconds(delayBetweenSpawnItem);
        itemCanSpawn = true;
    }
    private IEnumerator EnemySpawn()
    {
        randomEnemy = Random.Range(0, enemiesPrefabs.Count);
        randomXSpawnRange = Random.Range(-8f, 8f);
        spawnPosition = new Vector3(randomXSpawnRange, 5.5f, 0);
        Instantiate(enemiesPrefabs[randomEnemy], spawnPosition, Quaternion.Euler(0, 0, 180));

        yield return new WaitForSeconds(delayBetweenSpawnEnemy);
        enemyCanSpawn = true;
    }

    public void ChangePlayerPoints(int pointValue)
    {
        playerPoints += pointValue;
        playerPointsTextBox.text = playerPoints.ToString();
    }

    private void ChangeDifficultyOne()
    {
        delayBetweenSpawnEnemy -= 0.25f;
    }
    private void ChangeDifficultyTwo()
    {
        maxEnemyAmount += 1;
    }
}
