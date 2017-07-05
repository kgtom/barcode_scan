using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

namespace Barcode_scan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 生成条形码
        private void Create1DBtn_Click(object sender, EventArgs e)
        {

            Regex rg = new Regex("^[0-9]*$");
            if (!rg.IsMatch(this.ContentTxt.Text))
            {
                MessageBox.Show("需要输入数字");
                return;
            }


            // 1.设置条形码规格
            EncodingOptions encodeOption = new EncodingOptions();
            encodeOption.Height = 130; // 必须制定高度、宽度
            encodeOption.Width = 240;

            // 2.生成条形码图片并保存
            ZXing.BarcodeWriter wr = new BarcodeWriter();
            wr.Options = encodeOption;
            wr.Format = BarcodeFormat.CODE_39; //  条形码规格
            Bitmap img = wr.Write(this.ContentTxt.Text); // 生成图片
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\barcode-" + this.ContentTxt.Text + ".jpg";
            img.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);

            // 3.读取保存的图片
            this.ImgPathTxt.Text = filePath;
            this.barCodeImg.Image = img;
            MessageBox.Show("保存成功：" + filePath);
        }

        // 读取条形码
        private void Read1DBtn_Click(object sender, EventArgs e)
        {
            // 1.设置读取条形码规格
            DecodingOptions decodeOption = new DecodingOptions();
            decodeOption.PossibleFormats = new List<BarcodeFormat>() {
               BarcodeFormat.All_1D,

            };


            // 2.进行读取操作
            ZXing.BarcodeReader br = new BarcodeReader();
            br.Options = decodeOption;

            ZXing.Result rs = br.Decode(this.barCodeImg.Image as Bitmap);
            if (rs == null)
            {
                this.ContentTxt.Text = "读取失败";
                MessageBox.Show("读取失败");
            }
            else
            {
                this.ContentTxt.Text = rs.Text;
                MessageBox.Show("读取成功，内容：" + rs.Text);
            }
        }

        // 【打开图片】
        private void OpenImgBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            fileDialog.Filter = "图形文件(*.jpg)|*.jpg";
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = fileDialog.FileName;
                this.ImgPathTxt.Text = filePath;
                this.barCodeImg.Image = Bitmap.FromFile(filePath);
            }
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\barcode-" + "test001" + ".jpg";

            // CaptureImage(this.ImgPathTxt.Text, filePath, 240, 75, 420, -5);



           




            //this.ImgPathTxt.Text = @"C:\Users\tom\Desktop\行程单\2017-06-22\001.jpg";
            //  CutForSquare(FileToStream(this.ImgPathTxt.Text), filePath, 800, 80);

            //CutForCustom(FileToStream(this.ImgPathTxt.Text), filePath, 240, 130, 80);
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public Stream FileToStream(string fileName)
        {
            // 打开文件 
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[] 
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream 
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        /// 截取图片方法
        /// </summary>
        /// <param name="url">图片地址</param>
        /// <param name="beginX">开始位置-X</param>
        /// <param name="beginY">开始位置-Y</param>
        /// <param name="getX">截取宽度</param>
        /// <param name="getY">截取长度</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="fileExt">后缀名</param>
        public static void CaptureImage(string sFromFilePath, string saveFilePath, int width, int height, int spaceX, int spaceY)
        {
            //载入底图   
            Image fromImage = Image.FromFile(sFromFilePath);
            int x = 0;   //截取X坐标   
            int y = 0;   //截取Y坐标   
            //原图宽与生成图片宽   之差       
            //当小于0(即原图宽小于要生成的图)时，新图宽度为较小者   即原图宽度   X坐标则为0     
            //当大于0(即原图宽大于要生成的图)时，新图宽度为设置值   即width         X坐标则为   sX与spaceX之间较小者   
            //Y方向同理   
            int sX = fromImage.Width - width;
            int sY = fromImage.Height - height;
            if (sX > 0)
            {
                x = sX > spaceX ? spaceX : sX;
            }
            else
            {
                width = fromImage.Width;
            }
            if (sY > 0)
            {
                y = sY > spaceY ? spaceY : sY;
            }
            else
            {
                height = fromImage.Height;
            }


            //创建新图位图   
            Bitmap bitmap = new Bitmap(width, height);
            //创建作图区域   
            Graphics graphic = Graphics.FromImage(bitmap);
            //截取原图相应区域写入作图区   
            graphic.DrawImage(fromImage, 0, 0, new Rectangle(x, y,350, 90), GraphicsUnit.Point);

            //设置质量
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //graphic.Clear(Color.White);

            //从作图区生成新图   
            Image saveImage = Image.FromHbitmap(bitmap.GetHbitmap());


            //保存图象   
            //saveImage.Save(saveFilePath, ImageFormat.Jpeg);

            //关键质量控制
            //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff
            ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo i in icis)
            {
                if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                {
                    ici = i;
                }
            }
            EncoderParameters ep = new EncoderParameters(1);
            ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)200);
            saveImage.Save(saveFilePath, ici, ep);
            //释放资源   
            saveImage.Dispose();
            bitmap.Dispose();
            graphic.Dispose();
        }

        

    }
}
