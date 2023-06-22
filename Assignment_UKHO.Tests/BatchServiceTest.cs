using Microsoft.EntityFrameworkCore;
using NUnit;
using NUnit.Framework;
using Assignment_UKHO.Data;
using FileAttributes = Assignment_UKHO.Data.FileAttributes;
using Assignment_UKHO.Services;
using AutoMapper;
using Assignment_UKHO.Dto;

namespace Assignment_UKHO.Tests
{
    [TestFixture]
    public class BatchServiceTest
    {
        private static DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
                                                                    .UseInMemoryDatabase(databaseName: "Assignment_Service_Test")
                                                                     .Options;

        AppDbContext context;
        BatchService service;



        [OneTimeSetUp]
        public void OneTimeSetup()
        {

            context = new AppDbContext(options);
            service = new BatchService(context);
            context.Database.EnsureCreated();
            SeedDatabase();
        }

        [Test]
        public void CreateBatch_WhenCalled_CreatesABatch()
        {
            var obj = new BatchDto
            {
                BatchId = Guid.NewGuid(),
                BusinessUnit = "Unit" ,
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
            var result = service.Create(obj);


            Assert.That(Task.FromResult(result), Is.Not.Null);
        }

        [Test]
        public void GetBatchById_WhenCalled_ReturnsBatch()
        {
            var result = service.GetBatchById(Guid.NewGuid());

            Assert.That(result, Is.Not.Null);
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
                BusinessUnit = "Unit1",
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
