# ![Anlog](https://user-images.githubusercontent.com/5340818/38121535-4b734df8-33a6-11e8-98aa-e9b8d7234de0.png)

**(Yet) Another .NET Logger**

[![NuGet Version](http://img.shields.io/nuget/v/Anlog.svg?style=flat)](https://www.nuget.org/packages/Anlog/)

Fast and lightweight key/value pair logger for .NET Core projects.

## Contents
1. [Features](#features)
1. [Quick start](#quick-start)
1. [Sinks](#sinks)
1. [Formatters](#formatters)
1. [License](#license)

## Features

* Key/value pair approach ensures a standardization on logs.
* Writes to file or memory. 
* Easy to use through a static `Log` object.
* Fast when formatting and writing.

## Quick start

Install *Anlog* from the NuGet Gallery:

```
Install-Package Anlog
```

Create a console application and configure the logger:

```cs
using Anlog;
using Anlog.Factories;
using Anlog.Sinks.Console;

namespace QuickStart
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creates the logger.
            Log.Logger = new LoggerFactory()
                .WriteTo.Console()
                .CreateLogger();
            
            // Writes the log to console.
            Log.Append("key", "value").Info();
        }
    }
}
```

Log written: `2018-03-29 22:22:07.656 [INF] c=Program.Main:17 key=value`

## Sinks

Sinks are objects that write the log to some output, like a file or memory.

### Console

Writes the log to the console.

```cs
Log.Logger = new LoggerFactory()
    .WriteTo.Console()
    .CreateLogger();
```

#### Settings

- *async*: true if write to the console should be asynchronous, otherwise false. The default is false.


### SingleFile

Writes the log to a single file with unlimited size.

```cs
Log.Logger = new LoggerFactory()
    .WriteTo.SingleFile()
    .CreateLogger();
```

#### Settings

- *logFilePath*: log file path. 
- *encoding*: file encoding. The default is UTF8.
- *bufferSize*: buffer size to be used. The default is 4096.

### InMemory

Writes the log to a memory buffer. 

It's recommended to use only in tests.

```cs
Log.Logger = new LoggerFactory()
    .WriteTo.InMemory()
    .CreateLogger();
```

To get the written logs, use `Log.GetSink()`:
```cs
var logs = Log.GetSink<InMemorySink>()?.GetLogs();
```

## Formatters

Formats the key/value entries. All formatters include class, method name and line number of the log call.

### CompactKeyValue

Formats the key/value entries using a compact approach.

`2018-03-29 22:22:07.656 [INF] c=Program.Main:17 key=value`.

This is the default formatter.

```cs
Log.Logger = new LoggerFactory()
    .FormatAs.CompactKeyValue()
    .CreateLogger();
```

#### Formats

- *Strings*: use the given string. E.g.: `key=text`
- *Numbers*: use `ToString(culture)`. E.g.: `key=24.11`
- *Dates*: use `ToString(dateTimeFormat)`. E.g.: `date=2018-03-25 23:00:00.000`
- *Enums*: use `ToString()`. E.g.: `key=Value`
- *Arrays* and *IEnumerable*: writes the items between `[]` separated by comma. E.g.: `key=[11.1,24.2,69.3,666.4]`
- *Classes* with fields and/or properties: writes the fields/properties as key/value pairs between `{}`. E.g.: `{field=24 property=666.11}`

#### Settings

- *culture*: culture to be used. The default is `CultureInfo.InvariantCulture`.
- *dateTimeFormat*: date/time log format. The default format is "yyyy-MM-dd HH:mm:ss.fff".

## License

Licensed under the [The MIT License (MIT)](http://opensource.org/licenses/MIT). Please see [LICENSE](https://raw.githubusercontent.com/intentor/anlog/master/LICENSE) for more information.
