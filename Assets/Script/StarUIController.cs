using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StarUIController : MonoBehaviour
{
    public GameObject[] uiElements;
    private int starIndex = 0; 
    private bool isDestroyed = false;


    private void Start()
    {
        foreach (var element in uiElements)
        {
            element.SetActive(false);
        }

    }

    private void OnEnable()
    {
        StarCollect.onDestroyed += ShowNextUIElement;
    }
    private void OnDisable()
    {
        StarCollect.onDestroyed -= ShowNextUIElement;
    }

    private void ShowNextUIElement()
    {
        if (isDestroyed) return;
        if (starIndex < uiElements.Length)
        {
            uiElements[starIndex].SetActive(true);
            starIndex++;
        }
    }
    public void MarkStarDestroyed()
    {
        isDestroyed = true;
    }
}
