using BaseLib.Abstracts;
using Godot;
using MoeNegiMod.Moya.Character;
using MoeNegiMod.Moya.Relics;
using System;

namespace MoeNegiMod.Moya.Character;

public partial class MoyaRelicPool : CustomRelicPoolModel
{
	public override string EnergyColorName => Moya.CharacterId;

	public override Color LabOutlineColor => Moya.Color;
}
