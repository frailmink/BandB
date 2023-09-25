extends Camera2D

var zoom_speed: float = 0.1
var zoom_min: float = 0.5
var zoom_max: float = 2.0
var smoothing: float = 0.1
var targets: Array = []


func add_target(t):
	targets.append(t)
	
func _process(delta):
	var mid = Vector2.ZERO
	#var count = 0
	#var midpoint = (target1.global_position + target2.global_position) / 2
	#var distance = target1.global_position.distance_to(target2.global_position)
	
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
	var desired_zoom = clamp(1000 / max_distance, zoom_min, zoom_max)
	zoom.x = lerp(zoom.x, desired_zoom, zoom_speed)
	zoom.y = lerp(zoom.y, desired_zoom, zoom_speed)
	
	global_position = global_position.lerp(midpoint, smoothing)

func Delete_Target(n):
	for target in targets:
		if target.name == n:
			targets.erase(target)
	if targets.size() == 0:
		var current_scene = get_tree().get_current_scene()
		get_tree().reload_current_scene()
	print_debug(targets)
