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
    // Start is called before the first frame update
    void Start()
    {
        healthText.text = $"100/100";
        ammoText.text = $"30/30";
        GunText.text = $"Gun";
        ammoText.text = $"999";
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = $"{Player.Instance.health}/100";
        ammoText.text = $"{Player.Instance.firearm.currentAmmo}/30";
        GunText.text = $"{Player.Instance.firearm.GunType}";
    }
}
