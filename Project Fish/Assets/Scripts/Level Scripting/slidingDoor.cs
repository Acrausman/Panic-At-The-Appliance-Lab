using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class slidingDoor : MonoBehaviour
{
    public AudioClip sound;

    public string[] scenesToLoad;
    public string[] scenesToUnload;
    public enum openDirection
    {
        up,
        down,
        left,
        right
    }

    public openDirection direction;
    public float openOffset = 5;
    public float speed = 50;

    Vector3 origin;
    Vector3 openedPos;

    public bool canOpen = true;
    bool isOpen;
    public GameObject door;
    public BoxCollider doorCollider;

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isOpen = false;
        origin = door.transform.position;
        switch (direction)
        {
            case openDirection.up:
                openedPos = new Vector3(door.transform.position.x, door.transform.position.y + openOffset,door.transform.position.z);
                break;

            case openDirection.down:
                openedPos = new Vector3(door.transform.position.x, door.transform.position.y - openOffset, door.transform.position.z);
                break;

            case openDirection.left:
                openedPos = new Vector3(door.transform.position.x - openOffset, door.transform.position.y, door.transform.position.z);
                break;

            case openDirection.right:
                openedPos = new Vector3(door.transform.position.x + openOffset, door.transform.position.y, door.transform.position.z);
                break;

            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen && Vector3.Distance(door.transform.position, openedPos) > 0)
        {
            updateMovement(true);
        }
        else if(!isOpen && Vector3.Distance(door.transform.position, origin) > 0)
        {
            updateMovement(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && canOpen)
        {
            audioSource.PlayOneShot(sound);
            door.GetComponent<BoxCollider>().enabled = false;
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canOpen)
        {
            door.GetComponent<BoxCollider>().enabled = true;
            isOpen = false;
        }
    }

    void updateMovement(bool opening)
    {
        if(opening)
        {
            loadSections();
            unloadSections();
            door.transform.position = Vector3.MoveTowards(door.transform.position, openedPos, speed * Time.deltaTime);
        }
        else if(!opening)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, origin, speed * Time.deltaTime);
        }
    }

    void loadSections()
    {
        int x = scenesToLoad.Length;
        if(x > 0)
        {
            for (int i = 0; i < x; i++)
            {
                Scene target = SceneManager.GetSceneByName(scenesToLoad[i]);
                if(!target.isLoaded)SceneManager.LoadScene(scenesToLoad[i], LoadSceneMode.Additive);
            }
        }
    }

    void unloadSections()
    {
        int x = scenesToUnload.Length;
        if(x > 0)
        {
            for (int i = 0; i < x; i++)
            {
                Scene target = SceneManager.GetSceneByName(scenesToUnload[i]);
                if(target.isLoaded)SceneManager.UnloadSceneAsync(scenesToUnload[i]);
            }
        }
        
    }

}
