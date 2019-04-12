/// <reference path="../../../scripts/angular.min.js" />

var app = angular.module('myApp', []);

app.controller('myController', myController);

myController.$inject = ['$scope', '$http'];

function myController($scope, $http) {

    // defined function when start project
    $scope.init = function () {
        $scope.LoadAllProduct();
        $scope.GetListCart();
        //$scope.CheckQuantity();

        //ToDo
    }
    // get all product where status == true

    $scope.LoadAllProduct = function () {
        $http({
            url: "/Home/LoadAllProduct",
            method: 'GET'
        }).then(function mySuccess(res) {
            $scope.ListProduct = res.data.query;
        }, function myError(res) {
        });
    }

    $scope.ProductDetail = function (item) {
        //$scope.soluong = 1;
        var Id = item;
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
            $scope.SizeID = $scope.GetDetail.SizeID;
            $scope.ListStone = res.data.listStone;
            $scope.ListSugar = res.data.listSugar;
            $scope.ListTopping = res.data.listTopping;

        }, function myError(res) {

        });
    };

    //lấy danh sách giỏ hàng
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
            //debugger
            if ($scope.ListShopCart == null || $scope.ListShopCart.length == 0) {
                $scope.CartNull = true;
                $scope.isNull = "Giỏ hàng của bạn hiện chưa có sản phẩm nào !";
                $scope.BanCheckout = "disabled";
            }
            else {
                //debugger
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
            $scope.message = "Số lượng đặt phải là số !";
            return
        }
        else if ($scope.quantity <= 0) {
            $scope.message = "Số lượng đặt phải lớn hơn không !";
            return
        }
        else if ($scope.quantity > 15) {
            $scope.message = "Chỉ có thể đặt tối đa 15 sản phẩm !";
            return
        } else {
            $scope.message = "";
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
                $('#product_view').modal('hide');
                swal("Thành công!", res.data.messages.Contents, "success");
                $scope.GetListCart();
            }
            else {
                $('#product_view').modal('hide');
                swal("Thất bại!", res.data.messages.Contents, "error");
                $scope.GetListCart();
            }
        }, function (res) {
            $('#product_view').modal('hide');
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

    $scope.EditQuantity = function (content, quantity, change) {

        switch (change) {
            case 'plus':
                quantity++;
                break;
            case 'minus':
                if (quantity > 1) {
                    quantity--;
                }
                break
        }
        var data = { productid: content, quantity: quantity }

        var response = $http({
            url: "/Cart/ChangeCartItem",
            method: "POST",
            data: JSON.stringify(data),
            dataType: "json"
        });
        response.then(function (res) {
            $scope.GetListCart();
        }, function (res) {
            appendtotoastr(false, "thông báo", "... lỗi rồi !");
        });
    }

    //$scope.EditCart = function (productID) {

    //    var data = {
    //        result: productID
    //    }
    //}

    $scope.EditCart = function (productID) {
        var data = {
            productID: productID
        };
        var response = $http({
            url: "/Cart/EditCart",
            method: "POST",
            data: JSON.stringify(data),
            dataType: "json"
        });
        response.then(function mySuccess(res) {
            $scope.GetDetail = res.data.editCart;
            $scope.ProductID = $scope.GetDetail.ProductID;
            $scope.ProductName = $scope.GetDetail.ProductName;
            $scope.Price = $scope.GetDetail.Price;
            $scope.MetaTitle = $scope.GetDetail.MetaTitle;
            $scope.SupplierName = $scope.GetDetail.SupplierName;
            $scope.Image = $scope.GetDetail.Image;
            $scope.CategoryName = $scope.GetDetail.CategoryName;
            $scope.Description = $scope.GetDetail.Description;
            $scope.SizeID = $scope.GetDetail.SizeID;
            $scope.AmountStoneName = $scope.GetDetail.AmountOfStone;
            $scope.AmountSugarName = $scope.GetDetail.AmountOfSugar;
            $scope.ToppingID = $scope.GetDetail.ToppingID;
            $scope.quantity = $scope.GetDetail.Quantity;

            $scope.ListSize = res.data.listSize;
            $scope.ListStone = res.data.listStone;
            $scope.ListSugar = res.data.listSugar;
            $scope.ListTopping = res.data.listTopping;
            //console.log($scope.ListProduct);
        }, function myError(res) {
        });
    }
};