app.factory('sounds', ['$http', function($http) { 
  return $http.get('./sounds.json')
            .success(function(data) { 
              return data; 
            }) 
            .error(function(err) { 
              return err; 
            }); 
}]);