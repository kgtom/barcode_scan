using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace barcode
{
    public partial class BarCode : Form
    {

        List<FileInfo> uploadFileList = new List<FileInfo>();

        public BarCode()
        {
            InitializeComponent();
            // 加入这行
            Control.CheckForIllegalCrossThreadCalls = false;
            LoadListView();
        }
        /// <summary>  
        /// 初始化上传列表  
        /// </summary>  
        void LoadListView()
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("文件名", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("条形码", 100, HorizontalAlignment.Center);

        }
       

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_test_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "img files (*.jpg)|*.jpg|All files (*.*)|*.*";
            ofd.Multiselect = true;           
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int i = 0;
                    foreach (string fileName in ofd.FileNames)
                    {
                        FileInfo fi = new FileInfo(fileName);
                        ListViewItem lvi = new ListViewItem(Path.GetFileNameWithoutExtension(fileName));
                        lvi.Text = ofd.SafeFileNames[i];
                        lvi.SubItems.Add(ScanBarCode(fileName));                     
                        lvi.SubItems.Add(fileName);
                        listView1.Items.Add(lvi);
                        i++;

                    }                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            
        }



       
        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            this.txtImgPath.Text = path.SelectedPath;

            //C#遍历指定文件夹中的所有文件 
            DirectoryInfo theFolder = new DirectoryInfo(path.SelectedPath);
            //遍历文件夹
            foreach (DirectoryInfo NextFolder in theFolder.GetDirectories())
            {

                MessageBox.Show("请选择图片所在的文件夹");
                return;
            }



            uploadFileList = theFolder.GetFiles().ToList();
            this.toolStripProgressBar1.Visible = true;
            this.toolStripProgressBar1.Maximum = uploadFileList.Count();
            listView1.Items.Clear();
            Task.Run(() =>
            {

               
                BindDataLisView();

            });
        




            //this.toolStripProgressBar1.Visible = true;
            //this.toolStripProgressBar1.Maximum = theFolder.GetFiles().Count();
            //int completeCount = 0;
            ////遍历文件
            //foreach (FileInfo NextFile in theFolder.GetFiles())
            //{
            //    //this.listBox2.Items.Add(NextFile.Name);
            //    //this.listBox2.Items.Add(NextFile.FullName);

            //    this.SetProgress(++completeCount);
            //    this.SetInfoText(string.Format("正在努力处理第{1}条，数据：{0}", NextFile.Name, completeCount));

            //    FileInfo fi = new FileInfo(NextFile.Name);
            //    ListViewItem lvi = new ListViewItem(Path.GetFileNameWithoutExtension(NextFile.Name));
            //    lvi.Tag = NextFile.Name;
            //    lvi.SubItems.Add(ScanBarCode(NextFile.FullName));

            //    lvi.SubItems.Add(NextFile.FullName);


            //    listView1.Items.Add(lvi);

            //}


        }


        //绑定 listView
        private void BindDataLisView()
        {
           
            int completeCount = 0;
            int successCount = 0;

            //this.BeginInvoke(new Action(() =>
            //{
            var start = DateTime.Now;

            foreach (var NextFile in uploadFileList)
                {

                    if (listView1.Items.Count == 999)
                    {
                        Thread.Sleep(5000);
                    }
                    this.SetProgress(++completeCount);
                this.SetInfoText(string.Format("正在努力处理第{1}条,共{2}条，数据：{0}", NextFile.Name, completeCount, uploadFileList.Count));

                    FileInfo fi = new FileInfo(NextFile.Name);
                    ListViewItem lvi = new ListViewItem(Path.GetFileNameWithoutExtension(NextFile.Name));
                    lvi.Text = NextFile.Name;
                var barcode = ScanBarCode(NextFile.FullName);
                if (!string.IsNullOrEmpty(barcode))
                {
                    ++successCount;
                }
                    lvi.SubItems.Add(barcode);
                    lvi.SubItems.Add(NextFile.FullName);
                    listView1.Items.Add(lvi);

                   
                }

            var stamp = DateTime.Now - start;
            //}));
            this.SetInfoText(string.Format("数据处理完成,成功扫描{0}条,未识别{1}条,耗时{2}s", successCount,uploadFileList.Count-successCount,Math.Floor(stamp.TotalSeconds)));
        }

        /// <summary>
        /// 设置进度条文本
        /// </summary>
        /// <param name="text"></param>
        private void SetInfoText(string text)
        {
            this.BeginInvoke(new Action(() =>
            {
                this.toolStripLabel1.Text = text;
            }));
        }

        /// <summary>
        /// 设置进度条百分比
        /// </summary>
        /// <param name="progress"></param>
        private void SetProgress(int progress)
        {
            this.BeginInvoke(new Action(() =>
            {
                this.toolStripProgressBar1.Value = progress;
            }));
        }

        // <summary>
        /// 条码识别
        /// </summary>
        private string ScanBarCode(string fileName)
        {
            DateTime now = DateTime.Now;
            Image primaryImage = Image.FromFile(fileName);

            Bitmap pImg = MakeGrayscale3((Bitmap)primaryImage);
            using (ZBar.ImageScanner scanner = new ZBar.ImageScanner())
            {
                scanner.SetConfiguration(ZBar.SymbolType.None, ZBar.Config.Enable, 0);
                scanner.SetConfiguration(ZBar.SymbolType.CODE39, ZBar.Config.Enable, 1);
                scanner.SetConfiguration(ZBar.SymbolType.CODE128, ZBar.Config.Enable, 1);

                List<ZBar.Symbol> symbols = new List<ZBar.Symbol>();
                symbols = scanner.Scan((Image)pImg);

                if (symbols != null && symbols.Count > 0)
                {
                    string result = string.Empty;
                    //this.ContentTxt.Text = symbols.FirstOrDefault().Data;
                    return symbols.FirstOrDefault().Data;
                    // symbols.ForEach(s => result += "条码内容:" + s.Data + " 条码质量:" + s.Quality + Environment.NewLine);
                    //MessageBox.Show(result);
                }
                else
                {
                    //this.ContentTxt.Text = "未识别";
                    return "";
                }
            }
        }

        /// <summary>
        /// 处理图片灰度
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static Bitmap MakeGrayscale3(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix(
               new float[][]
              {
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
              });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }

        /// <summary>
        /// 选中listview数据行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.FocusedItem != null)
            {
                this.txtImgName.Text = this.listView1.FocusedItem.SubItems[0].Text;
                this.barCodeImg.Image = Bitmap.FromFile(this.listView1.FocusedItem.SubItems[2].Text);
                this.txtImgPath.Text = this.listView1.FocusedItem.SubItems[2].Text;
                this.txtBarcode.Text = this.listView1.FocusedItem.SubItems[1].Text;
            }
        }

    }
}
