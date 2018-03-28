app.controller('HomeController', ['$scope', 'Page', 'sounds', function($scope, Page, sounds) {
    $scope.sounds = [];
    sounds.success(function(data) {
        $scope.sounds = data; //store the data in $scope.example
    });
    
    Page.setTitle("Flying Parrot");
}]);