using System;
using System.Text;

namespace PerformanceMarkers.Printers
{
	public class ActivityPointStackPrinter
	{
		public static string CreateLines (ActivityPointStack StackParam)
		{
			ActivityPointStack TempStack = new ActivityPointStack();
			
			try
			{
				StringBuilder PrintStringBuilder = new StringBuilder();
				PrintStringBuilder.AppendLine("ActivityName,PointDateTime,PointType,SequenceNumber");
			
				while (StackParam.IsNotEmpty)
				{
					ActivityPoint CurrentPoint = StackParam.Pop();
					PrintStringBuilder.AppendLine(CreateLine(CurrentPoint));
					TempStack.Push(CurrentPoint);
				}
				
				return PrintStringBuilder.ToString();
			}
			finally
			{
				while (TempStack.IsNotEmpty)
					StackParam.Push(TempStack.Pop());
			}
		}
		
		
		public static string CreateLine (ActivityPoint CurrentPoint)
		{
			StringBuilder LineBuilder = new StringBuilder();
			LineBuilder.Append(CurrentPoint.ActivityName);
			LineBuilder.Append(",");
			LineBuilder.Append(CurrentPoint.PointDateTime == null ? "" : CurrentPoint.PointDateTime.Value.ToString("s"));
			LineBuilder.Append(",");
			LineBuilder.Append(CurrentPoint.PointType.ToString());
			LineBuilder.Append(",");
			LineBuilder.Append(CurrentPoint.SequenceNumber.ToString());
			return LineBuilder.ToString();
		}
	}
}
