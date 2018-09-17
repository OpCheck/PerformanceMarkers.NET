namespace PerformanceMarkers
{
	/// <summary>
	/// The official marker configuration reference.
	/// Generally, this should be managed by a configurator object.
	/// The configuration provider relies on these values to generate configuration objects.
	/// </summary>
	public class MarkerConfigReference
	{
		static MarkerConfigReference ()
		{
			//
			// SET SYSTEM DEFAULTS.
			//
			Type = MarkerType.Enabled;
			FailureMode = MarkerFailureMode.HighlyVisible;
			ReportFactoryType = MarkerReportFactoryType.PlainText;
		}
	
		public static MarkerType Type;
		public static MarkerFailureMode FailureMode;
		public static MarkerReportFactoryType ReportFactoryType;
	}
}
