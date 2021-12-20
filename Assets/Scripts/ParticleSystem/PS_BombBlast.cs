using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_BombBlast : MonoBehaviour
{
    void Update()
    {
        Destroy(transform.parent.gameObject, 1);
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player")
            Debug.Log("Урон игроку");
    }
}
