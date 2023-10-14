﻿// Copyright (c) .NET Core Community. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using DotNetCore.CAP;
using DotNetCore.CAP.Transport;

using Microsoft.Extensions.Options;

namespace Template.RabbitMQ
{
    internal sealed class RabbitMQConsumerClientFactory : IConsumerClientFactory
    {
        private readonly IConnectionChannelPool _connectionChannelPool;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<RabbitMQOptions> _rabbitMQOptions;

        public RabbitMQConsumerClientFactory(IOptions<RabbitMQOptions> rabbitMQOptions, IConnectionChannelPool channelPool, IServiceProvider serviceProvider)
        {
            _rabbitMQOptions = rabbitMQOptions;
            _connectionChannelPool = channelPool;
            _serviceProvider = serviceProvider;
        }

        public IConsumerClient Create(string groupId)
        {
            try
            {
                var client = new RabbitMQConsumerClient(groupId, _connectionChannelPool, _rabbitMQOptions, _serviceProvider);
                client.Connect();
                return client;
            }
            catch (Exception e)
            {
                throw new BrokerConnectionException(e);
            }
        }
    }
}