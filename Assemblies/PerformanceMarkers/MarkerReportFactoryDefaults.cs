using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceMarkers
{
	public class MarkerReportFactoryDefaults
	{
		public static string TimingDisplayFormatCode
		{
			get
			{
				return "#,0.0";
			}
		}


		public static string TimingDisplayFormatCodeForAverages
		{
			get
			{
				return "#,0.000";
			}
		}


		public static string CountDisplayFormatCode
		{
			get
			{
				return "#,#";
			}
		}
	}
}
