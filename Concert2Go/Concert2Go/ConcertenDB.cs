using System;
using SQLite;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Concert2Go
{
	public class ConcertenDB
	{
		public SQLiteConnection db;
		public string result;
		private string sqldb_query;
		private string sqldb_message;


		public ConcertenDB ()
		{
			// Bepalen van het database bestand op een specifieke locatie
			var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
			var pathToDatabase = System.IO.Path.Combine (docsFolder, "dbConcerten.db");

			result = createDatabase (pathToDatabase);
		}

		// Aanmaken database
		private string createDatabase(string path)
		{
			try
			{
				db = new SQLiteConnection(path);
				db.CreateTable<Concerten>();
				return "Database created";
			}
			catch(SQLiteException ex) 
			{
				return ex.Message;
			}
		}

		// Toevoegen en updaten van data
		public bool insertUpdateData(Concerten data)
		{
			try
			{
				if (db.Insert(data) != 0)
				{
					db.Update(data);
				}
					
				return true;
			}
			catch(SQLiteException ex) 
			{
				return false;
			}
		}

		// Laat zien hoeveel rijen er in de database zitten
		public int aantalRijen()
		{
			try
			{
				var count = db.ExecuteScalar<int>("SELECT Count(*) FROM Concerten");

				return count;
			}
			catch(SQLiteException)
			{
				return -1;
			}
		}

		// Laat laatste ID zien
		public int LaatsteID()
		{
			try
			{
				// Selecteren van de laatste toegevoegde ID
				var max = db.ExecuteScalar<int>("select MAX(ID) from Concerten");

				return max;
			}
			catch(SQLiteException) {
				return -5;
			}
		}

		// Zoeken op ID
		public static IEnumerable<Concerten> QueryConcerten (SQLiteConnection db, int id)
		{
			return db.Query<Concerten> ("select * from Concerten where ID = ?", id);
		}

		// Method om data te laten zien in het overzicht op het Overzicht scherm
		public static List<String> alleRijen(SQLiteConnection db)
		{
			List<String> rijen = new List<String> ();
			var table = db.Query<Concerten> ("SELECT * FROM Concerten ORDER BY Datum ASC");
			foreach (var s in table) {
				rijen.Add (string.Format ("Datum: {1} | Artiest: {2} | Land: {3} | Plaats: {4} | Zaal: {5} | Genre: {6} | Opmerking: {7} | ID: {8}", s.Datum.ToShortDateString(), s.Datum, s.Artiest, s.Land, s.Plaats, s.Zaal, s.Muzieksoort, s.Opmerking, s.ID));
			}
			return rijen;
		}

		// Verwijderen van data
		public void DeleteRecord(int iId)
		{
			try
			{
				sqldb_query = "DELETE FROM Concerten WHERE ID ='" + iId + "';";
				db.Execute(sqldb_query);
				sqldb_message = "Record " + iId + " Verwijderd";
			}
			catch(SQLiteException ex) 
			{
				sqldb_message = ex.Message;
			}
		}

	}
}

