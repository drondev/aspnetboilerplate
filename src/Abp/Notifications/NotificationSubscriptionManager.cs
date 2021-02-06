using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Json;

namespace Abp.Notifications
{
    /// <summary>
    /// Implements <see cref="INotificationSubscriptionManager"/>.
    /// </summary>
    public class NotificationSubscriptionManager : INotificationSubscriptionManager, ITransientDependency
    {
        private readonly INotificationStore _store;
        private readonly INotificationDefinitionManager _notificationDefinitionManager;
        private readonly IGuidGenerator _guidGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationSubscriptionManager"/> class.
        /// </summary>
        public NotificationSubscriptionManager(
            INotificationStore store, 
            INotificationDefinitionManager notificationDefinitionManager,
            IGuidGenerator guidGenerator)
        {
            _store = store;
            _notificationDefinitionManager = notificationDefinitionManager;
            _guidGenerator = guidGenerator;
        }

        public async Task SubscribeAsync(UserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            if (await IsSubscribedAsync(user, notificationName, entityIdentifier))
            {
                return;
            }

            await _store.InsertSubscriptionAsync(
                new NotificationSubscriptionInfo(
                    _guidGenerator.Create(),
                    user.TenantId,
                    user.UserId,
                    notificationName,
                    entityIdentifier
                    )
                );
        }

        public void Subscribe(UserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            if (IsSubscribed(user, notificationName, entityIdentifier))
            {
                return;
            }

            _store.InsertSubscription(
                new NotificationSubscriptionInfo(
                    _guidGenerator.Create(),
                    user.TenantId,
                    user.UserId,
                    notificationName,
                    entityIdentifier
                    )
                );
        }

        public async Task SubscribeToAllAvailableNotificationsAsync(UserIdentifier user)
        {
            var notificationDefinitions = (await _notificationDefinitionManager
                .GetAllAvailableAsync(user))
                .Where(nd => nd.EntityType == null)
                .ToList();

            foreach (var notificationDefinition in notificationDefinitions)
            {
                await SubscribeAsync(user, notificationDefinition.Name);
            }
        }

        public void SubscribeToAllAvailableNotifications(UserIdentifier user)
        {
            var notificationDefinitions = (_notificationDefinitionManager
                .GetAllAvailable(user))
                .Where(nd => nd.EntityType == null)
                .ToList();

            foreach (var notificationDefinition in notificationDefinitions)
            {
                Subscribe(user, notificationDefinition.Name);
            }
        }

        public async Task UnsubscribeAsync(UserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            await _store.DeleteSubscriptionAsync(
                user,
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString()
                );
        }

        public void Unsubscribe(UserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null)
        {
             _store.DeleteSubscription(
                user,
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString()
                );
        }

        // TODO: Can work only for single database approach!
        public async Task<List<NotificationSubscription>> GetSubscriptionsAsync(string notificationName, EntityIdentifier entityIdentifier = null)
        {
            var notificationSubscriptionInfos = await _store.GetSubscriptionsAsync(
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString()
                );

            return notificationSubscriptionInfos
                .Select(nsi => nsi.ToNotificationSubscription())
                .ToList();
        }

        // TODO: Can work only for single database approach!
        public List<NotificationSubscription> GetSubscriptions(string notificationName, EntityIdentifier entityIdentifier = null)
        {
            var notificationSubscriptionInfos =  _store.GetSubscriptions(
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString()
                );

            return notificationSubscriptionInfos
                .Select(nsi => nsi.ToNotificationSubscription())
                .ToList();
        }

        public async Task<List<NotificationSubscription>> GetSubscriptionsAsync(Guid? tenantId, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            var notificationSubscriptionInfos = await _store.GetSubscriptionsAsync(
                new[] { tenantId },
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString()
                );

            return notificationSubscriptionInfos
                .Select(nsi => nsi.ToNotificationSubscription())
                .ToList();
        }

        public List<NotificationSubscription> GetSubscriptions(Guid? tenantId, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            var notificationSubscriptionInfos =  _store.GetSubscriptions(
                new[] { tenantId },
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString()
                );

            return notificationSubscriptionInfos
                .Select(nsi => nsi.ToNotificationSubscription())
                .ToList();
        }

        public async Task<List<NotificationSubscription>> GetSubscribedNotificationsAsync(UserIdentifier user)
        {
            var notificationSubscriptionInfos = await _store.GetSubscriptionsAsync(user);

            return notificationSubscriptionInfos
                .Select(nsi => nsi.ToNotificationSubscription())
                .ToList();
        }

        public List<NotificationSubscription> GetSubscribedNotifications(UserIdentifier user)
        {
            var notificationSubscriptionInfos =  _store.GetSubscriptions(user);

            return notificationSubscriptionInfos
                .Select(nsi => nsi.ToNotificationSubscription())
                .ToList();
        }

        public Task<bool> IsSubscribedAsync(UserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            return _store.IsSubscribedAsync(
                user,
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString()
                );
        }

        public bool IsSubscribed(UserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            return _store.IsSubscribed(
                user,
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString()
                );
        }
    }
}