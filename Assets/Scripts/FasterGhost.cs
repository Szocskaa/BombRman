using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic{
public class FasterGhost : MonoBehaviour
{
    private GhostScript ghostScript;
    private bool speedChanged = false;

    void Start()
    {
        ghostScript = GetComponent<GhostScript>();
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
        if (ghostScript != null)
        {
            ghostScript.Speed = newSpeed;
        }
    }
}}