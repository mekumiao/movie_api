using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MovieAPI.DAL;

/// <summary>
/// 上传的文件表
/// </summary>
public class UploadFile : IOperationEntity, ITable
{
    [Key, StringColumn]
    public string SaveName { get; set; } = string.Empty;
    [StringColumn]
    public string UploadName { get; set; } = string.Empty;
    [PathColumn]
    public string FilePath { get; set; } = string.Empty;
    [RemarkColumn]
    public string Remark { get; set; } = string.Empty;
    /// <summary>
    /// 是否已禁用
    /// </summary>
    public bool IsDisabled { get; set; }
    /// <summary>
    /// 文件扩展名（例如：.txt）
    /// </summary>
    [StringColumn]
    public string Ext { get; set; } = string.Empty;
    /// <summary>
    /// 创建人ID
    /// </summary>
    [IgnoreDataMember]
    public long? CreateUserId { get; set; }
    /// <summary>
    /// 修改人ID
    /// </summary>
    [IgnoreDataMember]
    public long? UpdateUserId { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    [IgnoreDataMember]
    public DateTime? CreateTime { get; set; }
    /// <summary>
    /// 修改时间
    /// </summary>
    [IgnoreDataMember]
    public DateTime? UpdateTime { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    [IgnoreDataMember]
    public User? CreateUser { get; set; }
    /// <summary>
    /// 修改人
    /// </summary>
    [IgnoreDataMember]
    public User? UpdateUser { get; set; }
}
