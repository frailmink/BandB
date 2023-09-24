extends Node2D

var moveSpeed = 500

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	var inputVector = Vector2.ZERO
	inputVector.x = Input.get_action_strength("Gamepad_Right") - Input.get_action_strength("Gamepad_Left")
	inputVector.y = Input.get_action_strength("ui_down") - Input.get_action_strength("ui_up")
	
	inputVector = inputVector
	
	var velocity = inputVector * moveSpeed * delta
	position += velocity
