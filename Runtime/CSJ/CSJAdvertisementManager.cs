#if UNITY_IOS || UNITY_ANDROID

using System;
using GameFrameX.Advertisement.Runtime;
using GameFrameX.Runtime;
using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.Advertisement.CSJ.Runtime
{
    /// <summary>
    /// 穿山甲（CSJ）聚合广告管理器。
    /// </summary>
    /// <remarks>
    /// 渠道身份常量 <see cref="ChannelId"/> 供注册表显式路由与人工选型使用；初始化统一走
    /// <see cref="Initialize(AdvertisementConfig)"/> 的强类型 <c>CSJAdvertisementConfig</c>（由 CSJAdvertisementBootstrap 工厂闭包注入）。
    /// </remarks>
    [Preserve]
    public sealed class CSJAdvertisementManager : BaseAdvertisementManager, IShutdownableModule
    {
        /// <summary>
        /// 穿山甲渠道唯一标识。
        /// </summary>
        [Preserve]
        public const string ChannelId = "csj";

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

#if !UNITY_EDITOR
            CSJBridge.Init(_appId, _isDebug);
#endif
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

        public void Shutdown()
        {
            CSJBridge.Destroy();
        }
    }
}

#endif
