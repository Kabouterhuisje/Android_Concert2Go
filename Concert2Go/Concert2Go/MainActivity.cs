using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Concert2Go
{
	[Activity (Label = "Concert2Go", MainLauncher = true, Icon = "@drawable/Concert2Go")]
	public class MainActivity : Activity
	{
		public ListView lv;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);


		}

		public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
		{
			base.OnCreateOptionsMenu(menu);
			int groupId = 0;
			int menuItemId = Android.Views.Menu.First;
			int menuItemOrder = Android.Views.Menu.None;
			// Text to be displayed for this menu item.
			int menuItemText = Resource.String.menuitem1;
			// Create the menu item and keep a reference to it.
			//Het eerste menu item wordt hier toegevoegd
			IMenuItem menuItem1 = menu.Add(groupId, menuItemId, menuItemOrder, menuItemText);
			menuItem1.SetShortcut('1', 'a');
			Int32 MenuGroup = 10;
			//Het tweede menu item wordt hier toegevoegd
			IMenuItem menuItem2 =
				menu.Add(MenuGroup, menuItemId + 1, menuItemOrder + 1,
					new Java.Lang.String("Toevoegen"));
			menuItem2.SetShortcut('2', 'b');
			//Het derde menu item wordt hier toegevoegd
			IMenuItem menuItem3 =
				menu.Add(MenuGroup, menuItemId + 2, menuItemOrder + 2,
					new Java.Lang.String("Overzicht"));
			menuItem3.SetShortcut('3', 'c');
			return true;
		}

		public override bool OnMenuItemSelected(int featureID, IMenuItem item)
		{
			base.OnMenuItemSelected (featureID, item);
			switch (item.ItemId) 
			{
			case (1):
				var intentHome = new Intent (this, typeof(MainActivity));
				StartActivity (intentHome);
				break;
			case (2):
				var intent = new Intent (this, typeof(Toevoegen));
				StartActivity (intent);
				break;
			case (3):
				var intent2 = new Intent (this, typeof(Overzicht));
				StartActivity (intent2);
				break;
			}
			return false;
		}


	}
}


