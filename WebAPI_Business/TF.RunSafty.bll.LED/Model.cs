using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.LED
{
    public class SimpleTrainman
    {
        public SimpleTrainman()
        { }

        //人员编号
        public string trianmanGUID;

        //人员工号
        public string TrainmanNo;

        //人员姓名
        public string TrainmanName;

        //职务
        public string DutyName;

        //人员交路
        public string TrainmanJiaoLuGUID;

        //请假类型
        public string LeaveTypeName;

        //请假开始时间
        public DateTime LeaveStartTime;

        //请假结束时间
        public DateTime leaveEndTime;

    }

    public class GroupItem
    {
        public GroupItem()
        { }

        //序号
        public string Index;

        //组内人员信息
        public SimpleTrainmanList TrainmanList = new SimpleTrainmanList();

        //退勤时间
        public string EndWorkTime;

    }

    public class SimpleTrainmanList : List<SimpleTrainman>
    {
    }

    public class GroupList : List<GroupItem>
    {
    }

    public class LedFile
    {
        public LedFile()
        { }

        //nid
        public int nid;

        //文件GUID
        public string strFileGUID;

        //车间编号
        public string strWorkShopGUID;

        //更新日期
        public DateTime DtUpdate;

        //文件路径
        public string strFilePathName;

        //文件名称
        public string strFileName;

        //客户端编号
        public string clientid;

    }

    public class LedFileList : List<LedFile>
    {
    }
}
