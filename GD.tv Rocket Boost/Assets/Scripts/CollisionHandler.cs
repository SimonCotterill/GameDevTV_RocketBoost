using System.Diagnostics;
using UnityEngine;

using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

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
                LoadNextLevel();
                break;
            default:
                UnityEngine.Debug.Log("ooch ouch hit something bad");
                ReloadLevel();
                break;
        }
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
