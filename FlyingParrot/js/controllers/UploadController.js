app.controller('UploadController', ['$scope', 'Page', '$http', '$location', function ($scope, Page, $http, $location) {
	//$http.get('/api/Category/AllSounds')
	//	.success(function (data) {
	//		$scope.cats = data;
	//		//return data;
	//	})
	//	.error(function (err) {
	//		//return err;
	//	}); 
	
	Page.setTitle("Flying Parrot");

	$scope.upload = function () {
		var f = document.getElementById('file').files[0],
			r = new FileReader();

		r.onloadend = function (e) {
			var metadata = {
				"text": $scope.displayText.toString(),
				"uploader": $scope.uploader,
				"categoryName": $scope.category,
				"filename": Date.now().toString()
			};
			var data = e.target.result;
			//send your binary data via $http or $resource or do anything else with it
			$http({
				url: "/api/Sound",
				method: "POST",
				data: metadata,
				headers: { 'Content-Type': "application/json" }
			}).success(function (response) {
				$http({
					url: "/api/Sound/Upload?input=" + metadata.filename,
					method: "POST",
					data: data,
					headers: { 'Content-Type': "audio/mpeg" }
				}).success(function (response) {
					$location.path("/sound/" + response).replace();
				}).error(function (response) {
					alert("Failed to upload file.\n" + response);
				});
			}).error(function (response) {
				alert("Failed to send metadata.\n" + response);
			});
		}

		r.readAsBinaryString(f);
	}
}]);