using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using MoeNegiMod.Moya.Powers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoeNegiMod.Moya.Cards;

#pragma warning disable STS001 // Symbol missing localization
public class MoyaBullet() : MoyaCard(cost: 1,
#pragma warning restore STS001 // Symbol missing localization

    CardType.Skill, CardRarity.Basic,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(4, ValueProp.Move),new DynamicVar("BuletPower",2m)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Retain];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<Bulet>()
    ];
    public override async Task<Task> AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card != this)
        {
            return Task.CompletedTask;
        }

        await PowerCmd.Apply<Bulet>(choiceContext, base.Owner.Creature, base.DynamicVars["BuletPower"].BaseValue, base.Owner.Creature, this);
        return Task.CompletedTask;
    }
    protected override void OnUpgrade()
    {
        DynamicVars["BuletPower"].UpgradeValueBy(1);
    }
}
