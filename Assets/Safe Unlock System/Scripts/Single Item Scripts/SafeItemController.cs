using UnityEngine;

namespace SafeUnlockSystem
{
    public class SafeItemController : MonoBehaviour
    {
        [SerializeField] private SafeController _safeController = null;

        public void ShowSafeLock()
        {
            _safeController.ShowSafeLock();
        }
    }
}
