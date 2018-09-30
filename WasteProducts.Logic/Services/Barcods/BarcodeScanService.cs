﻿using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using ZXing;
using Spire.Barcode;
using WasteProducts.Logic.Common.Services.Barcods;
using System;
using System.Text.RegularExpressions;

namespace WasteProducts.Logic.Services.Barcods
{
    /// <inheritdoc />
    public class BarcodeScanService : IBarcodeScanService
    {
        private Bitmap _image;
        private Graphics _graphics;

        /// <summary>
        /// get a numeric barcode from the photo
        /// </summary>
        /// <param name="stream"> image of barcode photo</param>
        /// <returns>string of a numerical barcode</returns>
        public string Scan(Stream stream)
        {
            string code = null;
            using (var resizeStream = Resize(stream, 400, 400))
            {
                try
                {
                    code = ScanByZxing(resizeStream);
                }
                catch
                {
                    code = ScanBySpire(resizeStream);
                }
            }
            if (code == null)
                return null;
            if (!IsValid(code))
                return null;
            else
                return code;
        }

        /// <inheritdoc />
        public Stream Resize(Stream stream, int width, int height)
        {
            MemoryStream resultStream = new MemoryStream();
            Bitmap img = new Bitmap(stream);
            Bitmap result = new Bitmap(width, height);
            using (_graphics = Graphics.FromImage(result))
            {
                _graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                _graphics.DrawImage(img, 0, 0, width, height);
                _graphics.Dispose();
            }
            result.Save(resultStream, ImageFormat.Bmp);

            return resultStream;
        }

        /// <inheritdoc />
        public string ScanByZxing(Stream stream)
        {
            Result result = null;
            var barcodeReader = new BarcodeReader
            {
                Options = new ZXing.Common.DecodingOptions()
                {
                    TryHarder = true
                },
                AutoRotate = true
            };
            using (stream)
            {
                _image = new Bitmap(stream);
                result = barcodeReader.Decode(_image);
            }
            if(result == null)
            {
                return null;
            }
            string decoded = result.ToString().Trim();
            if (!IsValid(decoded))
            {
                decoded = null;
            }
            return decoded;
        }

        /// <inheritdoc />
        public string ScanBySpire(Stream stream)
        {
            string decoded = "";

            using (stream)
            {
                decoded = BarcodeScanner.ScanOne(stream, true);
            }
            if (!IsValid(decoded))
            {
                decoded = null;
            }
            return decoded;
        }

        /// <summary>
        /// String code validation.
        /// </summary>
        /// <param name="code">String code.</param>
        /// <returns>true or false</returns>  
        private bool IsValid(string code)
        {
            if (code != (new Regex("[^0-9]")).Replace(code, ""))
            {
                // is not numeric
                return false;
            }
            // pad with zeros to lengthen to 14 digits
            switch (code.Length)
            {
                case 8:
                    code = "000000" + code;
                    break;
                case 12:
                    code = "00" + code;
                    break;
                case 13:
                    code = "0" + code;
                    break;
                case 14:
                    break;
                default:
                    // wrong number of digits
                    return false;
            }
            // calculate check digit
            int[] a = new int[13];
            a[0] = int.Parse(code[0].ToString()) * 3;
            a[1] = int.Parse(code[1].ToString());
            a[2] = int.Parse(code[2].ToString()) * 3;
            a[3] = int.Parse(code[3].ToString());
            a[4] = int.Parse(code[4].ToString()) * 3;
            a[5] = int.Parse(code[5].ToString());
            a[6] = int.Parse(code[6].ToString()) * 3;
            a[7] = int.Parse(code[7].ToString());
            a[8] = int.Parse(code[8].ToString()) * 3;
            a[9] = int.Parse(code[9].ToString());
            a[10] = int.Parse(code[10].ToString()) * 3;
            a[11] = int.Parse(code[11].ToString());
            a[12] = int.Parse(code[12].ToString()) * 3;
            int sum = a[0] + a[1] + a[2] + a[3] + a[4] + a[5] + a[6] + a[7] + a[8] + a[9] + a[10] + a[11] + a[12];
            int check = (10 - (sum % 10)) % 10;
            // evaluate check digit
            int last = int.Parse(code[13].ToString());
            return check == last;
        }
    }
}
