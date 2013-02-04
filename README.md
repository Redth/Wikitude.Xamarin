# Wikitude Bindings for MonoTouch and Mono for Android #

by Redth - [http://redth.info](http://redth.info "http://redth.info")


The Wikitude Bindings for MonoTouch and Mono for Android enables Xamarin developers to embed an Augmented Reality view into their MonoTouch and/or Mono for Android projects.  One can create a fully featured app with advanced Augmented Reality features including Image Recognition.


- Available for MonoTouch and Mono for Android
- Simple MonoTouch and Mono for Android integration
- Fully customizable Augmented Reality view
- Includes full feature set of Wikitude SDK
- AR content is purely written in HTML and JavaScript
- Includes Image Recognition functionality


### The Augmented Reality View ###

From a technical point of view the SDK adds a UI component, similar to a web view. In contrast to a standard web view this AR view can render Augmented Reality content.

Note: Content developed for this AR View is written in JavaScript and HTML. The AR engine working in the background is called ARchitect Engine and is powering the SDK.


### Prerequisites ###
- MonoTouch 6.2 or higher / Mono for Android 4.5 or higher
- Wikitude SDK for Android and/or iOS
- Vuforia SDK for Android and/or iOS
- Android requires: ```<uses-sdk android:minSdkVersion="8"/>``` or higher in AndroidManifest.xml be aware that the Wikitude SDK runs only on Android 2.2+ devices (Android SDK v8)


### SETUP ###

It is critical that you follow the setup guides for each platform, as you need to download the Wikitude SDK and the Vuforia SDK and extract some files from each, and put them in the right place in order to build the bindings and sample projects (or your own projects).

Failure to follow these guides will likely result in your applications not functioning!

Each platform (MonoTouch and Mono for Android) has its own README for how to SETUP your environment!


### LICENSE ###
Copyright 2012 Redth

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
