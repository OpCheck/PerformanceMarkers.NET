using System;

namespace PerformanceMarkers
{
	/// <summary>
	/// Creates a performance marker.
	/// </summary>
	public class MarkerFactory
	{
		/// <summary>
		/// Creates a performance marker using the specified marker type enumeration value.
		/// The marker will have no name, so the caller must set the name of the marker afterward.
		/// The marker must also be started by the caller.
		/// </summary>
		public static Marker CreateMarker (MarkerType MarkerTypeParam)
		{
			return (Marker)Activator.CreateInstance(null, String.Format("PerformanceMarkers.Markers.{0}Marker", MarkerTypeParam)).Unwrap();
		}


		/// <summary>
		/// Creates a performance marker using the specified type string - which must be a valid marker type enumeration value.
		/// The marker will have no name, so the caller must set the name of the marker afterward.
		/// The marker must also be started by the caller.
		/// </summary>
		public static Marker CreateMarker (string MarkerTypeParam)
		{
			return CreateMarker(MarkerTypeParser.Parse(MarkerTypeParam));
		}


		/// <summary>
		/// Creates a performance marker using the specified marker type string - which must be a valid marker type enumeration value.
		/// The second parameter is the marker name.
		/// The marker is not started automatically - so it must also be started by the caller.
		/// </summary>
		public static Marker CreateMarkerWithTypeAndName (string MarkerTypeParam, string NameParam)
		{
			Marker CreatedMarker = CreateMarker(MarkerTypeParam);
			CreatedMarker.Name = NameParam;
			return CreatedMarker;
		}
		
		
		/// <summary>
		/// Creates, names, and automatically starts a performance marker using the specified marker type string - which must be a valid marker type enumeration value.
		/// </summary>
		public static Marker StartMarkerWithTypeAndName (string MarkerTypeParam, string NameParam)
		{
			Marker CreatedMarker = CreateMarker(MarkerTypeParam);
			CreatedMarker.Name = NameParam;
			CreatedMarker.Start();
			return CreatedMarker;
		}
	}
}
