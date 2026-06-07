<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Advertisement (CSJ / 穿山甲)

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.advertisement.csj)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.csj/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.advertisement.csj)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.csj/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams

<br />

[Documentation](https://gameframex.doc.alianblank.com) · [Quick Start](#quick-start) · [QQ Group](https://qm.qq.com/q/urCUAqJCJm)

<br />

**English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>
## Project Overview

CSJ (穿山甲/Pangle) platform adapter for the [Game Frame X Advertisement](https://github.com/GameFrameX/com.gameframex.unity.advertisement) system. This package provides rewarded video ad integration for iOS and Android apps using ByteDance's CSJ (穿山甲) advertising SDK.

### Features

- Rewarded video ad support via CSJ (穿山甲/Pangle) SDK
- Native iOS and Android platform support
- IL2CPP code stripping protection
- Conditional compilation (`UNITY_IOS`, `UNITY_ANDROID`)
- Seamless integration with the Game Frame X Advertisement component

## Architecture

This package is an **adapter implementation** of `BaseAdvertisementManager` from the Game Frame X Advertisement core. It is discovered and loaded automatically by `AdvertisementComponent` via Unity Inspector configuration.

| Class | Description |
|-------|-------------|
| `CSJAdvertisementManager` | Rewarded video ad manager — load, show, and lifecycle |
| `CSJAdvertisementConfig` | Configuration with appId and adUnitId fields |
| `CSJBridge` | Native bridge for iOS/Android platform calls |
| `CSJNativeMessageHandler` | Native message callback handler |
| `GameFrameXAdvertisementCSJCroppingHelper` | IL2CPP link.xml alternative — preserves type references |

## Quick Start

### Installation

1. Install the [Advertisement core package](https://github.com/GameFrameX/com.gameframex.unity.advertisement)
2. Add this adapter via Unity Package Manager (UPM):

```json
{
  "dependencies": {
    "com.gameframex.unity.advertisement": "https://github.com/GameFrameX/com.gameframex.unity.advertisement.git",
    "com.gameframex.unity.advertisement.csj": "https://github.com/gameframex/com.gameframex.unity.advertisement.csj.git"
  }
}
```

Or add via git URL in the Unity Package Manager window.

### Usage

Configure in Unity Inspector: add the `AdvertisementComponent` to a GameObject, then select `CSJAdvertisementManager` from the implementation dropdown.

```csharp
using GameFrameX.Advertisement.Runtime;

// Standard: via GameEntry (no dependency on com.gameframex.unity.entry)
var adComponent = GameEntry.GetComponent<AdvertisementComponent>();

// Set server-side verification data (optional)
adComponent.SetExtraData("userId", player.UserId);

// Play rewarded video ad
var option = new AdvertisementPlayOption
{
    OnSuccess    = (data) => Debug.Log("Ad shown successfully"),
    OnFail       = (err) => Debug.LogError($"Ad failed: {err}"),
    OnShowResult = (watched) =>
    {
        if (watched)
        {
            // Reward the user
        }
    },
};
adComponent.Play(option);

// Shortcut: via GameApp (requires com.gameframex.unity.entry)
GameApp.Advertisement.SetExtraData("userId", player.UserId);
var option2 = new AdvertisementPlayOption
{
    OnSuccess    = (data) => Debug.Log("Ad shown successfully"),
    OnFail       = (err) => Debug.LogError($"Ad failed: {err}"),
    OnShowResult = (watched) =>
    {
        if (watched)
        {
            // Reward the user
        }
    },
};
GameApp.Advertisement.Play(option2);
```

## Platform Support

| Platform | Supported |
|----------|-----------|
| iOS | Yes |
| Android | Yes |
| WebGL | No |
| Standalone | No |

> Requires `UNITY_IOS` or `UNITY_ANDROID` scripting define symbols.

## Documentation & Resources

- [Game Frame X Documentation](https://gameframex.doc.alianblank.com)
- [CSJ (穿山甲) Developer Portal](https://www.csjplatform.com)

## Community & Support

- QQ Group: [Join](https://qm.qq.com/q/urCUAqJCJm)
- GitHub Issues: [Report a bug](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/issues)

## Changelog

### v1.0.0

- Initial release
- Rewarded video ad support for CSJ (穿山甲/Pangle) platform
- IL2CPP cropping protection

## License

This project is licensed under the [MIT License](LICENSE.md) and [Apache License 2.0](LICENSE.md).
