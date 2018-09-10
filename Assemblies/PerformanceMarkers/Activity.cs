using System;

namespace PerformanceMarkers
{
	public class Activity
	{
		/// <summary>
		/// A unique name for the activity.
		/// This should be short but readable.
		/// </summary>
		public string Name;
		
		
		/// <summary>
		/// Describes what this activity is doing - such as querying a database or connecting to a web server.
		/// </summary>
		public string Desc;
	}
}
