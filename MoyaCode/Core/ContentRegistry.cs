using System;
using System.Collections.Generic;

namespace MoeNegiMod.Moya.Core;

/// <summary>
/// 内容注册表（预留扩展端口）。
///
/// 背景：当前模组内容由 BaseLib 通过特性自动发现并注册
/// （例如卡牌类上的 [Pool(typeof(MoyaCardPool))]、遗物类上的
/// [Pool(typeof(MoyaRelicPool))]），无需手动注册即可进入游戏。
///
/// 本表提供统一的「内容清单」登记入口，用于：
///   1. 集中盘点模组已有内容（卡牌 / 能力 / 遗物 / 怪物 / 事件）；
///   2. 为后续接入调试面板、配置系统、内容统计预留端口；
///   3. 给新增内容一个显式的登记位置，便于协作与审计。
///
/// 使用约定：
///   - 新增内容在实现类完成后，于模组初始化处（MainFile.Initialize 或
///     对应类构造完成处）调用 Register 登记一条记录；
///   - category 取值建议：Card / Power / Relic / Monster / Event。
/// </summary>
public static class ContentRegistry
{
    private static readonly List<ContentEntry> Entries = new();

    /// <summary>当前已登记的全部内容（只读）。</summary>
    public static IReadOnlyList<ContentEntry> All => Entries;

    /// <summary>
    /// 登记一条内容记录。
    /// </summary>
    /// <param name="category">内容类别：Card / Power / Relic / Monster / Event。</param>
    /// <param name="id">内容标识（建议使用类名，与本地化 key 对应）。</param>
    /// <param name="displayName">显示名称（可空，缺省时取 id）。</param>
    public static void Register(string category, string id, string displayName = "")
    {
        Entries.Add(new ContentEntry(category, id, string.IsNullOrEmpty(displayName) ? id : displayName));
    }

    /// <summary>
    /// 按类别查询已登记内容。
    /// </summary>
    public static IEnumerable<ContentEntry> Query(string category)
    {
        foreach (ContentEntry entry in Entries)
        {
            if (entry.Category == category)
            {
                yield return entry;
            }
        }
    }

    /// <summary>
    /// 单条内容记录。
    /// </summary>
    /// <param name="Category">内容类别。</param>
    /// <param name="Id">内容标识。</param>
    /// <param name="DisplayName">显示名称。</param>
    public readonly record struct ContentEntry(string Category, string Id, string DisplayName);
}
