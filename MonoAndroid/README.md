# Wikitude Bindings for Mono for Android #

by Redth - [http://redth.info](http://redth.info "http://redth.info")


The Wikitude Bindings for MonoTouch and Mono for Android enables Xamarin developers to embed an Augmented Reality view into their MonoTouch and/or Mono for Android projects.  One can create a fully featured app with advanced Augmented Reality features including Image Recognition.



### SETUP ###

It is critical that you follow the setup guides for each platform, as you need to download the Wikitude SDK and the Vuforia SDK and extract some files from each, and put them in the right place in order to build the bindings and sample projects (or your own projects).

Failure to follow these guides will likely result in your applications not functioning!


##### Wikitude SDK SETUP #####
	1) Download the Wikitude Android SDK from: http://wikitude.com
	2) Inside the extracted zip file's contents, find the file:
		```/SDK/Extensions/VuforiaExtension/libExtensionVuforia.so```
	3) Ensure your project has the following folders inside of it:
		```/lib/armeabi/```
		```/lib/armeabi-v7a/```
	4) Copy the ```libExtensionVuforia.so``` file into both of your project's ```lib/armeabi*``` folders:
		```/lib/armeabi/libExtensionVuforia.so```
		```/lib/armeabi-v7a/libExtensionVuforia.so```
	5) For both copies of the ```libExtensionVuforia.so``` file, set the build action 
		(by right clicking each file and from the Build Action menu) to: ```AndroidNativeLibrary```
	7) Inside the extracted zip file's contents, find the file:
		```/SDK/wikitudesdk.jar```
	8) Copy the ```wikitudesdk.jar``` into the ```/Jars/``` folder of the ```Wikitude.SDK.MonoAndroid``` project
	9) For the ```wikitudesdk.jar``` file, set the build action 
		(by right clicking each file and from the Build Action menu) to: ```EmbeddedJar```


##### Vuforia SDK SETUP #####
	1) Download the Vuforia SDK from: https://developer.vuforia.com/resources/sdk/android
	2) After installing the SDK, find the files in the installed location:
		```/build/lib/armeabi/libQCAR.so```
		```/build/lib/armeabi-v7a/libQCAR.so```
	3) Copy both ```libQCAR.so``` files into their respective ```/lib/armeabi/``` and 
		```/lib/armeabi-v7a``` folders inside of your project 
		(the same folders you put the libExtensionVuforia.so in)
	4) Again, set the build action of both ```libQCAR.so``` files to ```AndroidNativeLibrary```
	5) After installing the SDK, find the files in the installed location:
		```/build/java/QCAR/QCAR.jar```
	6) Copy ```QCAR.jar``` into the ```/Jars/``` folder of the ```Wikitude.SDK.MonoAndroid``` project
	7) Again, set the build action of ```QCAR.jar``` to ```EmbeddedJar```



### LICENSE ###
Copyright 2012 Redth

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
