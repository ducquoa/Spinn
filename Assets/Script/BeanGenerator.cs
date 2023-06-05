using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using System;

public class BeanGenerator : MonoBehaviour
{
    [SerializeField] GameObject cylinderPrefab;
    [SerializeField] float spacing = 0.35f;
    [SerializeField] float yOffset = 0.1f;

    public event Action OnBeanColored;

    private int totalCylinderCount = 0;
    private int coloredCylinderCount = 0;

    private bool coloringComplete = false;

    private void Start()
    {
        GenerateCylinderGrid();
    }

    private void GenerateCylinderGrid()
    {
        // Generate cylinder container
        GameObject beansContainer = new GameObject("BeansContainer");

        // Get the size of the plane
        Renderer planeRenderer = GetComponent<Renderer>();
        Vector3 planeSize = planeRenderer.bounds.size;

        // Calculate the number of rows and columns based on the spacing and plane size
        int rows = Mathf.FloorToInt(planeSize.z / spacing);
        int columns = Mathf.FloorToInt(planeSize.x / spacing);

        // Calculate the starting position of the grid
        Vector3 gridStartPosition = transform.position - new Vector3(planeSize.x * 0.5f, 0f, planeSize.z * 0.5f);

        // Spawn the cylinders in a grid pattern
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Calculate the position of the current cylinder
                Vector3 position = gridStartPosition + new Vector3(col * spacing + 0.25f, -yOffset, row * spacing + 0.25f);

                // Spawn the cylinder prefab at the calculated position with no rotation
                GameObject bean = Instantiate(cylinderPrefab, position, Quaternion.identity);
                totalCylinderCount++;

                // Throw generated beans into the container
                bean.transform.parent = beansContainer.transform;
            }
        }

        beansContainer.transform.parent = transform;
    }

    public void HandleColorChanged()
    {
        coloredCylinderCount++;

        if (coloredCylinderCount == totalCylinderCount)
        {
            //Debug.Log("All cylinders are colored!");
            coloringComplete = true;
            OnBeanColored?.Invoke();
        }
    }
    public bool IsColoringComplete()
    {
        return coloringComplete;
    }

    public void ResetColoringStatus()
    {
        coloringComplete = false;
        coloredCylinderCount = 0;
    }
    public int GetTotalCylinderCount()
    {
        return totalCylinderCount;
    }
}
