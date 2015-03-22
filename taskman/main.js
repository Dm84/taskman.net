requirejs(["app"], function(app) {
	requirejs(["bootstrap"], function () {
		$(function () {
			app.start();	
		});
	});		
});

requirejs.config({
	baseUrl: '/Content/',
	paths: {
		jquery: 'http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery',
		jquery_ui: 'js/libs/jquery-ui',
		backbone: 'http://cdnjs.cloudflare.com/ajax/libs/backbone.js/1.1.2/backbone',
		handlebars: 'http://cdnjs.cloudflare.com/ajax/libs/handlebars.js/2.0.0/handlebars',
		underscore: 'http://cdnjs.cloudflare.com/ajax/libs/underscore.js/1.7.0/underscore',
		chart: 'http://cdnjs.cloudflare.com/ajax/libs/Chart.js/1.0.1-beta.2/Chart',		
		bootstrap: 'js/libs/bootstrap',
		marionette: 'http://cdnjs.cloudflare.com/ajax/libs/backbone.marionette/2.2.2/backbone.marionette'

	}
});