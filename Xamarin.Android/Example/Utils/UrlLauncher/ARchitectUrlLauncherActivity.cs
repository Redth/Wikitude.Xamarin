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
using System.IO;

namespace Com.Wikitude.Samples
{
	[Activity (Label = "ARchitectUrlLauncherActivity")]			
	public class ARchitectUrlLauncherActivity : Activity
	{
		const int MENU_ID_HISTORY_CLEAR = 1;
		const string TmpInformationFileName = "visitedUrl.tmp";

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.urllauncher_main);

			var url = FindViewById<AutoCompleteTextView> (Resource.Id.url);

			RefreshAutoCompletionUrls ();

			var buttonVisitUrl = FindViewById<Button> (Resource.Id.button_visit_url);

			buttonVisitUrl.Click += (sender, e) => {

				var urlString = url.Text.ToString();

				if (!urlString.StartsWith("http://"))
					urlString = "http://" + urlString;

				var visitedUrls = LoadVisitedUrls();

				if (!visitedUrls.Contains(urlString))
				{
					visitedUrls.Add(urlString);
				}

				SaveVisitedUrls(visitedUrls);
				LaunchArchitectCam(urlString);

			};
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			RefreshAutoCompletionUrls ();
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			SaveVisitedUrls (new List<string> ());
			RefreshAutoCompletionUrls ();

			return true;
		}

		public override bool OnPrepareOptionsMenu (IMenu menu)
		{
			menu.Clear ();
			var menuCounter = 0;
			var item = menu.Add (1, MENU_ID_HISTORY_CLEAR, menuCounter++, Resource.String.urllauncher_menu_clear_history);
			item.SetIcon(Android.Resource.Drawable.IcDelete);

			return true;
		}

		void RefreshAutoCompletionUrls()
		{
			var url = FindViewById<AutoCompleteTextView> (Resource.Id.url);

			var visitedUrls = LoadVisitedUrls ();

			if (visitedUrls != null)
				url.Adapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleDropDownItem1Line, visitedUrls);
		}

		void LaunchArchitectCam(string url)
		{
			var architectIntent = new Intent(this, typeof(BasicArchitectActivity));
			architectIntent.PutExtra (BasicArchitectActivity.EXTRAS_KEY_ACTIVITY_TITLE_STRING, "Launch URL");

			//var encodedUrl = Java.Net.URLEncoder.Encode (url, "UTF-8");
			architectIntent.PutExtra (BasicArchitectActivity.EXTRAS_KEY_ACTIVITY_ARCHITECT_WORLD_URL, url);

			StartActivity (architectIntent);
		}

		List<string> LoadVisitedUrls ()
		{
			try
			{
				var file = Path.Combine (CacheDir.AbsolutePath, TmpInformationFileName);

				return File.ReadAllLines (file).ToList ();
			}
			catch { return new List<string>(); }
		}

		void SaveVisitedUrls(List<string> urls) 
		{
			var file = Path.Combine (CacheDir.AbsolutePath, TmpInformationFileName);

			File.WriteAllLines (file, urls.ToArray ());
		}
	}
}

