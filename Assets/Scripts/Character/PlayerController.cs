using UnityEngine;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        public CrosshairScript crosshairComponent => crosshairScript; // Read only
        [SerializeField] private CrosshairScript crosshairScript;

        public bool IsFiring;
        public bool IsReloading;
        public bool IsJumping;
        public bool IsRunning;
    }
}
