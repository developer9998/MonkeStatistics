using UnityEngine;

namespace MonkeStatistics.Core
{
    internal class CreateWatch
    {
        public GameObject WatchObj;
        public CreateWatch()
        {
            WatchObj = UnityEngine.Object.Instantiate((GameObject)Util.AssetLoader.GetAsset("Watch"));
            Transform WatchTransform = WatchObj.transform;
            Transform HandTransform = GorillaTagger.Instance.offlineVRRig.leftHandTransform.parent;
            WatchTransform.SetParent(HandTransform);
            WatchTransform.localPosition = new Vector3(0.0288f, 0.0267f, -0.004f);
            WatchTransform.localRotation = Quaternion.Euler(-26.97f, 94.478f, -93.21101f);

            new UIManager(WatchObj);

            GameObject Trigger = WatchTransform.Find("Trigger").gameObject;
            Trigger.layer = 18;
            Trigger.AddComponent<Behaviors.WatchButton>();
        }
    }
}
