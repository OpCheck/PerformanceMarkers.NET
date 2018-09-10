using System;
using System.Text;

namespace PerformanceMarkers
{
	public class EndPointNotFoundException : Exception
	{
		public EndPointNotFoundException (string MessageParam, ActivityPoint ActivityPointParam) : base(MessageParam)
		{
			_ActivityPoint = ActivityPointParam;
		}


		public override string Message
		{
			get
			{
				StringBuilder MessageBuilder = new StringBuilder();
				MessageBuilder.AppendFormat("Could not find a corresponding end point for the activity start point with name '{0}'.", _ActivityPoint.ActivityName).AppendLine();
				MessageBuilder.AppendLine("-----");
				MessageBuilder.AppendLine("ActivityPoint");
				MessageBuilder.AppendLine("-----");
				MessageBuilder.AppendFormat("ActivityName: '{0}'.", _ActivityPoint.ActivityName).AppendLine();
				MessageBuilder.AppendFormat("PointDateTime: '{0}'.", _ActivityPoint.PointDateTime == null ? "" : _ActivityPoint.PointDateTime.Value.ToString("s")).AppendLine();
				MessageBuilder.AppendFormat("PointType: '{0}'.", _ActivityPoint.PointType).AppendLine();
				MessageBuilder.AppendFormat("SequenceNumber: '{0}'.", _ActivityPoint.SequenceNumber).AppendLine();
				return MessageBuilder.ToString();
			}
		}
		
		
		private ActivityPoint _ActivityPoint;
	}
}
