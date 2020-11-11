using LinksShare.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;

namespace LinksShare.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings dbSettings)
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(dbSettings.ConnectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            var database = mongoClient.GetDatabase(dbSettings.DatabaseName);
            _books = database.GetCollection<Book>(dbSettings.BooksCollectionName);
        }

        public List<Book> Get() => _books.Find(book => true).ToList();
        public Book Get(string id) => _books.Find<Book>(book => book.Id == id).FirstOrDefault();
        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }
        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);
        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);
        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);
    }
}
