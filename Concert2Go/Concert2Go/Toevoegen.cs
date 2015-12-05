﻿
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
using SQLite;

namespace Concert2Go
{
	[Activity (Label = "Toevoegen")]			
	public class Toevoegen : Activity
	{
		private TextView dateDisplay;
		private Button pickDate;
		private DateTime date;
		private Button btnSave;

		private EditText art;
		private EditText lnd;
		private EditText plts;
		private EditText zl;
		private Spinner sp;
		private EditText dtm;
		private EditText opm;

		const int DATE_DIALOG_ID = 0;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Toevoegen);

			// capture our View elements
			dateDisplay = FindViewById<TextView> (Resource.Id.DatumTekst);
			pickDate = FindViewById<Button> (Resource.Id.pickDate);
			btnSave = FindViewById<Button> (Resource.Id.btnSave);

			art = FindViewById<EditText> (Resource.Id.ArtiestTekst);
			lnd = FindViewById<EditText> (Resource.Id.LandTekst);
			plts = FindViewById<EditText> (Resource.Id.PlaatsTekst);
			zl = FindViewById<EditText> (Resource.Id.ZaalTekst);
			sp = FindViewById<Spinner> (Resource.Id.SpinnerMuzieksoort);
			dtm = FindViewById<EditText> (Resource.Id.DatumTekst);
			opm = FindViewById<EditText> (Resource.Id.OpmerkingTekst);


			// add a click event handler to the button
			pickDate.Click += delegate { ShowDialog (DATE_DIALOG_ID); };

			// get the current date
			date = DateTime.Today;

			// display the current date (this method is below)
			UpdateDisplay ();

			sp.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (sp_ItemSelected);
			var adapter = ArrayAdapter.CreateFromResource (
				this, Resource.Array.genre_array, Android.Resource.Layout.SimpleSpinnerItem);

			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			sp.Adapter = adapter;


			// Click event voor het opslaan van de data
			btnSave.Click += (object IntentSender, EventArgs e) => {
				
				ConcertenDB cdb = new ConcertenDB();
				Concerten dataInput = new Concerten();
				dataInput.Artiest = art.Text.ToString();
				dataInput.Land = lnd.Text.ToString();
				dataInput.Plaats = plts.Text.ToString();
				dataInput.Zaal = zl.Text.ToString();
				dataInput.Muzieksoort = sp.SelectedItem.ToString();
				dataInput.Datum = DateTime.Parse(dtm.Text);
				dataInput.Opmerking = opm.Text.ToString();
				// Refreshen van de input velden en melding tonen na succesvol invoeren van data
				if(cdb.insertUpdateData(dataInput))
				{
					Toast.MakeText(this, "Je gegevens zijn succesvol toegevoegd!", ToastLength.Long).Show();
					art.Text = "";
					lnd.Text = "";
					plts.Text = "";
					zl.Text = "";
					sp.SelectedItem.Dispose();
					dtm.Text = DateTime.Now.ToString();
					opm.Text = "";
				}
			};
		
		}

		// updates the date in the TextView
		private void UpdateDisplay ()
		{
			dateDisplay.Text = date.ToString ("d");
		}

		// the event received when the user "sets" the date in the dialog
		void OnDateSet (object sender, DatePickerDialog.DateSetEventArgs e)
		{
			this.date = e.Date;
			UpdateDisplay ();
		}

		protected override Dialog OnCreateDialog (int id)
		{
			switch (id) {
			case DATE_DIALOG_ID:
				return new DatePickerDialog (this, OnDateSet, date.Year, date.Month - 1, date.Day); 
			}
			return null;
		}

		// Menu aanmaken met de drie schermen voor navigatie binnen de applicatie
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

		private void sp_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;

			string toast = string.Format ("Je hebt gekozen voor {0}", spinner.GetItemAtPosition (e.Position));
			Toast.MakeText (this, toast, ToastLength.Long).Show ();
		}


	}
}

