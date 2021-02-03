using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace Weapons
//{
    public class AK47WeaponComponent : WeaponComponent
    {
        private Camera viewCamera;

        private RaycastHit hitLocation;

        private void Awake()
        {
            viewCamera = Camera.main;
        }

        protected new void FireWeapon()
        {
            if (weaponStats.bulletsInClip > 0 && !reloading)
            {
                Ray screenRay = viewCamera.ScreenPointToRay(new Vector3(Crosshair.currentAimPosition.x, Crosshair.currentAimPosition.y, 0));
            
                if (!Physics.Raycast(screenRay, out RaycastHit hit, weaponStats.fireDistance, weaponStats.weaponHitLayer))
                    return;
                
                Vector3 rayDirection = hitLocation.point - viewCamera.transform.position;
                
                Debug.DrawRay(viewCamera.transform.position, rayDirection * weaponStats.fireDistance, Color.red);

                hitLocation = hit;

                weaponStats.bulletsInClip--;
            }
            else
            {
                StartReloading();
            }
        }

        private void OnDrawGizmos()
        {
            if (hitLocation.transform)
            {
                Gizmos.DrawSphere(hitLocation.point, 0.2f);
            }
        }
    }
//}

