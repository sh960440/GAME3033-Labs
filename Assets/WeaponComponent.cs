using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    public Transform handPosition => gripIKLocation;
    [SerializeField] private Transform gripIKLocation;
}
