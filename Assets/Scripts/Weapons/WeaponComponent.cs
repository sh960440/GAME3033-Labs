using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//namespace Weapons
//{
    [Serializable]
    public struct WeaponStats
    {
        public string name;
        public float damage;
        public int bulletsInClip;
        public int clipSize;
        public int totalBulletsAvailable;
        
        public float fireStartDelay;
        public float fireRate;
        public float fireDistance;
        public bool repeating;

        public LayerMask weaponHitLayer;
    }

    public class WeaponComponent : MonoBehaviour
    {
        public Transform handPosition => gripIKLocation;
        [SerializeField] private Transform gripIKLocation;

        public bool firing { get; private set; }
        public bool reloading { get; set; }

        public WeaponStats weaponStats;

        protected WeaponHolder WeaponHolder;
        protected CrosshairScript Crosshair;

        public void Initialize(WeaponHolder weaponHolder, CrosshairScript crosshair)
        {
            WeaponHolder = weaponHolder;
            Crosshair = crosshair;
        }

        public virtual void StartFiring()
        {
            firing = true;
            if (weaponStats.repeating)
            {
                InvokeRepeating(nameof(FireWeapon), weaponStats.fireStartDelay, weaponStats.fireRate);
            }
            else
            {
                FireWeapon();
            }
        }

        public virtual void StopFiring()
        {
            firing = false;
            CancelInvoke(nameof(FireWeapon));
        }

        protected virtual void FireWeapon()
        {

        }

        public void StartReloading()
        {
            reloading = true;
            ReloadWeapon();
        }

        public void StopReloading()
        {
            reloading = false;
        }

        private void ReloadWeapon()
        {
            int bulletToReload = weaponStats.totalBulletsAvailable - weaponStats.clipSize;
            if (bulletToReload < 0)
            {
                weaponStats.bulletsInClip += weaponStats.totalBulletsAvailable;
                weaponStats.totalBulletsAvailable = 0;
            }
            else
            {
                weaponStats.bulletsInClip = weaponStats.clipSize;
                weaponStats.totalBulletsAvailable -= weaponStats.clipSize;
            }
        }
    }
//}

