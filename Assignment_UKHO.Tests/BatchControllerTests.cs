using Assignment_UKHO.Controllers;
using Assignment_UKHO.Data;
using Assignment_UKHO.Dto;
using Assignment_UKHO.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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


        BatchController controller;
        Mock<ILogger<BatchController>> logger;
        Mock<IMapper> mapper;
        Mock<IValidator<BatchDto>> validator;
       
        [OneTimeSetUp]
        public void OneTimeSetup()
        {

            context = new AppDbContext(options);
            validator = new Mock<IValidator<BatchDto>>();
            mapper = new Mock<IMapper>();
            logger = new Mock<ILogger<BatchController>>();
           
            controller = new(context,validator.Object,mapper.Object,logger.Object);

            context.Database.EnsureCreated();
            SeedDatabase();
        }

        [Test]
        public void HttpGet_GetBatchById_ReturnsNotFound()
        {
            var obj = new BatchResponseDto
            {
                BatchId= Guid.Parse("4A47E450-37F9-4D57-8954-5BA07DB103AA"),
                BusinessUnit = "Unit1",
                Status = "Incomplte",
                Attributes = new List<Attributes> { new Attributes() { Id = 1, Key = "Key", Value = "Value" } },
                ExpiryDate = DateTime.UtcNow.AddDays(1),
                BatchPublicationDate=DateTime.Now,
                Files = new List<Files> { new Files() {
                    Id=1,
                    FileName="Document",
                    FileSize=49,
                    MimeType="PDF",
                    Hash="HashCode",
                    Attributes=new List<FileAttributes> { new FileAttributes() {  Id=1,Key="Key",Value ="Value"} }
                } }

            };
            var obj1 = new BatchDto
            {
                BatchId= Guid.Parse("4A47E450-37F9-4D57-8954-5BA07DB103AA"),
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

            validator.Setup(s=>s.Validate(obj1).IsValid).Returns(true);
            mapper.Setup(m => m.Map<BatchResponseDto>(obj1)).Returns(obj);
            
            var result = controller.GetBatchById(Guid.Parse("4A47E450-37F9-4D57-8954-5BA07DB103AA"));
          
            Assert.That(result.Result,Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public void HttpGet_GetBatchById_ReturnsOk()
        {
            var id = Guid.NewGuid();
            var obj = new BatchResponseDto
            {
                BatchId = id,
                BusinessUnit = "Unit1",
                Status = "Incomplte",
                Attributes = new List<Attributes> { new Attributes() { Id = 1, Key = "Key", Value = "Value" } },
                ExpiryDate = DateTime.UtcNow.AddDays(1),
                BatchPublicationDate = DateTime.Now,
                Files = new List<Files> { new Files() {
                    Id=1,
                    FileName="Document",
                    FileSize=49,
                    MimeType="PDF",
                    Hash="HashCode",
                    Attributes=new List<FileAttributes> { new FileAttributes() {  Id=1,Key="Key",Value ="Value"} }
                } }

            };
            var obj1 = new BatchDto
            {
               
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

            validator.Setup(s => s.Validate(obj1).IsValid).Returns(true);
            mapper.Setup(m => m.Map<BatchResponseDto>(obj1)).Returns(obj);

            var result = controller.GetBatchById(GetBatchId());

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        }


        [Test]
        public void HttpPost_CreateBatch_BusinessUnitIsInvalid_ReturnsBadRequest()
        {
            var obj = new BatchDto
            {
                
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

          
            validator.Setup(s => s.Validate(obj).IsValid).Returns(true);
           
            var result = controller.CreateBatch(obj);
            Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());

        }

        [Test]
        public void HttpPost_CreateBatch_BusinessUnitIsValid_ReturnsCreated()
        {
            var obj = new BatchDto
            {
                BatchId=Guid.NewGuid(),
                BusinessUnit = "BusinessUnit",
                Acl = new Acl()
                {
                    ReadUsers = new List<ReadUsers> { new ReadUsers() {  User = "User1" } },
                    ReadGroups = new List<ReadGroups> { new ReadGroups() { Name = "Group1" } }
                },
                Attributes = new List<Attributes> { new Attributes() { Key = "Key", Value = "Value" } },
                ExpiryDate = DateTime.Today,
                Files = new List<Files> { new Files() {
                    Id=1,
                    FileName="Document",
                    FileSize=49,
                    MimeType="PDF",
                    Hash="HashCode",
                    Attributes=new List<FileAttributes> { new FileAttributes() {  Key="Key",Value ="Value"} }
                } }

            };
            var obj1 = new Batch
            {

                BusinessUnit = "BusinessUnit",
                Acl = new Acl()
                {
                    ReadUsers = new List<ReadUsers> { new ReadUsers() { User = "User1" } },
                    ReadGroups = new List<ReadGroups> { new ReadGroups() { Name = "Group1" } }
                },
                Attributes = new List<Attributes> { new Attributes() { Key = "Key", Value = "Value" } },
                ExpiryDate = DateTime.Today,
                Files = new List<Files> { new Files() {
                       
                    FileName="Document",
                    FileSize=49,
                    MimeType="PDF",
                    Hash="HashCode",
                    Attributes=new List<FileAttributes> { new FileAttributes() { Key="Key",Value ="Value"} }
                } }

            };


            validator.Setup(s => s.Validate(obj).IsValid).Returns(true);
            
            mapper.Setup(m => m.Map<Batch>(obj)).Returns(obj1);
            mapper.Setup(m => m.Map<BatchDto>(obj1)).Returns(obj);
            var result = controller.CreateBatch(obj);
            Assert.That(result.Result, Is.TypeOf<CreatedResult>());

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
                BusinessUnit = "BusinessUnit" ,
                Acl = new Acl()
                {
                    ReadUsers = new List<ReadUsers> { new ReadUsers() { User = "User1" } },
                    ReadGroups = new List<ReadGroups> { new ReadGroups() { Name = "Group1" } }
                },
                Attributes = new List<Attributes> { new Attributes() { Key = "Key", Value = "Value" } },
                ExpiryDate = DateTime.Today,
                Files = new List<Files> { new Files() {
                        
                    FileName="Document",
                    FileSize=49,
                    MimeType="PDF",
                    Hash="HashCode",
                    Attributes=new List<FileAttributes> { new FileAttributes() {  Key="Key",Value ="Value"} }
                } }

            };
            context.Batches.Add(obj);
            context.SaveChanges();
            var bu = new BusinessUnit()
            {
              
                UnitName="BusinessUnit"
            };
            context.BusinessUnit.Add(bu);
            context.SaveChanges();
            //var bu1 = new BusinessUnit()
            //{
            //    Id = 1,
            //    UnitName = "Unit1"
            //};
            //context.BusinessUnit.Add(bu1);
            //context.SaveChanges();
        }


        private Guid GetBatchId()
        {
            var list = context.Batches.ToList();
            return list[0].Acl.BatchId;
        }
    }
}
