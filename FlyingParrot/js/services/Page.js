app.factory('Page', function() {
   var title = 'Parrot';
   return {
     title: function() { return title; },
     setTitle: function(newTitle) { title = newTitle }
   };
});