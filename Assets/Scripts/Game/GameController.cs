using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    //Синглтон на GameController
    static protected GameController instance;
    static public GameController GetInstance() => instance;
    protected PlayerController player;

    public int debugLevelNumber = 0;

    [SerializeField]
    static private int levelNumber;

    public SpawnerBox spawner;

    //Ссылка на префаб меню
    public GameObject postGame;

    //Основные параметры игры
    // public float spawnEveryNSeconds = 0.5f;
    public float waitAfterWave = 5.0f;

    public float waitOnStart = 2.0f;
    private int allEnemiesSpawned = 0;
    private int allEnemies;

    private int enemiesKilled = 0;

    private GameObject lastEnemy;
    private GameObject lastSpawnedEnemy;

    //Состояния игры
    private bool isGameOver = false;
    private bool isLevelEnded = false;
    private bool isInfluencerTryToSpawn = false;

    [SerializeField] private bool isBossMode = false;

    //Параметр для контроля спауна волн.
    //Можно отключить извне для тестов
    public bool shouldSpawnWave = true;

    [SerializeField]
    private LevelData[] levelsData;
    public LevelData levelData;

    //Аудио свойства
    private AudioSource audioSource;
    public AudioClip failSound;
    public AudioClip successSound;
    public AudioClip backgroundMusic;

    [SerializeField]
    private float minPlanetSize = 1;
    [SerializeField]
    private float maxPlanetSize = 1.5f;

    private Vector3 moveBounds;

    private GameObject boss;

    [SerializeField] private GameObject pausePrefab;
    [SerializeField] GameObject MainUIObject;

    [SerializeField]
    private Vector3 bossSpawnLocation;

    private bool bossWasSpawned = false;

    private bool bossInWaitingToReturn;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    public static int LevelNumber { get => levelNumber; set => levelNumber = value; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        instance = this;

        //Получить данные по номеру уровня
        if (debugLevelNumber > 0)
        {
            LevelNumber = debugLevelNumber;
        }

        levelData = levelsData[LevelNumber];
        isBossMode = levelData.BossLevel;

        allEnemies = levelData.EnemiesInWave * levelData.NumberOfWaves;

        SceneObjectCreate();
        SpawnBoss();
        StartCoroutine(SpawnWave());

        //Сохранить ссылку на игрока
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        StartCoroutine(AddBullets());
    }

    protected void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.volume = 0.1f;
        audioSource.Play();
        instance = this;
    }

    // Update is called once per frame
    protected virtual void Update()
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

            //Если сейчас идет бой с боссом, то процесс спауна волн не должен запускаться
            //но при смене стадий будет выключен признак Boss Fight'а и волна должна будет запуститься
            if (isBossMode)
            {
                yield return new WaitForSeconds(5.0f);
                continue;
            }

            if (levelData.BossLevel)
            {
                //Если босс готов вернуться, то не делаем спаун новых врагов
                if (boss.GetComponent<BossController>().IsReadyToReturn())
                {
                    yield return new WaitForSeconds(1.5f);
                    continue;
                }
            }

            if (!isInfluencerTryToSpawn)
            {
                _ = StartCoroutine(SpawnBonus());
            }

            if (enemiesSpawned < levelData.EnemiesInWave)
            {

                //Спаун случайного противника
                GameObject spawned = spawner.Spawn(GetRandomEnemy());
                lastSpawnedEnemy = spawned;
                spawnedEnemies.Add(spawned);

                enemiesSpawned++;
                allEnemiesSpawned++;

                //Сохранить последнего противника  из уровня
                lastEnemy = allEnemiesSpawned == allEnemies ? spawned : null;

                yield return new WaitForSeconds(levelData.SpawnEveryNSeconds);
            }
            else if (enemiesKilled < allEnemiesSpawned)
            {
                yield return new WaitForSeconds(waitAfterWave);
            }

            else
            {
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
        isLevelEnded = true;
        PlaySound(successSound);
        StartCoroutine(PostGame(levelData.WaitAfterLevel));
    }

    protected void Restart()
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

        if (bossWasSpawned && !boss)
        {
            LevelEnded();
        }

        if (!levelData.BossLevel) //Данная проверка нужна только на уровнях без босса
        {
            if (allEnemies == allEnemiesSpawned && enemiesKilled == allEnemiesSpawned)
            {
                if (lastEnemy == null)
                {
                    LevelEnded();
                }
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
        return levelData.Enemies[Random.Range(0, levelData.Enemies.Length)];
    }

    IEnumerator SpawnBonus()
    {
        //Сразу выставляем признак, что не нужно запускать следующий спаун
        //Так как надо сначала выждать паузу, а затем уже 
        isInfluencerTryToSpawn = true;

        //Сначала запускаем паузу. Объект должен добавиться после ожидания. 
        yield return new WaitForSeconds(levelData.InfluencerSpawnRate);

        //Для расчета вероятности воспользуемся Random.Range
        float randomSeed = Random.Range(0.0f, 1.0f);

        //Если полученное значение входит в допустимую вероятность, то можно спаунить объект
        if (randomSeed <= levelData.InfluencerChance && levelData.Influencers.Length != 0)
        {
            spawner.Spawn(GetInfluencer());
        }

        isInfluencerTryToSpawn = false;
    }

    //Получить случайный инфлюенсер
    GameObject GetInfluencer()
    {
        if (levelData.Influencers.Length == 0)
        {
            return null;
        }

        return levelData.Influencers[Random.Range(0, levelData.Influencers.Length)];
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


    protected IEnumerator AddBullets()
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
        if (!clip || !audioSource)
        {
            return;
        }

        audioSource.PlayOneShot(clip, volume);
    }


    IEnumerator PostGame(float time = 2)
    {
        yield return new WaitForSeconds(time);

        if (levelNumber == levelsData.Length - 1 && !isGameOver)
        {
            //Можно перейти к титрам и финалу игры
            SceneManager.LoadScene("Credits");
        }
        else
        {
            _ = Instantiate(postGame);
        }

    }

    private void SpawnPlanet()
    {

        if (!levelData.Planet)
        {
            return;
        }

        //Определить границы экрана и задать позицию для планеты
        Vector3 limits = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.y));
        Vector3 planetPosition = new Vector3(Random.Range(-limits.x, limits.x), -30.0f, Random.Range(-limits.z, limits.z));
        GameObject planet = Instantiate(levelData.Planet, planetPosition, Quaternion.identity);

        //Установить случайный размер планеты для разного визуального эффекта 
        float size = Random.Range(minPlanetSize, maxPlanetSize);
        planet.transform.localScale = new Vector3(size, size, size);
    }

    //Метод для настройки сцены и спауна необходимых объектов    
    private void SceneObjectCreate()
    {
        SpawnPlanet();
    }

    //Метод на неуязвимость (для синхронизации разным скриптам)
    public void GetInvulnerablePlayer(bool variable)
    {
        player.Invulnerable(variable);
    }

    public Vector3 ScreenBound()
    {
        //Используя ScreenToWorldPoint. размеры окна и позицию по высоте камеры формируем границы движения
        moveBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.y));

        return moveBounds;
    }

    //Управление режимом Boss Fight
    public bool IsBossMode() => isBossMode;

    public bool SetBossMode()
    {
        Camera.main.GetComponent<Animator>().SetBool("isPlus", true);

        isBossMode = true;
        return isBossMode;
    }

    public bool SetBossModeOff()
    {
        Camera.main.GetComponent<Animator>().SetBool("isMinus", true);
        Camera.main.GetComponent<Animator>().SetBool("isPlus", false);

        isBossMode = false;
        return isBossMode;
    }

    void SpawnBoss()
    {
        if (!levelData.BossLevel || levelData.Boss == null)
        {
            return;
        }

        StartCoroutine(SpawnBossCoroutine());
    }

    IEnumerator SpawnBossCoroutine()
    {
        yield return new WaitForSeconds(levelData.WaitBeforeBoss);
        SetBossMode();
        boss = Instantiate(levelData.Boss, bossSpawnLocation, Quaternion.identity);
        bossWasSpawned = true;
    }

    public bool IsLastEnemyAlive()
    {
        spawnedEnemies.RemoveAll(spawned => spawned == null);
        return spawnedEnemies.Count > 0 ? true : false;
    }

    public void PauseModeOn()
    {
        MainUIObject.SetActive(false);
        Instantiate(pausePrefab, pausePrefab.transform.position, pausePrefab.transform.rotation);
        Time.timeScale = 0.0f;
    }

    public void PauseModeOff()
    {
        Time.timeScale = 1.0f;
        MainUIObject.SetActive(true);
        GameObject pause = GameObject.Find("PauseMode(Clone)");
        Destroy(pause);
    }

    public GameObject GetBoss() => boss;
}
