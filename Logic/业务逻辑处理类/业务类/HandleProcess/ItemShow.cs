using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace TF.RunSafty.Logic
{
    /// <summary>
    ///ItemShow 的摘要说明
    /// </summary>
    public class ItemShow
    {
        public ItemShow()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 绑定取证信息
        /// </summary>
        public static string BindQzInfo(string TableName,string StrID)
        {
            string ListHtml = "";
            try
            {
                
                DataTable dtProofFilesRecord = TF.RunSafty.Logic.ProofFiles.ProofFilesRecord("and strTableName='" + TableName + "' and strRecordID='" + StrID + "' and nExpand=0 ");
                for (int i = 0; i < dtProofFilesRecord.Rows.Count; i++)
                {
                    switch (PageBase.static_ext_int(dtProofFilesRecord.Rows[i]["nType"]))
                    {
                        case 0:
                            ListHtml+= "<div class='l mt5 ml5'><div><a href='#' onclick=\"artDialogOpen(encodeURI('/Page/HandleProcess/Pg_RecordPhoto.aspx?url=/文件/机车整备场管理信息系统/取证文件/" + PageBase.static_ext_date1(dtProofFilesRecord.Rows[i]["dtDateTime"], "yyyyMMdd") + "/" + dtProofFilesRecord.Rows[i]["strFileName"] + "&type= " + dtProofFilesRecord.Rows[i]["nType"].ToString() + "'), '查看取证',700,500)\"><img width='73' height='74' src=\"/文件/机车整备场管理信息系统/取证文件/" + PageBase.static_ext_date1(dtProofFilesRecord.Rows[i]["dtDateTime"], "yyyyMMdd") + "/" + dtProofFilesRecord.Rows[i]["strFileName"].ToString() + "\"/></a></div><div class='c' style='text-align:center; font-size:13px;'>" + (i+1) + "</div></div>";
                            break;
                        case 1:
                            ListHtml += "<div class='l mt5 ml5'><div><a href='#' onclick=\"artDialogOpen(encodeURI('/Page/HandleProcess/Pg_RecordPhoto.aspx?url=/文件/机车整备场管理信息系统/取证文件/" + PageBase.static_ext_date1(dtProofFilesRecord.Rows[i]["dtDateTime"], "yyyyMMdd") + "/" + dtProofFilesRecord.Rows[i]["strFileName"] + "&type= " + dtProofFilesRecord.Rows[i]["nType"].ToString() + "'), '查看取证',700,500)\"><img src=\"/_Images/lx.png\"/></a></div><div class='c' style='text-align:center; font-size:13px;'>" + (i + 1) + "</div></div>";
                            break;
                        case 2:
                            ListHtml += "<div class='l mt5 ml5'><div><a href='#' onclick=\"artDialogOpen(encodeURI('/Page/HandleProcess/Pg_RecordPhoto.aspx?url=/文件/机车整备场管理信息系统/取证文件/" + PageBase.static_ext_date1(dtProofFilesRecord.Rows[i]["dtDateTime"], "yyyyMMdd") + "/" + dtProofFilesRecord.Rows[i]["strFileName"] + "&type= " + dtProofFilesRecord.Rows[i]["nType"].ToString() + "'), '查看取证',700,500)\"><img src=\"/_Images/ly.png\"/></a></div><div class='c' style='text-align:center; font-size:13px;'>" + (i + 1) + "</div></div>";
                            break;
                    }
                }
                
            }
            catch(Exception ex)
            {
                PageBase.log("整备步骤取证文件信息："+ex.Message);
            }
            return ListHtml;
        }

        /// <summary>
        /// 绑定取证信息
        /// </summary>
        public static string BindQzInfoashx(string TableName, string StrID)
        {
            string ListHtml = "";
            try
            {

                DataTable dtProofFilesRecord = TF.RunSafty.Logic.ProofFiles.ProofFilesRecord("and strTableName='" + TableName + "' and strRecordID='" + StrID + "' and nExpand=0 and nType=2");
                for (int i = 0; i < dtProofFilesRecord.Rows.Count; i++)
                {
                    ListHtml += "<a href='#' onclick=\\\"artDialogOpen(encodeURI('/Page/HandleProcess/Pg_RecordPhoto.aspx?url=/文件/机车整备场管理信息系统/取证文件/" + PageBase.static_ext_date1(dtProofFilesRecord.Rows[i]["dtDateTime"], "yyyyMMdd") + "/" + dtProofFilesRecord.Rows[i]["strFileName"] + "&type= " + dtProofFilesRecord.Rows[i]["nType"].ToString() + "'), '查看取证',700,500)\\\">录音" + (i + 1) + "</a></br>";
                }

            }
            catch (Exception ex)
            {
                PageBase.log("整备步骤取证文件信息：" + ex.Message);
            }

            return ListHtml;
        }
    }
}