using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_BombBlast : MonoBehaviour
{
    protected float damage = 2.0f;
    protected bool isCollision; //этой переменной ограничу воздействие частиц только первой частицей
    private SphereCollider blastColBomb;

    void Start()
    {
        blastColBomb = GetComponentInChildren<SphereCollider>();

        isCollision = false;
        blastColBomb.radius = 1;
    }

    void Update()
    {
        Destroy(transform.parent.gameObject, 1.5f);
    }

    private void FixedUpdate()
    {
        blastColBomb.radius += 0.02f;
        if (blastColBomb.radius >= 1.9f)
        {
            blastColBomb.gameObject.SetActive(false);
        }
    }
}
