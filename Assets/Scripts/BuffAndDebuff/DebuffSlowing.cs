using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffSlowing : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameObject.Find("Game").GetComponent<TimerController>().isSlowing == false)
            {
                GameObject.Find("Game").GetComponent<TimerController>().isSlowing = true;
                GameObject.Find("Player").GetComponent<PlayerController>().speedPlayer = 10.0f;
                Destroy(gameObject);
            }
            else
            {
                GameObject.Find("Game").GetComponent<TimerController>().timerStatrForSlowing = 10.0f;
                Destroy(gameObject);
            }
        }
    }
}
