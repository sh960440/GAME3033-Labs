using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private Transform weaponSocket;

    private Transform gripLocation;

    private PlayerController playerController;
    private Animator playerAnimator;

    private Camera mainCamera;
    private WeaponComponent equippedWeapon;

    private readonly int AimVerticalHash = Animator.StringToHash("AimVertical");
    private readonly int AimHorizontalHash = Animator.StringToHash("AimHorizontal");
    private readonly int IsFiringHash = Animator.StringToHash("IsFiring");
    private readonly int IsReloadingHash = Animator.StringToHash("IsReloading");

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerAnimator = GetComponent<Animator>();

        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    private void Start()
    {
        GameObject spawnedWeapon = Instantiate(weapon, weaponSocket.position, weaponSocket.rotation);

        if (!spawnedWeapon) return;

        spawnedWeapon.transform.parent = weaponSocket;
        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        gripLocation = equippedWeapon.handPosition;
        
        equippedWeapon.Initialize(this, playerController.crosshairComponent);
        
        PlayerEvents.Invoke_OnWeaponEquipped(equippedWeapon);
    }

    public void OnLook(InputValue delta)
    {
        Vector3 independentMousePosition = mainCamera.ScreenToViewportPoint(playerController.crosshairComponent.currentAimPosition);
        
        playerAnimator.SetFloat(AimVerticalHash, independentMousePosition.y);
        playerAnimator.SetFloat(AimHorizontalHash, independentMousePosition.x);
    }

    public void OnFire(InputValue button)
    {
        if (button.isPressed)
        {
            playerController.IsFiring = true;
            playerAnimator.SetBool(IsFiringHash, playerController.IsFiring);
            equippedWeapon.StartFiring();
        }
        else
        {
            playerController.IsFiring = false;
            playerAnimator.SetBool(IsFiringHash, playerController.IsFiring);
            equippedWeapon.StopFiring();
        }

    }

    public void OnReload(InputValue button)
    {
        StartReloading();
    }

    public void StartReloading()
    {
        playerController.IsReloading = true;
        playerAnimator.SetBool(IsReloadingHash, playerController.IsReloading);
        equippedWeapon.StartReloading();

        InvokeRepeating(nameof(StopReloading), 0, 0.1f);
    }

    public void StopReloading()
    {
        if (playerAnimator.GetBool(IsReloadingHash)) return;

        playerController.IsReloading = false;
        equippedWeapon.StopReloading();

        CancelInvoke(nameof(StopReloading));
    }

    private void OnAnimatorIK(int layerIndex)
    {
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, gripLocation.position);
    }
}
