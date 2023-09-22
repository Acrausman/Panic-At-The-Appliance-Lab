using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string Name = "Gun";
    public enum Type
    {
        pistol,
        smg,
        shotgun
    }
    public Type gunType;

    public enum AmmoType
    {
        light,
        medium,
        heavy
    }
    public AmmoType ammoType;

    public int ammoCapacity = 10;

    public int fireRate = 1;

    public int ammoPerShot = 1;
}
