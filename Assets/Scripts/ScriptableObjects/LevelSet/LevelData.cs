using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New LevelData", menuName = "LevelData", order = 51)]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private float spawnEveryNSeconds;

    [SerializeField]
    private int enemiesInWave;

    [SerializeField]
    private int numberOfWaves; 

    [SerializeField]
    private float influencerSpawnRate;

    [SerializeField]
    private float influencerChance;

    [SerializeField]
    private GameObject planet;

    [SerializeField]
    //Массив для противников
    private GameObject[] enemies;

    [SerializeField]
    //Массив инфлюенсеров - бонусов и дебафов
    private GameObject[] influencers;

    [SerializeField]
    private bool bossLevel = false;

    [SerializeField]
    private float waitBeforeBoss;

    [SerializeField]
    private GameObject boss;

    public float SpawnEveryNSeconds { get => spawnEveryNSeconds; set => spawnEveryNSeconds = value; }
    public int EnemiesInWave { get => enemiesInWave; set => enemiesInWave = value; }
    public int NumberOfWaves { get => numberOfWaves; set => numberOfWaves = value; }
    public float InfluencerSpawnRate { get => influencerSpawnRate; set => influencerSpawnRate = value; }
    public float InfluencerChance { get => influencerChance; set => influencerChance = value; }
    public GameObject Planet { get => planet; set => planet = value; }
    public GameObject[] Enemies { get => enemies; set => enemies = value; }
    public GameObject[] Influencers { get => influencers; set => influencers = value; }
    public bool BossLevel { get => bossLevel; set => bossLevel = value; }
    public float WaitBeforeBoss { get => waitBeforeBoss; set => waitBeforeBoss = value; }
    public GameObject Boss { get => boss; set => boss = value; }
}
