angular.module('myAppKeywordCtrl', [])
.controller("KeywordsController", ["$scope", "$http", function ($scope, $http) {
    $scope.keywords = [];
    /* getting all keywords */
    $http.get('api/Keywords')
        .then(function (response) {
            $scope.keywords = response.data;
        });
}])
.controller("AddKeywordController", ["$scope", "$http", '$location', function ($scope, $http, $location) {
    $scope.keyword = {
        id: 0,
            name: "",
            icon: "fas fa-bolt",
            description: ""
        };
        /* saving keyword */
    $scope.SaveKeyword = function () {
         $http.post('api/Keywords', $scope.keyword)
             .then(function (response) {
                 $location.path('/keywords');
         })
     }
}])