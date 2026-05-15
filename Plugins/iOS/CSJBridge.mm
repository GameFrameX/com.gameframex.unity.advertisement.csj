#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <BUAdSDK/BUAdSDK.h>

static BUNativeExpressRewardedVideoAd *_rewardedVideoAd = nil;

@interface CSJBridgeDelegate : NSObject <BUNativeExpressRewardedVideoAdDelegate>
@end

@implementation CSJBridgeDelegate

- (void)nativeExpressRewardedVideoAdDidLoad:(BUNativeExpressRewardedVideoAd *)rewardedVideoAd {
    NSLog(@"[CSJ] Ad material loaded");
}

- (void)nativeExpressRewardedVideoAdDidDownLoadVideo:(BUNativeExpressRewardedVideoAd *)rewardedVideoAd {
    NSLog(@"[CSJ] Video download complete");
    UnitySendMessage("CSJBridgeReceiver", "OnNativeMessage", "LoadSuccess");
}

- (void)nativeExpressRewardedVideoAd:(BUNativeExpressRewardedVideoAd *)rewardedVideoAd didFailWithError:(NSError *)error {
    NSLog(@"[CSJ] Ad load failed: %@", error.localizedDescription);
    NSString *msg = [NSString stringWithFormat:@"LoadFail|%@", error.localizedDescription ?: @""];
    UnitySendMessage("CSJBridgeReceiver", "OnNativeMessage", [msg UTF8String]);
    _rewardedVideoAd = nil;
}

- (void)nativeExpressRewardedVideoAdViewRenderSuccess:(BUNativeExpressRewardedVideoAd *)rewardedVideoAd {
    NSLog(@"[CSJ] Ad render success");
}

- (void)nativeExpressRewardedVideoAdViewRenderFail:(BUNativeExpressRewardedVideoAd *)rewardedVideoAd error:(NSError *)error {
    NSLog(@"[CSJ] Ad render fail: %@", error.localizedDescription);
    NSString *msg = [NSString stringWithFormat:@"ShowFail|%@", error.localizedDescription ?: @""];
    UnitySendMessage("CSJBridgeReceiver", "OnNativeMessage", [msg UTF8String]);
    _rewardedVideoAd = nil;
}

- (void)nativeExpressRewardedVideoAdDidVisible:(BUNativeExpressRewardedVideoAd *)rewardedVideoAd {
    NSLog(@"[CSJ] Ad visible");
}

- (void)nativeExpressRewardedVideoAdDidClose:(BUNativeExpressRewardedVideoAd *)rewardedVideoAd {
    NSLog(@"[CSJ] Ad closed");
    _rewardedVideoAd = nil;
}

- (void)nativeExpressRewardedVideoAdDidClick:(BUNativeExpressRewardedVideoAd *)rewardedVideoAd {
    NSLog(@"[CSJ] Ad clicked");
}

- (void)nativeExpressRewardedVideoAdDidPlayFinish:(BUNativeExpressRewardedVideoAd *)rewardedVideoAd didFailWithError:(NSError *)error {
    NSLog(@"[CSJ] Ad play finished");
}

- (void)nativeExpressRewardedVideoAdServerRewardDidSucceed:(BUNativeExpressRewardedVideoAd *)rewardedVideoAd verify:(BOOL)verify {
    NSLog(@"[CSJ] Server reward verify succeed, verify: %d", verify);
    UnitySendMessage("CSJBridgeReceiver", "OnNativeMessage", verify ? "RewardVerify|true" : "RewardVerify|false");
}

- (void)nativeExpressRewardedVideoAdServerRewardDidFail:(BUNativeExpressRewardedVideoAd *)rewardedVideoAd error:(NSError *)error {
    NSLog(@"[CSJ] Server reward verify failed: %@", error.localizedDescription);
    UnitySendMessage("CSJBridgeReceiver", "OnNativeMessage", "RewardVerify|false");
}

@end

static CSJBridgeDelegate *_delegate = nil;

extern UIViewController *UnityGetGLViewController();

extern "C" {

void _CSJInit(const char *appId, bool debug) {
    NSLog(@"[CSJ] Init with appId: %s, debug: %d", appId, debug);

    if (!_delegate) {
        _delegate = [[CSJBridgeDelegate alloc] init];
    }
    if (!appId) {
        NSLog(@"[CSJ] Error: appId is NULL");
        return;
    }
    NSString * appIdString = [NSString stringWithUTF8String:appId];
    BUAdSDKConfiguration *config = [BUAdSDKConfiguration configuration];
    NSLog(@"[CSJ] appIdString: %@", appIdString);
    config.appID = appIdString;
    config.SDKDEBUG = debug;
    config.debugLog = debug ? @(1) : @(0);
    config.useMediation = NO;
    @try {
        [BUAdSDKManager startWithAsyncCompletionHandler:^(BOOL success, NSError *error) {
            if (success) {
                NSLog(@"[CSJ] SDK init success");
            } else {
                NSLog(@"[CSJ] SDK init failed: %@", error.localizedDescription);
            }
        }];
    } @catch (NSException *exception) {
        NSLog(@"[CSJ] SDK init exception: %@", exception);
    }
}

void _CSJLoadAd(const char *slotId, const char *userId, const char *customData) {
    NSLog(@"[CSJ] Load ad with slotId: %s, userId: %s, customData: %s", slotId, userId ?: "(null)", customData ?: "(null)");

    if (_rewardedVideoAd) {
        NSLog(@"[CSJ] Ad already loaded, reusing");
        UnitySendMessage("CSJBridgeReceiver", "OnNativeMessage", "LoadSuccess");
        return;
    }

    NSString *slotIdStr = [NSString stringWithUTF8String:slotId];
    BURewardedVideoModel *model = [[BURewardedVideoModel alloc] init];

    if (userId && strlen(userId) > 0) {
        model.userId = [NSString stringWithUTF8String:userId];
    }

    if (customData && strlen(customData) > 0) {
        model.extra = [NSString stringWithUTF8String:customData];
    }

    _rewardedVideoAd = [[BUNativeExpressRewardedVideoAd alloc] initWithSlotID:slotIdStr rewardedVideoModel:model];
    _rewardedVideoAd.delegate = _delegate;
    [_rewardedVideoAd loadAdData];
}

bool _CSJIsAdReady() {
    return _rewardedVideoAd != nil;
}

void _CSJShowAd() {
    NSLog(@"[CSJ] Show ad");

    if (!_rewardedVideoAd) {
        NSLog(@"[CSJ] No ad ready to show");
        UnitySendMessage("CSJBridgeReceiver", "OnNativeMessage", "ShowFail|广告未加载完成");
        UnitySendMessage("CSJBridgeReceiver", "OnNativeMessage", "RewardVerify|false");
        return;
    }

    [_rewardedVideoAd showAdFromRootViewController:UnityGetGLViewController()];
}

void _CSJDestroy() {
    NSLog(@"[CSJ] Destroy");
    _rewardedVideoAd = nil;
}

}
