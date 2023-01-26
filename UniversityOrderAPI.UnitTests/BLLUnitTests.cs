using System.Transactions;
using Mapster;
using UniversityOrderAPI.BLL.Category;
using UniversityOrderAPI.BLL.Client;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.BLL.Manufacturer;
using UniversityOrderAPI.BLL.Product;
using UniversityOrderAPI.DAL;
using UniversityOrderAPI.Models.Category;
using UniversityOrderAPI.Models.Client;
using UniversityOrderAPI.Models.Manufacturer;
using UniversityOrderAPI.Models.Product;

namespace UniversityOrderAPI.UnitTests;

[TestFixture]
public class BLLUnitTests
{
    private UniversityOrderAPIDbContext Db;
    private const int StudentStoreId = 1;

    [SetUp]
    public void SetUp()
    {
        Db = new UniversityOrderAPIDbContext();
    }

    [Test]
    public void Category_Name_Null_Or_Empty_Should_Fail()
    {
        // Arrange
        ICommandHandler<CreateCategoryCommand, CreateCategoryCommandResult> commandHandler =
            new CreateCategoryCommandHandler(Db);

        var categoryRequest = new CreateCategoryRequest
        {
            // Enter here any name or (null or empty)
            Name = "SomeName"
        };

        var actualResult = false;
        const bool expectedResult = false;

        // Act
        using (new TransactionScope(
                   TransactionScopeOption.Required,
                   new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
        {
            // Run an EF Core command in the transaction
            try
            {
                commandHandler
                    .Handle(new CreateCategoryCommand(StudentStoreId, categoryRequest.Adapt<CategoryDTO>()),
                        new CancellationToken());
            }
            catch (Exception e) when (e.Message.Contains("Category name null or empty"))
            {
                actualResult = true;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        // Assert
        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }
    
    [Test]
    public void Client_Name_Null_Or_Empty_Should_Fail()
    {
        // Arrange
        ICommandHandler<CreateClientCommand, CreateClientCommandResult> commandHandler =
            new CreateClientCommandHandler(Db);

        var clientRequest = new CreateClientRequest
        {
            // Enter here any name or (null or empty)
            FirstName = "SomeName"
        };

        var actualResult = false;
        const bool expectedResult = false;

        // Act
        using (new TransactionScope(
                   TransactionScopeOption.Required,
                   new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
        {
            // Run an EF Core command in the transaction
            try
            {
                commandHandler
                    .Handle(new CreateClientCommand(StudentStoreId, clientRequest.Adapt<ClientDTO>()),
                        new CancellationToken());
            }
            catch (Exception e) when (e.Message.Contains("Client name null or empty"))
            {
                actualResult = true;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        // Assert
        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }
    
    [Test]
    public void Manufacturer_Name_Null_Or_Empty_Should_Fail()
    {
        // Arrange
        ICommandHandler<CreateManufacturerCommand, CreateManufacturerCommandResult> commandHandler =
            new CreateManufacturerCommandHandler(Db);

        var manufacturerRequest = new CreateManufacturerRequest
        {
            // Enter here any name or (null or empty)
            Name = "SomeName"
        };

        var actualResult = false;
        const bool expectedResult = false;

        // Act
        using (new TransactionScope(
                   TransactionScopeOption.Required,
                   new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
        {
            // Run an EF Core command in the transaction
            try
            {
                commandHandler
                    .Handle(new CreateManufacturerCommand(StudentStoreId, manufacturerRequest.Adapt<ManufacturerDTO>()),
                        new CancellationToken());
            }
            catch (Exception e) when (e.Message.Contains("Manufacturer name null or empty"))
            {
                actualResult = true;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        // Assert
        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }

    [Test]
    public async Task Product_Name_Or_Description_Null_Or_Empty_Should_Fail()
    {
        // Arrange
        ICommandHandler<CreateProductCommand, CreateProductCommandResult> commandHandler =
            new CreateProductCommandHandler(Db);

        var productRequest = new CreateProductRequest
        {
            Name = "SomeName",
            Description = "SomeDescription"
        };

        var actualResult = false;
        const bool expectedResult = false;

        // Act
        using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            // Run an EF Core command in the transaction
            try
            {
                await commandHandler
                    .Handle(new CreateProductCommand(StudentStoreId, productRequest.Adapt<ProductDTO>()),
                        new CancellationToken());
            }
            catch (Exception e) when (e.Message.Contains("Product name is null or empty"))
            {
                actualResult = true;
            }
            catch (Exception e) when (e.Message.Contains("Product description is null or empty"))
            {
                actualResult = true;
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
        // Assert
        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }
}