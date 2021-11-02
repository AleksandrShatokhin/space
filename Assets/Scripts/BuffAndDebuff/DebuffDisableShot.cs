using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffDisableShot : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameObject.Find("Game").GetComponent<TimerController>().isDisable == false)
            {
                GameObject.Find("Game").GetComponent<TimerController>().isDisable = true;
                GameObject.Find("Player").GetComponent<PlayerController>().isDisableShot = true;
                Destroy(gameObject);
            }
            else
            {
                GameObject.Find("Game").GetComponent<TimerController>().timerStatrForDisableShot = 10.0f;
                Destroy(gameObject);
            }
        }
    }
}
