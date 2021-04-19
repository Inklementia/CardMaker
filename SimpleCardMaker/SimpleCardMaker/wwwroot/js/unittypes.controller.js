angular.module('myAppUnitTypeCtrl', [])
.controller("UnitTypesController", ["$scope", "$http", function ($scope, $http) {
    $scope.unittypes = [];
    /* getting all unittypes */
    $http.get('api/UnitTypes')
        .then(function (response) {
            $scope.unittypes = response.data;
        })
}])
    .controller("AddUnitTypeController", ["$scope", "$http", '$location', function ($scope, $http, $location) {
        $scope.unittype = {
            id: 0,
            name: "",
            description: ""
        };
        /* saving unitype */
        $scope.SaveUnitType = function () {
            $http.post('api/UnitTypes', $scope.unittype)
                .then(function (response) {
                    $location.path('/unittypes');
                })
        }
    }]);