<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Advertisement (CSJ / 穿山甲)

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.advertisement.csj)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.csj/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.advertisement.csj)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.csj/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams

<br />

[Documentation](https://gameframex.doc.alianblank.com) · [Quick Start](#quick-start) · QQ Group: 467608841 / 233840761

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

This package is an **adapter implementation** of `BaseAdvertisementManager` from the Game Frame X Advertisement core. It registers itself into the core package's `AdvertisementRegistry` via `CSJAdvertisementBootstrap` during the `BeforeSceneLoad` phase (if this package is not installed the code is stripped and no registration happens; referencing the package is the registration). Channel initialization is performed by the Bootstrap factory closure, which injects the strongly typed configuration.

| Class | Description |
|-------|-------------|
| `CSJAdvertisementManager` | Rewarded video ad manager — load, show, and lifecycle |
| `CSJAdvertisementConfig` | Configuration with appId and adUnitId fields, injected at startup via `CSJAdvertisementBootstrap.Config` |
| `CSJAdvertisementBootstrap` | Channel bootstrap registration — exposes a startup-time `Config` static property and self-registers into `AdvertisementRegistry` at `BeforeSceneLoad` |
| `CSJBridge` | Native bridge for iOS/Android platform calls |
| `CSJNativeMessageHandler` | Native message callback handler |
| `GameFrameXAdvertisementCSJCroppingHelper` | IL2CPP link.xml alternative — preserves type references |

## Quick Start

### Installation

Choose one of the following methods:

1. Edit your Unity project's `Packages/manifest.json` and add the `scopedRegistries` section:
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

   `scopes` controls which packages are resolved through this registry. Only packages whose names start with `com.gameframex` will be fetched from it.

2. Add to `manifest.json` dependencies:
   ```json
   {
      "com.gameframex.unity.advertisement.csj": "https://github.com/gameframex/com.gameframex.unity.advertisement.csj.git"
   }
   ```
3. Use **Package Manager** in Unity with **Git URL**: `https://github.com/gameframex/com.gameframex.unity.advertisement.csj.git`
4. Clone the repository into your Unity project's `Packages` directory. It will be loaded automatically.

### Channel Registration & Configuration

Assign `CSJAdvertisementBootstrap.Config` at startup (before the first ad call):

```csharp
using GameFrameX.Advertisement.CSJ.Runtime;

CSJAdvertisementBootstrap.Config = new CSJAdvertisementConfig
{
    appId = "your CSJ AppId",
    adUnitId = "your ad unit id",
    isDebug = false,
};
```

- This package self-registers into `AdvertisementRegistry` via `CSJAdvertisementBootstrap`; the channel constant is `ChannelId = "csj"`;
- When installed alongside the TopOn channel, use `AdvertisementRegistry.ResolveManager("csj")` for explicit routing / manual selection.
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

### v2.0.0

- The channel now self-registers into the core package's `AdvertisementRegistry` via `CSJAdvertisementBootstrap` at `BeforeSceneLoad`; configuration is injected at startup via `CSJAdvertisementBootstrap.Config`
- **Breaking change**: the `AdvertisementComponent` Inspector type dropdown and macro-selected ad unit ID fields have been removed; appId / adUnitId are now injected via `CSJAdvertisementBootstrap.Config`

### v1.0.0

- Initial release
- Rewarded video ad support for CSJ (穿山甲/Pangle) platform
- IL2CPP cropping protection


## Dependencies

| Package | Description |
|---------|-------------|
| (无) | - |

## License

See [LICENSE.md](LICENSE.md) for license information.
