using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.Res;

namespace xamarinTesseract.Droid
{
    public class ProcessAssert : IProcessAssert
    {
        private readonly AssetManager _assetManager;

        public ProcessAssert(AssetManager assetManager)
        {
            _assetManager = assetManager;
        }

        public Stream GetStreamByPath(string Path)
        {
            return _assetManager.Open(Path);
        }
    }
}