using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPHubsUtil.Img
{
    /// <summary>
    /// 压缩图片
    /// </summary>
    public class ImgThumbnailUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgStr"></param>
        /// <param name="multiple">缩小倍数（0-100）</param>
        /// <param name="quality">压缩质量(数字越小压缩率越高) 1-100</param>
        /// <returns></returns>
        public byte[] thumbnail(byte[] imgStr, int multiple = 70, int quality = 60)
        {
            int width = 0;
            int height = 0;
            return magickImage(imgStr, multiple, quality, ref width, ref height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgStr"></param>
        /// <param name="multiple">缩小倍数（0-100）</param>
        /// <param name="quality">压缩质量(数字越小压缩率越高) 1-100</param>
        /// <returns></returns>
        public byte[] thumbnail(byte[] imgStr, ref int width, ref int height, int multiple = 70, int quality = 60)
        {
            return magickImage(imgStr, multiple, quality, ref width, ref height);
        }

        /// <summary>
        /// 自动压缩图片
        /// </summary>
        /// <param name="imgStr"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public byte[] thumbnail(byte[] imgStr, ref int width, ref int height)
        {
            long len = imgStr.Length;
            int multiple = 0;
            int quality = 0;

            if (len > 0 && len < 204800)
            {
                multiple = 100; quality = 100;
            }
            else if (len >= 204800 && len < 512000)
            {
                multiple = 80; quality = 60;
            }
            else if (len >= 512000 && len < 1048576)
            {
                multiple = 70; quality = 60;
            }
            else
            {
                multiple = 50; quality = 50;
            }
            return magickImage(imgStr, multiple, quality, ref width, ref height);
        }

        /// <summary>
        /// 当前方式压缩只支持Windows平台
        /// </summary>
        /// <param name="img"></param>
        /// <param name="multiple"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        private byte[] magickImage(byte[] img, int multiple, int quality, ref int width, ref int height)
        {
            MagickImage image = null;
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream();
                image = new MagickImage(img);
                image.Resize(new Percentage(multiple));
                image.Strip();
                //压缩级别，数字越小压缩率越高
                image.Quality = quality;

                image.Write(stream);
                width = image.Width;
                height = image.Height;
            }
            catch { }
            finally
            {
                image?.Dispose();
            }
            return stream.ToArray();
        }
    }
}
