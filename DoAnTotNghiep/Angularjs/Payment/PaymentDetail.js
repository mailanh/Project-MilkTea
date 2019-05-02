var app = angular.module('myApp', []);

app.controller('myController', myController);

myController.$inject = ['$scope', '$http'];

function myController($scope, $http) {

    $scope.init = function () {
        $scope.GetListCart();
        $scope.PaymentDetail();
    }

    // Get all list cart
    $scope.GetListCart = function () {
        $scope.CountCart = 0;
        $scope.TotalCart = 0;
        $scope.TotalQuantity = 0;
        $scope.BanCheckout = "";
        $http({
            url: "/Cart/GetAllCart",
            method: 'GET'
        }).then(function mySuccess(res) {
            $scope.ListShopCart = res.data.ListCartItem;
            if ($scope.ListShopCart == null || $scope.ListShopCart.length == 0) {
                $scope.CartNull = true;
                $scope.isNull = "Giỏ hàng của bạn hiện chưa có sản phẩm nào !";
                $scope.BanCheckout = "disabled";
            }
            else {
                $scope.CartNull = false;
                $scope.BanCheckout = "";
                $scope.isNull = "";
                $scope.CountCart = $scope.ListShopCart.length;
                for (var i = 0; i < $scope.ListShopCart.length; i++) {
                    $scope.TotalCart += $scope.ListShopCart[i].Totals;
                    $scope.TotalQuantity += $scope.ListShopCart[i].Quantity;
                }
            }


        }, function myError(res) {
        });
    }

    $scope.PaymentDetail = function () {
        var ID = content;
        debugger
        $http({
            url: "/Payment/ViewOrderDetails/" + ID,
            method: 'GET',
            //dataType: "json"
        }).then(function mySuccess(res) {

            $scope.GetDetail = res.data.result[0];
            console.log($scope.GetDetail)
            $scope.OrderID = $scope.GetDetail.OrderID;
            $scope.OrderDate = moment($scope.GetDetail.OrderDate).format("DD-MM-YYYY");
            $scope.OrderDateLocate = moment($scope.GetDetail.OrderDate).fromNow();
            $scope.CustomerID = $scope.GetDetail.customerID;
            $scope.Total = $scope.GetDetail.Total;
            $scope.Total2 = $scope.GetDetail.Total + 15000;
            $scope.CustomerName = $scope.GetDetail.CustomerName;
            $scope.Phone = $scope.GetDetail.Phone;
            $scope.Gender = $scope.GetDetail.Gender;
            $scope.DateOfBirth = moment($scope.GetDetail.DateOfBirth).format("DD-MM-YYYY");
            $scope.Address = $scope.GetDetail.Address;
            $scope.Gmail = $scope.GetDetail.Gmail;

            if ($scope.GetDetail.Status == 0) {
                $scope.Status = "Chờ xác nhận";
                $scope.Class = "label label-warning";
            } else if ($scope.GetDetail.Status == 1) {
                $scope.Status = "Đang giao hàng";
                $scope.Class = "label label-info";
            } else {
                $scope.Status = "Đã thanh toán";
                $scope.Class = "label label-success";
            }
            
        }, function myError(res) {
        });
    }
};