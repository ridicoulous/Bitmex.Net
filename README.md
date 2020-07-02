# ![Icon](https://github.com/ridicoulous/Bitmex.Net/blob/master/Bitmex.Net/Icon/icon.png?raw=true) Bitmex.Net 

![Build status](https://travis-ci.org/ridicoulous/Bitmex.Net.svg?branch=master)

Bitmex.Net.Client is a .Net wrapper for the Bitmex API as described on [Bitmex](https://www.bitmex.com/api/explorer/). It includes all features the API provides using clear and readable C# objects including 
* Reading market info
* Placing and managing orders
* Reading balances and funds
* Live updates using the websocket
* Loading historical data

Additionally it adds some convenience features like:
* Configurable rate limiting
* Autmatic logging

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/ridicoulous/Bitmex.Net.Client/issues)**

## CryptoExchange.Net
Implementation is build upon the CryptoExchange.Net library, make sure to also check out the documentation on that: [docs](https://github.com/JKorf/CryptoExchange.Net)

Other CryptoExchange.Net implementations:
<table>
<tr>
<td><a href="https://github.com/JKorf/Bitfinex.Net"><img src="https://github.com/JKorf/Bitfinex.Net/blob/master/Bitfinex.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Bitfinex.Net">Bitfinex</a>
</td>
<td><a href="https://github.com/JKorf/Bittrex.Net"><img src="https://github.com/JKorf/Bittrex.Net/blob/master/Bittrex.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Bittrex.Net">Bittrex</a>
</td>
<td><a href="https://github.com/JKorf/Binance.Net"><img src="https://github.com/JKorf/Binance.Net/blob/master/Binance.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Binance.Net">Binance</a>
</td>
<td><a href="https://github.com/JKorf/CoinEx.Net"><img src="https://github.com/JKorf/CoinEx.Net/blob/master/CoinEx.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/CoinEx.Net">CoinEx</a>
</td>
<td><a href="https://github.com/JKorf/Huobi.Net"><img src="https://github.com/JKorf/Huobi.Net/blob/master/Huobi.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Huobi.Net">Huobi</a>
</td>
<td><a href="https://github.com/JKorf/Kucoin.Net"><img src="https://github.com/JKorf/Kucoin.Net/blob/master/Kucoin.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Kucoin.Net">Kucoin</a>
</td>
<td><a href="https://github.com/JKorf/Kraken.Net"><img src="https://github.com/JKorf/Kraken.Net/blob/master/Kraken.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Kraken.Net">Kraken</a>
</td>
<td><a href="https://github.com/Zaliro/Switcheo.Net"><img src="https://github.com/Zaliro/Switcheo.Net/blob/master/Resources/switcheo-coin.png?raw=true"></a>
<br />
<a href="https://github.com/Zaliro/Switcheo.Net">Switcheo</a>
</td>
<td><a href="https://github.com/ridicoulous/LiquidQuoine.Net"><img src="https://github.com/ridicoulous/LiquidQuoine.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/ridicoulous/LiquidQuoine.Net">Liquid</a>
</td>
<td><a href="https://github.com/burakoner/OKEx.Net"><img src="https://raw.githubusercontent.com/burakoner/OKEx.Net/master/Okex.Net/Icon/icon.png"></a>
<br />
<a href="https://github.com/burakoner/OKEx.Net">OKEx</a>
</td>
</tr>
</table>

## Installation
![Nuget version](https://img.shields.io/nuget/v/Bitmex.Net.Client.svg) ![Nuget downloads](https://img.shields.io/nuget/dt/Bitmex.Net.Client.svg)

Available on [NuGet](https://www.nuget.org/packages/Bitmex.Net.Client/):
```
PM> Install-Package Bitmex.Net.Client
```
To get started with Bitmex.Net.Client first you will need to get the library itself. The easiest way to do this is to install the package into your project using [NuGet](https://www.nuget.org/packages/Bitmex.Net.Client/). Using Visual Studio this can be done in two ways.

### Using the package manager
In Visual Studio right click on your solution and select 'Manage NuGet Packages for solution...'. A screen will appear which initially shows the currently installed packages. In the top bit select 'Browse'. This will let you download net package from the NuGet server. In the search box type 'Bitmex.Net.Client' and hit enter. The Bitmex.Net.Client package should come up in the results. After selecting the package you can then on the right hand side select in which projects in your solution the package should install. After you've selected all project you wish to install and use Bitmex.Net.Client in hit 'Install' and the package will be downloaded and added to you projects.

### Using the package manager console
In Visual Studio in the top menu select 'Tools' -> 'NuGet Package Manager' -> 'Package Manager Console'. This should open up a command line interface. On top of the interface there is a dropdown menu where you can select the Default Project. This is the project that Bitmex.Net.Client will be installed in. After selecting the correct project type  `Install-Package Bitmex.Net.Client`  in the command line interface. This should install the latest version of the package in your project.

After doing either of above steps you should now be ready to actually start using Bitmex.Net.Client.

## Getting started
After  it's time to actually use it. To get started we have to add the Bitmex.Net.Client namespace:  `using Bitmex.Net.Client;`.

Bitmex.Net.Client provides three clients to interact with the Bitmex API:

The `BitmexClient` provides all rest API calls.

The `BitmexSocketClient` provides functions to interact with the websocket provided by the Bitmex API.

The `BitmexHistoricalTradesLoader` provides ability to bulk load historical trading data from [Bitmex public data endpoint](https://www.bitmex.com/app/apiOverview#Historical-Data)

See examples at Bitmex.Net.ClientExample. Add your own appconfig.json and set up connection to [testnet](https://testnet.bitmex.com/) or [production](https://bitmex.com/) Bitmex API:

```
{
  "testnet": {
    "key": "yourTestentKey",
    "secret": "yourTestentSecret"
  },
  "prod": {
    "key": "yourProdKey",
    "secret": "yourProdSecret"
  }  
}
```

## Donations
Donations are greatly appreciated and a motivation to keep improving.

**Btc**:  14nuXrFEKTrvyhHWYW7RgRt4zVxBfwff5V  
**Eth**:  0x7CD82F45b173891e36d68ea4311B8b13A11a3B4b
