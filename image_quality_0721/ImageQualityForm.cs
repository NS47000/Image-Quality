using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System.IO;
using Emgu.CV.Aruco;
using Emgu.CV.CvEnum;

namespace image_quality_0721
{
    public partial class ImageQualityForm : Form
    {
        string imagepath = "";
        public ImageQualityForm()
        {
            InitializeComponent();
            comboBoxSelectImage.Enabled = false;
            loadbutton.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxAVGorSD.SelectedIndex = 0;
        }

        private void loadbutton_Click(object sender, EventArgs e)//讀取圖片資訊，計算後將資料存入TXT中
        {
            Image<Gray, byte> [,,] imagedata;
            Image<Gray, byte> imageshow;
            ngdtextBox.Text = "圖片讀取中，請稍後";
            ngdtextBox.Refresh();
            setting config = new setting(9, 8, 5, 50, imagepath, "D:\\test");//設定(相機數量,角度數量,一張圖切成幾成幾,分析sample的最大數量(比實際sample數量多即可),影像位置,存資料位置)
            LoadandSave.loadimage( out imagedata, out imageshow, config);
            //imagepath輸入檔案資料夾位置,imagedata輸出全部拼接後的影像,imageshow顯示拼接後的亮度影像,maxsample最大樣本數,savedatapath存txt檔之位置
            imageBox1.Image = imageshow;//SHOW出拼接後的亮度影像
            if (gdcomboBox.SelectedIndex == 0)
            {
                CvInvoke.Imwrite(config.datapath + "\\golden.bmp", imageshow);//將拼接後的亮度影像儲存，給別的function畫圖用
            }
            else if (gdcomboBox.SelectedIndex == 1)
            {
                CvInvoke.Imwrite(config.datapath + "\\normal.bmp", imageshow);//將拼接後的亮度影像儲存，給別的function畫圖用
            }
            ngdtextBox.Clear();
        }

        private void tablebutton_Click(object sender, EventArgs e)//將TXT的資料拿出來使用並框出目前超出容許範圍之區域
        {
            setting config = new setting(9, 8, 5, 50, imagepath, "D:\\test");//設定(相機數量,角度數量,一張圖切成幾成幾,影像位置,存資料位置)
            comboBoxSelectImage.Enabled = true;
            Image<Gray, byte> imageshowsrc = new Image<Gray, Byte>(config.datapath+"\\normal.bmp");//取出用來畫圖的影像
            Image<Bgr, byte> imageshow = imageshowsrc.Convert<Bgr, byte>();//轉彩色
            string textshow = "";//顯示錯誤狀況用的
            float threshold = Convert.ToSingle(textBox1.Text);//讀取閥值參數
            float brightoffset = Convert.ToSingle(brighttextBox.Text);//讀取亮度閥值OFFSET
            float sharpoffset = Convert.ToSingle(sharptextBox.Text);//讀取銳利度閥值OFFSET
            //List < Rectangle> errorroi=new List<Rectangle>();
            int loadparametertype = 0;//選擇比較的參數
            if (comboBoxAVGorSD.SelectedIndex == 0)//看mean偏差
                loadparametertype = 1;
            else if(comboBoxAVGorSD.SelectedIndex == 1)//看dev差值
                loadparametertype = 2;
            LoadandSave.loadtxt(threshold, ref textshow,ref imageshow, brightoffset, sharpoffset,config, loadparametertype);
            //利用輸入閥值分析TXT檔案中的資訊，並將不合理的區域在imageshow中圈出來
            imageBox1.Image = imageshow;
            ngdtextBox.Text = textshow;
        }

        private void comboBoxSelectImage_SelectedIndexChanged(object sender, EventArgs e)//選擇顯示golden或者normal的圖片
        {
            setting config = new setting(9, 8, 5, 50, imagepath, "D:\\test");
            string textshow = "";//顯示錯誤狀況用的
            float threshold = Convert.ToSingle(textBox1.Text);//讀取閥值參數
            float brightoffset = Convert.ToSingle(brighttextBox.Text);//讀取亮度閥值OFFSET
            float sharpoffset = Convert.ToSingle(sharptextBox.Text);//讀取銳利度閥值OFFSET
            int loadparametertype = 0;//看顯示的是mean還是deviation
            if (comboBoxAVGorSD.SelectedIndex == 0)//看mean偏差
                loadparametertype = 1;
            else if (comboBoxAVGorSD.SelectedIndex == 1)//看dev差值
                loadparametertype = 2;
            if (comboBoxSelectImage.SelectedIndex == 0)
            {
                Image<Gray, byte> imageshowsrc = new Image<Gray, Byte>(config.datapath+"\\golden.bmp");
                Image<Bgr, byte> imageshow = imageshowsrc.Convert<Bgr, byte>();//轉彩色
                LoadandSave.loadtxt(threshold, ref textshow, ref imageshow, brightoffset, sharpoffset, config, loadparametertype);
                imageBox1.Image = imageshow;
            }
            else if (comboBoxSelectImage.SelectedIndex == 1)
            {
                Image<Gray, byte> imageshowsrc = new Image<Gray, Byte>(config.datapath + "\\normal.bmp");
                Image<Bgr, byte> imageshow = imageshowsrc.Convert<Bgr, byte>();//轉彩色
                LoadandSave.loadtxt(threshold, ref textshow, ref imageshow, brightoffset, sharpoffset, config, loadparametertype);
                imageBox1.Image = imageshow;
            }
            imageBox1.Refresh();
        }//觀看影像的選擇

        private void gdcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gdcomboBox.SelectedIndex == 0)
            {
                imagepath = "D:\\test\\ImageData\\GD";//golden sample的圖檔資料夾名稱
            }
            else if (gdcomboBox.SelectedIndex == 1)
            {
                imagepath = "D:\\test\\ImageData\\NGD";//normal sample的圖檔資料夾名稱
            }
            loadbutton.Enabled = true;
        }

        private void brightsetlabel_Click(object sender, EventArgs e)
        {

        }
    }
}
