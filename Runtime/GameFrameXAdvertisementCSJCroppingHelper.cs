#if UNITY_IOS || UNITY_ANDROID

using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.Advertisement.CSJ.Runtime
{
    [Preserve]
    public class GameFrameXAdvertisementCSJCroppingHelper : MonoBehaviour
    {
        [Preserve]
        private void Start()
        {
            _ = typeof(CSJAdvertisementManager);
        }
    }
}

#endif
