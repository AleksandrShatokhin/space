using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Animator cameraAnim;
    public static bool shake;

    void Start()
    {
        cameraAnim = GetComponent<Animator>();
        shake = false;
    }

    void Update()
    {
        if(shake == true) // по такому условию происходит воспроизведение анимации
            cameraAnim.SetBool("isShake", true);
    }

    public void IsNotShake()
    {
    // фунция вызывается по Event на анимационном клипе
    // по окончании анимации происходит возват условий в default
            cameraAnim.SetBool("isShake", false);
            shake = false;
    }
}
