using System;
using System.Collections.Generic;

namespace PerformanceMarkers
{
	public class ActivityReportItemCalculator
	{
		public static double Count (IEnumerable<ActivityReportItem> ActivityReportItems)
		{
			double ItemsCount = 0d;
		
			foreach (ActivityReportItem CurrentItem in ActivityReportItems)
			{
				ItemsCount += 1d;
			}
			
			return ItemsCount;
		}


		public static double TotalDuration (IEnumerable<ActivityReportItem> ActivityReportItems)
		{
			double DurationCalculation = 0d;
			
			foreach (ActivityReportItem CurrentItem in ActivityReportItems)
			{
				if (CurrentItem.Duration != null)
					DurationCalculation += CurrentItem.Duration.Value.TotalMilliseconds;
			}
			
			return DurationCalculation;
		}


		public static double MaxDuration (IEnumerable<ActivityReportItem> ActivityReportItems)
		{
			double DurationCalculation = Double.MinValue;
			
			foreach (ActivityReportItem CurrentItem in ActivityReportItems)
			{
				if (CurrentItem.Duration != null && CurrentItem.Duration.Value.TotalMilliseconds > DurationCalculation)
					DurationCalculation = CurrentItem.Duration.Value.TotalMilliseconds;
			}
			
			return DurationCalculation;
		}


		public static double MinDuration (IEnumerable<ActivityReportItem> ActivityReportItems)
		{
			double DurationCalculation = Double.MaxValue;
			
			foreach (ActivityReportItem CurrentItem in ActivityReportItems)
			{
				if (CurrentItem.Duration != null && CurrentItem.Duration.Value.TotalMilliseconds < DurationCalculation)
					DurationCalculation = CurrentItem.Duration.Value.TotalMilliseconds;
			}
			
			return DurationCalculation;
		}


		public static double AvgDuration (IEnumerable<ActivityReportItem> ActivityReportItems)
		{
			return TotalDuration(ActivityReportItems) / Count(ActivityReportItems);
		}
	}
}
