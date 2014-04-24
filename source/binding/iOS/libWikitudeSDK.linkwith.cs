using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libWikitudeSDK.a", LinkTarget.ArmV6 | LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Arm64 | LinkTarget.Simulator | LinkTarget.Simulator64, IsCxx=true, LinkerFlags="-lc++ -lz -ObjC", Frameworks="AssetsLibrary AVFoundation CFNetwork CoreGraphics CoreLocation CoreMedia CoreMotion CoreText CoreVideo Foundation MediaPlayer OpenGLES QuartzCore Security UIKit")]

//-lsqlite3.0 - this doesn't seem to be required in 3.3+