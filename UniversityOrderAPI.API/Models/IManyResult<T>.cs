namespace UniversityOrderAPI.Models;

public class IManyResult<T>
{
    public IEnumerable<T> Items { get; set; }
}