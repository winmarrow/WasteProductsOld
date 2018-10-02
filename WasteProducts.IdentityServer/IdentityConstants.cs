﻿using System;

namespace WasteProducts.IdentityServer
{
    public class IdentityConstants
    {
        public static string WasteProducts_Api_ClientID = "wasteproducts.api";
        public static string WasteProducts_Api_Scope = "wasteproducts-api";
        public static string WasteProducts_Api_Name = "Waste Products Web Api";
        public static string WasteProducts_Api_Description = "Waste Products Web Api description";
        public static string WasteProducts_Api_Secret = Guid.NewGuid().ToString();

        public static string WasteProducts_Front_ClientID = "wasteproducts.front.angular";
        public static string WasteProducts_Front_ClientUrl = "http://localhost:4200";
        public static string WasteProducts_Front_ClientName = "Waste Products Angular Client";
        
    }
}