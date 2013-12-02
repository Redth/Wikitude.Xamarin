using System;
using MonoTouch.UIKit;
using Wikitude.Architect;
using MonoTouch.Foundation;

namespace WikitudeSample
{
	public class ARViewController : UIViewController
	{
		ArchitectView arView;

		public string WorldOrUrl { get; private set; }
		public bool IsUrl { get; private set; }

		public ARViewController (string worldOrUrl, bool isUrl) : base()
		{
			WorldOrUrl = worldOrUrl;
			IsUrl = isUrl;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			if (ArchitectView.IsDeviceSupported (ARMode.Geo))
			{
				arView = new ArchitectView (UIScreen.MainScreen.Bounds);
				View = arView;
			
				arView.Initialize ("YOUR_LICENSE_KEY_HERE", null);

				var absoluteWorldUrl = WorldOrUrl;

				if (!IsUrl)
					absoluteWorldUrl = NSBundle.MainBundle.BundleUrl.AbsoluteString + "/ARchitectExamples/" + WorldOrUrl + "/index.html";

				arView.LoadArchitectWorld (NSUrl.FromString (absoluteWorldUrl));

			}
			else
			{
				var adErr = new UIAlertView ("Unsupported Device", "This device is not capable of running ARchitect Worlds. Requirements are: iOS 5 or higher, iPhone 3GS or higher, iPad 2 or higher. Note: iPod Touch 4th and 5th generation are only supported in WTARMode_IR.", null, "OK", null);
				adErr.Show ();
			}
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			if (arView != null)
				arView.Start ();
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			if (arView != null)
				arView.Stop ();
		}
	}
}

