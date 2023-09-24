extends Node2D
@onready var trapKeyboard = load("res://traps/spike.tscn").instantiate()
@onready var trapController = load("res://traps/spike.tscn").instantiate()
@onready var map = $Map1
@onready var tileMap = $Map1/TileMap
@onready var cursorMouse = $Map1/cursorKeyboard/Sprite2D/Marker2D
@onready var cursorController = $Map1/cursorController/Sprite2D/Marker2D
@onready var buildingMode = true
var colKeyboard
var colController
var dimensionsKeyboard
var dimensionsController
var buildingModeMouse
var buildingModeController

# Called when the node enters the scene tree for the first time.
func _ready():
	$Map1/Camera2D.add_target($Map1/Player1, $Map1/Player2)
	tileMap.set_layer_enabled(0, true)
	StartOverAgain()
	
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if buildingMode:
		if buildingModeMouse:
			buildingModeMouse = building(cursorMouse, trapKeyboard, colKeyboard, "click")
		if buildingModeController:
			buildingModeController = building(cursorController, trapController, colController, "Gamepad_Jump")
		if buildingModeController == false and buildingModeMouse == false:
			buildingMode = false
	else:
		tileMap.set_layer_enabled(0, false)
		
func building(loc, trapName, col, buttonName):
	var placeable = MoveTrap(loc.global_position, trapName)
	if Input.is_action_just_pressed(buttonName) and placeable:
		map.get_node("" + trapName.name).modulate = Color("ffffff")
		col.set_disabled(false)
		trapName.OnPlace()
		return false
	return true
	
	
func MoveTrap(cursorLoc, trapName):
	var tempLoc = cursorLoc
#	var loc = Vector2i(0, 0)
	tempLoc = tileMap.local_to_map(tempLoc)
#	var free = trap.CheckIfPlaceable(loc,tileMap)
	var free = trapName.CheckIfPlaceable(tempLoc,tileMap)
	tempLoc = tileMap.map_to_local(tempLoc)
	trapName.global_position = tempLoc
	return free
	
func StartOverAgain():
	buildingMode = true
	buildingModeMouse = true
	buildingModeController = true
	colKeyboard = trapKeyboard.GetCollision()
	colController = trapController.GetCollision()
	dimensionsKeyboard = trapKeyboard.GetDimensions()
	dimensionsController = trapController.GetDimensions()
	SetTrapAgain(trapKeyboard, colKeyboard)
	SetTrapAgain(trapController, colController)
	
func SetTrapAgain(trapName, col):
	trapName.position = Vector2.ZERO
	col.set_disabled(true)
	map.add_child(trapName, true)
	
	
#func CheckIfPlaceable(loc):
#	var tile
#	var tempVec = loc
#	for x in range(dimensions.x):
#		for y in range(dimensions.y):
#			tempVec = loc + Vector2i(x, y)
#			tile = tileMap.get_cell_tile_data(1, tempVec)
#			if tile:
#				map.get_node("" + trap.name).modulate = Color("ad54ff3c")
#				return false
#	map.get_node("" + trap.name).modulate = Color("adff4545")
#	return true
