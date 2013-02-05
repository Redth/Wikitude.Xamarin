using System;
using System.Drawing;
using MonoTouch.CoreFoundation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Wikitude.Architect;
using System.Collections.Generic;
using MonoTouch.CoreMotion;

namespace Wikitude.SDK.MonoTouch.SimpleARBrowser
{
    public class SimpleARBrowserViewController : UIViewController
    {
        private const string WIKITUDE_SDK_LICENSE = "";

        private static float  TEST_LATITUDE =  47.77318f;
        private static float  TEST_LONGITUDE = 13.069730f;
        private static float    TEST_ALTITUDE = 150;
        
        UrlInvokeDelegate urlDel;        
        ArchitectView arView;

        public SimpleARBrowserViewController() : base()
        {
        }
        
        bool didInitAR = false;
        
        public override void ViewDidLoad()
        {
            
            
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
        }
        
        public override void ViewWillAppear(bool animated)
        {
            if (!ArchitectView.IsDeviceSupported())
            {
                Console.WriteLine("Device Not Supported!!!");
                return;
            }
            
            if (!didInitAR)
            {
                didInitAR = true;
                
                arView = new ArchitectView(this.View.Bounds);
                
                arView.Initialize("", null);

                this.View.AddSubview(arView);
                
                var urlRes = NSBundle.MainBundle.PathForResource("SimpleARBrowserWorld", "html");
                
                urlDel = new UrlInvokeDelegate(url => {
                    
                    Console.WriteLine("URL CLICK: " + url);
                    
                });
                
                arView.Delegate = urlDel;
               
                //Load HTML
                arView.LoadArchitectWorldFromUrl(urlRes);
                
                //Call javascript POI's
                var json = GeneratePoiJson();
                arView.CallJavaScript(string.Format("newData('{0}')", json));
            }
            
            arView.Start();
            
            arView.SetUseInjectedLocation(true);
            
            arView.InjectLocationWithLatitude(TEST_LATITUDE, TEST_LONGITUDE, TEST_ALTITUDE, 1f);
        }

        Random rnd = new Random();
        
        string GeneratePoiJson()
        {
            Console.WriteLine("LOADING...");
            
            
            Console.WriteLine("LOADED...");
            var pois = new List<string>();
            
            Console.WriteLine("LOCATION: " + TEST_LATITUDE + ", " + TEST_LONGITUDE + ", " + TEST_ALTITUDE);
            
            for (int i = 0; i < 50; i++)
            {
                var loc = CreateRandomLocation();

                var title = "POI #" + i;
                var desc = "Probably one of the best POIs you have ever seen. This is the description of Poi #" + i;
                
                var point = "{" + string.Format("\"latitude\":{0},\"longitude\":{1},\"altitude\":{2}", loc[0], loc[1], loc[2]) + "}";
                
                var json = "{" +
                    string.Format("\"id\":\"{0}\",\"name\":\"{1}\",\"description\":\"{2}\",\"type\":{3},\"Point\":{4}", i,
                                  title, desc, rnd.Next(1, 3), point) + "}";
                
                Console.WriteLine("POINT: " + point);
                
                pois.Add(json);
            }
            
            Console.WriteLine("Added POIS");
            
            var datajson = "[" + string.Join(",", pois) + "]";

            return datajson;
        }
        
        double[] CreateRandomLocation()
        {
            return new double[] { TEST_LATITUDE + ((rnd.NextDouble() - 0.5) / 500), TEST_LONGITUDE + ((rnd.NextDouble() - 0.5) / 500), TEST_ALTITUDE + ((rnd.NextDouble() - 0.5) * 10) };
        }
        
        public override void ViewWillDisappear(bool animated)
        {
            arView.Stop();
        }
        
    }
    
    public class UrlInvokeDelegate : ArchitectViewDelegate
    {
        public UrlInvokeDelegate() : base()
        {}
        
        public UrlInvokeDelegate(Action<string> urlInvokedAction) : base()
        {
            this.UrlInvokedAction = urlInvokedAction;
        }
        
        Action<string> UrlInvokedAction { get;set; }
        
        public override void UrlWasInvoked(string url)
        {
            if (UrlInvokedAction != null)
                UrlInvokedAction(url);
        }
    }
    
    
}

