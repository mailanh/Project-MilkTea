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
            method: 'GET'
        }).then(function mySuccess(res) {
            console.log(res)

            //chi tiết đơn hàng và sản phẩm
            //$scope.ListCTDH = res.data.result[0].CHITIETDONBAN;

            ////thông tin khách hàng
            //$scope.TenKH = res.data.result[0].KHACHHANG.TenKH;
            //$scope.SDT = 0 + "" + res.data.result[0].KHACHHANG.SDT;
            //$scope.DiaChi = res.data.result[0].KHACHHANG.DiaChi + " - " + res.data.result[0].KHACHHANG.HUYEN.TenHuyen + " - " + res.data.result[0].KHACHHANG.THANHPHO.TenThanhPho;

            ////thông tin đơn hàng
            //$scope.MaDB = res.data.result[0].MaDB;
            //$scope.NgayDat = res.data.result[0].NgayDat + "-" + res.data.result[0].ThangDat + "-" + res.data.result[0].NamDat + " " + res.data.result[0].GioDat;
            //if (res.data.result[0].PhuongThuc == 0) {
            //    $scope.PhuongThuc = "Thanh toán tiền mặt";
            //    $scope.Phiship = res.data.result[0].PhiShip;
            //}
            //else {
            //    $scope.PhuongThuc = "Thanh toán qua thẻ";
            //}
            //if (res.data.result[0].TrangThaiThanhToan == true) {
            //    $scope.TrangThaiThanhToan = "Đã thanh toán";
            //}
            //else {
            //    $scope.TrangThaiThanhToan = "Chưa thanh toán";
            //}
            //$scope.TrangThaiDonHang = "Chờ xử lý";
            //$scope.TongTien = res.data.result[0].TongTien;
        }, function myError(res) {
        });
    }
};