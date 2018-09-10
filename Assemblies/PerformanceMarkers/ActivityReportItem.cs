using System;

namespace PerformanceMarkers
{
	public class ActivityReportItem
	{
		public string ActivityName;
		public TimeSpan? Duration;
		public string DurationFormatCode;
		public string Desc;
		public Granularity Granularity;

		public ActivityPoint StartPoint;
		public ActivityPoint EndPoint;
		
		/// <summary>
		/// The child report items.
		/// </summary>
		public ActivityReportItem[] ChildReportItems;
		
		
		public ActivityReportItem NextSiblingReportItem;
	}
}
