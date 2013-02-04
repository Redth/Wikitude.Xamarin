using Android.App;
using Android.OS;
using Android.Views;
using Com.Qualcomm.QCAR;
using Com.Wikitude.Architect;

namespace Wikitude.SDK.MonoAndroid
{
	[Activity(Label = "Simple IR Example", MainLauncher = true)]
	public class SimpleIRExampleActivity : Activity
	{
		const string WIKITUDE_LICENSE_KEY = "";
		static float TEST_LATITUDE = 47.77318f;
		static float TEST_LONGITUDE = 13.069730f;
		static float TEST_ALTITUDE = 150;

		private ArchitectView architectView;
		private VuforiaServiceImplementation serviceImplementation;

		static SimpleIRExampleActivity()
		{
			Java.Lang.JavaSystem.LoadLibrary("QCAR");
			Java.Lang.JavaSystem.LoadLibrary("ExtensionVuforia");	
		}

		void LoadWorld()
		{
			this.architectView.Load("SimpleIRWorld.html");
		}

		
		protected override void OnCreate(Bundle bundle)
		{
			RequestWindowFeature(WindowFeatures.NoTitle);

			base.OnCreate(bundle);

			// Create your application here
			SetContentView(Resource.Layout.SimpleIRExampleLayout);

		
			this.architectView = this.FindViewById<ArchitectView>(Resource.Id.architectView);

			var config = new ArchitectView.ArchitectConfig(WIKITUDE_LICENSE_KEY);
			
			serviceImplementation = new VuforiaServiceImplementation();

			config.VuforiaInterface = serviceImplementation;

			this.architectView.OnCreate(config);
		}

		protected override void OnPostCreate(Bundle savedInstanceState)
		{
			base.OnPostCreate(savedInstanceState);

			if (this.architectView != null)
				this.architectView.OnPostCreate();
		}

		protected override void OnResume()
		{
			base.OnResume();

			this.architectView.OnResume();
			this.architectView.SetLocation(TEST_LATITUDE, TEST_LONGITUDE, TEST_ALTITUDE, 1.0f);

			LoadWorld();
		}

		protected override void OnPause()
		{
			base.OnPause();
			if (this.architectView != null)
				this.architectView.OnPause();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (this.architectView != null)
				this.architectView.OnDestroy();
		}

		public override void OnLowMemory()
		{
			base.OnLowMemory();
			if (this.architectView != null)
				this.architectView.OnLowMemory();
		}
	}

	public class VuforiaServiceImplementation : Java.Lang.Object, IVuforiaInterface
	{
		public void DeInit() 
		{
			QCAR.Deinit();
		}

		public int Init() {
			return QCAR.Init();
		}

		public void OnPause() {
			QCAR.OnPause();
		}

		public void OnResume() {
			QCAR.OnResume();
		}

		public void OnSurfaceChanged(int width, int height) {
			QCAR.OnSurfaceChanged(width, height);
		}

		public void OnSurfaceCreated() {
			QCAR.OnSurfaceCreated();
		}

		public void SetInitParameters(Activity activity, int nFlags) {
			QCAR.SetInitParameters(activity, nFlags);
		}
		
	}
}