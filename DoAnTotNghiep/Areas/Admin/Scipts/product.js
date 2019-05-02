/// <reference path="../../../scripts/angular.min.js" />

var app = angular.module('myApp', []);

app.controller('myCtrl', function ($scope, $http) {
    // define function when start project
    $scope.init = function () {
        $scope.LoadAllProduct();
    }

    $scope.LoadAllProduct = function () {
        $http({
            url: "/Admin/Product/GetAllProduct",
            method: 'GET'
        }).then(function mySuccess(res) {
            
            $scope.ListProduct = res.data.result;
            
            $scope.CreateDate = res.data.CreatedDate;
            console.log($scope.CreateDate);
        }, function myError(res) {
        });
    }

});