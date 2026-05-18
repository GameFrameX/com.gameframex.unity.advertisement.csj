#if UNITY_IOS || UNITY_ANDROID

using System;
using GameFrameX.Advertisement.Runtime;
using GameFrameX.Runtime;
using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.Advertisement.CSJ.Runtime
{
    [Preserve]
    [AdvertisementConfig(typeof(CSJAdvertisementConfig))]
    public sealed class CSJAdvertisementManager : BaseAdvertisementManager
    {
        private string _adUnitId;
        private string _appId;
        private bool _isDebug;

        [Preserve]
        public override void Initialize(AdvertisementConfig config)
        {
            var csjConfig = config as CSJAdvertisementConfig;
            if (csjConfig == null)
            {
                throw new ArgumentException("Config must be CSJAdvertisementConfig", nameof(config));
            }

            GameFrameworkGuard.NotNullOrEmpty(csjConfig.appId, nameof(csjConfig.appId));
            GameFrameworkGuard.NotNullOrEmpty(csjConfig.adUnitId, nameof(csjConfig.adUnitId));
            _appId = csjConfig.appId;
            _adUnitId = csjConfig.adUnitId;
            _isDebug = csjConfig.isDebug;

            CSJBridge.Init(_appId, _isDebug);
        }

        [Preserve]
        public override void Play(AdvertisementPlayOption option)
        {
#pragma warning disable CS0618
            Load((s) =>
            {
                Show(option.OnSuccess, option.OnFail, option.OnShowResult, option.customData);
            }, (fail) =>
            {
                Debug.Log($"[CSJ] Play Load Fail: {fail}");
                option.OnFail?.Invoke(fail);
                option.OnShowResult?.Invoke(false);
            }, option.extraData);
#pragma warning restore CS0618
        }

#pragma warning disable CS0618
        [Preserve]
        public override void Play(Action<bool> playResult, string customData = null)
        {
            Load((s) =>
            {
                Show(
                    (success) => { Debug.Log($"[CSJ] Play Success: {success}"); },
                    (fail) =>
                    {
                        Debug.Log($"[CSJ] Play Fail: {fail}");
                        playResult?.Invoke(false);
                    },
                    playResult,
                    customData
                );
            }, (fail) =>
            {
                Debug.Log($"[CSJ] Play Load Fail: {fail}");
                playResult?.Invoke(false);
            }, customData);
        }

        [Preserve]
        public override void Show(Action<string> success, Action<string> fail, Action<bool> onShowResult, string customData = null)
        {
            if (!CSJBridge.IsAdReady())
            {
                fail?.Invoke("广告未加载完成");
                onShowResult?.Invoke(false);
                return;
            }

            CSJBridge.Show(
                (rewardVerify) =>
                {
                    onShowResult?.Invoke(rewardVerify);
                    if (rewardVerify)
                    {
                        success?.Invoke(customData);
                    }
                },
                (errorMsg) =>
                {
                    fail?.Invoke(errorMsg);
                    onShowResult?.Invoke(false);
                });
        }

        [Preserve]
        public override void Load(Action<string> success, Action<string> fail, string extraData = null)
        {
            var userId = GetExtraDataValue("userId");
            var serialized = SerializeExtraData();
            var customData = extraData ?? serialized;
            Debug.Log($"[CSJ] Load ad: slotId={_adUnitId}, userId={userId ?? "(null)"}, extraData={customData ?? "(null)"}");
            CSJBridge.LoadAd(_adUnitId, userId, customData,
                () => success?.Invoke(customData),
                (errorMsg) => fail?.Invoke(errorMsg)
            );
        }
#pragma warning restore CS0618

        protected override void Shutdown()
        {
            CSJBridge.Destroy();
        }
    }
}

#endif
