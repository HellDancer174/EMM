using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EMM.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Picker), typeof(HackFix20181030PickerRenderer))]
namespace EMM.Droid
{
    public class HackFix20181030PickerRenderer : Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer
    {
        public HackFix20181030PickerRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Focusable = false;
            }
        }
    }


}