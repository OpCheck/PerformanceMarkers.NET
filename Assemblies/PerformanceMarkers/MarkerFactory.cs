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
		/// Creates and automatically starts a performance marker.
		/// </summary>
		public static Marker StartMarker (string NameParam)
		{
			Marker CreatedMarker = CreateMarker(NameParam);
			CreatedMarker.Start();
			return CreatedMarker;
		}
		

		/// <summary>
		/// Creates a marker configured by the marker config provider.
		/// </summary>
		public static Marker CreateMarker (string NameParam)
		{
			//
			// GET THE CONFIGURATION.
			//
			MarkerConfig CurrentMarkerConfig = MarkerConfigProvider.GetMarkerConfig();
		
			//
			// CREATE THE MARKER.
			//
			Marker CreatedMarker = CreateMarker(CurrentMarkerConfig.Type);
			CreatedMarker.Name = NameParam;
			CreatedMarker.Config = CurrentMarkerConfig;
			CreatedMarker.Init();
			return CreatedMarker;
		}
	}
}
