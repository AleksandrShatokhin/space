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
    void FixedUpdate()
    {
        //Астероид просто движется вперед. В сторону игрока
        transform.Translate(transform.forward * Time.deltaTime * movementSpeed);
    }

}
