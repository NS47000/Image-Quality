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
    public class imageoperation
    {
        public static Image<Gray,byte> imagecombine(Image<Gray, byte>[,,] imagein,int type,int piece,setting config)//影像合併用
        {
            int singleimageheight = imagein[0, 0, 0].Height;
            int singleimagewidth = imagein[0, 0, 0].Width;
            Image<Gray, byte> imageout = new Image<Gray, byte>(singleimagewidth * config.anglenumber, singleimageheight * config.cameranumber);//設定4*8的架構
            for(int i=0;i< imageout.Height;i++)
            {
                for(int j=0;j< imageout.Width;j++)
                {
                    imageout.Data[i, j, 0] = imagein[8*(i/ singleimageheight)+(j/ singleimagewidth), type, piece].Data[i% singleimageheight, j% singleimagewidth, 0];
                        
                }
            }
            return imageout;
        }
        public static float[,,,] getvalue(Image<Gray, byte>[,] imagein,int heightcut,int weightcut)//將影像資料輸出到矩陣
        {
            int height = imagein[0, 0].Height;
            int width = imagein[0, 0].Width;
            float[,,,] data = new float[heightcut, weightcut, 2, imagein.GetLength(1)];
            for (int piece = 0; piece < imagein.GetLength(1); piece++)
            {
                for (int type = 0; type < imagein.GetLength(0); type++)
                {
                    for (int m = 0; m < heightcut; m++)
                    {
                        for (int n = 0; n < weightcut; n++)
                        {
                            int sum = 0;
                            for (int i = 0; i < height / heightcut; i++)
                            {
                                for (int j = 0; j < width / weightcut; j++)
                                {
                                    sum += imagein[type, piece].Data[m * height / heightcut + i, n * width/ weightcut + j, 0];

                                }
                            }
                            data[m, n, type, piece] = sum / ((height / heightcut) * (width / weightcut));
                        }
                    }
                }
                
            }
            return data;
        }
        public static float[,,] dataavg(float[,,,] datain)//計算矩陣中每個piece平均值
        {
            float[,,] avgdata = new float[datain.GetLength(0), datain.GetLength(1), datain.GetLength(2)];
            for(int i=0;i<datain.GetLength(0);i++)
            {
                for(int j=0;j<datain.GetLength(1);j++)
                {
                    for(int type=0;type< datain.GetLength(2); type++)
                    {
                        float sum = 0;
                        float avg = 0;
                        for (int piece = 0; piece < datain.GetLength(3); piece++)
                        {
                            sum += datain[i, j, type, piece];
                        }
                        avg = sum / datain.GetLength(3);
                        avgdata[i,j,type] = avg;
                    }
                }
            }
            return avgdata;
        }
        public static float[,,] datasd(float[,,,] datain,float[,,]avgdata)//計算矩陣中每個piece之標準差
        {
            float[,,] sddata = new float[datain.GetLength(0), datain.GetLength(1), datain.GetLength(2)];
            for (int i = 0; i < datain.GetLength(0); i++)
            {
                for (int j = 0; j < datain.GetLength(1); j++)
                {
                    for (int type = 0; type < datain.GetLength(2); type++)
                    {
                        float diffsum = 0;
                        for (int piece = 0; piece < datain.GetLength(3); piece++)
                        {
                           diffsum +=Convert.ToSingle( Math.Pow( datain[i, j, type, piece]-avgdata[i,j,type],2));
                        }
                        sddata [i,j,type]=Convert.ToSingle( Math.Pow(diffsum / datain.GetLength(3), 0.5));
                    }
                }
            }
            return sddata;
        }
        public static void writedata(float[,,] datain,string title, int analysistype,string writepath)//將資料寫入txt中
        {
            string write = "";
            string oldintxt = "";
            for (int type=0;type<datain.GetLength(2);type++)
            {
                if(type==0)
                    write +="bright"+title+":"+Environment.NewLine;
                if (type == 1)
                    write += "laplace" + title + ":" + Environment.NewLine;
                for (int i=0;i<datain.GetLength(0);i++)
                {
                    for(int j=0;j<datain.GetLength(1);j++)
                    {
                        float a = datain[i, j, type];
                        write += string.Format("{0:000.000}",a) + ",";
                    }
                    write += Environment.NewLine;
                }
            }
            if (analysistype == 1)
            {
                if (title == "sd")
                {
                    if (System.IO.File.Exists(writepath+ @"\goldentabledata.txt"))
                    {
                        StreamReader olddata = new StreamReader(writepath + @"\goldentabledata.txt");//讀取舊檔案
                        while (olddata.Peek() >= 0)
                        {               // 每次讀取一行，直到檔尾
                            oldintxt += olddata.ReadLine() + Environment.NewLine;

                        }
                        olddata.Close();
                    }
                }
                if(!Directory.Exists(@writepath))
                {
                    Directory.CreateDirectory(@writepath);
                }
                StreamWriter txtwrite = new StreamWriter(writepath + @"\goldentabledata.txt");//寫入txt
                txtwrite.Write(oldintxt + write);
                txtwrite.Close();
            }
            if (analysistype == 2)
            {
                if (title == "sd")
                {
                    if (System.IO.File.Exists(writepath + @"\normaltabledata.txt"))
                    {
                        StreamReader olddata = new StreamReader(writepath + @"\normaltabledata.txt");//讀取舊檔案
                        while (olddata.Peek() >= 0)
                        {               // 每次讀取一行，直到檔尾
                            oldintxt += olddata.ReadLine() + Environment.NewLine;

                        }
                        olddata.Close();
                    }
                }
                StreamWriter txtwrite = new StreamWriter(writepath + @"\normaltabledata.txt");//寫入txt
                txtwrite.Write(oldintxt + write);
                txtwrite.Close();
            }
        }
        public static void datatype(string CurLine, ref int type,ref int i)//檢測當前字串並回傳資料類型
        {
            if (CurLine.Contains("brightavg:"))
            {
                i = 0;
                type = 1;
            }
            if (CurLine.Contains("laplaceavg:"))
            {
                i = 0;
                type = 2;
            }
            if (CurLine.Contains("brightsd:"))
            {
                i = 0;
                type = 3;
            }
            if (CurLine.Contains("laplacesd:"))
            {
                i = 0;
                type = 4;
            }
        }
        public static void loadGD(ref float[,,] GDavgresult, ref float[,,] GDsdresult,string writepath)//讀取TXT檔的資料再丟入矩陣
        {
            StreamReader loadtxt = new StreamReader(writepath + @"\goldentabledata.txt");
            String[] Piecewise;
            String CurLine;
            int i = 0;//拿來輸出矩陣第i列的資料用
            int type = 0;//看是輸出哪種矩陣(亮度平均,銳利度平均,亮度標準差,銳利度標準差)
            while (loadtxt.Peek() >= 0)
            {
                CurLine = loadtxt.ReadLine();
                imageoperation.datatype(CurLine, ref type, ref i);
                switch (type)
                {
                    case 1:

                        if (CurLine.Contains(","))
                        {
                            Piecewise = CurLine.Trim().Split(',');

                            for (int j = 0; j < Piecewise.Length - 1; j++)
                            {
                                GDavgresult[i, j, 0] = Convert.ToSingle(Convert.ToDouble(Piecewise[j]));
                            }
                            i += 1;
                        }
                        break;
                    case 2:
                        if (CurLine.Contains(","))
                        {
                            Piecewise = CurLine.Trim().Split(',');
                            for (int j = 0; j < Piecewise.Length - 1; j++)
                            {
                                GDavgresult[i, j, 1] = Convert.ToSingle(Convert.ToDouble(Piecewise[j]));
                            }
                            i += 1;
                        }
                        break;
                    case 3:
                        if (CurLine.Contains(","))
                        {
                            Piecewise = CurLine.Trim().Split(',');
                            for (int j = 0; j < Piecewise.Length - 1; j++)
                            {
                                GDsdresult[i, j, 0] = Convert.ToSingle(Convert.ToDouble(Piecewise[j]));
                            }
                            i += 1;
                        }
                        break;
                    case 4:
                        if (CurLine.Contains(","))
                        {
                            Piecewise = CurLine.Trim().Split(',');
                            for (int j = 0; j < Piecewise.Length - 1; j++)
                            {
                                GDsdresult[i, j, 1] = Convert.ToSingle(Convert.ToDouble(Piecewise[j]));
                            }
                            i += 1;
                        }
                        break;
                }
            }
            loadtxt.Close();


        }
        public static void loadNGD(ref float[,,] NGDavgresult, ref float[,,] NGDsdresult, string writepath)//讀取TXT檔的資料再丟入矩陣
        {
            StreamReader loadtxt = new StreamReader(writepath + @"\normaltabledata.txt");
            String[] Piecewise;
            String CurLine;
            int i = 0;
            int type = 0;
            while (loadtxt.Peek() >= 0)
            {
                CurLine = loadtxt.ReadLine();
                imageoperation.datatype(CurLine, ref type, ref i);
                switch (type)
                {
                    case 1:

                        if (CurLine.Contains(","))
                        {
                            Piecewise = CurLine.Trim().Split(',');

                            for (int j = 0; j < Piecewise.Length - 1; j++)
                            {
                                NGDavgresult[i, j, 0] = Convert.ToSingle(Convert.ToDouble(Piecewise[j]));
                            }
                            i += 1;
                        }
                        break;
                    case 2:
                        if (CurLine.Contains(","))
                        {
                            Piecewise = CurLine.Trim().Split(',');
                            for (int j = 0; j < Piecewise.Length - 1; j++)
                            {
                                NGDavgresult[i, j, 1] = Convert.ToSingle(Convert.ToDouble(Piecewise[j]));
                            }
                            i += 1;
                        }
                        break;
                    case 3:
                        if (CurLine.Contains(","))
                        {
                            Piecewise = CurLine.Trim().Split(',');
                            for (int j = 0; j < Piecewise.Length - 1; j++)
                            {
                                NGDsdresult[i, j, 0] = Convert.ToSingle(Convert.ToDouble(Piecewise[j]));
                            }
                            i += 1;
                        }
                        break;
                    case 4:
                        if (CurLine.Contains(","))
                        {
                            Piecewise = CurLine.Trim().Split(',');
                            for (int j = 0; j < Piecewise.Length - 1; j++)
                            {
                                NGDsdresult[i, j, 1] = Convert.ToSingle(Convert.ToDouble(Piecewise[j]));
                            }
                            i += 1;
                        }
                        break;
                }
            }
            loadtxt.Close();


        }
        public static void ImageAvgShow(Image<Gray, byte>[,] imagein,ref Image<Gray,byte> imageout)//將圖檔灰階值平均並輸出
        {
            float[,] sum = new float[imagein[0, 0].Height, imagein[0, 0].Width];
            for(int piece=0; piece < imagein.GetLength(1); piece++)
            {
                for(int i=0;i< imagein[0, 0].Height;i++)
                {
                    for (int j = 0; j < imagein[0, 0].Width; j++)
                    {
                        sum[i, j] += imagein[0, piece].Data[i, j, 0];
                    }
                }
            }
            for (int i = 0; i < imagein[0, 0].Height; i++)
            {
                for (int j = 0; j < imagein[0, 0].Width; j++)
                {
                    imageout.Data[i,j,0]=Convert.ToByte( sum[i, j] / imagein.GetLength(1));
                }
            }
        }
    }
}
