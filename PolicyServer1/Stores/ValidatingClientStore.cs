using Microsoft.Extensions.Logging;
using PolicyServer1.Models;
using PolicyServer1.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Stores {
    public class ValidatingClientStore<T> : IClientStore where T : IClientStore {

        private readonly T _inner;
        private readonly IClientConfigurationValidator _validator;
        private readonly ILogger<ValidatingClientStore<T>> _logger;
        private readonly String _validatorType;

        public ValidatingClientStore(
            T inner,
            IClientConfigurationValidator validator,
            ILogger<ValidatingClientStore<T>> logger
        ) {
            _inner = inner;
            _validator = validator;
            _logger = logger;
            _validatorType = _validator.GetType().FullName;
        }

        public Task<Guid> CreateAsync(Client item) {
            return _inner.CreateAsync(item);
        }

        public Task<Client> GetAsync(Guid id) {
            return _inner.GetAsync(id);
        }

        public IQueryable<Client> Query() {
            return _inner.Query();
        }

        public Task<Client> GetFromClientIdAsync(String clientId) {
            return _inner.GetFromClientIdAsync(clientId);
        }

        public Task RemoveAsync(Guid id) {
            return _inner.RemoveAsync(id);
        }

        public Task RemoveClientIdAsync(String cliendId) {
            return _inner.RemoveClientIdAsync(cliendId);
        }

        public Task UpdateAsync(Guid id, Client item) {
            return _inner.UpdateAsync(id, item);
        }

        //public async Task<Client> GetAsync(Int32 clientId) {
        //    Client client = await _inner.GetAsync(clientId);

        //    if (client != null) {
        //        _logger.LogTrace("Validate client : {validator}", _validatorType);

        //        ConfigurationValidationContext<Client> context = new ConfigurationValidationContext<Client>(client);
        //        await _validator.ValidateAsync(context);

        //        if (context.IsValid) {
        //            _logger.LogDebug("Client configuration validation for {clientId} succeeded", client.ClientId);
        //            return client;
        //        } else {
        //            _logger.LogError("Invalid client configuration for {clientId}: {error}", client.ClientId, context.ErrorMessage);
        //            //await _events.RaiseAsync(new InvalidConfigurationEvent<Client>(client, context.ErrorMessage));

        //            return null;
        //        }

        //    }

        //    return null;
        //}

        //public async Task<Client> GetFromClientIdAsync(String clientId) {
        //    Client client = await _inner.GetFromClientIdAsync(clientId);

        //    if (client != null) {
        //        _logger.LogTrace("Validate client : {validator}", _validatorType);

        //        ConfigurationValidationContext<Client> context = new ConfigurationValidationContext<Client>(client);
        //        await _validator.ValidateAsync(context);

        //        if (context.IsValid) {
        //            _logger.LogDebug("Client configuration validation for {clientId} succeeded", client.ClientId);
        //            return client;
        //        } else {
        //            _logger.LogError("Invalid client configuration for {clientId}: {error}", client.ClientId, context.ErrorMessage);
        //            //await _events.RaiseAsync(new InvalidConfigurationEvent<Client>(client, context.ErrorMessage));

        //            return null;
        //        }

        //    }

        //    return null;
        //}

        //public Task<Int32> CreateAsync(Client newClient) {
        //    return _inner.CreateAsync(newClient);
        //}

        //public Task UpdateAsync(Int32 clientId, Client client) {
        //    return _inner.UpdateAsync(clientId, client);
        //}

        //public Task RemoveAsync(Int32 cliendId) {
        //    return _inner.RemoveAsync(cliendId);
        //}

        //public Task RemoveClientIdAsync(String cliendId) {
        //    return _inner.RemoveClientIdAsync(cliendId);
        //}

    }
}
