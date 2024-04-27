using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickup : MonoBehaviour
{
    public boxAmmo box;
    public int ammo = 50;
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
                bool ignore = false;
                playerData data = other.GetComponentInParent<playerData>();
                for(int i = 0; i < data.getWeaponList().Count; i++)
                {
                    Gun gunToCheck = data.getWeaponList()[i].GetComponent<Gun>();
                    //print(gunToCheck.gameObject.name);
                    if (weapon.GetComponent<Gun>().gunType == gunToCheck.gunType)
                    {
                        ignore = true;
                        //break;
                    }
                }               
                if (!ignore)
                {
                    data.addWeapon(weapon);
                    data.setCurrGun(data.getWeaponList()[gun.index]);                   
                }
                else data.addAmmo(box, ammo);
                Destroy(gameObject);
            }
        }
    }
}
