using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using System.IO;
using System.Collections.Generic;

namespace WikitudeSample
{
	public class RecentUrlsViewController : DialogViewController
	{
		ARViewController arController;
		UIBarButtonItem buttonAdd;

		public RecentUrlsViewController () : base(UITableViewStyle.Plain, new RootElement("Recent Urls"), true)
		{
			Root.Add (new Section ());
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			buttonAdd = new UIBarButtonItem (UIBarButtonSystemItem.Add);
			buttonAdd.Clicked += (sender, e) => {
				var arAdd = new UIAlertView("Load Url", "Enter a Wikitude World URL to Load:", null, "Cancel", "Load");
				arAdd.AlertViewStyle = UIAlertViewStyle.PlainTextInput;

				arAdd.Clicked += (sender2, e2) => {
					var tf = arAdd.GetTextField(0);

					var url = tf.Text;

					var isOk = true;

					try { var uri = new Uri(url); }
					catch { isOk = false; }

					if (isOk)
					{
						AddUrl(url);
						Reload();

						arController = new ARViewController (url, true);
						NavigationController.PushViewController (arController, true);
					}
				};

				arAdd.Show();
			};

			NavigationItem.RightBarButtonItem = buttonAdd;

			Reload ();
		}

		public static void AddUrl(string url)
		{
			var path = Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "recenturls.json");

			var urls = new List<string> ();

			try { urls = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>> (File.ReadAllText (path)); }
			catch { }

			urls.Add (url);

			try { File.WriteAllText(path, Newtonsoft.Json.JsonConvert.SerializeObject(urls)); }
			catch { }
		}

		void Reload()
		{
			var path = Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "recenturls.json");

			var urls = new List<string> ();

			try { urls = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>> (File.ReadAllText (path)); }
			catch { }

			foreach (var url in urls)
			{
				Root [0].Add (new StyledStringElement (url, () =>
				{
					arController = new ARViewController (url, true);
					NavigationController.PushViewController (arController, true);
				}));
			}
		}
	}
}

