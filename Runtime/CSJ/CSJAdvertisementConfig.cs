#if UNITY_IOS || UNITY_ANDROID

using System;
using GameFrameX.Advertisement.Runtime;
using UnityEngine.Scripting;

namespace GameFrameX.Advertisement.CSJ.Runtime
{
    [Preserve]
    [Serializable]
    public class CSJAdvertisementConfig : AdvertisementConfig
    {
        [Preserve]
        public string appId;

        [Preserve]
        public string adUnitId;
    }
}

#endif
