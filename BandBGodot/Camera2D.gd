extends Camera2D

var zoom_speed: float = 0.1
var zoom_min: float = 0.5
var zoom_max: float = 2.0
var smoothing: float = 0.1
var targets: Array = []


func add_target(t):
	targets.append(t)
	
func _process(delta):
	#var new_rect = Rect2(Vector2(0, 0), camera.viewport_size) # Create a new rectangle starting from the top-left corner of the screen
	#viewport_rect = new_rect # Apply the new rectangle to the camera's viewport
	var mid = Vector2.ZERO
	#var count = 0
	#var midpoint = (target1.global_position + target2.global_position) / 2
	#var distance = target1.global_position.distance_to(target2.global_position)
	if targets.size() == 0:
		zoom = Vector2(0.9,0.9) #0.56
		global_position = Vector2(-999,0)
	else:
		for target in targets:
			#count += 1
			
			mid += target.global_position
			
		var midpoint = mid / targets.size()
		
		var max_distance = 0.0
		for target1 in targets:
			for target2 in targets:
				if target1 != target2:
					var distance = target1.global_position.distance_to(target2.global_position)
					max_distance = max(max_distance, distance)
		#var desired_zoom = clamp(1000 / distance, zoom_min, zoom_max)
		var desired_zoom = clamp(800/max_distance, zoom_min, zoom_max)
		zoom.x = lerp(zoom.x, desired_zoom, zoom_speed)
		zoom.y = lerp(zoom.y, desired_zoom, zoom_speed)
		
		global_position = global_position.lerp(midpoint, smoothing)

func Delete_Target(n):
	for target in targets:
		if target.name == n:
			targets.erase(target)
	if targets.size() == 0:
		#var current_scene = get_tree().get_current_scene()
		#get_tree().reload_current_scene()
		get_node("/root/Game").playersDead = true
	print_debug(targets)
