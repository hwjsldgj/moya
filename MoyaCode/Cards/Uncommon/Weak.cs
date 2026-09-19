using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using MoeNegiMod.Moya.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoeNegiMod.Moya.Cards;

#pragma warning disable STS001 // Symbol missing localization
public class Weakk() : MoyaCard(cost: 1,
#pragma warning restore STS001 // Symbol missing localization
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<WeakPower>(),
        HoverTipFactory.FromPower<VulnerablePower>()
    ];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("power",3)];
    public override IEnumerable<CardKeyword> CanonicalKeywords =>[CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int amount = base.DynamicVars["power"].IntValue;
        await PowerCmd.Apply<WeakPower>(choiceContext,cardPlay.Target, amount, base.Owner.Creature, this);
        await PowerCmd.Apply<VulnerablePower>(choiceContext,cardPlay.Target, amount, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["Power"].UpgradeValueBy(2m);
    }
}
