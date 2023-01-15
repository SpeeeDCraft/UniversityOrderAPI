namespace UniversityOrderAPI.Models.Category;

public class CreateCategoryRequest
{
    public string Name { get; set; }
}


public class CreateCategoryResponse : ISingleResult<CategoryAPIDTO>
{
    
}