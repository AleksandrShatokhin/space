using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    static private GameController instance;
    static public GameController GetInstance() => instance;

    public SpawnerBox enemiesSpawner;
    private bool shouldSpawnWave = true;
    public GameObject asteroid;
    public float spawnEveryNSeconds = 0.5f;
    public float waitAfterWave = 5.0f;
    public int enemiesInWave = 10;
    public float waitOnStart = 2.0f;

    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartCoroutine(SpawnWave());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        SceneManager.LoadScene("SampleScene");
    }


    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(waitOnStart);

        int enemiesSpawned = 0;

        while (shouldSpawnWave)
        {
            if (enemiesSpawned < enemiesInWave) {
                enemiesSpawner.Spawn(asteroid);
                enemiesSpawned++;
                yield return new WaitForSeconds(spawnEveryNSeconds);
            }
            else {
                enemiesSpawned = 0;
                yield return new WaitForSeconds(waitAfterWave);
            }
        }
    }


    public bool IsGameOver() => isGameOver;
    public void GameOver() => isGameOver = true;
}
