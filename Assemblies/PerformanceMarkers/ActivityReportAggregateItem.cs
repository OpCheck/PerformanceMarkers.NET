using System;

namespace PerformanceMarkers
{
	public class ActivityReportAggregateItem
	{
		public string ActivityName;
		public double Count;
		public double? TotalDuration;
		public double? TotalDurationPercent;
		public double? MaxDuration;
		public double? AvgDuration;
		public double? MinDuration;
	}
}
