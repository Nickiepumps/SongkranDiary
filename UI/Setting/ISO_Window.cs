using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ISO_Window : MonoBehaviour
{
    [Header("Game UI Controller Reference")]
    public GameUIController gameUIController;
    [Header("Switch Animation Speed")]
    public float animationSpeed;
    public IEnumerator ISOWindowTransition(GameObject currentWindow, GameObject targetWindow)
    {
        while (currentWindow.transform.rotation.eulerAngles.y < 90)
        {
            currentWindow.transform.Rotate(0, animationSpeed, 0);
            yield return null;
        }
        currentWindow.SetActive(false);
        targetWindow.SetActive(true);
        yield return null;
    }
    public IEnumerator WindowRotate(GameObject window)
    {
        while (window.transform.rotation.eulerAngles.y > 0)
        {
            window.transform.Rotate(0, animationSpeed, 0);
            yield return null;
        }
        yield return null;
    }
}
