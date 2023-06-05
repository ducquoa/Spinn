using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CylinderCountUI : MonoBehaviour
{
    [SerializeField] GameObject[] groundObjects;
    [SerializeField] TextMeshProUGUI countText;


    private void Start()
    {
        if (groundObjects != null && groundObjects.Length > 0 && countText != null)
        {
            int totalCylinderCount = 0;

            foreach (GameObject groundObject in groundObjects)
            {
                BeanGenerator beanGenerator = groundObject.GetComponent<BeanGenerator>();

               /* if (beanGenerator != null)
                {
                    int cylinderCount = beanGenerator.GetTotalCylinderCount();
                    totalCylinderCount += cylinderCount;
                   // Debug.Log("UI.cylinderCount" + cylinderCount);
                }
               */
            }
            
            UpdateCylinderCountText(totalCylinderCount);
            
        }
    }

    void UpdateCylinderCountText(int count)
    {
        if (countText != null)
        {
            countText.SetText(count.ToString());
        }
    }

    
    
}
