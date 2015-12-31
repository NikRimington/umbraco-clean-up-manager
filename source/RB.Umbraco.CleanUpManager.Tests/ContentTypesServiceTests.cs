using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RB.Umbraco.CleanUpManager.Services;
using RB.Umbraco.CleanUpManager.Wrappers;
using Umbraco.Core.Models;

namespace RB.Umbraco.CleanUpManager.Tests
{
    /// <summary>
    /// Class ContentTypesServiceTests.
    /// </summary>
    [TestClass]
    public class ContentTypesServiceTests
    {
        #region Declarations        
        /// <summary>
        /// The _mock content types
        /// </summary>
        private List<IContentType> _mockContentTypes;
        /// <summary>
        /// The _parent content type
        /// </summary>
        private Mock<IContentType> _parentContentType;
        /// <summary>
        /// The _umbraco content service mock
        /// </summary>
        private Mock<IUmbracoContentService> _umbracoContentServiceMock;
        /// <summary>
        /// The _umbraco content type service mock
        /// </summary>
        private Mock<IUmbracoContentTypeService> _umbracoContentTypeServiceMock;
        /// <summary>
        /// The _content types service mock
        /// </summary>
        private Mock<ContentTypesService> _contentTypesServiceMock;
        #endregion

        #region Setup Methods
        /// <summary>
        /// Setups this instance.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            SetupContentTypes();

            SetupContentTypeService();

            _umbracoContentServiceMock = new Mock<IUmbracoContentService>();

            _contentTypesServiceMock = new Mock<ContentTypesService>(_umbracoContentServiceMock.Object,
                                                                     _umbracoContentTypeServiceMock.Object
             )
            { CallBase = true };

        }

        /// <summary>
        /// Setups the content type service.
        /// </summary>
        private void SetupContentTypeService()
        {

            _umbracoContentTypeServiceMock = new Mock<IUmbracoContentTypeService>();
            _umbracoContentTypeServiceMock.Setup(x => x.GetAllContentTypes(It.IsAny<int[]>()))
                                          .Returns(() => _mockContentTypes);

            _umbracoContentTypeServiceMock.Setup(x => x.Delete(It.IsAny<ContentType>(), It.IsAny<int>()))
                                          .Callback(() => { });

        }

        /// <summary>
        /// Setups the content types.
        /// </summary>
        private void SetupContentTypes()
        {
            _parentContentType = new Mock<IContentType>();
            _parentContentType.SetupGet(x => x.Id).Returns(7777777);
            _parentContentType.SetupGet(x => x.Alias).Returns("Master Parent");

            _mockContentTypes = new List<IContentType> { _parentContentType.Object };

            var orphanContentTypes = new[] { 'A', 'B', 'C', 'D', 'E', 'F' };
            var nonOrphanContentTypes = new[] { 'G', 'H', 'I', 'J', 'L', 'L', 'M', 'N', 'O', 'P' };
            var counter = 0;

            foreach (var orphanContentType in orphanContentTypes)
            {
                var contentType = new Mock<IContentType>();
                contentType.SetupGet(x => x.Id).Returns(counter++ * 1000);
                contentType.SetupGet(x => x.Alias).Returns(new string(orphanContentType, 4));
                _mockContentTypes.Add(contentType.Object);
            }


            foreach (var nonOrphanContentType in nonOrphanContentTypes)
            {
                var contentType = new Mock<IContentType>();
                contentType.SetupGet(x => x.Id).Returns(counter++ * 1000);
                contentType.SetupGet(x => x.Alias).Returns(new string(nonOrphanContentType, 4));
                contentType.SetupGet(x => x.ParentId).Returns(7777777);
                _mockContentTypes.Add(contentType.Object);
            }
        }

        #endregion

        #region Test Public Methods

        /// <summary>
        /// Shoulds the get orphan content types.
        /// </summary>
        [TestMethod]
        public void ShouldGetOrphanContentTypes()
        {
            //Arrange
            var expected = _mockContentTypes;

            _contentTypesServiceMock.Setup(x => x.HasNoInstanceOfItsOwn(It.IsAny<IContentType>()))
                                    .Returns(true);

            _contentTypesServiceMock.Setup(x => x.IsNotAParentOfAnotherContentType(It.IsAny<IContentType>()))
                                    .Returns(true);

            _contentTypesServiceMock.Setup(
                x =>
                    x.CheckIfContentTypeIsNotPartOfAnotherContentTypeComposition(
                        It.IsAny<IEnumerable<IContentType>>(),
                        It.IsAny<IContentType>()))
               .Returns(true);


            //Act
            var actual = _contentTypesServiceMock.Object.GetOrphanContentTypes();

            //Assert
            actual.ShouldAllBeEquivalentTo(expected);

            _contentTypesServiceMock.Verify(x => x.HasNoInstanceOfItsOwn(It.IsAny<IContentType>()), Times.Exactly(_mockContentTypes.Count));


            _contentTypesServiceMock.Verify(x => x.IsNotAParentOfAnotherContentType(It.IsAny<IContentType>()), Times.Exactly(_mockContentTypes.Count));

            _contentTypesServiceMock.Verify(
                x =>
                    x.CheckIfContentTypeIsNotPartOfAnotherContentTypeComposition(
                        It.IsAny<IEnumerable<IContentType>>(),
                        It.IsAny<IContentType>()), Times.Exactly(_mockContentTypes.Count));
        }

        /// <summary>
        /// Shoulds the get no orphan content types if has instance of its own.
        /// </summary>
        [TestMethod]
        public void ShouldGetNoOrphanContentTypesIfHasInstanceOfItsOwn()
        {
            //Arrange            
            var expected = new List<IContentType>();

            _contentTypesServiceMock.Setup(x => x.HasNoInstanceOfItsOwn(It.IsAny<IContentType>()))
                                    .Returns(false);

            _contentTypesServiceMock.Setup(x => x.IsNotAParentOfAnotherContentType(It.IsAny<IContentType>()))
                                    .Returns(true);

            _contentTypesServiceMock.Setup(
                x =>
                    x.CheckIfContentTypeIsNotPartOfAnotherContentTypeComposition(
                        It.IsAny<IEnumerable<IContentType>>(),
                        It.IsAny<IContentType>()))
               .Returns(true);

            //Act
            var actual = _contentTypesServiceMock.Object.GetOrphanContentTypes();

            //Assert
            actual.ShouldAllBeEquivalentTo(expected);

            _contentTypesServiceMock.Verify(x => x.HasNoInstanceOfItsOwn(It.IsAny<IContentType>()), Times.Exactly(_mockContentTypes.Count));


            _contentTypesServiceMock.Verify(x => x.IsNotAParentOfAnotherContentType(It.IsAny<IContentType>()), Times.Exactly(0));

            _contentTypesServiceMock.Verify(
                x =>
                    x.CheckIfContentTypeIsNotPartOfAnotherContentTypeComposition(
                        It.IsAny<IEnumerable<IContentType>>(),
                        It.IsAny<IContentType>()), Times.Exactly(0));
        }

        /// <summary>
        /// Shoulds the type of the get no orphan content types if is a parent of another content.
        /// </summary>
        [TestMethod]
        public void ShouldGetNoOrphanContentTypesIfIsAParentOfAnotherContentType()
        {
            //Arrange            
            var expected = new List<IContentType>();

            _contentTypesServiceMock.Setup(x => x.HasNoInstanceOfItsOwn(It.IsAny<IContentType>()))
                                    .Returns(true);

            _contentTypesServiceMock.Setup(x => x.IsNotAParentOfAnotherContentType(It.IsAny<IContentType>()))
                                    .Returns(false);

            _contentTypesServiceMock.Setup(
                x =>
                    x.CheckIfContentTypeIsNotPartOfAnotherContentTypeComposition(
                        It.IsAny<IEnumerable<IContentType>>(),
                        It.IsAny<IContentType>()))
               .Returns(true);

            //Act
            var actual = _contentTypesServiceMock.Object.GetOrphanContentTypes();

            //Assert
            actual.ShouldAllBeEquivalentTo(expected);

            _contentTypesServiceMock.Verify(x => x.HasNoInstanceOfItsOwn(It.IsAny<IContentType>()), Times.Exactly(_mockContentTypes.Count));


            _contentTypesServiceMock.Verify(x => x.IsNotAParentOfAnotherContentType(It.IsAny<IContentType>()), Times.Exactly(_mockContentTypes.Count));

            _contentTypesServiceMock.Verify(
                x =>
                    x.CheckIfContentTypeIsNotPartOfAnotherContentTypeComposition(
                        It.IsAny<IEnumerable<IContentType>>(),
                        It.IsAny<IContentType>()), Times.Exactly(0));
        }

        /// <summary>
        /// Shoulds the get no orphan content types if content type is not part of another content type composition.
        /// </summary>
        [TestMethod]
        public void ShouldGetNoOrphanContentTypesIfContentTypeIsNotPartOfAnotherContentTypeComposition()
        {
            //Arrange            
            var expected = new List<IContentType>();

            _contentTypesServiceMock.Setup(x => x.HasNoInstanceOfItsOwn(It.IsAny<IContentType>()))
                                    .Returns(true);

            _contentTypesServiceMock.Setup(x => x.IsNotAParentOfAnotherContentType(It.IsAny<IContentType>()))
                                    .Returns(true);

            _contentTypesServiceMock.Setup(
                x =>
                    x.CheckIfContentTypeIsNotPartOfAnotherContentTypeComposition(
                        It.IsAny<IEnumerable<IContentType>>(),
                        It.IsAny<IContentType>()))
               .Returns(false);

            //Act
            var actual = _contentTypesServiceMock.Object.GetOrphanContentTypes();

            //Assert
            actual.ShouldAllBeEquivalentTo(expected);

            _contentTypesServiceMock.Verify(x => x.HasNoInstanceOfItsOwn(It.IsAny<IContentType>()), Times.Exactly(_mockContentTypes.Count));


            _contentTypesServiceMock.Verify(x => x.IsNotAParentOfAnotherContentType(It.IsAny<IContentType>()), Times.Exactly(_mockContentTypes.Count));

            _contentTypesServiceMock.Verify(
                x =>
                    x.CheckIfContentTypeIsNotPartOfAnotherContentTypeComposition(
                        It.IsAny<IEnumerable<IContentType>>(),
                        It.IsAny<IContentType>()), Times.Exactly(_mockContentTypes.Count));
        }


        /// <summary>
        /// Shoulds the delete all orphan content types.
        /// </summary>
        [TestMethod]
        public void ShouldDeleteAllOrphanContentTypes()
        {
            //Arrange

            _contentTypesServiceMock.Setup(x => x.GetOrphanContentTypes())
                                    .Returns(() => _mockContentTypes);

            //Act
            var actual = _contentTypesServiceMock.Object.DeleteOrphanContentTypes();

            //Assert
            actual.ShouldBeEquivalentTo(true);
            _umbracoContentTypeServiceMock.Verify(x => x.Delete(It.IsAny<IContentType>(),
                                                                It.IsAny<int>()),
                                                                Times.Exactly(_mockContentTypes.Count));
            _contentTypesServiceMock.Verify(x => x.LogCleanseOperations(It.IsAny<List<IContentType>>()),
                                                                       Times.Once);
        }

        /// <summary>
        /// Shoulds the delete no content types.
        /// </summary>
        [TestMethod]
        public void ShouldDeleteNoContentTypes()
        {
            //Arrange
            _contentTypesServiceMock.Setup(x => x.GetOrphanContentTypes())
                                   .Returns(() => new List<IContentType>());

            _contentTypesServiceMock.Setup(x => x.LogCleanseOperations(It.IsAny<List<IContentType>>()))
                                    .Callback(() => { });

            //Act
            var actual = _contentTypesServiceMock.Object.DeleteOrphanContentTypes();

            //Assert
            actual.ShouldBeEquivalentTo(true);
            _umbracoContentTypeServiceMock.Verify(x => x.Delete(It.IsAny<IContentType>(),
                                                                It.IsAny<int>()),
                                                                Times.Never);

            _contentTypesServiceMock.Verify(x => x.LogCleanseOperations(It.IsAny<List<IContentType>>()),
                                                                       Times.Never);

        }


        /// <summary>
        /// Shoulds the type of the delete relevant orphan content.
        /// </summary>
        [TestMethod]
        public void ShouldDeleteRelevantOrphanContentType()
        {
            //Arrange
            _contentTypesServiceMock.Setup(x => x.GetOrphanContentTypes())
                                    .Returns(() => _mockContentTypes);

            _contentTypesServiceMock.Setup(x => x.LogCleanseOperation(It.IsAny<IContentType>()))
                                    .Callback(() => { });

            //Act
            var actual = _contentTypesServiceMock.Object.DeleteOrphanContentType(1000);

            //Assert
            actual.ShouldBeEquivalentTo(true);
            _umbracoContentTypeServiceMock.Verify(x => x.Delete(It.IsAny<IContentType>(),
                                                                It.IsAny<int>()),
                                                                Times.Exactly(1));

            _contentTypesServiceMock.Verify(x => x.LogCleanseOperation(It.IsAny<IContentType>()),
                                                                       Times.Once);
        }


        /// <summary>
        /// Shoulds the type of the delete no orphan content.
        /// </summary>
        [TestMethod]
        public void ShouldDeleteNoOrphanContentType()
        {
            //Arrange
            _contentTypesServiceMock.Setup(x => x.GetOrphanContentTypes())
                                    .Returns(() => new List<IContentType>());

            _contentTypesServiceMock.Setup(x => x.LogCleanseOperation(It.IsAny<IContentType>()))
                                    .Callback(() => { });


            //Act
            var actual = _contentTypesServiceMock.Object.DeleteOrphanContentType(1000);

            //Assert
            actual.ShouldBeEquivalentTo(true);
            _umbracoContentTypeServiceMock.Verify(x => x.Delete(It.IsAny<IContentType>(),
                                                                It.IsAny<int>()),
                                                                Times.Exactly(1));

            _contentTypesServiceMock.Verify(x => x.LogCleanseOperation(It.IsAny<IContentType>()),
                                                                       Times.Never);
        }

        #endregion

        #region Test Interval Methods

        /// <summary>
        /// Shoulds the fail to execute log cleanse operations if input parameter content type is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldFailToExecuteLogCleanseOperationsIfInputParameterContentTypeIsNull()
        {
            //Act
            _contentTypesServiceMock.Object.LogCleanseOperations(default(List<IContentType>));
            //Assert
            Assert.Fail();
        }

        /// <summary>
        /// Shoulds the log cleanse operations.
        /// </summary>
        [TestMethod]
        public void ShouldLogCleanseOperations()
        {
            //Arrange
            _contentTypesServiceMock.Setup(x => x.LogCleanseOperation(It.IsAny<IContentType>()))
                                 .Callback(() => { });
            //Act
            _contentTypesServiceMock.Object.LogCleanseOperations(_mockContentTypes);

            //Assert
            _contentTypesServiceMock.Verify(x => x.LogCleanseOperation(It.IsAny<IContentType>()), Times.Exactly(_mockContentTypes.Count));

        }

        /// <summary>
        /// Shoulds the fail to execute log cleanse operation if input parameter content type is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldFailToExecuteLogCleanseOperationIfInputParameterContentTypeIsNull()
        {
            //Act
            _contentTypesServiceMock.Object.LogCleanseOperation(default(IContentType));
            //Assert
            Assert.Fail();
        }

        /// <summary>
        /// Shoulds the log cleanse operation.
        /// </summary>
        [TestMethod]
        public void ShouldLogCleanseOperation()
        {
            //No need to test as this method is just a wrapper around the Umbraco LogHelper corresposnding method
            Assert.IsTrue(true);
        }

        #endregion
    }
}
