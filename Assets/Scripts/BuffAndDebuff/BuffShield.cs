using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffShield : MonoBehaviour
{
    public GameObject shield;
    
    void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerController>().isShield = true;
            Destroy(gameObject);
            Instantiate(shield, transform.position, transform.rotation);
        }
    }
}
