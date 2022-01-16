using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        
    }

    void FixedUpdate()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Если астероид попал в игрока, то убиваем игрока.
        //Потенциально заменить на вызов метода у игрока    
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().AddDamage(1);
            Death();
            CameraController.shake = true;
        }
        
    }

    protected void SpawnCollectableHP()
    {

    }

    public override void Death()
    {
        //Спаун жизней
        SpawnCollectableHP();
        base.Death();
    }

    // при такой реализации через триггер не работают некоторые необходимые действия
    /*void OnTriggerEnter(Collider other)
    {
        поменял для теста на триггер только по игроку, 
        чтоб астероиды игнорировали все обстальные игровые объекты, кроме игрока,
        и не отскакивали от них
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().Death();
            Destroy(gameObject);
        }
    }*/
}
