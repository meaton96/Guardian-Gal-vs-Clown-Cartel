// Copyright Epic Games, Inc. All Rights Reserved.
/*===========================================================================
	Generated code exported from UnrealHeaderTool.
	DO NOT modify this manually! Edit the corresponding .h files instead!
===========================================================================*/

#include "UObject/GeneratedCppIncludes.h"
#include "GGvsCC/GGvsCCGameMode.h"
#ifdef _MSC_VER
#pragma warning (push)
#pragma warning (disable : 4883)
#endif
PRAGMA_DISABLE_DEPRECATION_WARNINGS
void EmptyLinkFunctionForGeneratedCodeGGvsCCGameMode() {}
// Cross Module References
	GGVSCC_API UClass* Z_Construct_UClass_AGGvsCCGameMode_NoRegister();
	GGVSCC_API UClass* Z_Construct_UClass_AGGvsCCGameMode();
	ENGINE_API UClass* Z_Construct_UClass_AGameModeBase();
	UPackage* Z_Construct_UPackage__Script_GGvsCC();
// End Cross Module References
	void AGGvsCCGameMode::StaticRegisterNativesAGGvsCCGameMode()
	{
	}
	UClass* Z_Construct_UClass_AGGvsCCGameMode_NoRegister()
	{
		return AGGvsCCGameMode::StaticClass();
	}
	struct Z_Construct_UClass_AGGvsCCGameMode_Statics
	{
		static UObject* (*const DependentSingletons[])();
#if WITH_METADATA
		static const UE4CodeGen_Private::FMetaDataPairParam Class_MetaDataParams[];
#endif
		static const FCppClassTypeInfoStatic StaticCppClassTypeInfo;
		static const UE4CodeGen_Private::FClassParams ClassParams;
	};
	UObject* (*const Z_Construct_UClass_AGGvsCCGameMode_Statics::DependentSingletons[])() = {
		(UObject* (*)())Z_Construct_UClass_AGameModeBase,
		(UObject* (*)())Z_Construct_UPackage__Script_GGvsCC,
	};
#if WITH_METADATA
	const UE4CodeGen_Private::FMetaDataPairParam Z_Construct_UClass_AGGvsCCGameMode_Statics::Class_MetaDataParams[] = {
		{ "Comment", "/**\n * The GameMode defines the game being played. It governs the game rules, scoring, what actors\n * are allowed to exist in this game type, and who may enter the game.\n *\n * This game mode just sets the default pawn to be the MyCharacter asset, which is a subclass of GGvsCCCharacter\n */" },
		{ "HideCategories", "Info Rendering MovementReplication Replication Actor Input Movement Collision Rendering Utilities|Transformation" },
		{ "IncludePath", "GGvsCCGameMode.h" },
		{ "ModuleRelativePath", "GGvsCCGameMode.h" },
		{ "ShowCategories", "Input|MouseInput Input|TouchInput" },
		{ "ToolTip", "The GameMode defines the game being played. It governs the game rules, scoring, what actors\nare allowed to exist in this game type, and who may enter the game.\n\nThis game mode just sets the default pawn to be the MyCharacter asset, which is a subclass of GGvsCCCharacter" },
	};
#endif
	const FCppClassTypeInfoStatic Z_Construct_UClass_AGGvsCCGameMode_Statics::StaticCppClassTypeInfo = {
		TCppClassTypeTraits<AGGvsCCGameMode>::IsAbstract,
	};
	const UE4CodeGen_Private::FClassParams Z_Construct_UClass_AGGvsCCGameMode_Statics::ClassParams = {
		&AGGvsCCGameMode::StaticClass,
		"Game",
		&StaticCppClassTypeInfo,
		DependentSingletons,
		nullptr,
		nullptr,
		nullptr,
		UE_ARRAY_COUNT(DependentSingletons),
		0,
		0,
		0,
		0x008802ACu,
		METADATA_PARAMS(Z_Construct_UClass_AGGvsCCGameMode_Statics::Class_MetaDataParams, UE_ARRAY_COUNT(Z_Construct_UClass_AGGvsCCGameMode_Statics::Class_MetaDataParams))
	};
	UClass* Z_Construct_UClass_AGGvsCCGameMode()
	{
		static UClass* OuterClass = nullptr;
		if (!OuterClass)
		{
			UE4CodeGen_Private::ConstructUClass(OuterClass, Z_Construct_UClass_AGGvsCCGameMode_Statics::ClassParams);
		}
		return OuterClass;
	}
	IMPLEMENT_CLASS(AGGvsCCGameMode, 2217711797);
	template<> GGVSCC_API UClass* StaticClass<AGGvsCCGameMode>()
	{
		return AGGvsCCGameMode::StaticClass();
	}
	static FCompiledInDefer Z_CompiledInDefer_UClass_AGGvsCCGameMode(Z_Construct_UClass_AGGvsCCGameMode, &AGGvsCCGameMode::StaticClass, TEXT("/Script/GGvsCC"), TEXT("AGGvsCCGameMode"), false, nullptr, nullptr, nullptr);
	DEFINE_VTABLE_PTR_HELPER_CTOR(AGGvsCCGameMode);
PRAGMA_ENABLE_DEPRECATION_WARNINGS
#ifdef _MSC_VER
#pragma warning (pop)
#endif
