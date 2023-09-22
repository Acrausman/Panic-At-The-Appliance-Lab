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

    void Start()
    {
        
    }

    void Update()
    {
        int newInt = (int)playerData.currHealth;
        num.text = newInt.ToString();
        bar.GetComponent<Image>().fillAmount = playerData.currHealth/playerData.maxHealth;
    }
}
