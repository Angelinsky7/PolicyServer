// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;

namespace IdentityServer4.Quickstart.UI {
    public class AccountOptions {
        public static Boolean AllowLocalLogin = true;
        public static Boolean AllowRememberLogin = true;
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        public static Boolean ShowLogoutPrompt = true;
        public static Boolean AutomaticRedirectAfterSignOut = true;

        // specify the Windows authentication scheme being used
        public static readonly String WindowsAuthenticationSchemeName = Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme;
        // if user uses windows auth, should we load the groups from windows
        public static Boolean IncludeWindowsGroups = false;

        public static String InvalidCredentialsErrorMessage = "Invalid username or password";
    }
}
