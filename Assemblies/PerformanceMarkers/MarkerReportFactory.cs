using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using PerformanceMarkers.Printers;

namespace PerformanceMarkers
{
	/// <summary>
	/// Creates a human-readable performance report in a condensed text format.
	/// </summary>
	public abstract class MarkerReportFactory
	{
		public MarkerReportFactory ()
		{
			_Encoding = Encoding.UTF8;
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
				return "";
			
			//
			// CREATE THE REPORT.
			//
			using (MemoryStream TargetStream = new MemoryStream())
			{
				_ActivityPoints = CurrentMarker.ActivityPoints;
				_TargetStream = TargetStream;
				WriteReport();
				return _Encoding.GetString(TargetStream.ToArray());
			}
		}


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
				_ActivityPoints = value.ActivityPoints;
			}
		}
		
		
		/// <summary>
		/// Set to true to show sequence numbers of every activity point in the report.
		/// </summary>
		public bool? ShowSequenceNumber
		{
			set
			{
				_ShowSequenceNumber = value;
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
		protected Stream _TargetStream;
		protected bool? _ShowSequenceNumber;
		protected Encoding _Encoding;
	}
}
