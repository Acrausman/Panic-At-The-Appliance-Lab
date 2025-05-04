using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTrail : MonoBehaviour
{
    public ParticleSystem trailParticles; // Assign the Particle System in the Inspector
    public float movementThreshold = 0.1f; // Adjust as needed

    private bool isMoving = false;

    void Update()
    {
        // Check if the player is moving
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontalInput) > movementThreshold || Mathf.Abs(verticalInput) > movementThreshold)
        {
            if (!isMoving)
            {
                trailParticles.Play(); // Start emitting
                isMoving = true;
            }
        }
        else
        {
            if (isMoving)
            {
                trailParticles.Stop(); // Stop emitting
                isMoving = false;
            }
        }
    }
}