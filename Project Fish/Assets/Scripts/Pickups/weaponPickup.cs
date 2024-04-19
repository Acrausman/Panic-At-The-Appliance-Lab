using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickup : MonoBehaviour
{
    bool used = false;
    public GameObject weapon;
    Gun gun;


    void Awake()
    {
        used = false;
        gun = weapon.GetComponent<Gun>();
        //print(weapon);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!used)
            {
                used = true;
                playerData data = other.GetComponentInParent<playerData>();
                data.addWeapon(weapon);
                data.setCurrGun(data.getWeaponList()[gun.index]);
                Destroy(gameObject);
            }
        }
    }
}
