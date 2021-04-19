angular.module('myAppHomeCtrl', [])
.controller("IndexController", ["$scope", "$http", function ($scope, $http) {
    $scope.message = "Welcome!";
}])