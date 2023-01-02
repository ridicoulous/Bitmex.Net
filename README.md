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
* 2.0.1
  * updated documentation
* 2.0.0
  * updated to CryptoExchange.Net v5.3.1. See [examples here](https://github.com/ridicoulous/Bitmex.Net/blob/master/Bitmex.Net.ClientExample/Program.cs) how to use BitmexClient. 
  * removed some unused fields due to bitmex.com updates
  * nontrade socket subscriptions on different endpoint (due to bitmex.com updates)
  * ratelimiter by default:
      30/min for unsigned requests
      120/min for signed ones
      10/1s (in addition to 120/min) for orders requests
  * fixed resubscribing SymbolOrderBook
  * position: renamed RealisedPnl=> RealisedPnlAfterRebalancing. RealisedPnl is a result of calculation now
  * renamed models Order=>BitmexOrder and so on, to avoid conflict with base lib naming
  * revert=true by default! Be careful, by default, requests return the latest values first
  * added a few new requests
* v1.6.1
  * ws endpoint update, bulk orders changes
* v1.6.0 
  * fix user wallet history deserialization error
* v1.5.2
  * base library major update, bulk orders cancelling fixes
* v1.4.8
  * dependencies update
* v1.4.7 
  * added milliseconds accuracy to start/end time filters
* v1.4.6
  * added auth in not auth methods for better rate limits
* v1.4.6
  * fix boolean values serialization
* v1.4.4
  * Wallet history fix
* v1.4.3
  * Fix typo at Get all price indices endpoint
* v1.4.2 
  * base library update

## Donations
Donations are greatly appreciated and a motivation to keep improving.

**Btc**:  14nuXrFEKTrvyhHWYW7RgRt4zVxBfwff5V  
**Eth**: 0xa399Fff5Cd14fc589030C85C5671cdEdC4C413b1
