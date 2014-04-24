using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using System.IO;
using System.Json;

namespace WikitudeSample
{
	public partial class ExampleListViewController : DialogViewController
	{
		ARViewController arController;
		RecentUrlsViewController urlsController;

		UIBarButtonItem buttonUrls;

		public ExampleListViewController () : base (UITableViewStyle.Grouped, null)
		{

			Root = new RootElement ("Examples");

			var indexList = Path.Combine (NSBundle.MainBundle.BundlePath, "IndexList.json");

			//var json = JArray.Parse (File.ReadAllText (indexList));
			var json = JsonArray.Parse (File.ReadAllText (indexList));

			var titleList = new string[] {
				"ImageRecognition",
				"3dAndImageRecognition",
				"PointOfInterest",
				"ObtainPoiData",
				"BrowsingPois",
				"Video",
				"Demo"
			};


			for (int i = 0; i < titleList.Length; i++)
			{
				var title = titleList [i];

				var indexArr = (JsonArray)json [i];

				var section = new Section (title);

				foreach (var jobj in indexArr)
				{
					var elem = new StyledStringElement (jobj["Title"].ToString(), () =>
					{
						var path = jobj["Path"].ToString();
						var vc = jobj["ViewController"].ToString();
						
						arController = new ARViewController(path, false);
						NavigationController.PushViewController(arController, true);
					});
					section.Add (elem);
				}

				Root.Add (section);
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			buttonUrls = new UIBarButtonItem ("URL's", UIBarButtonItemStyle.Bordered, new EventHandler((s, e) =>
			{
				urlsController = new RecentUrlsViewController();
				NavigationController.PushViewController(urlsController, true);
			}));

			NavigationItem.RightBarButtonItem = buttonUrls;
		}
	}
}
