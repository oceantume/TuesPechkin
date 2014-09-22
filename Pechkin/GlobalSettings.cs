﻿using System;
using System.ComponentModel;
using System.Globalization;
using TuesPechkin.Attributes;

namespace TuesPechkin
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GlobalSettings
    {
        private MarginSettings margins = new MarginSettings();

        public enum DocumentColorMode
        {
            Color,
            Grayscale
        }

        public enum DocumentOutputFormat
        {
            PDF,
            PS
        }

        public enum PaperOrientation
        {
            Portrait,
            Landscape
        }

        /// <summary>
        /// Whether to collate the copies. (Default: false)
        /// </summary>
        [GlobalSetting("collate")]
        public bool? Collate { get; set; }

        /// <summary>
        /// Whether to print in color or grayscale. (Default: color)
        /// </summary>
        public DocumentColorMode? ColorMode { get; set; }

        /// <summary>
        /// The path of a file used to store cookies.
        /// </summary>
        [GlobalSetting("load.cookieJar")]
        public string CookieJar { get; set; }

        /// <summary>
        /// How many copies to print. (Default: 1)
        /// </summary>
        [GlobalSetting("copies")]
        public int? Copies { get; set; }

        /// <summary>
        /// The title of the PDF document.
        /// </summary>
        [GlobalSetting("documentTitle")]
        public string DocumentTitle { get; set; }

        /// <summary>
        /// The DPI to use when printing.
        /// </summary>
        [GlobalSetting("dpi")]
        public int? DPI { get; set; }

        /// <summary>
        /// The path of a file to dump an XML outline of the document to.
        /// </summary>
        [GlobalSetting("dumpOutline")]
        public string DumpOutline { get; set; }

        /// <summary>
        /// The maximum DPI to use for images printed in the document.
        /// </summary>
        [GlobalSetting("imageDPI")]
        public int? ImageDPI { get; set; }

        [GlobalSetting("imageQuality")]
        public int? ImageQuality { get; set; }

        /// <summary>
        /// The margins to use throughout the document.
        /// </summary>
        public MarginSettings Margins
        {
            get
            {
                return this.margins;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                this.margins = value;
            }
        }

        /// <summary>
        /// The orientation of the output document, either Portrait or Landscape. (Default: Portrait)
        /// </summary>
        [GlobalSetting("orientation")]
        public PaperOrientation? Orientation { get; set; }

        /// <summary>
        /// The maximum depth of the outline. (Default: 4)
        /// </summary>
        [GlobalSetting("outlineDepth")]
        public int? OutlineDepth { get; set; }

        /// <summary>
        /// A path to output the converted document to.
        /// </summary>
        [GlobalSetting("out")]
        public string OutputFile { get; set; }

        /// <summary>
        /// Whether to output PDF or PostScript. (Default: PDF)
        /// </summary>
        [GlobalSetting("outputFormat")]
        public DocumentOutputFormat? OutputFormat { get; set; }

        /// <summary>
        /// A number that is added to all page numbers when printing headers, footers and table of content.
        /// </summary>
        [GlobalSetting("pageOffset")]
        public int? PageOffset { get; set; }

        /// <summary>
        /// The size of the output document.
        /// </summary>
        public PechkinPaperSize PaperSize { get; set; }

        /// <summary>
        /// Whether to generate an outline for the document. (Default: false)
        /// </summary>
        [GlobalSetting("outline")]
        public bool? ProduceOutline { get; set; }
        
        /// <summary>
        /// Whether to use lossless compression when creating the pdf file. (Default: true)
        /// </summary>
        [GlobalSetting("useCompression")]
        public bool? UseCompression { get; set; }

        [GlobalSetting("margin.bottom")]
        internal string MarginBottom
        {
            get
            {
                return this.GetMarginValue(this.margins.Bottom);
            }
        }

        [GlobalSetting("margin.left")]
        internal string MarginLeft
        {
            get
            {
                return this.GetMarginValue(this.margins.Left);
            }
        }

        [GlobalSetting("margin.right")]
        internal string MarginRight
        {
            get
            {
                return this.GetMarginValue(this.margins.Right);
            }
        }

        [GlobalSetting("margin.top")]
        internal string MarginTop
        {
            get
            {
                return this.GetMarginValue(this.margins.Top);
            }
        }

        /// <summary>
        /// The height of the output document, e.g. "12in".
        /// </summary>
        [GlobalSetting("size.height")]
        internal string PaperHeight
        {
            get
            {
                return this.PaperSize == null ? null : this.PaperSize.Height;
            }
        }

        /// <summary>
        /// The with of the output document, e.g. "4cm".
        /// </summary>
        [GlobalSetting("size.width")]
        internal string PaperWidth
        {
            get
            {
                return this.PaperSize == null ? null : this.PaperSize.Width;
            }
        }

        [GlobalSetting("colorMode")]
        internal string StringColorMode
        {
            get
            {
                if (this.ColorMode.HasValue)
                {
                    return this.ColorMode == DocumentColorMode.Color ? "color" : "grayscale";
                }
                else
                {
                    return null;
                }
            }
        }

        private string GetMarginValue(double? value)
        {
            if (!value.HasValue)
            {
                return null;
            }

            var strUnit = "in";

            switch (this.margins.Unit)
            {
                case (Unit.Centimeters):
                    strUnit = "cm";
                    break;
                case (Unit.Millimeters):
                    strUnit = "mm";
                    break;
            }

            return String.Format("{0}{1}", value.Value.ToString("0.##", CultureInfo.InvariantCulture), strUnit);
        }
    }
}