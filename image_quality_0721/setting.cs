using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace image_quality_0721
{
    public class setting
    {
        public int cameranumber = 4;
        public int anglenumber = 8;
        public int cutnumber = 4;
        public int maxsample = 50;
        public string imagepath = "";
        public string datapath = "";

        public setting(int camamount,int angleamount,int cutamount,int maxsamplevalue,string imagepathset, string datapathset)
        {
            cameranumber = camamount;
            anglenumber = angleamount;
            cutnumber = cutamount;
            maxsample = maxsamplevalue;
            imagepath = imagepathset;
            datapath = datapathset;
        }
        public void change(int camamount, int angleamount, int cutamount, int maxsamplevalue, string imagepathset, string datapathset)
        {
            this.cameranumber = camamount;
            this.anglenumber = angleamount;
            this.cutnumber = cutamount;
            this.maxsample = maxsamplevalue;
            this.imagepath = imagepathset;
            this.datapath = datapathset;
        }
    }
}
