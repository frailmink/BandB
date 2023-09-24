extends Node2D

var moveSpeed = 500

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	var inputVector = Vector2.ZERO

	inputVector.x = Input.get_action_strength("Cursor_Right") - Input.get_action_strength("Cursor_Left")
	inputVector.y = Input.get_action_strength("Cursor_Down") - Input.get_action_strength("Cursor_Up")

	var velocity = inputVector * moveSpeed * delta
	position += velocity
