using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerData : MonoBehaviour
{
    public float maxHealth;
    public float currHealth;

    public float waterAmmoReserve;
    public float sparkAmmoReserve;
    public float discoAmmoReserve;
    public float maxAmmo;
    public float currAmmo;


    public GameObject gunRoot;
    [HideInInspector]public Gun currGun;
    Gun.AmmoType currGunAmmoType;

    void Start()
    {
        currHealth = maxHealth;
        setCurrGun();
        
        
    }

    void Update()
    {
        //currAmmo = currGun.ammoCapacity;
        

    }

    void setCurrGun()
    {
        currGun = gunRoot.GetComponentInChildren<Gun>();
        currGunAmmoType = currGun.ammoType;

        switch (currGunAmmoType)
        {
            case Gun.AmmoType.light:
                if (waterAmmoReserve - currGun.ammoCapacity >= currGun.ammoCapacity) maxAmmo = waterAmmoReserve - currGun.ammoCapacity;
                else maxAmmo = sparkAmmoReserve;
                break;

            case Gun.AmmoType.medium:
                if (sparkAmmoReserve - currGun.ammoCapacity >= currGun.ammoCapacity) maxAmmo = sparkAmmoReserve - currGun.ammoCapacity;
                else maxAmmo = sparkAmmoReserve;
                break;

            case Gun.AmmoType.heavy:
                if (discoAmmoReserve - currGun.ammoCapacity >= currGun.ammoCapacity) maxAmmo = discoAmmoReserve - currGun.ammoCapacity;
                else maxAmmo = discoAmmoReserve;
                break;

            default:
                maxAmmo = waterAmmoReserve - currGun.ammoCapacity;
                break;
        }

        currAmmo = currGun.ammoCapacity;

    }

    public void reload()
    {
        int nextAmount = 1;
        for(int i = 0; i < currGun.ammoCapacity; i++)
        {
            if ((maxAmmo - 1 < 0)) break;

            if (currAmmo + nextAmount <= currGun.ammoCapacity)
            {
                currAmmo++;
                maxAmmo--;
            }
            else
            {
                break;
            }
        }
        
    }

    public void spendAmmo()
    {
        currAmmo--;
    }

}
