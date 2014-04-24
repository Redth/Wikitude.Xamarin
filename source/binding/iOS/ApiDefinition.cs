using System;
using System.Drawing;

using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreMotion;

namespace Wikitude.Architect
{
	[BaseType (typeof(UIView), Name="WTArchitectView", Delegates=new string[] { "WeakDelegate" }, Events=new Type[] { typeof(ArchitectViewDelegate) })]
    interface ArchitectView
    {
        [Export ("initWithFrame:")]
        IntPtr Constructor (RectangleF frame);

		[Export ("initWithFrame:motionManager:augmentedRealityMode:")]
		IntPtr Constructor (RectangleF frame, CMMotionManager motionManager, AugmentedRealityMode augmentedRealityMode);

		[Export ("setLicenseKey:")]
		void SetLicenseKey(string licenseKey);

		//[Deprecated("Use the constructor ctor(string key, CMMotionManager motionManager, AugmentedRealityMode augmentedRealityMode) instead!")]
		//[Export("initializeWithKey:motionManager:")]
		//void Initialize(string key, [NullAllowed]CMMotionManager motionManager);

		[Static, Export("isDeviceSupportedForAugmentedRealityMode:")]
		bool IsDeviceSupported(Wikitude.Architect.AugmentedRealityMode supportedMode);

        [Export("loadArchitectWorldFromUrl:")]
        void LoadArchitectWorld(NSUrl architectWorldUrl);

        [Export("callJavaScript:")]
        void CallJavaScript(string javaScript);

		[Export("injectLocationWithLatitude:longitude:altitude:accuracy:")]
        void InjectLocation(float latitude, float longitude, float altitude, float accuracy);

		[Export("injectLocationWithLatitude:longitude:accuracy:")]
		void InjectLocation(float latitude, float longitude, float accuracy);

        [Export("setUseInjectedLocation:")]
        void SetUseInjectedLocation(bool useInjectedLocation);

        [Export("isUsingInjectedLocation")]
		bool IsUsingInjectedLocation { get; }
    
        [Export("cullingDistance")]
		float CullingDistance { get;set; }

       	[Export("versionNumber")]
		string GetVersionNumber();

		[Static]
		[Export("versionNumber")]
		string VersionNumber { get; }

        [Export("clearCache")]
        void ClearCache();

		//[Export("captureScreenWithMode:usingSaveMode:saveOptions:context:")]
		//void CaptureScreen(WTScreenshotSaveMode saveMode, WTScreenshotSaveOptions options, NSDictionary context);

        [Export("setShouldRotate:toInterfaceOrientation:")]
        void SetShouldRotate(bool shouldAutoRotate, UIInterfaceOrientation interfaceOrientation);

        [Export("start")]
        void Start();

        [Export("stop")]
        void Stop();

		[Export ("isRunning")]
		bool IsRunning { get; }

        [Export("motionManager")]
        CMMotionManager MotionManager();

        [Export("delegate"), NullAllowed]
        NSObject WeakDelegate { get; set; }

        [Wrap("WeakDelegate")]
        ArchitectViewDelegate Delegate { get;set; }

		[Export ("shouldWebViewRotate")]
		bool ShouldWebViewRotate { get; set; }

		[Export ("isRotatingToInterfaceOrientation")]
		bool IsRotatingToInterfaceOrientation { get; }
    }

    [BaseType(typeof(NSObject), Name="WTArchitectViewDelegate")]
    [Model]
	[Protocol]
    interface ArchitectViewDelegate
    {
		[Export("architectView:invokedURL:"), EventArgs ("ArchitectViewInvokedURL")]
		void InvokedUrl(ArchitectView architectView, NSUrl url);

		[Export ("architectView:didFailLoadWithError:"), EventArgs ("ArchitectViewLoadFailed")]
		void DidFailLoadWithError (ArchitectView architectView, NSError error);

		[Export ("architectView:didCaptureScreenWithContext:"), EventArgs("ArchitectViewCaptureScreen")]
		void DidCaptureScreen (ArchitectView architectView, NSDictionary context);

		[Export ("architectView:didFailCaptureScreenWithError:"), EventArgs("ArchitectViewFailCaptureScreen")]
		void DidFailCaptureScreen (ArchitectView architectView, NSError error);

    }
   
}




//-------------------------------------------------------------------
//Old version
//-------------------------------------------------------------------

//using System;
//using System.Drawing;
//
//using MonoTouch.ObjCRuntime;
//using MonoTouch.Foundation;
//using MonoTouch.UIKit;
//using MonoTouch.CoreMotion;
//
//namespace Wikitude.Architect
//{
//	[BaseType (typeof(UIView), Name="WTArchitectView", Delegates=new string[] { "WeakDelegate" }, Events=new Type[] { typeof(ArchitectViewDelegate) })]
//	interface ArchitectView
//	{
//		[Export ("initWithFrame:")]
//		IntPtr Constructor (RectangleF frame);
//
//		[Export("initializeWithKey:motionManager:")]
//		void Initialize(string key, [NullAllowed]CMMotionManager motionManager);
//
//		[Static, Export("isDeviceSupported")]
//		bool IsDeviceSupported();
//
//		[Export("loadArchitectWorldFromUrl:")]
//		void LoadArchitectWorldFromUrl(string architectWorldUrl);
//
//		[Export("callJavaScript:")]
//		void CallJavaScript(string javaScript);
//
//		[Export("injectLocationWithLatitude:longitude:altitude:accuracy:")]
//		void InjectLocationWithLatitude(float latitude, float longitude, float altitude, float accuracy);
//
//		[Export("setUseInjectedLocation:")]
//		void SetUseInjectedLocation(bool useInjectedLocation);
//
//		[Export("isUsingInjectedLocation")]
//		bool IsUsingInjectedLocation();
//
//		[Export("setCullingDistance:")]
//		void SetCullingDistance(float cullingDistance);
//
//		[Export("cullingDistance")]
//		float CullingDistance();
//
//		[Export("versionNumber")]
//		string VersionNumber();
//
//		[Export("clearCache")]
//		void ClearCache();
//
//		[Export("setShouldRotate:toInterfaceOrientation:")]
//		void SetShouldRotate(bool shouldAutoRotate, UIInterfaceOrientation interfaceOrientation);
//
//		[Export("start")]
//		void Start();
//
//		[Export("stop")]
//		void Stop();
//
//		[Export("motionManager")]
//		CMMotionManager MotionManager();
//
//		[Export("delegate"), NullAllowed]
//		NSObject WeakDelegate { get; set; }
//
//		[Wrap("WeakDelegate")]
//		ArchitectViewDelegate Delegate { get;set; }
//	}
//
//	[BaseType(typeof(NSObject), Name="WTArchitectViewDelegate")]
//	[Model]
//	interface ArchitectViewDelegate
//	{
//		[Export("urlWasInvoked:")]
//		void UrlWasInvoked(string url);
//	}
//
//}