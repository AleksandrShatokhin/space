using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, Deathable
{

    public float movementSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
    }

    public void Death()
    {
        Debug.Log("Death");
        GameController.GetInstance().AddKilledEnemy();
        Destroy(gameObject);
    }

    void Deathable.Kill()
    {
        Death();
    }


    public void AddDamage(float damage)
    {
        GetComponent<HealtComponent>().Change(-damage);
    }
}
