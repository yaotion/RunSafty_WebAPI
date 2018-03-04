using System;
using System.Collections.Generic;

namespace TF.Api.Entity
{
    /// <summary>
    /// 类名：RunRecord_FileEntity
    /// 描述：运行记录文件
    /// </summary>
    public class RunRecord_FileEntity
    {
        public string workid
        { get; set; }

        public String worktype
        { get; set; }

        public String sid
        { get; set; }

        public List<RunRecord_File_FileListEntity> FileList
        { get; set; }
    }

    /// <summary>
    /// 类名：RunRecord_File_FileListEntity
    /// 描述：运行记录文件列表实体类
    /// </summary>
    public class RunRecord_File_FileListEntity
    {
        public string fid
        { get; set; }
    }
}
