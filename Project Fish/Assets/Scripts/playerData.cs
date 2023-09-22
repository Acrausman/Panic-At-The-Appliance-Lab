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
        currAmmo = currGun.ammoCapacity;
        

    }

    void setCurrGun()
    {
        currGun = gunRoot.GetComponentInChildren<Gun>();
        currGunAmmoType = currGun.ammoType;

        switch (currGunAmmoType)
        {
            case Gun.AmmoType.light:
                if (waterAmmoReserve - currGun.ammoCapacity >= currGun.ammoCapacity) maxAmmo = waterAmmoReserve - currGun.ammoCapacity;
                break;

            case Gun.AmmoType.medium:
                if (sparkAmmoReserve - currGun.ammoCapacity >= currGun.ammoCapacity) maxAmmo = sparkAmmoReserve - currGun.ammoCapacity;
                break;

            case Gun.AmmoType.heavy:
                if (discoAmmoReserve - currGun.ammoCapacity >= currGun.ammoCapacity) maxAmmo = discoAmmoReserve - currGun.ammoCapacity;
                break;

            default:
                maxAmmo = waterAmmoReserve - currGun.ammoCapacity;
                break;
        }

    }

}
