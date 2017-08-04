﻿using GurruPCL.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(GurruPCL.Droid.Helpers.PhotoCleaner))]

namespace GurruPCL.Droid.Helpers
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