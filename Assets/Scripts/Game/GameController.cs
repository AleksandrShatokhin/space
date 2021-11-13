using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    static private GameController instance;
    static public GameController GetInstance() => instance;


    //Параметр для контроля спауна волн.
    //Можно отключить извне для тестов
    public  bool shouldSpawnWave = true;

    public SpawnerBox spawner;
    public GameObject postGame;

    //Массив для противников
    public GameObject[] enemies;

    
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

                //Спаун случайного противника
                GameObject spawned = spawner.Spawn(GetRandomEnemy());
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



    //Выбор случайного противника из списка
    private GameObject GetRandomEnemy()
    {
        return enemies[Random.Range(0, enemies.Length)];
    }
}
