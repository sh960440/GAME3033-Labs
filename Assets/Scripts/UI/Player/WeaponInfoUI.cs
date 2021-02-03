using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CurrentClipText;
    [SerializeField] private TextMeshProUGUI WeaponNameText;
    [SerializeField] private TextMeshProUGUI TotalAmmoText;

    private WeaponComponent equippedWeapon;

    void OnEnable()
    {
        PlayerEvents.OnWeaponEquipped += OnWeaponEquipped;
    }

    void OnDisable()
    {
        PlayerEvents.OnWeaponEquipped -= OnWeaponEquipped;
    }


    private void OnWeaponEquipped(WeaponComponent weapon)
    {
        equippedWeapon = weapon;
        WeaponNameText.text = weapon.weaponStats.name;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentClipText.text = equippedWeapon.weaponStats.bulletsInClip.ToString();
        TotalAmmoText.text = equippedWeapon.weaponStats.totalBulletsAvailable.ToString();
    }
}
