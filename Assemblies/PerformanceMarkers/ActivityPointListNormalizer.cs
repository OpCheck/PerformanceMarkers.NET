using System;
using System.Collections.Generic;

namespace PerformanceMarkers
{
	/// <summary>
	/// Normalizes the list of activity points so all activities that have an explicit start point will also have and explicit end point.
	/// </summary>
	public class ActivityPointListNormalizer
	{
		public static ActivityPointStack Normalize (ActivityPoint[] ActivityPoints)
		{
			//
			// CREATE THE INPUT STACK.
			//
			ActivityPointStack SourcePointStack = new ActivityPointStack();
			
			foreach (ActivityPoint CurrentPoint in ActivityPoints)
			{
				SourcePointStack.Push(CurrentPoint);
			}
			
			//
			// IF THE STACK IS EMPTY THEN WE ARE DONE.
			//
			if (SourcePointStack.IsEmpty)
				return SourcePointStack;
			
			//
			// FLIP THE STACK TO CREATE OUR INPUT POINT STACK.
			//
			ActivityPointStack InputPointStack = SourcePointStack.Flip();
			
			//
			// THE FIRST POINT IS THE MARKER START POINT.
			// THIS IS GUARANTEED BY THE MARKER.
			//
			ActivityPoint MarkerStartPoint = InputPointStack.Pop();
			
			//
			// FIND THE CORRESPONDING END POINT FOR THE START POINT.
			//
			ActivityPoint MarkerEndPoint = GetEndPoint(MarkerStartPoint, InputPointStack);
			
			if (MarkerEndPoint == null)
			{
				//
				// NO MARKER END POINT FOUND.  INSERT IT.
				//
				InsertEndPointForMarkerStartPoint(MarkerStartPoint, InputPointStack);
			}
			
			//
			// PUSH THE MARKER START POINT ONTO THE PROCESSED STACK.
			//
			ActivityPointStack ProcessedPointStack = new ActivityPointStack();
			ProcessedPointStack.Push(MarkerStartPoint);
			
			while (InputPointStack.IsNotEmpty)
			{
				//
				// GET THE CURRENT POINT.
				//
				ActivityPoint CurrentPoint = InputPointStack.Pop();
				
				//
				// IF THIS IS AN END POINT, DO NOT LOOK FOR ANOTHER END POINT.
				// INSTEAD, PUSH IT DIRECTLY ONTO THE PROCESSED STACK AND MOVE ON TO THE NEXT POINT.
				//
				if (CurrentPoint.PointType == ActivityPointType.End)
				{
					ProcessedPointStack.Push(CurrentPoint);
					continue;
				}
				
				//
				// NOW WE KNOW THAT THE CURRENT POINT IS A START POINT.
				// GET ITS CORRESPONDING END POINT.
				//
				ActivityPoint CurrentEndPoint = GetEndPoint(CurrentPoint, InputPointStack);
				
				if (CurrentEndPoint == null)
				{
					//
					// NO END POINT FOUND.
					// INSERT IT AND 
					//
					InsertEndPointForStartPoint(CurrentPoint, InputPointStack);
				}
				
				//
				// PUSH THE CURRENT POINT ONTO THE PROCESSED STACK.
				//
				ProcessedPointStack.Push(CurrentPoint);
			}
			
			//
			// THE ACTIVITY POINTS LIST IS NOW NORMALIZED.
			// RETURN A STACK WHERE EACH POINT HAS AN ASSIGNED SEQUENCE NUMBER.
			//
			ActivityPointStack NormalizedPointStack = new ActivityPointStack();
			int PointSequenceNumber = ProcessedPointStack.Count - 1;
			
			while (ProcessedPointStack.IsNotEmpty)
			{
				ActivityPoint ProcessedPoint = ProcessedPointStack.Pop();
				ProcessedPoint.SequenceNumber = PointSequenceNumber--;
				NormalizedPointStack.Push(ProcessedPoint);
			}
			
			return NormalizedPointStack;
		}
		
		
		/// <summary>
		/// Gets the corresponding end point for the specified start point, if any.
		/// Returns null if no end point was found.
		/// </summary>
		public static ActivityPoint GetEndPoint (ActivityPoint StartPoint, ActivityPointStack InputPointStack)
		{
			ActivityPointStack TempStack = new ActivityPointStack();
			
			try
			{
				while (InputPointStack.IsNotEmpty)
				{
					//
					// GET THE END POINT CANDIDATE.
					//
					ActivityPoint EndPointCandidate = InputPointStack.Pop();
					
					//
					// SAVE IT TO THE TEMP STACK.
					//
					TempStack.Push(EndPointCandidate);

					//
					// CHECK TO SEE IF THIS MATCHES.
					//
					if (EndPointCandidate.PointType == ActivityPointType.End && EndPointCandidate.ActivityName.Equals(StartPoint.ActivityName))
						return EndPointCandidate;
				}
				
				//
				// NO END POINT FOUND.  RETURN NULL.
				//
				return null;
			}
			finally
			{
				//
				// PUT EVERYTHING BACK.
				//
				while (TempStack.Count > 0)
					InputPointStack.Push(TempStack.Pop());
			}
		}


		/// <summary>
		/// Inserts an end point for a marker start point at the appropriate place (the end).
		/// </summary>
		public static void InsertEndPointForMarkerStartPoint (ActivityPoint MarkerStartPoint, ActivityPointStack InputPointStack)
		{
			ActivityPointStack TempStack = new ActivityPointStack();
			
			//
			// EMPTY THE INPUT STACK.
			//
			while (InputPointStack.Count > 0)
				TempStack.Push(InputPointStack.Pop());

			//
			// PUSH AN ARTIFICIAL MARKER END POINT ON TO THE INPUT STACK.
			// NOTE THAT WE DO NOT SET THE POINT DATE/TIME - THIS KEEPS IT NULL.
			//
			ActivityPoint ArtificialEndPoint = new ActivityPoint();
			ArtificialEndPoint.ActivityName = MarkerStartPoint.ActivityName;
			ArtificialEndPoint.PointType = ActivityPointType.End;
			InputPointStack.Push(ArtificialEndPoint);

			//
			// PUT EVERYTHING BACK ON TO THE INPUT STACK.
			// THE MARKER NOW HAS AN END POINT, ALBEIT - AN ARTIFICIAL END, BUT AT LEAST WE CAN ALERT THE USER ABOUT THIS.
			//
			while (TempStack.Count > 0)
				InputPointStack.Push(TempStack.Pop());
		}


		/// <summary>
		/// Inserts an end point for a start point at the appropriate place.
		/// This is for non-marker start points.
		/// </summary>
		public static void InsertEndPointForStartPoint (ActivityPoint StartPoint, ActivityPointStack InputPointStack)
		{
			//
			// WE KNOW THAT THIS START POINT HAS NO EXPLICIT END POINT.
			// THEREFORE, WE MUST FIND WHERE THE END POINT IS IMPLIED AND CREATE AN EXPLICIT END POINT FOR IT.
			// THERE ARE 2 CASES WHERE THE END POINT IS IMPLIED:
			// 1. THE START OF AN ACTIVITY.
			// 2. THE END OF THE MARKER ACTIVITY.  THIS IS THE VERY LAST POINT ON THE INPUT STACK AND IS GUARANTEED TO EXIST.
			//
			
			//
			// CREATE THE TEMP STACK SO WE DON'T LOSE ANY POINTS.
			//
			ActivityPointStack TempStack = new ActivityPointStack();
			
			try
			{
				while (InputPointStack.IsNotEmpty)
				{
					//
					// GET THE NEXT POINT.
					//
					ActivityPoint PotentialEndPoint = InputPointStack.Pop();
					
					if (PotentialEndPoint.PointType == ActivityPointType.Start)
					{
						//
						// WE HAVE FOUND A START OF ANOTHER ACTIVITY.
						// THIS IS AN IMPLIED END POINT OF THE START POINT PARAMETER.
						// CREATE AN EXPLICIT, CORRESPONDING END POINT FOR THE START POINT PARAMETER.
						//
						ActivityPoint ExplicitEndPoint = new ActivityPoint();
						ExplicitEndPoint.ActivityName = StartPoint.ActivityName;
						ExplicitEndPoint.PointDateTime = PotentialEndPoint.PointDateTime;
						ExplicitEndPoint.PointType = ActivityPointType.End;
						
						//
						// PUSH THE IMPLIED END POINT BACK ONTO THE INPUT STACK.
						//
						InputPointStack.Push(PotentialEndPoint);
						
						//
						// PUSH THE EXPLICIT END POINT ONTO THE INPUT STACK.
						//
						InputPointStack.Push(ExplicitEndPoint);
						
						//
						// THE INSERT IS COMPLETE.
						//
						return;
					}
					
					//
					// THIS IS NOT AN IMPLIED END POINT.
					// SAVE THE POINT BY PUSHING IT ONTO THE TEMP STACK.
					//
					TempStack.Push(PotentialEndPoint);
				}
				
				//
				// WE HAVE EMPTIED THE INPUT STACK TRYING TO FIND AN IMPLIED END POINT.
				// WE USE THE MARKER EXPLICIT END POINT.
				// THE MARKER EXPLICIT END POINT IS AT THE TOP OF THE TEMP STACK.
				//
				ActivityPoint MarkerEndPoint = TempStack.Pop();
				
				//
				// CREATE AN EXPLICIT END POINT FOR THE START POINT.
				//
				ActivityPoint CreatedEndPoint = new ActivityPoint();
				CreatedEndPoint.ActivityName = StartPoint.ActivityName;
				CreatedEndPoint.PointDateTime = MarkerEndPoint.PointDateTime;
				CreatedEndPoint.PointType = ActivityPointType.End;
				
				//
				// PUSH THE MARKER END POINT BACK ONTO THE INPUT STACK SO IT REMAINS ON THE BOTTOM OF THE STACK.
				//
				InputPointStack.Push(MarkerEndPoint);
				
				//
				// PUSH THE EXPLICIT END POINT ONTO THE INPUT STACK.
				//
				InputPointStack.Push(CreatedEndPoint);
			}
			finally
			{
				//
				// PUT EVERYTHING BACK ON TO THE INPUT STACK.
				// THE MARKER NOW HAS AN END POINT, ALBEIT - AN ARTIFICIAL END, BUT AT LEAST WE CAN ALERT THE USER ABOUT THIS.
				//
				while (TempStack.Count > 0)
					InputPointStack.Push(TempStack.Pop());
			}
		}
	}
}
