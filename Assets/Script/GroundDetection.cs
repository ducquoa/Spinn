using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] PlayerController playerController;
    [SerializeField] Canvas gameOver;

    bool isAnchorOnGround = true;

    public void Start()
    {
        gameOver.enabled = false;
    }

    public PlayerController PlayerController
    {
        set { playerController = value; }
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerController.GetAnchorSphere().transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            isAnchorOnGround = true;
        }
        else
        {
            isAnchorOnGround = false;
        }
        if (!isAnchorOnGround) { GameOverScreen(); }

    }

    public bool IsAnchorOnGround()
    {
        return isAnchorOnGround;
    }
    public void GameOverScreen()
    {
        gameOver.enabled = true;
    }
}
