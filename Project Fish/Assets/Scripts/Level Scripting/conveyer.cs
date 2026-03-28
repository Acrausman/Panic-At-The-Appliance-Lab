using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyer : MonoBehaviour
{
    public float speed = 2.0f;

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().position -= transform.forward * speed * Time.deltaTime;
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * speed * Time.deltaTime);
    }
    //public Transform GameObject;
    //public float forceAmount; //factor to determine how strong of a force should be added to the character. 
    //public float forceDirectionOffset; //optional float to override expected direction of the boost, in degrees. Should usually be 0, to move the player in the y-rotation direction of the conveyer belt object (i.e. toward the red arrow).

    /*private void OnTriggerStay(Collider other)
    {
        //print("Hit");
        if (other.CompareTag("Player"))
        {
            Vector3 newForce = Vector3.zero;
            newForce.x = forceAmount;
            newForce = Quaternion.Euler(0, transform.rotation.y, 0) * newForce;
            newForce = Quaternion.Euler(0, forceDirectionOffset, 0) * newForce;
            other.GetComponentInParent<playerBehavior>().addForce(newForce);
        }

    }*/
}
