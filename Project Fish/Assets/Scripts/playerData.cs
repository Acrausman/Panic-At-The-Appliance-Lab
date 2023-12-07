using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerData : MonoBehaviour
{
    public AudioClip damage;
    public AudioClip heal1;
    public AudioClip heal2;

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

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currHealth = maxHealth;
        setCurrGun(weaponList[0]);
        
        
    }

    public void takeDamage(float amount)
    {
        audioSource.PlayOneShot(damage);
        currHealth -= amount;
        if(currHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
        }
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
                else maxAmmo = waterAmmoReserve;
                waterAmmoReserve = 0;
                break;

            case Gun.AmmoType.medium:
                if (sparkAmmoReserve - currGun.ammoCapacity >= currGun.ammoCapacity) maxAmmo = sparkAmmoReserve - currGun.ammoCapacity;
                else maxAmmo = sparkAmmoReserve;
                sparkAmmoReserve = 0;
                break;

            case Gun.AmmoType.heavy:
                if (discoAmmoReserve - currGun.ammoCapacity >= currGun.ammoCapacity) maxAmmo = discoAmmoReserve - currGun.ammoCapacity;
                else maxAmmo = discoAmmoReserve;
                discoAmmoReserve = 0;
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

    public void addAmmo(boxAmmo box, int amount)
    {
        switch(box.ammoType)
        {
            case boxAmmo.AmmoType.water:
                print("Activated");
                if(currGunAmmoType == Gun.AmmoType.light) maxAmmo += amount;
                else waterAmmoReserve += amount;
                break;

            case boxAmmo.AmmoType.spark:
                print("Activated");
                if (currGunAmmoType == Gun.AmmoType.medium) maxAmmo += amount;
                else sparkAmmoReserve += amount;
                break;

            case boxAmmo.AmmoType.disco:
                print("Activated");
                if (currGunAmmoType == Gun.AmmoType.heavy) maxAmmo += amount;
                else discoAmmoReserve += amount;
                break;
            default:
                print("Activated");
                if (currGunAmmoType == Gun.AmmoType.light) maxAmmo += amount;
                else waterAmmoReserve += amount;
                break;
        }
    }

    public void addHealth(int amount)
    {
        int no = Random.Range(0,1);
        switch(no)
        {
            case 0:
                audioSource.PlayOneShot(heal1);
                break;
            case 1:
                audioSource.PlayOneShot(heal2);
                break;
            default:
                audioSource.PlayOneShot(heal1);
                break;
        }
        if(currHealth + amount <= maxHealth)
        {
            currHealth += amount;
        }
    }

}
