using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Profile;
using ECommerce.Api.Products.Providers;

namespace ECommerce.Api.Products.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productsProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productsProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductsAsync();
            Assert.True(product.IsSuccess);
            Assert.True(product.Products.Any());
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnsProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingValidId))
                .Options;

            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productsProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productsProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync(1);
            Assert.True(product.IsSuccess);
            Assert.NotNull(product.Product);
            Assert.True(product.Product.Id ==1);
            Assert.Null(product.ErrorMessage);

        }

        [Fact]
        public async Task GetProductsReturnsProductUsingInvalidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingInvalidId))
                .Options;

            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productsProfile = new ProductProfile();
            var cofiguration = new MapperConfiguration(cfg => cfg.AddProfile(productsProfile));
            var mapper = new Mapper(cofiguration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync(-1);
            Assert.False(product.IsSuccess);
            Assert.Null(product.Product);
            Assert.NotNull(product.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext dbContent)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContent.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)
                });
            }
            dbContent.SaveChanges();
        }
    }
}