using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideReplayButton : MonoBehaviour
{
    [SerializeField] Button button;
    
    [SerializeField] GroundDetection[] groundDetections;


    private void Start()
    {
        button.gameObject.SetActive(true);
    }

    private void Update()
    {
        bool isAnchorOnGround = false;

        foreach (var groundDetection in groundDetections)
        {
            if (groundDetection.IsAnchorOnGround())
            {
                isAnchorOnGround = true;
                break;
            }
        }

        button.gameObject.SetActive(isAnchorOnGround);
    }

}
