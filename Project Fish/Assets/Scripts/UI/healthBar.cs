using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class healthBar : MonoBehaviour
{
    public playerData playerData;
    public GameObject bar;
    public TextMeshProUGUI num;

    public GameObject display;
    public Sprite healthy;
    public Color healthyColor;
    public Sprite damaged;
    public Color damagedColor;
    public Sprite critical;
    public Color criticalColor;

    void Start()
    {
        
    }

    void Update()
    {
        int newInt = (int)playerData.currHealth;
        
        num.text = newInt.ToString();
        float scaledHealth = playerData.currHealth / playerData.maxHealth;
        if (scaledHealth >= 0.60)
        {
            display.GetComponent<Image>().sprite = healthy;
            colorChange(healthyColor);
        }
        else if (scaledHealth >= 0.25)
        {
            display.GetComponent<Image>().sprite = damaged;
            colorChange(damagedColor);
        }
        else
        {
            display.GetComponent<Image>().sprite = critical;
            colorChange(criticalColor);
        }
        bar.GetComponent<Image>().fillAmount = scaledHealth;
    }

    void colorChange(Color change)
    {
        Color newColor = new Color(change.r, change.g, change.b);
        bar.GetComponent<Image>().color = newColor;
    }
}
