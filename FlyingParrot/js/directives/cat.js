app.directive('cat', function() { 
  return { 
    restrict: 'E', //create new element
    scope: { 
      cat: '=' //can pass in stuff with info="" attribute
    }, 
    templateUrl: 'js/directives/cat.html' //the template for the directive
  }; 
});