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
using Java.IO;

namespace Com.Wikitude.Samples
{
	[Activity (Label = "SamplesListActivity")]			
	public class SamplesListActivity : ListActivity
	{
		public const string EXTRAS_KEY_ACTIVITY_TITLE_STRING = "activityTitle";
		public const string EXTRAS_KEY_ACTIVITY_ARCHITECT_WORLD_URL = "activityArchitectWorldUrl";

		public const string EXTRAS_KEY_ACTIVITIES_ARCHITECT_WORLD_URLS_ARRAY = "activitiesArchitectWorldUrls";
		public const string EXTRAS_KEY_ACTIVITIES_TILES_ARRAY = "activitiesTitles";
		public const string EXTRAS_KEY_ACTIVITIES_CLASSNAMES_ARRAY = "activitiesClassnames";

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.list_sample);

			this.Title = Intent.Extras.GetString (EXTRAS_KEY_ACTIVITY_TITLE_STRING);

			var values = Intent.Extras.GetStringArray (EXTRAS_KEY_ACTIVITIES_TILES_ARRAY);

			ListAdapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleListItem1, Android.Resource.Id.Text1, values);
		}

		protected override void OnListItemClick (ListView l, View v, int position, long id)
		{
			base.OnListItemClick (l, v, position, id);

			var classNames = Intent.Extras.GetStringArray (EXTRAS_KEY_ACTIVITIES_CLASSNAMES_ARRAY);

			var className = classNames[position];

			try
			{
				var intent = new Intent(this, Java.Lang.Class.ForName(className));

				intent.PutExtra(EXTRAS_KEY_ACTIVITY_TITLE_STRING, Intent.Extras.GetStringArray(EXTRAS_KEY_ACTIVITIES_TILES_ARRAY)[position]);
				intent.PutExtra(EXTRAS_KEY_ACTIVITY_ARCHITECT_WORLD_URL, "samples" + File.Separator + 
				                Intent.Extras.GetStringArray(EXTRAS_KEY_ACTIVITIES_ARCHITECT_WORLD_URLS_ARRAY)[position]
				                + File.Separator + "index.html");

				StartActivity(intent);
			}
			catch (Exception)
			{
				Toast.MakeText (this, className + "\nnot defined/accessible", ToastLength.Short).Show ();
			}
		}
	}
}

