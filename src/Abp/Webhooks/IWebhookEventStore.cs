﻿using System;
using System.Threading.Tasks;

namespace Abp.Webhooks
{
    public interface IWebhookEventStore
    {
        /// <summary>
        /// Inserts to persistent store
        /// </summary>
        Task<Guid> InsertAndGetIdAsync(WebhookEvent webhookEvent);

        /// <summary>
        /// Inserts to persistent store
        /// </summary>
        Guid InsertAndGetId(WebhookEvent webhookEvent);

        /// <summary>
        /// Gets Webhook info by id
        /// </summary>
        Task<WebhookEvent> GetAsync(Guid? tenantId, Guid id);

        /// <summary>
        /// Gets Webhook info by id
        /// </summary>
        WebhookEvent Get(Guid? tenantId, Guid id);
    }
}
