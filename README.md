# ![Icon](https://github.com/ridicoulous/Bitmex.Net/blob/master/Bitmex.Net/Icon/icon.png?raw=true) Bitmex.Net 

[![publish to nuget](https://github.com/ridicoulous/Bitmex.Net/actions/workflows/publish.yml/badge.svg)](https://github.com/ridicoulous/Bitmex.Net/actions/workflows/publish.yml)

Bitmex.Net.Client is a .Net wrapper for the Bitmex API as described on [Bitmex](https://www.bitmex.com/api/explorer/). It includes all features the API provides using clear and readable C# objects including 
* Reading market info
* Placing and managing orders
* Reading balances and funds
* Live updates using the websocket
* Loading historical data

Additionally it adds some convenience features like:
* Configurable rate limiting
* Autmatic logging
## Installation
![Nuget version](https://img.shields.io/nuget/v/Bitmex.Net.Client.svg) ![Nuget downloads](https://img.shields.io/nuget/dt/Bitmex.Net.Client.svg)

Available on [NuGet](https://www.nuget.org/packages/Bitmex.Net.Client/):
```
PM> Install-Package Bitmex.Net.Client
```
To get started with Bitmex.Net.Client first you will need to get the library itself. The easiest way to do this is to install the package into your project using [NuGet](https://www.nuget.org/packages/Bitmex.Net.Client/).

## Getting started
To get started we have to add the Bitmex.Net.Client namespace:  `using Bitmex.Net.Client;`.

Bitmex.Net.Client provides three clients to interact with the Bitmex API:

The `BitmexClient` provides all rest API calls.

The `BitmexSocketClient` provides functions to interact with the websocket provided by the Bitmex API.

The `BitmexHistoricalTradesLoader` provides ability to bulk load historical trading data from [Bitmex public data endpoint](https://www.bitmex.com/app/apiOverview#Historical-Data)

See [examples here](https://github.com/ridicoulous/Bitmex.Net/blob/master/Bitmex.Net.ClientExample/Program.cs). Note that you have to add your own appconfig.json and set up connection to [testnet](https://testnet.bitmex.com/) or [production](https://bitmex.com/) Bitmex API:

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
<td><a href="https://github.com/d-ugarov/Exante.Net"><img src="https://github.com/d-ugarov/Exante.Net/blob/master/Exante.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/d-ugarov/Exante.Net">Exante</a>
</td>
</tr>
</table>

## Changelog
* 10/1/2021
  * base library major update, bulk orders cancelling fixes
* 1/28/2021
  * dependencies update
* 1/26/2021 
  * added milliseconds accuracy to start/end time filters
  * added auth in not auth methods for better rate limits
  * fix boolean values serialization

* 1/18/2021 
  * Wallet history fix
  * Fix typo at Get all price indices endpoint
  
* 1/11/2021 
  * base library update

## Donations
Donations are greatly appreciated and a motivation to keep improving.

**Btc**:  14nuXrFEKTrvyhHWYW7RgRt4zVxBfwff5V  
**Eth**:  0x7CD82F45b173891e36d68ea4311B8b13A11a3B4b
