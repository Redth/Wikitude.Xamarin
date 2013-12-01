using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libWikitudeSDK.a", LinkTarget.ArmV7, IsCxx=true, SmartLink=true, LinkerFlags="-lc++ -lsqlite3.0 -lz -ObjC", Frameworks="AssetsLibrary CoreVideo Security SystemConfiguration CoreMedia AVFoundation CFNetwork CoreLocation CoreMotion MediaPlayer OpenGLES QuartzCore UIKit Foundation CoreGraphics")]
