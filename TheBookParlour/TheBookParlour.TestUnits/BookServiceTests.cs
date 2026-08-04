using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TheBookParlour.Core.Services;
using TheBookParlour.Data.Entities;
using TheBookParlour.Data.Interfaces;

namespace TheBookParlour.TestUnits
{
    public class BookServiceTests
    {
        [Fact]
        public async Task AddBookAsync_ShouldSetCreatedAt_WhenBookIsAdded()
        {
            // Arrange – skapa en fejkad repo
            var mockRepo = new Mock<IBookRepo>();
            var mockLogger = new Mock<ILogger<BookService>>();

            // Säg åt mocken vad den ska returnera
            var book = new Book { BookId = 1, Title = "Harry Potter", Price =25};
            mockRepo.Setup(r => r.AddBookAsync(book)).ReturnsAsync(book);

            // Skapa service med fejkad repo
            var service = new BookService(mockRepo.Object, mockLogger.Object);

            // Act
            var result = await service.AddBookAsync(book);

            // Assert
            Assert.NotEqual(default, result.CreatedAt); // ← CreatedAt ska inte vara tomt
        }

        [Fact]
        public async Task DeleteBookAsync_ShouldReturnFalse_WhenBookDoesNotExist()
        {
            // Arrange – skapa fejkade beroenden
            var mockRepo = new Mock<IBookRepo>();
            var mockLogger = new Mock<ILogger<BookService>>();

            // Repot hittar inte boken – returnerar false
            mockRepo.Setup(r => r.DeleteBookAsync(1)).ReturnsAsync(false);

            // Skapa service med fejkad repo
            var service = new BookService(mockRepo.Object, mockLogger.Object);

            // Act – försök radera en bok som inte finns
            var result = await service.DeleteBookAsync(1);

            // Assert – servicen ska returnera false
            Assert.False(result);
        }
    }
}
