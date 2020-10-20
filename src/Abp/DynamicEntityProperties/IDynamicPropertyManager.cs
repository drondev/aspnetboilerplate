using System;
using System.Threading.Tasks;

namespace Abp.DynamicEntityProperties
{
    public interface IDynamicPropertyManager
    {
        DynamicProperty Get(Guid id);

        Task<DynamicProperty> GetAsync(Guid id);

        DynamicProperty Get(string propertyName);

        Task<DynamicProperty> GetAsync(string propertyName);

        void Add(DynamicProperty dynamicProperty);

        Task AddAsync(DynamicProperty dynamicProperty);

        void Update(DynamicProperty dynamicProperty);

        Task UpdateAsync(DynamicProperty dynamicProperty);

        void Delete(Guid id);

        Task DeleteAsync(Guid id);
    }
}
