//using System;
//using System.Collections.Generic;
//using System.Linq;
//using FluentAssertions;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using RB.Umbraco.CleanUpManager.Models;
//using RB.Umbraco.CleanUpManager.Services;
//using RB.Umbraco.CleanUpManager.Wrappers;
//using Umbraco.Core.Models;
//using Umbraco.Core.Persistence;

//namespace RB.Umbraco.CleanUpManager.Tests
//{
//    [TestClass]
//    public class ContentTypesServiceTests
//    {
//        #region Declarations
//        private List<IContentType> _mockContentTypes;
//        private Mock<IUmbracoContentService> _umbracoContentServiceMock;
//        private Mock<IUmbracoContentTypeService> _umbracoContentTypeServiceMock;
//        private Mock<ContentTypesService> _contentTypesServiceMock;
//        #endregion

//        #region Setup Methods
//        [TestInitialize]
//        public void Setup()
//        {
//            SetupContentTypes();

//            _umbracoContentServiceMock = new Mock<IUmbracoContentService>();
//            _umbracoContentTypeServiceMock = new Mock<IUmbracoContentTypeService>();

//            _contentTypesServiceMock = new Mock<ContentTypesService>(_umbracoContentTypeServiceMock.Object) { CallBase = true };
//        }

//        private void SetupContentTypes()
//        {
//            _mockContentTypes = new List<IContentType>
//            {
//                new ContentType
//                {
//                  Alias = "AAAAAA"
//                },
//            };
//        }

//        #endregion

//        #region Test Public Methods

//        [TestMethod]
//        public void ShouldGetOrphanContentTypes()
//        {
//            //Arrange
//            var expected = _mockContentTypes;

//            //Act
//            var actual = _contentTypesServiceMock.Object.GetOrphanContentTypes();

//            //Assert
//            actual.ShouldAllBeEquivalentTo(expected);
//            _umbracoContentTypeServiceMock.Verify(x => x.ExecuteReader<IContentType>(It.IsAny<string>()), Times.Once);

//        }

//        [TestMethod]
//        public void ShouldDeleteOrphanContentTypes()
//        {
//            //Arrange

//            _contentTypesServiceMock.Setup(x => x.GetOrphanContentTypes())
//                                 .Returns(() => _mockContentTypes);

//            _contentTypesServiceMock.Setup(x => x.CleanUpPropertyTypeTable(It.IsAny<List<IContentType>>()))
//                                 .Callback(() => { });

//            _contentTypesServiceMock.Setup(x => x.CleanUpContentTypePreValuesTable(It.IsAny<List<IContentType>>()))
//                                 .Callback(() => { });

//            _contentTypesServiceMock.Setup(x => x.CleanUpContentTypeTable(It.IsAny<List<IContentType>>()))
//                                 .Callback(() => { });

//            _contentTypesServiceMock.Setup(x => x.CleanUpUmbracoNodeTable(It.IsAny<List<IContentType>>()))
//                                 .Callback(() => { });


//            //Act
//            var actual = _contentTypesServiceMock.Object.DeleteOrphanContentTypes();

//            //Assert

//            _contentTypesServiceMock.Verify(x => x.GetOrphanContentTypes(), Times.Once);
//            _contentTypesServiceMock.Verify(x => x.CleanUpPropertyTypeTable(It.IsAny<List<IContentType>>()), Times.Once);
//            _contentTypesServiceMock.Verify(x => x.CleanUpContentTypePreValuesTable(It.IsAny<List<IContentType>>()), Times.Once);
//            _contentTypesServiceMock.Verify(x => x.CleanUpContentTypeTable(It.IsAny<List<IContentType>>()), Times.Once);
//            _contentTypesServiceMock.Verify(x => x.CleanUpUmbracoNodeTable(It.IsAny<List<IContentType>>()), Times.Once);

//            actual.ShouldBeEquivalentTo(true);

//        }

//        [TestMethod]
//        public void ShouldDeleteSingleOrphanContentType()
//        {
//            //Arrange

//            _contentTypesServiceMock.Setup(x => x.CleanUpPropertyTypeTable(It.IsAny<IContentType>()))
//                                 .Callback(() => { });

//            _contentTypesServiceMock.Setup(x => x.CleanUpContentTypePreValuesTable(It.IsAny<IContentType>()))
//                                 .Callback(() => { });

//            _contentTypesServiceMock.Setup(x => x.CleanUpContentTypeTable(It.IsAny<IContentType>()))
//                                 .Callback(() => { });

//            _contentTypesServiceMock.Setup(x => x.CleanUpUmbracoNodeTable(It.IsAny<IContentType>()))
//                                 .Callback(() => { });

//            _contentTypesServiceMock.Setup(x => x.LogCleanseOperation(It.IsAny<IContentType>()))
//                                 .Callback(() => { });


//            //Act

//            var actual = _contentTypesServiceMock.Object.DeleteOrphanContentType(1);

//            //Assert

//            _contentTypesServiceMock.Verify(x => x.CleanUpPropertyTypeTable(It.IsAny<IContentType>()), Times.Once);
//            _contentTypesServiceMock.Verify(x => x.CleanUpContentTypePreValuesTable(It.IsAny<IContentType>()), Times.Once);
//            _contentTypesServiceMock.Verify(x => x.CleanUpContentTypeTable(It.IsAny<IContentType>()), Times.Once);
//            _contentTypesServiceMock.Verify(x => x.CleanUpUmbracoNodeTable(It.IsAny<IContentType>()), Times.Once);
//            _contentTypesServiceMock.Verify(x => x.LogCleanseOperation(It.IsAny<IContentType>()), Times.Once);

//            actual.ShouldBeEquivalentTo(true);

//        }

//        [TestMethod]
//        public void ShouldReturnTrueIfContentTypeDoesNotExist()
//        {
//            //Act

//            var actual = _contentTypesServiceMock.Object.DeleteOrphanContentType(-10000);

//            //Assert

//            _contentTypesServiceMock.Verify(x => x.CleanUpPropertyTypeTable(It.IsAny<IContentType>()), Times.Never);
//            _contentTypesServiceMock.Verify(x => x.CleanUpContentTypePreValuesTable(It.IsAny<IContentType>()), Times.Never);
//            _contentTypesServiceMock.Verify(x => x.CleanUpContentTypeTable(It.IsAny<IContentType>()), Times.Never);
//            _contentTypesServiceMock.Verify(x => x.CleanUpUmbracoNodeTable(It.IsAny<IContentType>()), Times.Never);
//            _contentTypesServiceMock.Verify(x => x.LogCleanseOperation(It.IsAny<IContentType>()), Times.Never);

//            actual.ShouldBeEquivalentTo(true);


//        }

//        #endregion

//        #region Test Interval Methods

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void ShouldFailToCleanUpPropertyTypeTableIfInputParamterOrphanContentTypesIsNull()
//        {
//            //Act
//            _contentTypesServiceMock.Object.CleanUpPropertyTypeTable(default(List<IContentType>));
//            //Assert
//            Assert.Fail();
//        }

//        [TestMethod]
//        public void ShouldCleanUpPropertyTypeTable()
//        {
//            //Act
//            _contentTypesServiceMock.Object.CleanUpPropertyTypeTable(_mockContentTypes);

//            //Assert
//            _umbracoContentTypeServiceMock.Verify(x => x.Delete<CmsPropertyType>(It.IsAny<string>()), Times.Once);

//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void ShouldFailToCleanUpContentTypePreValuesTableIfInputParamterOrphanContentTypesIsNull()
//        {
//            //Act
//            _contentTypesServiceMock.Object.CleanUpContentTypePreValuesTable(default(List<IContentType>));
//            //Assert
//            Assert.Fail();
//        }

//        [TestMethod]
//        public void ShouldCleanUpContentTypePreValuesTable()
//        {
//            //Act
//            _contentTypesServiceMock.Object.CleanUpContentTypePreValuesTable(_mockContentTypes);

//            //Assert
//            _umbracoContentTypeServiceMock.Verify(x => x.Delete<IContentTypePreValues>(It.IsAny<string>()), Times.Once);

//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void ShouldFailToCleanUpContentTypeTableIfInputParamterOrphanContentTypesIsNull()
//        {
//            //Act
//            _contentTypesServiceMock.Object.CleanUpContentTypeTable(default(List<IContentType>));
//            //Assert
//            Assert.Fail();
//        }

//        [TestMethod]
//        public void ShouldCleanUpContentTypeTable()
//        {
//            //Act
//            _contentTypesServiceMock.Object.CleanUpContentTypeTable(_mockContentTypes);

//            //Assert
//            _umbracoContentTypeServiceMock.Verify(x => x.Delete<IContentType>(It.IsAny<string>()), Times.Once);

//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void ShouldFailToCleanUpUmbracoNodeTableIfInputParamterOrphanContentTypesIsNull()
//        {
//            //Act
//            _contentTypesServiceMock.Object.CleanUpContentTypePreValuesTable(default(List<IContentType>));
//            //Assert
//            Assert.Fail();
//        }

//        [TestMethod]
//        public void ShouldCleanUpUmbracoNodeTable()
//        {
//            //Act
//            _contentTypesServiceMock.Object.CleanUpUmbracoNodeTable(_mockContentTypes);

//            //Assert
//            _umbracoContentTypeServiceMock.Verify(x => x.Delete<UmbracoNode>(It.IsAny<string>()), Times.Once);

//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void ShouldFailToCleanUpSinglePropertyTypeTableIfInputParamterOrphanContentTypesIsNull()
//        {
//            //Act
//            _contentTypesServiceMock.Object.CleanUpPropertyTypeTable(default(IContentType));
//            //Assert
//            Assert.Fail();
//        }

//        [TestMethod]
//        public void ShouldCleanUpSinglePropertyTypeTable()
//        {
//            //Act
//            _contentTypesServiceMock.Object.CleanUpPropertyTypeTable(_mockContentTypes.FirstOrDefault());

//            //Assert
//            _umbracoContentTypeServiceMock.Verify(x => x.Delete<CmsPropertyType>(It.IsAny<string>()), Times.Once);

//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void ShouldFailToCleanUpSingleContentTypePreValuesTableIfInputParamterOrphanContentTypesIsNull()
//        {
//            //Act
//            _contentTypesServiceMock.Object.CleanUpContentTypePreValuesTable(default(IContentType));
//            //Assert
//            Assert.Fail();
//        }

//        [TestMethod]
//        public void ShouldCleanUpSingleContentTypePreValuesTable()
//        {
//            //Act
//            _contentTypesServiceMock.Object.CleanUpContentTypePreValuesTable(_mockContentTypes.FirstOrDefault());

//            //Assert
//            _umbracoContentTypeServiceMock.Verify(x => x.Delete<IContentTypePreValues>(It.IsAny<string>()), Times.Once);

//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void ShouldFailToCleanUpSingleContentTypeTableIfInputParamterOrphanContentTypesIsNull()
//        {
//            //Act
//            _contentTypesServiceMock.Object.CleanUpContentTypeTable(default(IContentType));
//            //Assert
//            Assert.Fail();
//        }

//        [TestMethod]
//        public void ShouldCleanUpSingleContentTypeTable()
//        {
//            //Act
//            _contentTypesServiceMock.Object.CleanUpContentTypeTable(_mockContentTypes.FirstOrDefault());

//            //Assert
//            _umbracoContentTypeServiceMock.Verify(x => x.Delete<IContentType>(It.IsAny<string>()), Times.Once);

//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void ShouldFailToCleanUpSingleUmbracoNodeTableIfInputParamterOrphanContentTypesIsNull()
//        {
//            //Act
//            _contentTypesServiceMock.Object.CleanUpContentTypePreValuesTable(default(IContentType));
//            //Assert
//            Assert.Fail();
//        }

//        [TestMethod]
//        public void ShouldCleanUpSingleUmbracoNodeTable()
//        {
//            //Act
//            _contentTypesServiceMock.Object.CleanUpUmbracoNodeTable(_mockContentTypes.FirstOrDefault());

//            //Assert
//            _umbracoContentTypeServiceMock.Verify(x => x.Delete<UmbracoNode>(It.IsAny<string>()), Times.Once);

//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void ShouldFailToExecuteLogCleanseOperationsIfInputParameterContentTypeIsNull()
//        {
//            //Act
//            _contentTypesServiceMock.Object.LogCleanseOperations(default(List<IContentType>));
//            //Assert
//            Assert.Fail();
//        }

//        [TestMethod]
//        public void ShouldLogCleanseOperations()
//        {
//            //Arrange
//            _contentTypesServiceMock.Setup(x => x.LogCleanseOperation(It.IsAny<IContentType>()))
//                                 .Callback(() => { });
//            //Act
//            _contentTypesServiceMock.Object.LogCleanseOperations(_mockContentTypes);

//            //Assert
//            _contentTypesServiceMock.Verify(x => x.LogCleanseOperation(It.IsAny<IContentType>()), Times.Exactly(_mockContentTypes.Count));

//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void ShouldFailToExecuteLogCleanseOperationIfInputParameterContentTypeIsNull()
//        {
//            //Act
//            _contentTypesServiceMock.Object.LogCleanseOperation(default(IContentType));
//            //Assert
//            Assert.Fail();
//        }

//        [TestMethod]
//        public void ShouldLogCleanseOperation()
//        {
//            //No need to test as this method is just a wrapper around the Umbraco LogHelper corresposnding method
//            Assert.IsTrue(true);
//        }

//        #endregion
//    }
//}
