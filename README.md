[![NuGet Version and Downloads count](https://buildstats.info/nuget/TJC.Logging)](https://www.nuget.org/packages/TJC.Logging)

## Format
- Timestamp
- Location (`Namespace`, `Type Name`, `Member Name`, `Line Number`)

## Extensions
> Extensions for `ILogger`

### [ILogger.LogMark()](TJC.Logging/Extensions/LogMarkExtension.cs)
> Logs detailed information about the current execution location.

### [ILogger.LogStep(int step)](TJC.Logging/Extensions/LogStepExtension.cs)
> Logs detailed information about the current execution location, including a step number for debugging purposes.
#### Example
```csharp
var step = 0; 
ILogger.LogStep(ref step);
// Do first step
ILogger.LogStep(ref step);
// Do second step
```

### [ILogger.LogGet<T>(T obj)](TJC.Logging/Extensions/Specialty/LogGetExtension.cs)
> Logs the value of an object and returns the object.

### [ILogger.LogSet()](TJC.Logging/Extensions/Specialty/LogSetExtension.cs)
> Logs the before and after values of a property and set it when done.
