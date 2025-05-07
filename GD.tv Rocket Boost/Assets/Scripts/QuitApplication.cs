using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{

    void Update()
    {
        RespondToQuit();
    }

    private void RespondToQuit()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            UnityEngine.Debug.Log("We Pushed Escape");
            Application.Quit();
        }
    }
}
