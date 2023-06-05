using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Events;
using System;

public class StarCollect : MonoBehaviour
{
    public static event Action onDestroyed;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Score.Instance.IncrementScore(100);
            onDestroyed?.Invoke();
            Destroy(gameObject);
            
        }
    }
}
