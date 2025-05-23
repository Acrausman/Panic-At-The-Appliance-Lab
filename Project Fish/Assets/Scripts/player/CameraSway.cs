using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSway : CameraAnimation
{
    public Rigidbody player;
    public override Vector2 GetPlayerLookMovement()
    {
        throw new System.NotImplementedException();
    }

    public override Vector3 GetPlayerVelocity()
    {
        //throw new System.NotImplementedException();
        return player.velocity;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
