using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.DevConsole.ConsoleCommands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoeNegiMod.Moya.Powers;

#pragma warning disable STS001 // Symbol missing localization
public sealed class Bulet() : MoyaPowers
#pragma warning restore STS001 // Symbol missing localization
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool AllowNegative => true;



    public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (base.Owner != dealer)
        {
            return 0m;
        }

        if (!props.IsPoweredAttack())
        {
            return 0m;
        }

        return base.Amount;
    }
    public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature dealer, DamageResult result, ValueProp props, Creature target, CardModel cardSource)
    {
        if (!dealer.HasPower<PBuletPower>())
        {
            await PowerCmd.Apply<PoisonPower>(choiceContext, target, base.Amount, base.Owner, null);
            return;
        }
        if (!target.IsEnemy) return;
        await PowerCmd.ModifyAmount(choiceContext, this, -1, null, null);
    }
}
