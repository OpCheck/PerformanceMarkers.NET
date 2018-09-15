namespace PerformanceMarkers
{
	/// <summary>
	/// Describes the behavior of the performance marker system when it encounters an error it cannot recover from.
	/// </summary>
	public enum MarkerFailureMode
	{
		/// <summary>
		/// Throw exceptions or otherwise do anything it can to alert the calling system that there is an error.
		/// Use this mode during development or when system disruption is desired or accepted.
		/// </summary>
		HighlyVisible,
		

		/// <summary>
		/// Do not throw exceptions or disrupt the function of the calling system.
		/// Instead, the marker reports will list these errors if the marker is enabled.
		/// </summary>
		Subtle,
		
		
		/// <summary>
		/// Do not throw exceptions or disrupt the function of the calling system.
		/// Do not report the error at all.
		/// </summary>
		CompletelyHidden
	}
}
