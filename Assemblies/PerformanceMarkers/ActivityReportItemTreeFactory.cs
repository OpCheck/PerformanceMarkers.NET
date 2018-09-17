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
				// CHECK IF THE NEXT START POINT OCCURS AFTER THE END POINT OF THIS REPORT ITEM.
				//
				if (CurrentReportItem.EndPoint.SequenceNumber < NextStartPoint.SequenceNumber)
				{
					//
					// THE NEXT START POINT OCCURS AFTER THE CURRENT REPORT ITEM ENDS.
					// WE ARE DONE BUILDING THE LIST OF CHILD ACTIVITY ITEMS.
					//
					
					//
					// PUT THE NEXT START POINT BACK ON THE STACK.
					//
					NormalizedPointStackParam.Push(NextStartPoint);
					
					//
					// THE LIST OF CHILD ACTIVITY ITEMS IS NOW COMPLETE.  GET OUT OF HERE.
					//
					break;
				}
				
				// 
				// BUILD THE REPORT ITEM FOR THE NEXT START POINT.
				// THIS IS ALWAYS A CHILD REPORT ITEM.
				//
				ActivityReportItem NextReportItem = CreateReportItemForStartPoint(NextStartPoint, NormalizedPointStackParam);
				ChildReportItemList.Add(NextReportItem);
			}
			
			CurrentReportItem.ChildReportItems = ChildReportItemList.ToArray();
			
			//
			// NOW THAT THIS REPORT ITEM HAS ALL OF ITS CHILDREN WE CAN CALCULATE THE HIDDEN PROCESSING TIME.
			//
			CurrentReportItem.HiddenDuration = ActivityReportItemCalculator.HiddenDuration(CurrentReportItem);
			
			return CurrentReportItem;
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
