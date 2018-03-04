using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace TF.RunSafty.SiteLogic
{
    public class DBSite : DBUtility
    {
        #region  ====================================外点日志数据库操作====================================

        #region 修改操作
        public void UpdateOL(LCSite.InputOperationLog ol)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update Tab_Site_OperationLog set ");
            strSql.Append(" strUrlDescription = @strUrlDescription, ");
            strSql.Append(" Pageid = @Pageid, ");
            strSql.Append(" OperationTime = @OperationTime, ");
            strSql.Append(" TrainmanNumber = @TrainmanNumber ");
            strSql.Append(" where Strid = @Strid ");
            using (var conn = GetConnection())
            {
                conn.Execute(strSql.ToString(), ol);
            }
        }
        #endregion

        #region 删除操作
        public void DelOL(LCSite.InputOperationLog ol)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Tab_Site_OperationLog ");
            strSql.Append(" where Strid = @Strid ");
            using (var conn = GetConnection())
            {
                conn.Execute(strSql.ToString(), new { Strid = ol.Strid });
            }
        }
        #endregion

        #region 添加操作
        public void AddOL(LCSite.InputOperationLog ol)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_Site_OperationLog");
            strSql.Append("(strUrlDescription,Strid,Pageid,OperationTime,TrainmanNumber)");
            strSql.Append("values(@strUrlDescription,@Strid,@Pageid,@OperationTime,@TrainmanNumber)");
            using (var conn = GetConnection())
            {
                conn.Execute(strSql.ToString(), ol);
            }
        }
        #endregion

        #endregion

        #region  ====================================外点干部出入寓====================================

        #region 修改操作
        public void UpdateInOutRoom(LCSite.InputGBInOutRoom gbInOuRoom)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update Tab_Site_GanBuInOutRoom set ");
            strSql.Append(" strDuty = @strDuty, ");
            strSql.Append(" nOutRoomLoginType = @nOutRoomLoginType, ");
            strSql.Append(" nIsOutRoom = @nIsOutRoom, ");
            strSql.Append(" strTypeName = @strTypeName, ");
            strSql.Append(" strInRoomQuDuan = @strInRoomQuDuan, ");
            strSql.Append(" strTrainmanID = @strTrainmanID, ");
            strSql.Append(" strCadresDutyTypeID = @strCadresDutyTypeID, ");
            strSql.Append(" strOutRoomQuDuan = @strOutRoomQuDuan, ");
            strSql.Append(" strInRoomType = @strInRoomType, ");
            strSql.Append(" strInRoomPhotoID = @strInRoomPhotoID, ");
            strSql.Append(" strNumber = @strNumber, ");
            strSql.Append(" nSiteID = @nSiteID, ");
            strSql.Append(" strJianChaContent = @strJianChaContent, ");
            strSql.Append(" dtOutRoomDateTime = @dtOutRoomDateTime, ");
            strSql.Append(" dtInputDateTime = @dtInputDateTime, ");
            strSql.Append(" strSiteNumber = @strSiteNumber, ");
            strSql.Append(" dtInRoomDateTime = @dtInRoomDateTime, ");
            strSql.Append(" nInRoomLoginType = @nInRoomLoginType, ");
            strSql.Append(" dtOutRoomCheCiDateTime = @dtOutRoomCheCiDateTime, ");
            strSql.Append(" strDepartmentName = @strDepartmentName, ");
            strSql.Append(" strInRoomCheCi = @strInRoomCheCi, ");
            strSql.Append(" strRemark = @strRemark, ");
            strSql.Append(" strZhengGaiYiJian = @strZhengGaiYiJian, ");
            strSql.Append(" strCheJianName = @strCheJianName, ");
            strSql.Append(" strOutRoomCheCi = @strOutRoomCheCi, ");
            strSql.Append(" strName = @strName, ");
            strSql.Append(" strOutRoomPhotoID = @strOutRoomPhotoID, ");
            strSql.Append(" strSiteName = @strSiteName ");
            strSql.Append(" where strID = @strID ");

            using (var conn = GetConnection())
            {
                conn.Execute(strSql.ToString(), gbInOuRoom);
            }
        }
        #endregion

        #region 删除操作
        public void DelInOutRoom(LCSite.InputGBInOutRoom gbInOuRoom)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Tab_Site_GanBuInOutRoom ");
            strSql.Append(" where strID = @strID ");
            using (var conn = GetConnection())
            {
                conn.Execute(strSql.ToString(), new { Strid = gbInOuRoom.strID });
            }
        }
        #endregion

        #region 添加操作
        public void AddInOutRoom(LCSite.InputGBInOutRoom gbInOuRoom)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_Site_GanBuInOutRoom");
            strSql.Append("(strDuty,nOutRoomLoginType,nIsOutRoom,strTypeName,strInRoomQuDuan,strTrainmanID,strCadresDutyTypeID,strID,strOutRoomQuDuan,strInRoomType,strInRoomPhotoID,strNumber,nSiteID,strJianChaContent,dtOutRoomDateTime,dtInputDateTime,strSiteNumber,dtInRoomDateTime,nInRoomLoginType,dtOutRoomCheCiDateTime,strDepartmentName,strInRoomCheCi,strRemark,strZhengGaiYiJian,strCheJianName,strOutRoomCheCi,strName,strOutRoomPhotoID,strSiteName)");
            strSql.Append("values(@strDuty,@nOutRoomLoginType,@nIsOutRoom,@strTypeName,@strInRoomQuDuan,@strTrainmanID,@strCadresDutyTypeID,@strID,@strOutRoomQuDuan,@strInRoomType,@strInRoomPhotoID,@strNumber,@nSiteID,@strJianChaContent,@dtOutRoomDateTime,@dtInputDateTime,@strSiteNumber,@dtInRoomDateTime,@nInRoomLoginType,@dtOutRoomCheCiDateTime,@strDepartmentName,@strInRoomCheCi,@strRemark,@strZhengGaiYiJian,@strCheJianName,@strOutRoomCheCi,@strName,@strOutRoomPhotoID,@strSiteName)");

            using (var conn = GetConnection())
            {
                conn.Execute(strSql.ToString(), gbInOuRoom);
            }
        }
        #endregion

        #endregion

        #region  ====================================房间巡查增删改====================================

        #region 修改操作
        public void UpdateRoomPatrol(LCSite.InputRoomPatrol RoomPatrol)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update TAB_Site_RoomPatrol set ");
            strSql.Append(" dtPatrolTime = @dtPatrolTime, ");
            strSql.Append(" nSiteID = @nSiteID, ");
            strSql.Append(" strSiteName = @strSiteName, ");
            strSql.Append(" nLoginType = @nLoginType, ");
            strSql.Append(" strNumber = @strNumber, ");
            strSql.Append(" strPhotoID = @strPhotoID, ");
            strSql.Append(" strTrainmanID = @strTrainmanID, ");
            strSql.Append(" strName = @strName ");
            strSql.Append(" where strID = @strID ");
            using (var conn = GetConnection())
            {
                conn.Execute(strSql.ToString(), RoomPatrol);
            }
        }
        #endregion

        #region 删除操作
        public void DelRoomPatrol(LCSite.InputRoomPatrol RoomPatrol)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_Site_RoomPatrol ");
            strSql.Append(" where strID = @strID ");
            using (var conn = GetConnection())
            {
                conn.Execute(strSql.ToString(), new { Strid = RoomPatrol.strID });
            }
        }
        #endregion

        #region 添加操作
        public void AddRoomPatrol(LCSite.InputRoomPatrol RoomPatrol)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Site_RoomPatrol");
            strSql.Append("(dtPatrolTime,nSiteID,strSiteName,nLoginType,strNumber,strPhotoID,strTrainmanID,strID,strName)");
            strSql.Append("values(@dtPatrolTime,@nSiteID,@strSiteName,@nLoginType,@strNumber,@strPhotoID,@strTrainmanID,@strID,@strName)");
            using (var conn = GetConnection())
            {
                conn.Execute(strSql.ToString(), RoomPatrol);
            }
        }
        #endregion

        #endregion

        #region  ====================================司机请假====================================

        #region 修改操作
        public void UpdateTMLeave(LCSite.InputTMLeave TMLeave)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update Tab_Site_TMLeave set ");
            strSql.Append(" strLeaveRemark = @strLeaveRemark, ");
            strSql.Append(" nEndLeaveLoginType = @nEndLeaveLoginType, ");
            strSql.Append(" strName = @strName, ");
            strSql.Append(" nEndLeaveAdminLoginType = @nEndLeaveAdminLoginType, ");
            strSql.Append(" strTrainmanID = @strTrainmanID, ");
            strSql.Append(" nAdminLoginType = @nAdminLoginType, ");
            strSql.Append(" strTimeOutReason = @strTimeOutReason, ");
            strSql.Append(" strEndAdminPhotoID = @strEndAdminPhotoID, ");
            strSql.Append(" nTimeOutMinute = @nTimeOutMinute, ");
            strSql.Append(" strAdminPhotoID = @strAdminPhotoID, ");
            strSql.Append(" nIsEndLeave = @nIsEndLeave, ");
            strSql.Append(" strUserNumber = @strUserNumber, ");
            strSql.Append(" strLeavePhotoID = @strLeavePhotoID, ");
            strSql.Append(" nSiteID = @nSiteID, ");
            strSql.Append(" dtLeaveTime = @dtLeaveTime, ");
            strSql.Append(" strEndLeaveName = @strEndLeaveName, ");
            strSql.Append(" strSiteName = @strSiteName, ");
            strSql.Append(" strEndLeavePhotoID = @strEndLeavePhotoID, ");
            strSql.Append(" dtEndLeaveTime = @dtEndLeaveTime, ");
            strSql.Append(" strNumber = @strNumber, ");
            strSql.Append(" nLeaveLoginType = @nLeaveLoginType, ");
            strSql.Append(" strUserID = @strUserID, ");
            strSql.Append(" strUserName = @strUserName, ");
            strSql.Append(" strEndLeaveTestAlcoholID = @strEndLeaveTestAlcoholID, ");
            strSql.Append(" nIsTimeOut = @nIsTimeOut, ");
            strSql.Append(" strEndLeaveNumber = @strEndLeaveNumber, ");
            strSql.Append(" dtLeaveCount = @dtLeaveCount, ");
            strSql.Append(" nTestAlcoholResult = @nTestAlcoholResult, ");
            strSql.Append(" strEndLeaveAdminID = @strEndLeaveAdminID ");
            strSql.Append(" where strid = @strid ");
            using (var conn = GetConnection())
            {
                conn.Execute(strSql.ToString(), TMLeave);
            }
        }
        #endregion

        #region 删除操作
        public void DelTMLeave(LCSite.InputTMLeave TMLeave)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Tab_Site_TMLeave ");
            strSql.Append(" where strid = @strid ");
            using (var conn = GetConnection())
            {
                conn.Execute(strSql.ToString(), new { strid = TMLeave.strid });
            }
        }
        #endregion

        #region 添加操作
        public void AddTMLeave(LCSite.InputTMLeave TMLeave)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_Site_TMLeave");
            strSql.Append("(strLeaveRemark,nEndLeaveLoginType,strName,nEndLeaveAdminLoginType,strTrainmanID,nAdminLoginType,strTimeOutReason,strEndAdminPhotoID,nTimeOutMinute,strAdminPhotoID,nIsEndLeave,strUserNumber,strLeavePhotoID,nSiteID,dtLeaveTime,strEndLeaveName,strSiteName,strEndLeavePhotoID,dtEndLeaveTime,strNumber,nLeaveLoginType,strid,strUserID,strUserName,strEndLeaveTestAlcoholID,nIsTimeOut,strEndLeaveNumber,dtLeaveCount,nTestAlcoholResult,strEndLeaveAdminID)");
            strSql.Append("values(@strLeaveRemark,@nEndLeaveLoginType,@strName,@nEndLeaveAdminLoginType,@strTrainmanID,@nAdminLoginType,@strTimeOutReason,@strEndAdminPhotoID,@nTimeOutMinute,@strAdminPhotoID,@nIsEndLeave,@strUserNumber,@strLeavePhotoID,@nSiteID,@dtLeaveTime,@strEndLeaveName,@strSiteName,@strEndLeavePhotoID,@dtEndLeaveTime,@strNumber,@nLeaveLoginType,@strid,@strUserID,@strUserName,@strEndLeaveTestAlcoholID,@nIsTimeOut,@strEndLeaveNumber,@dtLeaveCount,@nTestAlcoholResult,@strEndLeaveAdminID)");
            using (var conn = GetConnection())
            {
                conn.Execute(strSql.ToString(), TMLeave);
            }
        }
        #endregion

        #endregion

        #region 插入图片和日期的对应关系
        public void AddSite_Photo(string GUID, string Date)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Site_Photo");
            strSql.Append("(PicName,strDate)");
            strSql.Append("values(@PicName,@strDate)");
            using (var conn = GetConnection())
            {
                conn.Execute(strSql.ToString(), new { PicName = GUID, strDate = Date });
            }
        }
        #endregion


    }
}
