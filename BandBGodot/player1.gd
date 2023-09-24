extends CharacterBody2D


const SPEED = 300.0
const JUMP_VELOCITY = -500.0
var CanDoubleJump : bool = true
const Wall_Jump_PushBack = 500
const Wall_Sliding_Speed = 5

# Get the gravity from the project settings to be synced with RigidBody nodes.
var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")


func _physics_process(delta):
	# Add the gravity.
	if not is_on_floor():
		velocity.y += gravity * delta
	else:
		CanDoubleJump = true

	if is_on_wall():
		velocity.y += Wall_Sliding_Speed * delta
		if Input.is_action_just_pressed("Keyboard_Jump"):
			velocity.y = JUMP_VELOCITY
			velocity.x = Wall_Jump_PushBack
		
	# Handle Jump.
	if Input.is_action_just_pressed("Keyboard_Jump"):
		if is_on_floor():
			velocity.y = JUMP_VELOCITY
		elif Input.is_action_just_pressed("Keyboard_Jump") and CanDoubleJump == true:
			velocity.y = JUMP_VELOCITY
			CanDoubleJump = false
	

	# Get the input direction and handle the movement/deceleration.
	# As good practice, you should replace UI actions with custom gameplay actions.
	var direction = Input.get_axis("Keyboard_Left", "Keyboard_Right")
	if direction:
		velocity.x = direction * SPEED
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)

	move_and_slide()
