define(['jquery', 'underscore', 'js/views/task_base_item'], function ($, _, TaskItemView) {
	
	var MainTaskItemView = TaskItemView.extend({
		template: '#task-item-template',
		tagName: 'div class="task-item"',
		
		initialize: function () {
			this.listenTo(this.model, "change", this.render);

			this.model.bind("request", this.onRequest, this);
			this.model.bind("sync", this.onSync, this);
		},

		onRequest: function () {
			this.$el.append('<div class="loader"></div>');
		},

		onSync: function () {
			this.$el.find('.loader').remove();
		},
		
		onRender: function () {
			TaskItemView.prototype.onRender.call(this);
			this.$el.attr('id', 'task-' + this.model.id);				
			
			if (this.model.get('completed')) {
				this.$el.find('.task-completed-icon').
					addClass('task-completed-icon_status_true');
				this.$el.find('.task-date').removeClass('task-date_status_todo task-date_status_future task-date_status_overdue').
					addClass('task-date_status_completed');
			} else {
				this.$el.find('.task-completed-icon').addClass('task-completed-icon_status_false');
			}
		},
		
		events: {
			'click .task-completed-icon_status_false': function () {

				if (this.model.state !== 'completed') {
					//this.$el.find('.task-completed-icon').addClass('task-completed-icon_status_wait');
					this.model.save({ completed: true }, {
						wait: true, patch: true, error: _.bind(function () {
							$('.loader').remove();
						}, this)
					});
				}
			}
		}
	});

	return MainTaskItemView;
});