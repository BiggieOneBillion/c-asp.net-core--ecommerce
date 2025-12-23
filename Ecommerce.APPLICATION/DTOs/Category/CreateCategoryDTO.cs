

namespace Ecommerce.APPLICATION.DTOs.Category
{
    public record CreateCategoryDTO
    {
        public string CategoryName { get; init;} = string.Empty;

        public string CategoryDescription { get; init;} = string.Empty;

        public bool ActiveStatus {get; init;} = true;
    }
}