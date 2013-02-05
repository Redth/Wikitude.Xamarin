using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libQCAR2.a", LinkTarget.ArmV6 | LinkTarget.ArmV7, ForceLoad = true)]
