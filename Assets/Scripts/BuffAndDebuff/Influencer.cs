using UnityEngine;
using System.Collections;

public class Influencer : MonoBehaviour
{
    public AudioClip activateSound;

    public void ActivateSound()
    {
        GameController.GetInstance().PlaySound(activateSound);
    }
}
