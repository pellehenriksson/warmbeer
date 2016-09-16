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
        vm.items = [];
        vm.places = [];
        vm.center = {
            lat: 56.90410,
            lon: 14.83115,
            zoom: 12,
            projection: "EPSG:4326"
        };
   
        var locationStyle = {
            image: {
                icon: {
                    anchor: [0.5, 1],
                    anchorXUnits: 'fraction',
                    anchorYUnits: 'fraction',
                    src: 'content/images/Map-Marker-Ball-Pink.png'
                }
            }
        };

        vm.settings = [
            { id: 1, name: "Give me the best prices" },
            { id: 2, name: "Give me a really bad hangover" }
        ];

        vm.selectedSetting = vm.settings[0];
        vm.getData = getSuggestions;

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
        }

        function getSuggestions() {

            console.log("ran!");

            var alcohol = vm.selectedSetting.id === 2;

            $http.get("/api/items/suggestions?longitude=" + vm.currentLocation.longitude + "&latitude=" + vm.currentLocation.latitude + "&highestAlcohol=" + alcohol)
                .then(function(response) {

                    vm.items = response.data.items;

                    // render stores
                    response.data.stores.map(function (store) {
                        vm.places.push({
                            lat: store.latitude,
                            lon: store.longitude,
                            label: {
                                message: store.info,
                                show: true
                            }
                        });
                    });

                    // render current position
                    vm.places.push({
                        lat: vm.currentLocation.latitude,
                        lon: vm.currentLocation.longitude,
                        label: {
                            message: "You are here",
                            show: false
                        },
                        style: locationStyle
                    });

                });
        }
    }
})();