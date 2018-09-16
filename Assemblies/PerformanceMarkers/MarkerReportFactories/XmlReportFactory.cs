using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace PerformanceMarkers.MarkerReportFactories
{
	public class XmlReportFactory : MarkerReportFactory
	{
		public XmlReportFactory () : base()
		{
		}


		/// <summary>
		/// Creates an empty report to be returned when the marker is disabled.
		/// </summary>
		public override string CreateDisabledReport (Marker CurrentMarker)
		{
			XmlDocument EmptyDocument = new XmlDocument();
			EmptyDocument.LoadXml("<Activity></Activity>");
			return EmptyDocument.OuterXml;
		}
		
		
		/// <summary>
		/// Creates a report for the performance marker.
		/// </summary>
		public override void WriteReport ()
		{
			//
			// CREATE THE REPORT XML DOCUMENT.
			//
			_MarkerReportDocument = new XmlDocument();
		
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
			_MarkerReportDocument.AppendChild(CreateActivityReportElement(MarkerReportItem));

			//
			// WRITE THE REPORT TO THE TARGET STREAM.
			//
			_MarkerReportDocument.Save(_TargetStream);
		}


		/// <summary>
		/// Creates the activity report element.
		/// </summary>
		public XmlElement CreateActivityReportElement (ActivityReportItem ParentReportItem)
		{
			//
			// CREATE THE ACTIVITY ELEMENT.
			//
			XmlElement ActivityReportElement = _MarkerReportDocument.CreateElement("Activity");
			
			//
			// ACTIVITY NAME.
			//
			if (ParentReportItem.ActivityName != null)
				ActivityReportElement.SetAttribute("Name", ParentReportItem.ActivityName);
			
			//
			// DURATION.
			//
			if (ParentReportItem.Duration != null)
				ActivityReportElement.SetAttribute("Duration", ParentReportItem.Duration.Value.TotalMilliseconds.ToString());
			
			//
			// DESCRIPTION.
			//
			if (ParentReportItem.Desc != null)
			{
				XmlElement CreatedElement = _MarkerReportDocument.CreateElement("Desc");
				CreatedElement.InnerText = ParentReportItem.Desc;
				ActivityReportElement.AppendChild(CreatedElement);
			}
			
			//
			// CREATE A SUMMARY ELEMENT FOR EACH CHILD ACCORDING TO THE SPECIFIED CONFIGURATION.
			//
			ActivityReportItemListMap CreatedListMap = new ActivityReportItemListMap();
			CreatedListMap.AddRange(ParentReportItem.ChildReportItems);
			string[] ChildActivityNames = ActivityNamesArrayFactory.CreateArrayOfUniqueActivityNames(ParentReportItem.ChildReportItems);

			//
			// CREATE THE CHILD ACTIVITY SUMMARIES ELEMENT.
			//
			XmlElement ChildActivitySummariesElement = null;

			if (_ShowAllSummaries)
			{
				//
				// FORCE SHOW ALL SUMMARIES.
				// CREATE THE CHILD ACTIVITY SUMMARIES ELEMENT.
				//
				ChildActivitySummariesElement = _MarkerReportDocument.CreateElement("ChildActivitySummaries");
				ChildActivitySummariesElement.SetAttribute("Count", ChildActivityNames.Length.ToString());
				ActivityReportElement.AppendChild(ChildActivitySummariesElement);
			}
			else
			{
				//
				// DO NOT FORCE SHOW ALL SUMMARIES.
				// BY DEFAULT, THIS CREATE A SUMMARY FOR ANY CHILD ACTIVITY WITH 2 OR MORE OCCURRENCES.
				//
			
				//
				// COUNT THE NUMBER OF CHILD ACTIVITY SUMMARIES WITH MULTIPLE CHILDREN.
				//
				int ChildActivitySummariesWithMultipleChildrenCount = 0;
				
				foreach (string ChildActivityName in ChildActivityNames)
				{
					//
					// GET THE LIST OF CHILD ACTIVITIES.
					//
					ChildActivitySummariesWithMultipleChildrenCount += CreatedListMap[ChildActivityName].Count > 1 ? 1 : 0;
				}
				
				if (ChildActivitySummariesWithMultipleChildrenCount > 0)
				{
					ChildActivitySummariesElement = _MarkerReportDocument.CreateElement("ChildActivitySummaries");
					ChildActivitySummariesElement.SetAttribute("Count", ChildActivitySummariesWithMultipleChildrenCount.ToString());
					ActivityReportElement.AppendChild(ChildActivitySummariesElement);
				}
			}

			//
			// CREATE A SUMMARY RECORD FOR EACH CHILD ACTIVITY.
			//
			foreach (string ChildActivityName in ChildActivityNames)
			{
				//
				// GET THE LIST OF CHILD ACTIVITIES.
				//
				ActivityReportItemList ChildActivityList = CreatedListMap[ChildActivityName];
				
				//
				// CREATE THE AGGREGATE RECORD.
				//
				if (_ShowAllSummaries || ChildActivityList.Count > 1)
				{
					ActivityReportAggregateItem CreatedAggregateItem = new ActivityReportAggregateItem();
					CreatedAggregateItem.ActivityName = ChildActivityName;
					CreatedAggregateItem.Count = ChildActivityList.Count;
					CreatedAggregateItem.TotalDuration = ActivityReportItemCalculator.TotalDuration(ChildActivityList);
					CreatedAggregateItem.MaxDuration = ActivityReportItemCalculator.MaxDuration(ChildActivityList);
					CreatedAggregateItem.AvgDuration = ActivityReportItemCalculator.AvgDuration(ChildActivityList);
					CreatedAggregateItem.MinDuration = ActivityReportItemCalculator.MinDuration(ChildActivityList);
					
					//
					// CREATE THE SUMMARY ELEMENT FOR THE AGGREGATE RECORD.
					//
					XmlElement CreatedChildActivitySummaryElement = CreateActivityReportAggregateElementWithName(CreatedAggregateItem);
					
					//
					// ADD THE SUMMARY.
					//
					ChildActivitySummariesElement.AppendChild(CreatedChildActivitySummaryElement);
				}
			}
		
			//
			// COUNT THE NUMBER OF CHILD ACTIVITIES THAT ARE PARENTS OF CHILDREN THEMSELVES.
			//
			/*
			int ChildActivitiesThatAreParentsAlsoCount = 0;
			
			foreach (ActivityReportItem[] ChildReportItem in ParentReportItem.ChildReportItems)
			{
				ChildActivitiesThatAreParentsAlsoCount += ChildReportItem.ChildReportItems.Length > 0 ? 1 : 0;
			}
			*/
		
			//
			// CREATE THE CHILD ACTIVITIES ELEMENT.
			//
			XmlElement ChildActivitiesElement = _MarkerReportDocument.CreateElement("ChildActivities");
			//ChildActivitiesElement.SetAttribute("Count", ParentReportItem.ChildReportItems.Length.ToString());
			ActivityReportElement.AppendChild(ChildActivitiesElement);

			//
			// CREATE THE ITEMS FOR THE CHILDREN.
			//
			foreach (ActivityReportItem CurrentChildReportItem in ParentReportItem.ChildReportItems)
			{
				if (_ShowAllActivities || CurrentChildReportItem.ChildReportItems.Length > 0)
				{
					//
					// CREATE AND ADD THE CHILD REPORT ITEM TO THE CHILD ACTIVITIES ELEMENT
					//
					ChildActivitiesElement.AppendChild(CreateActivityReportElement(CurrentChildReportItem));
				}
			}
			
			return ActivityReportElement;
		}
		
		
		private XmlElement CreateActivityPointElementWithName (ActivityPoint ActivityPointParam, string ElementNameParam)
		{
			XmlElement ActivityPointElement = _MarkerReportDocument.CreateElement(ElementNameParam);
			
			//
			// ACTIVITY NAME.
			//
			if (ActivityPointParam.ActivityName != null)
				ActivityPointElement.SetAttribute("ActivityName", ActivityPointParam.ActivityName);
			
			//
			// POINT DATE/TIME.
			//
			if (ActivityPointParam.PointDateTime != null)
			{
				XmlElement CreatedElement = _MarkerReportDocument.CreateElement("PointDateTime");
				CreatedElement.InnerText = ActivityPointParam.PointDateTime.Value.ToString("s");
				ActivityPointElement.AppendChild(CreatedElement);
			}

			//
			// POINT TYPE.
			//
			{
				XmlElement CreatedElement = _MarkerReportDocument.CreateElement("PointType");
				CreatedElement.InnerText = ActivityPointParam.PointType.ToString();
				ActivityPointElement.AppendChild(CreatedElement);
			}

			//
			// SEQUENCE NUMBER.
			//
			{
				XmlElement CreatedElement = _MarkerReportDocument.CreateElement("SequenceNumber");
				CreatedElement.InnerText = ActivityPointParam.SequenceNumber.ToString();
				ActivityPointElement.AppendChild(CreatedElement);
			}
			
			return ActivityPointElement;
		}


		private XmlElement CreateActivityReportAggregateElementWithName (ActivityReportAggregateItem AggregateItemParam)
		{
			//
			// CREATE THE ACTIVITY ELEMENT.
			//
			XmlElement ActivityReportAggregateElement = _MarkerReportDocument.CreateElement("ActivitySummary");
			
			//
			// ACTIVITY NAME.
			//
			if (AggregateItemParam.ActivityName != null)
				ActivityReportAggregateElement.SetAttribute("Name", AggregateItemParam.ActivityName);
			
			//
			// COUNT.
			//
			ActivityReportAggregateElement.SetAttribute("Count", AggregateItemParam.Count.ToString(MarkerReportFactoryDefaults.CountDisplayFormatCode));

			//
			// TOTAL DURATION.
			//
			if (AggregateItemParam.TotalDuration != null)
				ActivityReportAggregateElement.SetAttribute("TotalDuration", AggregateItemParam.TotalDuration.Value.ToString(MarkerReportFactoryDefaults.TimingDisplayFormatCode));

			//
			// MAX DURATION.
			//
			if (AggregateItemParam.MaxDuration != null)
				ActivityReportAggregateElement.SetAttribute("MaxDuration", AggregateItemParam.MaxDuration.Value.ToString(MarkerReportFactoryDefaults.TimingDisplayFormatCode));

			//
			// AVERAGE DURATION.
			//
			if (AggregateItemParam.AvgDuration != null)
				ActivityReportAggregateElement.SetAttribute("AvgDuration", AggregateItemParam.AvgDuration.Value.ToString(MarkerReportFactoryDefaults.TimingDisplayFormatCodeForAverages));

			//
			// MIN DURATION.
			//
			if (AggregateItemParam.MinDuration != null)
				ActivityReportAggregateElement.SetAttribute("MinDuration", AggregateItemParam.MinDuration.Value.ToString(MarkerReportFactoryDefaults.TimingDisplayFormatCode));
			
			return ActivityReportAggregateElement;
		}


		private XmlDocument _MarkerReportDocument;
	}
}
