(function () {


    var dataTypesController = function ($scope,
                                        $filter,
                                        $timeout,
                                        notificationsService,
                                        dataTypesService) {

        $scope.totalRecords = 0;
        $scope.totalPages = 0;
        $scope.pageSize = 10;
        $scope.currentPage = 1;
        $scope.dataTypes = [];
        $scope.orderby = "PropertyEditorAlias";
        $scope.reverse = false;
        $scope.searchText = "";
        $scope.cardAnimationClass = "card-animation";
        $scope.DisplayModeEnum = {
            Card: 0,
            List: 1
        };

        var getDataTypes = function () {
            dataTypesService.getDataTypes($scope.currentPage - 1, $scope.pageSize, $scope.searchText)
            .then(function (data) {
                $scope.totalRecords = data.totalRecords;
                $scope.totalPages = Math.ceil(data.totalRecords / $scope.pageSize);
                $scope.dataTypes = data.results;
                if ($scope.dataTypes.length === 0) {
                    $scope.currentPage = 0;
                }
            }, function (e) {
                console.log(e);
                notificationsService.success("CleanUp Manager", "Error:  " + e);
            });
        }

        $scope.firstPage = function () {
            if (($scope.currentPage - 1) >= 1) {
                $scope.currentPage = 1;
                getDataTypes();
            }
        };

        $scope.previousPage = function () {
            if (($scope.currentPage - 1) >= 1) {
                $scope.currentPage--;
                getDataTypes();
            }
        };

        $scope.nextPage = function () {
            if (($scope.currentPage + 1) <= $scope.totalPages) {
                $scope.currentPage++;
                getDataTypes();
            }
        };

        $scope.lastPage = function () {
            if (($scope.currentPage + 1) <= $scope.totalPages) {
                $scope.currentPage = $scope.totalPages;
                getDataTypes();
            }
        };

        $scope.deleteDataTypes = function () {
            if (confirm("Are you sure you want to delete all data types?")) {
                dataTypesService.deleteDataTypes().then(function () {
                    getDataTypes();
                }, function (e) {
                    console.log(e);
                    notificationsService.success("CleanUp Manager", "Error:  " + e);
                });
            }
        };

        $scope.deleteDataType = function (id, alias) {
            if (confirm("Are you sure you want to delete " + alias + " data type?")) {
                dataTypesService.deleteDataType(id).then(function (response) {
                    console.log(response);
                    getDataTypes();
                }, function (e) {
                    console.log(e);
                    notificationsService.success("CleanUp Manager", "Error:  " + e);
                });
            }
        };


        $scope.setOrder = function (orderby) {
            // Need to implement this on the server
            //if (orderby === $scope.orderby) {
            //    $scope.reverse = !$scope.reverse;
            //}
            //$scope.orderby = orderby;
        };

        $scope.search = function () {
            $scope.currentPage = 1;
            getDataTypes();
        };

        $scope.showNode = function (nodeId) {
            window.location = "/umbraco#/developer/datatype/edit/" + nodeId;
        };

        function init() {
            getDataTypes();
        }

        init();
    };

    dataTypesController.$inject = ["$scope",
                                   "$filter",
                                   "$timeout",
                                   "notificationsService",
                                   "RB.Umbraco.CleanUpManager.DataTypesService"];

    angular.module("umbraco").controller("RB.Umbraco.CleanUpManager.DataTypesController", dataTypesController);

}());
