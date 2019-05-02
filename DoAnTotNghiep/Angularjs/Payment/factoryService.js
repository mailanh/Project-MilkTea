(function () {
    'use strict';

    angular
        .module('app')
        .factory('factoryService', factoryService);

    factoryService.$inject = ['$http'];

    function factoryService($http) {
        var service = {
            getData: getData
        };

        return service;

        function getData() { }
    }
})();