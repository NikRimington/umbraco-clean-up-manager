using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RB.Umbraco.CleanUpManager.Models;
using RB.Umbraco.CleanUpManager.Services;
using RB.Umbraco.CleanUpManager.Wrappers;
using Umbraco.Core.Persistence;

namespace RB.Umbraco.CleanUpManager.Tests
{
    [TestClass]
    public class CleanUpDataServiceTests
    {

        #region Declarations
        private List<CmsDataType> _mockDataTypes;
        private Mock<IUmbracoDatabaseWrapper> _umbracoDatabaseWrapperMock;
        private Mock<DataTypesService> _cleanUpDataServicesMock;
        #endregion

        #region Setup Methods
        [TestInitialize]
        public void Setup()
        {
            SetupDataTypes();
            SetupUmbracoDatabaseWrapperMock();
            _cleanUpDataServicesMock = new Mock<DataTypesService>(_umbracoDatabaseWrapperMock.Object) {CallBase = true};
        }

        private void SetupUmbracoDatabaseWrapperMock()
        {
            _umbracoDatabaseWrapperMock = new Mock<IUmbracoDatabaseWrapper>();

            
            _umbracoDatabaseWrapperMock.Setup(x => x.ExecuteScalar<List<CmsDataType>>(It.IsAny<string>(),
                                                                                      It.IsAny<object[]>()))
                                       .Returns(() => _mockDataTypes);            

            _umbracoDatabaseWrapperMock.Setup(x => x.ExecuteScalar<List<CmsDataType>>(It.IsAny<Sql>()))
                                       .Returns(() => _mockDataTypes);

            _umbracoDatabaseWrapperMock.Setup(x => x.ExecuteReader<CmsDataType>(It.IsAny<string>()))
                                       .Returns(() => _mockDataTypes);            

        }

        private void SetupDataTypes()
        {
            _mockDataTypes = new List<CmsDataType>
            {
                new CmsDataType
                {
                    Pk = 1,
                    NodeId = 1,
                    PropertyEditorAlias = "AAAA",
                    DbType = "ZZZZ"
                },
                new CmsDataType
                {
                    Pk = 2,
                    NodeId = 2,
                    PropertyEditorAlias = "BBBB",
                    DbType = "YYYY"
                },
                new CmsDataType
                {
                    Pk = 3,
                    NodeId = 3,
                    PropertyEditorAlias = "CCCC",
                    DbType = "XXXX"
                }
            };
        }

        #endregion

        #region Test Methods

        [TestMethod]
        public void ShouldGetOrphanDataTypes()
        {
            //Arrange
            var expected = _mockDataTypes;

            //Act
            var actual = _cleanUpDataServicesMock.Object.GetOrphanDataTypes();

            //Assert
            actual.ShouldAllBeEquivalentTo(expected);
            _umbracoDatabaseWrapperMock.Verify(x => x.ExecuteReader<CmsDataType>(It.IsAny<string>()), Times.Once);
        }        

        #endregion
    }
}
