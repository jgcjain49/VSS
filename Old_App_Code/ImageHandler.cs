using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
namespace Admin_CommTrex
{
    public enum ImageHandlerImgQuality : long
    {
        /// <summary>
        /// Small in size but quality is not good.
        /// </summary>
        Small = 10L,
        /// <summary>
        /// Balanced size and quality.
        /// </summary>
        Medimum = 50L,
        /// <summary>
        /// Max in size but Best quality.
        /// </summary>
        Max = 100L
    }

    public enum OutputImageExtension
    {
        /// <summary>
        /// Converts image to jpeg.
        /// </summary>
        Jpeg,
        /// <summary>
        /// Converts image to png.
        /// </summary>
        Png,
        /// <summary>
        /// Converts image to gif.
        /// </summary>
        Gif,
        /// <summary>
        /// Keeps image extension same as source.
        /// </summary>
        Same
    }

    class ImageHandler
    {
        /// <summary>
        /// Method to resize, convert and save the image.
        /// </summary>
        /// <param name="pStrImagePath">Bitmap image path.</param>
        /// <param name="pIntimgWidth">resize width.</param>
        /// <param name="pIntHeight">resize height.</param>
        /// <param name="enImgQuality">quality setting value.</param>
        /// <param name="pStrDestionFilePath">file path.</param>      
        public void Resize(string pStrImagePath, int pIntimgWidth, int pIntHeight, string pStrDestionFilePath, ImageHandlerImgQuality enImgQuality, OutputImageExtension enConvertTo)
        {
            Bitmap image = new Bitmap(pStrImagePath);
            // Get the image's original width and height
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            // To preserve the aspect ratio
            float ratioX = (float)pIntimgWidth / (float)originalWidth;
            float ratioY = (float)pIntHeight / (float)originalHeight;
            float ratio = Math.Min(ratioX, ratioY);

            // New width and height based on aspect ratio
            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            // Convert other formats (including CMYK) to RGB.
            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            // Draws the image in the specified size with quality mode set to HighQuality
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            // Get an ImageCodecInfo object that represents the JPEG codec.
            ImageCodecInfo imageCodecInfo = this.GetEncoderInfo(ImageFormat.Jpeg);
            switch (enConvertTo)
            {
                case OutputImageExtension.Jpeg:
                    // Already set
                    break;
                case OutputImageExtension.Png:
                    imageCodecInfo = this.GetEncoderInfo(ImageFormat.Png);
                    break;
                case OutputImageExtension.Gif:
                    imageCodecInfo = this.GetEncoderInfo(ImageFormat.Gif);
                    break;
                case OutputImageExtension.Same:
                    switch (Path.GetExtension(pStrImagePath).Replace(".", "").ToUpper())
                    {
                        case "JPEG":
                        case "JPG":
                            // Already set
                            break;
                        case "PNG":
                            imageCodecInfo = this.GetEncoderInfo(ImageFormat.Png);
                            break;
                        case "GIF":
                            imageCodecInfo = this.GetEncoderInfo(ImageFormat.Gif);
                            break;
                    }
                    break;
            }

            // Create an Encoder object for the Quality parameter.
            Encoder encoder = Encoder.Quality;

            // Create an EncoderParameters object. 
            EncoderParameters encoderParameters = new EncoderParameters(1);

            // Save the image as a JPEG file with quality level.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, (long)enImgQuality);
            encoderParameters.Param[0] = encoderParameter;
            newImage.Save(pStrDestionFilePath, imageCodecInfo, encoderParameters);
            image.Dispose(); 
           // File.Delete(pStrImagePath);
        }

        /// <summary>
        /// Method to get encoder infor for given image format.
        /// </summary>
        /// <param name="format">Image format</param>
        /// <returns>image codec info.</returns>
        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }
    }

}
