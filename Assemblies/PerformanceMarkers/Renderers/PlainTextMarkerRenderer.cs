using System;
using System.IO;

namespace PerformanceMarkers.Renderers
{
	public class PlainTextMarkerRenderer
	{
		public void Render (Marker MarkerParam)
		{
			StreamWriter TargetWriter = new StreamWriter(_TargetStream);
			
			TargetWriter.WriteLine("-----");
			TargetWriter.WriteLine("Marker");
			TargetWriter.WriteLine("-----");
			
			foreach (ActivityPoint CurrentPoint in MarkerParam.ActivityPoints)
			{
				TargetWriter.WriteLine(String.Format("{0},{1},{2}", CurrentPoint.ActivityName, CurrentPoint.PointType, CurrentPoint.SequenceNumber));
			}
			
			TargetWriter.Flush();
		}
		
		
		public Stream TargetStream
		{
			set
			{
				_TargetStream = value;
			}
		}


		private Stream _TargetStream;
	}
}
