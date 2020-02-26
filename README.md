InnerCore.Api.Kaiterra [![Build Status][azure build]][project]	[![NuGet][nuget badge]][nuget package]	  [![.NET Standard][dotnet-standard badge]][dotnet-standard doc]
=====================

Open source library to read sensor data from the Kaiterra devices (Laser Egg, Laser Egg+ Chemical, Laser Egg+ CO2 & SenseEdge). For more details about the devices itself see https://www.kaiterra.com.

## Important: two different clients are available

This library offers two different clients to access the data.

**KaiterraBasicClient**
This one is built around the official api (see https://www.kaiterra.com/dev/). You only need an api key and the serial number of the device you wish to read from. No account is required when you have generated a api key.
Using this client you can read all measurements such as temperature, relative humidity, CO2, TVOC, PM2.5 and PM10 values. 


**KaiterraExtendedClient**
This client is based around the dashboard (https://dashboard.kaiterra.cn/) and is not based on the official api. Be carefull as one day this might not work anymore. Compared to the basic client this one offers some benefits however:
 - no api key is required, but you'll need an account and it's credentials
 - allows you to see the devices that are connected to the account - no need to know the serial number of the device!
 - allows you to read some more values such as the air quality index, battery state, the charge state and the firmware
 - allows you to read historic values

## Usage of the KaiterraBasicClient

First you need an api key - check https://www.kaiterra.com/dev/ if you don't have one. Using this key you then can instanciate the client:
	var client = new KaiterraBasicClient("put your api key here");

If you have a Laser Egg you can read it's latest values using this line of code:
	var sensorReadings = await client.GetLaserEggDetails("put the serial of the device here");

If you have a SensorEdge device you can read it's latest values using this line of code:
	var sensorReadings = await client.GetSenseEdgeDetails("put the serial of the device here");

*Note: have a look at the provides sample application as it contains two demo devices*

## Usage of the KaiterraExtendedClient

tbd

## License

InnerCore.Api.Kaiterra is licensed under [MIT](http://www.opensource.org/licenses/mit-license.php "Read more about the MIT license form"). Refer to [license.txt](https://github.com/MadMonkey87/InnerCore.Api.Kaiterra/blob/master/LICENSE.txt) for more information.

## Contributions
Contributions are welcome. Fork this repository and send a pull request if you have something useful to add.

[azure build]: https://innercore.visualstudio.com/InnerCore.Api.Kaiterra/_apis/build/status/InnerCore.Api.Kaiterra?branchName=master
[project]: https://github.com/MadMonkey87/InnerCore.Api.Kaiterra
[nuget badge]: https://img.shields.io/nuget/v/InnerCore.Api.Kaiterra.svg
[nuget package]: https://www.nuget.org/packages/InnerCore.Api.Kaiterra
[dotnet-standard badge]: http://img.shields.io/badge/.NET_Standard-v2.0-green.svg
[dotnet-standard doc]: https://docs.microsoft.com/da-dk/dotnet/articles/standard/library