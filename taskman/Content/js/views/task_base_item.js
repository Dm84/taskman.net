define(['jquery', 'marionette'], function ($, Marionette) {
	
	var TaskItemView = Marionette.ItemView.extend({
		onRender: function () {
			var now = new Date(), nowMs = now.getTime(), 
				deadlineMs = this.model.get('deadline'), 
				deadlineDate = new Date(deadlineMs),
				overdue = nowMs - deadlineMs;					
			
			var $dateEl = $('.task-date', this.$el);

			switch (this.model.state) {
				case 'completed': $dateEl.addClass('task-date_status_completed'); break;
				case 'overdue': $dateEl.addClass('task-date_status_overdue'); break;
				case 'today': $dateEl.addClass('task-date_status_todo'); break;
				case 'todo': $dateEl.addClass('task-date_status_future'); break;
			}			
		},

	});
	
	return TaskItemView;	
})