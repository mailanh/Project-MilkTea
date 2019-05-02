///// <reference path="../../../scripts/angular.min.js" />

var app = angular.module('myApp', []);

app.controller('myController', myController);

myController.$inject = ['$scope', '$http'];

function myController($scope, $http) {
    $scope.Data = {};


    // defined function when start project
    $scope.init = function () {
        $scope.LoadAllProduct();
        $scope.GetListCart();
        //$scope.CheckQuantity();

        //ToDo
    }
    // get all product where status == true
    //$scope.LoadAllProduct = function () {
    //    $http({
    //        url: "/Home/LoadAllProduct",
    //        method: 'GET'
    //    }).then(function mySuccess(res) {
    //        $scope.ListProduct = res.data.query;
    //    }, function myError(res) {
    //    });
    //}
    $scope.LoadAllProduct = function () {
        var status = 2;
        var quantity = 0;
        $scope.Count = 0;
        var data = {
            quantity: quantity,
            status: status,
        }
        var response = $http({
            url: "/Home/LoadAllProduct",
            method: "POST",
            data: JSON.stringify(data),
            dataType: "json"
        });
        response.then(function mySuccess(res) {
            $scope.ListProduct = res.data.query;
            $scope.Count = res.data.query.length;
            $scope.ShowLoadMore = true;
            if ($scope.Count > 4) {
                $scope.ShowLoadMore = true;
            }
            else {
                $scope.HideLoadMore = false;
            }
        }, function myError(res) {
        });
    }

    $scope.ProductDetail = function (item) {
        //$scope.soluong = 1;
        $scope.isShowAdd = true;
        $scope.isShowEdit = false;
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

    // Validate quantity of cart items
    $scope.ChangQuantity = function () {
        var regex = /^[0-9]*$/;
        if ($scope.quantity == null || $scope.quantity == "") {
            $scope.message = "Vui lòng nhập số lượng";
            return;
        }
        else if (regex.test($scope.quantity) == false) {
            $scope.message = "Số lượng đặt phải là số";
            return;
        }
        else if ($scope.quantity <= 0) {
            $scope.message = "Số lượng đặt phải lớn hơn không !";
            return
        }
        else if ($scope.quantity > 15) {
            $scope.message = "Chỉ có thể đặt tối đa 15 sản phẩm !";
            return
        }
        else {
            $scope.message = "";
        }
    }

    // Add to cart items
    $scope.AddToCart = function () {
        var regex = /^[0-9]*$/;
        if ($scope.quantity == null || $scope.quantity == "") {
            $scope.message = "Vui lòng nhập số lượng";
            return;
        }
        else if (regex.test($scope.quantity) == false) {
            $scope.message = "Số lượng đặt phải là số";
            return;
        }
        else if ($scope.quantity <= 0) {
            $scope.message = "Số lượng đặt phải lớn hơn không !";
            return
        }
        else if ($scope.quantity > 15) {
            $scope.message = "Chỉ có thể đặt tối đa 15 sản phẩm !";
            return
        }
        else {
            $scope.message = "";
        }
        //debugger
        var data = {
            productID: $scope.ProductID,
            quantity: $scope.quantity,
            sizeID: $scope.SizeID,
            //topping: $scope.ToppingID,
            Topping1: $scope.ToppingID,
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

    // Change quantity of cart item
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
        var data = { index: content, quantity: quantity }

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

    // Get list cart on modal
    $scope.GetCartModal = function (item) {
        debugger
        $scope.isShowAdd = false;
        $scope.isShowEdit = true;

        var data = {
            ProductID: item.ProductID
        };
        var response = $http({
            url: "/Cart/GetCartModal",
            method: "POST",
            data: JSON.stringify(data),
            dataType: "json"
        });
        response.then(function mySuccess(res) {
            debugger
            $scope.ListSize = res.data.listSize;
            $scope.ListStone = res.data.listStone;
            $scope.ListSugar = res.data.listSugar;
            $scope.ListTopping = res.data.listTopping;

            //console.log(item)
            $scope.ProductID = item.ProductID;
            $scope.ProductName = item.ProductName;
            $scope.Price = item.Price;
            $scope.MetaTitle = item.MetaTitle;
            $scope.SupplierName = item.SupplierName;
            $scope.Image = item.Images;
            $scope.CategoryName = item.CategoryName;
            $scope.Description = item.Description;
            $scope.SizeID = item.SizeID;
            $scope.AmountStoneName = item.AmountOfStone;
            $scope.AmountSugarName = item.AmountOfSugar;
            $scope.quantity = item.Quantity;
            $scope.ToppingID = item.ToppingID1;
            $scope.index = item.index;

        }, function myError(res) {
        });

    }

    // Edit cart item
    $scope.EditCartItem = function () {
        $scope.isShowAdd = false;
        $scope.isShowEdit = true;
        $scope.test = false;
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
        debugger
        var data = {
            productID: $scope.ProductID,
            quantity: $scope.quantity,
            sizeID: $scope.SizeID,
            //topping: $scope.ToppingID,
            Topping1: $scope.ToppingID,
            amountOfStone: $scope.AmountStoneName,
            amountOfSugar: $scope.AmountSugarName,
            index: $scope.index,
        }
        var response = $http({
            url: "/Cart/EditCart",
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

    // Redirect from check out
    $scope.Redirect = function () {
        $http({
            url: "/Cart/GetAllCart",
            method: 'GET'
        }).then(function mySuccess(res) {

            $scope.ListShopCart = res.data.ListCartItem;
            if ($scope.ListShopCart.length != 0) {
                location.href = "/thanh-toan";
            }
        }, function myError(res) {
        });
    }
    /*---------------------------------------------E N D - C A R T---------------------------------------------*/

    $scope.ViewMore = function () {

        var quantity = $scope.Count + 4;
        var status = 1;
        var SoLe = quantity / 4;
        var data = {
            quantity: quantity,
            status: status,
        }
        var response = $http({
            url: "/Home/LoadAllProduct",
            method: "POST",
            data: JSON.stringify(data),
            dataType: "json"
        });
        response.then(function (res) {
            $scope.ListProduct = res.data.query;
            $scope.Count = $scope.ListProduct.length;
            if ($scope.Count > 4) {
                $scope.ShowLoadMore = true;
            }
            else {
                $scope.HideLoadMore = false;
            }
            if (res.data.messages.Success == true) {
                $scope.ShowLoadMore = false;
            }
            else {
                $scope.HideLoadMore = true;
            }
        }, function (res) {
            swal("Thông báo!", "Có lỗi xảy ra trên server", "error");
        });
    }

    $scope.HideMore = function () {
        debugger
        $scope.ShowLoadMore = true;
        var status = 0;
        var SoLe = $scope.Count / 4;
        var quantity = 0;
        if (SoLe % 2 == 0) {
            SoLe = 0;
            quantity = $scope.Count - 4;
        }
        else {
            SoLe = SoLe.toString().split(".");
            SoLe = SoLe[1];
            if (SoLe == 25) {
                quantity = $scope.Count - 1;
            }
            else if (SoLe == 5) {
                quantity = $scope.Count - 2;
            }
            else if (SoLe == 75) {
                quantity = $scope.Count - 3;
            }
        }

        var data = {
            quantity: quantity,
            status: status,
        }
        var response = $http({
            url: "/Home/LoadAllProduct",
            method: "POST",
            data: JSON.stringify(data),
            dataType: "json"
        });
        response.then(function (res) {
            $scope.ListProduct = res.data.query;
            $scope.Count = $scope.ListProduct.length;
            if ($scope.Count > 4) {
                $scope.HideLoadMore = true;
            }
            else {
                $scope.ShowLoadMore = false;
            }
        }, function (res) {
            swal("Thông báo!", "Có lỗi xảy ra trên server", "error");
        });
    }
};