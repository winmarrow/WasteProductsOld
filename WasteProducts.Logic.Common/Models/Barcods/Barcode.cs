﻿using System.Drawing;
using WasteProducts.Logic.Common.Models.Products;

namespace WasteProducts.Logic.Common.Models.Barcods
{
    /// <summary>
    /// Model for entity barcode.
    /// </summary>
    public class Barcode
    {
        /// <summary>
        /// Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Barcode number.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Product name.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Product сomposition.
        /// </summary>
        public string Composition { get; set; }

        /// <summary>
        /// Product brand.
        /// </summary>
        public string Brend { get; set; }

        /// <summary>
        /// Product country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Product weight.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Product picture path.
        /// </summary>
        public Image Picture { get; set; }      //TODO: Image => string path to image

        /// <summary>
        /// Specifies the concreat product
        /// </summary>
        public virtual Product Product { get; set; }
    }
}
