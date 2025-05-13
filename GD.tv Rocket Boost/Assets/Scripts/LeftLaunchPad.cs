using UnityEngine;

public class LeftLaunchPad : MonoBehaviour
{
    public bool boolLeftLaunchPad = false;

    private void OnTriggerExit(Collider other)
    {
        boolLeftLaunchPad = true;
        UnityEngine.Debug.Log("Left Launchpad");
    }
}
