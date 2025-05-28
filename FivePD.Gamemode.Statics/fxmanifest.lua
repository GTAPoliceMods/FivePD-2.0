---------------------------------------------------------------------------------------------
-- FivePD, and all included files, are the express intellectual property of GTAPoliceMods, --
-- unless otherwise noted by licenses included with this distribution of the gamemode or   --
-- or other related files and/or projects. Do not redistribute. Usage is subject to all    --
-- of the terms and conditions of the FivePD End User License Agreement (EULA) and the     --
-- GTAPoliceMods Community Guidelines. A copy of the FivePD EULA is included with this     --
-- distribution of FivePD. Thanks for playing. We hope you enjoy the project. - GPMDS      --
---------------------------------------------------------------------------------------------

-- Editing any of the lines below is not supported and may cause unexpected behavior.

fx_version 'cerulean'
game 'gta5'

author 'GTAPoliceMods Development Studios'
description 'The FivePD Gamemode'
version '2.0.0'

ui_page 'http://localhost:3000'
client_script 'Libraries/FivePD.Gamemode.Client/FivePD.Gamemode.Client.net.dll'
server_script 'Libraries/FivePD.Gamemode.Server/FivePD.Gamemode.Server.net.dll'

files {
    'Libraries/FivePD.Gamemode.Client/**/*',
    'Libraries/FivePD.Nui/**/*',
    'Localization/**/*',
	'Config/**/*'
}