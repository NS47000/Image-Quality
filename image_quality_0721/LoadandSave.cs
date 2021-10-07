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
    public class LoadandSave
    {
        public delegate void UpdateData(string word);
        public static void loadimage(out Image<Gray,byte>[,,] imageout,out Image<Gray,byte> showimage,setting config)
        {           
            imageout = new Image<Gray, byte>[config.cameranumber*config.anglenumber, 2, config.maxsample];
            string allpath = "";
            Size imagesize=new Size(0,0);
            int piece = 0;//有檔案的樣本數
            int analysistype = 0;
            if (config.imagepath.Contains("\\GD"))
            {
                analysistype =1;
            }
            else if(config.imagepath.Contains("\\NGD"))
            {
                analysistype = 2;
            }
            for (int i = 0; i < config.maxsample; i++)//將影像的亮度與銳利度存進imageout裡
            {
                for (int y = 1; y <= config.cameranumber; y++)
                {
                    for (int x = 1; x <= config.anglenumber; x++)
                    {
                        allpath = config.imagepath + (i+1).ToString() + "\\Y" + y.ToString() + "_X" + x.ToString() + ".bmp";
                        if (System.IO.File.Exists(allpath))
                        {
                            if(allpath.Contains("Y1_"))//以第一個相機的尺寸為基準
                            {
                                Image<Gray, Byte> image2 = new Image<Gray, Byte>(allpath);
                                imagesize = image2.Size;
                            }
                            Image<Gray, Byte> image1 = new Image<Gray, Byte>(allpath);
                            if(image1.Size!= imagesize)//如果尺寸不對就resize
                            {
                                Image<Gray, Byte> imageresize = new Image<Gray, byte>(imagesize);
                                CvInvoke.Resize(image1, imageresize, imagesize);
                                image1 = imageresize;
                            }
                            imageout[8 * (y - 1) + (x - 1),0,piece] = image1;//存亮度影像
                            Image<Gray, byte> laplaceimage = new Image<Gray, byte>(image1.Size);
                            CvInvoke.Laplacian(image1, laplaceimage, Emgu.CV.CvEnum.DepthType.Default);//拉氏轉換
                            imageout[8 * (y - 1) + (x - 1), 1, piece] = laplaceimage;//存銳利度影像
                        }
                    }
                }
                if (System.IO.File.Exists(config.imagepath + (i + 1).ToString() + "\\Y1_X1.bmp") || System.IO.File.Exists(config.imagepath + (i + 1).ToString() + "\\Y"+ (1+config.cameranumber).ToString() + "_X1.bmp"))
                    piece += 1;//計算真實有檔案的piece數
                ImageQualityForm.form1.ngdtextBox.Text = "正在載入:" + config.imagepath + i.ToString();
                ImageQualityForm.form1.ngdtextBox.Refresh();
            }
            Image<Gray, byte>[,] imagecombine = new Image<Gray, byte>[2, piece];
            for (int i=0;i<piece;i++)
            {
                ImageQualityForm.form1.ngdtextBox.Text = "正在拼第" + (i + 1).ToString() + "張圖";
                ImageQualityForm.form1.ngdtextBox.Refresh();
                imagecombine[0,i]= imageoperation.imagecombine(imageout, 0, i,config);//亮度影像合併後丟到imagecombine
                imagecombine[1, i]= imageoperation.imagecombine(imageout, 1, i,config);//銳利度影像合併後丟到imagecombine
                ImageQualityForm.form1.imageBox1.Image = imagecombine[0, i];
                ImageQualityForm.form1.imageBox1.Refresh();
            }
            ImageQualityForm.form1.ngdtextBox.Text = "資料儲存中";
            ImageQualityForm.form1.ngdtextBox.Refresh();
            float[,,,] data = new float[config.cameranumber*config.cutnumber,config.anglenumber*config.cutnumber, 2, imagecombine.GetLength(1)];//將圖片資料分區取平均後存到矩陣裡
            float[,,] dataavg = new float[config.cameranumber * config.cutnumber, config.anglenumber * config.cutnumber, 2];//存放所有piece的平均值
            float[,,] datasd = new float[config.cameranumber * config.cutnumber, config.anglenumber * config.cutnumber, 2];//存放所有piece的標準差
            data =imageoperation.getvalue(imagecombine, config.cameranumber * config.cutnumber, config.anglenumber * config.cutnumber);//將圖片切成16x32並存到ai裡
            dataavg = imageoperation.dataavg(data);//dataavg[16*32*type]
            datasd = imageoperation.datasd(data, dataavg);
            imageoperation.writedata(dataavg,"avg", analysistype, config.datapath);//儲存平均值資料到txt
            imageoperation.writedata(datasd,"sd", analysistype, config.datapath);//儲存標準差資料到txt
            showimage = new Image<Gray, byte>(imagecombine[0, 0].Size);//show出一張拼接後的圖片拿來顯示
            imageoperation.ImageAvgShow(imagecombine,ref showimage);
            ImageQualityForm.form1.ngdtextBox.Text = "完成";
            ImageQualityForm.form1.ngdtextBox.Refresh();
        }
        public static void loadtxt(float Threshold,ref string textshow, ref Image<Bgr, byte> imageshow,float brightoffset,float sharpoffset,setting config,int loadparametertype)
        {
            float[,,] GDavgresult = new float[config.cameranumber*config.cutnumber, config.anglenumber*config.cutnumber, 2];
            float[,,] GDsdresult = new float[config.cameranumber * config.cutnumber, config.anglenumber * config.cutnumber, 2];
            float[,,] NGDavgresult = new float[config.cameranumber * config.cutnumber, config.anglenumber * config.cutnumber, 2];
            float[,,] NGDsdresult = new float[config.cameranumber * config.cutnumber, config.anglenumber * config.cutnumber, 2];
            textshow = "";
            textshow += "";
            string typename="";
            try
            {
                imageoperation.loadGD(ref GDavgresult, ref GDsdresult, config.datapath);
                imageoperation.loadNGD(ref NGDavgresult, ref NGDsdresult, config.datapath);
            }
            catch(Exception)
            {
                MessageBox.Show("讀取txt檔失敗");
            }
            if (loadparametertype == 1)
            {
                for (int i = 0; i < config.cameranumber * config.cutnumber; i++)
                {
                    for (int j = 0; j < config.anglenumber * config.cutnumber; j++)
                    {
                        for (int type = 0; type < 2; type++)
                        {
                            if (type == 0)
                            {
                                if (NGDavgresult[i, j, type] > GDavgresult[i, j, type] + Threshold * GDsdresult[i, j, type] + brightoffset || NGDavgresult[i, j, type] < GDavgresult[i, j, type] - Threshold * GDsdresult[i, j, type] - brightoffset)
                                {
                                    Rectangle rectangle = new Rectangle(j * (imageshow.Width / (config.anglenumber * config.cutnumber)), i * (imageshow.Height / (config.cameranumber * config.cutnumber)), imageshow.Width / (config.anglenumber * config.cutnumber), imageshow.Height / (config.cameranumber * config.cutnumber));
                                    imageshow.Draw(rectangle, new Bgr(Color.Red), 15);
                                    typename = "bright error";
                                    textshow += "從上數來:" + (i + 1).ToString() + "從左數來:" + (j + 1).ToString() + Environment.NewLine + "種類:" + typename + Environment.NewLine + "允許區間:" + (GDavgresult[i, j, type] - Threshold * GDsdresult[i, j, type] - brightoffset).ToString() + "~" + (GDavgresult[i, j, type] + Threshold * GDsdresult[i, j, type] + brightoffset).ToString() + Environment.NewLine + "error數值:" + NGDavgresult[i, j, type].ToString() + Environment.NewLine;
                                }
                            }
                            if (type == 1)
                            {
                                if (NGDavgresult[i, j, type] > GDavgresult[i, j, type] + Threshold * GDsdresult[i, j, type] + sharpoffset || NGDavgresult[i, j, type] < GDavgresult[i, j, type] - Threshold * GDsdresult[i, j, type] - sharpoffset)
                                {
                                    Rectangle rectangle = new Rectangle((j * (imageshow.Width / (config.anglenumber * config.cutnumber))) + 15, (i * (imageshow.Height / (config.cameranumber * config.cutnumber))) + 15, (imageshow.Width / (config.anglenumber * config.cutnumber)) - 30, (imageshow.Height / (config.cameranumber * config.cutnumber)) - 30);
                                    imageshow.Draw(rectangle, new Bgr(Color.Blue), 15);
                                    typename = "sharp error";
                                    textshow += "從上數來:" + (i + 1).ToString() + "從左數來:" + (j + 1).ToString() + Environment.NewLine + "種類:" + typename + Environment.NewLine + "允許區間:" + (GDavgresult[i, j, type] - Threshold * GDsdresult[i, j, type] - sharpoffset).ToString() + "~" + (GDavgresult[i, j, type] + Threshold * GDsdresult[i, j, type] + sharpoffset).ToString() + Environment.NewLine + "error數值:" + NGDavgresult[i, j, type].ToString() + Environment.NewLine;
                                }
                            }
                        }

                    }
                }
            }
            else if(loadparametertype == 2)
            {
                for (int i = 0; i < config.cameranumber * config.cutnumber; i++)
                {
                    for (int j = 0; j < config.anglenumber * config.cutnumber; j++)
                    {
                        for (int type = 0; type < 2; type++)
                        {
                            if (type == 0)
                            {
                                if (NGDsdresult[i, j, type] >  Threshold * GDsdresult[i, j, type] + brightoffset)
                                {
                                    Rectangle rectangle = new Rectangle(j * (imageshow.Width / (config.anglenumber * config.cutnumber)), i * (imageshow.Height / (config.cameranumber * config.cutnumber)), imageshow.Width / (config.anglenumber * config.cutnumber), imageshow.Height / (config.cameranumber * config.cutnumber));
                                    imageshow.Draw(rectangle, new Bgr(Color.Red), 15);
                                    typename = "bright deviation error";
                                    textshow += "從上數來:" + (i + 1).ToString() + "從左數來:" + (j + 1).ToString() + Environment.NewLine + "種類:" + typename + Environment.NewLine + "允許區間:" + (Threshold * GDsdresult[i, j, type] + brightoffset).ToString() + Environment.NewLine + "error數值:" + NGDsdresult[i, j, type].ToString() + Environment.NewLine;
                                }
                            }
                            if (type == 1)
                            {
                                if (NGDsdresult[i, j, type] > Threshold * GDsdresult[i, j, type] + sharpoffset )
                                {
                                    Rectangle rectangle = new Rectangle((j * (imageshow.Width / (config.anglenumber * config.cutnumber))) + 15, (i * (imageshow.Height / (config.cameranumber * config.cutnumber))) + 15, (imageshow.Width / (config.anglenumber * config.cutnumber)) - 30, (imageshow.Height / (config.cameranumber * config.cutnumber)) - 30);
                                    imageshow.Draw(rectangle, new Bgr(Color.Blue), 15);
                                    typename = "sharp deviation error";
                                    textshow += "從上數來:" + (i + 1).ToString() + "從左數來:" + (j + 1).ToString() + Environment.NewLine + "種類:" + typename + Environment.NewLine + "允許區間:" + (Threshold * GDsdresult[i, j, type] + sharpoffset).ToString() + Environment.NewLine + "error數值:" + NGDsdresult[i, j, type].ToString() + Environment.NewLine;
                                }
                            }
                        }

                    }
                }
            }
            string write = "";//準備將容許範圍寫入table.txt
            write += "bright:" + Environment.NewLine;
            for (int i = 0; i < config.cameranumber * config.cutnumber; i++)
            {
                for (int j = 0; j < config.anglenumber * config.cutnumber; j++)
                {
                    
                    float min = GDavgresult[i, j,0] - Threshold * GDsdresult[i, j,0];
                    float max = GDavgresult[i, j,0] + Threshold * GDsdresult[i, j,0];
                    write += string.Format("{0:000.00}", min) + "~" + string.Format("{0:000.00}", max) + ",";
                }
                write += Environment.NewLine;
            }
            write += "laplace:" + Environment.NewLine;
            for (int i = 0; i < config.cameranumber * config.cutnumber; i++)
            {
                for (int j = 0; j < config.anglenumber * config.cutnumber; j++)
                {

                    float min = GDavgresult[i, j, 1] - Threshold * GDsdresult[i, j, 1];
                    float max = GDavgresult[i, j, 1] + Threshold * GDsdresult[i, j, 1];
                    write += string.Format("{0:00.000}", min)+"~"+string.Format("{0:00.000}", max) + ",";
                }
                write += Environment.NewLine;
            }
            StreamWriter txtwrite = new StreamWriter(config.datapath+ @"\table.txt");//寫入txt
            txtwrite.Write( write);
            txtwrite.Close();
        }

    }
}
