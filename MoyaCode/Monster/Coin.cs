using MegaCrit.Sts2.Core.Models.Monsters;
using MoeNegiMod.Moya.Extensions;

namespace MoeNegiMod.Moya.Monster;

/*
 * 预留扩展端口：硬币怪物（Coin）
 *
 * 当前为空骨架，尚未实现。本地化条目 MOENEGIMOD-COIN 已预留在
 * Moya/localization/zhs/monsters.json 中，实现后即可使用。
 *
 * 实现步骤（参考 STS2 / BaseLib 的怪物模型 API）：
 *   1. 继承合适的基类（如 MonsterModel / CustomMonsterModel）；
 *   2. 在类上添加注册特性（参照 MoyaCode/Cards/ 下的 [Pool] 用法）；
 *   3. 实现意图（intent）与行动（move）逻辑；
 *   4. 在 MoyaCode/Core/ContentRegistry.cs 中登记该内容。
 *
 * 注意：本文件保持“仅注释、无编译单元”状态不会影响构建；
 * 开始实现时请移除本注释块并补充正式实现。
 */
