app.controller('UploadController', ['$scope', 'Page', '$http', '$location', function ($scope, Page, $http, $location) {
	Page.setTitle("Upload - Flying Parrot");

	$scope.upload = function () {
		var f = document.getElementById('file').files[0];
		var metadata = {
			"text": $scope.displayText.toString(),
			"uploader": $scope.uploader,
			"categoryName": $scope.category,
			"filename": Date.now().toString()
		};
		$http.post("/api/Sound", metadata, { 'Content-Type': "application/json" })
			.then(function successCallback(response) {
				var config = {
					headers: { "Content-Type": undefined },
					params: {
						filename: f.name,
						size: f.size,
						type: f.type
					}
				};
				$http.post("/api/Sound/Upload?input=" + metadata.filename, f, config)
					.then(function successCallback(response) {
						$location.path("/sound/" + response).replace();
					}, function errorCallback(response) {
						alert("Failed to upload file.\n" + response);
					});
			}, function errorCallback(response) {
				alert("Failed to send metadata.\n" + response);
			});
	};
}]);