#### The Augmented Reality View####

Wikitude uses the concept of a Wikitude 'World', which is a HTML/CSS/Javascript page that you tell the `ArchitectView` to load.  In this page, you can access the Wikitude ARchitect Engine javascript API to add image recognition targets and markers, Geo markers, and more!  The idea is that you can share your 'World' code across iOS and Android platforms.

Therefore, on each platform, we only need to create the `ArchitectView` to load the Wikitude World into.

To learn more about building Wikitude Worlds, visit: http://developer.wikitude.com/ and also check out the samples included in this release that show you how to create and load Wikitude worlds.


#### iOS ####

To get started, you'll need to create a new instance of the `ArchitectView` and add it to your ViewController.  You should also check to see if the AR mode you want is supported on the device:

```csharp
//Check and see if Geo AR is supported
if (ArchitectView.IsDeviceSupported (ARMode.Geo))
{
	//Create our AR View
	arView = new ArchitectView (UIScreen.MainScreen.Bounds);
	
	//Set our ViewController's view to be the AR View
	this.View = arView;

	//Initialize our AR engine with our license key
	arView.SetLicenseKey ("YOUR-LICENSE-KEY");

	//Load an AR Wikitude World from a url
	arView.LoadArchitectWorld (NSUrl.FromString ("http://wikitude.world.url.com"));
}
```

The other important step is to tell the ArchitectView about lifecycle events in your View Controller, namely, when the Architect View should be Started and Stopped.  This usually happens in the `ViewDidAppear` and `ViewWillDisappear` methods:

```csharp
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
```


#### Android ####

To get started, you'll need to add an `ArchitectView` to your Android Layout:

```xml
<com.wikitude.architect.ArchitectView
  android:id="@+id/architectView"
  android:layout_width="fill_parent"
  android:layout_height="fill_parent" />
```

Next, you need to inform the ArchitectView of all the Activity or Fragment lifecycle changes:

```csharp
protected override void OnCreate (Bundle bundle)
{
	//Set your content view as usual
	SetContentView(Resource.Layout.main);

	//Find your Architect view from the layout you just used
	architectView = FindViewById<ArchitectView>(Resource.Id.architectView);

	//Activate the ArchitectView with your license
	architectView.OnCreate(new ArchitectView.ArchitectConfig("YOUR LICENSE KEY"));
}

bool worldLoaded = false;
protected override void OnResume ()
{
	base.OnResume ();

	if (architectView != null) {
		//Tell the AR View about the resume step in the lifecycle
		architectView.OnResume ();

		//Load your wikitude world if it hasn't been loaded yet
		if (!worldLoaded) {
			architectView.Load("http://wikitude.world.url.com");	
			worldLoaded = true;
		}
	}
}

protected override void OnPause ()
{
	base.OnPause ();

	if (architectView != null)
		architectView.OnPause ();
}

protected override void OnDestroy ()
{
	base.OnDestroy ();

	if (architectView != null)
		architectView.OnDestroy ();
}

public override void OnLowMemory ()
{
	base.OnLowMemory ();

	if (architectView != null)
		architectView.OnLowMemory ();
}
```

##### Android and Location Updates #####
On Android, you must also manually inject location updates to the ArchitectView.  There are several strategies for obtaining location data on Android, but the important thing is to call the `SetLocation` method on the ArchitectView whenever your location changes:

```csharp
if (architectView != null)
	architectView.SetLocation (LATITUDE, LONGITUDE, ALTITUDE, ACCURACY);
```

### Learn More
You can learn more about Wikitude by visiting http://wikitude.com

