using System;
using SQLite;

namespace Concert2Go
{
	public class Concerten
	{
		public Concerten ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string Artiest { get; set; }

		public string Land { get; set; }

		public string Plaats { get; set; }

		public string Zaal { get; set; }

		public string Muzieksoort { get; set; }

		public DateTime Datum { get; set; }

		public string Opmerking { get; set; }

		public override string ToString ()
		{
			return string.Format ("[Concerten: ID={0}, Artiest={1}, Land={2}, Plaats={3}, Zaal={4}, Muzieksoort={5}, Datum={6}, Opmerking={7}]", ID, Artiest, Land, Plaats, Zaal, Datum, Opmerking);
		}
	}
}

