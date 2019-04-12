/// <reference path="../../../scripts/angular.min.js" />

var app = angular.module('myApp', []);

app.controller('myCtrl', function ($scope, $http, $rootScope) {

    $scope.data = {};
    //Khởi tạo các hàm khi bắt đầu chạy giao diện
    $scope.init = function () {
        //Todo something...
    }

    $scope.btnLogin = function () {
        debugger
        function loadingOut(loading) {
            setTimeout(() => loading.out(), 1000);
        };

        var formData = new FormData();
        formData.append('UserName', $scope.UserName);
        formData.append('Password', $scope.Password);
        var request = {
            method: 'POST',
            url: "/Login/Login/",
            data: formData,
            headers: {
                'Content-Type': undefined
            }
        }
        
        $http(request)
            .then(function mySuccess(res) {
                console.log(res);
                if (res.data.messages.Success == false) {

                    var loading = new Loading();
                    loadingOut(loading);
                    setTimeout(() => {
                        $.notify(
                            {
                                //option
                                icon: 'fa fa-exclamation-triangle',
                                title: '<strong>Error: </strong>',
                                message: res.data.messages.Contents,
                            },
                            {
                                //setting
                                type: 'warning',

                                placement: {
                                    //from: "top",
                                    align: "center"
                                },
                                animate: {
                                    enter: 'animated bounceIn',
                                    exit: 'animated bounceOut'
                                }
                            },
                        );
                    }, 1500)


                }
                if (res.data.messages.Success == true) {
                    //$.notify({
                    //    // options <i class="fas fa-check-circle"></i>
                    //    icon: 'far fa-check-circle',
                    //    title: '<strong>Success: </strong><br>',
                    //    message: res.data.messages.Contents
                    //}, {
                    //        // settings
                    //        type: 'success',

                    //        placement: {
                    //            from: "top",
                    //            align: "right"
                    //        },
                    //        animate: {
                    //            enter: 'animated flipInY',
                    //            exit: 'animated flipOutX'
                    //        }
                    //    });
                    location.href = "/Admin/Home/Index";
                }
            }, function myError(res) {
                //$scope.load();
            });
    }

});