namespace Ecommerce.APPLICATION.DTOs.Category
{
    /// <summary>
    /// DTO for creating a new category
    /// </summary>
    public record CreateCategoryDTO
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        public string Name { get; init;} = string.Empty;

        /// <summary>
        /// Detailed description of the category
        /// </summary>
        public string Description { get; init;} = string.Empty;

         /// <summary>
        /// Parent Category if any
        /// </summary>
        public string? ParentCategoryId { get; init;} = null;

        /// <summary>
        /// Status indicating if the category is active (default: true)
        /// </summary>
        public bool ActiveStatus {get; init;} = true;
    }
}