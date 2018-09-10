using System;

namespace PerformanceMarkers
{
	/// <summary>
	/// A parser for marker type enumeration values.
	/// </summary>
	public class MarkerTypeParser
	{
		/// <summary>
		/// Parses the marker type string parameter and returns a valid marker type enum value.
		/// </summary>
		public static MarkerType Parse (string MarkerTypeParam)
		{
			return (MarkerType)Enum.Parse(typeof(MarkerType), MarkerTypeParam);
		}
	}
}
