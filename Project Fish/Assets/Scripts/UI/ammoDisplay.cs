using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ammoDisplay : MonoBehaviour
{
    public TextMeshProUGUI currText;
    public TextMeshProUGUI maxText;
    public TextMeshProUGUI storageText;
    public playerData player;
    public Sprite waterSprite;
    public Color waterColor;
    public Sprite sparkSprite;
    public Color sparkColor;
    public Sprite discoSprite;
    public Color discoColor;
    Image image;

    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if(player.currGun != null)
        {
            switch (player.currGun.ammoType)
            {
                case Gun.AmmoType.light:
                    //storageText.text = player.sparkAmmoReserve.ToString();
                    //storageText.color = new Color(sparkColor.r, sparkColor.g, sparkColor.b);
                    image.sprite = waterSprite;
                    //text.color = new Color(waterColor.r, waterColor.g, waterColor.b);
                    break;

                case Gun.AmmoType.medium:
                    //storageText.text = player.waterAmmoReserve.ToString();
                    //storageText.color = new Color(waterColor.r, waterColor.g, waterColor.b);
                    image.sprite = sparkSprite;
                    //text.color = new Color(sparkColor.r, sparkColor.g, sparkColor.b);
                    break;

                case Gun.AmmoType.heavy:
                    image.sprite = discoSprite;
                    //text.color = new Color(discoColor.r, discoColor.g, discoColor.b);
                    break;

                default:
                    //storageText.text = player.sparkAmmoReserve.ToString();
                    //storageText.color = new Color(sparkColor.r, sparkColor.g, sparkColor.b);
                    //text.color = new Color(waterColor.r, waterColor.g, waterColor.b);
                    break;
            }
            currText.text = (player.currAmmo.ToString());
            maxText.text = (player.maxAmmo.ToString());
        }
        
    }
}
