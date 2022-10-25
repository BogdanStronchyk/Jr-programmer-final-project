using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class inGameUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI GunText;
    public TextMeshProUGUI scoreText;

    // Update is called once per frame
    void Update()
    {
        healthText.text = $"{Player.Instance.health}/100";
        ammoText.text = $"{Player.Instance.firearm.currentAmmo}/{Player.Instance.firearm.maxAmmo}";
        GunText.text = $"{Player.Instance.firearm.GunType}";
    }
}
