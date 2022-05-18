using UnityEngine;

namespace SafeUnlockSystem
{
    public class SafeInputManager : MonoBehaviour
    {
        [Header("Safe Interaction")]
        public KeyCode openKey;

        [Header("Close Safe Interaction")]
        public KeyCode closeKey;

        [Header("Trigger Safe Interaction")]
        public KeyCode triggerInteractKey;

        public static SafeInputManager instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }
    }
}
