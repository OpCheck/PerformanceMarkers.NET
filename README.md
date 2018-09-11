# PerformanceMarkers.NET

Integrated performance metrics for building the fastest .NET applications known to man.

## Usage

	using PerformanceMarkers;

	//
 	// CREATE THE PERFORMANCE MARKER FOR THIS PROCESS.
	//
	Marker CreatedMarker = MarkerFactory.StartMarker("ProcessName");

	//
 	// RUN A DATABASE QUERY OR DO SOME NETWORK I/O.
	//
	CreatedMarker.Start("Query");

	DatabaseQuery.Run();

	CreatedMarker.End("Query");

	//
	// END THE PERFORMANCE MARKER FOR THIS PROCESS.
	//
	CreatedMarker.End();

	//
	// CREATE THE PERFORMANCE REPORT.
	//
	string PerformanceReport = MarkerReportFactory.CreateReport(CreatedMarker);

	//
	// OUTPUT THE REPORT WHEREVER YOU WANT.  IT'S JUST A STRING.
	//
	Console.WriteLine(PerformanceReport);

## Sample Report

Here is a sample report from a database-intensive financial analytics program: 

	RanksApp: 163,266 ms.
		+ BatchCommit: [total: 21,734 ms, avg: 5,434 ms, count: 4, max: 6,040 ms, min: 5,185 ms]
		MaxRankIdQuery: 153 ms.
		AllCompaniesQuery: 2,098 ms.
		BatchCommit: 5,185 ms.
		BatchCommit: 5,256 ms.
		BatchCommit: 5,252 ms.
		BatchCommit: 6,040 ms.
		LastBatchCommit: 4,164 ms.
