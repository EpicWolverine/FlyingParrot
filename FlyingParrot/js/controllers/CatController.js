app.controller('CatController', ['$scope', 'Page', '$http', '$routeParams', '$location', function ($scope, Page, $http, $routeParams, $location) {
	$http.get('/api/Category?id=' + $routeParams.cat)
		.success(function (data) {
			$scope.catDetail = data
			if ($scope.catDetail == null){
				$location.path("/").replace();
			}
			Page.setTitle($scope.catDetail.name + " - Flying Parrot");
		})
		.error(function (err) {
			$location.path("/").replace();
			//return err;
		}); 
}]);