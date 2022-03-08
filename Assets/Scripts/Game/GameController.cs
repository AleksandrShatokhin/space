using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Синглтон на GameController
    static private GameController instance;
    static public GameController GetInstance() => instance;
    private PlayerController player;

    public SpawnerBox spawner;

    //Ссылка на префаб меню
    public GameObject postGame;

    //Массив для противников
    public GameObject[] enemies;

    //Массив инфлюенсеров - бонусов и дебафов
    public GameObject[] influencers;

    //Основные параметры игры
    public float spawnEveryNSeconds = 0.5f;
    public float waitAfterWave = 5.0f;
    public int enemiesInWave = 10;
    public int numberOfWaves = 3;
    public float waitOnStart = 2.0f;
    private int allEnemiesSpawned = 0;
    private int allEnemies;

    private int enemiesKilled = 0;

    //Частота спауна бонусов в игре
    public float influencerSpawnRate = 5.0f;

    //Вероятность спауна бонуса 1 = 100%
    public float influencerChance = 1.0f;

    private GameObject lastEnemy;

    //Состояния игры
    private bool isGameOver = false;
    private bool isLevelEnded = false;
    private bool isInfluencerTryToSpawn = false;

    //Параметр для контроля спауна волн.
    //Можно отключить извне для тестов
    public bool shouldSpawnWave = true;

    //Аудио свойства
    private AudioSource audioSource;
    public AudioClip failSound;
    public AudioClip successSound;
    public AudioClip backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartCoroutine(SpawnWave());
        allEnemies = enemiesInWave * numberOfWaves;

        //Сохранить ссылку на игрока
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        StartCoroutine(AddBullets());
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();  
        audioSource.clip = backgroundMusic;
        audioSource.volume = 0.1f;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //Проверка на нажатие клавиши R для перезапуска
        Restart();
        //Проверка, что уровень завершен
        CheckIfLevelEnd();
    }


    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(waitOnStart);

        int enemiesSpawned = 0;

        while (shouldSpawnWave)
        {
            if (!isInfluencerTryToSpawn)
            {
                _ = StartCoroutine(SpawnBonus());
            }

            if (enemiesSpawned < enemiesInWave) {

                //Спаун случайного противника
                GameObject spawned = spawner.Spawn(GetRandomEnemy());
                enemiesSpawned++;
                allEnemiesSpawned++;


                //Сохранить последнего противника  из уровня
                lastEnemy = allEnemiesSpawned == allEnemies ? spawned : null;

                yield return new WaitForSeconds(spawnEveryNSeconds);
            }
            else if(enemiesKilled < allEnemiesSpawned){
                yield return new WaitForSeconds(waitAfterWave);
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
        float waitSec = 1;
        isGameOver = true;
        PlaySound(failSound);
        StartCoroutine(PostGame(waitSec));
    }

    public void LevelEnded()
    {
        float waitSec = 2;
        isLevelEnded = true;
        PlaySound(successSound);
        StartCoroutine(PostGame(waitSec));
    }

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    
    void CheckIfLevelEnd()
    {
        if (isLevelEnded)
        {
            return;
        }

        if (allEnemies == allEnemiesSpawned && enemiesKilled == allEnemiesSpawned)
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
    
    IEnumerator SpawnBonus()
    {
        //Сразу выставляем признак, что не нужно запускать следующий спаун
        //Так как надо сначала выждать паузу, а затем уже 
        isInfluencerTryToSpawn = true;

        //Сначала запускаем паузу. Объект должен добавиться после ожидания. 
        yield return new WaitForSeconds(influencerSpawnRate);

        //Для расчета вероятности воспользуемся Random.Range
        float randomSeed = Random.Range(0.0f, 1.0f);

        //Если полученное значение входит в допустимую вероятность, то можно спаунить объект
        if(randomSeed <= influencerChance)
        {
            spawner.Spawn(GetInfluencer());
        }

        isInfluencerTryToSpawn = false;
    }

    //Получить случайный инфлюенсер
    GameObject GetInfluencer()
    {
        return influencers[Random.Range(0, influencers.Length)];
    }


    //Для контроля над спауном нужно подсчитывать кол-во уничтоженных врагов
    //При уничтожении нужно вызывать данный метод
    public void AddKilledEnemy()
    {
        enemiesKilled++;
        Debug.Log("Killed enemies:" + enemiesKilled);
        Debug.Log("All enemies " + allEnemiesSpawned);
    }


    public PlayerController GetPlayer() => player;


    IEnumerator AddBullets()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            foreach (var weapon in player.weapons)
            {
                if (weapon.GettingBulletsByTime())
                {
                    weapon.AddBullets(1);
                }
            }
        }

    }


    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {

        if (!clip || !audioSource) {
            return;
        }


        audioSource.PlayOneShot(clip, volume);
    }


    IEnumerator PostGame(float time = 2)
    {
        yield return new WaitForSeconds(time);
        _ = Instantiate(postGame);
    }

}
