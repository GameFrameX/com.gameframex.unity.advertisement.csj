<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Advertisement (CSJ / 穿山甲)

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.advertisement.csj)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.csj/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.advertisement.csj)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.csj/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
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

本パッケージは Game Frame X 広告コアの `BaseAdvertisementManager` の**アダプター実装**です。`CSJAdvertisementBootstrap` が `BeforeSceneLoad` フェーズでコアパッケージの `AdvertisementRegistry` に自己登録します（本パッケージが未インストールの場合はコードがストリッピングされ、登録は行われません。参照することが登録です）。チャネルの初期化は Bootstrap のファクトリクロージャーにより厳密に型指定された設定を注入して行われます。

| クラス | 説明 |
|--------|------|
| `CSJAdvertisementManager` | リワード動画広告マネージャー — ロード、表示、ライフサイクル管理 |
| `CSJAdvertisementConfig` | appId と adUnitId フィールドを含む広告設定。起動時に `CSJAdvertisementBootstrap.Config` 経由で注入 |
| `CSJAdvertisementBootstrap` | チャネル起動登録 — 起動時 `Config` 静的プロパティを持ち、`BeforeSceneLoad` で `AdvertisementRegistry` に自己登録 |
| `CSJBridge` | iOS/Android ネイティブプラットフォーム呼び出しブリッジ |
| `CSJNativeMessageHandler` | ネイティブメッセージコールバックハンドラー |
| `GameFrameXAdvertisementCSJCroppingHelper` | IL2CPP link.xml の代替 — 型参照を保持 |

## クイックスタート

### インストール

以下のいずれかの方法を選択してください：

1. Unity プロジェクトの `Packages/manifest.json` を編集し、`scopedRegistries` セクションを追加してください：
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

   `scopes` は、どのパッケージをこのレジストリから解決するかを制御します。`com.gameframex` で始まるパッケージのみがこのレジストリから取得されます。

2. `manifest.json` の `dependencies` に直接追加：
   ```json
   {
      "com.gameframex.unity.advertisement.csj": "https://github.com/gameframex/com.gameframex.unity.advertisement.csj.git"
   }
   ```
3. Unity の **Package Manager** で **Git URL** を使用して追加：`https://github.com/gameframex/com.gameframex.unity.advertisement.csj.git`
4. リポジトリを Unity プロジェクトの `Packages` ディレクトリにクローンしてください。自動的に読み込まれます。

### チャネル登録と設定

起動時（初回の広告呼び出し前）に `CSJAdvertisementBootstrap.Config` を設定します：

```csharp
using GameFrameX.Advertisement.CSJ.Runtime;

CSJAdvertisementBootstrap.Config = new CSJAdvertisementConfig
{
    appId = "あなたの穿山甲 AppId",
    adUnitId = "あなたの広告プレースメントID",
    isDebug = false,
};
```

- 本パッケージは `CSJAdvertisementBootstrap` 経由で `AdvertisementRegistry` に自己登録します。チャネル定数は `ChannelId = "csj"` です；
- TopOn チャネルと併用する場合は、`AdvertisementRegistry.ResolveManager("csj")` で明示的にルーティングし手動選択できます。
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

### v2.0.0

- チャネルは `CSJAdvertisementBootstrap` により `BeforeSceneLoad` フェーズでコアパッケージの `AdvertisementRegistry` に自己登録する方式に変更。設定は起動時に `CSJAdvertisementBootstrap.Config` 経由で注入
- **破壊的変更**：`AdvertisementComponent` の Inspector タイプドロップダウンとマクロ選択の広告プレースメントIDフィールドを削除。appId / adUnitId は `CSJAdvertisementBootstrap.Config` 経由で注入されます

### v1.0.0

- 初回リリース
- 穿山甲（CSJ/Pangle）プラットフォームのリワード動画広告対応
- IL2CPP ストリッピング保護


## 依存関係

| パッケージ | 説明 |
|----------|------|
| (无) | - |

## ライセンス

詳しくは [LICENSE.md](LICENSE.md) をご参照ください。
