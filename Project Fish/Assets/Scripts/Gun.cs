using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Gun : MonoBehaviour
{
    public GameObject gunBarrel;
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

    public float fireRate = 1;

    public float range = 50;

    public int ammoPerShot = 1;

    public float damage = 20;

    Animator animator;
    private Camera cam;
    private AudioSource audioSource;
    private LineRenderer lineRenderer;
    

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
        cam = GetComponentInParent<Camera>();
        animator = GetComponentInChildren<Animator>();
    }

    public void fire()
    {
        animator.SetTrigger("shoot");
        lineRenderer.enabled = true;

        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;

        lineRenderer.SetPosition(0, gunBarrel.transform.position);

        if(Physics.Raycast(rayOrigin, cam.transform.forward,out hit,range))
        {
            lineRenderer.SetPosition(1, hit.point);
            enemyData hitTarget = hit.transform.gameObject.GetComponent<enemyData>();
            if (hitTarget != null) hitTarget.takeDamage(damage);
        }
        else
        {
            lineRenderer.SetPosition(1, rayOrigin + (cam.transform.forward * range));
        }
    }

}
