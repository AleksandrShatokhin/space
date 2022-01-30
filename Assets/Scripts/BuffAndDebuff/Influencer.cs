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
            Debug.Log(destroySound);
        }
    }

    public void ActivateSound()
    {
        GameController.GetInstance().PlaySound(activateSound);
    }


    public void OnDestroy()
    {
        GameController.GetInstance().PlaySound(destroySound, 4);
        Debug.Log("OnDestroyCalled");
    }
}
