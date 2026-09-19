using System.Collections.Generic;

namespace MoeNegiMod.Moya.Core;

/// <summary>
/// 模组元数据（扩展端口）。
/// 所有依赖模组标识的代码应引用此处的常量，避免散落的字符串字面量。
/// </summary>
public static class MoyaMod
{
    /// <summary>模组 ID（同时用作 Harmony 补丁命名空间与日志前缀）。</summary>
    public const string Id = "Moya";

    /// <summary>模组显示名称。</summary>
    public const string Name = "Moya";

    /// <summary>当前版本号，遵循语义化版本（SemVer）：主版本.次版本.修订。</summary>
    public const string Version = "0.1.0";

    /// <summary>作者。</summary>
    public const string Author = "hwjsldgj";

    /// <summary>源码仓库地址。</summary>
    public const string RepositoryUrl = "https://github.com/hwjsldgj/moya";

    /// <summary>
    /// 当前支持的语言代码列表（用于扩展指南与本地化审计）。
    /// 取值遵循 Godot 本地化命名：zhs = 简体中文。
    /// </summary>
    public static readonly IReadOnlyList<string> SupportedLocales = ["zhs"];
}
