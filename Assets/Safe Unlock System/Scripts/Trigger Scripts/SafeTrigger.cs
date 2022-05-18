using UnityEngine;

namespace SafeUnlockSystem
{
    public class SafeTrigger : MonoBehaviour
    {
        [Header("Safe Controller Object")]
        [SerializeField] private SafeItemController _safeItemController = null;

        [Header("UI Prompt")]
        public GameObject interactPrompt;

        private bool canUse;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                canUse = true;
                interactPrompt.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                canUse = false;
                interactPrompt.SetActive(false);
            }
        }

        private void Update()
        {
            if (canUse)
            {
                if (Input.GetKeyDown(SafeInputManager.instance.triggerInteractKey))
                {
                    interactPrompt.SetActive(false);
                    _safeItemController.ShowSafeLock();
                }
            }
        }
    }
}
