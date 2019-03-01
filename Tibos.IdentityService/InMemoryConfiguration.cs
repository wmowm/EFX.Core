using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tibos.IdentityService
{
    /// <summary>
    /// One In-Memory Configuration for IdentityServer => Just for Demo Use
    /// </summary>
    public class InMemoryConfiguration
    {
        public static IConfiguration Configuration { get; set; }
        /// <summary>
        /// Define which APIs will use this IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("clientservice", "CAS Client Service"){ApiSecrets = { new Secret("clientservice".Sha256()) }},
                new ApiResource("productservice", "CAS Product Service"){ApiSecrets = { new Secret("clientservice".Sha256()),new Secret("productservice".Sha256()) }},
                new ApiResource("agentservice", "CAS Agent Service"){ApiSecrets = { new Secret("clientservice".Sha256()),new Secret("productservice".Sha256()),new Secret("agentservice".Sha256()) }}
            };
        }

        /// <summary>
        /// Define which Apps will use thie IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "client.api.service",
                    ClientSecrets = new [] { new Secret("clientsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    //Jwt = 0；Reference = 1支持撤销；
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedScopes = new [] { "clientservice" }
                },
                new Client
                {
                    ClientId = "product.api.service",
                    ClientSecrets = new [] { new Secret("productsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    //Jwt = 0；Reference = 1支持撤销；
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedScopes = new [] { "clientservice", "productservice" }
                },
                new Client
                {
                    ClientId = "agent.api.service",
                    ClientSecrets = new [] { new Secret("agentsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    //Jwt = 0；Reference = 1支持撤销；
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedScopes = new [] { "agentservice", "clientservice", "productservice" }
                },
                 new Client //应用程序
                {
                    
                    ClientId = "cas.mvc.client.implicit",
                    ClientName = "CAS MVC Web App Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { $"http://localhost:6011/signin-oidc","http://localhost:6012/signin-oidc","http://localhost:6013/signin-oidc"},
                    PostLogoutRedirectUris = { $"http://localhost:6011/signin-oidc/signout-callback-oidc","http://localhost:6012/signin-oidc/signout-callback-oidc","http://localhost:6013/signin-oidc/signout-callback-oidc" },
                    AllowedScopes = new [] {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "agentservice", "clientservice", "productservice"
                    },
                    AllowAccessTokensViaBrowser = true // can return access_token to this client
                },
                 // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =           { "http://localhost:6011/js/callback.html","http://localhost:6012/js/callback.html","http://localhost:6013/js/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:6011/js/index.html","http://localhost:6012/js/index.html","http://localhost:6013/js/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:6011","http://localhost:6012","http://localhost:6013" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "agentservice"
                    }
                }
        };
    }
    

        /// <summary>
        /// Define which uses will use this IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TestUser> GetUsers()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "10001",
                    Username = "edison@hotmail.com",
                    Password = "edisonpassword"
                },
                new TestUser
                {
                    SubjectId = "10002",
                    Username = "andy@hotmail.com",
                    Password = "andypassword"
                },
                new TestUser
                {
                    SubjectId = "10003",
                    Username = "tibos@qq.com",
                    Password = "123456"
                }
            };
        }


        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
    }
}
