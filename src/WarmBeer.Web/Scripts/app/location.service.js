(function() {
    "use strict";

    angular.module("app")
        .factory("locationService", LocationService);

    LocationService.$inject = ["$http"];

    function LocationService($http) {
     
        var service = {
            getName: getName
        }

        return service;

        function getName(longitude, latitude) {

            return $http.get("/api/location/name?longitude=" + longitude + "&latitude=" + latitude)
                .then(function (response) {
                    return response.data;
                });
        }
    }

})();
