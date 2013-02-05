using System;
using System.Drawing;

using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreMotion;

namespace Wikitude.Architect
{
    // The first step to creating a binding is to add your native library ("libNativeLibrary.a")
    // to the project by right-clicking (or Control-clicking) the folder containing this source
    // file and clicking "Add files..." and then simply select the native library (or libraries)
    // that you want to bind.
    //
    // When you do that, you'll notice that MonoDevelop generates a code-behind file for each
    // native library which will contain a [LinkWith] attribute. MonoDevelop auto-detects the
    // architectures that the native library supports and fills in that information for you,
    // however, it cannot auto-detect any Frameworks or other system libraries that the
    // native library may depend on, so you'll need to fill in that information yourself.
    //
    // Once you've done that, you're ready to move on to binding the API...
    //
    //
    // Here is where you'd define your API definition for the native Objective-C library.
    //
    // For example, to bind the following Objective-C class:
    //
    //     @interface Widget : NSObject {
    //     }
    //
    // The C# binding would look like this:
    //
    //     [BaseType (typeof (NSObject))]
    //     interface Widget {
    //     }
    //
    // To bind Objective-C properties, such as:
    //
    //     @property (nonatomic, readwrite, assign) CGPoint center;
    //
    // You would add a property definition in the C# interface like so:
    //
    //     [Export ("center")]
    //     PointF Center { get; set; }
    //
    // To bind an Objective-C method, such as:
    //
    //     -(void) doSomething:(NSObject *)object atIndex:(NSInteger)index;
    //
    // You would add a method definition to the C# interface like so:
    //
    //     [Export ("doSomething:atIndex:")]
    //     void DoSomething (NSObject object, int index);
    //
    // Objective-C "constructors" such as:
    //
    //     -(id)initWithElmo:(ElmoMuppet *)elmo;
    //
    // Can be bound as:
    //
    //     [Export ("initWithElmo:")]
    //     IntPtr Constructor (ElmoMuppet elmo);
    //
    // For more information, see http://docs.xamarin.com/ios/advanced_topics/binding_objective-c_types
    //

    [BaseType (typeof(UIView), Name="WTArchitectView", Delegates=new string[] { "WeakDelegate" }, Events=new Type[] { typeof(ArchitectViewDelegate) })]
    interface ArchitectView
    {
        [Export ("initWithFrame:")]
        IntPtr Constructor (RectangleF frame);

        [Export("initializeWithKey:motionManager:")]
        void Initialize(string key, [NullAllowed]CMMotionManager motionManager);

        [Static, Export("isDeviceSupported")]
        bool IsDeviceSupported();

        [Export("loadArchitectWorldFromUrl:")]
        void LoadArchitectWorldFromUrl(string architectWorldUrl);

        [Export("callJavaScript:")]
        void CallJavaScript(string javaScript);

        [Export("injectLocationWithLatitude:longitude:altitude:accuracy:")]
        void InjectLocationWithLatitude(float latitude, float longitude, float altitude, float accuracy);

        [Export("setUseInjectedLocation:")]
        void SetUseInjectedLocation(bool useInjectedLocation);

        [Export("isUsingInjectedLocation")]
        bool IsUsingInjectedLocation();
    
        [Export("setCullingDistance:")]
        void SetCullingDistance(float cullingDistance);

        [Export("cullingDistance")]
        float CullingDistance();

        [Export("versionNumber")]
        string VersionNumber();

        [Export("clearCache")]
        void ClearCache();

        [Export("setShouldRotate:toInterfaceOrientation:")]
        void SetShouldRotate(bool shouldAutoRotate, UIInterfaceOrientation interfaceOrientation);

        [Export("start")]
        void Start();

        [Export("stop")]
        void Stop();

        [Export("motionManager")]
        CMMotionManager MotionManager();

        [Export("delegate"), NullAllowed]
        NSObject WeakDelegate { get; set; }

        [Wrap("WeakDelegate")]
        ArchitectViewDelegate Delegate { get;set; }
    }

    [BaseType(typeof(NSObject), Name="WTArchitectViewDelegate")]
    [Model]
    interface ArchitectViewDelegate
    {
        [Export("urlWasInvoked:")]
        void UrlWasInvoked(string url);
    }
   
}

