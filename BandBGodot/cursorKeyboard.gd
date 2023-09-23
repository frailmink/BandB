extends Node2D
@onready var sprite2D = $Sprite2D
@onready var marker = $Sprite2D/Marker2D

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	global_position = get_global_mouse_position()
	

