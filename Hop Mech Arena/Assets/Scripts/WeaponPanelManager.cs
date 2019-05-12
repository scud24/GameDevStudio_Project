using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPanelManager : MonoBehaviour
{
    public Image weaponImage;
    public Text weaponText;
    public List<Sprite> weaponImages;
    public List<string> weaponNames;
    public PlayerWeaponManager pwm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        weaponImage.sprite = weaponImages[pwm.currentWeapon];
        weaponText.text = weaponNames[pwm.currentWeapon];
    }
}
