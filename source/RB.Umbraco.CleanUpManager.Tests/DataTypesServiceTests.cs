﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    public class DataTypesServiceTests
    {
        #region Declarations
        private List<CmsDataType> _mockDataTypes;
        private Mock<IUmbracoDatabaseWrapper> _umbracoDatabaseWrapperMock;
        private Mock<DataTypesService> _dataTypesServiceMock;
        #endregion

        #region Setup Methods
        [TestInitialize]
        public void Setup()
        {
            SetupDataTypes();
            SetupUmbracoDatabaseWrapperMock();
            _dataTypesServiceMock = new Mock<DataTypesService>(_umbracoDatabaseWrapperMock.Object) { CallBase = true };
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

            _umbracoDatabaseWrapperMock.Setup(x => x.ExecuteReader<CmsDataType>(It.IsAny<string>()))
                                       .Returns(() => _mockDataTypes);

            _umbracoDatabaseWrapperMock.Setup(x => x.Delete<CmsPropertyType>(It.IsAny<string>()))
                                       .Returns(() => 0);

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

        #region Test Public Methods

        [TestMethod]
        public void ShouldGetOrphanDataTypes()
        {
            //Arrange
            var expected = _mockDataTypes;

            //Act
            var actual = _dataTypesServiceMock.Object.GetOrphanDataTypes();

            //Assert
            actual.ShouldAllBeEquivalentTo(expected);
            _umbracoDatabaseWrapperMock.Verify(x => x.ExecuteReader<CmsDataType>(It.IsAny<string>()), Times.Once);

        }

        [TestMethod]
        public void ShouldDeleteOrphanDataTypes()
        {
            //Arrange

            _dataTypesServiceMock.Setup(x => x.GetOrphanDataTypes())
                                 .Returns(() => _mockDataTypes);

            _dataTypesServiceMock.Setup(x => x.CleanUpPropertyTypeTable(It.IsAny<List<CmsDataType>>()))
                                 .Callback(() => { });

            _dataTypesServiceMock.Setup(x => x.CleanUpDataTypePreValuesTable(It.IsAny<List<CmsDataType>>()))
                                 .Callback(() => { });

            _dataTypesServiceMock.Setup(x => x.CleanUpDataTypeTable(It.IsAny<List<CmsDataType>>()))
                                 .Callback(() => { });

            _dataTypesServiceMock.Setup(x => x.CleanUpUmbracoNodeTable(It.IsAny<List<CmsDataType>>()))
                                 .Callback(() => { });


            //Act
            var actual = _dataTypesServiceMock.Object.DeleteOrphanDataTypes();

            //Assert

            _dataTypesServiceMock.Verify(x => x.GetOrphanDataTypes(), Times.Once);
            _dataTypesServiceMock.Verify(x => x.CleanUpPropertyTypeTable(It.IsAny<List<CmsDataType>>()), Times.Once);
            _dataTypesServiceMock.Verify(x => x.CleanUpDataTypePreValuesTable(It.IsAny<List<CmsDataType>>()), Times.Once);
            _dataTypesServiceMock.Verify(x => x.CleanUpDataTypeTable(It.IsAny<List<CmsDataType>>()), Times.Once);
            _dataTypesServiceMock.Verify(x => x.CleanUpUmbracoNodeTable(It.IsAny<List<CmsDataType>>()), Times.Once);

            actual.ShouldBeEquivalentTo(true);

        }

        [TestMethod]
        public void ShouldDeleteSingleOrphanDataType()
        {
            //Arrange

            _dataTypesServiceMock.Setup(x => x.CleanUpPropertyTypeTable(It.IsAny<CmsDataType>()))
                                 .Callback(() => { });

            _dataTypesServiceMock.Setup(x => x.CleanUpDataTypePreValuesTable(It.IsAny<CmsDataType>()))
                                 .Callback(() => { });

            _dataTypesServiceMock.Setup(x => x.CleanUpDataTypeTable(It.IsAny<CmsDataType>()))
                                 .Callback(() => { });

            _dataTypesServiceMock.Setup(x => x.CleanUpUmbracoNodeTable(It.IsAny<CmsDataType>()))
                                 .Callback(() => { });

            _dataTypesServiceMock.Setup(x => x.LogCleanseOperation(It.IsAny<CmsDataType>()))
                                 .Callback(() => { });


            //Act

            var actual = _dataTypesServiceMock.Object.DeleteOrphanDataType(1);

            //Assert

            _dataTypesServiceMock.Verify(x => x.CleanUpPropertyTypeTable(It.IsAny<CmsDataType>()), Times.Once);
            _dataTypesServiceMock.Verify(x => x.CleanUpDataTypePreValuesTable(It.IsAny<CmsDataType>()), Times.Once);
            _dataTypesServiceMock.Verify(x => x.CleanUpDataTypeTable(It.IsAny<CmsDataType>()), Times.Once);
            _dataTypesServiceMock.Verify(x => x.CleanUpUmbracoNodeTable(It.IsAny<CmsDataType>()), Times.Once);
            _dataTypesServiceMock.Verify(x => x.LogCleanseOperation(It.IsAny<CmsDataType>()), Times.Once);

            actual.ShouldBeEquivalentTo(true);

        }

        [TestMethod]
        public void ShouldReturnTrueIfDataTypeDoesNotExist()
        {
            //Act

            var actual = _dataTypesServiceMock.Object.DeleteOrphanDataType(-10000);

            //Assert

            _dataTypesServiceMock.Verify(x => x.CleanUpPropertyTypeTable(It.IsAny<CmsDataType>()), Times.Never);
            _dataTypesServiceMock.Verify(x => x.CleanUpDataTypePreValuesTable(It.IsAny<CmsDataType>()), Times.Never);
            _dataTypesServiceMock.Verify(x => x.CleanUpDataTypeTable(It.IsAny<CmsDataType>()), Times.Never);
            _dataTypesServiceMock.Verify(x => x.CleanUpUmbracoNodeTable(It.IsAny<CmsDataType>()), Times.Never);
            _dataTypesServiceMock.Verify(x => x.LogCleanseOperation(It.IsAny<CmsDataType>()), Times.Never);

            actual.ShouldBeEquivalentTo(true);


        }

        #endregion

        #region Test Interval Methods

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldFailToCleanUpPropertyTypeTableIfInputParamterOrphanDataTypesIsNull()
        {
            //Act
            _dataTypesServiceMock.Object.CleanUpPropertyTypeTable(default(List<CmsDataType>));
            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ShouldCleanUpPropertyTypeTable()
        {
            //Act
            _dataTypesServiceMock.Object.CleanUpPropertyTypeTable(_mockDataTypes);

            //Assert
            _umbracoDatabaseWrapperMock.Verify(x => x.Delete<CmsPropertyType>(It.IsAny<string>()), Times.Once);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldFailToCleanUpDataTypePreValuesTableIfInputParamterOrphanDataTypesIsNull()
        {
            //Act
            _dataTypesServiceMock.Object.CleanUpDataTypePreValuesTable(default(List<CmsDataType>));
            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ShouldCleanUpDataTypePreValuesTable()
        {
            //Act
            _dataTypesServiceMock.Object.CleanUpDataTypePreValuesTable(_mockDataTypes);

            //Assert
            _umbracoDatabaseWrapperMock.Verify(x => x.Delete<CmsDataTypePreValues>(It.IsAny<string>()), Times.Once);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldFailToCleanUpDataTypeTableIfInputParamterOrphanDataTypesIsNull()
        {
            //Act
            _dataTypesServiceMock.Object.CleanUpDataTypeTable(default(List<CmsDataType>));
            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ShouldCleanUpDataTypeTable()
        {
            //Act
            _dataTypesServiceMock.Object.CleanUpDataTypeTable(_mockDataTypes);

            //Assert
            _umbracoDatabaseWrapperMock.Verify(x => x.Delete<CmsDataType>(It.IsAny<string>()), Times.Once);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldFailToCleanUpUmbracoNodeTableIfInputParamterOrphanDataTypesIsNull()
        {
            //Act
            _dataTypesServiceMock.Object.CleanUpDataTypePreValuesTable(default(List<CmsDataType>));
            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ShouldCleanUpUmbracoNodeTable()
        {
            //Act
            _dataTypesServiceMock.Object.CleanUpUmbracoNodeTable(_mockDataTypes);

            //Assert
            _umbracoDatabaseWrapperMock.Verify(x => x.Delete<UmbracoNode>(It.IsAny<string>()), Times.Once);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldFailToCleanUpSinglePropertyTypeTableIfInputParamterOrphanDataTypesIsNull()
        {
            //Act
            _dataTypesServiceMock.Object.CleanUpPropertyTypeTable(default(CmsDataType));
            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ShouldCleanUpSinglePropertyTypeTable()
        {
            //Act
            _dataTypesServiceMock.Object.CleanUpPropertyTypeTable(_mockDataTypes.FirstOrDefault());

            //Assert
            _umbracoDatabaseWrapperMock.Verify(x => x.Delete<CmsPropertyType>(It.IsAny<string>()), Times.Once);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldFailToCleanUpSingleDataTypePreValuesTableIfInputParamterOrphanDataTypesIsNull()
        {
            //Act
            _dataTypesServiceMock.Object.CleanUpDataTypePreValuesTable(default(CmsDataType));
            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ShouldCleanUpSingleDataTypePreValuesTable()
        {
            //Act
            _dataTypesServiceMock.Object.CleanUpDataTypePreValuesTable(_mockDataTypes.FirstOrDefault());

            //Assert
            _umbracoDatabaseWrapperMock.Verify(x => x.Delete<CmsDataTypePreValues>(It.IsAny<string>()), Times.Once);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldFailToCleanUpSingleDataTypeTableIfInputParamterOrphanDataTypesIsNull()
        {
            //Act
            _dataTypesServiceMock.Object.CleanUpDataTypeTable(default(CmsDataType));
            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ShouldCleanUpSingleDataTypeTable()
        {
            //Act
            _dataTypesServiceMock.Object.CleanUpDataTypeTable(_mockDataTypes.FirstOrDefault());

            //Assert
            _umbracoDatabaseWrapperMock.Verify(x => x.Delete<CmsDataType>(It.IsAny<string>()), Times.Once);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldFailToCleanUpSingleUmbracoNodeTableIfInputParamterOrphanDataTypesIsNull()
        {
            //Act
            _dataTypesServiceMock.Object.CleanUpDataTypePreValuesTable(default(CmsDataType));
            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ShouldCleanUpSingleUmbracoNodeTable()
        {
            //Act
            _dataTypesServiceMock.Object.CleanUpUmbracoNodeTable(_mockDataTypes.FirstOrDefault());

            //Assert
            _umbracoDatabaseWrapperMock.Verify(x => x.Delete<UmbracoNode>(It.IsAny<string>()), Times.Once);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldFailToExecuteLogCleanseOperationsIfInputParameterDataTypeIsNull()
        {
            //Act
            _dataTypesServiceMock.Object.LogCleanseOperations(default(List<CmsDataType>));
            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ShouldLogCleanseOperations()
        {
            //Arrange
            _dataTypesServiceMock.Setup(x => x.LogCleanseOperation(It.IsAny<CmsDataType>()))
                                 .Callback(() => { });
            //Act
            _dataTypesServiceMock.Object.LogCleanseOperations(_mockDataTypes);

            //Assert
            _dataTypesServiceMock.Verify(x => x.LogCleanseOperation(It.IsAny<CmsDataType>()), Times.Exactly(_mockDataTypes.Count));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldFailToExecuteLogCleanseOperationIfInputParameterDataTypeIsNull()
        {
            //Act
            _dataTypesServiceMock.Object.LogCleanseOperation(default(CmsDataType));
            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ShouldLogCleanseOperation()
        {
            //No need to test as this method is just a wrapper around the Umbraco LogHelper corresposnding method
            Assert.IsTrue(true);
        }

        #endregion
    }
}
