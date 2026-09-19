using System;
using System.Collections.Generic;

using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MoeNegiMod.Moya.Cards;
using MoeNegiMod.Moya.Core;
using MoeNegiMod.Moya.Extensions;
using MoeNegiMod.Moya.Relics;

namespace MoeNegiMod.Moya.Character;

#pragma warning disable STS001 // Symbol missing localization
public class Moya : PlaceholderCharacterModel
#pragma warning restore STS001 // Symbol missing localization
{
	public const string CharacterId = "Moya";

	//public override string CustomCharacterSelectBg => "res://Selphina/Scenes/Char_Select/char_select_bg_selphina.tscn";

	public override string PlaceholderID => "necrobinder";

	public static readonly Color Color = new Color(ModConfig.CharacterColorHex);

	public override Color NameColor => Color;
	public override CharacterGender Gender => CharacterGender.Feminine;
	public override int StartingHp => ModConfig.StartingHp;

	public override IEnumerable<CardModel> StartingDeck => [
		ModelDb.Card<MoyaAttack>(),
		ModelDb.Card<MoyaAttack>(),
		ModelDb.Card<MoyaAttack>(),
		ModelDb.Card<MoyaAttack>(),
		ModelDb.Card<MoyaAttack>(),
		ModelDb.Card<MoyaBlock>(),
		ModelDb.Card<MoyaBlock>(),
		ModelDb.Card<MoyaBlock>(),
		ModelDb.Card<MoyaBlock>(),
		ModelDb.Card<MoyaBullet>(),
        ModelDb.Card<MoyaCoin>(),

    ];

	public override IReadOnlyList<RelicModel> StartingRelics => [ModelDb.Relic<Shell>()];

	public override CardPoolModel CardPool => ModelDb.CardPool<MoyaCardPool>();
	public override RelicPoolModel RelicPool => ModelDb.RelicPool<MoyaRelicPool>();
	public override PotionPoolModel PotionPool => ModelDb.PotionPool<SharedPotionPool>();

	/*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
		override all the other methods that define those assets.
		These are just some of the simplest assets, given some placeholders to differentiate your character with.
		You don't have to, but you're suggested to rename these images. */
	public override string CustomVisualPath => "res://Moya/Scenes/MoyaVisual.tscn";
	public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
	public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
	public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
	public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
}
