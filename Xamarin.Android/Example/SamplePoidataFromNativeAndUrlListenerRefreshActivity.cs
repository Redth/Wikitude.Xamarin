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
using Wikitude.Architect;
using Android.Util;

namespace Com.Wikitude.Samples
{
	[Activity (Label = "SamplePoidataFromNativeAndUrlListenerRefreshActivity")]			
	public class SamplePoidataFromNativeAndUrlListenerRefreshActivity : SamplePoidataFromNativeActivity, ArchitectView.IArchitectUrlListener
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			architectView.RegisterUrlListener (this);
		}

		#region IArchitectUrlListener implementation
		public bool UrlWasInvoked (string uri)
		{
			try
			{
			var parsedUri = Android.Net.Uri.Parse (uri);

			if (parsedUri.Host.Equals ("button", StringComparison.InvariantCultureIgnoreCase)
				&& parsedUri.Query.Equals ("type=refresh", StringComparison.InvariantCultureIgnoreCase))
				LoadData ();
			}
			catch (Exception ex)
			{
				Log.Error (Constants.LOG_TAG, ex.ToString());
			}

			return true;
		}
		#endregion
	}
}

