extends CharacterBody2D

# test comment

const SPEED = 300.0
const JUMP_VELOCITY = -500.0
var CanDoubleJump : bool = true
const Wall_Jump_PushBack = 1000
const Wall_Sliding_Speed = 5
const LERP_SPEED = 1000
var VEL = Vector2()
# Get the gravity from the project settings to be synced with RigidBody nodes.
var gravity = 1500 #ProjectSettings.get_setting("physics/2d/default_gravity")
@onready var anim = get_node("AnimatedSprite2D")

func _ready():
	anim.play("Idle")


func _physics_process(delta):
	# Add the gravity.
	if not is_on_floor():
		if is_on_wall() and velocity.y > 0:
			velocity.y += Wall_Sliding_Speed * delta
			CanDoubleJump = true
		else:
			velocity.y += gravity * delta
	else:
		CanDoubleJump = true

#	if is_on_wall():
#		if velocity.y < 0:
#			velocity.y += Wall_Sliding_Speed * delta
#		if Input.is_action_just_pressed("Keyboard_Jump"):
#			velocity.y = JUMP_VELOCITY
#			velocity.x = Wall_Jump_PushBack
	# Get the input direction and handle the movement/deceleration.
	# As good practice, you should replace UI actions with custom gameplay actions.
	var direction = Input.get_axis("Keyboard_Left", "Keyboard_Right")
	if direction:
		if velocity.x * direction < SPEED:
			if direction == -1:
				$AnimatedSprite2D.flip_h = true 
			elif direction == 1:
				$AnimatedSprite2D.flip_h = false 
			anim.play("Move")
			VEL = lerp(velocity, Vector2(LERP_SPEED * direction, 0), 0.42)
			velocity.x = VEL.x
		#velocity.x = direction * SPEED
	else:
		anim.play("Idle")
		velocity.x = move_toward(velocity.x, 0, SPEED)
		
	# Handle Jump.
	if Input.is_action_just_pressed("Keyboard_Jump"):
		if is_on_floor():
			velocity.y = JUMP_VELOCITY
		elif is_on_wall():
			velocity.y = JUMP_VELOCITY
			velocity.x += Wall_Jump_PushBack * -direction
		elif CanDoubleJump == true:
			velocity.y = JUMP_VELOCITY
			CanDoubleJump = false
	move_and_slide()

func die():
	print_debug("YOU DIE")
	queue_free()
	var rocket = preload("res://rocket.tscn").instantiate()
	rocket.SetInputs("Keyboard_Left", "Keyboard_Right", "Click_Mouse")
	get_node("/root/Game/Map1").add_child(rocket)
	rocket.set_global_position(global_position)
	get_node("/root/Game/Map1/Camera2D").Delete_Target(name)
	
