var app = angular.module('flying_parrot',['ngRoute']);

app.config(function ($routeProvider) {
	$routeProvider
		.when('/', {                        //when the url is this,
			controller: 'HomeController',   //use this controller
			templateUrl: './views/home.html'  //and this template for ng-view
		})
		.when('/cat/:cat', {
			controller: 'CatController',
			templateUrl: './views/cat.html'
		})
		.when('/sound/:id', {
			controller: 'SoundController',
			templateUrl: './views/sound.html'
		})
		.when('/upload', {
			controller: 'UploadController',
			templateUrl: './views/upload.html'
		})
		.otherwise({
			redirectTo: '/'
		});
});