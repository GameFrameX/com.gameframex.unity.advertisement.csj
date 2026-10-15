## [2.0.1](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/compare/2.0.0...2.0.1) (2026-10-15)


### Bug Fixes

* **advertisement.csj:** 显式限定 DontDestroyOnLoad 调用 ([6b4af28](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/commit/6b4af287a32a1f3e036efac5a4ad9602cd83355b))

# [2.0.0](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/compare/1.0.3...2.0.0) (2026-10-15)


### doc

* **readme:** 更新 v2.0.0 渠道注册与配置说明 ([b124258](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/commit/b124258ad81457aaaa39cce2f9a3c65698cde5ec))


### Features

* **advertisement.csj:** 适配 GameFrameX 3.0.0 ([b500a9a](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/commit/b500a9a3d66a29fe9bb8248196e7a0e95a4383a6))
* **advertisement:** 适配新模块契约并新增 Bootstrap 自注册 ([442e8e5](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/commit/442e8e57d78cb5954525cd0f2f6b55b4bcf124f4))


### BREAKING CHANGES

* **readme:** 移除 AdvertisementComponent Inspector 类型下拉与宏选广告位ID字段，配置改经 CSJAdvertisementBootstrap.Config 启动期注入

# [2.0.0](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/compare/1.0.3...2.0.0) (2026-10-08)


### ⚠ BREAKING CHANGES

* **module:** 模块契约全接口化：删除 `GameFrameworkModule` 抽象基类，改为 `IGameFrameworkModule` 标记接口 + 可选能力接口

**新契约形态与迁移对照：**

| 旧基类成员 | 新契约 | 迁移方式 |
|---|---|---|
| `: GameFrameworkModule` | `IGameFrameworkModule` | 声明处直接替换（标记接口，可注册进 `GameFrameworkEntry`） |
| `override void Update(float, float)` | `IUpdatableModule` | 真实实现改 `public`；空实现删除（未实现者不再被轮询） |
| `override void Shutdown()` | `IShutdownableModule` | 真实实现改 `public`；空实现删除（未实现者不再被关闭） |
| `override int Priority` | `IPrioritizedModule` | 改 `public int Priority { get; }`；未实现视为 0 |

本包变更：`CSJAdvertisementManager` 实现 `IShutdownableModule`（`Shutdown` 调用 `CSJBridge.Destroy()`，原为对空实现基类的覆写）。

`GameFrameworkEntry.Update` 仅分派 `IUpdatableModule`；`Shutdown` 按优先级反序仅分派 `IShutdownableModule`；tick 与关闭顺序与旧实现完全一致。能力方法仅由宿主入口调用，业务侧继续通过 `GetModule<IXxxManager>()` 访问模块。

## [1.0.3](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/compare/1.0.2...1.0.3) (2026-07-29)


### Bug Fixes

* **package:** 补齐缺失的依赖声明 ([a561c52](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/commit/a561c52ec3c87adec9bb443ffcfca39ef25da290))

## [1.0.2](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/compare/1.0.1...1.0.2) (2026-06-22)


### Bug Fixes

* **license:** 补全 LICENSE.md 的 Unity meta 文件 ([8efce17](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/commit/8efce170eec69de0918e36b4ca34e20102907051))

## [1.0.1](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/compare/1.0.0...1.0.1) (2026-06-08)


### Bug Fixes

* 补全包规范文件（LICENSE/CHANGELOG/URL 字段/unity 字段） ([e152896](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/commit/e152896d67d4648c8b76f9c96d87a6c197acc4e4))

# 1.0.0 (2026-05-15)


### Features

* initial commit ([cb217a2](https://github.com/gameframex/com.gameframex.unity.advertisement.csj/commit/cb217a285752211063df104923b0bd7c60bb3f04))
