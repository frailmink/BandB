extends CharacterBody2D

const SPEED = 400.0
const LERP_SPEED = 1000
const JUMP_VELOCITY = -500.0
var CanDoubleJump : bool = true
var gravoty = 1200 
var VEL = Vector2()
var SLPIPYYYY = 1



# Get the gravity from the project settings to be synced with RigidBody nodes.
var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")


func _physics_process(delta):
	# Add the gravity.
	if not is_on_floor():
		velocity.y += gravoty * delta
	else:
		CanDoubleJump = true
		
	# Handle Jump.
	if Input.is_action_just_pressed("Gamepad_Jump") and is_on_floor():
		velocity.y = JUMP_VELOCITY
	elif Input.is_action_just_pressed("Gamepad_Jump") and CanDoubleJump == true:
		velocity.y = JUMP_VELOCITY
		CanDoubleJump = false


	# Get the input direction and handle the movement/deceleration.
	# As good practice, you should replace UI actions with custom gameplay actions.
	var direction = Input.get_axis("Gamepad_Left", "Gamepad_Right")
	if direction:
		if velocity.x * direction < SPEED:
			VEL = lerp(velocity, Vector2(LERP_SPEED * direction, 0), 0.01)
			velocity.x = VEL.x
	else:
		velocity.x = move_toward(velocity.x, 0, 20)

	move_and_slide()
