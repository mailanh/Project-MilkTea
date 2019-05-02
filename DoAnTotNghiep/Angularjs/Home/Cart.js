///// <reference path="../../../scripts/angular.min.js" />

var app = angular.module('myApp', []);

app.controller('myController', function ($scope, $http, $rootScope) {

    $scope.Data = {};
    $scope.Login = true;

    $scope.init = function () {
        //các hàm dùng chung cho các giao diện
        $scope.AddToCart();
        $scope.GetALLLoaiSP();
        $scope.GetListShopCart();
    }

    

    ////Add to cart
    //$scope.AddToCart = function () {
    //    // Validate input quantity


    //    $scope.CheckQuantity() == true;

    //    $('#product_view').modal('hide');
    //    swal("Thành công!", "Thêm sản phẩm thành công vào giỏ hàng.", "success");
    //};

});