![image](http://www.ark-energy.eu/wp-content/uploads/ark-dark.png)
# Artesian.SDK

This Library adds support for building Artesian queries.

## Getting Started
### Installation
This library is provided in NuGet.

Support for .NET Framework 4.6.1, .NET Standard 2.0.

In the Package Manager Console -
```
Install-Package Artesian.SDK
```

or download directly from NuGet.

## How to use
To use the Artesian SDK to build a query, you will need to provide a valid authentication(Api-key/Auth credentials)
```csharp
 ArtesianServiceConfig _cfg = new ArtesianServiceConfig(
		new Uri("https://fake-artesian-env/"),
		"5418B0DB - 7AB9 - 4875 - 81BA - 6EE609E073B6"
		);

 ArtesianServiceConfig _cfg = new ArtesianServiceConfig(
		new Uri("https://fake-artesian-env/"),
		"audience",
		"domain",
		"client_id",
		"client_secret"
		);
```
Passing this authentication configuration to create a query service to start building queries.
```csharp
 var queryservice = new QueryService(_cfg);
```
For an example of a query. This is an Actual Time Series Query providing a time granularity of Day 
and a Date range to query by as well as a set of Market Data ids we want to return.
```csharp
var actualTimeSeries = qs.CreateActual()
                .ForMarketData(new int[] { 100000001, 100000002, 100000003 })
                .InGranularity(Granularity.Day)
                .InAbsoluteDateRange(new LocalDate(2018,08,01),new LocalDate(2018,08,10))
                .ExecuteAsync().Result;
```
### Artesian SDK Queries
<table>
  <tr><th>Query type</th><th>Description</th></tr>
  <tr><td>Market Assessment Market Data</td><td>Get a Market Assessment timeserie for the given Market Data IDs and Products names and extraction window.</td></tr>
  <tr><td>Actual Market Data</td><td>Gets a Timeseries for a set of Actual Market Data IDs over an extraction window at a specified Granularity</td></tr>
  <tr><td>Versioned Market Data for specific version</td><td>Gets the specific version within an extraction window.</td></tr>
  <tr><td>Versioned Market Data for last of days</td><td>Gets the latest version of each day in an extraction window.</td></tr>
  <tr><td>Versioned Market Data for last of months</td><td>Gets the latest version of each month in an extraction window.</td></tr>
  <tr><td>Versioned Market Data for last N</td><td>Gets the latest N timeserie versions that have data in an extraction window.</td></tr>
  <tr><td>Versioned Market Data for most updated version</td><td>Gets the timeserie of the most updated version of each timepoint.</td></tr>
</table>

### Artesian SDK Extraction Windows

Date Range
```csharp
 .InAbsoluteDateRange(new LocalDate(2018,08,01),new LocalDate(2018,08,10)
```
Relative Interval
```csharp
 .InRelativeInterval(RelativeInterval.RollingMonth)
```
Period
```csharp
 .InRelativePeriod(Period.FromDays(5))
```
Period Range
```csharp
 .InRelativePeriodRange(Period.FromWeeks(2), Period.FromDays(20))
```

### Artesian SDK Versioned Queries
```csharp
 var ver = qs.CreateVersioned()
		.ForMarketData(new int[] { 100000001 })
		.InGranularity(Granularity.Day)
		.ForLastOfMonths(Period.FromMonths(-4)) //Define the type of versioned query and provide an extraction time window
		.InRelativeInterval(RelativeInterval.RollingMonth)
		.ExecuteAsync().Result;
```
Version with date range
```csharp
 .ForVersion(new LocalDateTime(2018, 07, 19, 12, 0))
```
Last Of Days with Period Range
```csharp
 .ForLastOfDays(Period.FromMonths(-1), Period.FromDays(20))
```
Last Of Months with Period
```csharp
 .ForLastOfMonths(Period.FromMonths(-4))
```
Last N 3 the number of versions to retrieve in the extraction
```csharp
  .ForLastNVersions(3)
```
Most update version
```csharp
  .ForMUV()
```
## Links
* [Nuget](https://www.nuget.org/packages/Artesian.SDK/)
* [Github](https://github.com/ARKlab/Artesian.SDK)
* [Ark Energy](http://www.ark-energy.eu/)

## Acknowledgments
* [MessagePack.NodaTime for C#](https://github.com/ARKlab/MessagePack)
* [Flurl](https://flurl.io/docs/fluent-url/)
