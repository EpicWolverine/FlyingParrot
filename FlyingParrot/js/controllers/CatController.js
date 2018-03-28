app.controller('CatController', ['$scope', 'Page', 'sounds', '$routeParams', '$location', function($scope, Page, sounds, $routeParams, $location) {
     sounds.success(function(data) {
        var redirect = true;
        for(var cat in data.cats){
            if(data.cats[cat].name.toLowerCase() == $routeParams.cat.toLowerCase()){
                $scope.catDetail = data.cats[cat];
                redirect = false;
                Page.setTitle($scope.catDetail.name + " - Parrot");
                if($routeParams.subcat !== null){
                    var subredirect = true;
                    for(var subcat in $scope.catDetail.subcats){
                        if($scope.catDetail.subcats[subcat].name.toLowerCase() == $routeParams.subcat.toLowerCase()){
                            $scope.subcatDetail = $scope.catDetail.subcats[subcat];
                            subredirect = false;
                            Page.setTitle($scope.catDetail.name + " > " + $scope.subcatDetail.name + " - Parrot");
                        }
                    }
                    if(subredirect){
                        $location.path("/cat/" + $routeParams.cat).replace();
                    }
                }
            }
        }
        if(redirect){
            $location.path("/").replace();
        }
    });
}]);