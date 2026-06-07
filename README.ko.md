<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Advertisement (CSJ / 穿山甲)

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.advertisement.csj)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.csj/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.advertisement.csj)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.csj/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현

<br />

[문서](https://gameframex.doc.alianblank.com) · [빠른 시작](#quick-start) · QQ 그룹: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**

</div>

## 프로젝트 개요

[Game Frame X 광고 시스템](https://github.com/GameFrameX/com.gameframex.unity.advertisement)의穿山甲（CSJ/Pangle）플랫폼 어댑터. ByteDance의穿山甲광고 SDK를 사용하여 iOS/Android 앱을 위한 리워드 동영상 광고 통합을 제공합니다.

### 기능

- 穿山甲（CSJ/Pangle）SDK 기반 리워드 동영상 광고 지원
- 네이티브 iOS/Android 플랫폼 지원
- IL2CPP 코드 스트리핑 보호
- 조건부 컴파일 (`UNITY_IOS`, `UNITY_ANDROID`)
- Game Frame X 광고 컴포넌트와 원활한 통합

## 아키텍처

이 패키지는 Game Frame X 광고 코어의 `BaseAdvertisementManager` **어댑터 구현**입니다. Unity Inspector에서 `AdvertisementComponent`를 설정하면 자동으로 검색 및 로드됩니다.

| 클래스 | 설명 |
|--------|------|
| `CSJAdvertisementManager` | 리워드 동영상 광고 관리자 — 로드, 표시 및 수명 주기 관리 |
| `CSJAdvertisementConfig` | appId 및 adUnitId 필드가 포함된 광고 설정 |
| `CSJBridge` | iOS/Android 네이티브 플랫폼 호출 브릿지 |
| `CSJNativeMessageHandler` | 네이티브 메시지 콜백 핸들러 |
| `GameFrameXAdvertisementCSJCroppingHelper` | IL2CPP link.xml 대체 — 타입 참조 유지 |

## 빠른 시작

### 설치

Unity 프로젝트의 `Packages/manifest.json`을 편집하여 `scopedRegistries` 섹션을 추가하세요:

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

`scopes`는 이 레지스트리를 통해 어떤 패키지를 해석할지 제어합니다. `com.gameframex`로 시작하는 패키지만 이 레지스트리에서 가져옵니다.

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity.advertisement.csj": "1.0.0"
  }
}
```

## 플랫폼 지원

| 플랫폼 | 지원 |
|--------|------|
| iOS | 예 |
| Android | 예 |
| WebGL | 아니요 |
| Standalone | 아니요 |

> `UNITY_IOS` 또는 `UNITY_ANDROID` 스크립트 정의 기호가 필요합니다.

## 문서 및 자료

- [Game Frame X 문서](https://gameframex.doc.alianblank.com)
- [穿山甲개발자 포털](https://www.csjplatform.com)

## 커뮤니티 및 지원

- QQ 그룹: [가입](https://qm.qq.com/q/urCUAqJCJm)
- GitHub Issues: [버그 보고](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/issues)

## 변경 로그

### v1.0.0

- 초기 릴리스
- 穿山甲（CSJ/Pangle）플랫폼 리워드 동영상 광고 지원
- IL2CPP 스트리핑 보호


## 의존성

| 패키지 | 설명 |
|--------|------|
| (无) | - |

## 라이선스

자세한 내용은 [LICENSE.md](LICENSE.md) 파일을 참조하세요.
