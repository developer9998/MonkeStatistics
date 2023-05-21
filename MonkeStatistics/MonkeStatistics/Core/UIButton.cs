using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MonkeStatistics.Core
{
    public class UIButton : MonoBehaviour
    {
        public Material pressedMaterial;
        public Material unpressedMaterial;
        public MeshRenderer buttonRenderer;

        public bool isOn;

        public float debounceTime = 0.25f;
        public float touchTime;

        public bool testPress;
        public bool testHandLeft;

        [TextArea]
        public string offText;

        [TextArea]
        public string onText;

        public Text myText;

        public virtual void Start()
        {
        }

        private void OnEnable()
        {
            if (Application.isEditor)
            {
                StartCoroutine(TestPressCheck());
            }
        }

        private void OnDisable()
        {
            if (Application.isEditor)
            {
                StopAllCoroutines();
            }
        }

        private IEnumerator TestPressCheck()
        {
            while (true)
            {
                if (testPress)
                {
                    testPress = false;
                    ButtonActivation();
                    ButtonActivationWithHand(testHandLeft);
                }

                yield return new WaitForSeconds(1f);
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (enabled && Time.time >= (touchTime + debounceTime) && collider.TryGetComponent(out GorillaTriggerColliderHandIndicator component))
            {
                touchTime = Time.time;
                ButtonActivation();
                ButtonActivationWithHand(component.isLeftHand);
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, component.isLeftHand, 0.05f);
                GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 2f, GorillaTagger.Instance.tapHapticDuration);
            }
        }

        public virtual void UpdateColor()
        {
            if (isOn)
            {
                buttonRenderer.material = pressedMaterial;
                if (myText != null)
                {
                    myText.text = onText;
                }
            }
            else
            {
                buttonRenderer.material = unpressedMaterial;
                if (myText != null)
                {
                    myText.text = offText;
                }
            }
        }

        public virtual void ButtonActivation()
        {
        }

        public virtual void ButtonActivationWithHand(bool isLeftHand)
        {
        }
    }
}
