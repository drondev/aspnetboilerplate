using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Features;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Runtime.Caching;

namespace Abp.Application.Editions
{
    public class AbpEditionManager : IDomainService
    {
        private readonly IAbpZeroFeatureValueStore _featureValueStore;

        public IQueryable<Edition> Editions => EditionRepository.GetAll();

        public ICacheManager CacheManager { get; set; }

        public IFeatureManager FeatureManager { get; set; }

        protected IRepository<Edition> EditionRepository { get; set; }

        public AbpEditionManager(
            IRepository<Edition> editionRepository,
            IAbpZeroFeatureValueStore featureValueStore)
        {
            _featureValueStore = featureValueStore;
            EditionRepository = editionRepository;
        }

        public virtual Task<string> GetFeatureValueOrNullAsync(Guid editionId, string featureName)
        {
            return _featureValueStore.GetEditionValueOrNullAsync(editionId, featureName);
        }

        public virtual string GetFeatureValueOrNull(Guid editionId, string featureName)
        {
            return _featureValueStore.GetEditionValueOrNull(editionId, featureName);
        }

        public virtual Task SetFeatureValueAsync(Guid editionId, string featureName, string value)
        {
            return _featureValueStore.SetEditionFeatureValueAsync(editionId, featureName, value);
        }

        public virtual void SetFeatureValue(Guid editionId, string featureName, string value)
        {
            _featureValueStore.SetEditionFeatureValue(editionId, featureName, value);
        }

        public virtual async Task<IReadOnlyList<NameValue>> GetFeatureValuesAsync(Guid editionId)
        {
            var values = new List<NameValue>();

            foreach (var feature in FeatureManager.GetAll())
            {
                values.Add(new NameValue(feature.Name, await GetFeatureValueOrNullAsync(editionId, feature.Name) ?? feature.DefaultValue));
            }

            return values;
        }

        public virtual IReadOnlyList<NameValue> GetFeatureValues(Guid editionId)
        {
            var values = new List<NameValue>();

            foreach (var feature in FeatureManager.GetAll())
            {
                values.Add(new NameValue(feature.Name, GetFeatureValueOrNull(editionId, feature.Name) ?? feature.DefaultValue));
            }

            return values;
        }

        public virtual async Task SetFeatureValuesAsync(Guid editionId, params NameValue[] values)
        {
            if (values.IsNullOrEmpty())
            {
                return;
            }

            foreach (var value in values)
            {
                await SetFeatureValueAsync(editionId, value.Name, value.Value);
            }
        }

        public virtual void SetFeatureValues(Guid editionId, params NameValue[] values)
        {
            if (values.IsNullOrEmpty())
            {
                return;
            }

            foreach (var value in values)
            {
                SetFeatureValue(editionId, value.Name, value.Value);
            }
        }

        public virtual Task CreateAsync(Edition edition)
        {
            return EditionRepository.InsertAsync(edition);
        }

        public virtual void Create(Edition edition)
        {
            EditionRepository.Insert(edition);
        }

        public virtual Task<Edition> FindByNameAsync(string name)
        {
            return EditionRepository.FirstOrDefaultAsync(edition => edition.Name == name);
        }

        public virtual Edition FindByName(string name)
        {
            return EditionRepository.FirstOrDefault(edition => edition.Name == name);
        }

        public virtual Task<Edition> FindByIdAsync(Guid id)
        {
            return EditionRepository.FirstOrDefaultAsync(id);
        }

        public virtual Edition FindById(Guid id)
        {
            return EditionRepository.FirstOrDefault(id);
        }

        public virtual Task<Edition> GetByIdAsync(Guid id)
        {
            return EditionRepository.GetAsync(id);
        }

        public virtual Edition GetById(Guid id)
        {
            return EditionRepository.Get(id);
        }

        public virtual Task DeleteAsync(Edition edition)
        {
            return EditionRepository.DeleteAsync(edition);
        }

        public virtual void Delete(Edition edition)
        {
            EditionRepository.Delete(edition);
        }
    }
}
