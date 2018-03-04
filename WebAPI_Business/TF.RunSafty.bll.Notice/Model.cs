using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Notice
{
    public class LEDFile
    {
        public LEDFile()
        { }

        //文件编号
        public string strFileGUID;

        //文件名称
        public string strFileName;

        //阅读时间
        public DateTime dtReadTime;
        public string strTypeGUID;
        public string strfilePathName;
        public string strType;
        public string nReadInterval;
    }

    public class FileType
    {
        public FileType()
        { }

        //类型编号
        public string strTypeGUID;

        //类型名称
        public string strTypeName;

        //文件列表
        public LEDFile FileList = new LEDFile();

    }


    public class TypeList : List<FileType>
    {
    }
}
