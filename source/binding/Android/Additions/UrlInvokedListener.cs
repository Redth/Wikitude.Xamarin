//using System;
//
//namespace Wikitude.Architect
//{
//	public class UrlInvokedListener : Java.Lang.Object, IArchitectUrlListener
//	{
//		public UrlInvokedListener(Action<string> urlInvokedAction)
//		{
//			UrlInvokedAction = urlInvokedAction;
//		}
//
//		public Action<string> UrlInvokedAction { get; set; }
//
//		public bool UrlWasInvoked(string p0)
//		{
//			if (UrlInvokedAction != null)
//				UrlInvokedAction(p0);
//
//			return true;
//		}
//	}
//}