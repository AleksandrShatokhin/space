using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParticleController : MonoBehaviour
{

    [SerializeField]
    private AudioClip boom;

    // Start is called before the first frame update
    void Start()
    {
        if (boom)
        {
            StartCoroutine(SecondBoomSound());
        }
    }


    IEnumerator SecondBoomSound()
    {
        yield return new WaitForSeconds(0.7f);
        GameController.GetInstance().PlaySound(boom, 4.0f);

    }
}
