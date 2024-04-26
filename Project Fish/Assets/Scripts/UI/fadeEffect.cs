using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeEffect : MonoBehaviour
{
    public int timeActive = 5;
    Image image;
    public CanvasGroup canvasGroup;
    public float speed = 1;
    public bool fadeIn = false;
    public bool fadeOut = false;

    void Start()
    {
        image = GetComponent<Image>();
        //canvasGroup = GetComponent<CanvasGroup>();

    }

    private void Update()
    {
        if(fadeIn)
        {
            print("Fading");
            if(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += speed * Time.deltaTime;
                if(canvasGroup.alpha >= 1)
                {
                    StartCoroutine(autoFadeOut());
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            print("fading");
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= speed * Time.deltaTime;
                if (canvasGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }


    public void In()
    {
        fadeIn = true;
    }

    public void Out()
    {
        fadeOut = true;
    }

    IEnumerator autoFadeOut()
    {
        print("called");
        yield return new WaitForSeconds(1.5f);
        Out();
    }
}
