using System;
using System.IO;
using System.Collections;

namespace TF.CommonUtility
{
    /// <summary>
    /// 类名:FileUtil
    /// 描述:通用文件操作类
    /// </summary>
   public sealed class FileUtil
    {
        /// <summary>
        /// 获取文件名(包含扩展名)
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static String GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        /// <summary>
        /// 获取不含扩展名的文件名
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        /// <summary>
        /// 获取扩展名
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static String GetExtension(string filePath)
        {
            return Path.GetExtension(filePath);
        }

        /// <summary>
        /// 读取文本文件内容,每行存入arrayList 并返回arrayList对象
        /// </summary>
        /// <param name="sFileName"></param>
        /// <returns>arrayList</returns>
        public static ArrayList ReadFileRow(string sFileName)
        {
            string sLine = "";
            ArrayList alTxt = null;
            try
            {
                using (StreamReader sr = new StreamReader(sFileName))
                {
                    alTxt = new ArrayList();

                    while (!sr.EndOfStream)
                    {
                        sLine = sr.ReadLine();
                        if (sLine != "")
                        {
                            alTxt.Add(sLine.Trim());
                        }

                    }
                    sr.Close();
                }
            }
            catch
            {

            }
            return alTxt;
        }


        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <param name="overwrite">当目标文件存在时是否覆盖</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!System.IO.File.Exists(sourceFileName))
                throw new FileNotFoundException(sourceFileName + "文件不存在！");

            if (!overwrite && System.IO.File.Exists(destFileName))
                return false;

            try
            {
                System.IO.File.Copy(sourceFileName, destFileName, true);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 备份文件,当目标文件存在时覆盖
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName)
        {
            return BackupFile(sourceFileName, destFileName, true);
        }


        /// <summary>
        /// 恢复文件
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要恢复的文件名</param>
        /// <param name="backupTargetFileName">要恢复文件再次备份的名称,如果为null,则不再备份恢复文件</param>
        /// <returns>操作是否成功</returns>
        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            try
            {
                if (!System.IO.File.Exists(backupFileName))
                    throw new FileNotFoundException(backupFileName + "文件不存在！");

                if (backupTargetFileName != null)
                {
                    if (!System.IO.File.Exists(targetFileName))
                        throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                    else
                        System.IO.File.Copy(targetFileName, backupTargetFileName, true);
                }
                System.IO.File.Delete(targetFileName);
                System.IO.File.Copy(backupFileName, targetFileName);
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        /// <summary>
        /// 恢复文件（不再备份恢复文件）
        /// </summary>
        /// <param name="backupFileName"></param>
        /// <param name="targetFileName"></param>
        /// <returns></returns>
        public static bool RestoreFile(string backupFileName, string targetFileName)
        {
            return RestoreFile(backupFileName, targetFileName, null);
        }
        //看文件或文件夹是否存在
        public static bool FileExists(string strPath)
        {
            if (!File.Exists(strPath))
            {
                return false;
            }
            return true;
        }
    }
}
