using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using PerformanceMarkers.Printers;

namespace PerformanceMarkers.MarkerReportFactories
{
	public class XmlReportFactory : MarkerReportFactory
	{
		public XmlReportFactory () : base()
		{
		}
		
		
		public override void WriteReport ()
		{
			//
			// IF THE MARKER IS DISABLED THEN WE DO NOT CREATE A REPORT FOR IT.
			//
			//if (CurrentMarker.IsDisabled)
				//return "<PerformanceMarkerReport></PerformanceMarkerReport>";
		
		
			//
			// NORMALIZE THE ACTIVITY POINTS SO ALL ACTIVITIES WITH A START POINT HAVE AN EXPLICIT END POINT.
			//
			ActivityPointStack NormalizedPointStack = ActivityPointListNormalizer.Normalize(_ActivityPoints);
			
			//
			// CREATE THE ACTIVITY REPORT ITEM TREE.
			//
			ActivityReportItem MarkerReportItem = ActivityReportItemTreeFactory.CreateReportItemTree(NormalizedPointStack);

			//
			// CREATE THE REPORT.
			//
			
			//
			// WRITE THE REPORT TO THE TARGET STREAM.
			//
			/*
			using (StringReader SourceStringReader = new StringReader(ReportString))
			{
				StreamWriter TargetStreamWriter = new StreamWriter(_TargetStream);
				TargetStreamWriter.Write(SourceStringReader.ReadToEnd());
				TargetStreamWriter.Flush();
			}
			*/
		}
	}
}
