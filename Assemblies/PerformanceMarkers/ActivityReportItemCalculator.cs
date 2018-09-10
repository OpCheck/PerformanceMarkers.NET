using System;
using System.Collections.Generic;

namespace PerformanceMarkers
{
	public class ActivityReportItemCalculator
	{
		public static int Count (IEnumerable<ActivityReportItem> ActivityReportItems)
		{
			int ItemsCount = 0;
		
			foreach (ActivityReportItem CurrentItem in ActivityReportItems)
			{
				ItemsCount++;
			}
			
			return ItemsCount;
		}


		public static TimeSpan TotalDuration (IEnumerable<ActivityReportItem> ActivityReportItems)
		{
			TimeSpan CreatedTimeSpan = new TimeSpan(0, 0, 0);
			
			foreach (ActivityReportItem CurrentItem in ActivityReportItems)
			{
				if (CurrentItem.Duration != null)
					CreatedTimeSpan = CreatedTimeSpan.Add(CurrentItem.Duration.Value);
			}
			
			return CreatedTimeSpan;
		}


		public static TimeSpan MaxDuration (IEnumerable<ActivityReportItem> ActivityReportItems)
		{
			TimeSpan CreatedTimeSpan = new TimeSpan(0, 0, 0);
			
			foreach (ActivityReportItem CurrentItem in ActivityReportItems)
			{
				if (CurrentItem.Duration != null && CurrentItem.Duration.Value > CreatedTimeSpan)
					CreatedTimeSpan = CurrentItem.Duration.Value;
			}
			
			return CreatedTimeSpan;
		}


		public static TimeSpan MinDuration (IEnumerable<ActivityReportItem> ActivityReportItems)
		{
			TimeSpan CreatedTimeSpan = TimeSpan.MaxValue;
			
			foreach (ActivityReportItem CurrentItem in ActivityReportItems)
			{
				if (CurrentItem.Duration != null && CurrentItem.Duration.Value < CreatedTimeSpan)
					CreatedTimeSpan = CurrentItem.Duration.Value;
			}
			
			return CreatedTimeSpan;
		}


		public static TimeSpan AvgDuration (IEnumerable<ActivityReportItem> ActivityReportItems)
		{
			return TimeSpan.FromMilliseconds(TotalDuration(ActivityReportItems).TotalMilliseconds / ((double)Count(ActivityReportItems)));
		}
	}
}
