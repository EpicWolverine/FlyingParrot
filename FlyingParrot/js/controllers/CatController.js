app.controller('CatController', ['$scope', 'Page', '$http', '$routeParams', '$location', function ($scope, Page, $http, $routeParams, $location) {
	$http.get('/api/Sounds?category=' + $routeParams.cat)
		.success(function (data) {
			var redirect = true;
			$scope.catDetail = data
			redirect = false;
			Page.setTitle($scope.catDetail.name + " - Flying Parrot");
			if($routeParams.subcat !== null){
				var subredirect = true;
				for(var subcat in $scope.catDetail.subcats){
					if($scope.catDetail.subcats[subcat].name.toLowerCase() == $routeParams.subcat.toLowerCase()){
						$scope.subcatDetail = $scope.catDetail.subcats[subcat];
						subredirect = false;
						Page.setTitle($scope.catDetail.name + " > " + $scope.subcatDetail.name + " - Flying Parrot");
					}
				}
				if(subredirect){
					$location.path("/cat/" + $routeParams.cat).replace();
				}
			}
			if(redirect){
				$location.path("/").replace();
			}
		})
		.error(function (err) {
			//return err;
		}); 
}]);