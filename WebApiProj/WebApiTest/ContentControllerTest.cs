using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WebApiNymti;
using WebApiNymti.Controllers;
using WebApiNymti.Models.Entities;
using WebApiNymti.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebApiTest
{
    [TestClass]
    public class ContentControllerTest
    {
        //FIRST INITIALS
        static IQueryable<Content> listContent = new List<Content>()
        {
           new Content {ContentId = 1, GroupsId= 1, ContentText ="Content1" },
           new Content {ContentId = 2, GroupsId= 2, ContentText ="Content2" }
        }.AsQueryable();
        static IQueryable<Groups> listGroups = new List<Groups>()
        {
           new Groups {GroupsId= 1, GroupsTitle = "TestGroup1",  Contents =listContent.ToList() },
           new Groups {GroupsId= 2, GroupsTitle = "TestGroup2",  Contents =listContent.ToList() }
        }.AsQueryable();
        private Mock<IContentRepository> contentRepo;
        private ContentController contentController;

        //SETUP
        [TestInitialize]
        public void Setup()
        {
            contentRepo = new Mock<IContentRepository>();
            contentController = new ContentController(contentRepo.Object);

        }


        [TestMethod]
        [DataRow(1, "Content1")]
        [DataRow(2, "Content2")]
        public void GetContentByGroupId_SendId_ReceiveSpecificContentText(int id, string expectedContent)
        {
            //Arrange-->Setup()


            //Act       
            contentRepo.Setup(cr => cr.GetContentByGroupId(id)).Returns(listContent.Where(x => x.GroupsId == id));
            var res = (contentController.GetByGroupId(id) as OkObjectResult).Value as IEnumerable<Content>;

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
            contentRepo.Setup(cr => cr.GetContentByGroupId(id)).Returns(listContent.Where(x => x.GroupsId == id));
            var res = (contentController.GetByGroupId(id) as OkObjectResult).Value as IEnumerable<Content>;

            //Assert
            Assert.AreEqual(res.Count(), 0);
        }


        [TestMethod]
        public void GetAllContent_SendRequest_ReceiveListOfAllContent()
        {
            //Arrange-->Setup()

            //Act            
            contentRepo.Setup(cr => cr.GetAllContent()).Returns(listContent);
            var res = (contentController.GetAllContent() as OkObjectResult).Value  as IEnumerable<Content>;

            //Assert
            Assert.IsNotNull(res);
            Assert.AreEqual(res.Count(), 2);
            Assert.AreEqual(res.Select(x => x.ContentText).Skip(1).First(), "Content2"); //picking the second item from the rerutned Contentlist
        }

        [TestMethod]
        public void GetAllContent_DatabaseNull_ReceiveEmptyListOfContent()
        {
            //Arrange-->Setup()

            //Act            
            contentRepo.Setup(cr => cr.GetAllContent()).Returns( new List<Content>().AsQueryable);
            var res = (contentController.GetAllContent() as OkObjectResult).Value as IEnumerable<Content>;

            //Assert
            Assert.AreEqual(res.Count(), 0);
           
        }




        [TestMethod]
        public void GetAllGroups_SendRequest_ReceiveListOfAllGroups()
        {
            //Arrange-->Setup()

            //Act            
            contentRepo.Setup(cr => cr.GetAllGroups()).Returns(listGroups);
            var res = (contentController.GetAllGroups() as OkObjectResult).Value as IEnumerable<Groups>;

            //Assert
            Assert.AreEqual(res.Count(), 2);
            Assert.IsNotNull(res);
            Assert.AreEqual(res.Select(x=>x.GroupsTitle).Skip(1).First(), "TestGroup2"); //picking the second item from the rerutned grouplist



        }



        [TestMethod]
        public void GetAllGroups_DataBaseIsEmpty_ReceiveEmptyList()
        {
            //Arrange-->Setup()

            //Act            
            contentRepo.Setup(cr => cr.GetAllGroups()).Returns(new List<Groups>().AsQueryable);
            var res = (contentController.GetAllGroups() as OkObjectResult).Value as IEnumerable<Groups>;

            //Assert
            Assert.AreEqual(res.Count(), 0);
            
        }




    }
}
