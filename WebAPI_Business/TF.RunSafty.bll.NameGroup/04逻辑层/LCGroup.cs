using System;
using System.Text;
using System.Linq;
using TF.CommonUtility;
using ThinkFreely.DBUtility;

using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TF.RunSafty.NamePlate.MD;
using TF.RunSafty.NamePlate.PS;
using TF.RunSafty.NamePlate.DB;

namespace TF.RunSafty.NamePlate
{
    public class ChangeGrpJlParam
    {
        //源人员交路信息
        public TrainmanJiaoluMin SrcJiaolu = new TrainmanJiaoluMin();
        //机组信息
        public string GroupGUID;
        //机车ID
        public string TrainGUID;
        //车型车号
        public string TrainNumber;
        //车次1
        public string CheCi1;
        //车次2
        public string CheCi2;
        //目的人员交路信息
        public TrainmanJiaoluMin DestJiaolu = new TrainmanJiaoluMin();
        //值班员信息
        public DutyUser DutyUser = new DutyUser();
    }


    public class LCGroup
    {

        #region 检测指定的机组是否处于可被编辑状态(添加人员\删除人员)
        public class InCheckAddGroup
        {
            //要检测的机组
            public string GroupGUID;
        }
        public class OutCheckAddGroup
        {
            //是否检测通过(0通过，其它为不通过)
            public int Checked;
            //未通过原因
            public string CheckBrief;
        }

        /// <summary>
        /// 检测指定的机组是否处于可被编辑状态(添加人员\删除人员)
        /// </summary>
        public InterfaceOutPut CheckAddGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InCheckAddGroup InParams = javaScriptSerializer.Deserialize<InCheckAddGroup>(Data);
                OutCheckAddGroup OutParams = new OutCheckAddGroup();
                OutParams.Checked = 0;
                OutParams.CheckBrief = "初始赋值";
                string checkBrief = "";
                Group group = new Group();
                DBNameBoard.GetGroup(InParams.GroupGUID, group);
                if (!DBNameBoard.CheckTrainman(group.trainman1.trainmanNumber, ref checkBrief))
                {
                    OutParams.Checked = 1;
                    OutParams.CheckBrief = checkBrief;
                }
                if (!DBNameBoard.CheckTrainman(group.trainman2.trainmanNumber, ref checkBrief))
                {
                    OutParams.Checked = 1;
                    OutParams.CheckBrief = checkBrief;
                }
                if (!DBNameBoard.CheckTrainman(group.trainman3.trainmanNumber, ref checkBrief))
                {
                    OutParams.Checked = 1;
                    OutParams.CheckBrief = checkBrief;
                }
                if (!DBNameBoard.CheckTrainman(group.trainman4.trainmanNumber, ref checkBrief))
                {
                    OutParams.Checked = 1;
                    OutParams.CheckBrief = checkBrief;
                }

                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.CheckAddGroup:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 检测机组内人员是否已经在别的机组内
        public class InCheckGroupIsOwner
        {
            //乘务员1
            public string trainmanNumber1;
            //乘务员2
            public string trainmanNumber2;
            //乘务员3
            public string trainmanNumber3;
            //乘务员4
            public string trainmanNumber4;
        }
        public class OutCheckGroupIsOwner
        {
            //是否检测通过
            public int Checked;
            //检测不通过原因
            public string CheckBrief;
        }
        /// <summary>
        /// 检测机组内人员是否已经在别的机组内
        /// </summary>
        public InterfaceOutPut CheckGroupIsOwner(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InCheckGroupIsOwner InParams = javaScriptSerializer.Deserialize<InCheckGroupIsOwner>(Data);
                OutCheckGroupIsOwner OutParams = new OutCheckGroupIsOwner();
                OutParams.Checked = 0;
                OutParams.CheckBrief = "初始赋值";
                string checkBrief = "";
                if (!DBNameBoard.CheckTrainmanInGroup(InParams.trainmanNumber1, ref checkBrief))
                {
                    OutParams.Checked = 1;
                    OutParams.CheckBrief = checkBrief;
                }
                if (!DBNameBoard.CheckTrainmanInGroup(InParams.trainmanNumber2, ref checkBrief))
                {
                    OutParams.Checked = 1;
                    OutParams.CheckBrief = checkBrief;
                }
                if (!DBNameBoard.CheckTrainmanInGroup(InParams.trainmanNumber3, ref checkBrief))
                {
                    OutParams.Checked = 1;
                    OutParams.CheckBrief = checkBrief;
                }
                if (!DBNameBoard.CheckTrainmanInGroup(InParams.trainmanNumber4, ref checkBrief))
                {
                    OutParams.Checked = 1;
                    OutParams.CheckBrief = checkBrief;
                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.CheckGroupIsOwner:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 添加机组--------------- 添加机组（暂时没有被使用的方法）

        public class InAddGroup
        {
            //所属人员交路信息
            public TrainmanJiaoluMin TrainmanJiaolu = new TrainmanJiaoluMin();
            //值班员信息
            public DutyUser DutyUser = new DutyUser();
            //记名式交路机组
            public RRsNamedGroup NamedGroup = new RRsNamedGroup();
            //轮乘交路机组
            public TF.RunSafty.NamePlate.MD.OrderGroup OrderGroup = new TF.RunSafty.NamePlate.MD.OrderGroup();
            //包乘机组
            public RRsOrderGroupInTrain TogetherGroup = new RRsOrderGroupInTrain();
        }
        /// <summary>
        /// 添加机组
        /// </summary>
        public InterfaceOutPut AddGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InAddGroup InParams = javaScriptSerializer.Deserialize<InAddGroup>(Data);
                Group group = null;
                try
                {
                    switch (InParams.TrainmanJiaolu.jiaoluType)
                    {
                        case 2:
                            {
                                DBNameBoard.AddNamedGroup(InParams.TrainmanJiaolu.jiaoluID, InParams.NamedGroup);
                                group = InParams.NamedGroup.Group;
                                break;
                            }
                        case 3:
                            {
                                DBNameBoard.AddOrderGroup(InParams.TrainmanJiaolu.jiaoluID, InParams.OrderGroup);
                                group = InParams.OrderGroup.group;
                                break;
                            }
                        case 4:
                            {
                                DBNameBoard.AddGroupToTrain(InParams.TogetherGroup.strTrainGUID, InParams.TogetherGroup);
                                group = InParams.TogetherGroup.Group;
                                break;
                            }
                    }
                    if (group != null)
                    {
                        string strGroupGUID = group.groupID;
                        DBNameBoard.UpdateTrainmanJiaoLuToGroup(group, InParams.TrainmanJiaolu.jiaoluID);
                    }
                }
                finally
                {
                    string strContent = "";
                    TrainmanList trainmanList = new TrainmanList();
                    if (group.trainman1.trainmanID != "")
                    {
                        strContent += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman1.trainmanName);
                        trainmanList.Add(group.trainman1);
                    }
                    if (group.trainman2.trainmanID != "")
                    {
                        strContent += "," + string.Format("[{0}]{1}", group.trainman2.trainmanNumber, group.trainman2.trainmanName);
                        trainmanList.Add(group.trainman1);
                    }
                    if (group.trainman3.trainmanID != "")
                    {
                        strContent += "," + string.Format("[{0}]{1}", group.trainman3.trainmanNumber, group.trainman3.trainmanName);
                        trainmanList.Add(group.trainman1);
                    }
                    if (group.trainman4.trainmanID != "")
                    {
                        strContent += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman4.trainmanName);
                        trainmanList.Add(group.trainman1);
                    }
                    strContent = string.Format("机组【{0}】执行添加", strContent);
                    DBNameBoard.SaveChangeLog(InParams.TrainmanJiaolu, LBoardChangeType.btcAddGroup, strContent, InParams.DutyUser, trainmanList);
                }
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.AddGroup:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region   添加机组-------添加记名式交路机组
        public class InAddNamedGroup
        {
            //所属人员交路信息
            public TrainmanJiaoluMin TrainmanJiaolu = new TrainmanJiaoluMin();
            //值班员信息
            public DutyUser DutyUser = new DutyUser();
            //记名式机组的GUID
            public string CheciGUID;
            //int
            public int CheciOrder;
            //车次类型(0,1)
            public int CheciType;
            //往路车次
            public string Checi1;
            //回路车次
            public string Checi2;
            //乘务员1工号
            public string TrainmanNumber1;
            //乘务员2工号
            public string TrainmanNumber2;
            //乘务员3工号
            public string TrainmanNumber3;
            //乘务员4工号
            public string TrainmanNumber4;

        }

        /// <summary>
        /// 添加记名式交路机组
        /// </summary>
        public InterfaceOutPut AddNamedGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InAddNamedGroup InParams = javaScriptSerializer.Deserialize<InAddNamedGroup>(Data);
                //记名式交路机组
                RRsNamedGroup NamedGroup = new RRsNamedGroup();
                NamedGroup.nCheciOrder = InParams.CheciOrder;
                NamedGroup.nCheciType = InParams.CheciType;
                NamedGroup.strCheci1 = InParams.Checi1;
                NamedGroup.strCheci2 = InParams.Checi2;
                NamedGroup.strCheciGUID = InParams.CheciGUID;
                NamedGroup.strTrainmanJiaoluGUID = InParams.TrainmanJiaolu.jiaoluID;
                NamedGroup.dtLastArriveTime = DateTime.Parse("1899-01-01");
                NamedGroup.Group.groupID = Guid.NewGuid().ToString();
                TrainmanMin tm1 = new TrainmanMin();
                DBNameBoard.GetTrainman(InParams.TrainmanNumber1, out tm1);
                TrainmanMin tm2 = new TrainmanMin();
                DBNameBoard.GetTrainman(InParams.TrainmanNumber2, out tm2);
                TrainmanMin tm3 = new TrainmanMin();
                DBNameBoard.GetTrainman(InParams.TrainmanNumber3, out tm3);
                TrainmanMin tm4 = new TrainmanMin();
                DBNameBoard.GetTrainman(InParams.TrainmanNumber4, out tm4);

                NamedGroup.Group.trainman1.trainmanID = tm1.strTrainmanGUID;
                NamedGroup.Group.trainman1.trainmanNumber = tm1.strTrainmanNumber;
                NamedGroup.Group.trainman1.trainmanName = tm1.strTrainmanName;

                NamedGroup.Group.trainman2.trainmanID = tm2.strTrainmanGUID;
                NamedGroup.Group.trainman2.trainmanNumber = tm2.strTrainmanNumber;
                NamedGroup.Group.trainman2.trainmanName = tm2.strTrainmanName;

                NamedGroup.Group.trainman3.trainmanID = tm3.strTrainmanGUID;
                NamedGroup.Group.trainman3.trainmanNumber = tm3.strTrainmanNumber;
                NamedGroup.Group.trainman3.trainmanName = tm3.strTrainmanName;

                NamedGroup.Group.trainman4.trainmanID = tm4.strTrainmanGUID;
                NamedGroup.Group.trainman4.trainmanNumber = tm4.strTrainmanNumber;
                NamedGroup.Group.trainman4.trainmanName = tm4.strTrainmanName;

                DBNameBoard.AddNamedGroup(InParams.TrainmanJiaolu.jiaoluID, NamedGroup);
                Group group = NamedGroup.Group;
                string strContent = "";
                TrainmanList trainmanList = new TrainmanList();
                if (group.trainman1.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman1.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman2.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman2.trainmanNumber, group.trainman2.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman3.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman3.trainmanNumber, group.trainman3.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman4.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman4.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                strContent = string.Format("机组【{0}】执行添加", strContent);
                DBNameBoard.SaveChangeLog(InParams.TrainmanJiaolu, LBoardChangeType.btcAddGroup, strContent, InParams.DutyUser, trainmanList);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.AddNamedGroup:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region  添加机组-------添加轮乘机组
        public class InAddOrderGroup
        {
            //所属人员交路信息
            public TrainmanJiaoluMin TrainmanJiaolu = new TrainmanJiaoluMin();
            //值班员信息
            public DutyUser DutyUser = new DutyUser();
            //记名式机组的GUID
            public string OrderGUID;
            public DateTime LastArriveTime;
            public string PlaceID;

            //乘务员1工号
            public string TrainmanNumber1;
            //乘务员2工号
            public string TrainmanNumber2;
            //乘务员3工号
            public string TrainmanNumber3;
            //乘务员4工号
            public string TrainmanNumber4;

        }
        /// <summary>
        /// 添加轮乘交路机组
        /// </summary>
        public InterfaceOutPut AddOrderGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InAddOrderGroup InParams = javaScriptSerializer.Deserialize<InAddOrderGroup>(Data);
                //记名式交路机组
                TF.RunSafty.NamePlate.MD.OrderGroup orderGroup = new TF.RunSafty.NamePlate.MD.OrderGroup();

                orderGroup.lastArriveTime = InParams.LastArriveTime.ToString("yyyy-MM-dd HH:mm:ss");
                orderGroup.order = 0;
                orderGroup.orderID = InParams.OrderGUID;
                orderGroup.trainmanjiaoluID = InParams.TrainmanJiaolu.jiaoluID;
                orderGroup.group.place.placeID = InParams.PlaceID;
                orderGroup.group.groupID = Guid.NewGuid().ToString();
                TrainmanMin tm1 = new TrainmanMin();
                DBNameBoard.GetTrainman(InParams.TrainmanNumber1, out tm1);
                TrainmanMin tm2 = new TrainmanMin();
                DBNameBoard.GetTrainman(InParams.TrainmanNumber2, out tm2);
                TrainmanMin tm3 = new TrainmanMin();
                DBNameBoard.GetTrainman(InParams.TrainmanNumber3, out tm3);
                TrainmanMin tm4 = new TrainmanMin();
                DBNameBoard.GetTrainman(InParams.TrainmanNumber4, out tm4);

                orderGroup.group.trainman1.trainmanID = tm1.strTrainmanGUID;
                orderGroup.group.trainman1.trainmanNumber = tm1.strTrainmanNumber;
                orderGroup.group.trainman1.trainmanName = tm1.strTrainmanName;

                orderGroup.group.trainman2.trainmanID = tm2.strTrainmanGUID;
                orderGroup.group.trainman2.trainmanNumber = tm2.strTrainmanNumber;
                orderGroup.group.trainman2.trainmanName = tm2.strTrainmanName;

                orderGroup.group.trainman3.trainmanID = tm3.strTrainmanGUID;
                orderGroup.group.trainman3.trainmanNumber = tm3.strTrainmanNumber;
                orderGroup.group.trainman3.trainmanName = tm3.strTrainmanName;

                orderGroup.group.trainman4.trainmanID = tm4.strTrainmanGUID;
                orderGroup.group.trainman4.trainmanNumber = tm4.strTrainmanNumber;
                orderGroup.group.trainman4.trainmanName = tm4.strTrainmanName;

                DBNameBoard.AddOrderGroup(InParams.TrainmanJiaolu.jiaoluID, orderGroup);
                DBNameBoard dbNB = new DBNameBoard();
                PrepareTMOrder tmOrder = new PrepareTMOrder();
                if (!string.IsNullOrEmpty(tm1.strTrainmanNumber))
                {
                    LogClass.log(string.Format("人员1不为空{0}", tm1.strTrainmanNumber));
                    if (LCPrepareTMOrder.GetTrainmanOrder(tm1.strTrainmanNumber, tmOrder))
                    {
                        LogClass.log(string.Format("成功获取人员1的位置{0}", Newtonsoft.Json.JsonConvert.SerializeObject(tmOrder)));
                        LCPrepareTMOrder.DeletePrepareTrainmanOrder(tmOrder);
                        LogClass.log(string.Format("成功删除成员1的{0}", tm1.strTrainmanNumber));
                    }
                }
                if (!string.IsNullOrEmpty(tm2.strTrainmanNumber))
                {
                    LogClass.log(string.Format("人员2不为空{0}", tm2.strTrainmanNumber));
                    if (LCPrepareTMOrder.GetTrainmanOrder(tm2.strTrainmanNumber, tmOrder))
                    {
                        LogClass.log(string.Format("成功获取人员2的位置{0}", Newtonsoft.Json.JsonConvert.SerializeObject(tmOrder)));
                        LCPrepareTMOrder.DeletePrepareTrainmanOrder(tmOrder);
                        LogClass.log(string.Format("成功删除成员2的{0}", tm2.strTrainmanNumber));
                    }
                }
                if (!string.IsNullOrEmpty(tm3.strTrainmanNumber))
                {
                    if (LCPrepareTMOrder.GetTrainmanOrder( tm3.strTrainmanNumber, tmOrder))
                    {
                        LCPrepareTMOrder.DeletePrepareTrainmanOrder(tmOrder);
                    }
                }
                if (!string.IsNullOrEmpty(tm4.strTrainmanNumber))
                {
                    if (LCPrepareTMOrder.GetTrainmanOrder(tm4.strTrainmanNumber, tmOrder))
                    {
                        LCPrepareTMOrder.DeletePrepareTrainmanOrder(tmOrder);
                    }
                }
                Group group = orderGroup.group;
                string strContent = "";
                TrainmanList trainmanList = new TrainmanList();
                if (group.trainman1.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman1.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman2.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman2.trainmanNumber, group.trainman2.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman3.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman3.trainmanNumber, group.trainman3.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman4.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman4.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                strContent = string.Format("机组【{0}】执行添加", strContent);
                DBNameBoard.SaveChangeLog(InParams.TrainmanJiaolu, LBoardChangeType.btcAddGroup, strContent, InParams.DutyUser, trainmanList);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.AddNamedGroup:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region  添加机组-------添加包乘机组
        public class InAddTogetherGroup
        {
            //所属人员交路信息
            public TrainmanJiaoluMin TrainmanJiaolu = new TrainmanJiaoluMin();
            //值班员信息
            public DutyUser DutyUser = new DutyUser();
            //记名式机组的GUID
            public string OrderGUID;
            public int Order;
            public string TrainGUID;

            //乘务员1工号
            public string TrainmanNumber1;
            //乘务员2工号
            public string TrainmanNumber2;
            //乘务员3工号
            public string TrainmanNumber3;
            //乘务员4工号
            public string TrainmanNumber4;

        }
        /// <summary>
        /// 添加记名式交路机组
        /// </summary>
        public InterfaceOutPut AddTogetherGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InAddTogetherGroup InParams = javaScriptSerializer.Deserialize<InAddTogetherGroup>(Data);
                //记名式交路机组
                TF.RunSafty.NamePlate.MD.RRsOrderGroupInTrain orderGroup = new TF.RunSafty.NamePlate.MD.RRsOrderGroupInTrain();

                orderGroup.dtLastArriveTime = DateTime.Parse("1899-01-01");
                orderGroup.nOrder = 0;
                orderGroup.strOrderGUID = InParams.OrderGUID;
                orderGroup.strTrainGUID = InParams.TrainGUID;

                orderGroup.Group.groupID = Guid.NewGuid().ToString();
                TrainmanMin tm1 = new TrainmanMin();
                DBNameBoard.GetTrainman(InParams.TrainmanNumber1, out tm1);
                TrainmanMin tm2 = new TrainmanMin();
                DBNameBoard.GetTrainman(InParams.TrainmanNumber2, out tm2);
                TrainmanMin tm3 = new TrainmanMin();
                DBNameBoard.GetTrainman(InParams.TrainmanNumber3, out tm3);
                TrainmanMin tm4 = new TrainmanMin();
                DBNameBoard.GetTrainman(InParams.TrainmanNumber4, out tm4);

                orderGroup.Group.trainman1.trainmanID = tm1.strTrainmanGUID;
                orderGroup.Group.trainman1.trainmanNumber = tm1.strTrainmanNumber;
                orderGroup.Group.trainman1.trainmanName = tm1.strTrainmanName;

                orderGroup.Group.trainman2.trainmanID = tm2.strTrainmanGUID;
                orderGroup.Group.trainman2.trainmanNumber = tm2.strTrainmanNumber;
                orderGroup.Group.trainman2.trainmanName = tm2.strTrainmanName;

                orderGroup.Group.trainman3.trainmanID = tm3.strTrainmanGUID;
                orderGroup.Group.trainman3.trainmanNumber = tm3.strTrainmanNumber;
                orderGroup.Group.trainman3.trainmanName = tm3.strTrainmanName;

                orderGroup.Group.trainman4.trainmanID = tm4.strTrainmanGUID;
                orderGroup.Group.trainman4.trainmanNumber = tm4.strTrainmanNumber;
                orderGroup.Group.trainman4.trainmanName = tm4.strTrainmanName;

                DBNameBoard.AddGroupToTrain(InParams.TrainGUID, orderGroup);

                DBNameBoard dbNB = new DBNameBoard();
                PrepareTMOrder tmOrder = new PrepareTMOrder();
                if (!string.IsNullOrEmpty(tm1.strTrainmanNumber))
                {
                    LogClass.log(string.Format("人员1不为空{0}", tm1.strTrainmanNumber));
                    if (LCPrepareTMOrder.GetTrainmanOrder( tm1.strTrainmanNumber, tmOrder))
                    {
                        LogClass.log(string.Format("成功获取人员1的位置{0}", Newtonsoft.Json.JsonConvert.SerializeObject(tmOrder)));
                        LCPrepareTMOrder.DeletePrepareTrainmanOrder(tmOrder);
                        LogClass.log(string.Format("成功删除成员1的{0}", tm1.strTrainmanNumber));
                    }
                }
                if (!string.IsNullOrEmpty(tm2.strTrainmanNumber))
                {
                    LogClass.log(string.Format("人员2不为空{0}", tm2.strTrainmanNumber));
                    if (LCPrepareTMOrder.GetTrainmanOrder(tm2.strTrainmanNumber, tmOrder))
                    {
                        LogClass.log(string.Format("成功获取人员2的位置{0}", Newtonsoft.Json.JsonConvert.SerializeObject(tmOrder)));
                        LCPrepareTMOrder.DeletePrepareTrainmanOrder(tmOrder);
                        LogClass.log(string.Format("成功删除成员2的{0}", tm2.strTrainmanNumber));
                    }
                }
                if (!string.IsNullOrEmpty(tm3.strTrainmanNumber))
                {
                    if (LCPrepareTMOrder.GetTrainmanOrder(tm3.strTrainmanNumber, tmOrder))
                    {
                        LCPrepareTMOrder.DeletePrepareTrainmanOrder(tmOrder);
                    }
                }
                if (!string.IsNullOrEmpty(tm4.strTrainmanNumber))
                {
                    if (LCPrepareTMOrder.GetTrainmanOrder( tm4.strTrainmanNumber, tmOrder))
                    {
                        LCPrepareTMOrder.DeletePrepareTrainmanOrder(tmOrder);
                    }
                }

                Group group = orderGroup.Group;
                string strContent = "";
                TrainmanList trainmanList = new TrainmanList();
                if (group.trainman1.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman1.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman2.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman2.trainmanNumber, group.trainman2.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman3.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman3.trainmanNumber, group.trainman3.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman4.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman4.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                strContent = string.Format("机组【{0}】执行添加", strContent);
                DBNameBoard.SaveChangeLog(InParams.TrainmanJiaolu, LBoardChangeType.btcAddGroup, strContent, InParams.DutyUser, trainmanList);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.AddNamedGroup:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 删除机组--------包括删除记名式，轮乘，  包乘机组
        public class InDeleteGroup
        {
            //所属人员交路信息
            public TrainmanJiaoluMin TrainmanJiaolu = new TrainmanJiaoluMin();
            //待删机组信息
            public string GroupGUID;
            //值班员信息
            public DutyUser DutyUser = new DutyUser();
        }

        /// <summary>
        /// 删除机组
        /// </summary>
        public InterfaceOutPut DeleteGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InDeleteGroup InParams = javaScriptSerializer.Deserialize<InDeleteGroup>(Data);
                Group group = new Group();
                DBNameBoard.GetGroup(InParams.GroupGUID, group);
                if (group.groupState == 3)
                {
                    throw new Exception(string.Format("目标机组已被安排了计划，不能添加"));
                }
                if (group.groupState == 6)
                {
                    throw new Exception(string.Format("目标机组已出勤，不能添加"));
                }
            
                DeleteGroupM(InParams.TrainmanJiaolu, InParams.GroupGUID, InParams.DutyUser);                
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.DeleteGroup:" + ex.Message);                
            }
            return output;
        }
      
        public void DeleteGroupM(TrainmanJiaoluMin Jiaolu, string GroupGUID, DutyUser User)
        {
         
            try
            {
                switch (Jiaolu.jiaoluType)
                {
                    case 2:
                        {
                            DBNameBoard.DeleteNamedGroup(GroupGUID);

                            break;
                        }
                    case 3:
                        {

                            LCGroup.DeleteOrderGroup(Jiaolu.jiaoluID,Jiaolu.jiaoluName,GroupGUID);
                            
                            break;
                        }
                    case 4:
                        {
                            DBNameBoard.DeleteTogetherGroup(GroupGUID);
                            break;
                        }
                }

            }
            finally
            {
                string strContent = "";
                TrainmanList trainmanList = new TrainmanList();
                Group group = new Group();
                DBNameBoard.GetGroup(GroupGUID, group);

                if (group.trainman1.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman1.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman2.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman2.trainmanNumber, group.trainman2.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman3.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman3.trainmanNumber, group.trainman3.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman4.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman4.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                strContent = string.Format("机组【{0}】执行删除", strContent);
                DBNameBoard.SaveChangeLog(Jiaolu, LBoardChangeType.btcDeleteGroup, strContent, User, trainmanList);
            }
        }

        public static void DeleteOrderGroup(string TMJiaoluGUID,string TMJiaoluName,string GroupGUID)
        {
            if (LCPrepareTMOrder.IsTurnPrepare(TMJiaoluGUID))
            {
                LCPrepareTMOrder.SetToPrepareOrder(TMJiaoluGUID, TMJiaoluName, GroupGUID);
            }
            DBNameBoard.DeleteOrderGroup(GroupGUID);
        }
        #endregion

        #region 设置乘务员前  检查乘务员的状态

        public class InCheckCanAdd
        {
            //人员工号
            public string TMNumber;
            //所属车间GUID
            public string AimWSID;
            //所属人员交路GUID
            public string AimTMJLID;
            //目标机组id
            public string AimGroupID;
        }
        public class OutCheckCanAdd
        {
            //检测级别  0，允许  1，提示  2，禁止
            public int Level;
            public string CheckBrief;
        }

        public InterfaceOutPut CheckCanAdd(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 0;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InCheckCanAdd InParams = javaScriptSerializer.Deserialize<InCheckCanAdd>(Data);
                OutCheckCanAdd OutParams = new OutCheckCanAdd();
                OutParams.Level = 0;
                OutParams.CheckBrief = "";
                string checkBrief = "";
                System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                watch.Start();

                //通过工号找到该人员的id
                string strTMGUID = "";

                //判断所有传入参数是否是空字符串，如果一个为空，则禁止下一步
                if (!DBCheckCanAdd.checkIsNull(InParams, ref checkBrief))
                    return ReturnInfo(OutParams, output, checkBrief, 2);
                //判断是否属于别的车间，当前只有两种状态， 禁止和允许
                if (!DBCheckCanAdd.CheckTMInWs(InParams.TMNumber, InParams.AimWSID, ref checkBrief, ref strTMGUID))
                    return ReturnInfo(OutParams, output, checkBrief, 2);
                //判断是否处于请假状态
                if (!DBCheckCanAdd.CheckIsLeave(InParams.TMNumber, ref checkBrief))
                    return ReturnInfo(OutParams, output, checkBrief, 2);
                //判断目标机组是否出勤  是否安排有计划
                if (!DBCheckCanAdd.CheckIsInPlan(InParams.AimGroupID, ref checkBrief))
                    return ReturnInfo(OutParams, output, checkBrief, 2);
                //判断原有机组的机组状态，如果是已出勤，则给与提示
                if (!DBCheckCanAdd.CheckOldGroup(InParams.TMNumber, ref checkBrief, strTMGUID))
                    return ReturnInfo(OutParams, output, checkBrief, 2);
                //判断原有机组是否与目标机组是否在同一个交路  
                if (!DBCheckCanAdd.CheckTMJL(InParams.TMNumber, InParams.AimTMJLID, ref checkBrief, strTMGUID))
                    return ReturnInfo(OutParams, output, checkBrief, 2);
                // 判断是否是从一个机组移动到另一个机组
                if (!DBCheckCanAdd.CheckIsInOtherGroup(InParams.AimGroupID, InParams.TMNumber, ref checkBrief, strTMGUID))
                    return ReturnInfo(OutParams, output, checkBrief, 2);
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.CheckCanAdd:" + ex.Message);
                throw ex;
            }
            return output;
        }

        public InterfaceOutPut ReturnInfo(OutCheckCanAdd OutParams, InterfaceOutPut output, string checkBrief, int level)
        {
            OutParams.Level = level;
            OutParams.CheckBrief = checkBrief;
            output.data = OutParams;
            return output;
        }



        #endregion

        #region 设置乘务员-设置乘务员的同时，将原有机组的乘务员删除
        public class InAddTrainman
        {
            //待添加的人员工号
            public string TrainmanNumber;
            //所属人员交路信息
            public TrainmanJiaoluMin TrainmanJiaolu = new TrainmanJiaoluMin();
            //所在机组
            public string GroupGUID;
            //添加的位置(1,2,3,4)
            public int TrainmanIndex;
            //值班员
            public DutyUser DutyUser = new DutyUser();

        }

        /// <summary>
        /// 设置乘务员
        /// </summary>
        public InterfaceOutPut AddTrainman(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InAddTrainman InParams = javaScriptSerializer.Deserialize<InAddTrainman>(Data);
                TrainmanMin tm = new TrainmanMin();
                DB.DBNameBoard.GetTrainman(InParams.TrainmanNumber, out tm);

                Group group = new Group();
                DBNameBoard.GetGroup(InParams.GroupGUID, group);
                if (group.groupState == 3)
                {
                    throw new Exception(string.Format("目标机组已被安排了计划，不能添加"));
                }
                if (group.groupState == 6)
                {
                    throw new Exception(string.Format("目标机组已出勤，不能添加"));
                }
                //获取待加人员原来所属的机组信息
                string strSql = @"select top 1 * from VIEW_Nameplate_Group 
                     where  strTrainmanGUID1 = @strTrainmanGUID or strTrainmanGUID2=@strTrainmanGUID or strTrainmanGUID3=@strTrainmanGUID or strTrainmanGUID4=@strTrainmanGUID";
                SqlParameter[] sqlParamsOldGroup = new SqlParameter[]{
                    new SqlParameter("strTrainmanGUID",tm.strTrainmanGUID)
                };
                //删除待加人员在原来机组中的信息
                DataTable dtOldGroup = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsOldGroup).Tables[0];

                SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        if (dtOldGroup.Rows.Count > 0)
                        {
                            if (dtOldGroup.Rows[0]["GroupState"].ToString() == "7")
                                throw new Exception(string.Format("[{0}]所在的机组正在值乘中，不能添加", InParams.TrainmanNumber));

                            if (dtOldGroup.Rows[0]["strTrainmanJiaoluGUID"].ToString() != InParams.TrainmanJiaolu.jiaoluID)
                                throw new Exception(string.Format("[{0}]处于[{1}]交路名牌中，不能添加", InParams.TrainmanNumber, dtOldGroup.Rows[0]["strTrainmanJiaoluName"].ToString()));

                            string strOldGroupTrainman = "";
                            if (dtOldGroup.Rows[0]["strTrainmanGUID1"].ToString() == tm.strTrainmanGUID)
                                strSql = "update TAB_Nameplate_Group set strTrainmanGUID1=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                            else
                                strOldGroupTrainman += dtOldGroup.Rows[0]["strTrainmanGUID1"].ToString();
                            if (dtOldGroup.Rows[0]["strTrainmanGUID2"].ToString() == tm.strTrainmanGUID)
                                strSql = "update TAB_Nameplate_Group set strTrainmanGUID2=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                            else
                                strOldGroupTrainman += dtOldGroup.Rows[0]["strTrainmanGUID2"].ToString();
                            if (dtOldGroup.Rows[0]["strTrainmanGUID3"].ToString() == tm.strTrainmanGUID)
                                strSql = "update TAB_Nameplate_Group set strTrainmanGUID3=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                            else
                                strOldGroupTrainman += dtOldGroup.Rows[0]["strTrainmanGUID3"].ToString();

                            if (dtOldGroup.Rows[0]["strTrainmanGUID4"].ToString() == tm.strTrainmanGUID)
                                strSql = "update TAB_Nameplate_Group set strTrainmanGUID4=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                            else
                                strOldGroupTrainman += dtOldGroup.Rows[0]["strTrainmanGUID4"].ToString();

                            SqlParameter[] sqlParamsDel = new SqlParameter[]{
                                new SqlParameter("strTrainmanGUID",""),
                                new SqlParameter("strGroupGUID",dtOldGroup.Rows[0]["strGroupGUID"].ToString())
                            };


                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsDel);

                            //轮乘交路删除空机组
                            if (InParams.TrainmanJiaolu.jiaoluType == 3) //jltOrder
                            {
                                if (strOldGroupTrainman == "")
                                {
                                    strSql = "delete from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID";
                                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsDel);

                                    strSql = "delete from TAB_Nameplate_TrainmanJiaolu_Order where strGroupGUID = @strGroupGUID";
                                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsDel);
                                }
                            }
                        }


                        //在新机组中添加人员
                        switch (InParams.TrainmanIndex)
                        {
                            case 1:
                                {
                                    strSql = "update TAB_Nameplate_Group set strTrainmanGUID1=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                                    break;
                                }
                            case 2:
                                {
                                    strSql = "update TAB_Nameplate_Group set strTrainmanGUID2=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                                    break;
                                }
                            case 3:
                                {
                                    strSql = "update TAB_Nameplate_Group set strTrainmanGUID3=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                                    break;
                                }
                            case 4:
                                {
                                    strSql = "update TAB_Nameplate_Group set strTrainmanGUID4=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                                    break;
                                }
                        }
                        SqlParameter[] sqlParamsAdd = new SqlParameter[]{
                                new SqlParameter("strTrainmanGUID",tm.strTrainmanGUID),
                                new SqlParameter("strGroupGUID",InParams.GroupGUID)
                                };
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsAdd);

                        //获取新机组所在的人员交路和车间信息
                        string strTrainmanJiaoluGUID = "";
                        string strWorkShopGUID = "";
                        string strTrainJiaoluGUID = "";
                        strSql = "select strTrainmanJiaoluGUID from VIEW_Nameplate_Group_TrainmanJiaolu where strGroupGUID=@strGroupGUID";
                        SqlParameter[] sqlParamsGroup = new SqlParameter[] { new SqlParameter("strGroupGUID", group.groupID) };
                        DataTable dtTrainmanJiaolu = SqlHelper.ExecuteDataset(trans, CommandType.Text, strSql, sqlParamsGroup).Tables[0];
                        if (dtTrainmanJiaolu.Rows.Count > 0)
                        {
                            strTrainmanJiaoluGUID = dtTrainmanJiaolu.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                            strSql = "select strTrainJiaoluGUID,strWorkShopGUID from  VIEW_Base_JiaoluRelation where strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID";
                            SqlParameter[] sqlParamsTrainmanJialu = new SqlParameter[]{
                         new SqlParameter("strTrainmanJiaoluGUID",strTrainmanJiaoluGUID)
                            };
                            DataTable dtWorkShop = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTrainmanJialu).Tables[0];
                            if (dtWorkShop.Rows.Count > 0)
                            {
                                strTrainJiaoluGUID = dtWorkShop.Rows[0]["strTrainJiaoluGUID"].ToString();
                                strWorkShopGUID = dtWorkShop.Rows[0]["strWorkShopGUID"].ToString();
                            }
                        }
                        //更新人员的所属区段和所属车间
                        strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,
                    nTrainmanState = @nTrainmanState where strTrainmanGUID = @strTrainmanGUID";
                        SqlParameter[] sqlParamsTM = new SqlParameter[]{
                        new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUID),
                        new SqlParameter("strTrainmanJiaoluGUID",strTrainmanJiaoluGUID),                    
                        new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                        new SqlParameter("nTrainmanState",2), //tsNormal
                        new SqlParameter("strTrainmanGUID",tm.strTrainmanGUID)
                        };
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTM);
                        if (InParams.TrainmanNumber.Trim() != "")
                        { 
                            DB.DBNameBoard dbNB = new DB.DBNameBoard();
                            PrepareTMOrder tmOrder = new PrepareTMOrder();
                            if (LCPrepareTMOrder.GetTrainmanOrder(InParams.TrainmanNumber, tmOrder))
                            {
                                LCPrepareTMOrder.DeletePrepareTrainmanOrder(tmOrder);
                            }
                        }
                        trans.Commit();

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }

                }

                string strContent = "";
                TrainmanList trainmanList = new TrainmanList();

                if (group.trainman1.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman1.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman2.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman2.trainmanNumber, group.trainman2.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman3.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman3.trainmanNumber, group.trainman3.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman4.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman4.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                strContent = string.Format("向机组【{0}】的第【{1}】位添加人员【{2}】", strContent, InParams.TrainmanIndex, tm.strTrainmanNumber + "_" + tm.strTrainmanName);
                DBNameBoard.SaveChangeLog(InParams.TrainmanJiaolu, LBoardChangeType.btcAddTrainman, strContent, InParams.DutyUser, trainmanList);

                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.AddTrainman:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region  设置乘务员V2
        /// <summary>
        /// 设置乘务员_V2
        /// </summary>
        public InterfaceOutPut AddTrainman_V2(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InAddTrainman InParams = javaScriptSerializer.Deserialize<InAddTrainman>(Data);


                Group group = new Group();
                DBNameBoard.GetGroup(InParams.GroupGUID, group);

                TrainmanMin tm = new TrainmanMin();
                DB.DBNameBoard.GetTrainman(InParams.TrainmanNumber, out tm);

                //获取待加人员原来所属的机组信息
                string strSql = @"select top 1 * from VIEW_Nameplate_Group 
                     where  strTrainmanGUID1 = @strTrainmanGUID or strTrainmanGUID2=@strTrainmanGUID or strTrainmanGUID3=@strTrainmanGUID or strTrainmanGUID4=@strTrainmanGUID";
                SqlParameter[] sqlParamsOldGroup = new SqlParameter[]{
                    new SqlParameter("strTrainmanGUID",tm.strTrainmanGUID)
                };
                //删除待加人员在原来机组中的信息
                DataTable dtOldGroup = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsOldGroup).Tables[0];
                SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        string strOldGroupTrainman = "";
                        if (dtOldGroup.Rows.Count > 0)
                        {
                            if (dtOldGroup.Rows[0]["strTrainmanGUID1"].ToString() == tm.strTrainmanGUID)
                                strSql = "update TAB_Nameplate_Group set strTrainmanGUID1=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                            else
                                strOldGroupTrainman += dtOldGroup.Rows[0]["strTrainmanGUID1"].ToString();
                            if (dtOldGroup.Rows[0]["strTrainmanGUID2"].ToString() == tm.strTrainmanGUID)
                                strSql = "update TAB_Nameplate_Group set strTrainmanGUID2=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                            else
                                strOldGroupTrainman += dtOldGroup.Rows[0]["strTrainmanGUID2"].ToString();
                            if (dtOldGroup.Rows[0]["strTrainmanGUID3"].ToString() == tm.strTrainmanGUID)
                                strSql = "update TAB_Nameplate_Group set strTrainmanGUID3=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                            else
                                strOldGroupTrainman += dtOldGroup.Rows[0]["strTrainmanGUID3"].ToString();

                            if (dtOldGroup.Rows[0]["strTrainmanGUID4"].ToString() == tm.strTrainmanGUID)
                                strSql = "update TAB_Nameplate_Group set strTrainmanGUID4=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                            else
                                strOldGroupTrainman += dtOldGroup.Rows[0]["strTrainmanGUID4"].ToString();

                            SqlParameter[] sqlParamsDel = new SqlParameter[]{
                                new SqlParameter("strTrainmanGUID",""),
                                new SqlParameter("strGroupGUID",dtOldGroup.Rows[0]["strGroupGUID"].ToString())
                            };
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsDel);

                            //轮乘交路删除空机组
                            if (InParams.TrainmanJiaolu.jiaoluType == 3) //jltOrder
                            {
                                if (strOldGroupTrainman == "")
                                {
                                    strSql = "delete from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID";
                                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsDel);

                                    strSql = "delete from TAB_Nameplate_TrainmanJiaolu_Order where strGroupGUID = @strGroupGUID";
                                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsDel);
                                }
                            }
                        }


                        //在新机组中添加人员
                        switch (InParams.TrainmanIndex)
                        {
                            case 1:
                                {
                                    strSql = "update TAB_Nameplate_Group set strTrainmanGUID1=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                                    break;
                                }
                            case 2:
                                {
                                    strSql = "update TAB_Nameplate_Group set strTrainmanGUID2=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                                    break;
                                }
                            case 3:
                                {
                                    strSql = "update TAB_Nameplate_Group set strTrainmanGUID3=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                                    break;
                                }
                            case 4:
                                {
                                    strSql = "update TAB_Nameplate_Group set strTrainmanGUID4=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                                    break;
                                }
                        }
                        SqlParameter[] sqlParamsAdd = new SqlParameter[]{
                                new SqlParameter("strTrainmanGUID",tm.strTrainmanGUID),
                                new SqlParameter("strGroupGUID",InParams.GroupGUID)
                                };
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsAdd);

                        //获取新机组所在的人员交路和车间信息
                        string strTrainmanJiaoluGUID = "";
                        string strWorkShopGUID = "";
                        string strTrainJiaoluGUID = "";
                        strSql = "select strTrainmanJiaoluGUID from VIEW_Nameplate_Group_TrainmanJiaolu where strGroupGUID=@strGroupGUID";
                        SqlParameter[] sqlParamsGroup = new SqlParameter[] { new SqlParameter("strGroupGUID", group.groupID) };
                        DataTable dtTrainmanJiaolu = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsGroup).Tables[0];
                        if (dtTrainmanJiaolu.Rows.Count > 0)
                        {
                            strTrainmanJiaoluGUID = dtTrainmanJiaolu.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                            strSql = "select strTrainJiaoluGUID,strWorkShopGUID from  VIEW_Base_JiaoluRelation where strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID";
                            SqlParameter[] sqlParamsTrainmanJialu = new SqlParameter[]{
                         new SqlParameter("strTrainmanJiaoluGUID",strTrainmanJiaoluGUID)
                            };
                            DataTable dtWorkShop = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTrainmanJialu).Tables[0];
                            if (dtWorkShop.Rows.Count > 0)
                            {
                                strTrainJiaoluGUID = dtWorkShop.Rows[0]["strTrainJiaoluGUID"].ToString();
                                strWorkShopGUID = dtWorkShop.Rows[0]["strWorkShopGUID"].ToString();
                            }
                        }
                        //更新人员的所属区段和所属车间
                        strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,
                    nTrainmanState = @nTrainmanState where strTrainmanGUID = @strTrainmanGUID";
                        SqlParameter[] sqlParamsTM = new SqlParameter[]{
                        new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUID),
                        new SqlParameter("strTrainmanJiaoluGUID",strTrainmanJiaoluGUID),                    
                        new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                        new SqlParameter("nTrainmanState",2), //tsNormal
                        new SqlParameter("strTrainmanGUID",tm.strTrainmanGUID)
                        };
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTM);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }

                }

                string strContent = "";
                TrainmanList trainmanList = new TrainmanList();

                if (group.trainman1.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman1.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman2.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman2.trainmanNumber, group.trainman2.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman3.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman3.trainmanNumber, group.trainman3.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman4.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman4.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                strContent = string.Format("向机组【{0}】的第【{1}】位添加人员【{2}】", strContent, InParams.TrainmanIndex, tm.strTrainmanNumber + "_" + tm.strTrainmanName);
                DBNameBoard.SaveChangeLog(InParams.TrainmanJiaolu, LBoardChangeType.btcAddTrainman, strContent, InParams.DutyUser, trainmanList);

                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.AddTrainman:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 交换乘务员 只能是同一人员交路下的司机才能交换
        public class InChangeTM
        {
            //源机组id
            public string SrcGrp;
            //源人员位置
            public int SrcPos;
            //源人员id
            public string SrcTm;
            //目标机组id
            public string DestGrp;
            //目标人员位置
            public int DestPos;
            //目标人员id
            public string DestTm;
        }
        public InterfaceOutPut ExchangeTM(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InChangeTM InParams = javaScriptSerializer.Deserialize<InChangeTM>(Data);
                output.result = 0;
                output.resultStr = "执行成功！";
                //判断所有传入参数是否是空字符串，如果一个为空，则禁止下一步
                DBChangeTM.checkIsNull(InParams);
                //判断人员是否属于同一人员区段，是否是请假状态
                DBChangeTM.CheckIsOneTMJLAndIsLeave(InParams.SrcTm, InParams.DestTm, InParams.SrcGrp, InParams.DestGrp);
                //判断是否有值乘的计划
                string checkBrief = "";
                if (!DBCheckCanAdd.CheckIsInPlan(InParams.SrcGrp, ref checkBrief))
                    throw new Exception(string.Format("源" + checkBrief));
                if (!DBCheckCanAdd.CheckIsInPlan(InParams.DestGrp, ref checkBrief))
                    throw new Exception(string.Format("目标" + checkBrief));
                DBChangeTM.changeTM(InParams);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.ChangeTM:" + ex.Message);
                throw ex;
            }
            return output;
        }


        #endregion

        #region 删除乘务员-将乘务员从机组移除，修改人员表状态

        public class InDeleteTrainman
        {
            //所属人员交路信息
            public TrainmanJiaoluMin TrainmanJiaolu = new TrainmanJiaoluMin();
            //所属机组
            public string GroupGUID;
            //所在位置(1,2,3,4)
            public int TrainmanIndex;
            //待删人员
            public string TrainmanNumber;
            //值班员信息
            public DutyUser DutyUser = new DutyUser();
        }

        /// <summary>
        /// 删除乘务员
        /// </summary>
        public InterfaceOutPut DeleteTrainman(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InDeleteTrainman InParams = javaScriptSerializer.Deserialize<InDeleteTrainman>(Data);

                TrainmanMin tm = new TrainmanMin();
                DB.DBNameBoard.GetTrainman(InParams.TrainmanNumber, out  tm);

                Group group = new Group();
                DB.DBNameBoard.GetGroup(InParams.GroupGUID, group);
                if ((group.groupState == 3) || (group.groupState == 6))
                {
                    throw new Exception("指定人员所在机组处于已安排计划或者已出勤中,不能删除");
                }

                SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //将乘务员状态修改为预备
                        string strSql = "update Tab_Org_Trainman set nTrainmanState = @nTrainmanState,dtBecomeReady = getdate() where strTrainmanGUID = @strTrainmanGUID";
                        SqlParameter[] sqlParams = new SqlParameter[] { 
                            new SqlParameter("nTrainmanState",1),  //tsReady
                            new SqlParameter("strTrainmanGUID",tm.strTrainmanGUID)
                        };
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParams);

                        strSql = "update TAB_Nameplate_Group set strTrainmanGUID1=@NullTrainmanGUID where strTrainmanGUID1 = @strTrainmanGUID";
                        SqlParameter[] sqlParamsNull = new SqlParameter[] { 
                            new SqlParameter("NullTrainmanGUID",""),  //tsReady
                            new SqlParameter("strTrainmanGUID",tm.strTrainmanGUID)
                        };
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsNull);

                        strSql = "update TAB_Nameplate_Group set strTrainmanGUID2=@NullTrainmanGUID where strTrainmanGUID2 = @strTrainmanGUID";
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsNull);

                        strSql = "update TAB_Nameplate_Group set strTrainmanGUID3=@NullTrainmanGUID where strTrainmanGUID3 = @strTrainmanGUID";
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsNull);

                        strSql = "update TAB_Nameplate_Group set strTrainmanGUID4=@NullTrainmanGUID where strTrainmanGUID4 = @strTrainmanGUID";
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsNull);

                        output.result = 0;
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                string strContent = "";
                TrainmanList trainmanList = new TrainmanList();

                if (group.trainman1.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman1.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman2.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman2.trainmanNumber, group.trainman2.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman3.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman3.trainmanNumber, group.trainman3.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                if (group.trainman4.trainmanID != "")
                {
                    strContent += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman4.trainmanName);
                    trainmanList.Add(group.trainman1);
                }
                strContent = string.Format("从机组【{0}】的第【{1}】位移除人员【{2}】", strContent, InParams.TrainmanIndex, tm.strTrainmanNumber + "_" + tm.strTrainmanName);
                DBNameBoard.SaveChangeLog(InParams.TrainmanJiaolu, LBoardChangeType.btcDeleteTrainman, strContent, InParams.DutyUser, trainmanList);
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.DeleteTrainman:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 交换机组-存储过程
        public class InSwapGroup
        {
            //所在人员交路
            public TrainmanJiaoluMin TrainmanJiaolu = new TrainmanJiaoluMin();
            //待交换机组GUID
            public string SourceGroupGUID;
            //被交换机组GUID
            public string DestGroupGUID;
            //值班员
            public DutyUser DutyUser = new DutyUser();

        }

        /// <summary>
        /// 移动机组上一组、下一组
        /// </summary>
        public InterfaceOutPut SwapGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InSwapGroup InParams = javaScriptSerializer.Deserialize<InSwapGroup>(Data);

                string strSql = "PROC_Nameplate_ExchangeGroup";
                SqlParameter[] sqlParams = new SqlParameter[]{
                    new SqlParameter("jiaoluType",InParams.TrainmanJiaolu.jiaoluType),
                    new SqlParameter("groupGUID1",InParams.SourceGroupGUID),
                    new SqlParameter("groupGUID2",InParams.DestGroupGUID)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.StoredProcedure, strSql, sqlParams);



                TrainmanList trainmanList = new TrainmanList();
                Group sourceGroup = new Group();
                DBNameBoard.GetGroup(InParams.SourceGroupGUID, sourceGroup);

                string strSourceContent = "";
                if (sourceGroup.trainman1.trainmanID != "")
                {
                    strSourceContent += "," + string.Format("[{0}]{1}", sourceGroup.trainman1.trainmanNumber, sourceGroup.trainman1.trainmanName);
                    trainmanList.Add(sourceGroup.trainman1);
                }
                if (sourceGroup.trainman2.trainmanID != "")
                {
                    strSourceContent += "," + string.Format("[{0}]{1}", sourceGroup.trainman2.trainmanNumber, sourceGroup.trainman2.trainmanName);
                    trainmanList.Add(sourceGroup.trainman1);
                }
                if (sourceGroup.trainman3.trainmanID != "")
                {
                    strSourceContent += "," + string.Format("[{0}]{1}", sourceGroup.trainman3.trainmanNumber, sourceGroup.trainman3.trainmanName);
                    trainmanList.Add(sourceGroup.trainman1);
                }
                if (sourceGroup.trainman4.trainmanID != "")
                {
                    strSourceContent += "," + string.Format("[{0}]{1}", sourceGroup.trainman1.trainmanNumber, sourceGroup.trainman4.trainmanName);
                    trainmanList.Add(sourceGroup.trainman1);
                }

                Group destGroup = new Group();
                DBNameBoard.GetGroup(InParams.DestGroupGUID, destGroup);
                string strDestContent = "";
                if (destGroup.trainman1.trainmanID != "")
                {
                    strDestContent += "," + string.Format("[{0}]{1}", destGroup.trainman1.trainmanNumber, destGroup.trainman1.trainmanName);
                    trainmanList.Add(destGroup.trainman1);
                }
                if (destGroup.trainman2.trainmanID != "")
                {
                    strDestContent += "," + string.Format("[{0}]{1}", destGroup.trainman2.trainmanNumber, destGroup.trainman2.trainmanName);
                    trainmanList.Add(destGroup.trainman1);
                }
                if (destGroup.trainman3.trainmanID != "")
                {
                    strDestContent += "," + string.Format("[{0}]{1}", destGroup.trainman3.trainmanNumber, destGroup.trainman3.trainmanName);
                    trainmanList.Add(destGroup.trainman1);
                }
                if (destGroup.trainman4.trainmanID != "")
                {
                    strDestContent += "," + string.Format("[{0}]{1}", destGroup.trainman1.trainmanNumber, destGroup.trainman4.trainmanName);
                    trainmanList.Add(destGroup.trainman1);
                }
                string strContent = string.Format("机组【{0}{1}】与机组【{2}{3}】执行交换", strSourceContent, sourceGroup.arriveTime,
                        strDestContent, destGroup.arriveTime);
                DBNameBoard.SaveChangeLog(InParams.TrainmanJiaolu, LBoardChangeType.btcExchangeGroup, strContent, InParams.DutyUser, trainmanList);

                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.SwapGroup:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion 交换机组

        #region 修改包乘机组的所属机车（移动机组到某一个机车下）

        public class In_ChangeTogetherTrian
        {
            public string TrainID;
            public string GroupID;
        }
        public InterfaceOutPut ChangeTogetherTrian(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                In_ChangeTogetherTrian InParams = javaScriptSerializer.Deserialize<In_ChangeTogetherTrian>(Data);
                if (string.IsNullOrEmpty(InParams.TrainID))
                    throw new Exception("机车不能为空！");
                if (string.IsNullOrEmpty(InParams.GroupID))
                    throw new Exception("机组不能为空！");

                string strSql = "update TAB_Nameplate_TrainmanJiaolu_OrderInTrain set strTrainGUID='" + InParams.TrainID + "',dtLastArriveTime='" + DateTime.Now + "'  where strGroupGUID = '" + InParams.GroupID + "'";
                if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql) == 1)
                {
                    output.result = 0;
                    output.resultStr = "执行成功！";
                }
                else
                    throw new Exception("找不到所要修改的机组！");

            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.ChangeTogetherTrian:" + ex.Message);
                throw ex;
            }
            return output;
        }



        #endregion

        #region 清除机组最近到达时间

        public class InClearArriveTime
        {
            //待清除的机组
            public string GroupGUID;
            //清除前的时间
            public DateTime OldTime;
            public TrainmanJiaoluMin TrainmanJiaolu = new TrainmanJiaoluMin();
            //值班员信息
            public DutyUser DutyUser = new DutyUser();
        }

        /// <summary>
        /// 清除机组最后到达时间
        /// </summary>
        public InterfaceOutPut ClearArriveTime(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InClearArriveTime InParams = javaScriptSerializer.Deserialize<InClearArriveTime>(Data);

                DateTime newArriveTime = DateTime.Parse("1899-12-30 00:00:00");
                DBNameBoard.UpdateGroupArriveTime(InParams.GroupGUID, newArriveTime);

                TF.RunSafty.NamePlate.MD.TuiQinTimeLog log = new TF.RunSafty.NamePlate.MD.TuiQinTimeLog();

                log.strGroupGUID = InParams.GroupGUID;
                log.dtCreateTime = DateTime.Now;
                log.nType = 1;
                log.dtOldArriveTime = InParams.OldTime;
                log.dtNewArriveTime = newArriveTime;
                log.strDutyUserNumber = InParams.DutyUser.strDutyName;
                log.strDutyUserName = InParams.DutyUser.strDutyName;
                TF.RunSafty.NamePlate.DB.DBTuiQinTimeLog.Save(log);

                Group group = new Group();
                if (GetGroup(InParams.GroupGUID, group))
                {
                    string strContent = string.Format("清除【{0}】最后到达时间", GetGroupString(group));

                    //在升级过程中，可能会有没有传这两个对象的数据
                    if (InParams.TrainmanJiaolu == null)
                    {
                        InParams.TrainmanJiaolu = new TrainmanJiaoluMin();
                    }

                    if (InParams.DutyUser == null)
                    {
                        InParams.DutyUser = new DutyUser();
                    }

                    SaveNameplateLog(group, InParams.TrainmanJiaolu, InParams.DutyUser, LBoardChangeType.btcClearArriveTime, strContent);
                }

                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.ClearArriveTime:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 修改机组最近到达时间


        public class InUpdateArriveTime
        {
            //待修改的机组
            public string GroupGUID;
            //修改前的时间
            public DateTime OldTime;
            //新时间
            public DateTime NewTime;
            public TrainmanJiaoluMin TrainmanJiaolu = new TrainmanJiaoluMin();
            //值班员信息
            public DutyUser DutyUser = new DutyUser();
        }

        /// <summary>
        /// 修改机组最后到达时间
        /// </summary>
        public InterfaceOutPut UpdateArriveTime(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InUpdateArriveTime InParams = javaScriptSerializer.Deserialize<InUpdateArriveTime>(Data);

                DateTime newArriveTime = InParams.NewTime;
                DBNameBoard.UpdateGroupArriveTime(InParams.GroupGUID, newArriveTime);

                TF.RunSafty.NamePlate.MD.TuiQinTimeLog log = new TF.RunSafty.NamePlate.MD.TuiQinTimeLog();

                log.strGroupGUID = InParams.GroupGUID;
                log.dtCreateTime = DateTime.Now;
                log.nType = 1;
                log.dtOldArriveTime = InParams.OldTime;
                log.dtNewArriveTime = InParams.NewTime;
                log.strDutyUserNumber = InParams.DutyUser.strDutyName;
                log.strDutyUserName = InParams.DutyUser.strDutyName;
                TF.RunSafty.NamePlate.DB.DBTuiQinTimeLog.Save(log);

                Group group = new Group();
                if (GetGroup(InParams.GroupGUID, group))
                {
                    string strContent = string.Format("修改【{0}】最后到达时间 由[{1}]修改为[{2}]", GetGroupString(group), InParams.OldTime.ToString(), InParams.NewTime.ToString());

                    //在升级过程中，可能会有没有传这两个对象的数据
                    if (InParams.TrainmanJiaolu == null)
                    {
                        InParams.TrainmanJiaolu = new TrainmanJiaoluMin();
                    }

                    if (InParams.DutyUser == null)
                    {
                        InParams.DutyUser = new DutyUser();
                    }

                    SaveNameplateLog(group, InParams.TrainmanJiaolu, InParams.DutyUser, LBoardChangeType.btcChangeArriveTime, strContent);
                }


                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.UpdateArriveTime:" + ex.Message);
                throw ex;
            }
            return output;
        }

        #region 获取人员所属机组信息
        public class InGetGroupOfTrainman
        {
            //人员GUID
            public string TrainmanGUID;
        }

        public class OutGetGroupOfTrainman
        {
            //机组信息
            public Group Group = new Group();
        }

        /// <summary>
        /// 获取人员所在机组
        /// </summary>
        public InterfaceOutPut GetGroupOfTrainman(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetGroupOfTrainman InParams = javaScriptSerializer.Deserialize<InGetGroupOfTrainman>(Data);
                OutGetGroupOfTrainman OutParams = new OutGetGroupOfTrainman();
                string strSql = @"select top 1 * from VIEW_Nameplate_Group 
                         where  strTrainmanGUID1 = @strTrainmanGUID or strTrainmanGUID2=@strTrainmanGUID or strTrainmanGUID3=@strTrainmanGUID or strTrainmanGUID4=@strTrainmanGUID";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strTrainmanGUID",InParams.TrainmanGUID)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    PSNameBoard.GroupFromDB(OutParams.Group, dt.Rows[0]);
                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetGroupOfTrainman:" + ex.Message);
                throw ex;
            }
            return output;
        }

        #endregion
        #endregion

        #region 获取机组正在值乘的计划信息

        public class InGetPlanOfGroup
        {
            //机组GUID
            public string GroupGUID;
        }

        public class OutGetPlanOfGroup
        {
            //机组所属人员计划
            public TrainPlan Plan = new TrainPlan();
            public int Exist = 0;
        }

        /// <summary>
        /// 获取机组当前正在值乘的计划信息
        /// </summary>
        public InterfaceOutPut GetPlanOfGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetPlanOfGroup InParams = javaScriptSerializer.Deserialize<InGetPlanOfGroup>(Data);
                OutGetPlanOfGroup OutParams = new OutGetPlanOfGroup();
                string strSql = @"Select * from VIEW_Plan_Train where  
                    strTrainPlanGUID in (select strTrainPlanGUID from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID)";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strGroupGUID",InParams.GroupGUID)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    PSNameBoard.TrainPlanFromDB(OutParams.Plan, dt.Rows[0]);
                    OutParams.Exist = 1;
                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetPlanOfGroup:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 静态方法获取机组信息
        public static Boolean GetGroup(string GroupGUID, Group Group)
        {
            string strSql = @"select top 1 * from VIEW_Nameplate_Group  where  strGroupGUID = @strGroupGUID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strGroupGUID",GroupGUID)
                };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                PS.PSNameBoard.GroupFromDB(Group, dt.Rows[0]);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 获取指定编号的机组信息
        public class InGetGroupInfo
        {
            //机组GUID
            public string GroupGUID;
        }

        public class OutGetGroupInfo
        {
            //机组信息
            public Group Group = new Group();
            //是否存在
            public int Exist = 0;
        }

        /// <summary>
        /// 获取指定编号的机组信息
        /// </summary>
        public InterfaceOutPut GetGroupInfo(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetGroupInfo InParams = javaScriptSerializer.Deserialize<InGetGroupInfo>(Data);
                OutGetGroupInfo OutParams = new OutGetGroupInfo();
                output.data = OutParams;
                if (GetGroup(InParams.GroupGUID, OutParams.Group))
                {
                    OutParams.Exist = 1;
                }
                else
                {
                    OutParams.Exist = 0;
                }


                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetGroupInfo:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion 获取指定编号的机组信息

        #region 获取人员所在的机组信息(机组内包含寓休信息)
        public class InGetTrainmanGroupWithInRoomTime
        {
            //人员工号
            public string TrainmanNumber;
        }

        public class OutGetTrainmanGroupWithInRoomTime
        {
            //机组
            public Group Group = new Group();
            //是否存在
            public int Exist = 0;
        }

        /// <summary>
        /// 获取人员所在的机组信息(机组内包含寓休信息)
        /// </summary>
        public InterfaceOutPut GetTrainmanGroupWithInRoomTime(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetTrainmanGroupWithInRoomTime InParams = javaScriptSerializer.Deserialize<InGetTrainmanGroupWithInRoomTime>(Data);
                OutGetTrainmanGroupWithInRoomTime OutParams = new OutGetTrainmanGroupWithInRoomTime();
                output.data = OutParams;
                string strSql = @"select top 1 *,
                     (select max(dtInRoomTime) from 
		              TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber1 and (DATEPART (Hour,dtInRoomTime) < 4 or DATEPART (Hour,dtInRoomTime) > 12) ) as InRoomTime1, 
                  (select max(dtInRoomTime) from 
		              TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber2 and (DATEPART (Hour,dtInRoomTime) < 4 or DATEPART (Hour,dtInRoomTime) > 12) ) as InRoomTime2, 
                      (select max(dtInRoomTime) from 
		              TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber3 and (DATEPART (Hour,dtInRoomTime) < 4 or DATEPART (Hour,dtInRoomTime) > 12) ) as InRoomTime3, 
                   (select max(dtInRoomTime) from 
		              TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber4 and (DATEPART (Hour,dtInRoomTime) < 4 or DATEPART (Hour,dtInRoomTime) > 12) ) as InRoomTime4 
                     from VIEW_Nameplate_Group 
                     where  strTrainmanNumber1 =@strTrainmanNumber or strTrainmanNumber2=@strTrainmanNumber or strTrainmanNumber3=@strTrainmanNumber or strTrainmanNumber4=@strTrainmanNumber";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strGroupGUID",InParams.TrainmanNumber)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    OutParams.Exist = 1;
                    PS.PSNameBoard.GroupWithRestFromDB(OutParams.Group, dt.Rows[0]);
                }

                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetTrainmanGroupWithInRoomTime:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取机组所在人员交路信息

        public class InGetTrainmanJiaoluOfGroup
        {
            //机组GUID
            public string GroupGUID;
        }

        public class OutGetTrainmanJiaoluOfGroup
        {
            //人员交路简略信息
            public TrainmanJiaoluMin TrainmanJiaolu = new TrainmanJiaoluMin();
            //是否存在
            public int Exist = 0;
        }

        /// <summary>
        /// 获取机组所在人员交路信息
        /// </summary>
        public InterfaceOutPut GetTrainmanJiaoluOfGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetTrainmanJiaoluOfGroup InParams = javaScriptSerializer.Deserialize<InGetTrainmanJiaoluOfGroup>(Data);
                OutGetTrainmanJiaoluOfGroup OutParams = new OutGetTrainmanJiaoluOfGroup();
                output.data = OutParams;
                string strSql = @"select * from tab_Base_TrainmanJiaolu where strTrainmanJiaoluGUID in 
                        (select strTrainmanJiaoluGUID from VIEW_Nameplate_Group where strGroupGUID = @strGroupGUID)";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strGroupGUID",InParams.GroupGUID)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    OutParams.Exist = 1;
                    OutParams.TrainmanJiaolu.jiaoluID = dt.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                    OutParams.TrainmanJiaolu.jiaoluName = dt.Rows[0]["strTrainmanJiaoluName"].ToString();
                    OutParams.TrainmanJiaolu.jiaoluType = TF.RunSafty.Utils.Parse.TFParse.DBToInt(dt.Rows[0]["nJiaoluType"], 0);
                }

                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetTrainmanJiaoluOfGroup:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region '获取多个人员交路的机组数组、按照时间排序'
        public class InGetGroupArrayInTrainmanJiaolus
        {
            //人员交路GUID列表
            public string TrainmanJiaolus;
            //交路类型
            public int JiaoluType;
        }

        public class OutGetGroupArrayInTrainmanJiaolus
        {
            //机组列表
            public GroupList Groups = new GroupList();
        }

        /// <summary>
        /// //获取多个人员交路的机组数组、按照时间排序
        /// </summary>
        public InterfaceOutPut GetGroupArrayInTrainmanJiaolus(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetGroupArrayInTrainmanJiaolus InParams = javaScriptSerializer.Deserialize<InGetGroupArrayInTrainmanJiaolus>(Data);
                OutGetGroupArrayInTrainmanJiaolus OutParams = new OutGetGroupArrayInTrainmanJiaolus();
                output.data = OutParams;
                string[] types = InParams.TrainmanJiaolus.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string trainmanjiaolus = "";
                for (int i = 0; i < types.Length; i++)
                {
                    if (trainmanjiaolus == "")
                    {
                        trainmanjiaolus = string.Format("'{0}'", types[i]);
                    }
                    else
                    {
                        trainmanjiaolus += string.Format(",'{0}'", types[i]);
                    }
                }
                if (trainmanjiaolus == "")
                {
                    trainmanjiaolus = string.Format("('{0}')", trainmanjiaolus);
                }
                else
                {
                    trainmanjiaolus = string.Format("({0})", trainmanjiaolus);
                }

                string strSql = @"select *,
                     (select max(dtInRoomTime) from 
		               TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber1) as InRoomTime1, 
                  (select max(dtInRoomTime) from 
		             TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber2) as InRoomTime2, 
                     (select max(dtInRoomTime) from 
		            TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber3) as InRoomTime3, 
                  (select max(dtInRoomTime) from 
		              TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber4) as InRoomTime4 
                     from VIEW_Nameplate_Group 
                    where strTrainmanJiaoluGUID in {0} order by groupState,(case when year(dtLastArriveTime)=1899  then 1 else 0 end ),dtLastArriveTime";


                //jltNamed
                if (InParams.JiaoluType == 2)
                {
                    strSql = @"select *,
                     (select max(dtInRoomTime) from 
                      TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber1) as InRoomTime1, 
                      (select max(dtInRoomTime) from 
                      TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber2) as InRoomTime2, 
                          (select max(dtInRoomTime) from 
                      TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber3) as InRoomTime3, 
                      (select max(dtInRoomTime) from 
                      TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber4) as InRoomTime4 
                     from VIEW_Nameplate_TrainmanJiaolu_Named 
                     where strTrainmanJiaoluGUID in {0}  and nCheciType=1 order by groupState,(case when year(dtLastArriveTime)=1899  then 1 else 0 end ),nCheciOrder";
                }
                //jltTogether
                if (InParams.JiaoluType == 4)
                {
                    strSql = @"select *, 
                         (select max(dtInRoomTime) from 
        		              TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber1) as InRoomTime1, 
                          (select max(dtInRoomTime) from 
                          TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber2) as InRoomTime2, 
                              (select max(dtInRoomTime) from 
                          TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber3) as InRoomTime3, 
                          (select max(dtInRoomTime) from 
                          TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber4) as InRoomTime4 
                       from VIEW_Nameplate_TrainmanJiaolu_TogetherTrain 
                     where strTrainmanJiaoluGUID in {0} order by groupState,(case when year(dtLastArriveTime)=1899  then 1 else 0 end ),strTrainmanJiaoluGUID,nOrder";
                }
                strSql = string.Format(strSql, trainmanjiaolus);
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Group g = new Group();
                    PS.PSNameBoard.GroupWithRestFromDB(g, dt.Rows[i]);
                    OutParams.Groups.Add(g);
                }

                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetGroupArrayInTrainmanJiaolus:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion



        #region 删除名牌变动日志


        public class InDelBoardChangeLog
        {
            //开始时间
            public DateTime dtBeginTime;
            //结束时间
            public DateTime dtEndTime;
            //交路GUID列表以逗号分割
            public string trainmanjiaolus;
            //变化类型
            public int changeType;
            //关键字
            public string key;
        }



        /// <summary>
        /// 删除名牌变动日志
        /// </summary>
        public InterfaceOutPut DeleteBoardChangeLog(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InQueryBoardChangeLog InParams = javaScriptSerializer.Deserialize<InQueryBoardChangeLog>(Data);
                OutQueryBoardChangeLog OutParams = new OutQueryBoardChangeLog();
                string[] types = InParams.trainmanjiaolus.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string trainmanjiaolus = "";
                for (int i = 0; i < types.Length; i++)
                {
                    if (trainmanjiaolus == "")
                    {
                        trainmanjiaolus = string.Format("'{0}'", types[i]);
                    }
                    else
                    {
                        trainmanjiaolus += string.Format(",'{0}'", types[i]);
                    }
                }
                if (trainmanjiaolus == "")
                {
                    trainmanjiaolus = string.Format("('{0}')", trainmanjiaolus);
                }
                else
                {
                    trainmanjiaolus = string.Format("({0})", trainmanjiaolus);
                }
                string condition = " and 1=1";
                if (InParams.changeType > 0)
                {
                    condition = condition + " and nBoardChangeType = " + InParams.changeType.ToString();
                }


                if (!string.IsNullOrEmpty(InParams.key))
                {
                    condition = condition + string.Format(" and strContent like '%{0}%'", InParams.key);
                }
                string strSql = @" delete from TAB_Nameplate_Log where dtEventTime>=@dtBeginTime
                             and dtEventTime <=@dtEndTime and strTrainmanJiaoluGUID in {0} ";

                strSql = string.Format(strSql, trainmanjiaolus);


                strSql = strSql + condition + "";


                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("dtBeginTime",InParams.dtBeginTime),
                    new SqlParameter("dtEndTime",InParams.dtEndTime)
                };
                
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);


                output.resultStr = "删除成功!";
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.DelBoardChangeLog:" + ex.Message);
                throw ex;
            }
            return output;
        }





        #endregion





        #region 查询名牌变动日志
        public class InQueryBoardChangeLog
        {
            //开始时间
            public DateTime dtBeginTime;
            //结束时间
            public DateTime dtEndTime;
            //交路GUID列表以逗号分割
            public string trainmanjiaolus;
            //变化类型
            public int changeType;
            //关键字
            public string key;
        }

        public class OutQueryBoardChangeLog
        {
            //变动日志列表
            public BoardChangeLogList Logs = new BoardChangeLogList();
        }

        /// <summary>
        /// 查询名牌变动日志
        /// </summary>
        public InterfaceOutPut QueryBoardChangeLog(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InQueryBoardChangeLog InParams = javaScriptSerializer.Deserialize<InQueryBoardChangeLog>(Data);
                OutQueryBoardChangeLog OutParams = new OutQueryBoardChangeLog();
                string[] types = InParams.trainmanjiaolus.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string trainmanjiaolus = "";
                for (int i = 0; i < types.Length; i++)
                {
                    if (trainmanjiaolus == "")
                    {
                        trainmanjiaolus = string.Format("'{0}'", types[i]);
                    }
                    else
                    {
                        trainmanjiaolus += string.Format(",'{0}'", types[i]);
                    }
                }
                if (trainmanjiaolus == "")
                {
                    trainmanjiaolus = string.Format("('{0}')", trainmanjiaolus);
                }
                else
                {
                    trainmanjiaolus = string.Format("({0})", trainmanjiaolus);
                }
                string condition = " and 1=1";
                if (InParams.changeType > 0)
                {
                    condition = condition + " and nBoardChangeType = " + InParams.changeType.ToString();
                }


                if (!string.IsNullOrEmpty(InParams.key))
                {
                    condition = condition + string.Format(" and strContent like '%{0}%'", InParams.key);
                }
                string strSql = @"select * from TAB_Nameplate_Log where dtEventTime>=@dtBeginTime
                             and dtEventTime <=@dtEndTime and strTrainmanJiaoluGUID in {0} ";

                strSql = string.Format(strSql, trainmanjiaolus);


                strSql = strSql + condition + "order by dtEventTime";


                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("dtBeginTime",InParams.dtBeginTime),
                    new SqlParameter("dtEndTime",InParams.dtEndTime)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BoardChangeLog log = new BoardChangeLog();


                    log.strLogGUID = dt.Rows[i]["strLogGUID"].ToString();
                    log.strTrainmanJiaoluGUID = dt.Rows[i]["strTrainmanJiaoluGUID"].ToString();
                    log.strTrainmanJiaoluName = dt.Rows[i]["strTrainmanJiaoluName"].ToString();
                    log.nBoardChangeType = TF.RunSafty.Utils.Parse.TFParse.DBToInt(dt.Rows[i]["nBoardChangeType"], 0);
                    log.strContent = dt.Rows[i]["strContent"].ToString();
                    log.strDutyUserGUID = dt.Rows[i]["strDutyUserGUID"].ToString();
                    log.strDutyUserNumber = dt.Rows[i]["strDutyUserNumber"].ToString();
                    log.strDutyUserName = dt.Rows[i]["strDutyUserName"].ToString();
                    log.dtEventTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dt.Rows[i]["dtEventTime"], DateTime.Parse("1899-01-01"));
                    log.nid = TF.RunSafty.Utils.Parse.TFParse.DBToInt(dt.Rows[i]["nid"], 0);

                    OutParams.Logs.Add(log);
                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.QueryBoardChangeLog:" + ex.Message);
                throw ex;
            }
            return output;
        }


        #endregion

        #region 获取退勤日志列表
        public class InGetTuiqinTimeLog
        {
            //开始时间按
            public DateTime BeginTime;
            //结束时间
            public DateTime EndTime;
        }

        public class OutGetTuiqinTimeLog
        {
            //日志列表
            public TuiQinTimeLogList Logs = new TuiQinTimeLogList();
        }

        /// <summary>
        /// 获取退勤时间修改记录
        /// </summary>
        public InterfaceOutPut GetTuiqinTimeLog(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetTuiqinTimeLog InParams = javaScriptSerializer.Deserialize<InGetTuiqinTimeLog>(Data);
                OutGetTuiqinTimeLog OutParams = new OutGetTuiqinTimeLog();
                string strSql = "select * from Tab_Plan_ModifyLastArriveTime_Log where dtCreateTime between @BeginTime and @EndTime";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("BeginTime",InParams.BeginTime),
                    new SqlParameter("EndTime",InParams.EndTime)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TuiQinTimeLog log = new TuiQinTimeLog();
                    PSNameBoard.TuiQinTimeLogFromDB(log, dt.Rows[i]);
                    OutParams.Logs.Add(log);
                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetTuiqinTimeLog:" + ex.Message);
                throw ex;
            }
            return output;
        }

        #endregion

        public static void SaveNameplateLog(Group group, TrainmanJiaoluMin TrainmanJiaolu, DutyUser DutyUser, LBoardChangeType changeType, string strContent)
        {
            TrainmanList trainmanList = new TrainmanList();
            if (group.trainman1.trainmanID != "")
            {
                trainmanList.Add(group.trainman1);
            }
            if (group.trainman2.trainmanID != "")
            {
                trainmanList.Add(group.trainman2);
            }
            if (group.trainman3.trainmanID != "")
            {
                trainmanList.Add(group.trainman3);
            }
            if (group.trainman4.trainmanID != "")
            {
                trainmanList.Add(group.trainman4);
            }
            DBNameBoard.SaveChangeLog(TrainmanJiaolu, changeType, strContent, DutyUser, trainmanList);
        }

        public static string GetGroupString(Group group)
        {
            string result = string.Empty;
            if (group.trainman1.trainmanID != "")
            {
                result += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman1.trainmanName);
            }
            if (group.trainman2.trainmanID != "")
            {
                result += "," + string.Format("[{0}]{1}", group.trainman2.trainmanNumber, group.trainman2.trainmanName);
            }
            if (group.trainman3.trainmanID != "")
            {
                result += "," + string.Format("[{0}]{1}", group.trainman3.trainmanNumber, group.trainman3.trainmanName);
            }
            if (group.trainman4.trainmanID != "")
            {
                result += "," + string.Format("[{0}]{1}", group.trainman1.trainmanNumber, group.trainman4.trainmanName);
            }
            if (result != string.Empty)
            {
                result = result.Substring(1, result.Length - 1);
            }

            return result;
        }

        public static string GetGroupStringByID(string GroupGUID)
        {
            Group group = new Group();
            string result = string.Empty;
            if (GetGroup(GroupGUID, group))
            {
                result = GetGroupString(group);
            }

            return result;
        }

        public static string GetTrainmanStringByID(string TrainmanGUID)
        {
            string strSql = @"select top 1 * from TAB_Org_Trainman  where  strTrainmanGUID = @TrainmanGUID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("TrainmanGUID",TrainmanGUID)
                };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return string.Format("[{0}]{1}", dt.Rows[0]["strTrainmanNumber"].ToString(), dt.Rows[0]["strTrainmanName"].ToString());
            }
            else
            {
                return "";
            }
        }

        public static void SaveNameplateLogWithGUID(string GroupGUID, DutyUser DutyUser, LBoardChangeType changeType, string strContent)
        {
            TrainmanJiaoluMin TrainmanJiaolu = new TrainmanJiaoluMin();
            Group group = new Group();
            string strSql = @"select top 1 * from VIEW_Nameplate_Group_TrainmanJiaolu  where  strGroupGUID = @GroupGUID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("GroupGUID",GroupGUID)
                };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                TrainmanJiaolu.jiaoluID = dt.Rows[0]["strTrainmanJiaoluGUID"].ToString();

                strSql = "select * from TAB_Base_TrainmanJiaolu where strTrainmanJiaoluGUID = '{0}'";
                strSql = string.Format(strSql, TrainmanJiaolu.jiaoluID);

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    TrainmanJiaolu.jiaoluName = dt.Rows[0]["strTrainmanJiaoluName"].ToString();
                    TrainmanJiaolu.jiaoluType = Convert.ToInt32(dt.Rows[0]["nJiaoluType"]);
                }
            }
            GetGroup(GroupGUID, group);
            SaveNameplateLog(group, TrainmanJiaolu, DutyUser, changeType, strContent);
        }

    }


}
