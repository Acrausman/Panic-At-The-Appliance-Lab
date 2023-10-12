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
    public Vector3 gunOffset;

    [SerializeField]
    List<GameObject> weaponList;


    public GameObject gunRoot;
    [HideInInspector]public Gun currGun;
    Gun.AmmoType currGunAmmoType;

    void Start()
    {
        currHealth = maxHealth;
        setCurrGun(weaponList[0]);
        
        
    }

    void Update()
    {
        //currAmmo = currGun.ammoCapacity;
        

    }

    public void switchWeapon(int index)
    {
        storeAmmo();
        Destroy(currGun.gameObject);
        setCurrGun(weaponList[index]);
    }

    void setCurrGun(GameObject newWeapon)
    {
        GameObject addedWeapon = GameObject.Instantiate(newWeapon, gunRoot.transform, worldPositionStays: false);
        addedWeapon.transform.localPosition = gunOffset;
        currGun = addedWeapon.GetComponent<Gun>();
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

    void storeAmmo()
    {
        currGunAmmoType = currGun.ammoType;

        switch (currGunAmmoType)
        {
            case Gun.AmmoType.light:
                waterAmmoReserve = currAmmo + maxAmmo;
                break;

            case Gun.AmmoType.medium:
                sparkAmmoReserve = currAmmo + maxAmmo;
                break;

            case Gun.AmmoType.heavy:
                discoAmmoReserve = currAmmo + maxAmmo;
                break;

            default:
                waterAmmoReserve = currAmmo + maxAmmo;
                break;
        }
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
