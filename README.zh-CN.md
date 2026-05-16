<div align="center">

![GameFrameX Logo](https://download.alianblank.com/gameframex/gameframex_logo_320.png)

# Game Frame X Advertisement (CSJ / 穿山甲)

[![Version](https://img.shields.io/github/v/release/gameframex/com.gameframex.unity.advertisement.csj?label=version&color=green)](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/releases)
[![License](https://img.shields.io/badge/license-MIT+Apache%202.0-orange.svg)](LICENSE.md)
[![Documentation](https://img.shields.io/badge/docs-gameframex-brightgreen.svg)](https://gameframex.doc.alianblank.com)

**独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使**

[📖 文档](https://gameframex.doc.alianblank.com) • [🚀 快速开始](#快速开始) • [💬 QQ群](https://qm.qq.com/q/urCUAqJCJm)

---

🌐 **语言**: [English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

---

</div>

## 项目简介

[Game Frame X 广告系统](https://github.com/GameFrameX/com.gameframex.unity.advertisement)的穿山甲（CSJ/Pangle）平台适配器。为 iOS 和 Android 应用提供基于字节跳动穿山甲广告 SDK 的激励视频广告集成。

### 功能特性

- 基于穿山甲（CSJ/Pangle）SDK 的激励视频广告支持
- 原生 iOS 和 Android 平台支持
- IL2CPP 代码裁剪保护
- 条件编译（`UNITY_IOS`、`UNITY_ANDROID`）
- 与 Game Frame X 广告组件无缝集成

## 架构概览

本包是 Game Frame X 广告核心 `BaseAdvertisementManager` 的**适配器实现**。通过 Unity Inspector 配置 `AdvertisementComponent` 后，自动发现并加载。

| 类 | 说明 |
|----|------|
| `CSJAdvertisementManager` | 激励视频广告管理器 — 加载、展示及生命周期管理 |
| `CSJAdvertisementConfig` | 广告配置，包含 appId 和 adUnitId 字段 |
| `CSJBridge` | iOS/Android 原生平台调用桥接 |
| `CSJNativeMessageHandler` | 原生消息回调处理器 |
| `GameFrameXAdvertisementCSJCroppingHelper` | IL2CPP link.xml 替代方案 — 保留类型引用 |

## 快速开始

### 安装

1. 安装[广告核心包](https://github.com/GameFrameX/com.gameframex.unity.advertisement)
2. 通过 Unity Package Manager (UPM) 添加本适配器：

```json
{
  "dependencies": {
    "com.gameframex.unity.advertisement": "https://github.com/GameFrameX/com.gameframex.unity.advertisement.git",
    "com.gameframex.unity.advertisement.csj": "https://github.com/gameframex/com.gameframex.unity.advertisement.csj.git"
  }
}
```

或在 Unity Package Manager 窗口中通过 git URL 添加。

### 使用示例

在 Unity Inspector 中配置：将 `AdvertisementComponent` 添加到 GameObject，然后在实现类型下拉框中选择 `CSJAdvertisementManager`。

> **提示：** 如果已安装 `com.gameframex.unity.entry`，可以使用 `GameApp` 静态门面类进行快捷调用：`GameApp.Advertisement` 替代 `GameEntry.GetComponent<AdvertisementComponent>()`。

```csharp
using GameFrameX.Advertisement.Runtime;

// 设置服务端验证数据（可选）
GameApp.Advertisement.SetExtraData("userId", player.UserId);

// 播放激励视频广告
var option = new AdvertisementPlayOption
{
    OnSuccess    = (data) => Debug.Log("广告展示成功"),
    OnFail       = (err) => Debug.LogError($"广告展示失败: {err}"),
    OnShowResult = (watched) =>
    {
        if (watched)
        {
            // 发放奖励
        }
    },
};
GameApp.Advertisement.Play(option);
```

## 平台支持

| 平台 | 支持 |
|------|------|
| iOS | 是 |
| Android | 是 |
| WebGL | 否 |
| Standalone | 否 |

> 需要 `UNITY_IOS` 或 `UNITY_ANDROID` 脚本宏定义。

## 文档与资源

- [Game Frame X 文档](https://gameframex.doc.alianblank.com)
- [穿山甲开发者平台](https://www.csjplatform.com)

## 社区与支持

- QQ群：[加入](https://qm.qq.com/q/urCUAqJCJm)
- GitHub Issues：[报告问题](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/issues)

## 更新日志

### v1.0.0

- 初始发布
- 支持穿山甲（CSJ/Pangle）平台激励视频广告
- IL2CPP 裁剪保护

## 开源协议

本项目基于 [MIT 许可证](LICENSE.md) 和 [Apache 许可证 2.0](LICENSE.md) 双重授权。
