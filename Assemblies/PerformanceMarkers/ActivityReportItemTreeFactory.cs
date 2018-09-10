using System.Collections.Generic;

namespace PerformanceMarkers
{
	public class ActivityReportItemTreeFactory
	{
		public static ActivityReportItem CreateReportItemTree (ActivityPointStack NormalizedPointStackParam)
		{
			//
			// WE CAN'T DO ANYTHING IF THERE ARE NO POINTS.
			//
			if (NormalizedPointStackParam.IsEmpty)
				return null;
		
			//
			// THE TOP ITEM ON THE STACK IS A START POINT.
			// SPECIFICALLY, IT IS THE MARKER START POINT.
			//
			return CreateReportItemForStartPoint(NormalizedPointStackParam.Pop(), NormalizedPointStackParam);
		}
		
		
		public static ActivityReportItem CreateReportItemForStartPoint (ActivityPoint CurrentStartPoint, ActivityPointStack NormalizedPointStackParam)
		{
			//
			// PULL THE CURRENT END POINT FROM THE STACK.
			//
			ActivityPoint EndPoint = PullActivityPointEnd(CurrentStartPoint, NormalizedPointStackParam);

			//
			// CREATE THE REPORT ITEM.
			//
			ActivityReportItem CurrentReportItem = new ActivityReportItem();
			CurrentReportItem.ActivityName = CurrentStartPoint.ActivityName;
			CurrentReportItem.StartPoint = CurrentStartPoint;
			CurrentReportItem.EndPoint = EndPoint;
			CurrentReportItem.Duration = DurationCalculator.CalcDuration(CurrentStartPoint, EndPoint);
		
			//
			// CREATE THE ARRAY OF CHILD REPORT ITEMS.
			//
			List<ActivityReportItem> ChildReportItemList = new List<ActivityReportItem>();
			
			while (NormalizedPointStackParam.IsNotEmpty)
			{
				//
				// THE NEXT POINT WILL ALWAYS BE A START POINT.
				//
				ActivityPoint NextStartPoint = NormalizedPointStackParam.Pop();
				
				//
				// BUILD THE REPORT ITEM FOR THE NEXT START POINT.
				//
				ActivityReportItem NextReportItem = CreateReportItemForStartPoint(NextStartPoint, NormalizedPointStackParam);
				
				//
				// CHECK IF THE NEXT REPORT ITEM (ACTIVITY) SHOULD BE A CHILD OF THE CURRENT ACTIVITY IF IT SHOULD BE A SIBLING OF THE CURRENT ACTIVITY.
				//
				if (CurrentReportItem.StartPoint.SequenceNumber < NextReportItem.StartPoint.SequenceNumber && NextReportItem.EndPoint.SequenceNumber < CurrentReportItem.EndPoint.SequenceNumber)
				{
					//
					// THE NEXT ACTIVITY IS A CHILD ACTIVITY.
					//
					ChildReportItemList.Add(NextReportItem);
				}
				else
				{
					//
					// THIS NEXT ACTIVITY IS A SIBLING AND WE ARE OUT OF BOUNDS FOR THE CURRENT ACTIVITY.
					// THERE ARE NO MORE CHILDREN TO BE FOUND FOR THIS ACTIVITY.
					// MAKE THE NEXT ACTIVITY A SIBLING OF THE CURRENT ONE.
					//
					CurrentReportItem.NextSiblingReportItem = NextReportItem;
					break;
				}
			}
			
			//
			// COLLECT SIBLINGS AND COMPLETE THE TREE.
			//
			List<ActivityReportItem> NextSiblingReportItemList = new List<ActivityReportItem>();
			
			foreach (ActivityReportItem ChildReportItem in ChildReportItemList)
			{
				NextSiblingReportItemList.AddRange(GetNextSiblings(ChildReportItem));
			}
			
			ChildReportItemList.AddRange(NextSiblingReportItemList);
			
			CurrentReportItem.ChildReportItems = ChildReportItemList.ToArray();
			return CurrentReportItem;
		}
		
		
		public static ActivityReportItem[] GetNextSiblings (ActivityReportItem ReportItemParam)
		{
			//
			// CREATE THE LIST OF SIBLINGS.
			//
			List<ActivityReportItem> ActivityReportItemList = new List<ActivityReportItem>();
			
			ActivityReportItem CurrentReportItem = ReportItemParam;
			
			while (CurrentReportItem.NextSiblingReportItem != null)
			{
				ActivityReportItemList.Add(CurrentReportItem.NextSiblingReportItem);
				CurrentReportItem = CurrentReportItem.NextSiblingReportItem;
			}
			
			return ActivityReportItemList.ToArray();
		}


		public static ActivityPoint PullActivityPointEnd (ActivityPoint StartPoint, ActivityPointStack NormalizedPointStackParam)
		{
			//
			// CREATE THE TEMP STACK SO WE DON'T LOSE ANY POINTS.
			//
			ActivityPointStack TempStack = new ActivityPointStack();

			try
			{
				while (NormalizedPointStackParam.IsNotEmpty)
				{
					//
					// GET THE NEXT POINT.
					//
					ActivityPoint PotentialEndPoint = NormalizedPointStackParam.Pop();
					
					if (PotentialEndPoint.PointType == ActivityPointType.End && PotentialEndPoint.ActivityName.Equals(StartPoint.ActivityName))
					{
						//
						// THIS IS AN END POINT.
						// IF THIS IS THE CORRESPONDING END POINT FOR THE START POINT THEN RETURN IT.
						// BY DOING THIS WE REMOVE IT FROM THE STACK.
						//
						return PotentialEndPoint;
					}

					//
					// THIS IS NOT A MATCH.  SAVE IT TO THE TEMP STACK.
					//
					TempStack.Push(PotentialEndPoint);
				}
				
				//
				// IF WE COULD NOT FIND AN END POINT THEN THIS IS A MAJOR ISSUE.
				// WE ASSUME THAT EVERY START POINT HAS A CORRESPONDING END.
				// THIS SHOULD NEVER HAPPEN FOR A NORMALIZED POINT STACK.
				//
				throw new EndPointNotFoundException("Could not find a corresponding end point for the activity start point.", StartPoint);
			}
			finally
			{
				//
				// PUT EVERYTHING BACK ON TO THE INPUT STACK.
				//
				TempStack.EmptyIntoStack(NormalizedPointStackParam);
			}
		}
	}
}
