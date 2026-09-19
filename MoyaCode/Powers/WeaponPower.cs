using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoeNegiMod.Moya.Powers;

#pragma warning disable STS001 // Symbol missing localization
public sealed class WeaponPower() : MoyaPowers
#pragma warning restore STS001 // Symbol missing localization
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool AllowNegative => true;

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)
    {

        if (player != base.Owner.Player)
        {
            return;
        }

        for (int i = 0; i < base.Amount; i++)
        {
            CardModel cardModel = CardFactory.GetDistinctForCombat(player, from c in player.Character.CardPool.GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint)
                                                                           where c.Type == CardType.Attack
                                                                           select c, 1, player.RunState.Rng.CombatCardGeneration).FirstOrDefault();
            if (cardModel != null)
            {
                if (!cardModel.Keywords.Contains(CardKeyword.Exhaust))
                {
                    cardModel.AddKeyword(CardKeyword.Exhaust);
                }
                cardModel.SetToFreeThisTurn();
                await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, base.Owner.Player);
            }
        }
    }
}
