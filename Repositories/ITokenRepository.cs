using System.Runtime.InteropServices;
using Models.Domains;
using Microsoft.AspNetCore.Identity;

namespace Repositories;
public interface ITokenRepository
{
    string CreateJWTToken(IdentityUser identityUser, List<string> roles);
}