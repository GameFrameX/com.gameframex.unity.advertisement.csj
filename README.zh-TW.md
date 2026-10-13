<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Advertisement (CSJ / 穿山甲)

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.advertisement.csj)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.csj/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.advertisement.csj)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.csj/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使

<br />

[文檔](https://gameframex.doc.alianblank.com) · [快速開始](#quick-start) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## 項目簡介

[Game Frame X 廣告系統](https://github.com/GameFrameX/com.gameframex.unity.advertisement)的穿山甲（CSJ/Pangle）平台適配器。為 iOS 和 Android 應用提供基於字節跳動穿山甲廣告 SDK 的激勵影片廣告整合。

### 功能特性

- 基於穿山甲（CSJ/Pangle）SDK 的激勵影片廣告支援
- 原生 iOS 和 Android 平台支援
- IL2CPP 程式碼裁剪保護
- 條件編譯（`UNITY_IOS`、`UNITY_ANDROID`）
- 與 Game Frame X 廣告元件無縫整合

## 架構概覽

本套件是 Game Frame X 廣告核心 `BaseAdvertisementManager` 的**適配器實現**。透過 `CSJAdvertisementBootstrap` 在 `BeforeSceneLoad` 階段自註冊進主套件 `AdvertisementRegistry`（未安裝本套件時代碼被裁剪，註冊不會發生；引用即註冊）；渠道初始化由 Bootstrap 工廠閉包注入強型別配置。

| 類別 | 說明 |
|------|------|
| `CSJAdvertisementManager` | 激勵影片廣告管理器 — 載入、展示及生命週期管理 |
| `CSJAdvertisementConfig` | 廣告配置，包含 appId 和 adUnitId 欄位，經 `CSJAdvertisementBootstrap.Config` 在啟動期注入 |
| `CSJAdvertisementBootstrap` | 渠道啟動註冊 — 含啟動期 `Config` 靜態屬性，`BeforeSceneLoad` 階段自註冊進 `AdvertisementRegistry` |
| `CSJBridge` | iOS/Android 原生平台呼叫橋接 |
| `CSJNativeMessageHandler` | 原生訊息回調處理器 |
| `GameFrameXAdvertisementCSJCroppingHelper` | IL2CPP link.xml 替代方案 — 保留類型引用 |

## 快速開始

### 安裝

選擇以下任一方式：

1. 編輯 Unity 專案的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：
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

   `scopes` 控制哪些套件透過此註冊表解析。只有以 `com.gameframex` 開頭的套件才會從這個註冊表取得。

2. 直接在 `manifest.json` 的 `dependencies` 節點下添加以下內容：
   ```json
   {
      "com.gameframex.unity.advertisement.csj": "https://github.com/gameframex/com.gameframex.unity.advertisement.csj.git"
   }
   ```
3. 在 Unity 的 `Package Manager` 中使用 `Git URL` 的方式添加庫，地址為：`https://github.com/gameframex/com.gameframex.unity.advertisement.csj.git`
4. 直接下載倉庫放置到 Unity 專案的 `Packages` 目錄下，會自動載入識別。

### 渠道註冊與配置

在啟動期（首次廣告呼叫前）對 `CSJAdvertisementBootstrap.Config` 賦值：

```csharp
using GameFrameX.Advertisement.CSJ.Runtime;

CSJAdvertisementBootstrap.Config = new CSJAdvertisementConfig
{
    appId = "你的穿山甲 AppId",
    adUnitId = "你的廣告位ID",
    isDebug = false,
};
```

- 本套件透過 `CSJAdvertisementBootstrap` 自註冊進 `AdvertisementRegistry`，渠道常量 `ChannelId = "csj"`；
- 與 TopOn 渠道同裝時，可用 `AdvertisementRegistry.ResolveManager("csj")` 顯式路由做人工選型。
## 平台支援

| 平台 | 支援 |
|------|------|
| iOS | 是 |
| Android | 是 |
| WebGL | 否 |
| Standalone | 否 |

> 需要 `UNITY_IOS` 或 `UNITY_ANDROID` 腳本巨集定義。

## 文檔與資源

- [Game Frame X 文檔](https://gameframex.doc.alianblank.com)
- [穿山甲開發者平台](https://www.csjplatform.com)

## 社區與支援

- QQ群：[加入](https://qm.qq.com/q/urCUAqJCJm)
- GitHub Issues：[回報問題](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/issues)

## 更新日誌

### v2.0.0

- 渠道改為透過 `CSJAdvertisementBootstrap` 在 `BeforeSceneLoad` 階段自註冊進主套件 `AdvertisementRegistry`，配置經 `CSJAdvertisementBootstrap.Config` 啟動期注入
- **破壞性變更**：`AdvertisementComponent` 的 Inspector 類型下拉與巨集選廣告位ID欄位已移除，appId / adUnitId 改經 `CSJAdvertisementBootstrap.Config` 注入

### v1.0.0

- 初始發佈
- 支援穿山甲（CSJ/Pangle）平台激勵影片廣告
- IL2CPP 裁剪保護


## 依賴

| 套件 | 說明 |
|------|------|
| (无) | - |

## 開源協議

詳見 [LICENSE.md](LICENSE.md) 檔案。
