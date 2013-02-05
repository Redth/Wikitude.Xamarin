using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libWikitudeSDK.a", LinkTarget.ArmV7, ForceLoad = true, IsCxx=true, LinkerFlags="-lstdc++ -lsqlite3.0", Frameworks="CoreVideo Security SystemConfiguration CoreMedia AVFoundation CFNetwork CoreLocation CoreMotion MediaPlayer OpenGLES QuartzCore UIKit Foundation CoreGraphics")]
