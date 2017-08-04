using System;
using GurruPCL.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(GurruPCL.iOS.Helpers.PhotoCleaner))]

namespace GurruPCL.iOS.Helpers
{
    public class PhotoCleaner : IPhotoCleaner
    {
        public void DeletePhoto(string path)
        {
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }

    }
}