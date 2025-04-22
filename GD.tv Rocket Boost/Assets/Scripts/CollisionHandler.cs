using System.Diagnostics;
using UnityEngine;

using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{



    private void OnCollisionEnter(Collision other)
    {
        //(other.gameObject.tag)

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
                break;
            default:
                UnityEngine.Debug.Log("ooch ouch hit something bad");
                ReloadLevel();
                break;
        }
    }


    private void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
