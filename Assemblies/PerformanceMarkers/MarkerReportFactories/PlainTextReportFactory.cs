using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PerformanceMarkers.MarkerReportFactories
{
	/// <summary>
	/// Creates performance reports in a condensed, human-readable, plain text format ideal for logging or just viewing.
	/// </summary>
	public class PlainTextReportFactory : MarkerReportFactory
	{
		public PlainTextReportFactory () : base()
		{
		}


		public override string CreateDisabledReport (Marker CurrentMarker)
		{
			return "";
		}
	
	
		public override void WriteReport ()
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
			// CREATE A SUMMARY ELEMENT FOR EACH CHILD ACCORDING TO THE SPECIFIED CONFIGURATION.
			//
			ActivityReportItemListMap CreatedListMap = new ActivityReportItemListMap();
			CreatedListMap.AddRange(ParentReportItem.ChildReportItems);
			string[] ChildActivityNames = ActivityNamesArrayFactory.CreateArrayOfUniqueActivityNames(ParentReportItem.ChildReportItems);

			foreach (string ChildActivityName in ActivityNamesArrayFactory.CreateArrayOfUniqueActivityNames(ParentReportItem.ChildReportItems))
			{
				//
				// GET THE LIST OF CHILD ACTIVITIES.
				//
				ActivityReportItemList ChildActivityList = CreatedListMap[ChildActivityName];
				
				if (_ShowAllSummaries || ChildActivityList.Count > 1)
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
				if (_ShowAllActivities || CurrentChildReportItem.ChildReportItems.Length > 0)
				{
					//
					// SHOW THE CHILD ACTIVITY IF IT'S FORCED TO BE SHOWN OR IF IT'S A PARENT ITSELF.
					//
					ReportBuilder.Append(CreateReportForActivityReportItem(CurrentChildReportItem, NestingLevel + 1));
				}
			}
			
			return ReportBuilder.ToString();
		}


		public string CreateLineItemForAggregate (ActivityReportAggregateItem AggregateItemParam, int NestingLevel)
		{
			StringBuilder LineItemBuilder = new StringBuilder();
			
			//
			// APPLY INDENTATION.
			//
			for (int i = 0; i < NestingLevel; i++)
				LineItemBuilder.Append("\t");

			//
			// ACTIVITY NAME.
			//
			LineItemBuilder.AppendFormat("+ {0} [", AggregateItemParam.ActivityName);

			//
			// COUNT.
			//
			LineItemBuilder.AppendFormat("count: {0},", AggregateItemParam.Count.ToString(MarkerReportFactoryDefaults.CountDisplayFormatCode));
			
			//
			// TOTAL DURATION.
			//
			LineItemBuilder.AppendFormat(" total: {0} ms", AggregateItemParam.TotalDuration.Value.ToString(MarkerReportFactoryDefaults.TimingDisplayFormatCode));

			//
			// IF THE COUNT IS 1 - MEANING THERE WAS JUST 1 CHILD ACTIVITY, THEN JUST SHOW THE TOTAL TIME.
			// IT MAKES NO SENSE TO SHOW ANYTHING ELSE BECAUSE IT'S ALL THE SAME NUMBER.
			//
			if (AggregateItemParam.Count == 1)
			{
				LineItemBuilder.Append("]");
			}
			else
			{
				LineItemBuilder.Append(",");

				//
				// AVERAGE.
				//
				LineItemBuilder.AppendFormat(" avg: {0},", AggregateItemParam.AvgDuration.Value.ToString(MarkerReportFactoryDefaults.TimingDisplayFormatCodeForAverages));


				//
				// MAX.
				//
				LineItemBuilder.AppendFormat(" max: {0},", AggregateItemParam.MaxDuration.Value.ToString(MarkerReportFactoryDefaults.TimingDisplayFormatCode));

				//
				// MIN.
				//
				LineItemBuilder.AppendFormat(" min: {0}]", AggregateItemParam.MinDuration.Value.ToString(MarkerReportFactoryDefaults.TimingDisplayFormatCode));
			}

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
			if (_ShowSequenceNumbers)
				LineItemBuilder.AppendFormat("{0}. ", ActivityReportItemParam.StartPoint.SequenceNumber);
			
			//
			// ACTIVITY NAME.
			//
			LineItemBuilder.Append(ActivityReportItemParam.ActivityName);
			
			//
			// TOTAL DURATION.
			//
			LineItemBuilder.AppendFormat(" [total: {0} ms].", ActivityReportItemParam.Duration == null ? "?" : ActivityReportItemParam.Duration.Value.TotalMilliseconds.ToString(MarkerReportFactoryDefaults.TimingDisplayFormatCode));
			
			return LineItemBuilder.ToString();
		}
	}
}
