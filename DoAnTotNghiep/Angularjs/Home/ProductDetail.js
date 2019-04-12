/// <reference path="../../../scripts/angular.min.js" />

var app = angular.module('myApp', []);

app.controller('myController', myController);

myController.$inject = ['$scope', '$http'];

function myController($scope, $http) {
    $scope.init = function () {
        $scope.ProductDetail();
        $scope.GetRelatedProduct();
        //$scope.CheckQuantity();

        //ToDo
    };

    $scope.ProductDetail = function () {
        var Id = content;
        var response = $http({
            url: "/Home/GetProductModal/" + Id,
            method: 'GET'
        });
        response.then(function mySuccess(res) {
            // return array id = id input
            $scope.GetDetail = res.data.result[0];
            $scope.ProductID = $scope.GetDetail.ProductID;
            $scope.ProductName = $scope.GetDetail.ProductName;
            $scope.Price = $scope.GetDetail.Price;
            $scope.MetaTitle = $scope.GetDetail.MetaTitle;
            $scope.SupplierName = $scope.GetDetail.SupplierName;
            $scope.Image = $scope.GetDetail.Image;
            $scope.CategoryName = $scope.GetDetail.CategoryName;
            $scope.Description = $scope.GetDetail.Description;

            $scope.ListSize = res.data.listSize;
            $scope.SizeID = res.data.listSize[0].SizeID;
            $scope.ListStone = res.data.listStone;
            $scope.ListSugar = res.data.listSugar;
            $scope.ListTopping = res.data.listTopping;

        }, function myError(res) {

        });
    };

    // Get Related product
    $scope.GetRelatedProduct = function () {
        debugger
        var data = {
            Id: content,
        }
        var response = $http({
            url: "/Home/RelatedProduct",
            method: "POST",
            data: JSON.stringify(data),
            dataType: "json"
        })
        response.then(function mySuccess(res) {
            $scope.ListRelatedProduct = res.data.result;
            console.log($scope.ListProduct);
        }, function myError(res) {
        });
    };
};

