using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGameController : GameController
{
    protected override void Start()
    {
        instance = this;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        _ = StartCoroutine(AddBullets());
    }

    protected override void Update()
    {

    }
}
