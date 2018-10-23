using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WebApiNymti;
using WebApiNymti.Controllers;
using WebApiNymti.Models.Entities;
using WebApiNymti.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApiNymti.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApiTest
{
    [TestClass]
    public class ContentRepositoryUnitTest
    {

        private Mock<IApplicationDbContext> mockDbContextObj;
        private ContentRepository contentRepository;


        private static Mock<DbSet<T>> MockDbSet<T>(IEnumerable<T> list) where T : class, new()
        {
            IQueryable<T> queryableList = list.AsQueryable();
            Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(queryableList.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(queryableList.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());

            return dbSetMock;
        }


        //SETUP
        [TestInitialize]
        public void Setup()
        {
            var contentList = new List<Content> {
                     new Content { ContentId = 1, GroupsId = 1, ContentText = "Content1" },
                     new Content { ContentId = 2, GroupsId = 2, ContentText = "Content2" }
           };
            var mockSetContent = MockDbSet<Content>(contentList);
            var mockSetGroups = MockDbSet<Groups>(new List<Groups> {
                      new Groups { GroupsId = 1, GroupsTitle = "TestGroup1", Contents = contentList},
                       new Groups { GroupsId = 2, GroupsTitle = "TestGroup2", Contents = contentList }
            });

            mockDbContextObj = new Mock<IApplicationDbContext>();
            mockDbContextObj.Setup(m => m.Groups).Returns(mockSetGroups.Object);
            mockDbContextObj.Setup(m => m.Contents).Returns(mockSetContent.Object);

            contentRepository = new ContentRepository(mockDbContextObj.Object);

        }


        [TestMethod]
        public void GetAllContent_GetRequest_ReceiveListOfAllContent()
        {
            //Arrange-->Setup()

            //Act    


            var res = contentRepository.GetAllContent() as IQueryable<Content>;

            //Assert
            Assert.IsNotNull(res);
            Assert.AreEqual(res.Count(), 2);
            Assert.AreEqual(res.Select(x => x.ContentText).Skip(1).First(), "Content2"); //picking the second item from the rerutned Contentlist
        }



        [TestMethod]
        [DataRow(1, "Content1")]
        [DataRow(2, "Content2")]
        public void GetContentByGroupId_SendId_ReceiveSpecificContentText(int id, string expectedContent)
        {
            //Arrange-->Setup()

            //Act       
            var res = contentRepository.GetContentByGroupId(id) as IQueryable<Content>;

            //Assert
            Assert.IsNotNull(res);
            Assert.AreEqual(res.First().ContentText, expectedContent);
        }

        [TestMethod]
        [DataRow(40, "")]
        public void GetContentByGroupId_SendIdOutOfRange_ReceiveEmptyStringContentList(int id, string expectedContent)
        {
            //Arrange-->Setup()
            
            //Act       
            var res = contentRepository.GetContentByGroupId(id) as IQueryable<Content>;

            //Assert
            Assert.AreEqual(res.Count(), 0);
        }


        [TestMethod]
        public void GetAllGroups_SendRequest_ReceiveListOfAllGroups()
        {
            //Arrange-->Setup()

            //Act           
            var res = contentRepository.GetAllGroups() as IQueryable<Groups>;

            //Assert
            Assert.IsNotNull(res);
            Assert.AreEqual(res.Count(), 2);
            Assert.AreEqual(res.Select(x => x.GroupsTitle).Skip(1).First(), "TestGroup2"); //picking the second item from the rerutned grouplist

        }



        [TestMethod]
        public void GetAllContent_DatabaseNull_ReceiveEmptyListOfContent()
        {
            //Arrange-->Setup()

             mockDbContextObj.Setup(m => m.Contents).Returns(MockDbSet<Content>(new List<Content>()).Object);
            //Act      

            var res = contentRepository.GetAllContent() as IQueryable<Content>;

            //Assert
            Assert.AreEqual(res.Count(), 0);

        }



        [TestMethod]
        public void GetAllGroups_DataBaseIsEmpty_ReceiveEmptyList()
        {
            //Arrange-->Setup()
            mockDbContextObj.Setup(m => m.Groups).Returns(MockDbSet<Groups>(new List<Groups>()).Object);

            //Act            
            var res = contentRepository.GetAllGroups() as IQueryable<Groups>;

            //Assert
            Assert.AreEqual(res.Count(), 0);

        }




    }


}
