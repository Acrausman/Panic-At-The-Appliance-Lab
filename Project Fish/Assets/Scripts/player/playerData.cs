using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerData : MonoBehaviour
{
    public static playerData instance;
    public suitVoice voice;

    public AudioClip damage;
    public float maxHealth;
    public float currHealth;
    public static float storedHealth;
    public float invTime = 1;
    public float currInv = 0;

    public float waterAmmoReserve;
    public static float storedWater;
    public float sparkAmmoReserve;
    public static float storedSpark;
    public float discoAmmoReserve;
    public static float storedDisco;
    public static int currWeaponIndex;
    public float maxAmmo;
    public float currAmmo;
    public Vector3 gunOffset;
    float reloadTime;
    public bool canReload = true;
    public bool canFire = true;

    public float meleeDamage = 10;
    public float meleeRange = 10;

    public static List<GameObject> weaponList = new List<GameObject>();


    public GameObject gunRoot;
    public Gun currGun;
    Gun.AmmoType currGunAmmoType;

    AudioSource audioSource;

    void Start()
    {
        instance = this;
        currInv = 0;
        audioSource = GetComponent<AudioSource>();
        currHealth = maxHealth;
        //print(waterAmmoReserve);
        //print(sparkAmmoReserve);
        //print(discoAmmoReserve);
        if (weaponList[0].GetComponent<Gun>()) setCurrGun(weaponList[currWeaponIndex]);

        DontDestroyOnLoad(gameObject);
    }


    private void Update()
    {
        if(currInv > 0)
        {
            currInv -= 1 * Time.deltaTime;
        }
    }
 
    


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
            currInv = invTime;
            //StartCoroutine(invincibilityPeriod());
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
                voice.playWaterRestored();
                break;

            case boxAmmo.AmmoType.spark:
                print("Activated");
                if (currGunAmmoType == Gun.AmmoType.medium) maxAmmo += amount;
                else sparkAmmoReserve += amount;
                voice.playSparkRestored();
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
        else
        {
            currHealth = maxHealth;
        }
    }

    /*IEnumerator invincibilityPeriod()
    {
        Animator anim = currGun.GetComponent<Animator>();
        //anim.runtimeAnimatorController.
        yield return new WaitForSeconds(invTime);
        currInv = 0;
    }*/

    public void addWeapon(GameObject newWeapon)
    {
        //print(newWeapon.name);
        weaponList.Add(newWeapon);
        //print(weaponList.ToString());
    }

    public void storeForNextLevel()
    {
        storeAmmo();
        storedHealth = currHealth;
        currWeaponIndex = currGun.index;
        storedWater = waterAmmoReserve;
        //print(storedWater);
        storedSpark = sparkAmmoReserve;
        //print(storedSpark);
        storedDisco = discoAmmoReserve;
        //print(storedDisco);

    }

    public void restoreInventory()
    {
        print("Restored Inventory");
        currHealth = storedHealth;
        waterAmmoReserve = storedWater;
        sparkAmmoReserve = storedSpark;
        discoAmmoReserve = storedDisco;

    }

    IEnumerator reloadDelay()
    {
        yield return new WaitForSeconds(currGun.reloadTime);
        this.gameObject.GetComponent<playerBehavior>().readyToFire = true;
        canReload = true;
    }

    public List<GameObject> getWeaponList()
    {
        return weaponList;
    }

    public bool isWeaponListIndexValid(int x)
    {
        return weaponList[x];
    }
}
