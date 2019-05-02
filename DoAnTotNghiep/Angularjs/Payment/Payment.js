
var app = angular.module('myApp', []);

app.controller('myController', myController);

myController.$inject = ['$scope', '$http'];

function myController($scope, $http) {
    $scope.Data = {};
    // defined function when start project
    $scope.init = function () {
        $scope.GetListCart();
    }



    /*---------------------------------------------C A R T - I T E M---------------------------------------------*/
    // Get All List Cart
    $scope.GetListCart = function () {
        $scope.CountCart = 0;
        $scope.TotalCart = 0;
        $scope.TotalQuantity = 0;
        $scope.BanCheckout = "";
        $http({
            url: "/Cart/GetAllCart",
            method: 'GET'
        }).then(function mySuccess(res) {
            console.log(res)
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

    /*---------------------------------------------E N D - C A R T---------------------------------------------*/

    /*---------------------------------------------O R D E R------------------------------------------------*/
    $scope.Order = function () {
        if ($scope.Data.Name == null || $scope.Data.Name == "") {
            $scope.ChangeName();
            return
            //$scope.ErrorName = "Vui lòng nhập họ & tên";
            //return
        }
        else if ($scope.Data.Gender == null || $scope.Data.Gender == "") {
            $scope.ChangeGender();
            //$scope.ErrorGender = "Vui lòng nhập giới tính";
            return
        }
        else if ($scope.Data.DateOfBirth == null || $scope.Data.DateOfBirth == "") {
            $scope.ChangeDateOfBirth();
            //$scope.ErrorDateOfBirth = "Vui lòng nhập ngày sinh";
            return
        }
        else if ($scope.Data.Phone == null || $scope.Data.Phone == "") {
            $scope.ChangePhone()
            //$scope.ErrorPhone = "Vui lòng nhập số điện thoại";
            return
        }
        else if ($scope.Data.Address == null || $scope.Data.Address == "") {
            $scope.ChangeAddress();
            //$scope.ErrorAddress = "Vui lòng nhập địa chỉ";
            return
        }
        else if ($scope.Data.Gmail == null || $scope.Data.Gmail == "") {
            $scope.ChangeGmail();
            //$scope.ErrorGmail = "Vui lòng nhập gmail";
            return
        }
        else {

            var response = $http({
                url: "/Payment/AddOrder",
                method: "POST",
                data: JSON.stringify($scope.Data),
                dataType: "json"
            });
            response.then(function (res) {
                debugger
                swal({
                    title: "Thành công !",
                    text: "Bạn đã gửi đơn hàng thành công!",
                    icon: "success",
                    buttons: true,
                    dangerMode: true,
                })
                    .then((isRedirect) => {
                        if (isRedirect) {
                            location.href = "/xac-nhan-don-hang-cua-ban"; 
                        } else {
                            location.href = "/Home/Index";
                        }
                    });
                //swal("Thành công!", "Bạn đã đặt hàng thành công", "success");

            }, function (res) {
                swal("Thất bại!", "Có lỗi xảy ra trên server.", "error");
            });
            //});

        }


    }

    // Validate form order
    $scope.ChangeName = function () {
        if ($scope.Data.Name == null || $scope.Data.Name == "") {
            $scope.ErrorName = "Vui lòng nhập họ & tên";
            return;
        }
        else {
            $scope.ErrorName = "";
        }
    }

    $scope.ChangeGender = function () {
        if ($scope.Data.Gender == null || $scope.Data.Gender == "") {
            $scope.ErrorGender = "Vui lòng nhập giới tính";
            return;
        }
        else {
            $scope.ErrorGender = "";
        }
    }

    $scope.ChangeDateOfBirth = function () {
        var regex_date = /^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$/;
        if ($scope.Data.DateOfBirth == null || $scope.Data.DateOfBirth == "") {
            $scope.ErrorDateOfBirth = "Vui lòng nhập ngày sinh";
            return;
        }
        else if (regex_date.test($scope.Data.DateOfBirth) == false) {
            $scope.ErrorDateOfBirth = "Sai định dạng ngày sinh";
            return;
        }
        else
        {
            $scope.ErrorDateOfBirth = "";
        }
    }

    $scope.ChangePhone = function () {

        var vnf_regex = /^[0-9]*$/;
        if ($scope.Data.Phone == null || $scope.Data.Phone == "") {
            $scope.ErrorPhone = "Vui lòng nhập số điện thoại";
            return;
        }
        else if (vnf_regex.test($scope.Data.Phone) == false) {
            $scope.ErrorPhone = "Số điện thoại không đúng định dạng";
            return;
        }
        else {
            $scope.ErrorPhone = "";
            $scope.Data.Phone = $scope.Data.Phone;
        }
    }

    $scope.ChangeAddress = function () {
        if ($scope.Data.Address != null || $scope.Data.Address == "") {
            $scope.ErrorAddress = "Vui lòng nhập địa chỉ";
            return
        } else {
            $scope.ErrorAddress = "";
        }

    }

    $scope.ChangeGmail = function () {

        var re = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if ($scope.Data.Gmail == null || $scope.Data.Gmail == "") {
            $scope.ErrorGmail = "Vui lòng nhập gmail";
            return;
        }

        else if (!re.test($scope.Data.Gmail)) {
            $scope.ErrorGmail = "Gmail sai định dạng";
            return;
        }
        else {
            $scope.ErrorGmail = "";
        }
    }
    /*---------------------------------------------E N D - O R D E R---------------------------------------------*/
};