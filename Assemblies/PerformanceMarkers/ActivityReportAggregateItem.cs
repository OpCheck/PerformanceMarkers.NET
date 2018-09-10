using System;

namespace PerformanceMarkers
{
	public class ActivityReportAggregateItem
	{
		public string ActivityName;
		public int Count;
		public TimeSpan? TotalDuration;
		public TimeSpan? MaxDuration;
		public TimeSpan? AvgDuration;
		public TimeSpan? MinDuration;
	}
}
