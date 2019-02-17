﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.Common
{
    public static class ImageUtility
    {
        public static Bitmap GetImageWithRatioSize(Image stream, double widthRatio, double heightRatio)

        {

            using (var image = stream)

            {

                var ratioHeight = image.Height * heightRatio;

                var ratioWidth = image.Width * widthRatio;



                var originalWidth = image.Width;

                var originalHeight = image.Height;



                int newWidth;

                int newHeight;



                if (originalWidth > ratioWidth || originalHeight > ratioHeight)

                {

                    var ratioX = ratioWidth / originalWidth;

                    var ratioY = ratioHeight / originalHeight;

                    var ratio = Math.Min(ratioX, ratioY);

                    newWidth = (int)(originalWidth * ratio);

                    newHeight = (int)(originalHeight * ratio);

                }

                else

                {

                    newWidth = originalWidth;

                    newHeight = originalHeight;

                }



                return new Bitmap(image, new Size(newWidth, newHeight));



            }

        }



        public static Bitmap CropImage(Bitmap srcImage, Rectangle cropArea)

        {

            return srcImage.Clone(cropArea, srcImage.PixelFormat);

        }



        public static Rectangle GetCropArea(Bitmap data, int padding)

        {

            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;



            //// find x1

            //for (var i = 0; i < data.Width; i++)

            //{

            //    for (var j = 0; j < data.Height; j++)

            //    {

            //        if (!HasOpacity(data.GetPixel(i, j))) continue;

            //        x1 = i;

            //        break;

            //    }

            //    if (x1 > 0)

            //        break;

            //}



            // find y1

            for (var j = 0; j < data.Height; j++)

            {

                for (var i = x1; i < data.Width; i++)

                {

                    if (!HasOpacity(data.GetPixel(i, j))) continue;

                    y1 = j;

                    break;

                }

                if (y1 > 0)

                    break;

            }



            // find x2

            for (var i = data.Width - 1; i > x1; i--)

            {

                for (var j = data.Height - 1; j > y1; j--)

                {

                    if (!HasOpacity(data.GetPixel(i, j))) continue;

                    x2 = i;

                    break;

                }

                if (x2 > 0)

                    break;

            }



            // find y2

            for (var j = data.Height - 1; j > y1; j--)

            {

                for (var i = x2; i > x1; i--)

                {

                    if (!HasOpacity(data.GetPixel(i, j))) continue;

                    y2 = j;

                    break;

                }

                if (y2 > 0)

                    break;

            }




            x1 = data.Width - x2 - padding;

            y1 -= padding;

            x2 += padding;

            y2 += padding;

           


            var width = x2 - x1;

            var height = y2 - y1;



            return new Rectangle(x1, y1, width, height);

        }



        public static bool HasOpacity(Color c)

        {

            return c.A > 0;

        }
    }
}
