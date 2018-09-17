namespace PerformanceMarkers
{
	public class MarkerConfigProvider
	{
		public static MarkerConfig GetMarkerConfig ()
		{
			MarkerConfig CreatedConfig = new MarkerConfig();
			CreatedConfig.Type = MarkerConfigReference.Type;
			CreatedConfig.FailureMode = MarkerConfigReference.FailureMode;
			CreatedConfig.ReportFactoryType = MarkerConfigReference.ReportFactoryType;
			return CreatedConfig;
		}
	}
}
