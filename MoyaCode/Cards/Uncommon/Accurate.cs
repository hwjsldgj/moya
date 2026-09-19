using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using MoeNegiMod.Moya.Cards;
using MoeNegiMod.Moya.Powers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoeNegiMod.Moya.Cards;



#pragma warning disable STS001 // Symbol missing localization
public class Accurate() : MoyaCard(cost: 1,
#pragma warning restore STS001 // Symbol missing localization

	CardType.Skill, CardRarity.Uncommon,
	TargetType.Self)
{
	private readonly string _tempStrengthKey;
	

	public override IEnumerable<CardKeyword> CanonicalKeywords =>
	[
		CardKeyword.Retain,
	];

	protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("SetupStrikePower", 0)];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<StrengthPower>()
	];
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)

	{
		Creature user = base.Owner.Creature;
		int currentVigor = user.GetPowerAmount<CoinsPower>();
		int loseAmount = currentVigor - 1;
		if (loseAmount > 0)
		{
			
			await CreatureCmd.TriggerAnim(user, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<CoinsPower>(choiceContext,user, -loseAmount, user, this);
			await PowerCmd.Apply<StrengthPower>(choiceContext,user, loseAmount, user, this); 
		}
		
		await Task.CompletedTask;
	}
	protected override void OnUpgrade()
	{
		EnergyCost.UpgradeBy(-1);
	}
}
