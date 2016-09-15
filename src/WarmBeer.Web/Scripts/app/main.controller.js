(function() {
    "use strict";

    angular
        .module("app")
        .controller("MainController", MainController);

    MainController.$inject = ["$scope", "$http"];

    function MainController($scope, $http) {

        var vm = this;

        vm.currentLocationName = "";
        vm.currentLocation = {};
        vm.stores = [];
        vm.items = [];

        activate();

        function activate() {
            getLocation();
        }

        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    vm.currentLocation = position.coords;
                    getSuggestions();
                    // $scope.$apply();
                });
            }
        }

        function getSuggestions() {

            $http.get("/api/items/suggestions?longitude=" + vm.currentLocation.longitude + "&latitude=" + vm.currentLocation.latitude)
                .then(function(response) {
                    vm.stores = response.data.stores;
                    vm.items = response.data.items;
                });
        }
    }
})();