using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Com.Wikitude.Architect
{
	public class UrlInvokedListener : Java.Lang.Object, Com.Wikitude.Architect.IArchitectUrlListener
	{
		public UrlInvokedListener(Action<string> urlInvokedAction)
		{
			UrlInvokedAction = urlInvokedAction;
		}

		public Action<string> UrlInvokedAction { get; set; }

		public bool UrlWasInvoked(string p0)
		{
			if (UrlInvokedAction != null)
				UrlInvokedAction(p0);

			return true;
		}
	}
}