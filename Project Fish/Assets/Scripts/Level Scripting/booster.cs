using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class booster : MonoBehaviour
{
    public float boostAmount; //factor to multiply speed by for duration of boost
    public float boostLength; //how many seconds this boost should last for

    private void OnTriggerEnter(Collider other)
    {
        //print("Hit");
        if (other.CompareTag("Player"))
        {
            print("player sped up");
            other.GetComponentInParent<playerBehavior>().boost(boostAmount, boostLength);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
