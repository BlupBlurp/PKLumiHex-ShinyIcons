<div align=center>
    <h1>PKLumiHeX<br>
        <h2>Download:
            <a href="https://github.com/TalonSabre/PKLumiHex/releases/latest">Latest Release</a>
        </h2>
    </h1>
</div>
<div>
    <h3 align=center>
        Fork of
        <a href="https://github.com/kwsch/PKHeX">PKHeX</a>
        to support Luminescent Platinum
    </h3> 
    <p align=center>Some features could break your Luminescent Platinum save, always keep a back up!</p>
</div>

##

<div>
    <h3 align=left>Features working on Luminescent Platinum saves</h3>
    <ul>
        <li><h4>Pokémon Editor</h4></li>
        <ul>
            <li>Luminescent Platinum's Stat and Ability changes will display</li>
            <ul><li>Custom Pokémon forms included!</li></ul>
            <li>Individual Pokémon can be imported (*.pb8, *.pb8lumi) and exported (*.pb8lumi)</li>
            <ul><li>Includes box importing and exporting (*.bin)</li></ul>
        </ul>
        <li><h4>Bag Editor</h4></li>
        <ul><li>New items from Luminescent Platinum included!</li></ul>
        <li><h4>Pokédex Editor</h4></li>
        <ul>
            <li>Allows setting seen and caught data</li>
            <ul><li>Luminescent Platinum has significantly shrunk dex data. Setting one form sets all.</li></ul>
            <li><em>Dex editor can potentially break Dex completion progress, use at your own risk!</em></li>
        </ul>
        <li><h5>Box Layout Editor</h5></li>
        <li><h5>Seal Stickers Editor</h5></li>
        <li><h4>Trainer Info Editor</h4></li>
        <ul>
            <li>Luminescent Platinum only supports Brilliant Diamond Version, and the English Language for now!</li>
            <ul><li>If you started your game in a language besides English, you can change it with this editor.</li></ul>
        </ul>
    </ul>
</div>
<div>
    <h3 align=left>Known 'Issues'</h3>
    <ul>
        <li><h4>Pokedex editor can cause issues if with dex completion tasks.</h4></li>
        <ul><li>Don't use the dex editor if you want those digital certificates.</li></ul>
        <li><h4>Certain Pokémon show up as a Ditto</h4></li>
        <ul><li>Affected Pokémon are not currently available in Luminescent Platinum 2.0F</li></ul>
        <li><h4>Certain forms' shiny display as the default pixel art sprites</h4></li>
        <ul><li>Unfortunately, we do not have shiny sprites for them</li></ul>
        <li><h4>Sprites for Luminescent forms do not change regardless of shinyness or sprite preference</h4></li>
        <ul><li>Same issue as above, but the shiny symbol <em>will</em> display</li></ul>
    </ul>
</div>
<div>
    <h1 align=center>Screenshots</h1>
    <div align=center>
        <p align=center>
            <img src="https://i.imgur.com/UoyTq53.png" width="47.5%">
            <img src="https://i.imgur.com/311Nlkd.png" width="47.5%">
        </p>
        <p>
            <img align=center src="https://i.imgur.com/wYu3RFK.png" width="47.5%">
            <img align=center src="https://i.imgur.com/8wgwPlS.png" width="47.5%">
        </p>
        <div align=center>
            <p>
                <img src="https://i.imgur.com/gGJ0PFK.png" width="22.635%">
                <img src="https://i.imgur.com/t0HF7f3.png" width="22.5%">
                <img src="https://i.imgur.com/HklgScH.png" width="48.5%">
            </p>
        </div>
    </div>
</div>

-----
# PKHeX
<div>
  <span>English</span> / <a href=".github/README-es.md">Español</a> / <a href=".github/README-fr.md">Français</a> / <a href=".github/README-de.md">Deutsch</a> / <a href=".github/README-it.md">Italiano</a> / <a href=".github/README-zhHK.md">繁體中文</a> / <a href=".github/README-zh.md">简体中文</a>
</div>

![License](https://img.shields.io/badge/License-GPLv3-blue.svg)

Pokémon core series save editor, programmed in [C#](https://en.wikipedia.org/wiki/C_Sharp_%28programming_language%29).

Supports the following files:
* Save files ("main", \*.sav, \*.dsv, \*.dat, \*.gci, \*.bin)
* GameCube Memory Card files (\*.raw, \*.bin) containing GC Pokémon savegames.
* Individual Pokémon entity files (.pk\*, \*.ck3, \*.xk3, \*.pb7, \*.sk2, \*.bk4, \*.rk4)
* Mystery Gift files (\*.pgt, \*.pcd, \*.pgf, .wc\*) including conversion to .pk\*
* Importing GO Park entities (\*.gp1) including conversion to .pb7
* Importing teams from Decrypted 3DS Battle Videos
* Transferring from one generation to another, converting formats along the way.

Data is displayed in a view which can be edited and saved.
The interface can be translated with resource/external text files so that different languages can be supported.

Pokémon Showdown sets and QR codes can be imported/exported to assist in sharing.

PKHeX expects save files that are not encrypted with console-specific keys. Use a savedata manager to import and export savedata from the console ([Checkpoint](https://github.com/FlagBrew/Checkpoint), save_manager, [JKSM](https://github.com/J-D-K/JKSM), or SaveDataFiler).

**We do not support or condone cheating at the expense of others. Do not use significantly hacked Pokémon in battle or in trades with those who are unaware hacked Pokémon are in use.**

## Building

PKHeX is a Windows Forms application which requires [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0).

The executable can be built with any compiler that supports C# 11.

### Build Configurations

Use the Debug or Release build configurations when building. There isn't any platform specific code to worry about!

## Dependencies

PKHeX's QR code generation code is taken from [QRCoder](https://github.com/codebude/QRCoder), which is licensed under [the MIT license](https://github.com/codebude/QRCoder/blob/master/LICENSE.txt).

PKHeX's BDSP shiny sprite collection is taken from [bdsp-shiny-icons](https://github.com/BlupBlurp/bdsp-shiny-icons), which is licensed under [the MIT license](https://github.com/BlupBlurp/bdsp-shiny-icons/blob/main/LICENSE).

PKHeX's Pokémon Legends: Arceus sprite collection is taken from the [National Pokédex - Icon Dex](https://www.deviantart.com/pikafan2000/art/National-Pokedex-Version-Delta-Icon-Dex-824897934) project and its abundance of collaborators and contributors.

### IDE

PKHeX can be opened with IDEs such as [Visual Studio](https://visualstudio.microsoft.com/downloads/) by opening the .sln or .csproj file.
