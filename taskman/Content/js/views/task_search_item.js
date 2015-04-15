define(['js/views/task_base_item'], function (TaskItemView) {
	
	var SearchTaskItemView = TaskItemView.extend({
		template: '#search-task-template',
		tagName: 'div class="task-item task-item_block_search"',
		hasSeparator: false,
		span: '',
		
		initialize: function (options) {
			this.hasSeparator = options.hasSeparator;
			this.span = options.span;								
		},
		
		events: {
			"click": function () {
				$('html, body').animate({
			        scrollTop: $("#task-" + this.model.id).offset().top
			    });
			}
		},
		
		onRender: function () {
			TaskItemView.prototype.onRender.call(this);
			
			if (this.hasSeparator) {					
				this.$el.addClass('task-item_separator_true');
			}
			
			if (this.span.length) {					
				var span = this.span;					
				this.$el.html(function(i, html) {						
					return html.replace(
							new RegExp(span, 'i'), '<span class=\"item-match\">$&</span>');
				});					
			}				
		}		
	});
	
	return SearchTaskItemView;
});