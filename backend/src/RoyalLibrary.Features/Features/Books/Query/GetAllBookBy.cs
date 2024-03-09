using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

using MediatR;
using Newtonsoft.Json.Converters;

using RoaylLibrary.Domain;
using RoaylLibrary.Data.Cache;
using RoaylLibrary.Data.DbContext.Client;


namespace RoyalLibrary.Features.Features.Books.Query
{
    
    public record GetBookByFilterRequest
    {
        public int Page { get; set; }

        public int PageSize { get; set; } = 20;

        [JsonConverter(typeof(StringEnumConverter))]
        public SearchByType SearchBy { get; set; }

        public string? SearchValue { get; set; }

        public bool IsValid()
        {
            return  !string.IsNullOrEmpty(SearchValue);
        }
    }

    public enum SearchByType
    {
        Authors = 0,
        Isbn = 1,
        Title = 2
    }

    public record GetAllBookResponse
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<GetAllBookItemResponse> Items { get; set; } = [];
    }

    public record GetAllBookItemResponse
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Publish { get; set; }
        public required string Authors { get; set; }
        public required string BookType { get; set; }
        public required string Isbn { get; set; }
        public required string Category { get; set; }
        public required string AvailiableCopies { get; set; } 
    }

    public class GetAllBookByQuery : IRequest<GetAllBookResponse>
    {
       
        public required GetBookByFilterRequest FilterRequest;
    }

    public class GetAllBookByHandler : IRequestHandler<GetAllBookByQuery, GetAllBookResponse>
    {
        private readonly RoyalLibraryDbContext Context;
        private readonly IDataCacheService DataCacheService;

        public GetAllBookByHandler(RoyalLibraryDbContext context, IDataCacheService dataCacheService)
        {
            Context = context;
            DataCacheService = dataCacheService;
        }
        public async Task<GetAllBookResponse> Handle(GetAllBookByQuery request, CancellationToken cancellationToken)
        {
            var objectKey = CreateKey(request);

            var bookCacheResponse = await DataCacheService.GetAsync<GetAllBookResponse>(objectKey,cancellationToken);
            if (bookCacheResponse != null)
            {
                return bookCacheResponse;
            }

            if (!request.FilterRequest.IsValid())
            {

                 var books = await Context.Books
                                .Skip((request.FilterRequest.Page - 1) * request.FilterRequest.PageSize)
                                .Take(request.FilterRequest.PageSize)
                                .ToListAsync(cancellationToken);

                var bookResponse = MapTo(books);

                await DataCacheService.SetAsync(CreateKey(request), bookResponse, new DataCacheExpirationOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
                }   ,cancellationToken);

                return bookResponse;
            }

            var query = default(IQueryable<Book>);

            query = request.FilterRequest.SearchBy switch
            {
                SearchByType.Authors => Context.Books.Where(x => x.Author.Contains(request.FilterRequest.SearchValue)),
                SearchByType.Isbn => Context.Books.Where(x => x.Isbn.Contains(request.FilterRequest.SearchValue)),
                SearchByType.Title => Context.Books.Where(x => x.Title.Contains(request.FilterRequest.SearchValue)),
                _ => Context.Books,
            };

            query.Skip((request.FilterRequest.Page - 1) * request.FilterRequest.PageSize)
                 .Take(request.FilterRequest.PageSize);

            var bookQueryResponse = await query.ToListAsync(cancellationToken);
            var bookAllQueryResponse = MapTo(bookQueryResponse);

            await DataCacheService.SetAsync(CreateKey(request), bookQueryResponse, new DataCacheExpirationOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
            }, cancellationToken);

            return bookAllQueryResponse;
        }

        private static string CreateKey(GetAllBookByQuery query)
        {
            var serialized = JsonSerializer.Serialize(query);
            return CreateMD5Hash(serialized);
        }

        private static string CreateMD5Hash(string input)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        private static GetAllBookResponse MapTo(List<Book> books)
        {
           return new GetAllBookResponse()
            {
                PageSize = books.Count,
                Items = books.Select(book => new GetAllBookItemResponse
                {
                    Id = book.Id,
                    Title = book.Title,
                    Authors = book.Author,
                    AvailiableCopies = $"{book.TotalCopies} / {book.CopiesInUse}",
                    Category = book.Category,
                    BookType = book.BookType,
                    Isbn = book.Isbn,
                    Publish = book.Publisher,
                }).ToList(),
            };
        }
    }
}
