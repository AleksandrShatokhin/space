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
            collision.gameObject.GetComponent<PlayerController>().Death();
        }
    }
}
