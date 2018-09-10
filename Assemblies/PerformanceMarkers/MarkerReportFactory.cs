using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using PerformanceMarkers.Printers;

namespace PerformanceMarkers
{
	/// <summary>
	/// Creates a human-readable performance report.
	/// </summary>
	public class MarkerReportFactory
	{
		public static string CreateReport (Marker CurrentMarker)
		{
			using (MemoryStream TargetStream = new MemoryStream())
			{
				MarkerReportFactory CreatedFactory = new MarkerReportFactory();
				CreatedFactory.PerformanceMarker = CurrentMarker;
				CreatedFactory.TargetStream = TargetStream;
				CreatedFactory.WriteReport();
				
				return Encoding.UTF8.GetString(TargetStream.ToArray());
			}
		}
	
	
		public void WriteReport ()
		{
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
			string ReportString = CreateReportForActivityReportItem(MarkerReportItem, 0);
			
			//
			// WRITE THE REPORT TO THE TARGET STREAM.
			//
			using (StringReader SourceStringReader = new StringReader(ReportString))
			{
				StreamWriter TargetStreamWriter = new StreamWriter(_TargetStream);
				TargetStreamWriter.Write(SourceStringReader.ReadToEnd());
				TargetStreamWriter.Flush();
			}
		}


		public string CreateReportForActivityReportItem (ActivityReportItem ParentReportItem, int NestingLevel)
		{
			//
			// CREATE THE REPORT LINE ITEM FOR THE PARENT.
			//
			StringBuilder ReportBuilder = new StringBuilder();
			ReportBuilder.AppendLine(CreateReportLineItem(ParentReportItem, NestingLevel));
		
			//
			// CREATE A SUMMARY LINE ITEM FOR EACH CHILD.
			//
			ActivityReportItemListMap CreatedListMap = new ActivityReportItemListMap();
			CreatedListMap.AddRange(ParentReportItem.ChildReportItems);

			foreach (string ChildActivityName in ActivityNamesArrayFactory.CreateArrayOfUniqueActivityNames(ParentReportItem.ChildReportItems))
			{
				//
				// GET THE LIST OF CHILD ACTIVITIES.
				//
				ActivityReportItemList ChildActivityList = CreatedListMap[ChildActivityName];
				
				if (ChildActivityList.Count > 1)
				{
					//
					// CREATE THE AGGREGATE RECORD.
					//
					ActivityReportAggregateItem CreatedAggregateItem = new ActivityReportAggregateItem();
					CreatedAggregateItem.ActivityName = ChildActivityName;
					CreatedAggregateItem.Count = ChildActivityList.Count;
					CreatedAggregateItem.TotalDuration = ActivityReportItemCalculator.TotalDuration(ChildActivityList);
					CreatedAggregateItem.MaxDuration = ActivityReportItemCalculator.MaxDuration(ChildActivityList);
					CreatedAggregateItem.AvgDuration = ActivityReportItemCalculator.AvgDuration(ChildActivityList);
					CreatedAggregateItem.MinDuration = ActivityReportItemCalculator.MinDuration(ChildActivityList);
					
					//
					// CREATE THE LINE ITEM FOR THE AGGREGATE RECORD.
					//
					ReportBuilder.AppendLine(CreateLineItemForAggregate(CreatedAggregateItem, NestingLevel + 1));
				}
			}
		
			//
			// CREATE THE REPORT LINE ITEMS FOR THE CHILDREN.
			//
			foreach (ActivityReportItem CurrentChildReportItem in ParentReportItem.ChildReportItems)
			{
				ReportBuilder.Append(CreateReportForActivityReportItem(CurrentChildReportItem, NestingLevel + 1));
			}
			
			return ReportBuilder.ToString();
		}


		public string CreateLineItemForAggregate (ActivityReportAggregateItem AggregateItemParam, int NestingLevel)
		{
			//
			// 6. CopySectorFactor: summary, count: 42, avg: 2ms, max: 40 ms, min: 0 ms, total: 44 ms.
			//
			StringBuilder LineItemBuilder = new StringBuilder();
			
			//
			// APPLY INDENTATION.
			//
			for (int i = 0; i < NestingLevel; i++)
				LineItemBuilder.Append("\t");

			//
			// ACTIVITY NAME.
			//
			LineItemBuilder.AppendFormat("+ {0}: [", AggregateItemParam.ActivityName);
			
			//
			// TOTAL DURATION.
			//
			LineItemBuilder.AppendFormat("total: {0} ms,", AggregateItemParam.TotalDuration.Value.TotalMilliseconds.ToString("N0"));

			//
			// AVERAGE.
			//
			LineItemBuilder.AppendFormat(" avg: {0} ms,", AggregateItemParam.AvgDuration.Value.TotalMilliseconds.ToString("N0"));

			//
			// COUNT.
			//
			LineItemBuilder.AppendFormat(" count: {0},", AggregateItemParam.Count);

			//
			// MAX.
			//
			LineItemBuilder.AppendFormat(" max: {0} ms,", AggregateItemParam.MaxDuration.Value.TotalMilliseconds.ToString("N0"));

			//
			// MIN.
			//
			LineItemBuilder.AppendFormat(" min: {0} ms]", AggregateItemParam.MinDuration.Value.TotalMilliseconds.ToString("N0"));

			return LineItemBuilder.ToString();
		}
		
		
		public string CreateReportLineItem (ActivityReportItem ActivityReportItemParam, int NestingLevel)
		{
			StringBuilder LineItemBuilder = new StringBuilder();
			
			//
			// INDENT.
			//
			for (int i = 0; i < NestingLevel; i++)
				LineItemBuilder.Append("\t");
			
			//
			// SEQUENCE NUMBER.
			//
			if (_ShowSequenceNumber != null && _ShowSequenceNumber.Value)
				LineItemBuilder.AppendFormat("{0}. ", ActivityReportItemParam.StartPoint.SequenceNumber);
			
			//
			// ACTIVITY NAME.
			//
			LineItemBuilder.Append(ActivityReportItemParam.ActivityName);
			LineItemBuilder.Append(": ");
			
			//
			// DURATION.
			//
			LineItemBuilder.Append(ActivityReportItemParam.Duration == null ? "?" : ActivityReportItemParam.Duration.Value.TotalMilliseconds.ToString("N0"));
			LineItemBuilder.Append(" ms.");
			
			return LineItemBuilder.ToString();
		}
		
		
		public Marker PerformanceMarker
		{
			set
			{
				_ActivityPoints = value.ActivityPoints;
			}
		}
		
		
		public bool? ShowSequenceNumber
		{
			set
			{
				_ShowSequenceNumber = value;
			}
		}
		
		
		public Stream TargetStream
		{
			set
			{
				_TargetStream = value;
			}
		}


		//
		// INPUT FIELDS.
		//	
		private ActivityPoint[] _ActivityPoints;
		private Stream _TargetStream;
		private bool? _ShowSequenceNumber;
	}
}
