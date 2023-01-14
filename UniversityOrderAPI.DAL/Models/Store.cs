namespace UniversityOrderAPI.DAL.Models;


/// <summary>
/// Магазин (используется для того, чтобы студент заполнил базу данных своими данными)
/// </summary>
public class Store:BaseDBModel
{
    /// <summary>
    /// Название
    /// </summary>
    public int Name { get; set; }
}