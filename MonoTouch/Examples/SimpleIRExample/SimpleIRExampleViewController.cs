using System;
using System.Drawing;
using MonoTouch.CoreFoundation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Wikitude.Architect;
using System.Collections.Generic;
using MonoTouch.CoreMotion;

namespace Wikitude.SDK.MonoTouch.SimpleIRExample
{
    public class SimpleIRExampleViewController : UIViewController
    {
        private const string WIKITUDE_SDK_LICENSE = "";
        
        private static float  TEST_LATITUDE =  47.77318f;
        private static float  TEST_LONGITUDE = 13.069730f;
        private static float    TEST_ALTITUDE = 150;
        
        UrlInvokeDelegate urlDel;        
        ArchitectView arView;
        
        public SimpleIRExampleViewController() : base()
        {
        }
        
        bool didInitAR = false;
        
        public override void ViewDidLoad()
        {

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
                
                var urlRes = NSBundle.MainBundle.PathForResource("SimpleIRExampleWorld", "html");
                
                urlDel = new UrlInvokeDelegate(url => {
                    
                    Console.WriteLine("URL CLICK: " + url);
                    
                });
                
                arView.Delegate = urlDel;
                
                //Load HTML
                arView.LoadArchitectWorldFromUrl(urlRes);
            }
            
            arView.Start();
            
            arView.SetUseInjectedLocation(true);
            
            arView.InjectLocationWithLatitude(TEST_LATITUDE, TEST_LONGITUDE, TEST_ALTITUDE, 1f);
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

