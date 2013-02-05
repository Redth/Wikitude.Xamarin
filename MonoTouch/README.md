# Wikitude Bindings for MonoTouch #

by Redth - [http://redth.info](http://redth.info "http://redth.info")


The Wikitude Bindings for MonoTouch and Mono for Android enables Xamarin developers to embed an Augmented Reality view into their MonoTouch and/or Mono for Android projects.  One can create a fully featured app with advanced Augmented Reality features including Image Recognition.



### MonoTouch SETUP ###

It is critical that you follow the setup guides for each platform, as you need to download the Wikitude SDK and the Vuforia SDK and extract some files from each, and put them in the right place in order to build the bindings and sample projects (or your own projects).

Failure to follow these guides will likely result in your applications not functioning!


##### Wikitude SDK SETUP - MonoTouch #####

	1) Download the Wikitude iOS SDK from: http://wikitude.com
	2) Inside the extracted zip file's contents, find the file:
		```/SDK/SDK/lib/Release-iphoneos/libWikitudeSDK.a```
	3) Copy ```libWikitudeSDK.a``` files into the ```/Lib/Device/``` folder inside of your project 	
	4) Inside the extracted zip file's contents, find the file:
		```/SDK/Extensions/VuforiaExtension/libExtensionVuforia.a```
	5) Copy ```libExtensionVuforia.a``` files into the ```/Lib/Device/``` folder inside of your project 	
	6) For both files, set the build action to ```ObjcBindingNativeLibrary```
		(by right clicking each file and from the Build Action menu)


##### Vuforia SDK SETUP - MonoTouch #####

	1) Download the Vuforia iOS SDK from: https://developer.vuforia.com/resources/sdk/ios
	2) After installing the SDK, find the file in the installed location:
		```/build/lib/arm/libQCAR2.a```
	3) Copy ```libQCAR2.a``` files into the ```/Lib/Device/``` folder inside of your project 
	4) Again, set the build action of both ```libQCAR2.a``` files to ```ObjcBindingNativeLibrary```
	


### LICENSE ###
Copyright 2012 Redth

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
