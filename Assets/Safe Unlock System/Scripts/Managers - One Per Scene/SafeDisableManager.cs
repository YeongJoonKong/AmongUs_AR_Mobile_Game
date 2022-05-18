using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

namespace SafeUnlockSystem
{
    public class SafeDisableManager : MonoBehaviour
    {
        public static SafeDisableManager instance;

        [SerializeField] private Image crosshair = null;
        [SerializeField] private FirstPersonController player = null;
        [SerializeField] private SafeRaycast raycastManager = null;

        void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }

        public void DisablePlayer(bool disable)
        {
            if (disable)
            {
                raycastManager.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                crosshair.enabled = false;
                player.enabled = false;
            }
            else
            {
                raycastManager.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                crosshair.enabled = true;
                player.enabled = true;
            }
        }
    }
}
