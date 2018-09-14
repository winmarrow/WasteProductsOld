﻿using WasteProducts.Logic.Common.Models.Donations;

namespace WasteProducts.Logic.Common.Services.Donations
{
    /// <summary>
    /// This interface provides the log method for donations.
    /// </summary>
    public interface IDonationService
    {
        /// <summary>
        /// Log new donation.
        /// </summary>
        /// <param name="donation">New donation to log.</param>
        void Log(Donation donation);
    }
}