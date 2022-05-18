using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

namespace SafeUnlockSystem
{
    public class SafeController : MonoBehaviour
    {
        [Header("Safe Model Reference")]
        [SerializeField] private GameObject safeModel = null;
        [SerializeField] private Transform safeDial = null;

        [Header("Animation References")]
        [SerializeField] private string safeAnimationName = "SafeDoorOpen";
        private Animator safeAnim;

        [Header("Animation Timers - Default: 1.0f / 0.5f")]
        [SerializeField] private float beforeAnimationStart = 1.0f;
        [SerializeField] private float beforeOpenDoor = 0.5f;

        [Header("Safe UI")]
        [SerializeField] private GameObject safeUI = null;

        [Header("Safe Solution: 0-15")]
        [Range(0, 15)][SerializeField] private int safeSolutionNum1 = 0;
        [Range(0, 15)][SerializeField] private int safeSolutionNum2 = 0;
        [Range(0, 15)][SerializeField] private int safeSolutionNum3 = 0;

        [Header("UI Numbers")]
        [SerializeField] private Text firstNumberUI = null;
        [SerializeField] private Text secondNumberUI = null;
        [SerializeField] private Text thirdNumberUI = null;

        [Header("UI Arrows")]
        [SerializeField] private Button firstArrowUI = null;
        [SerializeField] private Button secondArrowUI = null;
        [SerializeField] private Button thirdArrowUI = null;

        private bool firstNumber;
        private bool secondNumber;
        private bool thirdNumber;

        public bool disableClose = false;

        public int lockNumberInt;

        [Header("Trigger Interaction?")]
        [SerializeField] private bool isTriggerInteraction = false; 
        [SerializeField] private GameObject triggerObject = null; 

        [Header("Unity Event - What happens when you open the safe?")]
        [SerializeField] private UnityEvent safeOpened = null;

        void Awake()
        {
            if (isTriggerInteraction)
            {
                disableClose = true;
            }
            firstNumber = true;
            safeAnim = safeModel.gameObject.GetComponent<Animator>();

            firstNumberUI.color = Color.white;
            secondNumberUI.color = Color.gray;
            thirdNumberUI.color = Color.gray;

            firstArrowUI.interactable = true;
            secondArrowUI.interactable = false;
            thirdArrowUI.interactable = false;

            ColorBlock firstArrowCB = firstArrowUI.colors; firstArrowCB.normalColor = Color.white; firstArrowUI.colors = firstArrowCB;
            ColorBlock secondArrowCB = secondArrowUI.colors; secondArrowCB.normalColor = Color.gray; secondArrowUI.colors = secondArrowCB;
            ColorBlock thirdArrowCB = thirdArrowUI.colors; thirdArrowCB.normalColor = Color.gray; thirdArrowUI.colors = thirdArrowCB;
        }

        public void ShowSafeLock()
        {
            if (isTriggerInteraction)
            {
                disableClose = false;
                triggerObject.SetActive(false);
            }

            safeUI.SetActive(true);
            SafeDisableManager.instance.DisablePlayer(true);
            SafeAudioManager.instance.Play("InteractSound");
        }

        private void Update()
        {
            if (!disableClose)
            {
                if (Input.GetKeyDown(SafeInputManager.instance.closeKey))
                {
                    if (isTriggerInteraction)
                    {
                        disableClose = true;
                        triggerObject.SetActive(true);
                    }

                    SafeDisableManager.instance.DisablePlayer(false);
                    safeUI.SetActive(false);
                }
            }
        }

        private IEnumerator CheckCode()
        {
            string playerInputNumber = firstNumberUI.text + secondNumberUI.text + thirdNumberUI.text;
            string safeSolution = safeSolutionNum1.ToString("0") + safeSolutionNum2.ToString("0") + safeSolutionNum3.ToString("0");

            if (playerInputNumber == safeSolution)
            {
                SafeDisableManager.instance.DisablePlayer(false);
                safeUI.SetActive(false);
                safeModel.tag = "Untagged";

                SafeAudioManager.instance.Play("BoltUnlock");
                yield return new WaitForSeconds(beforeAnimationStart);
                safeAnim.Play(safeAnimationName, 0, 0.0f);  
                SafeAudioManager.instance.Play("HandleSpin");
                yield return new WaitForSeconds(beforeOpenDoor);
                SafeAudioManager.instance.Play("SafeDoorOpen");

                if (isTriggerInteraction)
                {
                    disableClose = true;
                    triggerObject.SetActive(false);
                }

                safeOpened.Invoke();
            }
            else
            {
                SafeAudioManager.instance.Play("LockRattle");
                firstNumberUI.text = "0";
                secondNumberUI.text = "0";
                thirdNumberUI.text = "0";
                firstNumber = true;
                secondNumber = false;
                thirdNumber = false;

                firstArrowUI.interactable = true;
                secondArrowUI.interactable = false;
                thirdArrowUI.interactable = false;

                firstNumberUI.color = Color.white;
                secondNumberUI.color = Color.gray;
                thirdNumberUI.color = Color.gray;

                ColorBlock firstArrowCB = firstArrowUI.colors; firstArrowCB.normalColor = Color.white; firstArrowUI.colors = firstArrowCB;
                ColorBlock secondArrowCB = secondArrowUI.colors; secondArrowCB.normalColor = Color.gray; secondArrowUI.colors = secondArrowCB;
                ColorBlock thirdArrowCB = thirdArrowUI.colors; thirdArrowCB.normalColor = Color.gray; thirdArrowUI.colors = thirdArrowCB;

                safeDial.transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
                lockNumberInt = 0;
            }
        }

        public void AcceptKey()
        {
            EventSystem.current.SetSelectedGameObject(null);
            SafeAudioManager.instance.Play("InteractSound");

            if (firstNumber)
            {
                firstNumber = false;
                secondNumber = true;
                thirdNumber = false;

                firstArrowUI.interactable = false;
                secondArrowUI.interactable = true;
                thirdArrowUI.interactable = false;

                secondNumberUI.text = lockNumberInt.ToString("0");

                firstNumberUI.color = Color.gray;
                secondNumberUI.color = Color.white;
                thirdNumberUI.color = Color.gray;

                ColorBlock firstArrowCB = firstArrowUI.colors; firstArrowCB.normalColor = Color.gray; firstArrowUI.colors = firstArrowCB;
                ColorBlock secondArrowCB = secondArrowUI.colors; secondArrowCB.normalColor = Color.white; secondArrowUI.colors = secondArrowCB;
                ColorBlock thirdArrowCB = thirdArrowUI.colors; thirdArrowCB.normalColor = Color.gray; thirdArrowUI.colors = thirdArrowCB;
            }
            else if (secondNumber)
            {
                firstNumber = false;
                secondNumber = false;
                thirdNumber = true;

                thirdNumberUI.text = lockNumberInt.ToString("0");

                firstArrowUI.interactable = false;
                secondArrowUI.interactable = false;
                thirdArrowUI.interactable = true;

                firstNumberUI.color = Color.gray;
                secondNumberUI.color = Color.gray;
                thirdNumberUI.color = Color.white;

                ColorBlock firstArrowCB = firstArrowUI.colors; firstArrowCB.normalColor = Color.gray; firstArrowUI.colors = firstArrowCB;
                ColorBlock secondArrowCB = secondArrowUI.colors; secondArrowCB.normalColor = Color.gray; secondArrowUI.colors = secondArrowCB;
                ColorBlock thirdArrowCB = thirdArrowUI.colors; thirdArrowCB.normalColor = Color.white; thirdArrowUI.colors = thirdArrowCB;
            }
            else if (thirdNumber)
            {
                firstNumber = false;
                secondNumber = false;
                thirdNumber = false;

                firstArrowUI.interactable = false;
                secondArrowUI.interactable = false;
                thirdArrowUI.interactable = false;

                firstNumberUI.color = Color.gray;
                secondNumberUI.color = Color.gray;
                thirdNumberUI.color = Color.gray;

                ColorBlock firstArrowCB = firstArrowUI.colors; firstArrowCB.normalColor = Color.gray; firstArrowUI.colors = firstArrowCB;
                ColorBlock secondArrowCB = secondArrowUI.colors; secondArrowCB.normalColor = Color.gray; secondArrowUI.colors = secondArrowCB;
                ColorBlock thirdArrowCB = thirdArrowUI.colors; thirdArrowCB.normalColor = Color.gray; thirdArrowUI.colors = thirdArrowCB;

                StartCoroutine(CheckCode());
            }
        }

        public void UpKey(int lockNumberSelection)
        {
            EventSystem.current.SetSelectedGameObject(null);
            SafeAudioManager.instance.Play("SafeClick");

            if (firstNumber && lockNumberSelection == 1)
            {
                if (lockNumberInt <= 14)
                {
                    safeDial.transform.Rotate(0.0f, 0.0f, -22.5f, Space.Self);
                    lockNumberInt++;
                    firstNumberUI.text = lockNumberInt.ToString("0");
                }
                else
                {
                    lockNumberInt = 0;
                    safeDial.transform.Rotate(0.0f, 0.0f, -22.5f, Space.Self);
                    firstNumberUI.text = lockNumberInt.ToString("0");
                }
            }

            if (secondNumber && lockNumberSelection == 2)
            {
                if (lockNumberInt >= 1)
                {
                    safeDial.transform.Rotate(0.0f, 0.0f, 22.5f, Space.Self);
                    lockNumberInt--;
                    secondNumberUI.text = lockNumberInt.ToString("0");
                }
                else
                {
                    lockNumberInt = 15;
                    safeDial.transform.Rotate(0.0f, 0.0f, 22.5f, Space.Self);
                    secondNumberUI.text = lockNumberInt.ToString("0");
                }
            }

            if (thirdNumber && lockNumberSelection == 3)
            {
                if (lockNumberInt <= 14)
                {
                    safeDial.transform.Rotate(0.0f, 0.0f, -22.5f, Space.Self);
                    lockNumberInt++;
                    thirdNumberUI.text = lockNumberInt.ToString("0");
                }
                else
                {
                    lockNumberInt = 0;
                    safeDial.transform.Rotate(0.0f, 0.0f, -22.5f, Space.Self);
                    thirdNumberUI.text = lockNumberInt.ToString("0");
                }
            }
        }
    }
}