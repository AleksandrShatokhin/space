using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastBombCollider : PS_BombBlast
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isCollision == false)
        {
            isCollision = true;
            other.gameObject.GetComponent<PlayerController>().AddDamage(damage);
            CameraController.shake = true;
        }
    }
}
