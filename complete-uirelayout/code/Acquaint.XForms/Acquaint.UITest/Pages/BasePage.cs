﻿using System;

using NUnit.Framework;

using Xamarin.UITest;
using Xamarin.UITest.iOS;
using Xamarin.UITest.Queries;

namespace Acquaint.UITest
{
	public class BasePage
	{
		protected readonly IApp app;
		protected readonly bool OnAndroid;
		protected readonly bool OniOS;
		protected bool OniPad => app is iOSApp && ((iOSApp)app).Device.IsTablet;
		protected bool OniPhone => app is iOSApp && ((iOSApp)app).Device.IsPhone;

		protected BasePage(IApp app, Platform platform)
		{
			this.app = app;

			OnAndroid = platform == Platform.Android;
			OniOS = platform == Platform.iOS;
		}

		protected BasePage(IApp app, Platform platform, Func<AppQuery, AppQuery> androidTrait, Func<AppQuery, AppQuery> iOSTrait)
			: this(app, platform)
		{
			if (OnAndroid)
				Assert.DoesNotThrow(() => app.WaitForElement(androidTrait), "Unable to verify on page: " + this.GetType().Name);
			if (OniOS)
				Assert.DoesNotThrow(() => app.WaitForElement(iOSTrait), "Unable to verify on page: " + this.GetType().Name);

			app.Screenshot("On " + this.GetType().Name);
		}
	}
}