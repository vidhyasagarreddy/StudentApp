using Xunit;
using MockAPI.Controllers;
using MockAPI.Models;
using MockAPI; // For ApplicationDbContext
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; // Required for OkObjectResult, NotFoundResult etc.
using System.Collections.Generic; // Required for IEnumerable

namespace MockAPI.Tests
{
    public class ProductsControllerTests
    {
        private DbContextOptions<ApplicationDbContext> GetInMemoryDbContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString()) // Unique name for each test run
                .Options;
        }

        [Fact]
        public async Task GetProducts_ReturnsEmptyList_WhenDbIsEmpty()
        {
            // Arrange
            var options = GetInMemoryDbContextOptions();
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new ProductsController(context);

                // Act
                var result = await controller.GetProducts();

                // Assert
                var actionResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
                // For OkObjectResult, the value is directly the content.
                // If the controller returns Ok(value), then it's OkObjectResult.
                // If it returns ActionResult<T>.Value, then it's directly T.
                // Our controller returns await _context.Products.ToListAsync(); which is List<Product>
                // and ASP.NET Core wraps it in ActionResult<IEnumerable<Product>>.
                // The actual value is accessed via .Value property of ActionResult.
                Assert.Empty(actionResult.Value);
            }
        }

        [Fact]
        public async Task GetProducts_ReturnsAllProducts_WhenDbHasData()
        {
            // Arrange
            var options = GetInMemoryDbContextOptions();
            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(new Product { Id = 1, Name = "Product 1", Price = 10 });
                context.Products.Add(new Product { Id = 2, Name = "Product 2", Price = 20 });
                await context.SaveChangesAsync();

                var controller = new ProductsController(context);

                // Act
                var result = await controller.GetProducts();

                // Assert
                var actionResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
                Assert.Equal(2, actionResult.Value.Count());
            }
        }

        [Fact]
        public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var options = GetInMemoryDbContextOptions();
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new ProductsController(context);

                // Act
                var result = await controller.GetProduct(999);

                // Assert
                var actionResult = Assert.IsType<ActionResult<Product>>(result);
                Assert.IsType<NotFoundResult>(actionResult.Result);
            }
        }

        [Fact]
        public async Task GetProduct_ReturnsProduct_WhenProductExists()
        {
            // Arrange
            var options = GetInMemoryDbContextOptions();
            var testProduct = new Product { Id = 1, Name = "Test Product", Price = 15.99m, Description = "Test Desc" };
            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(testProduct);
                await context.SaveChangesAsync();

                var controller = new ProductsController(context);

                // Act
                var result = await controller.GetProduct(testProduct.Id);

                // Assert
                var actionResult = Assert.IsType<ActionResult<Product>>(result);
                var actualProduct = Assert.IsType<Product>(actionResult.Value);
                Assert.Equal(testProduct.Id, actualProduct.Id);
                Assert.Equal(testProduct.Name, actualProduct.Name);
                Assert.Equal(testProduct.Price, actualProduct.Price);
                Assert.Equal(testProduct.Description, actualProduct.Description);
            }
        }
    }
}
