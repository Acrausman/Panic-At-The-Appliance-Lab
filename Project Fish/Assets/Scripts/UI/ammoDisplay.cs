using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ammoDisplay : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField]TextMeshProUGUI storageText;
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
                storageText.text = player.sparkAmmoReserve.ToString();
                storageText.color = new Color(sparkColor.r, sparkColor.g, sparkColor.b);
                text.color = new Color(waterColor.r, waterColor.g, waterColor.b);
                    break;

            case Gun.AmmoType.medium:
                storageText.text = player.waterAmmoReserve.ToString();
                storageText.color = new Color(waterColor.r, waterColor.g, waterColor.b);
                text.color = new Color(sparkColor.r, sparkColor.g, sparkColor.b);
                break;

            case Gun.AmmoType.heavy:
                text.color = new Color(discoColor.r, discoColor.g, discoColor.b);
                break;

            default:
                text.color = new Color(waterColor.r, waterColor.g, waterColor.b);
                break;
        }
        text.text = (player.currAmmo.ToString() + " / " + player.maxAmmo.ToString());
    }
}
