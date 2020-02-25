using System;
using System.Collections.Generic;
using System.Text;


namespace    Bitmex.Net.Client.Objects
{
    public enum GlobalNotificationType
    {
        [System.Runtime.Serialization.EnumMember(Value = @"success")]
        Success = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"error")]
        Error = 1,

        [System.Runtime.Serialization.EnumMember(Value = @"info")]
        Info = 2,

    }


    public enum UserEventType
    {
        [System.Runtime.Serialization.EnumMember(Value = @"apiKeyCreated")]
        ApiKeyCreated = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"deleverageExecution")]
        DeleverageExecution = 1,

        [System.Runtime.Serialization.EnumMember(Value = @"depositConfirmed")]
        DepositConfirmed = 2,

        [System.Runtime.Serialization.EnumMember(Value = @"depositPending")]
        DepositPending = 3,

        [System.Runtime.Serialization.EnumMember(Value = @"banZeroVolumeApiUser")]
        BanZeroVolumeApiUser = 4,

        [System.Runtime.Serialization.EnumMember(Value = @"liquidationOrderPlaced")]
        LiquidationOrderPlaced = 5,

        [System.Runtime.Serialization.EnumMember(Value = @"login")]
        Login = 6,

        [System.Runtime.Serialization.EnumMember(Value = @"pgpMaskedEmail")]
        PgpMaskedEmail = 7,

        [System.Runtime.Serialization.EnumMember(Value = @"pgpTestEmail")]
        PgpTestEmail = 8,

        [System.Runtime.Serialization.EnumMember(Value = @"passwordChanged")]
        PasswordChanged = 9,

        [System.Runtime.Serialization.EnumMember(Value = @"positionStateLiquidated")]
        PositionStateLiquidated = 10,

        [System.Runtime.Serialization.EnumMember(Value = @"positionStateWarning")]
        PositionStateWarning = 11,

        [System.Runtime.Serialization.EnumMember(Value = @"resetPasswordConfirmed")]
        ResetPasswordConfirmed = 12,

        [System.Runtime.Serialization.EnumMember(Value = @"resetPasswordRequest")]
        ResetPasswordRequest = 13,

        [System.Runtime.Serialization.EnumMember(Value = @"transferCanceled")]
        TransferCanceled = 14,

        [System.Runtime.Serialization.EnumMember(Value = @"transferCompleted")]
        TransferCompleted = 15,

        [System.Runtime.Serialization.EnumMember(Value = @"transferReceived")]
        TransferReceived = 16,

        [System.Runtime.Serialization.EnumMember(Value = @"transferRequested")]
        TransferRequested = 17,

        [System.Runtime.Serialization.EnumMember(Value = @"twoFactorDisabled")]
        TwoFactorDisabled = 18,

        [System.Runtime.Serialization.EnumMember(Value = @"twoFactorEnabled")]
        TwoFactorEnabled = 19,

        [System.Runtime.Serialization.EnumMember(Value = @"withdrawalCanceled")]
        WithdrawalCanceled = 20,

        [System.Runtime.Serialization.EnumMember(Value = @"withdrawalCompleted")]
        WithdrawalCompleted = 21,

        [System.Runtime.Serialization.EnumMember(Value = @"withdrawalConfirmed")]
        WithdrawalConfirmed = 22,

        [System.Runtime.Serialization.EnumMember(Value = @"withdrawalRequested")]
        WithdrawalRequested = 23,

        [System.Runtime.Serialization.EnumMember(Value = @"addressSkipConfirmRequested")]
        AddressSkipConfirmRequested = 24,

        [System.Runtime.Serialization.EnumMember(Value = @"addressSkipConfirmVerified")]
        AddressSkipConfirmVerified = 25,

        [System.Runtime.Serialization.EnumMember(Value = @"verify")]
        Verify = 26,

    }


    public enum UserEventStatus
    {
        [System.Runtime.Serialization.EnumMember(Value = @"success")]
        Success = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"failure")]
        Failure = 1,

    }

}

