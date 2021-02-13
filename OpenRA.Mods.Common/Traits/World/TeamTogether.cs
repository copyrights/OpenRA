#region Copyright & License Information
/*
 * Copyright 2007-2020 The OpenRA Developers (see AUTHORS)
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */
#endregion

using System.Collections.Generic;
using OpenRA.Traits;

namespace OpenRA.Mods.Common.Traits
{
	[Desc("Controls the team together checkbox in the lobby options.")]
	public class TeamTogetherInfo : TraitInfo, ILobbyOptions
	{
		[Desc("Descriptive label for the command allied units checkbox in the lobby.")]
		public readonly string CmdAlliedUnitsCheckboxLabel = "Command Allied Units";

		[Desc("Tooltip description for the command allied units checkbox in the lobby.")]
		public readonly string CmdAlliedUnitsCheckboxDescription = "Allow team members to command your units and like wise command team members units.";

		[Desc("Default value of the command allied units checkbox in the lobby.")]
		public readonly bool CmdAlliedUnitsCheckboxEnabled = false;

		[Desc("Prevent the command allied units state from being changed in the lobby.")]
		public readonly bool CmdAlliedUnitsCheckboxLocked = false;

		[Desc("Whether to display the command allied units checkbox in the lobby.")]
		public readonly bool CmdAlliedUnitsCheckboxVisible = true;

		[Desc("Display order for the command allied units checkbox in the lobby.")]
		public readonly int CmdAlliedUnitsCheckboxDisplayOrder = 0;

		IEnumerable<LobbyOption> ILobbyOptions.LobbyOptions(Ruleset rules)
		{
			yield return new LobbyBooleanOption("cmdalliedunits", CmdAlliedUnitsCheckboxLabel, CmdAlliedUnitsCheckboxDescription,
				CmdAlliedUnitsCheckboxVisible, CmdAlliedUnitsCheckboxDisplayOrder, CmdAlliedUnitsCheckboxEnabled, CmdAlliedUnitsCheckboxLocked);
		}

		public override object Create(ActorInitializer init) { return new TeamTogether(this); }
	}

	public class TeamTogether : INotifyCreated
	{
		readonly TeamTogetherInfo info;
		public bool CmdAlliedUnitsEnabled { get; private set; }

		public TeamTogether(TeamTogetherInfo info)
		{
			this.info = info;
		}

		void INotifyCreated.Created(Actor self)
		{
			CmdAlliedUnitsEnabled = self.World.LobbyInfo.GlobalSettings
				.OptionOrDefault("cmdalliedunits", info.CmdAlliedUnitsCheckboxEnabled);
		}
	}
}
