using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxRepair : MonoBehaviour
{
    public int amount = 15;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<playerData>().addHealth(amount);
        }
        Destroy(gameObject);
    }
}
