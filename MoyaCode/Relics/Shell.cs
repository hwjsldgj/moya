using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.ValueProps;
using MoeNegiMod.Moya.Character;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoeNegiMod.Moya.Relics;



#pragma warning disable STS001 // Symbol missing localization
public class Shell: MoyaRelics
#pragma warning restore STS001 // Symbol missing localization

{
	public override RelicRarity Rarity => RelicRarity.Starter;

	public override async Task<Task> AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature dealer, CardModel cardSource)
	{
		if (CombatManager.Instance.IsInProgress && target == base.Owner.Creature && result.UnblockedDamage > 0)
		{
			await CreatureCmd.LoseMaxHp(choiceContext, target, (decimal)result.Props / 2, false);
		}
		return base.AfterDamageReceived(choiceContext, target, result, props, dealer, cardSource);
	   
	}
	public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature target, bool wasRemovalPrevented, float deathAnimLength)
	{
		if (target.Side != base.Owner.Creature.Side)
		{
			Flash();
			await CreatureCmd.GainMaxHp(base.Owner.Creature, 6);
		}
	}
}
