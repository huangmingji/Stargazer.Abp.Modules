using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Stargazer.Abp.Categories.Domain
{
    public class Category : AuditedAggregateRoot<Guid>, IMultiTenant
    {
        public  Guid? TenantId { get; protected set; }

        public Guid StoreId { get; protected set; }

        public  string? UniqueName { get; protected set; }
        
        public  string Description { get; protected set; }
        
        public  string MediaResources { get; protected set; } 
        
        public  bool IsHidden { get; protected set; }

        #region Properties of ITree

        public string DisplayName { get; set; }

        public string Code { get; set; } = "";
        
        public  int Level { get; set; }
        
        public  Guid? ParentId { get; set; }
        
        #endregion

        public Category(
            Guid id,
            Guid storeId,
            string? uniqueName,
            string displayName,
            string code,
            string description,
            string mediaResources,
            bool isHidden,
            Guid? parentId
        ) : base(id)
        {
            Check.NotNullOrWhiteSpace(displayName, nameof(displayName));
            
            StoreId = storeId;
            UniqueName = uniqueName;
            DisplayName = displayName;
            Code = code;
            Description = description;
            MediaResources = mediaResources;
            IsHidden = isHidden;
            ParentId = parentId;
        }

        public void Update(
            string? uniqueName,
            string displayName,
            string code,
            string description,
            string mediaResources,
            bool isHidden,
            Guid? parentId)
        {
            Check.NotNullOrWhiteSpace(displayName, nameof(displayName));
            
            UniqueName = uniqueName;
            DisplayName = displayName;
            Code = code;
            Description = description;
            MediaResources = mediaResources;
            IsHidden = isHidden;
            ParentId = parentId;
        }
    }
}
