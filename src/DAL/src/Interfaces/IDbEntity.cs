namespace MovieAPI.DAL;

/// <summary>
/// 数据库实体标识
/// </summary>
public interface IDbEntity { }
/// <summary>
/// 视图模型标识
/// </summary>
public interface IView : IDbEntity { }
/// <summary>
/// 表模型标识
/// </summary>
public interface ITable : IDbEntity { }
