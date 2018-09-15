using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using PerformanceMarkers.Printers;

namespace PerformanceMarkers
{
	/// <summary>
	/// Creates a performance report in a string representation.
	/// This could be plain text, XML or JSON.
	/// This class is not thread-safe.  Do not share instances of it between threads.
	/// </summary>
	public abstract class MarkerReportFactory
	{
		public MarkerReportFactory ()
		{
			_Encoding = Encoding.UTF8;
			_ShowAllSummaries = true;
			_ShowAllActivities = false;
			_ShowSequenceNumbers = false;
		}


		/// <summary>
		/// A convenience method for creating a report as a string.
		/// All this method does is write to a memory stream and return it as a UTF8 string.
		/// Callers can also write directly to any stream by setting the TargetStream property and calling the WriteReport method.
		/// </summary>
		public virtual string CreateReport (Marker CurrentMarker)
		{
			//
			// IF THIS MARKER IS DISABLED THEN WE DO NOT CREATE A REPORT FOR IT.
			//
			if (CurrentMarker.IsDisabled)
				return CreateDisabledReport(CurrentMarker);
			
			//
			// CREATE THE REPORT.
			//
			using (MemoryStream TargetStream = new MemoryStream())
			{
				PerformanceMarker = CurrentMarker;
				_TargetStream = TargetStream;
				WriteReport();
				return _Encoding.GetString(TargetStream.ToArray());
			}
		}
		
		
		public abstract string CreateDisabledReport (Marker CurrentMarker);


		/// <summary>
		/// Creates and writes the report to the target stream.
		/// </summary>
		public abstract void WriteReport ();
		
		
		/// <summary>
		/// Sets the performance marker to use for report generation.
		/// </summary>
		public Marker PerformanceMarker
		{
			set
			{
				_Marker = value;
				_ActivityPoints = value.ActivityPoints;
			}
		}
		
		
		/// <summary>
		/// Set to true to show sequence numbers of every activity point in the report.
		/// By default, we do not show sequence numbers.
		/// </summary>
		public bool ShowSequenceNumbers
		{
			set
			{
				_ShowSequenceNumbers = value;
			}
		}


		/// <summary>
		/// Set to true to always show a summary for every child activity point.
		/// By default, this is true.
		/// </summary>
		public bool ShowAllSummaries
		{
			set
			{
				_ShowAllSummaries = value;
			}
		}


		/// <summary>
		/// Set to true to always show every child activity point.
		/// By default, this is false.
		/// </summary>
		public bool ShowAllActivities
		{
			set
			{
				_ShowAllActivities = value;
			}
		}
		
		
		/// <summary>
		/// Sets the stream to write the report to.
		/// </summary>
		public Stream TargetStream
		{
			set
			{
				_TargetStream = value;
			}
		}


		/// <summary>
		/// Sets the text encoding to use for the report.
		/// The default is UTF8.
		/// </summary>
		public Encoding Encoding
		{
			set
			{
				_Encoding = value;
			}
		}


		//
		// INPUT FIELDS.
		//	
		protected ActivityPoint[] _ActivityPoints;
		protected Marker _Marker;
		protected Stream _TargetStream;
		protected bool _ShowSequenceNumbers;
		protected bool _ShowAllSummaries;
		protected bool _ShowAllActivities;
		protected Encoding _Encoding;
	}
}
