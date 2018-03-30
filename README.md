# ![Anlog](https://user-images.githubusercontent.com/5340818/38121535-4b734df8-33a6-11e8-98aa-e9b8d7234de0.png)

**Another .NET Logger**

A fast and lightweight key/value pair logger for .NET Core projects.

## Contents
1. <a href="#quick-start">Quick start</a>
1. <a href="#features">Features</a>
1. <a href="#license">License</a>

## <a id="quick-start"></a>Quick start

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

## <a id="features"></a>Features

* Key/value pair approach ensures a standardization on logs.
* Write to file or memory. 
* Easy to use through a static `Log` object.
* Fast when formatting and writing.

## <a id="license"></a>License

Licensed under the [The MIT License (MIT)](http://opensource.org/licenses/MIT). Please see [LICENSE](LICENSE) for more information.
