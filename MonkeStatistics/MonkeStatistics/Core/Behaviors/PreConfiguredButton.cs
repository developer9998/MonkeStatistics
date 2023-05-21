using MonkeStatistics.API;
using System.Collections;
using UnityEngine;

namespace MonkeStatistics.Core.Behaviors
{
    internal class PreConfiguredButton : UIButton
    {
        public bool _Toggling;
        public bool AllowHooking = true;

        public override void Start()
        {
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            boxCollider.isTrigger = true;
            gameObject.layer = 18;
            WardrobeItemButton wardrobeItemButton = GameObject.FindObjectOfType<WardrobeItemButton>();

            buttonRenderer = GetComponent<MeshRenderer>();
            pressedMaterial = wardrobeItemButton.pressedMaterial;
            unpressedMaterial = wardrobeItemButton.unpressedMaterial;

            buttonRenderer.material = unpressedMaterial;
        }

        public IEnumerator ToggleDelay(ButtonInfo Info = null)
        {
            _Toggling = true;
            isOn = !isOn;
            UpdateColor();
            if (Info != null)
                Info.RaiseEvent(isOn);
            yield return new WaitForSeconds(0.25f); // buffer
            _Toggling = false;
        }
        public IEnumerator ButtonDelay()
        {
            if (!isOn)
            {
                isOn = true;
                UpdateColor();
                yield return new WaitForSeconds(0.25f);
                isOn = false;
                UpdateColor();
            }
        }

        #region Disabling button
        private void OnDestroy()
        {
            StopAllCoroutines();
        }
        private void OnDisable()
        {
            StopAllCoroutines();
            GetComponent<MeshRenderer>().material = unpressedMaterial;
        }
        #endregion
    }
}
