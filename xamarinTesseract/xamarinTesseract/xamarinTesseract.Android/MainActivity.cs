using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Tesseract;
using Tesseract.Droid;
using Android.Content.Res;

namespace xamarinTesseract.Droid
{
    [Activity(Label = "xamarinTesseract", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;

            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            
            NinjectIoC.Initialize();

            NinjectIoC.Container.Bind<ITesseractApi>()
                      .ToConstructor(ctorArg => new TesseractApi(Android.App.Application.Context, AssetsDeployment.OncePerInitialization));

            NinjectIoC.Container.Bind<IProcessAssert>()
                      .ToConstructor(ctorArg => new ProcessAssert(this.Assets));

            LoadApplication(new App());
  
        }
    }
}

