using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    static private GameController instance;
    static public GameController GetInstance() => instance;

    public SpawnerBox enemiesSpawner;
    public GameObject postGame;

    private bool shouldSpawnWave = true;
    public GameObject asteroid;
    public float spawnEveryNSeconds = 0.5f;
    public float waitAfterWave = 5.0f;
    public int enemiesInWave = 10;
    public int numberOfWaves = 3;
    public float waitOnStart = 2.0f;
    private int allEnemiesSpawned = 0;
    private int allEnemies;

    private GameObject lastEnemy;

    private bool isGameOver = false;
    private bool isLevelEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartCoroutine(SpawnWave());
        allEnemies = enemiesInWave * numberOfWaves;
    }

    // Update is called once per frame
    void Update()
    {
        Restart();
        CheckIfLevelEnd();

    }


    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(waitOnStart);

        int enemiesSpawned = 0;

        while (shouldSpawnWave)
        {
            if (enemiesSpawned < enemiesInWave) {
                GameObject spawned = enemiesSpawner.Spawn(asteroid);
                enemiesSpawned++;
                allEnemiesSpawned++;


                //Сохранить последнего противника  из уровня
                lastEnemy = allEnemiesSpawned == allEnemies ? spawned : null;

                yield return new WaitForSeconds(spawnEveryNSeconds);
            }
            else {
                enemiesSpawned = 0;
                yield return new WaitForSeconds(waitAfterWave);
            }
        }
    }


    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void GameOver()
    {
        isGameOver = true;
        _ = Instantiate(postGame);
    }

    public void LevelEnded()
    {
        isLevelEnded = true;
        _ = Instantiate(postGame);
    }

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }




    void CheckIfLevelEnd()
    {
        if (isLevelEnded)
        {
            return;
        }

        if (allEnemies == allEnemiesSpawned)
        {
            if (lastEnemy == null)
            {
                LevelEnded();
            }
        }
        
    }


    public bool IsLevelEnded()
    {
        return isLevelEnded;
    }
}
