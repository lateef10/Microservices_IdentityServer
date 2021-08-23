using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Config
    {
        //Client is the application that can access the identity
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                   new Client
                   {// standard definition. Won't be using this
                        ClientId = "movieClient",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        AllowedScopes = { "movieAPI" }
                   },
                   new Client
                   {
                       ClientId = "movies_mvc_client",
                       ClientName = "Movies MVC Web App",
                       AllowedGrantTypes = GrantTypes.Hybrid,
                       RequirePkce = false,
                       AllowRememberConsent = false,
                       RedirectUris = new List<string>()
                       {
                           "https://localhost:5002/signin-oidc"
                       },
                       PostLogoutRedirectUris = new List<string>()
                       {
                           "https://localhost:5002/signout-callback-oidc"
                       },
                       ClientSecrets = new List<Secret>
                       {
                           new Secret("secret".Sha256())
                       },
                       AllowedScopes = new List<string>
                       {
                           IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile,
                           IdentityServerConstants.StandardScopes.Address,
                           IdentityServerConstants.StandardScopes.Email,
                           "movieAPI",
                           "roles"
                       }
                   }
            };

        //ApiScopes is the types of resources access that can be granted to the client
        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new ApiScope("movieAPI", "Movie API"),
               new ApiScope(name: "movieapi.read",   displayName: "Read your data"),
               new ApiScope(name: "movieapi.write",  displayName: "Write your data"),
           };

        //ApiResource is what we are trying to protect
        public static IEnumerable<ApiResource> ApiResources =>
          new ApiResource[]
          {
               /*new ApiResource("movieAPI", "Movie API")
               {
                   Scopes = new List<string>{ "movieapi.read", "movieapi.write" },
                   ApiSecrets = new List<Secret>{new Secret("secret".Sha256())},
                   UserClaims = new List<string>{"role"}
               }*/
          };

        public static IEnumerable<IdentityResource> IdentityResources =>
          new IdentityResource[]
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResources.Email(),
              new IdentityResource(
                    "roles",
                    "Your role(s)",
                    new List<string>() { "role" })
          };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "test",
                    Password = "test",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "testName"),
                        new Claim(JwtClaimTypes.FamilyName, "testFamilyName")
                    }
                }
            };
    }
}
