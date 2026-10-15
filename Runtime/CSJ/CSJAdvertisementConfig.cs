#if UNITY_IOS || UNITY_ANDROID

using System;
using GameFrameX.Advertisement.Runtime;

namespace GameFrameX.Advertisement.CSJ.Runtime
{
    [UnityEngine.Scripting.Preserve]
    [Serializable]
    public class CSJAdvertisementConfig : AdvertisementConfig
    {
        [UnityEngine.Scripting.Preserve]
        public string appId;

        [UnityEngine.Scripting.Preserve]
        public string adUnitId;
    }
}

#endif
