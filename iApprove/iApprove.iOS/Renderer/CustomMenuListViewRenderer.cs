using iApprove.CustomControl;
using iApprove.iOS.Renderer;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMenuListView), typeof(CustomMenuListViewRenderer))]

namespace iApprove.iOS.Renderer
{
    class CustomMenuListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            if (e.NewElement == null)
                return;

            base.OnElementChanged(e);
            if (Control != null)
            {


            }
        }
    }
}
