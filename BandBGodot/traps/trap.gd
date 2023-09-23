extends Node2D
class_name trap

var width = 1
var height = 1

func GetCollision():
	return $Sprite2D/StaticBody2D/CollisionShape2D

func GetDimensions():
	return Vector2(width, height)
	
func OnPlace():
	pass
