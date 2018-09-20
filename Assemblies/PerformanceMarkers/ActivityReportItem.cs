using System;

namespace PerformanceMarkers
{
	public class ActivityReportItem
	{
		public string ActivityName;
		
		public double? Duration;
		
		public double? HiddenDuration;
		public double? HiddenDurationPercent;
		
		public string DurationFormatCode;
		public string Desc;

		public ActivityPoint StartPoint;
		public ActivityPoint EndPoint;
		
		/// <summary>
		/// The child report items.
		/// </summary>
		public ActivityReportItem[] ChildReportItems;
		
		
		public ActivityReportItem NextSiblingReportItem;
	}
}
