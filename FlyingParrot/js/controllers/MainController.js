app.controller('MainController', ['$scope', 'Page', function($scope, Page) {
    $scope.Page = Page;
    Page.setTitle("Parrot");
}]);