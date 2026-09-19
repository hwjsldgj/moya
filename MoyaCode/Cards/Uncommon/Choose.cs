using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;
using MoeNegiMod.Moya.Character;
using MoeNegiMod.Moya.Powers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MegaCrit.Sts2.Core.Models.Monsters.KnowledgeDemon;

namespace MoeNegiMod.Moya.Cards;





#pragma warning disable STS001 // Symbol missing localization
public class Choose() : MoyaCard(cost: 1,
#pragma warning restore STS001 // Symbol missing localization

    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromCard<AddCard>(base.IsUpgraded), HoverTipFactory.FromCard<AddCoins>(base.IsUpgraded)];

    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(5, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        List<CardModel> cards = new List<CardModel>()
{
    CombatState.CreateCard(ModelDb.Card<AddCard>(), base.Owner),
    CombatState.CreateCard(ModelDb.Card<AddCoins>(), base.Owner)
};
        if (base.IsUpgraded)
        {
            CardCmd.Upgrade(cards, CardPreviewStyle.HorizontalLayout);
        }

        CardModel cardModel = await CardSelectCmd.FromChooseACardScreen(choiceContext, cards, base.Owner, canSkip: false);
        if (cardModel != null)
        {

            await CardCmd.AutoPlay(choiceContext,cardModel,target: null,type: AutoPlayType.Default);

        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3m);
    }

}
