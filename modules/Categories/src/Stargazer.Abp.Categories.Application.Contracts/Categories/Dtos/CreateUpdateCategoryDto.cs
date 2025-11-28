using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;

namespace Stargazer.Abp.Categories.Application.Contracts
{
    [Serializable]
    public class CreateUpdateCategoryDto
    {
        public Guid  StoreId { get; set; }
        
        [DisplayName("CategoryParentId")]
        public Guid? ParentId { get; set; }
        
        [DisplayName("CategoryUniqueName")]
        public string UniqueName { get; set; } = "";
        
        [Required]
        [DisplayName("CategoryDisplayName")]
        public string DisplayName { get; set; } = "";

        public string Code { get; set; } = "";

        [DisplayName("CategoryDescription")]
        public string Description { get; set; } = "";

        [DisplayName("CategoryMediaResources")]
        public string MediaResources { get; set; } = "";
        
        [DisplayName("CategoryIsHidden")]
        public bool IsHidden { get; set; }
    }
}