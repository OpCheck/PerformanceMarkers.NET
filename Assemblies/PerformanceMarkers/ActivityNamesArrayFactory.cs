using System;
using System.Collections.Generic;

namespace PerformanceMarkers
{
	public class ActivityNamesArrayFactory
	{
		public static string[] CreateArrayOfUniqueActivityNames (ActivityReportItem[] ActivityReportItems)
		{	
			List<string> ActivityNameList = new List<string>();
			
			foreach (ActivityReportItem CurrentItem in ActivityReportItems)
			{
				if (!ActivityNameList.Contains(CurrentItem.ActivityName))
					ActivityNameList.Add(CurrentItem.ActivityName);
			}
			
			return ActivityNameList.ToArray();
		}
	}
}
