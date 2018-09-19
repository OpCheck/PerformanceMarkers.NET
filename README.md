# PerformanceMarkers.NET

A lightweight, extremely easy-to-use performance optimization library for .NET that helps you:

* precisely determine where the bottlenecks are and where "hidden" processing time occurs in your application.  This  performance optimization subprocess is called the **Exposure Phase**.
* prove (or disprove) the performance impact of a change - or set of changes.  This is called **Tuning Verification**.
* methodically increase your application's performance over time to reach its speed limits as a fundamental part of your software development process.  This is called **Convergence**.

How you implement this strategy for your application is simple: performance collection statements are inserted directly into your code - very similar to how you do logging.  In effect, measuring performance becomes a function of the application itself.  This is called the **Integrated Performance Metrics Approach**.

We believe that collecting these metrics should be a fundamental part of the software development process due to the enormously positive quality impact it has.

Since the performance metrics collection is integrated into your program - and at some point your program may be deployed to a production environment - we provide ways to disable this library.  This makes it similar to a logging framework where you want it to be less "chatty" in production scenarios.

**PerformanceMarkers.NET** is already used in production environments across several industries (credit card processing, financial analytics) for the most demanding applications that have to be measured in milliseconds.

## Usage

Let's jump right in and show you some code, with some comments to explain what is going on.  We will go into much further detail below.

	//
	// REFERENCE THE PERFORMANCE MARKERS NAMESPACE.
	//
	using PerformanceMarkers;

	//
 	// CREATE A PERFORMANCE MARKER NAMED 'MyProcessName'.
	//
	Marker CreatedMarker = MarkerFactory.StartMarker("ProcessName");

	//
 	// START TIMING AN ACTIVITY NAMED 'Query'.
	//
	CreatedMarker.Start("Query");

	//
	// DO THE ACTIVITY YOU WANT TO TIME.
	//
	Thread.Sleep(577);

	//
	// STOP TIMING THE ACTIVITY.
	//
	CreatedMarker.End("Query");

	//
	// STOP TIMING THE PROCESS.
	//
	CreatedMarker.End();

	//
	// CREATE A PERFORMANCE REPORT THAT WILL BE EASILY-READABLE BY HUMANS.
	//
	string PerformanceReport = MarkerReportFactoryProvider.CreateReportFactory().CreateReport(CreatedMarker);

	//
	// VIEW THE REPORT BY LOGGING IT OR DUMPING IT TO THE CONSOLE.
	//
	Console.WriteLine(PerformanceReport);

## Sample Report

Here is a sample report from a database-intensive financial analytics program that summaries about 100,000 activities:

	RanksApp [total: 164,655.4 ms; hidden: 2.0].
		+ MaxRankIdQuery [count: 1; total: 155.0 ms]
		+ AllCompaniesQuery [count: 1; total: 2,058.1 ms]
		+ Transaction [count: 5; total: 162,440.3 ms; avg: 32,488.058; max: 34,607.0; min: 28,140.6]
		Transaction [total: 33,297.9 ms; hidden: 20.0].
			+ BeginTransaction [count: 1; total: 4.0 ms]
			+ Loader.Load [count: 5,000; total: 23,685.3 ms; avg: 4.737; max: 69.0; min: 4.0]
			+ _CompanyCache.GetByTickerOrSEDOL6 [count: 5,000; total: 6.0 ms; avg: 0.001; max: 1.0; min: 0.0]
			+ TargetRankQuery.List [count: 5,000; total: 4,026.2 ms; avg: 0.805; max: 27.0; min: 0.0]
			+ SessionSaveNewRank [count: 5,000; total: 214.0 ms; avg: 0.043; max: 39.0; min: 0.0]
			+ Commit [count: 1; total: 5,342.3 ms]
		(THE REMAINING 4 TRANSACTIONS WERE OMITTED FOR BREVITY.)

This report gives us a summary of how long each query and transaction took to complete.  Let's break this report down more and go through each line:

	RanksApp [total: 164,655.4 ms; hidden: 2.0].

This line tells us that the 'RanksApp' took a total of 164,655.4 ms to run - about 165 seconds, and that there was 2.0 ms of hidden processing time.  **Hidden processing time** is the difference between a parent activity's total time and the sum of its child activity total times.

		+ MaxRankIdQuery [count: 1; total: 155.0 ms]
		+ AllCompaniesQuery [count: 1; total: 2,058.1 ms]

These are **child activity summaries**.  Summaries are always prefixed with a '+'.  These summaries tell us that each query was executed once (count: 1) along with the total processing times for each.

		+ Transaction [count: 5; total: 162,440.3 ms; avg: 32,488.058; max: 34,607.0; min: 28,140.6]

This is a summary for a child activity that observed more than once.  In this case, a transaction that ran 5 times (count: 5).  We also include the total, average, maximum, and minimum times for each child activity.

		Transaction [total: 33,297.9 ms; hidden: 20.0].
			+ BeginTransaction [count: 1; total: 4.0 ms]
			+ Loader.Load [count: 5,000; total: 23,685.3 ms; avg: 4.737; max: 69.0; min: 4.0]
			+ _CompanyCache.GetByTickerOrSEDOL6 [count: 5,000; total: 6.0 ms; avg: 0.001; max: 1.0; min: 0.0]
			+ TargetRankQuery.List [count: 5,000; total: 4,026.2 ms; avg: 0.805; max: 27.0; min: 0.0]
			+ SessionSaveNewRank [count: 5,000; total: 214.0 ms; avg: 0.043; max: 39.0; min: 0.0]
			+ Commit [count: 1; total: 5,342.3 ms]

This is one of the 5 transactions that were run.  It has its own set of child activity summaries.  Thus, performance reports are hierarchical.  Notice how it is summarizing several thousand children for us.  In this case, the activity named 'Loader.Load' is consuming most of the processing time (23,685.3 ms out of 33,297.9 ms).  Thus, this real-world example has likely exposed what we need to examine next.

## Performance Reports

Collecting performance points is the easy part. Creating easy-to-read reports with summaries is what PerformanceMarkers does well.


You can export to the following media types:

* **Text** - a human-readable hierarchy of activities with summaries of how long each activity took to complete.  See above for a sample report.
* **XML** - same as the text format, except you can also easily load it using any XML parser.  This is useful when you may need to write a script that aggregates several performance reports.


## Sequential Performance Tracking

## Thread Safety

Instances of the Marker class are not thread-safe.  Ensure that exactly 1 thread is accessing a marker at a time.

Instances of the  MarkerFactory class are not thread-safe, either.  However the following statement is thread-safe because it creates a marker factory and uses it. 

	MarkerReportFactoryProvider.CreateReportFactory(MarkerReportFactoryType.Xml).CreateReport(CreatedMarker);

## Concurrent Performance Tracking

Now that we know that markers and marker factories are not thread-safe, we can develop a strategy 

## Performance Reports - XML

There are 2 ways to get a performance report in XML format.

Get as a string:

	string PerformanceReport = MarkerReportFactoryProvider.CreateReportFactory(MarkerReportFactoryType.Xml).CreateReport(CreatedMarker);

Write to a stream:

	using (FileStream TargetStream = new FileStream("path/to/file.xml", FileMode.Create))
	{
		MarkerReportFactory CreatedFactory = MarkerReportFactoryProvider.CreateReportFactory(MarkerReportFactoryType.Xml)
		CreatedFactory.TargetStream = TargetStream;
		CreatedFactory.Marker = CreatedMarker;
		CreatedFactory.WriteReport();
	}


## Recommended Configuration

* Development & Testing Environments


* Production Environments

## Configuration Reference

	<MarkerConfig>
		<MarkerType>Enabled</MarkerType>
		<MarkerFailureMode>HighlyVisible</MarkerFailureMode>
		<MarkerReportFactoryType>PlainText</MarkerReportFactoryType>
	</MarkerConfig>
