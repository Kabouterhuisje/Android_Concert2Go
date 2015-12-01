
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Concert2Go
{
	[Activity (Label = "Overzicht")]			
	public class Overzicht : Activity
	{
		private ListView lv;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Overzicht);

			lv = FindViewById<ListView> (Resource.Id.lvConcerten);
			ConcertenDB csdb = new ConcertenDB ();
			ArrayAdapter<string> adapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleListItem1, ConcertenDB.alleRijen (csdb.db));
			lv.Adapter = adapter;

			lv.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
				Toast.MakeText(this, e.Position.ToString(), ToastLength.Short).Show();
			};
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

