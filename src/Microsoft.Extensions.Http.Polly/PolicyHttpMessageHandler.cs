// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Polly;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Http
{
    public class PolicyHttpMessageHandler : DelegatingHandler
    {
        private readonly IAsyncPolicy<HttpResponseMessage> _policy;

        public PolicyHttpMessageHandler(IAsyncPolicy<HttpResponseMessage> policy)
        {
            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }

            _policy = policy;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return _policy.ExecuteAsync(() => base.SendAsync(request, cancellationToken));
        }
    }
}
