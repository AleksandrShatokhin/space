using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveElements : MonoBehaviour
{
    private float speed = 5.0f;


    //public float timerStatr = 20.0f;
    //public float timerEnd = 0.0f;

    void Start()
    {
        //FindObjectOfType<PlayerController>();
        //GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        /*if (timerStatr > 0)
        {
            timerStatr = timerStatr - 0.007f;
            if (timerStatr <= timerEnd)
            Destroy(gameObject);
        }*/
    }

    /*void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        
    }*/

    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Destroy(gameObject);
        }
    }*/
}
