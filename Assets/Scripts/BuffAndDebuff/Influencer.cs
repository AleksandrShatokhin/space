using UnityEngine;
using System.Collections;
using UnityEditor;

public class Influencer : MonoBehaviour
{
    public AudioClip activateSound;
    public AudioClip destroySound;


    private void Awake()
    {
        if (!destroySound)
        {
            destroySound = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Sounds/S_InfluencerDestroyed.mp3", typeof(AudioClip));
        }
    }

    public void ActivateSound()
    {
        GameController.GetInstance().PlaySound(activateSound, 0.5f);
    }


    public void OnDestroy()
    {
        if (!destroySound)
        {
            return;
        }

        GameController.GetInstance().PlaySound(destroySound, 4);
    }
}
