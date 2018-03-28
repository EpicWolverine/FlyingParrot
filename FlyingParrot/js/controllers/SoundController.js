app.controller('SoundController', ['$scope', 'Page', 'sounds', '$routeParams', '$location', function($scope, Page, sounds, $routeParams, $location) {
     sounds.success(function(data) {
        var redirect = true;
        var looping = true;
        for(var cat in data.cats){
            for(var sound in data.cats[cat].sounds){
                if(data.cats[cat].sounds[sound].src.toLowerCase() == $routeParams.filename.toLowerCase()){
                    $scope.soundDetail = data.cats[cat].sounds[sound];
                    redirect = false;
                    looping = false;
                    Page.setTitle($scope.soundDetail.text + " - Parrot");
                    break;
                }
            }
            for(var subcat in data.cats[cat].subcats){
                for(var sound in data.cats[cat].subcats[subcat].sounds){
                    if(data.cats[cat].subcats[subcat].sounds[sound].src.toLowerCase() == $routeParams.filename.toLowerCase()){
                        $scope.soundDetail = data.cats[cat].subcats[subcat].sounds[sound];
                        redirect = false;
                        looping = false;
                        Page.setTitle($scope.soundDetail.text + " - Parrot");
                        break;
                    }
                }
                if(!looping) break;
            }
            if(!looping) break;
        }
        if(redirect){
            $location.path("/").replace();
        }
    });
}]);