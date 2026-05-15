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
#if UNITY_IOS || UNITY_ANDROID
            _ = typeof(CSJAdvertisementManager);
#endif
        }
    }
}
