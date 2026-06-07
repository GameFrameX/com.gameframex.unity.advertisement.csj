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

Edit your Unity project's `Packages/manifest.json` and add the `scopedRegistries` section:

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
  ]
}
```

`scopes` controls which packages are resolved through this registry. Only packages whose names start with `com.gameframex` will be fetched from it.

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity.advertisement.csj": "1.0.0"
  }
}
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


## Dependencies

| Package | Description |
|---------|-------------|
| (无) | - |

## License

See [LICENSE.md](LICENSE.md) for license information.
