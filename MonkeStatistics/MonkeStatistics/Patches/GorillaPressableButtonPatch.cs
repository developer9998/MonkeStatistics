using HarmonyLib;
using MonkeStatistics.API;
using MonkeStatistics.Core;
using MonkeStatistics.Core.Behaviors;
using MonkeStatistics.Util;

namespace MonkeStatistics.Patches
{
    [HarmonyPatch(typeof(UIButton), "OnTriggerEnter", MethodType.Normal)]
    internal class GorillaPressableButtonPatch
    {
        [HarmonyPrefix]
        private static bool Hook(UIButton __instance)
        {
            // get if watchbutton
            bool IsWatch = WatchButton.Instance.gameObject == __instance.gameObject;
            if (IsWatch && !WatchButton.Instance.GetFacingUp())
            {
                // throw new System.Exception("Button not facing up!");
                return false;
            }

            // pre configured button
            bool IsPreConfigured = __instance is PreConfiguredButton;
            if (IsPreConfigured)
            {
                "Preconfigured button pressed".BepInLog();
                PreConfiguredButton Button = __instance as PreConfiguredButton;
                if (Button.AllowHooking && Button._Toggling)
                {
                    // throw new System.Exception("This button is currently being toggled, please fuck off and stop trying to spam my button. It makes me sad and angry. If you spam my button I am going to cry.");
                    return false;
                }
            }

            bool IsAPIButton = __instance is Button;
            if (IsAPIButton)
            {
                Button button = __instance as Button;
                if (button.GetOnInitDelay())
                {
                    // throw new System.Exception("Button delay init.");
                    return false;
                }
            }

            return true;
        }
    }
}