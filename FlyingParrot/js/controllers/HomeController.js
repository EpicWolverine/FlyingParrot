app.controller('HomeController', ['$scope', 'Page', '$http', function ($scope, Page, $http) {
	//$scope.sounds = [];
	//sounds.success(function(data) {
	//    $scope.sounds = data; //store the data in $scope.example
	//});
	$http.get('/api/Category/AllSounds')
		.success(function (data) {
			$scope.cats = data;
			//return data;
		})
		.error(function (err) {
			//return err;
		}); 
	
	Page.setTitle("Flying Parrot");
}]);