(function() {
    "use strict";

    angular.module("app")
      .factory("suggestionsService", SuggestionsService);

    SuggestionsService.$inject = ["$http"];
    
    function SuggestionsService($http) {
        
        var service = {
            getItems: getItems
        }

        var locationStyle = {
            image: {
                icon: {
                    anchor: [0.5, 1],
                    anchorXUnits: "fraction",
                    anchorYUnits: "fraction",
                    src: "content/images/Map-Marker-Ball-Pink.png"
                }
            }
        };
    
        return service;
        
        function getItems(longitude, latitude, setting) {
            return $http.get("/api/items/suggestions?longitude=" + longitude + "&latitude=" + latitude + "&setting=" + setting)
               .then(function (response) {

                        var result = {
                            items: [],
                            places: []
                        };

                       result.items = response.data.items;
                   
                       response.data.stores.map(function (store) {
                           result.places.push({
                               lat: store.latitude,
                               lon: store.longitude,
                               label: {
                                   message: store.info,
                                   show: true
                               }
                           });
                       });

                       result.places.push({
                           lat: latitude,
                           lon: longitude,
                           label: {
                               message: "You are here",
                               show: false
                           },
                           style: locationStyle
                       });

                    return result;

                });
        }
    }

})();