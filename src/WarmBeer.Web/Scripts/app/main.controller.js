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

        vm.places = [];

        vm.center = {
            lat: 56.90410,
            lon: 14.83115,
            zoom: 12,
            projection: "EPSG:4326"
        };
   
        activate();

        function activate() {
            getLocation();
        }

        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    vm.currentLocation = position.coords;

                    getSuggestions();
                    getLocationName();
                    centerMapOnLocation();
                    
                    // set marker on current location
                });
            }
        }

        function getLocationName() {
            $http.get("/api/location/name?longitude=" + vm.currentLocation.longitude + "&latitude=" + vm.currentLocation.latitude)
                .then(function (response) {
                    vm.currentLocationName = response.data.name;
                });
        }

        function centerMapOnLocation() {
            vm.center.lat = vm.currentLocation.latitude;
            vm.center.lon = vm.currentLocation.longitude;

            vm.places.push({
                lat: vm.currentLocation.latitude,
                lon: vm.currentLocation.longitude,
                label: {
                    message: "You are here",
                    show: true
                }
            });
        }

        function getSuggestions() {
            $http.get("/api/items/suggestions?longitude=" + vm.currentLocation.longitude + "&latitude=" + vm.currentLocation.latitude)
                .then(function(response) {
                    vm.stores = response.data.stores;
                    vm.items = response.data.items;

                    vm.stores.map(function (store) {
                        vm.places.push({
                            lat: store.latitude,
                            lon: store.longitude,
                            label: {
                                message: store.info,
                                show: true
                            }
                        });
                    });
                });
        }
    }
})();