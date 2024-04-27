using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Gun : MonoBehaviour
{
    public GameObject bulletModel;
    public float bulletSpeed = 500;
    public ParticleSystem muzzle;
    public GameObject gunBarrel;
    public string Name = "Gun";
    public enum Type
    {
        pistol,
        smg,
        shotgun,
        gatling
    }
    public Type gunType;

    public enum AmmoType
    {
        light,
        medium,
        heavy
    }
    public int index;

    public AmmoType ammoType;

    public int ammoCapacity = 10;

    public float fireRate = 1;

    public float range = 50;

    public int ammoPerShot = 1;

    public float reloadTime = 3;

    public float damage = 20;

    public float kickback = 10;

    public AudioClip sound;
    public AudioClip reloadSound;
    public AudioClip emptySound;

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

    public void noAmmo()
    {
        audioSource.PlayOneShot(emptySound);
    }

    public void fire()
    {
        muzzle.Play();
        animator.SetTrigger("shoot");
        audioSource.PlayOneShot(sound);
        lineRenderer.enabled = false;

        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0));
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        //lineRenderer.SetPosition(0, gunBarrel.transform.position);

        if(Physics.Raycast(rayOrigin, cam.transform.forward,out hit,range))
        {
            targetPoint = hit.point;
            lineRenderer.SetPosition(1, hit.point);
            enemyData hitTarget = hit.transform.gameObject.GetComponent<enemyData>();
            if (hitTarget != null) hitTarget.takeDamage(damage);
        }
        else
        {
            targetPoint = ray.GetPoint(range);
            lineRenderer.SetPosition(1, rayOrigin + (cam.transform.forward * range));
        }

        Vector3 direction = targetPoint - gunBarrel.transform.position;

        GameObject newBullet = Instantiate(bulletModel, gunBarrel.transform.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().target = targetPoint;
        newBullet.GetComponent<Bullet>().range = range;
        newBullet.transform.forward = direction.normalized;
        newBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * bulletSpeed, ForceMode.Impulse);
    }

    public void melee(float mDamage, float mRange)
    {
        animator.SetTrigger("melee");
        lineRenderer.enabled = false;

        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //lineRenderer.SetPosition(0, gunBarrel.transform.position);

        if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, range))
        {
            lineRenderer.SetPosition(1, hit.point);
            enemyData hitTarget = hit.transform.gameObject.GetComponent<enemyData>();
            if (hitTarget != null) hitTarget.takeDamage(mDamage);
        }
        else
        {
            lineRenderer.SetPosition(1, rayOrigin + (cam.transform.forward * range));
        }
    }

    public void reload()
    {
        audioSource.PlayOneShot(reloadSound);
        animator.SetTrigger("reload");
    }

}
