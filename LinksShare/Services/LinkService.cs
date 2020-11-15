using LinksShare.Models;
using LinksShare.Services.Interfaces;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace LinksShare.Services
{
    public class LinkService : ILinkService
    {
        private readonly ILogger<ILinkService> _logger;
        private readonly IMongoCollection<UserLinks> _userLinksCollection;

        public LinkService(ILinksDatabaseSettings dbSettings, ILogger<ILinkService> logger)
        {
            _logger = logger;
            var settings = MongoClientSettings.FromUrl(new MongoUrl(dbSettings.ConnectionString));
            settings.SslSettings = new SslSettings
            {
                EnabledSslProtocols = SslProtocols.Tls12,
              
            };
            var mongoClient = new MongoClient(settings);
            
            var database = mongoClient.GetDatabase(dbSettings.DatabaseName);
            _userLinksCollection = database.GetCollection<UserLinks>(dbSettings.LinksCollectionName);
        }

        public async Task<bool> AddLinkToUserAsync(string userName, LinkInfo linkInfo)
        {
            var userLinks = await GetUserLinksAsync(userName).ConfigureAwait(false);
            bool isNewUser = false;
            if(userLinks == null)
            {
                userLinks = new UserLinks
                {
                    UserName = userName,
                    Links = new List<LinkInfo>()
                };
                isNewUser = true;
            }
            var nextLinkId = GetNextLinkId(userLinks.Links);
            linkInfo.LinkId = nextLinkId;
            userLinks.Links.Add(linkInfo);

            if(isNewUser)
            {
                await AddNewUserLinksAsync(userLinks);
                return true;
            } else
            {
                var update = Builders<UserLinks>.Update.Set(uLinks => uLinks.Links, userLinks.Links);
                var result = await _userLinksCollection.UpdateOneAsync((userLinks) => userLinks.UserName == userName, update).ConfigureAwait(false);
                return result.ModifiedCount > 0;
            }
        }

        private int GetNextLinkId(List<LinkInfo> links)
        {
            if(links == null || links.Count == 0)
            {
                return 1;
            }
            return links.Max(link => link.LinkId) + 1;
        }

        private async Task AddNewUserLinksAsync(UserLinks userLinks)
        {
            await _userLinksCollection.InsertOneAsync(userLinks).ConfigureAwait(false);
        }


        public async Task<bool> DeleteUserLink(string userName, int linkId)
        {
            var userLinks = await GetUserLinksAsync(userName).ConfigureAwait(false);
            if(userLinks == null)
            {
                return false;
            }
            userLinks.Links ??= new List<LinkInfo>();
            if(userLinks.Links == null)
            {
                return false;
            }
            var linkToDelete = userLinks.Links.FirstOrDefault(link => link.LinkId == linkId);
            if(linkToDelete == null)
            {
                return false;
            }
            userLinks.Links.Remove(linkToDelete);
            var update = Builders<UserLinks>.Update.Set(uLinks => uLinks.Links, userLinks.Links);
            var result = await _userLinksCollection.UpdateOneAsync((userLinks) => userLinks.UserName == userName, update).ConfigureAwait(false);
            return result.ModifiedCount > 0;
        }

        public async Task<IEnumerable<UserLinks>> GetAllUserLinksAsync()
        {
            var results = await _userLinksCollection.FindAsync(uLinks => true).ConfigureAwait(false);
            return await results.ToListAsync().ConfigureAwait(false);
        }

        public async Task<UserLinks> GetUserLinksAsync(string userName)
        {
            var results = await _userLinksCollection.FindAsync(uLinks => uLinks.UserName == userName).ConfigureAwait(false);
            return await results.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<bool> UpdateUserLinkAsync(string userName, LinkInfo updatedLink)
        {
            var userLinks = await _userLinksCollection.FindAsync(uLinks => uLinks.UserName == userName).ConfigureAwait(false);
            if(userLinks == null)
            {
                return false;
            }
            var userLink = await userLinks.FirstOrDefaultAsync().ConfigureAwait(false);
            if(userLink == null)
            {
                return false;
            }
            var targetLink = userLink.Links?.FirstOrDefault(uLink => uLink.LinkId == updatedLink.LinkId);
            if(targetLink == null)
            {
                return false;
            }
            targetLink.Link = updatedLink.Link;
            targetLink.Description = updatedLink.Description;
            var update = Builders<UserLinks>.Update.Set(uLinks => uLinks.Links, userLink.Links);
            var result = await _userLinksCollection.UpdateOneAsync((userLinks) => userLinks.UserName == userName, update).ConfigureAwait(false);
            return result.ModifiedCount > 0;
        }
    }
}
