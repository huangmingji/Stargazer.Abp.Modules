using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Stargazer.Abp.Categories.Application.Contracts
{
    [Serializable]
    public class CategoryDto : AuditedEntityDto<Guid>
    {
        public string UniqueName { get; set; } = "";
        
        public string DisplayName { get; set; } = "";
        
        public string Code { get; set; } = "";
        
        public int Level { get; set; }
        
        public Guid? ParentId { get; set; }

        public string Description { get; set; } = "";

        public string MediaResources { get; set; } = "";
        
        public bool IsHidden { get; set; }
    }
}