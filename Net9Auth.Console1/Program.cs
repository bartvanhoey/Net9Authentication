using Net9Auth.Console1;
using static System.Console;

WriteLine("Hello, World from Console1!");


var staticApiKeyTester = new StaticApiKeyWeatherForecastControllerTester();
await staticApiKeyTester.Start();

var dynamicApiKeyTester = new DynamicApiKeyWeatherForecastControllerTester();
await dynamicApiKeyTester.Start();

var rateLimitTester = new RateLimitingWeatherForecastControllerTester();
await rateLimitTester.Start();