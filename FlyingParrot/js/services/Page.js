app.factory('Page', function() {
   var title = 'Flying Parrot';
   return {
     title: function() { return title; },
     setTitle: function(newTitle) { title = newTitle }
   };
});