<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Advertisement (CSJ / 穿山甲)

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.advertisement.csj)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.csj/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.advertisement.csj)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.csj/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使

<br />

[文档](https://gameframex.doc.alianblank.com) · [快速开始](#quick-start) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

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

本包是 Game Frame X 广告核心 `BaseAdvertisementManager` 的**适配器实现**。通过 `CSJAdvertisementBootstrap` 在 `BeforeSceneLoad` 阶段自注册进主包 `AdvertisementRegistry`（未安装本包时代码被裁剪，注册不会发生；引用即注册）；渠道初始化由 Bootstrap 工厂闭包注入强类型配置。

| 类 | 说明 |
|----|------|
| `CSJAdvertisementManager` | 激励视频广告管理器 — 加载、展示及生命周期管理 |
| `CSJAdvertisementConfig` | 广告配置，包含 appId 和 adUnitId 字段，经 `CSJAdvertisementBootstrap.Config` 在启动期注入 |
| `CSJAdvertisementBootstrap` | 渠道启动注册 — 含启动期 `Config` 静态属性，`BeforeSceneLoad` 阶段自注册进 `AdvertisementRegistry` |
| `CSJBridge` | iOS/Android 原生平台调用桥接 |
| `CSJNativeMessageHandler` | 原生消息回调处理器 |
| `GameFrameXAdvertisementCSJCroppingHelper` | IL2CPP link.xml 替代方案 — 保留类型引用 |

## 快速开始

### 安装

选择以下任一方式：

1. 编辑 Unity 项目的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：
   ```json
   {
     "scopedRegistries": [
       {
         "name": "GameFrameX",
         "url": "https://gameframex.upm.alianblank.uk",
         "scopes": [
           "com.gameframex"
         ]
       }
     ],
     "dependencies": {
       "com.gameframex.unity.advertisement.csj": "1.0.0"
     }
   }
   ```

   `scopes` 控制哪些包通过此注册表解析。只有以 `com.gameframex` 开头的包才会从这个注册表获取。

2. 直接在 `manifest.json` 的 `dependencies` 节点下添加以下内容：
   ```json
   {
      "com.gameframex.unity.advertisement.csj": "https://github.com/gameframex/com.gameframex.unity.advertisement.csj.git"
   }
   ```
3. 在 Unity 的 `Package Manager` 中使用 `Git URL` 的方式添加库，地址为：`https://github.com/gameframex/com.gameframex.unity.advertisement.csj.git`
4. 直接下载仓库放置到 Unity 项目的 `Packages` 目录下，会自动加载识别。

### 渠道注册与配置

在启动期（首次广告调用前）对 `CSJAdvertisementBootstrap.Config` 赋值：

```csharp
using GameFrameX.Advertisement.CSJ.Runtime;

CSJAdvertisementBootstrap.Config = new CSJAdvertisementConfig
{
    appId = "你的穿山甲 AppId",
    adUnitId = "你的广告位ID",
    isDebug = false,
};
```

- 本包通过 `CSJAdvertisementBootstrap` 自注册进 `AdvertisementRegistry`，渠道常量 `ChannelId = "csj"`；
- 与 TopOn 渠道同装时，可用 `AdvertisementRegistry.ResolveManager("csj")` 显式路由做人工选型。
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

### v2.0.0

- 渠道改为通过 `CSJAdvertisementBootstrap` 在 `BeforeSceneLoad` 阶段自注册进主包 `AdvertisementRegistry`，配置经 `CSJAdvertisementBootstrap.Config` 启动期注入
- **破坏性变更**：`AdvertisementComponent` 的 Inspector 类型下拉与宏选广告位ID字段已移除，appId / adUnitId 改经 `CSJAdvertisementBootstrap.Config` 注入

### v1.0.0

- 初始发布
- 支持穿山甲（CSJ/Pangle）平台激励视频广告
- IL2CPP 裁剪保护


## 依赖

| 包 | 说明 |
|----|------|
| (无) | - |

## 开源协议

详见 [LICENSE.md](LICENSE.md) 文件。
