using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;

namespace TF.CommonUtility
{
    /// <summary>
    ///PageBase 的摘要说明
    /// </summary>
    public class ControlBindClass 
    {
        public ControlBindClass()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        #region DropDownList处理
        /// <summary>
        /// DropDownList数据绑定
        /// dd DropDownList id
        /// id 绑定的DataTextField
        /// text 绑定的DataValueField
        /// dt 用于绑定的数据源
        /// isall 显示全部还是请选择
        /// </summary>
        /// <param name="dd"></param>
        /// <param name="id"></param>
        /// <param name="isall"></param>
        public static void DdlistBind(DropDownList dd, string id, string text, DataTable dt, int type)
        {
            dd.DataSource = dt;
            dd.DataTextField = text;
            dd.DataValueField = id;
            dd.DataBind();
            switch (type)
            {
                case 0:
                    dd.Items.Insert(0, new ListItem("全部", ""));
                    break;
                case 1:
                    dd.Items.Insert(0, new ListItem("--请选择--", ""));
                    break;
                case 2:
                    break;
            }
        }
        #endregion

        #region CheckBoxListBind处理
        /// <summary>
        /// 绑定CheckBoxList
        /// </summary>
        /// <param name="cbl"></param>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="dt"></param>
        public static void CheckBoxListBind(CheckBoxList cbl, string id, string text, DataTable dt)
        {
            cbl.DataSource = dt;
            cbl.DataTextField = text;
            cbl.DataValueField = id;
            cbl.DataBind();
        }
        #endregion
        #region ListBox处理
        /// <summary>
        /// 绑定listbox
        /// </summary>
        /// <param name="dd"></param>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="dt"></param>
        public static void ListBoxBind(ListBox dd, string id, string text, DataTable dt)
        {
            dd.DataSource = dt;
            dd.DataTextField = text;
            dd.DataValueField = id;
            dd.DataBind();
        }


        /// <summary>
        /// b1-b2 去掉listbox b1中含有的b2项
        /// </summary>
        public static void cutb1withb2(ListBox b1, ListBox b2)
        {
            int count = b2.Items.Count;
            for (int i = 0; i < count; i++)
            {
                ListItem item = b2.Items[i];
                if (b1.Items.Contains(item))
                {
                    b1.Items.Remove(item);
                }
            }
        }

        /// <summary>
        /// 转移一个或多个
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        public static void move(ListBox b1, ListBox b2)
        {
            int count = b1.Items.Count;
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                ListItem item = b1.Items[index];
                if (b1.Items[index].Selected == true)
                {

                    b1.Items.Remove(item);
                    b2.Items.Add(item);
                    index--;
                }
                index++;
            }
        }

        /// <summary>
        /// 转移全部b1tob2
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        public static void moveall(ListBox b1, ListBox b2)
        {
            int count = b1.Items.Count;
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                ListItem item = b1.Items[index];
                b1.Items.Remove(item);
                b2.Items.Add(item);
                index--;
                index++;
            }
        }

        /// <summary>
        /// 将listbox中项值写入数组并返回
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string[] GetlistBoxItemsCount(ListBox b)
        {
            int count = b.Items.Count;
            string[] str = new string[count];
            for (int i = 0; i < count; i++)
            {
                str[i] = b.Items[i].Value;
            }
            return str;
        }

        /// <summary>
        /// 将listbox选中项值写入字符串并返回
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string GetlistBoxValues(ListBox b)
        {
            int count = b.Items.Count;
            int[] selectindex = b.GetSelectedIndices();
            string[] str = new string[count];
            string selectvalue = "";
            for (int i = 0; i < count; i++)
            {
                str[i] = b.Items[i].Value;
            }
            for (int j = 0; j < selectindex.Length; j++)
            {
                selectvalue += str[selectindex[j]].ToString() + ",";
            }
            selectvalue = selectvalue.Length > 0 ? selectvalue.Substring(0, selectvalue.Length - 1) : selectvalue;
            return selectvalue;
        }
        #endregion
    }


}
