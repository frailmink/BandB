
extends Node2D
class_name trap

var width = 1
var height = 1
var grounded = true 

func GetCollision():
	return $Sprite2D/StaticBody2D/CollisionPolygon2D

func GetDimensions():
	return Vector2(width, height)

func OnPlace():
	pass

func CheckIfPlaceable(loc,tileMap):
	var tile
	var tempVec = loc

	for x in range(width):
		for y in range(height):
			tempVec = loc + Vector2i(x, y)
			tile = tileMap.get_cell_tile_data(1, tempVec)
			if tile:
				modulate = Color("ad54ff3c")
				return false
	
	if grounded:
		return Grounded(loc, tileMap)
	else:
		modulate = Color("adff4545")
		return true

func Grounded(loc,tileMap):
	var tile
	var tempVec = loc
	for x in range(width):
		tempVec = loc + Vector2i(x, 1)
		tile = tileMap.get_cell_tile_data(1, tempVec)
		if tile == null:
			modulate = Color("ad54ff3c")
			return false
	modulate = Color("adff4545")
	return true 



