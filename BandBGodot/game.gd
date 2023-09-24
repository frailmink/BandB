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
	$Map1/Camera2D.add_target($Map1/Player1)
	$Map1/Camera2D.add_target($Map1/Player2)
	tileMap.set_layer_enabled(0, true)
	StartOverAgain()
	
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if buildingMode:
		if buildingModeMouse:
			buildingModeMouse = building(cursorMouse, trapKeyboard, colKeyboard, "Click_Mouse")
		if buildingModeController:
			buildingModeController = building(cursorController, trapController, colController, "Click_Controller")
		if buildingModeController == false and buildingModeMouse == false:
			buildingMode = false
	else:
		tileMap.set_layer_enabled(0, false)
		
func building(loc, trapName, col, buttonName):
	var placeable = MoveTrap(loc.global_position, trapName)
	if Input.is_action_just_pressed(buttonName) and placeable:
		map.get_node("" + trapName.name).modulate = Color("ffffff")
		col.set_disabled(false)
		trapName.OnPlace(tileMap.local_to_map(loc.global_position), tileMap)
		return false
	return true
	
	
func MoveTrap(cursorLoc, trapName):
	var tempLoc = cursorLoc
	tempLoc = tileMap.local_to_map(tempLoc)
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
	
func Restart_Scene():
	# Implement what happens when the player dies.
	# For example, you can play an animation, show a game over screen, or reset the scene.
	# To restart the scene, you can use the following code:

	# Get the current scene and reload it.
	var current_scene = get_tree().get_current_scene()
	get_tree().reload_current_scene()
