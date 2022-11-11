using Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Requests;
using CryptoExchange.Net.Interfaces.CommonClients;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace  Bitmex.Net.Client.Interfaces
{
    public interface IBitmexMarginClient : IFuturesClient
    {
        #region  Position : Summary of Open and Closed Positions 
        /// <summary>
        /// The fields largely follow the <see href="http://www.onixs.biz/fix-dictionary/5.0.SP2/msgType_AP_6580.html"> FIX spec definitions</see>.
        /// </summary>
        /// <param name="filter">can use here filter, columns and count parameters</param>
        /// <returns></returns>
        Task<WebCallResult<List<Position>>> GetPositionsAsync(BitmexRequestWithFilter filter = null, CancellationToken ct = default);

        /// <summary>
        /// Enable isolated margin or cross margin per-position.
        /// </summary>
        /// <param name="symbol">Position symbol to isolate</param>
        /// <param name="isolate">True for isolated margin, false for cross margin.</param>
        /// <returns></returns>
        Task<WebCallResult<Position>> SetPositionIsolationAsync(string symbol, bool isolate, CancellationToken ct = default);

        /// <summary>
        /// Choose leverage for a position.
        /// </summary>
        /// <param name="symbol">Symbol of position to adjust.</param>
        /// <param name="leverage">Leverage value. Send a number between 0.01 and 100 to enable isolated margin with a fixed leverage. Send 0 to enable cross margin.</param>
        /// <returns></returns>
        Task<WebCallResult<Position>> SetPositionLeverageAsync(string symbol, decimal leverage, CancellationToken ct = default);

        /// <summary>
        /// Update your risk limit.
        /// </summary>
        /// <param name="symbol">Symbol of position to update risk limit on.</param>
        /// <param name="riskLimit">New Risk Limit, in Satoshis.</param>
        /// <returns></returns>
        Task<WebCallResult<Position>> SetPositionRiskLimitAsync(string symbol, decimal riskLimit, CancellationToken ct = default);

        /// <summary>
        /// Transfer equity in or out of a position
        /// </summary>
        /// <param name="symbol">Symbol of position to isolate.</param>
        /// <param name="amount">Amount to transfer, in Satoshis. May be negative.</param>
        /// <returns></returns>
        Task<WebCallResult<Position>> SetPositionTransferMarginAsync(string symbol, decimal amount, CancellationToken ct = default);
        #endregion
        #region Liquidation : Active Liquidations 

        /// <summary>
        /// Get liquidation orders
        /// </summary>
        /// <param name="requestWithFilter"></param>
        /// <returns></returns>
        Task<WebCallResult<List<Liquidation>>> GetLiquidationsAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default);
        #endregion        
        #region Funding : Swap Funding History 
        /// <summary>
        /// Get funding history.
        /// </summary>
        /// <param name="requestWithFilter"></param>
        /// <returns></returns>
        Task<WebCallResult<List<Funding>>> GetFundingAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default);
        #endregion
    }
}
