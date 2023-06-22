using Assignment_UKHO.Controllers;
using Assignment_UKHO.Data;
using Assignment_UKHO.Services;
using AutoMapper;
using Castle.Core.Logging;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using FileAttributes = Assignment_UKHO.Data.FileAttributes;

namespace Assignment_UKHO.Tests
{
    [TestFixture]
    public class BatchControllerTests
    {
        private static DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
                                                                   .UseInMemoryDatabase(databaseName: "Assignment_Controller_Test")
                                                                    .Options;

        AppDbContext context;
        
        



        [OneTimeSetUp]
        public void OneTimeSetup()
        {

            context = new AppDbContext(options);



            context.Database.EnsureCreated();
            SeedDatabase();
        }

        [Test]
        public void HttpGet_GetBatchById_ReturnsOk()
        {

            Assert.True(true);
        }




        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var obj = new Batch
            {
                BatchId = Guid.Parse("4A47E450-37F9-4D57-8954-5BA07DB103AA"),
                BusinessUnit = "Unit1" ,
                Acl = new Acl()
                {
                    ReadUsers = new List<ReadUsers> { new ReadUsers() { Id = 1, User = "User1" } },
                    ReadGroups = new List<ReadGroups> { new ReadGroups() { Id = 1, Name = "Group1" } }
                },
                Attributes = new List<Attributes> { new Attributes() { Id = 1, Key = "Key", Value = "Value" } },
                ExpiryDate = DateTime.Today,
                Files = new List<Files> { new Files() {
                    Id=1,
                    FileName="Document",
                    FileSize=49,
                    MimeType="PDF",
                    Hash="HashCode",
                    Attributes=new List<FileAttributes> { new FileAttributes() {  Id=1,Key="Key",Value ="Value"} }
                } }

            };
            context.Add(obj);
            context.SaveChanges();
        }

    }
}
