extends CharacterBody2D

var widthEllips = 90
var heightEllips = 45
var speed = 100
var leftInput = ""
var rightInput = ""
var spawn = ""
var spawned = false
var camera
var temp = Camera2D.new()
var timer = 0

func _ready():
	camera = get_node("/root/Game/Map1/Camera2D")
	camera.add_target(self)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
#	if spawned:
#		MoveRocket()
#	else:
#		CalcNotSpawnedPosition()
#		if Input.is_action_just_pressed("" + spawn):
#			spawned = true
	timer += delta
	MoveRocket()
	
func MoveRocket():
	if Input.is_action_pressed("" + leftInput):
		global_rotation -= 0.01
	if Input.is_action_pressed("" + rightInput):
		global_rotation += 0.01
	
	var rot = global_rotation
	velocity.x = sin(rot) * speed
	velocity.y = -(cos(rot) * speed)
	
	move_and_slide()
	
func SetInputs(left, right, spw):
	leftInput = left
	rightInput = right
	spawn = spw

func _on_area_2d_body_entered(body):
	if timer >= 0.5:
		print_debug("exploded")
		var explosion = preload("res://explosion.tscn").instantiate()
		get_node("/root/Game/Map1").add_child(explosion)
		explosion.global_position = global_position
		queue_free()
		get_node("/root/Game/Map1/Camera2D").Delete_Target(name)

#func CalcNotSpawnedPosition():
#	global_rotation = get_angle_to(camera.get_screen_center_position())
