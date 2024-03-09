USE [app]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book]
(
  [Id] [uniqueidentifier] NOT NULL,
  [Title] [varchar](500) NOT NULL,
  [Author] [varchar](200) NOT NULL,
  [TotalCopies] [int] NOT NULL,
  [CopiesInUse] [int] NOT NULL,
  [BookType] [varchar](50) NOT NULL,
  [Isbn] [varchar](100) NOT NULL,
  [Category] [varchar](150) NOT NULL,
  [Publisher] [varchar](200) NOT NULL,
  CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Book]
  ([Id], [Title], [Author], [TotalCopies], [CopiesInUse], [BookType], [Isbn], [Category], [Publisher])
VALUES
  (N'41cfcd3c-aad1-444e-933d-036a5165c63a', N'The Hobbit', N'J.R.R. Tolkien', 45, 6, N'Print', N'ISBN456789', N'Fantasy', N'Publisher H')
GO
INSERT [dbo].[Book]
  ([Id], [Title], [Author], [TotalCopies], [CopiesInUse], [BookType], [Isbn], [Category], [Publisher])
VALUES
  (N'dd54c089-d034-4d48-9005-694911950a72', N'Brave New World', N'Aldous Huxley', 20, 1, N'Print', N'ISBN567890 ', N'Dystopian', N'Publisher E')
GO
INSERT [dbo].[Book]
  ([Id], [Title], [Author], [TotalCopies], [CopiesInUse], [BookType], [Isbn], [Category], [Publisher])
VALUES
  (N'c65b760e-c05d-4c88-807b-71f3887877bd', N'The Great Gatsby', N'F. Scott Fitzgerald ', 25, 3, N'Print ', N'ISBN987654', N'Classic', N'Publisher D')
GO
INSERT [dbo].[Book]
  ([Id], [Title], [Author], [TotalCopies], [CopiesInUse], [BookType], [Isbn], [Category], [Publisher])
VALUES
  (N'8d3f1a04-03e2-4d09-816e-9bd192dcf41d', N'Harry Potter and the Sorcerer''s Stone', N'J.K. Rowling', 70, 12, N'Print ', N'ISBN678901', N'Fantasy', N'Publisher J')
GO
INSERT [dbo].[Book]
  ([Id], [Title], [Author], [TotalCopies], [CopiesInUse], [BookType], [Isbn], [Category], [Publisher])
VALUES
  (N'fc9d8739-428e-42a5-b009-a6d5dbcf5f9c', N'The Lord of the Rings', N'J.R.R. Tolkien', 60, 10, N'Print', N'ISBN345678', N'Fantasy', N'Publisher G')
GO
INSERT [dbo].[Book]
  ([Id], [Title], [Author], [TotalCopies], [CopiesInUse], [BookType], [Isbn], [Category], [Publisher])
VALUES
  (N'195e1148-ba36-4589-9596-a8848cc22b6d', N'To Kill a Mockingbird', N'Harper Lee', 50, 5, N'Print', N'ISBN123456', N'Fiction', N'Publisher A')
GO
INSERT [dbo].[Book]
  ([Id], [Title], [Author], [TotalCopies], [CopiesInUse], [BookType], [Isbn], [Category], [Publisher])
VALUES
  (N'26fb5987-a69b-4e6b-b509-da848a622145', N'Pride and Prejudice', N'Jane Austen', 40, 8, N'Print', N'ISBN345678', N'Romance', N'Publisher C')
GO
INSERT [dbo].[Book]
  ([Id], [Title], [Author], [TotalCopies], [CopiesInUse], [BookType], [Isbn], [Category], [Publisher])
VALUES
  (N'6866422b-e3db-4535-8b31-dda447d11d7c', N'The Hunger Games', N'Suzanne Collins', 55, 7, N'Print', N'ISBN567890', N'Dystopian', N'Publisher I')
GO
INSERT [dbo].[Book]
  ([Id], [Title], [Author], [TotalCopies], [CopiesInUse], [BookType], [Isbn], [Category], [Publisher])
VALUES
  (N'd83a1536-b18a-46fd-a0ef-e055f9a69b47', N'The Catcher in the Rye', N'J.D. Salinger', 35, 4, N'Print', N'ISBN234567', N'Coming of Age', N'Publisher F ')
GO
INSERT [dbo].[Book]
  ([Id], [Title], [Author], [TotalCopies], [CopiesInUse], [BookType], [Isbn], [Category], [Publisher])
VALUES
  (N'5c057449-4d03-43c5-bc86-e4e27e537678', N'1984', N'George Orwell', 30, 2, N'Print', N'ISBN789012', N'Science Fiction', N'Publisher B')
GO
