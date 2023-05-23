using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{   

    [SerializeField]
    private AudioClip mainTheme;


    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = mainTheme;
        audioSource.volume = 0.4f;
        audioSource.Play();
    }


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        IsItTimeToGoToMainMenu();
    }


    void IsItTimeToGoToMainMenu(){
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) )
        {
            SceneManager.LoadScene("StartLevel");
        }
    }
}
