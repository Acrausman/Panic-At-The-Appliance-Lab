using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ammoDisplay : MonoBehaviour
{
    TextMeshProUGUI text;
    public playerData player;

    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = (player.currAmmo.ToString() + " / " + player.maxAmmo.ToString());
    }
}
