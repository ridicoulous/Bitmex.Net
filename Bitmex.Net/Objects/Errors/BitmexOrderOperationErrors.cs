using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client.Objects.Errors
{
    public static class BitmexOrderOperationErrors
    {
        public static HashSet<string> FatalOrderErrors = new HashSet<string>()
        {
            "Forbidden",
            "Duplicate clOrdID",
            "Invalid orderID",
            "Duplicate orderID",
"Invalid symbol",
"Instruments do not match",
"Instrument not listed for trading yet",
"Instrument expired",
"Instrument has no mark price",
"Accounts do not match",
"Invalid account",
"Account is suspended",
"Account has no",
"Invalid ordStatus",//(trying to amend a canceled or filled order)
"Invalid triggered",
"Invalid workingIndicator",
"Invalid side",
"Invalid orderQty or simpleOrderQty",
"Invalid simpleOrderQty",
"Invalid orderQty",
"Invalid simpleLeavesQty",
"Invalid simpleCumQty",
"Invalid leavesQty",
"Invalid cumQty",
"Invalid avgPx",
"Invalid price",
"Invalid price tickSize",
"Invalid displayQty",
"Unsupported ordType",
"Unsupported pegPriceType",
"Invalid pegPriceType for ordType",
"Invalid pegOffsetValue for pegPriceType",
"Invalid pegOffsetValue tickSize",
"Invalid stopPx for ordType",
"Invalid stopPx tickSize",
"Unsupported timeInForce",
"Unsupported execInst",
"Invalid execInst",
"Invalid ordType or timeInForce for execInst",
"Invalid displayQty for execInst",
"Invalid ordType for execInst",
"Unsupported contingencyType",
"Invalid clOrdLinkID for contingencyType",
"Invalid multiLegReportingType",
"Invalid currency",
"Invalid settlCurrency"

        };
    }
}
