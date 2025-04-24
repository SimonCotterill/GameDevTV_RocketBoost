using System.Diagnostics;
using UnityEngine;

using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float crashDelay = 2f;
    [SerializeField] float finishDelay = 2f;

    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip finishAudio;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
    
        switch (other.gameObject.tag)
        {
            case "Friendly":
                UnityEngine.Debug.Log("hit a friend");
                break;
            case "Fuel":
                UnityEngine.Debug.Log("hit fuel");
                break;
            case "Finish":
                UnityEngine.Debug.Log("hit finish");
                StartFinishSequence();
                break;
            default:
                UnityEngine.Debug.Log("ooch ouch hit something bad");
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        audioSource.PlayOneShot(crashAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", crashDelay);
    }

    private void StartFinishSequence()
    {
        audioSource.PlayOneShot(finishAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", finishDelay);
    }

    private void ReloadLevel()
    {
        int currentScene = currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    private void LoadNextLevel()
    {
        int currentScene = currentScene = SceneManager.GetActiveScene().buildIndex;
        currentScene++;
        
        if (currentScene == SceneManager.sceneCountInBuildSettings)
        {
            currentScene = 0;
        }

        SceneManager.LoadScene(currentScene);
    }
}
