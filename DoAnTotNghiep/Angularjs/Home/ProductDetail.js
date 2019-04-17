/// <reference path="../../../scripts/angular.min.js" />

var app = angular.module('myApp', []);

app.controller('myController', myController);

myController.$inject = ['$scope', '$http'];

function myController($scope, $http) {
    $scope.init = function () {
        $scope.ProductDetail();
        $scope.GetRelatedProduct();
        $scope.GetListCart();

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
            console.log($scope.ListRelatedProduct);
        }, function myError(res) {
        });
    };

    $scope.GetListCart = function () {
        $scope.CountCart = 0;
        $scope.TotalCart = 0;
        $scope.TotalQuantity = 0;
        $scope.BanCheckout = "";
        $http({
            url: "/Cart/GetAllCart",
            method: 'GET'
        }).then(function mySuccess(res) {
            //console.log(res)
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

    $scope.AddToCart = function () {
        if (isNaN($scope.quantity)) {
            $.notify({
                // options <i class="fas fa-check-circle"></i>
                icon: 'fa fa-exclamation-triangle',
                title: '<strong>Error: </strong><br>',
                message: "Số lượng đặt phải là số !"
            }, {
                    // settings
                    type: 'danger',

                    placement: {
                        from: "top",
                        align: "right"
                    },
                    animate: {
                        enter: 'animated flipInY',
                        exit: 'animated flipOutX'
                    }
                });
            return
        }
        else if ($scope.quantity <= 0) {
            $.notify({
                // options <i class="fas fa-check-circle"></i>
                icon: 'fa fa-exclamation-triangle',
                title: '<strong>Error: </strong><br>',
                message: "Số lượng đặt phải lớn hơn không !"
            }, {
                    // settings
                    type: 'danger',

                    placement: {
                        from: "top",
                        align: "right"
                    },
                    animate: {
                        enter: 'animated flipInY',
                        exit: 'animated flipOutX'
                    }
                });
            return
        }
        else if ($scope.quantity > 15) {
            $.notify({
                // options <i class="fas fa-check-circle"></i>
                icon: 'fa fa-exclamation-triangle',
                title: '<strong>Error: </strong><br>',
                message: "Chỉ có thể đặt tối đa 15 sản phẩm !"
            }, {
                    // settings
                    type: 'danger',

                    placement: {
                        from: "top",
                        align: "right"
                    },
                    animate: {
                        enter: 'animated flipInY',
                        exit: 'animated flipOutX'
                    }
                });
            return
        } else {
        }
        //debugger
        var data = {
            productID: $scope.ProductID,
            quantity: $scope.quantity,
            sizeID: $scope.SizeID,
            topping: $scope.ToppingID,
            amountOfStone: $scope.AmountStoneName,
            amountOfSugar: $scope.AmountSugarName
        }
        var response = $http({
            url: "/Cart/AddToCart",
            method: "POST",
            data: JSON.stringify(data),
            dataType: "json"
        });
        response.then(function (res) {
            console.log(res);
            if (res.data.messages.Success == true) {
                swal("Thành công!", res.data.messages.Contents, "success");
                $scope.GetListCart();
            }
            else {
                swal("Thất bại!", res.data.messages.Contents, "error");
                $scope.GetListCart();
            }
        }, function (res) {
            swal("Thất bại!", "Có lỗi xảy ra trên server.", "error");
            $scope.GetListCart();
        });
    }

    // Delete CartItem By ID
    $scope.DeleteCart = function (content) {

        var data = {
            productID: content,
        }
        var response = $http({
            url: "/Cart/DeleteCartItem",
            method: "POST",
            data: JSON.stringify(data),
            dataType: "json"
        });
        response.then(function (res) {
            console.log(res);
            if (res.data.messages.Success == true) {
                $.notify({
                    // options <i class="fas fa-check-circle"></i>
                    icon: 'far fa-check-circle',
                    title: '<strong>Success: </strong><br>',
                    message: res.data.messages.Contents
                }, {
                        // settings
                        type: 'success',

                        placement: {
                            from: "top",
                            align: "right"
                        },
                        animate: {
                            enter: 'animated flipInY',
                            exit: 'animated flipOutX'
                        }
                    });
                $scope.GetListCart();
            }
            else {
                $.notify({
                    // options <i class="fas fa-check-circle"></i>
                    icon: 'fa fa-exclamation-triangle',
                    title: '<strong>Error: </strong><br>',
                    message: res.data.messages.Contents
                }, {
                        // settings
                        type: 'danger',

                        placement: {
                            from: "top",
                            align: "right"
                        },
                        animate: {
                            enter: 'animated flipInY',
                            exit: 'animated flipOutX'
                        }
                    });
                $scope.GetListCart();

            }
        }, function (res) {
            AppendToToastr(false, "Thông báo", "... Lỗi rồi !");
        });
    }
};

