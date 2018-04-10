app.controller('SoundController', ['$scope', 'Page', '$http', '$routeParams', '$location', function ($scope, Page, $http, $routeParams, $location) {
	$http.get('/api/Sound?id=' + $routeParams.id)
		.success(function (data) {
			$scope.soundDetail = data;
			if ($scope.soundDetail === null) {
				$location.path("/").replace();
			}
			Page.setTitle($scope.catDetail.name + " - Flying Parrot");
		})
		.error(function (err) {
			$location.path("/").replace();
			//return err;
		}); 
}]);