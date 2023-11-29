using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ammoReserveDisplay : MonoBehaviour
{
    TextMeshProUGUI text;
    public playerData player;
    public Color waterColor;
    public Color sparkColor;
    public Color discoColor;

    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        switch(player.currGun.ammoType)
        {
            case Gun.AmmoType.light:
                text.text = (player.sparkAmmoReserve.ToString());
                text.color = new Color(waterColor.r, waterColor.g, waterColor.b);
                    break;

            case Gun.AmmoType.medium:
                text.text = (player.waterAmmoReserve.ToString());
                text.color = new Color(sparkColor.r, sparkColor.g, sparkColor.b);
                break;

            case Gun.AmmoType.heavy:
                text.color = new Color(discoColor.r, discoColor.g, discoColor.b);
                break;

            default:
                text.color = new Color(sparkColor.r, sparkColor.g, sparkColor.b);
                break;
        }
    }
}
