using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace image_quality_0721
{
    public class setting
    {
        internal int cameranumber = 4;
        internal int anglenumber = 8;
        internal int cutnumber = 4;
        internal int maxsample = 50;
        internal string imagepath = "";
        internal string datapath = "";
        internal float threshold;
        internal float brightoffset;
        internal float sharpoffset;
        internal int parametertype;
 

        public setting(int camamount,int angleamount,int cutamount,int maxsamplevalue,string imagepathset, string datapathset)
        {
            cameranumber = camamount;
            anglenumber = angleamount;
            cutnumber = cutamount;
            maxsample = maxsamplevalue;
            imagepath = imagepathset;
            datapath = datapathset;
        }
        public void settingthreshold(float thresholdin, float brightoffsetin, float sharpoffsetin,int parametertypein)
        {
            this.threshold = thresholdin;
            this.brightoffset = brightoffsetin;
            this.sharpoffset = sharpoffsetin;
            this.parametertype = parametertypein;
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
