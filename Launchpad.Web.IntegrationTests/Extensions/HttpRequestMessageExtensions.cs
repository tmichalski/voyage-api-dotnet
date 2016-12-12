﻿using System.Net.Http;
using System.Net.Http.Formatting;

namespace Launchpad.Web.IntegrationTests.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage WithJson<TModel>(this HttpRequestMessage requestMessage, TModel model)
        {
            requestMessage.Content = new ObjectContent<TModel>(model, new JsonMediaTypeFormatter());
            return requestMessage;
        }
    }
}
