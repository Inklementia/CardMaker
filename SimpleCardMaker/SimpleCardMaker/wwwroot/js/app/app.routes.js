var myapp = angular.module('myAppRouter', ['ngRoute']);
myapp.config(function ($routeProvider) {
    $routeProvider
        .when("/",
            {
                templateUrl: "pages/index.html",
                controller: "IndexController"
            })
        .when("/cards",
            {
                templateUrl: "pages/cards.html",
                controller: "CardsController"
            })
        .when("/cards/add",
            {
                templateUrl: "pages/add-edit-card.html",
                controller: "AddCardController"
            })
        .when("/cards/edit/:cardId",
            {
                templateUrl: "pages/add-edit-card.html",
                controller: "EditCardController"
            })
        .when("/cards/:cardId",
            {
                templateUrl: "pages/single-card.html",
                controller: "SingleCardController"
            })
        .when("/keywords",
            {
                templateUrl: "pages/keywords.html",
                controller: "KeywordsController"
            })
        .when("/keywords/add",
            {
                templateUrl: "pages/add-edit-keyword.html",
                controller: "AddKeywordController"
            })
        .when("/unittypes",
            {
                templateUrl: "pages/unittypes.html",
                controller: "UnitTypesController"
            })
        .when("/unittypes/add",
            {
                templateUrl: "pages/add-edit-unittype.html",
                controller: "AddUnitTypeController"
            })
        .when("/404",
            {
                templateUrl: "pages/404.html",
                controller: "NowFoundController"
            })
        .otherwise(
            {
                redirectTo: "/404"
            }
        );
});