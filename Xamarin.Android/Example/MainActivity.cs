using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Util;
using Java.IO;
using Wikitude.Architect;

namespace Com.Wikitude.Samples
{
	[Activity (Label = "Wikitude Samples", MainLauncher = true)]
	public class MainActivity : ListActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.list_startscreen);

			deleteDirectoryContent (ArchitectView.GetCacheDirectoryAbsoluteFilePath (this));

			var values = getListLabels ();

			this.ListAdapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleListItem1, Android.Resource.Id.Text1, values);

			var buttonLaunchUrl = FindViewById<Button> (Resource.Id.buttonLaunchUrl);

			buttonLaunchUrl.Click += (sender, e) => StartActivity(typeof(ARchitectUrlLauncherActivity));
		}

		protected override void OnListItemClick (ListView l, View v, int position, long id)
		{
			base.OnListItemClick (l, v, position, id);

			var intent = new Intent(this, typeof(SamplesListActivity));

			var activitiesToLaunch = getActivitiesToLaunch()[position];
			var activityTitle = activitiesToLaunch[0].CategoryId + ". " + activitiesToLaunch[0].CategoryName.Replace("$", " ");
			var activityTitles = new String[activitiesToLaunch.Count];
			var activityUrls = new String[activitiesToLaunch.Count];
			var activityClasses = new String[activitiesToLaunch.Count];

			for (int i= 0; i < activitiesToLaunch.Count; i++) 
			{
				var meta = activitiesToLaunch[i];
				activityTitles[i] = (meta.CategoryId + "." + meta.SampleId + " " + meta.SampleName.Replace("$", " "));
				activityUrls[i] =  (meta.Path);

				switch (meta.CategoryId) 
				{
					case 4: 
						if (meta.SampleId==3) 
							activityClasses[i] = ("com.wikitude.samples.SamplePoidataFromNativeActivity");
						else
							activityClasses[i] = ("com.wikitude.samples.BasicArchitectActivity");
						break;

					case 5:
						switch (meta.SampleId) 
						{
							case 1:
								activityClasses[i] = ("com.wikitude.samples.SamplePoidataFromNativeAndUrlListenerActivity");
								break;
							case 4:
								activityClasses[i] = ("com.wikitude.samples.SamplePoidataFromNativeAndUrlListenerRefreshActivity");
								break;
							default:
								activityClasses[i] = ("com.wikitude.samples.SamplePoidataFromNativeActivity");
								break;	
						}
						break;

					default:
						activityClasses[i] = ("com.wikitude.samples.BasicArchitectActivity");
						break;
				}

			}

			intent.PutExtra(SamplesListActivity.EXTRAS_KEY_ACTIVITIES_ARCHITECT_WORLD_URLS_ARRAY, activityUrls);
			intent.PutExtra(SamplesListActivity.EXTRAS_KEY_ACTIVITIES_CLASSNAMES_ARRAY, activityClasses);
			intent.PutExtra(SamplesListActivity.EXTRAS_KEY_ACTIVITIES_TILES_ARRAY, activityTitles);
			intent.PutExtra(SamplesListActivity.EXTRAS_KEY_ACTIVITY_TITLE_STRING, activityTitle);

			/* launch activity */
			this.StartActivity(intent);
		}

		private static void deleteDirectoryContent(string path) 
		{
			try 
			{
				var dir = new File (path);
				if (dir.Exists() && dir.IsDirectory) 
				{
					var children = dir.List();
					for (int i = 0; i < children.Length; i++) {
						new File(dir, children[i]).Delete();
					}
				}
			} 
			catch (Exception ex) 
			{
				Log.Error ("WIKITUDE_EXAMPLE", ex.ToString());
			}
		}

		protected string[] getListLabels() 
		{
			var samples = getActivitiesToLaunch();
			var labels = new string[samples.Keys.Count];

			for (int i = 0; i < labels.Length; i++)
				labels[i] = samples[i][0].CategoryId + ". " + samples[i][0].CategoryName.Replace("$", " ");

			return labels;
		}


		private Dictionary<int, List<SampleMeta>> getActivitiesToLaunch()
		{
			var pos2activites = new Dictionary<int, List<SampleMeta>>();

			string[] assetsIWant;

			try 
			{
				assetsIWant = this.Assets.List("samples");

				int pos = -1;
				int lastCategoryId = -1;
				foreach (var asset in assetsIWant) 
				{
					if (!asset.Contains("_"))
						continue;

					SampleMeta sampleMeta = new SampleMeta(asset);
					if (sampleMeta.CategoryId!=lastCategoryId) 
					{
						pos++;
						if (!pos2activites.ContainsKey(pos))
							pos2activites.Add(pos, new List<SampleMeta>());
					} 
					pos2activites[pos].Add(sampleMeta);
					lastCategoryId = sampleMeta.CategoryId;
				}

				return pos2activites;


			} catch (Exception ex) 
			{
				Log.Error ("WIKITUDE_EXAMPLE", ex.ToString());
				return null;
			}
		}

		internal class SampleMeta 
		{
			public string Path { get; private set; }
			public string CategoryName { get; private set; }
			public string SampleName { get; private set; }
			public int CategoryId { get; private set; }
			public int SampleId { get; private set; }


			public SampleMeta(string path) 
			{
				Path = path;

				try
				{
					CategoryId = int.Parse(Path.Substring(0, Path.IndexOf("_")));
					Path = Path.Substring(Path.IndexOf("_") + 1);
					CategoryName = Path.Substring(0, Path.IndexOf("_"));
					Path = Path.Substring(Path.IndexOf("_") + 1);
					SampleId = int.Parse(Path.Substring(0, Path.IndexOf("_")));
					Path = Path.Substring(Path.IndexOf("_") + 1);
					SampleName = Path;

					Path = path;
				}
				catch(Exception ex)
				{
					Log.Debug("WIKITUDE_EXAMPLE", ex.ToString());
				}
			}

			public override string ToString ()
			{
				return string.Format ("categoryId:{0}, categoryName:{1}, sampleId:{2}, sampleName:{3}, path:{4}",
				                      CategoryId, CategoryName, SampleId, SampleName, Path);
			}
		}
	}
}


