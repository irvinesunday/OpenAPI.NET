﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Exceptions;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Properties;

namespace Microsoft.OpenApi.Models
{
    /// <summary>
    /// Extension methods for Open API elements.
    /// </summary>
    public static class OpenApiElementExtensions
    {
        /// <summary>
        /// Add a <see cref="OpenApiPathItem"/> into the <see cref="OpenApiDocument"/>.
        /// </summary>
        /// <param name="document">The Open API document.</param>
        /// <param name="name">The path item name.</param>
        /// <param name="configure">The path item configuration action.</param>
        public static void AddPathItem(this OpenApiDocument document, string name, Action<OpenApiPathItem> configure)
        {
            if (document == null)
            {
                throw Error.ArgumentNull(nameof(document));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrWhiteSpace(nameof(name));
            }

            if (configure == null)
            {
                throw Error.ArgumentNull(nameof(configure));
            }

            var pathItem = new OpenApiPathItem();
            configure(pathItem);

            if (document.Paths == null)
            {
                document.Paths = new OpenApiPaths();
            }

            document.Paths.Add(name, pathItem);
        }

        /// <summary>
        /// Add a <see cref="OpenApiOperation"/> into the <see cref="OpenApiPathItem"/>.
        /// </summary>
        /// <param name="pathItem">The Open API path item.</param>
        /// <param name="operationType">The operation type kind.</param>
        /// <param name="configure">The operation configuration action.</param>
        public static void AddOperation(
            this OpenApiPathItem pathItem,
            OperationType operationType,
            Action<OpenApiOperation> configure)
        {
            if (pathItem == null)
            {
                throw Error.ArgumentNull(nameof(pathItem));
            }

            if (configure == null)
            {
                throw Error.ArgumentNull(nameof(configure));
            }

            var operation = new OpenApiOperation();

            configure(operation);

            pathItem.AddOperation(operationType, operation);
        }

        /// <summary>
        /// Add a <see cref="OpenApiHeader"/> into the response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="name">The header name.</param>
        /// <param name="configure">The header configure action.</param>
        public static void AddHeader(this OpenApiResponse response, string name, Action<OpenApiHeader> configure)
        {
            if (response == null)
            {
                throw Error.ArgumentNull(nameof(response));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrWhiteSpace(nameof(name));
            }

            if (configure == null)
            {
                throw Error.ArgumentNull(nameof(configure));
            }

            var header = new OpenApiHeader();
            configure(header);

            if (response.Headers == null)
            {
                response.Headers = new Dictionary<string, OpenApiHeader>();
            }

            response.Headers.Add(name, header);
        }

        /// <summary>
        /// Add a <see cref="OpenApiMediaType"/> into the response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="name">The content name.</param>
        /// <param name="configure">The content configure action.</param>
        public static void AddMediaType(this OpenApiResponse response, string name, Action<OpenApiMediaType> configure)
        {
            if (response == null)
            {
                throw Error.ArgumentNull(nameof(response));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrWhiteSpace(nameof(name));
            }

            if (configure == null)
            {
                throw Error.ArgumentNull(nameof(configure));
            }

            var mediaType = new OpenApiMediaType();
            configure(mediaType);

            if (response.Content == null)
            {
                response.Content = new Dictionary<string, OpenApiMediaType>();
            }

            response.Content.Add(name, mediaType);
        }

        /// <summary>
        /// Add a <see cref="OpenApiLink"/> into the response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="name">The link name.</param>
        /// <param name="configure">The link configure action.</param>
        public static void AddLink(this OpenApiResponse response, string name, Action<OpenApiLink> configure)
        {
            if (response == null)
            {
                throw Error.ArgumentNull(nameof(response));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrWhiteSpace(nameof(name));
            }

            if (configure == null)
            {
                throw Error.ArgumentNull(nameof(configure));
            }

            var link = new OpenApiLink();
            configure(link);

            if (response.Links == null)
            {
                response.Links = new Dictionary<string, OpenApiLink>();
            }

            response.Links.Add(name, link);
        }

        /// <summary>
        /// Add a <see cref="OpenApiResponse"/> into the <see cref="OpenApiOperation"/>.
        /// </summary>
        /// <param name="operation">The Open API operation.</param>
        /// <param name="name">The response name.</param>
        /// <param name="configure">The response configuration action.</param>
        public static void AddResponse(this OpenApiOperation operation, string name, Action<OpenApiResponse> configure)
        {
            if (operation == null)
            {
                throw Error.ArgumentNull(nameof(operation));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrWhiteSpace(nameof(name));
            }

            if (configure == null)
            {
                throw Error.ArgumentNull(nameof(configure));
            }

            var response = new OpenApiResponse();
            configure(response);

            if (operation.Responses == null)
            {
                operation.Responses = new OpenApiResponses();
            }

            operation.Responses.Add(name, response);
        }

        /// <summary>
        /// Add a <see cref="OpenApiSecurityScheme"/> into the <see cref="OpenApiSecurityRequirement"/>.
        /// </summary>
        /// <param name="securityRequirement">The security requirement to add the security scheme to.</param>
        /// <param name="securityScheme">The security scheme to add as key.</param>
        /// <param name="scopes">The scopes array to be used as the value.</param>
        public static void AddSecurityScheme(
            this OpenApiSecurityRequirement securityRequirement,
            OpenApiSecurityScheme securityScheme,
            List<string> scopes)
        {
            if (securityRequirement == null)
            {
                throw Error.ArgumentNull(nameof(securityRequirement));
            }

            if (securityScheme == null)
            {
                throw Error.ArgumentNull(nameof(securityScheme));
            }

            if (scopes == null)
            {
                throw Error.ArgumentNull(nameof(scopes));
            }

            if (securityRequirement.Schemes == null)
            {
                securityRequirement.Schemes = new OpenApiSecuritySchemeDictionary();
            }

            securityRequirement.Schemes.Add(securityScheme, scopes);
        }

        /// <summary>
        /// Try find the referenced element.
        /// </summary>
        /// <typeparam name="T"><see cref="IOpenApiReferenceable"/>.</typeparam>
        /// <param name="reference">The reference element. </param>
        /// <param name="document">The Open API document.</param>
        public static IOpenApiElement Find<T>(this T reference, OpenApiDocument document)
            where T : IOpenApiReferenceable
        {
            if (reference == null ||
                document == null ||
                document.Components == null ||
                reference.Reference == null ||
                reference.Reference.ExternalResource != null)
            {
                return null;
            }

            switch (reference.Reference.Type)
            {
                case ReferenceType.Schema:
                    return document.Components.Schemas?[reference.Reference.Id];

                case ReferenceType.Parameter:
                    return document.Components.Parameters?[reference.Reference.Id];

                case ReferenceType.Header:
                    return document.Components.Headers?[reference.Reference.Id];

                case ReferenceType.Response:
                    return document.Components.Responses?[reference.Reference.Id];

                case ReferenceType.RequestBody:
                    return document.Components.RequestBodies?[reference.Reference.Id];

                case ReferenceType.Example:
                    return document.Components.Examples?[reference.Reference.Id];

                case ReferenceType.SecurityScheme:
                    return document.Components.SecuritySchemes?[reference.Reference.Id];

                case ReferenceType.Callback:
                    return document.Components.Callbacks?[reference.Reference.Id];

                case ReferenceType.Link:
                    return document.Components.Links?[reference.Reference.Id];

                case ReferenceType.Tag:
                    return document?.Tags.FirstOrDefault(e => e.Name == reference.Reference.Id);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Check whether the element is reference element.
        /// </summary>
        /// <typeparam name="T"><see cref="IOpenApiElement"/>.</typeparam>
        /// <param name="element">The referencable element.</param>
        /// <returns>
        /// True if the element implements <see cref="IOpenApiReferenceable"/> and pointer is not null,
        /// False otherwise.
        /// </returns>
        public static bool IsReference<T>(this T element) where T : IOpenApiElement
        {
            var reference = element as IOpenApiReferenceable;
            return reference?.Reference != null;
        }

        /// <summary>
        /// Add extension into the Extensions
        /// </summary>
        /// <typeparam name="T"><see cref="IOpenApiExtensible"/>.</typeparam>
        /// <param name="element">The extensible Open API element. </param>
        /// <param name="name">The extension name.</param>
        /// <param name="any">The extension value.</param>
        public static void AddExtension<T>(this T element, string name, IOpenApiAny any)
            where T : IOpenApiExtensible
        {
            if (element == null)
            {
                throw Error.ArgumentNull(nameof(element));
            }

            VerifyExtensionName(name);

            if (element.Extensions == null)
            {
                element.Extensions = new Dictionary<string, IOpenApiAny>();
            }

            element.Extensions[name] = any ?? throw Error.ArgumentNull(nameof(any));
        }

        private static void VerifyExtensionName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrWhiteSpace(nameof(name));
            }

            if (!name.StartsWith(OpenApiConstants.ExtensionFieldNamePrefix))
            {
                throw new OpenApiException(string.Format(SRResource.ExtensionFieldNameMustBeginWithXDash, name));
            }
        }
    }
}