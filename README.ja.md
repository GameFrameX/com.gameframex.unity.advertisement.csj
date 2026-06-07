<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Advertisement (CSJ / 穿山甲)

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.advertisement.csj)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.csj/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.advertisement.csj)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.csj/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援

<br />

[ドキュメント](https://gameframex.doc.alianblank.com) · [クイックスタート](#quick-start) · QQグループ: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **日本語** | [한국어](README.ko.md)

</div>
## プロジェクト概要

[Game Frame X 広告システム](https://github.com/GameFrameX/com.gameframex.unity.advertisement)の穿山甲（CSJ/Pangle）プラットフォームアダプター。ByteDance の穿山甲広告 SDK を使用し、iOS/Android アプリ向けにリワード動画広告の統合を提供します。

### 機能

- 穿山甲（CSJ/Pangle）SDK によるリワード動画広告サポート
- ネイティブ iOS/Android プラットフォームサポート
- IL2CPP コードストリッピング保護
- 条件付きコンパイル（`UNITY_IOS`、`UNITY_ANDROID`）
- Game Frame X 広告コンポーネントとのシームレスな統合

## アーキテクチャ

本パッケージは Game Frame X 広告コアの `BaseAdvertisementManager` の**アダプター実装**です。Unity Inspector で `AdvertisementComponent` を設定することで自動的に検出・読み込みされます。

| クラス | 説明 |
|--------|------|
| `CSJAdvertisementManager` | リワード動画広告マネージャー — ロード、表示、ライフサイクル管理 |
| `CSJAdvertisementConfig` | appId と adUnitId フィールドを含む広告設定 |
| `CSJBridge` | iOS/Android ネイティブプラットフォーム呼び出しブリッジ |
| `CSJNativeMessageHandler` | ネイティブメッセージコールバックハンドラー |
| `GameFrameXAdvertisementCSJCroppingHelper` | IL2CPP link.xml の代替 — 型参照を保持 |

## クイックスタート

### インストール

1. [広告コアパッケージ](https://github.com/GameFrameX/com.gameframex.unity.advertisement)をインストール
2. 本アダプターを Unity Package Manager (UPM) で追加：

```json
{
  "dependencies": {
    "com.gameframex.unity.advertisement": "https://github.com/GameFrameX/com.gameframex.unity.advertisement.git",
    "com.gameframex.unity.advertisement.csj": "https://github.com/gameframex/com.gameframex.unity.advertisement.csj.git"
  }
}
```

または Unity Package Manager ウィンドウで git URL から追加。

### 使用例

Unity Inspector で設定：GameObject に `AdvertisementComponent` を追加し、実装タイプのドロップダウンから `CSJAdvertisementManager` を選択します。

```csharp
using GameFrameX.Advertisement.Runtime;

// 標準: GameEntry 経由（com.gameframex.unity.entry 非依存）
var adComponent = GameEntry.GetComponent<AdvertisementComponent>();

// サーバーサイド検証データを設定（オプション）
adComponent.SetExtraData("userId", player.UserId);

// リワード動画広告を再生
var option = new AdvertisementPlayOption
{
    OnSuccess    = (data) => Debug.Log("広告の表示に成功しました"),
    OnFail       = (err) => Debug.LogError($"広告の表示に失敗しました: {err}"),
    OnShowResult = (watched) =>
    {
        if (watched)
        {
            // ユーザーに報酬を付与
        }
    },
};
adComponent.Play(option);

// ショートカット: GameApp 経由（com.gameframex.unity.entry が必要）
GameApp.Advertisement.SetExtraData("userId", player.UserId);
var option2 = new AdvertisementPlayOption
{
    OnSuccess    = (data) => Debug.Log("広告の表示に成功しました"),
    OnFail       = (err) => Debug.LogError($"広告の表示に失敗しました: {err}"),
    OnShowResult = (watched) =>
    {
        if (watched)
        {
            // ユーザーに報酬を付与
        }
    },
};
GameApp.Advertisement.Play(option2);
```

## プラットフォーム対応

| プラットフォーム | 対応 |
|------------------|------|
| iOS | はい |
| Android | はい |
| WebGL | いいえ |
| Standalone | いいえ |

> `UNITY_IOS` または `UNITY_ANDROID` スクリプト定義シンボルが必要です。

## ドキュメントとリソース

- [Game Frame X ドキュメント](https://gameframex.doc.alianblank.com)
- [穿山甲開発者ポータル](https://www.csjplatform.com)

## コミュニティとサポート

- QQグループ：[参加](https://qm.qq.com/q/urCUAqJCJm)
- GitHub Issues：[バグ報告](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/issues)

## 変更履歴

### v1.0.0

- 初回リリース
- 穿山甲（CSJ/Pangle）プラットフォームのリワード動画広告対応
- IL2CPP ストリッピング保護

## ライセンス

本プロジェクトは [MIT ライセンス](LICENSE.md) および [Apache ライセンス 2.0](LICENSE.md) のデュアルライセンスです。
