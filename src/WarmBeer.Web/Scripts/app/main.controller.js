(function() {
    "use strict";

    angular
        .module("app")
        .controller("MainController", MainController);

    MainController.$inject = ["locationService", "suggestionsService"];

    function MainController(locationService, suggestionsService) {

        var vm = this;
        vm.currentLocationName = "";
        vm.loading = true;
        vm.currentLocation = {};
        vm.items = [];
        vm.places = [];
        vm.center = {
            lat: 0,
            lon: 0,
            zoom: 12,
            projection: "EPSG:4326"
        };

        vm.settings = [
            { id: 1, name: "Give me the cheapest possible" },
            { id: 2, name: "Give me the strongest stuff" },
            { id: 3, name: "Give me the most expensive stuff" }
        ];

        vm.selectedSetting = vm.settings[0];
        vm.getData = getSuggestions;

        activate();

        function activate() {
            getLocation();
        }

        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function(position) {
                    vm.currentLocation = position.coords;
                    getSuggestions();
                    getLocationName();
                    centerMapOnLocation();
                });
            }
        }

        function getLocationName() {
            locationService.getName(vm.currentLocation.longitude, vm.currentLocation.latitude)
               .then(function (data) {
                    vm.currentLocationName = data.name;
                    vm.loadingLocationName = false;
               });
        }

        function centerMapOnLocation() {

            vm.center.lat = vm.currentLocation.latitude;
            vm.center.lon = vm.currentLocation.longitude;
        }

        function getSuggestions() {
            suggestionsService.getItems(vm.currentLocation.longitude, vm.currentLocation.latitude, vm.selectedSetting.id)
                .then(function(data) {
                    vm.places = data.places;
                    vm.items = data.items;
                    vm.loading = false;
                });
        }
    }
})();