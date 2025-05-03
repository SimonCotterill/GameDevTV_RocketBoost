using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float crashDelay = 2f;
    [SerializeField] float finishDelay = 2f;

    [SerializeField] InputAction skipLevel;
    [SerializeField] InputAction skipCrash;

    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip finishAudio;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void OnCollisionEnter(Collision other)
    {
        //If the rocket is not controllable (crashed, level finished, etc.)
        //then don't finish this method
            //prevents unnecesary sounds
        if (!isControllable || !isCollidable)
        {
            return;
        }
    

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
                isControllable = false;
                break;
            default:
                UnityEngine.Debug.Log("ooch ouch hit something bad");
                StartCrashSequence();
                isControllable = false;
                break;
        }
    }

    private void StartCrashSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        GetComponent<Movement>().enabled = false;
        crashParticles.Play();
        Invoke("ReloadLevel", crashDelay);     
    }

    private void StartFinishSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(finishAudio);
        GetComponent<Movement>().enabled = false;
        successParticles.Play();
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

    private void RespondToDebugKeys()
    {

        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            UnityEngine.Debug.Log("skip level button pressed");
            LoadNextLevel();
        }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }

}
