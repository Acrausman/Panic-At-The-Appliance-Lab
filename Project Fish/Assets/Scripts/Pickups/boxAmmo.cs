using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxAmmo : MonoBehaviour
{
    public enum AmmoType
    {
        water,
        spark,
        disco
    }
    public AmmoType ammoType;

    public int amount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<playerData>().addAmmo(GetComponent<boxAmmo>(), amount);
        }
        Destroy(gameObject);
    }

}
