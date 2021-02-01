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

    private readonly int AimVerticalHash = Animator.StringToHash("AimVertical");
    private readonly int AimHorizontalHash = Animator.StringToHash("AimHorizontal");

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
        WeaponComponent Weapon = spawnedWeapon.GetComponent<WeaponComponent>();
        gripLocation = Weapon.handPosition;

    }

    public void OnLook(InputValue delta)
    {
        Vector3 independentMousePosition = mainCamera.ScreenToViewportPoint(playerController.crosshairComponent.currentAimPosition);
        
        playerAnimator.SetFloat(AimVerticalHash, independentMousePosition.y);
        playerAnimator.SetFloat(AimHorizontalHash, independentMousePosition.x);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, gripLocation.position);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
