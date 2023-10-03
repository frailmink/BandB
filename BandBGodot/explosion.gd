extends Area2D

var timer = 0
var push = 1000

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	timer += delta
	if timer >= 10:
		print_debug("explosion ended")
		queue_free()


func _on_body_entered(body):
	print_debug("hit something")
	if body.has_method("die"):
		var direction = (body.global_position - global_position).normalized()
		var impulse = direction * push
		body.velocity += impulse
#		var angle = get_angle_to(body.global_position)
#		if global_position - body.global_position <= Vector2.ZERO:
#			body.velocity.x = -(sin(angle) * push)
#		else:
#			body.velocity.x = sin(angle) * push
#
#		body.velocity.y = cos(angle) * push
#		print_debug(sin(angle) * push)
#		print_debug(-(cos(angle) * push))
