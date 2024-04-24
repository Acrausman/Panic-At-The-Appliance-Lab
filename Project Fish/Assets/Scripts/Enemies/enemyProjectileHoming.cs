using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectileHoming : MonoBehaviour
{
    public float damage;
    public float force = 20;
    Transform target;
    Rigidbody rigidbody;
    bool firing = false;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(firing)
        {
            transform.LookAt(target);
            rigidbody.AddForce(transform.forward * force);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<playerData>() != null)
        {
            collision.gameObject.GetComponentInParent<playerData>().takeDamage(damage, false);
        }
        GameObject.Destroy(gameObject);
    }

    public void beginSequence(float x)
    {
        StartCoroutine(startup(x));
    }

    public IEnumerator startup(float x)
    {
        yield return new WaitForSeconds(x);
        firing = true;
    }


}
