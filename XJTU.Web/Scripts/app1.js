var myApp1 = angular.module("myApp1", ['ui.router', 'ui.bootstrap', 'ngAnimate']);
myApp1.config(function ($stateProvider, $httpProvider, $urlRouterProvider) {
    //$urlRouterProvider.when("", "/PageTab");
    $urlRouterProvider.otherwise('/HomeIndex');
    $stateProvider.state("HomeIndex", {
             url: "/HomeIndex",
             controller: 'HomeIndexController',
             templateUrl: "HomeIndexContent.html"
         });
});
