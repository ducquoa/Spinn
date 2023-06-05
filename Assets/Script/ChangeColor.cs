using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeColor : MonoBehaviour
{
    [SerializeField] Material newMaterial;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] PlayerController playerController;
    [SerializeField] Score score;

    private bool colorChanged = false;
    public void Awake()
    {
        if (skinnedMeshRenderer == null)
            skinnedMeshRenderer = FindObjectOfType<SkinnedMeshRenderer>();
        if (playerController == null)
            playerController = GetComponent<PlayerController>();
        if (score == null)
            score = FindObjectOfType<Score>();

    }

    private void Start()
    {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!colorChanged && collision.gameObject.CompareTag("Player"))
        {
            skinnedMeshRenderer.material = newMaterial;
            score.IncrementScore(1);   // Increase the Score by 1
            score.UpdateComboMultiplier(); // Update the combo multiplier in the Score script
            colorChanged = true;

            // Notify the attached BeanGenerator script of the colored bean
            BeanGenerator beanGenerator = GetComponentInParent<BeanGenerator>();
            if (beanGenerator != null)
            {
                beanGenerator.HandleColorChanged();
            }
        }
    }
    private void Update()
    {
        if (colorChanged && !score.IsComboCooldownActive())
        {
            score.ResetComboMultiplier();   // Reset the combo mul if the combo is not active
            colorChanged = true;
        }
    }
}
