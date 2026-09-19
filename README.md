# Moya — Slay the Spire 2 自定义角色 Mod

> *Blood is Fuel.*

**Moya** 是一个为《杀戮尖塔 2》（Slay the Spire 2 / STS2）制作的自定义可玩角色模组。她以「硬币 · 子弹 · 过热」为核心资源体系，围绕"攒币—花币"的经济循环与"血即是燃料"的生命转换主题设计了一套原创卡牌与机制。

## 特性一览

- 🎴 **24 张原创卡牌**：基础 4 / 普通 7 / 罕见 7 / 稀有 3 / 衍生 3，全部带简体中文本地化；
- ⚙️ **7 个原创能力 + 1 个专属遗物**：硬币、子弹、热核、过热、毒弹、高机动、武器库；
- 🩸 **血量经济**：专属遗物「躯壳」以最大生命值为代价换取成长，契合角色主题；
- 🛠️ **预留扩展端口**：模组元数据（`MoyaMod`）、集中平衡配置（`ModConfig`）、内容注册表（`ContentRegistry`）一应俱全，便于后续扩展新卡牌、能力、遗物与怪物。

## 角色介绍

| 项目 | 内容 |
|---|---|
| 名称 | Moya |
| 生命值 | 75 |
| 主题色 | 绿 `#1d6925` |
| 起始牌组 | 打击 ×5 · 防御 ×4 · 装填 ×1 · 硬币 ×1 |
| 起始遗物 | 躯壳 |
| 卡牌池 | MoyaCardPool（专属） |
| 遗物池 | MoyaRelicPool（专属） |
| 药水池 | 共享药水池 |

### 核心机制

- **硬币 Coins**：核心资源。层数为 1 时，攻击会随机对一个敌人追加等量伤害；敌人回合末自动 -1。「取舍 / 精确瞄准 / 锤币 / 全力防御」等卡围绕"攒币—花币"形成经济闭环；
- **子弹 Bulet**：为攻击附加固定伤害，攻击后 -1；未获得「毒弹」时，每次攻击同时为目标施加等量中毒；
- **热核 Code**：打出攻击牌后，对全体敌人造成一次爆发伤害并移除自身；
- **过热 OverHeat**（敌方减益）：攻击伤害 -25%，回合末 -2 力量；
- **高机动 TwoSide**：硬币层数增减时获得格挡；
- **武器库 Weapon**：回合开始时免费发放随机攻击牌（带消耗）。

### 卡牌总览

| 稀有度 | 卡牌 | 核心效果 |
|---|---|---|
| 基础 | 打击 / 防御 / 装填 / 硬币 | 标准攻防 + 抽取时装填子弹 / 叠硬币 |
| 普通 | 全力防御、贯穿者、热能溅射、锤币、冲刺闪避、转化战法、持续打击 | 防御换硬币、3 连击、溅射易伤、抽牌联动、手牌转化、自我复制 |
| 罕见 | 精确瞄准、闪电神经、nuke、取舍、过热、毒弹、破绽百出 | 硬币转力量 / 转能量抽牌、二选一、全屏核爆、过热减益 |
| 稀有 | 电磁轨道炮、高机动、武器库 | 硬币层数追伤、硬币联动格挡、免费武器引擎 |
| 衍生 | 兴奋、肾上腺素、投币 | 硬币体系的功能组件 |

### 遗物

| 遗物 | 效果 |
|---|---|
| 躯壳 Shell（起始） | 受到未格挡伤害时失去最大生命值；击杀敌人时 +6 最大生命值 |

## 安装方法

> 模组开发中，以下为通用 STS2 模组安装方式。

1. 将编译产物（DLL 与 Godot 资源包）放入 `SteamLibrary/steamapps/common/Slay the Spire 2/mods/Moya/`；
2. 或通过 Steam 创意工坊订阅（发布后提供）；
3. 启动游戏，在模组列表确认 **Moya** 已加载。

**依赖**：需要 [BaseLib](https://steamcommunity.com) 模组框架。

## 开发环境

| 依赖 | 版本/说明 |
|---|---|
| Slay the Spire 2 | 最新版（游戏本体，含 `sts2.dll`、`GodotSharp.dll`、`0Harmony.dll`） |
| BaseLib | Steam 创意工坊模组框架（提供 `CustomCardModel` 等基类） |
| Godot | 4.5.x（含 .NET / C# 支持），用于资源导入与导出 |
| Visual Studio | 2026（18.x）或任意支持 .NET 9 的 IDE |
| .NET SDK | 9.0 |

### 构建步骤

1. 用 Godot 4.5 打开 `project.godot`，首次打开会自动生成 `.godot/` 导入缓存；
2. 用 Visual Studio 打开 `Moya.sln`；
3. 在 `Moya.csproj` 中核对各 DLL 的 `HintPath`（本地 Steam 路径可能不同），如缺失则更新为你的本机路径：
   - `0Harmony.dll` / `sts2.dll` / `GodotSharp.dll`：`.../Slay the Spire 2/data_sts2_windows_x86_64/`
   - `BaseLib.dll`：Steam 创意工坊 BaseLib 目录
4. 编译（Debug / ExportDebug / ExportRelease 均可）。

> 注意：仓库提交了 `export_presets.cfg` 作为参考，但导出产物（`.exe`、`.pck`）已从版本库移除（见 `.gitignore`），请在本地自行导出。

## 目录结构

```
moya_repo/
├── MainFile.cs                  # 模组入口（Harmony PatchAll）
├── Moya.csproj / Moya.sln       # C# 工程 / 解决方案
├── project.godot                # Godot 4.5 工程配置（C#）
├── export_presets.cfg           # Windows 导出预设（参考）
├── .editorconfig                # 统一代码规范
├── MoyaCode/                    # C# 源码
│   ├── Core/                    # ★ 扩展端口：模组元数据 / 平衡配置 / 内容注册表
│   ├── Cards/                   # 24 张卡牌（按稀有度分目录）
│   ├── Powers/                  # 7 个能力
│   ├── Relics/                  # 遗物（躯壳）
│   ├── Character/               # 角色定义 + 卡池 / 遗物池
│   ├── Monster/                 # 怪物扩展位（Coin 骨架）
│   └── Extensions/              # 资源路径工具
├── Moya/                        # Godot 资源
│   ├── Images/                  # 卡牌插画、角色立绘、图标
│   ├── localization/zhs/        # 简体中文本地化 JSON
│   └── Scenes/MoyaVisual.tscn   # 角色战斗场景
```

## 扩展指南

模组已通过抽象基类体系（`MoyaCard` / `MoyaPowers` / `MoyaRelics`）+ BaseLib 特性自动发现提供扩展端口，新增内容只需遵循固定模式。

### 新增一张卡牌

1. 在 `MoyaCode/Cards/` 对应稀有度目录新建 `MyCard.cs`：
```csharp
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using MoeNegiMod.Moya.Cards;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoeNegiMod.Moya.Cards;

#pragma warning disable STS001 // Symbol missing localization
public class MyCard() : MoyaCard(cost: 1,
#pragma warning restore STS001 // Symbol missing localization
    CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
            .Execute(choiceContext);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(3m);
}
```
2. 在 `Moya/localization/zhs/cards.json` 添加条目：
```json
"MOENEGIMOD-MY_CARD.title": "我的卡牌",
"MOENEGIMOD-MY_CARD.description": "造成{Damage:diff()}点伤害"
```
3. 在 `MoyaCode/Core/ContentRegistry.cs` 的登记说明处追加（或在初始化时调用 `ContentRegistry.Register("Card", nameof(MyCard))`）。

> 卡牌将自动进入 `MoyaCardPool`（类上的 `[Pool(typeof(MoyaCardPool))]` 由基类继承）。

### 新增能力 / 遗物

- 能力：继承 `MoyaPowers`，实现 `Type` / `StackType` / 钩子方法（如 `ModifyDamageAdditive`、`AfterDamageGiven`），并在 `powers.json` 加本地化；
- 遗物：继承 `MoyaRelics` 并添加 `[Pool(typeof(MoyaRelicPool))]`，在 `relics.json` 加本地化。

### 新增怪物

在 `MoyaCode/Monster/` 新建实现类（参照 `Coin.cs` 骨架注释），完成后在 `monsters.json` 补充本地化并在注册表登记。

### 数值调整

角色级参数集中在 `MoyaCode/Core/ModConfig.cs`（如 `StartingHp`）；卡牌级数值暂以卡内声明为准，迁移至配置的示例见 README 与 `ModConfig` 注释。

## 代码规范

- **命名空间**：全部代码统一使用 `MoeNegiMod.Moya.*`（子命名空间按职责划分）；
- **命名**：类名 PascalCase，与文件名一致；本地化 key 前缀 `MOENEGIMOD-` + 类名大写（含下划线分隔），新增内容必须补齐本地化；
- **格式**：4 空格缩进、LF 换行、UTF-8，由 `.editorconfig` 统一约束（VS / VS Code / Rider 自动生效）；
- **日志**：统一使用 `MainFile.Logger`，日志标签使用 `[Moya]`；
- **最小 using**：仅保留实际使用的引用，避免 IDE 自动导入残留（`.editorconfig` 已开启 IDE0005 提醒）；
- **数值**：使用 `decimal` 表达游戏内数值，与 STS2 `DynamicVar` 对齐。

## 版本记录

### v0.1.0（2026-09-19）
- 首个规范化版本：统一命名空间与日志标签、清理构建产物与 IDE 残留、新增扩展端口（`Core/`）、补充项目文档；
- 游戏内容与初版保持一致（24 卡 / 7 能力 / 1 遗物）。

## 许可证

本项目暂未指定开源许可证，保留所有权利。发布前请补充 LICENSE 文件。
