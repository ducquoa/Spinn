using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] BeanGenerator[] beanGenerators;
    [SerializeField] GameObject levelCompleteUI;

    private int totalCylinderCount = 0;
    private int coloredCylinderCount = 0;
    private int completedGrounds = 0;

    private void Start()
    {
        levelCompleteUI.SetActive(false);
        // Calculate the total cylinder count across all bean generators
        foreach (BeanGenerator beanGenerator in beanGenerators)
        {
            totalCylinderCount += beanGenerator.GetTotalCylinderCount();
            beanGenerator.OnBeanColored += HandleCylinderColored;
        }
    }

    private void Update()
    {
        // Check the completion status of each bean generator
        for (int i = 0; i < beanGenerators.Length; i++)
        {
            if (beanGenerators[i].IsColoringComplete())
            {
                completedGrounds++;
                beanGenerators[i].ResetColoringStatus(); // Reset the coloring status for the next round

                if (completedGrounds == beanGenerators.Length)
                { 
                    Debug.Log("Level complete.");
                    Invoke("LevelCompleteUI", 1f);
                    Invoke("LoadNextLevel", 1f);

                }
            }
        }
    }

    private void LevelCompleteUI()
    {
        levelCompleteUI.SetActive(true);
    }
    private void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;
        if (nextLevelIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);

    }

    private void HandleCylinderColored()
    {
        coloredCylinderCount++;

        if (coloredCylinderCount == totalCylinderCount)
        {
            coloredCylinderCount = 0; // Reset the colored cylinder count for the next ground
        }
    }
}