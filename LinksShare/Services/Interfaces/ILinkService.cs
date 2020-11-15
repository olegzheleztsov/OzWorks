using LinksShare.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinksShare.Services.Interfaces
{
    public interface ILinkService
    {
        Task<UserLinks> GetUserLinksAsync(string userName);
        Task<bool> AddLinkToUserAsync(string userName, LinkInfo linkInfo);

        Task<bool> DeleteUserLink(string userName, int linkId);

        Task<IEnumerable<UserLinks>> GetAllUserLinksAsync();

        Task<bool> UpdateUserLinkAsync(string userName, LinkInfo updatedLink);
    }
}
