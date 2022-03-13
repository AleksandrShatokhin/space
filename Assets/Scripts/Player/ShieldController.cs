using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private Transform targetPlayerPos;
    public bool isShield = false;
    public float timerStatrForShield = 10.0f;
    private float timerEndForShield = 0.0f;

    void Start()
    {
        targetPlayerPos = GameObject.Find("Player").GetComponent<Transform>();
        isShield = true;
    }

    void LateUpdate()
    {
        transform.position = targetPlayerPos.transform.position;

        if (isShield == true)
        {
            if (timerStatrForShield > 0)
            {
                timerStatrForShield = timerStatrForShield - 0.007f;
                if (timerStatrForShield <= timerEndForShield)
                isShield = false;
                if (timerStatrForShield < 0)
                {
                    timerStatrForShield = 10.0f;
                }            
            }
        }
        if (isShield == false)
        {
            Destroy(gameObject);
            GameController.GetInstance().GetInvulnerablePlayer(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "PlayerProjectile" && other.gameObject.tag != "EnemyShip")
        {
            Destroy(other.gameObject);

            if (other.gameObject.tag == "Asteroid")
            {
                other.GetComponent<EnemyBase>().Death();
            }    
        }
    }
}
