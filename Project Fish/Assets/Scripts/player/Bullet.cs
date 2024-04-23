using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float range;
    public Vector3 target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //print(Vector3.Distance(currPos, target).ToString());
        if(Vector3.Distance(currPos, target) >= range)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
