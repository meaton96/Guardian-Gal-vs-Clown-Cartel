// Copyright Epic Games, Inc. All Rights Reserved.

#include "GGvsCCGameMode.h"
#include "GGvsCCCharacter.h"

AGGvsCCGameMode::AGGvsCCGameMode()
{
	// Set default pawn class to our character
	DefaultPawnClass = AGGvsCCCharacter::StaticClass();	
}
