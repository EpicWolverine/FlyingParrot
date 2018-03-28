app.directive('soundButton', function() { 
  return { 
    restrict: 'E', //create new element
    scope: { 
      sound: '=', //can pass in stuff with info="" attribute
    }, 
    templateUrl: 'js/directives/soundButton.html', //the template for the directive
		link: function($scope){
			$scope.playSound = function(filename) {
				console.log("Playing " + filename);
				var soundObj = document.getElementById('sound');
				soundObj.pause(); 
				soundObj.src = './sounds/' + filename + '.mp3';
				soundObj.play();
			};
		}
  }; 
});