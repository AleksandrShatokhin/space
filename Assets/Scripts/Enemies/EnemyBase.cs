using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{

    public float movementSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
    }

    // Update is called once per frame
    


    public void Death()
    {
        Debug.Log("Death");
        GameController.GetInstance().AddKilledEnemy();
        Destroy(gameObject);
    }

}
