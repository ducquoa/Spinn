using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI comboMultiplierText;
    [SerializeField] float starPopForce = 30f;
    [SerializeField] GameObject starPrefab;
    [SerializeField] GameObject starSpawnPoint;

    int currentScore = 0;
    int comboMultiplier = 1;
    float comboCooldownDuration = 0.5f;
    float comboCooldownTimer = 0f;

    bool starSpawned = false;

    public static Score Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreText();
        UpdateComboMultiplierText();
    }
    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void SetCurrentScore(int score)
    {
        currentScore = score;
        UpdateScoreText();
        CheckSpawnStars();
    }

    public void IncrementScore(int amount)
    {
        currentScore += amount * comboMultiplier;
        UpdateScoreText();
        CheckSpawnStars();
    }
    public void UpdateComboMultiplier()
    {
        comboMultiplier++; 
        comboCooldownTimer = comboCooldownDuration; // Reset the combo cooldown timer
        UpdateComboMultiplierText();
    }
    public bool IsComboCooldownActive()
    {
        return comboCooldownTimer > 0f;
    }
    public void ResetComboMultiplier()
    {
        comboMultiplier = 1; // Reset the combo multiplier
        UpdateComboMultiplierText();
    }
    private void Update()
    {
        if (comboCooldownTimer > 0f)
        {
            comboCooldownTimer -= Time.deltaTime;
        }
    }
    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = currentScore.ToString();
        
    }

    private void CheckSpawnStars()
    {
        if (currentScore >= 5000 && !starSpawned)
        {
            SpawnStars();
            starSpawned = true;
            
        }
    }

    private void SpawnStars()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject star = Instantiate(starPrefab, starSpawnPoint.transform.position, Quaternion.identity);
            Rigidbody starRigidbody = star.GetComponent<Rigidbody>();
            if (starRigidbody != null)
            {
                // Apply a pop force to make the star pop
                starRigidbody.AddForce(Vector3.up * starPopForce, ForceMode.Impulse);
            }

        }
    }

    private void UpdateComboMultiplierText()
    {
        if (comboMultiplier == 1)
        {
            comboMultiplierText.enabled = false;
        }

        if (comboMultiplierText != null && comboMultiplier != 1)
        {
            comboMultiplierText.enabled = true;
            comboMultiplierText.text = "x" + comboMultiplier.ToString();
        }
    }

}
