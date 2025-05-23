using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CameraAnimation : MonoBehaviour
{
    [SerializeField] Transform TargetTransform;

    [Header("Movement/Look Sway Settings:")]
    [SerializeField, Min(0)] float SwaySpeed = 5f;
    [SerializeField, Tooltip("The multiplier for the sway caused by looking around"), Min(0)] float LookSwayMultiplier = 0.26f;
    [SerializeField] Vector3 MovementSwayStrength = new Vector3(5f, 1f, 1f);
    [SerializeField, Space()] Vector3 MinSwayRotation = new Vector3(-15, -5, -5);
    [SerializeField] Vector3 MaxSwayRotation = new Vector3(15, 5, 5);

    [Header("Idle Sway Settings:")]
    [SerializeField] bool EnableIdleSway = true;
    [SerializeField] float IdlePositionSwayStrength = 1f;
    [SerializeField] float IdlePositionSwaySpeed = 1f;

    [Header("Multiplier:")]
    public float CurrentMultiplier = 1f;


    void Update()
    {
        if (Time.frameCount < 10) return;

        var mouse = GetPlayerLookMovement() * LookSwayMultiplier;
        var movement = transform.InverseTransformDirection(GetPlayerVelocity());
        movement.x *= MovementSwayStrength.x;
        movement.y *= MovementSwayStrength.y;
        movement.z *= MovementSwayStrength.z;

        movement *= CurrentMultiplier;

        var rotationX = Quaternion.AngleAxis(mouse.x, Vector3.up);
        var rotationY = Quaternion.AngleAxis(-mouse.y, Vector3.right);

        var rotationZ = Quaternion.AngleAxis(-movement.x, Vector3.forward);
        var rotationY2 = Quaternion.AngleAxis(movement.y, Vector3.right);
        var rotationY3 = Quaternion.AngleAxis(movement.z, Vector3.right);
        var quatTargetRotation = rotationZ * rotationX * rotationY * rotationY2 * rotationY3;
        var angles = GetSignedEulerAngles(quatTargetRotation.eulerAngles);

        var targetRotation = Quaternion.Euler(
            Mathf.Clamp(angles.x, MinSwayRotation.x, MaxSwayRotation.x),
            Mathf.Clamp(angles.y, MinSwayRotation.y, MaxSwayRotation.y),
            Mathf.Clamp(angles.z, MinSwayRotation.z, MaxSwayRotation.z)
        );

        TargetTransform.localRotation = Quaternion.Slerp(TargetTransform.localRotation, targetRotation, SwaySpeed * Time.deltaTime);

        if (EnableIdleSway)
        {
            var idleSwayX = Mathf.PerlinNoise(Time.time * IdlePositionSwaySpeed, Mathf.Epsilon) - 0.5f;
            var idleSwayY = Mathf.PerlinNoise(Time.time * IdlePositionSwaySpeed + 0.25f, Mathf.Epsilon) - 0.5f;
            var idleSway = new Vector3(idleSwayX, idleSwayY, 0) * IdlePositionSwayStrength * CurrentMultiplier;
            TargetTransform.localPosition = Vector3.Lerp(TargetTransform.localPosition, idleSway, Time.deltaTime);
        }

    }

    public Vector3 GetSignedEulerAngles(Vector3 angles)
    {
        Vector3 signedAngles = Vector3.zero;
        for (int i = 0; i < 3; i++)
        {
            signedAngles[i] = (angles[i] + 180f) % 360f - 180f;
        }
        return signedAngles;
    }

    public abstract Vector2 GetPlayerLookMovement();
    public abstract Vector3 GetPlayerVelocity();

}