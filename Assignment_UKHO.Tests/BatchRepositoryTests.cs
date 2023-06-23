using NUnit.Framework;
using Moq;
using Assignment_UKHO.Data.Repository;
using Assignment_UKHO.Data;

namespace Assignment_UKHO.Tests
{
    [TestFixture]
    public class BatchRepositoryTests
    {
        Mock<IRepository> mockRepo;

        [OneTimeSetUp]
        public void Setup()
        {
            mockRepo = new Mock<IRepository>();
        }
        [Test]
        public void CreateBatch_WhenCalled_CreatesABatch()
        {
            var obj = new Batch
            {
                BatchId = Guid.NewGuid(),
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
                    Attributes=new List<Data.FileAttributes> { new Data.FileAttributes() {  Id=1,Key="Key",Value ="Value"} }
                } }

            };

            mockRepo.Object.CreateBatch(obj);


            mockRepo.Verify(r => r.CreateBatch(It.IsAny<Batch>()), Times.Once());

        }


        [Test]
        public void GetBatchById_WhenCalled_ReturnsBatch()
        {
            var batchId = Guid.NewGuid();
            var expectedBatch = new Batch
            {
                BatchId = batchId,
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
                    Attributes=new List<Data.FileAttributes> { new Data.FileAttributes() {  Id=1,Key="Key",Value ="Value"} }
                } }
            };


            mockRepo.Setup(r => r.GetBatchById(batchId)).Returns(Task.FromResult(expectedBatch));



            var result = mockRepo.Object.GetBatchById(batchId);

            //Assert.AreEqual(Task.FromResult(expectedBatch),result);

            Assert.That(result.Result, Is.Not.Null);

        }

        [Test]
        public void GetBatchById_WhenCalled_ReturnsNull()
        {
            var batchId = Guid.NewGuid();
            Batch expectedBatch =null;


            mockRepo.Setup(r => r.GetBatchById(batchId)).Returns(Task.FromResult(expectedBatch));



            var result = mockRepo.Object.GetBatchById(batchId);

            //Assert.AreEqual(Task.FromResult(expectedBatch),result);

            //Assert.That(result.Result, Is.Null);
            Assert.IsNull(result.Result);

        }


    }
}
