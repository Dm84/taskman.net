define(	['jquery', 'backbone', 'marionette', 'handlebars', 'jquery_ui', 
       	 'js/collections/tasks', 'js/views/task_list', 'js/views/header'], 
       	 function (	$, Backbone, Marionette, Handlebars, jquery_ui, 
       			 	TaskCollection, TaskListView, HeaderView)
{
	Marionette.TemplateCache.prototype.compileTemplate = function (rawTemplate) {
	    return Handlebars.compile(rawTemplate);
	};
	
	Handlebars.registerHelper('dateFormat', function(date) {
		
		function pad(n, width, z) {
			  z = z || '0';
			  n = n + '';
			  return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
		}
		
		var date = new Date(date);
		return	pad(date.getDate(), 2) + "." + pad(date.getMonth() + 1, 2) + "." + 
				date.getFullYear() + " " + 
				pad(date.getHours(), 2) + ":" + pad(date.getMinutes(), 2);
	});
	

	var app = new Backbone.Marionette.Application();		
	
	app.endpointUrl = location.host === 'localhost:8080' ? '/artifact/endpoint/tasks' :
		'/endpoint/tasks'; 
	
	app.addRegions({tasksRegion: "#task-list", headerRegion: '#header'});
	
	app.taskList = new TaskCollection([], {url: app.endpointUrl});	
	
	app.searchTaskList = new TaskCollection([], {url: app.endpointUrl});
	app.listView = new TaskListView({ collection: app.taskList });
	
	app.headerView = new HeaderView({ 
		collection: app.searchTaskList, 
		mainCollection: app.taskList
	});
	
	app.on("start", function () {					
			
		app.taskList.fetch();					
		app.tasksRegion.show(app.listView);
		app.headerRegion.show(app.headerView);
	});		
	
	return app;
});