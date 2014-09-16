using System;

using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;

using Appracatappra.ActionComponents.ActionTray;

namespace ActionTrayTest
{
    [Activity(Label = "ActionTrayTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static readonly float DRAWER_WIDTH_PERCENTAGE = 0.83f;
        
		Button trayButton;
		Button gcButton;
		Button imageButton;
        UIActionTray rightTray;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
			trayButton = FindViewById<Button>(Resource.Id.TrayButton);
			gcButton = FindViewById<Button>(Resource.Id.GCButton);
			imageButton = FindViewById<Button>(Resource.Id.ImageButton);

            rightTray = FindViewById<UIActionTray>(Resource.Id.tray_right);
            rightTray.trayType = UIActionTrayType.Popup;
            rightTray.frameType = UIActionTrayFrameType.EdgeOnly;
            rightTray.orientation = UIActionTrayOrientation.Right;
            rightTray.tabType = UIActionTrayTabType.Plain;

            rightTray.bringToFrontOnTouch = true;
            rightTray.layoutParams.Width = (int)(Resources.DisplayMetrics.WidthPixels * DRAWER_WIDTH_PERCENTAGE);
            rightTray.CloseTray(true);
        }

        protected override void OnResume()
        {
            base.OnResume();
			trayButton.Click += OnTrayButtonClicked;
            gcButton.Click += OnGCButtonClicked;
			imageButton.Click += OnImageButtonClicked;
        }

        protected override void OnPause()
        {
            trayButton.Click -= OnTrayButtonClicked;
			gcButton.Click -= OnGCButtonClicked;
			imageButton.Click -= OnImageButtonClicked;
            base.OnPause();
        }

        #region Events
        void OnTrayButtonClicked(object sender, EventArgs e)
        {
            if (rightTray.isOpened)
            {
                rightTray.CloseTray(true);
            }
            else
            {
                rightTray.OpenTray(true);
            }
        }

		void OnGCButtonClicked (object sender, EventArgs e)
		{
			GC.Collect ();
		}

		void OnImageButtonClicked (object sender, EventArgs e)
		{
			using (BitmapFactory.DecodeResource (Resources, Resource.Drawable.image)) {
				Console.WriteLine ("Image created");
			}
		}
        #endregion
    }
}
