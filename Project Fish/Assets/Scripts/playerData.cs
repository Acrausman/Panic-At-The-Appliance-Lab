using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerData : MonoBehaviour
{
    public suitVoice voice;

    public AudioClip damage;
    public float maxHealth;
    public float currHealth;
    public float invTime = 1;
    float currInv = 0;

    public float waterAmmoReserve;
    public float sparkAmmoReserve;
    public float discoAmmoReserve;
    public float maxAmmo;
    public float currAmmo;
    public Vector3 gunOffset;
    float reloadTime;
    public bool canReload = true;
    public bool canFire = true;

    public float meleeDamage = 10;
    public float meleeRange = 10;

    public List<GameObject> weaponList;


    public GameObject gunRoot;
    public Gun currGun;
    Gun.AmmoType currGunAmmoType;

    AudioSource audioSource;

    void Start()
    {
        currInv = 0;
        audioSource = GetComponent<AudioSource>();
        currHealth = maxHealth;
        //if(weaponList[0] != null) setCurrGun(weaponList[0]);
        
        
    }

    /*
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            takeDamage(maxHealth);
        }
    }
    */
    

    public void takeDamage(float amount)
    {
        if (currInv <= 0)
        {
            audioSource.PlayOneShot(damage);
            float healthProp = currHealth / maxHealth;
            print(healthProp);
            currHealth -= amount;
            if (currHealth <= 0)
            {
                levelManager1 manager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<levelManager1>();
                manager.respawnPlayer();
            }
            if (healthProp <= 0.40)
            {
                //print("play");
                voice.playLowHealth();
            }
            currInv = 1;
            StartCoroutine(invincibilityPeriod());
        }
        
    }

    public void switchWeapon(int index)
    {
        if(weaponList[index] != null)
        {
            //print("Switching weapon to " + index.ToString());
            storeAmmo();
            setCurrGun(weaponList[index]);
        }

    }

    public void setCurrGun(GameObject newWeapon)
    {
        if (currGun)
        {
            //print("Destroyed"); 
            Destroy(currGun.gameObject);
        }
        GameObject addedWeapon = GameObject.Instantiate(newWeapon, gunRoot.transform, worldPositionStays: false);
        addedWeapon.transform.localPosition = gunOffset;
        currGun = addedWeapon.GetComponent<Gun>();
        currGunAmmoType = currGun.ammoType;


        switch (currGunAmmoType)
        {
            case Gun.AmmoType.light:
                if (waterAmmoReserve - currGun.ammoCapacity >= currGun.ammoCapacity) maxAmmo = waterAmmoReserve - currGun.ammoCapacity;
                else maxAmmo = waterAmmoReserve;
                //waterAmmoReserve = 0;
                break;

            case Gun.AmmoType.medium:
                if (sparkAmmoReserve - currGun.ammoCapacity >= currGun.ammoCapacity) maxAmmo = sparkAmmoReserve - currGun.ammoCapacity;
                else maxAmmo = sparkAmmoReserve;
                //sparkAmmoReserve = 0;
                break;

            case Gun.AmmoType.heavy:
                if (discoAmmoReserve - currGun.ammoCapacity >= currGun.ammoCapacity) maxAmmo = discoAmmoReserve - currGun.ammoCapacity;
                else maxAmmo = discoAmmoReserve;
                //discoAmmoReserve = 0;
                break;

            default:
                maxAmmo = waterAmmoReserve - currGun.ammoCapacity;
                break;
        }

        currAmmo = currGun.ammoCapacity;

    }

    void storeAmmo()
    {
        if(currGun != null)
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
        
    }

    public void reload()
    {
        if(canReload)
        {
            if (currAmmo != currGun.ammoCapacity)
            {
                this.gameObject.GetComponent<playerBehavior>().readyToFire = false;
                currGun.reload();
                canReload = false;
                StartCoroutine(reloadDelay());
                int nextAmount = 1;
                for (int i = 0; i < currGun.ammoCapacity; i++)
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
        voice.playHeal();
        if(currHealth + amount <= maxHealth)
        {
            currHealth += amount;
        }
    }

    IEnumerator invincibilityPeriod()
    {
        Animator anim = currGun.GetComponent<Animator>();
        //anim.runtimeAnimatorController.
        yield return new WaitForSeconds(invTime);
        currInv = 0;
    }

    IEnumerator reloadDelay()
    {
        yield return new WaitForSeconds(currGun.reloadTime);
        this.gameObject.GetComponent<playerBehavior>().readyToFire = true;
        canReload = true;
    }
}
