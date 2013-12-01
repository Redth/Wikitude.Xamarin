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
	[Activity (Label = "SamplePoidataFromNativeAndUrlListenerActivity")]			
	public class SamplePoidataFromNativeAndUrlListenerActivity : SamplePoidataFromNativeActivity, IArchitectUrlListener
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			architectView.RegisterUrlListener (this);
			// Create your application here
		}

		#region IArchitectUrlListener implementation

		public bool UrlWasInvoked (string uriString)
		{
			var invokedUri = Android.Net.Uri.Parse (uriString);

			if ("markerselected".Equals(invokedUri.Host, StringComparison.InvariantCultureIgnoreCase) 
			    && invokedUri.GetQueryParameter("id") != null) 
			{
				var poiDataIndex = int.Parse(invokedUri.GetQueryParameter("id"));
				try 
				{
					var poiDetailIntent = new Intent(this, typeof(SamplePoiDetailActivity));

					var selPoiData = this.poiData[poiDataIndex - 1].ToString();

					poiDetailIntent.PutExtra(SamplePoiDetailActivity.EXTRAS_KEY_POIDATA, selPoiData);

					StartActivity(poiDetailIntent);
				} 
				catch (Exception ex) 
				{
					Log.Error (Constants.LOG_TAG, ex.ToString());
				}

			}
			return false;
		}

		#endregion
	}
}

