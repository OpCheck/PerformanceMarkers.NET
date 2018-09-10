using System;
using System.Collections.Generic;

namespace PerformanceMarkers
{
	public class ActivityReportItemListMap : Dictionary<string, ActivityReportItemList>
	{
		public void AddRange (IEnumerable<ActivityReportItem> ActivityReportItems)
		{
			foreach (ActivityReportItem CurrentReportItem in ActivityReportItems)
			{
				Add(CurrentReportItem);
			}
		}


		public void Add (ActivityReportItem ActivityReportItemParam)
		{
			if (!ContainsKey(ActivityReportItemParam.ActivityName))
				this[ActivityReportItemParam.ActivityName] = new ActivityReportItemList();
				
			this[ActivityReportItemParam.ActivityName].Add(ActivityReportItemParam);
		}
	}
}
