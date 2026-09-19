using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using MoeNegiMod.Moya.Powers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoeNegiMod.Moya.Cards;

#pragma warning disable STS001 // Symbol missing localization
public class BlueShooter() : MoyaCard(cost: 1,
#pragma warning restore STS001 // Symbol missing localization

    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(5, ValueProp.Move), new PowerVar<VulnerablePower>("VulnerablePower",2m)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<VulnerablePower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {

        
        var owner = this.Owner;
        if (owner == null) return;

        var enemies = CombatState.HittableEnemies;
        if (enemies.Count <= 0) return;
        var random = new Random();
        Creature randomTarget = enemies[random.Next(enemies.Count)];
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(randomTarget).Execute(choiceContext);
        await PowerCmd.Apply<VulnerablePower>(choiceContext,randomTarget, base.DynamicVars["VulnerablePower"].BaseValue, base.Owner.Creature, this);

    }
    
    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
        base.DynamicVars.Vulnerable.UpgradeValueBy(1m);
    }
}
