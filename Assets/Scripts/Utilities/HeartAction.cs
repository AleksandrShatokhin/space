using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAction : MonoBehaviour
{
    private byte hp = 1;
    public AudioClip activateSound;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<PlayerController>().AddHealth(hp);
            GameController.GetInstance().PlaySound(activateSound, 1.0f);
        }
    }
}
