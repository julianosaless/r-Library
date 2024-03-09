using System.Text;
using System.Text.Json;

using MediatR;
using Microsoft.EntityFrameworkCore;

using RoaylLibrary.Domain;
using RoaylLibrary.Data.Cache;
using RoaylLibrary.Data.DbContext.Client;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace RoyalLibrary.Features.Features.Books.Query
{
    
    public record GetBookByFilterRequest
    {
        public int Page { get; set; }

        public int PageSize { get; set; } = 20;

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
        public required string EbookType { get; set; }
        public required string Isbn { get; set; }
        public required string Category { get; set; }
        public required string AvailiableCopies { get; set; } 
    }

    public class GetAllBookByQuery : IRequest<GetAllBookResponse>
    {
       
        public required GetBookByFilterRequest FilterRequest;
    }

    public class GetAllBookByHandler(RoyalLibraryDbContext context, IDataCacheService dataCacheService) : IRequestHandler<GetAllBookByQuery, GetAllBookResponse>
    {
        public async Task<GetAllBookResponse> Handle(GetAllBookByQuery request, CancellationToken cancellationToken)
        {
            var objectKey = createKey(request);

            var bookCacheResponse = await dataCacheService.GetAsync<GetAllBookResponse>(objectKey,cancellationToken); ;
            if (bookCacheResponse != null)
            {
                return bookCacheResponse;
            }

            if (!request.FilterRequest.IsValid())
            {

                 var books = await context.Books
                                .Skip((request.FilterRequest.Page - 1) * request.FilterRequest.PageSize)
                                .Take(request.FilterRequest.PageSize)
                                .ToListAsync(cancellationToken);

                await dataCacheService.SetAsync(createKey(request), MapTo(books),new DataCacheExpirationOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
                }   ,cancellationToken);
            }

            var query =  context.Books
                               .Skip((request.FilterRequest.Page - 1) * request.FilterRequest.PageSize)
                               .Take(request.FilterRequest.PageSize);

            switch (request.FilterRequest.SearchBy)
            {
                case SearchByType.Authors:
                    query.Where(x => x.Author.Contains(request.FilterRequest.SearchValue));
                    break;
                case SearchByType.Isbn:
                    query.Where(x => x.Isbn.Contains(request.FilterRequest.SearchValue));
                    break;
                case SearchByType.Title:
                    query.Where(x => x.Title.Contains(request.FilterRequest.SearchValue));
                    break;
            }

            var bookQueryResponse = await query.ToListAsync(cancellationToken);
            var bookResponse = MapTo(bookQueryResponse);

            await dataCacheService.SetAsync(createKey(request), bookResponse, new DataCacheExpirationOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
            }, cancellationToken);

            return bookResponse;
        }

        private static string createKey(GetAllBookByQuery query)
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
                    Title = book.Title,
                    Authors = book.Author,
                    AvailiableCopies = $"{book.TotalCopies} / {book.CopiesInUse}",
                    Category = book.Category,
                    EbookType = book.EbookType,
                    Isbn = book.Isbn,
                    Publish = book.Publisher,
                }).ToList(),
            };
        }
    }
}
