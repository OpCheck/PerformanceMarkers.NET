namespace PerformanceMarkers
{
	/// <summary>
	/// The official marker configuration reference.
	/// Generally, this should be managed by a configurator object.
	/// The configuration provider relies on these values to generate configuration objects.
	/// </summary>
	public class MarkerConfigReference
	{
		/// <summary>
		/// Sets the initial system defaults.
		/// The initial system defaults are targeted for development environments.
		/// </summary>
		static MarkerConfigReference ()
		{
			//
			// SET SYSTEM DEFAULTS.
			//
			MarkerConfig = new MarkerConfig();
			MarkerConfig.Type = MarkerType.Enabled;
			MarkerConfig.FailureMode = MarkerFailureMode.HighlyVisible;
			MarkerConfig.ReportFactoryType = MarkerReportFactoryType.PlainText;
		}

	
		/// <summary>
		/// The "official" configuration object.
		/// This object is managed by the configuration provider.
		/// All requests for this reference should go through the provider.
		/// </summary>
		public static MarkerConfig MarkerConfig;
	}
}
