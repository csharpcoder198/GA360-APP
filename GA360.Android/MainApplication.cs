using System;
using Android.App;
using Android.Runtime;
using Shiny;

namespace GA360.Droid
{
	[Application]
	public class MainApplication : Application
	{
		public MainApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer) { }

		public override void OnCreate()
		{
			this.ShinyOnCreate(new Startup());
			base.OnCreate();
		}
	}
}
