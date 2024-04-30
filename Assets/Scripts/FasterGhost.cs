using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic{
public class FasterGhost : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private bool speedChanged = false;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!speedChanged && Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeGhostSpeed(8);
            speedChanged = true;
        }
    }

    public void ChangeGhostSpeed(float newSpeed)
    {
        if (playerMovement != null)
        {
            playerMovement.playerSpeed = newSpeed;
        }
    }
}}