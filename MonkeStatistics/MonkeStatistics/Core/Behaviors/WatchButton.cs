using System.Collections;
using UnityEngine;

namespace MonkeStatistics.Core.Behaviors
{
    internal class WatchButton : UIButton
    {
        internal static WatchButton Instance;

        public override void Start()
        {
            Instance = this;
            gameObject.layer = 18;
        }
        public override void ButtonActivation()
        {
            base.ButtonActivation(); // not sure if anything is running on this, and to lazy to check O-o
            UIManager.Instance.WatchButtonPressed();
        }
        private void Update()
        {
            if (!GetFacingUp() && UIManager.Instance.MenuObj.activeSelf)
            {
                UIManager.Instance.ShowPage(typeof(Pages.MainPage));
                UIManager.Instance.MenuObj.SetActive(false);
            }
        }

        private IEnumerator ButtonDelay()
        {
            isOn = true;
            UpdateColor();
            yield return new WaitForSeconds(0.35f);
            isOn = false;
            UpdateColor();
        }

        internal bool GetFacingUp()
        {
            float Distance = Vector3.Distance(transform.forward, Vector3.up);
            return Distance <= 0.65;
        }
    }
}
