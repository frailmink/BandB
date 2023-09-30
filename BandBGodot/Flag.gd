extends Sprite2D


# Called when the node enters the scene tree for the first time.
func _ready():
	$AnimationPlayer.play("FlagMove")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass


func _on_EndPoint_body_entered(body):
	body.die()
	if body.name == "Player1":  # Assuming your player's name is "Player"
		print_debug("Player1 Finished")
	if body.name == "Player2":
		print_debug("Player2 Finished")
