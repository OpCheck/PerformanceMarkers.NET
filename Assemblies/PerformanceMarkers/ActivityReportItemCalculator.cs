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
					DurationCalculation += CurrentItem.Duration.Value;
			}
			
			return DurationCalculation;
		}


		public static double? TotalDurationPercent (ActivityReportItem ParentReportItem, IEnumerable<ActivityReportItem> ActivityReportItems)
		{
			if (ParentReportItem.Duration == null || ParentReportItem.Duration.Value == 0d)
				return null;
		
			return TotalDuration(ActivityReportItems) / ParentReportItem.Duration.Value;
		}


		public static double MaxDuration (IEnumerable<ActivityReportItem> ActivityReportItems)
		{
			double DurationCalculation = Double.MinValue;
			
			foreach (ActivityReportItem CurrentItem in ActivityReportItems)
			{
				if (CurrentItem.Duration != null && CurrentItem.Duration.Value > DurationCalculation)
					DurationCalculation = CurrentItem.Duration.Value;
			}
			
			return DurationCalculation;
		}


		public static double MinDuration (IEnumerable<ActivityReportItem> ActivityReportItems)
		{
			double DurationCalculation = Double.MaxValue;
			
			foreach (ActivityReportItem CurrentItem in ActivityReportItems)
			{
				if (CurrentItem.Duration != null && CurrentItem.Duration.Value < DurationCalculation)
					DurationCalculation = CurrentItem.Duration.Value;
			}
			
			return DurationCalculation;
		}


		public static double AvgDuration (IEnumerable<ActivityReportItem> ActivityReportItems)
		{
			return TotalDuration(ActivityReportItems) / Count(ActivityReportItems);
		}
		
		
		public static double? HiddenDuration (ActivityReportItem ParentReportItem)
		{
			if (ParentReportItem.Duration == null)
				return null;
		
			double HiddenDuration = ParentReportItem.Duration.Value;
			
			foreach (ActivityReportItem CurrentChildReportItem in ParentReportItem.ChildReportItems)
			{
				if (CurrentChildReportItem.Duration != null)
					HiddenDuration -= CurrentChildReportItem.Duration.Value;
			}
			
			return HiddenDuration;
		}


		public static double? HiddenDurationPercent (ActivityReportItem ParentReportItem)
		{
			double? HiddenDurationTime = HiddenDuration(ParentReportItem);
		
			if (HiddenDurationTime == null)
				return null;
				
			double TotalDurationCalc = TotalDuration(ParentReportItem.ChildReportItems);
			
			if (TotalDurationCalc > 0d)
				return HiddenDurationTime.Value / TotalDurationCalc;
		
			return null;
		}
	}
}
