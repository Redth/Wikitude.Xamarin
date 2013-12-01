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

        [Export("initializeWithKey:motionManager:")]
        void Initialize(string key, [NullAllowed]CMMotionManager motionManager);

        [Static, Export("isDeviceSupportedForARMode:")]
        bool IsDeviceSupported(Wikitude.Architect.ARMode supportedMode);

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

        [Export("clearCache")]
        void ClearCache();

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
        [Export("urlWasInvoked:")]
        void UrlWasInvoked(string url);

		[Export ("architectView:didFailLoadWithError:"), EventArgs ("ArchitectViewLoadFailed")]
		void DidFailLoadWithError (ArchitectView architectView, NSError error);
    }
   
}

