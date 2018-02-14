// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Http;
using Polly;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PollyHttpClientBuilderExtensions
    {
        public static IHttpClientBuilder AddPolicyHandler(this IHttpClientBuilder builder, IAsyncPolicy policy)
        {
            //builder.AddHttpMessageHandler(() => new PolicyHttpMessageHandler(createPolicy()));
            return builder;
        }

        public static IHttpClientBuilder AddPolicyHandler(this IHttpClientBuilder builder, IAsyncPolicy<HttpResponseMessage> policy)
        {
            //builder.AddHttpMessageHandler(() => new PolicyHttpMessageHandler(createPolicy()));
            return builder;
        }

        public static IHttpClientBuilder AddExceptionPolicyHandler(this IHttpClientBuilder builder, Func<PolicyBuilder, IAsyncPolicy> createPolicy)
        {
            var policyBuilder = Policy.Handle<HttpRequestException>();
            var policy = createPolicy(policyBuilder);

            //builder.AddHttpMessageHandler(() => new PolicyHttpMessageHandler(createPolicy()));
            return builder;
        }

        public static IHttpClientBuilder AddExceptionPolicyHandler(this IHttpClientBuilder builder, Func<PolicyBuilder, IAsyncPolicy<HttpResponseMessage>> createPolicy)
        {
            //builder.AddHttpMessageHandler(() => new PolicyHttpMessageHandler(createPolicy()));
            return builder;
        }

        public static IHttpClientBuilder AddServerErrorPolicyHandler(
            this IHttpClientBuilder builder, 
            Func<PolicyBuilder<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>> createPolicy)
        {
            var policyBuilder = Policy.Handle<HttpRequestException>().OrResult<HttpResponseMessage>(response =>
            {
                return response.StatusCode >= HttpStatusCode.InternalServerError;
            });
            var policy = createPolicy(policyBuilder);

            //builder.AddHttpMessageHandler(() => new PolicyHttpMessageHandler(createPolicy()));
            return builder;
        }

        public static IHttpClientBuilder AddBadRequestPolicyHandler(
            this IHttpClientBuilder builder,
            Func<PolicyBuilder<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>> createPolicy)
        {
            var policyBuilder = Policy.Handle<HttpRequestException>().OrResult<HttpResponseMessage>(response =>
            {
                return response.StatusCode >= HttpStatusCode.BadRequest;
            });
            var policy = createPolicy(policyBuilder);

            //builder.AddHttpMessageHandler(() => new PolicyHttpMessageHandler(createPolicy()));
            return builder;
        }
    }
}
