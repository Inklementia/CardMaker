angular.module('myAppCardCtrl', [])
.controller("CardsController", ["$scope", "$http", function ($scope, $http) {
    $scope.cards = [];
    $scope.keywords = [];
    $scope.unittypes = [];

    $scope.selectedKeyword = null;
    $scope.selectedUnitType = null;

    /* getting unitypes and keywords */
    $http.get('api/Keywords')
        .then(function (response) {
            $scope.keywords = response.data;
        });
    $http.get('api/UnitTypes')
        .then(function (response) {
            $scope.unittypes = response.data;
        });
    /* getting unitypes and keywords end*/
    /* getting filtered cards */
    $scope.GetCards = function () {
        var url = 'api/Cards';
        if ($scope.selectedKeyword && $scope.selectedUnitType) {
            url += '?keywordId=' + $scope.selectedKeyword.id + '&unitTypeId=' + $scope.selectedUnitType.id;
        }
        else if ($scope.selectedKeyword) {
            url += '?keywordId=' + $scope.selectedKeyword.id;
        }
        else if ($scope.selectedUnitType) {
            url += '?unitTypeId=' + $scope.selectedUnitType.id;
        }

        $http.get(url)
            .then(function (response) {
                $scope.cards = response.data;
                console.log(response.data);
            });
    };
    $scope.GetCards(null);
}])
.controller("AddCardController", ["$scope", "$http", '$location', function ($scope, $http, $location) {
    $scope.card = {
            id: 0,
            name: "",
            description: "",
            attack: 0,
            defence: 0,
            manaCost: 0,
            imageFileLink: "https://cdn-lor.mobalytics.gg/production/images/set3/en_us/img/card/game/03SI009-full.webp",
            artDescription: "",
            keywordId: null,
            unitTypeId: null
    };
     $scope.keywords = [];
    $http.get('api/Keywords')
            .then(function (response) {
                $scope.keywords = response.data;
            });
        $scope.unitTypes = [];
    $http.get('api/UnitTypes')
            .then(function (response) {
                $scope.unitTypes = response.data;
            });
        /* saving card */
        $scope.SaveCard = function () {
            $http.post('api/Cards', $scope.card)
                .then(function (response) {
                    $location.path('/cards');
                })
        }
    }])
    .controller("EditCardController", ["$scope", "$http", '$location', '$routeParams', function ($scope, $http, $location, $routeParams) {
        $scope.card = {
            id: 0,
            name: "",
            description: "",
            attack: 0,
            defence: 0,
            manaCost: 0,
            imageFileLink: "https://cdn-lor.mobalytics.gg/production/images/set3/en_us/img/card/game/03SI009-full.webp",
            artDescription: "",
            keywordId: null,
            unitTypeId: null
        };
        $scope.keywords = [];
        $http.get('api/Keywords')
            .then(function (response) {
                $scope.keywords = response.data;
            });
        $scope.unitTypes = [];
        $http.get('api/UnitTypes')
            .then(function (response) {
                $scope.unitTypes = response.data;
            });

        $http.get('api/Cards/' + $routeParams.cardId)
            .then(function (response) {
                $scope.card = response.data;
            });
        /* save edited card */
        $scope.SaveCard = function () {
            $http.put('api/Cards/' + $routeParams.cardId, $scope.card)
                .then(function (response) {
                    $location.path('/cards');
                })
        }
    }])
    .controller("SingleCardController", ["$scope", "$http", '$location', '$routeParams', function ($scope, $http, $location, $routeParams) {
        $scope.card = [];

        $http.get('api/Cards/' + $routeParams.cardId)
            .then(function (response) {

                $scope.card = response.data;
                console.log(response.data);
            });
        /* deleting card */
        $scope.DeleteCard = function (card) {
            $http.delete('api/Cards/' + card.id).then(function (response) {
                $location.path('/cards');
                var index = $scope.cards.indexOf(card);
                $scope.cards.splice(index, 1);
            });
        }
    }])