# Steel Spartan, the Fabricator

A hero mod, introducing The Steel Spartan, an engineer who uses technology to buff up his allies. 

This currently does not include any events or quests related to The Steel Spartan. 

Designed by @jonahds29

A couple of notes:
## Notes:
- I understand that things are going to be janky at times, and there are definitely bugs that will be worked out
- **What to do if The Steel Spartan is not unlocked:** Due to some jankiness of the way the code works, The Steel Spartan is unlocked only for the profile that is open when you launch the game (and for new profiles). So if they aren't unlocked in the correct profile, switch to that profile, close the game and re-open it and they will be unlocked. I'll fix this in the future, but most people won't notice it. You can also just use the profile editor to fix it.
- There are **no character events** for The Steel Spartan at this time beyond the ones that are available to all characters of a given class (such as pet trainers or healers being able to remove cards at Rest areas).
- The Steel Spartan's selection location (in the Hero Selection screen) is intentionally in position 5 (the far right). I have not yet automated the process of placing characters, and this is to accommodate other heroes. If you wish to change this, you can access the `fabricator.json` file and the `OrderInList` property with whatever you wish.

This mod relies on [Obeliskial Content](https://across-the-obelisk.thunderstore.io/package/meds/Obeliskial_Content/).


<details>
<summary>Hero Overview</summary>

### Overview
![Fabricator](https://raw.githubusercontent.com/binbinmods/Fabricator/refs/heads/main/Assets/fabricator.png)


### Starter Card
![Defense Drone](https://raw.githubusercontent.com/binbinmods/Fabricator/refs/heads/main/Assets/defensedrone.png)
![Attack Turret](https://raw.githubusercontent.com/binbinmods/Fabricator/refs/heads/main/Assets/attackturret.png)
![Target Locked](https://raw.githubusercontent.com/binbinmods/Fabricator/refs/heads/main/Assets/targetlocked.png)

### Level 1
- Quick Thinking: When you play an Enchantment on a hero grant 1 Inspire and 1 Energize

### Level 2

![Mental Mutilator](https://raw.githubusercontent.com/binbinmods/Fabricator/refs/heads/main/Assets/mentalmutilator.png)
![Elemental Repeater](https://raw.githubusercontent.com/binbinmods/Fabricator/refs/heads/main/Assets/elementalrepeater.png)
![Torso Terror](https://raw.githubusercontent.com/binbinmods/Fabricator/refs/heads/main/Assets/torsoterror.png)

![Hold the Line](https://raw.githubusercontent.com/binbinmods/Fabricator/refs/heads/main/Assets/holdtheline.png)

### Level 3

- Overclocking: Effects on this hero that occur at the start of turn happen twice
- Preparing for Distaster: Taunt +1. Taunt on this hero can stack. This hero gains 5% more Block and Shield for each stack of Taunt.

### Level 4

![Think Fast!](https://raw.githubusercontent.com/binbinmods/Fabricator/refs/heads/main/Assets/thinkfast.png)

![Decoy Kit](https://raw.githubusercontent.com/binbinmods/Fabricator/refs/heads/main/Assets/decoykit.png)

### Level 5

- Great Minds Think Alike: Whenever a different hero plays an Enchantment on a hero, shuffle with Vanish into your deck and theirs. The version in the other hero's deck has the cost increased by 1, the version in your deck has the cost unchanged. (3x/round)
- The Great Wall: At end of turn, all heroes gain 10 Block and Shield for every Taunt you have

</details>


## Installation (manual)

1. Install [Obeliskial Essentials](https://across-the-obelisk.thunderstore.io/package/meds/Obeliskial_Essentials/) and [Obeliskial Content](https://across-the-obelisk.thunderstore.io/package/meds/Obeliskial_Content/).
2. Click _Manual Download_ at the top of the page.
3. In Steam, right-click Across the Obelisk and select _Manage_->_Browse local files_.
4. Extract the archive into the game folder. Your _Across the Obelisk_ folder should now contain a _BepInEx_ folder and a _doorstop\_libs_ folder.
5. Run the game. If everything runs correctly, you will see this mod in the list of registered mods on the main menu.
6. Press F5 to open/close the Config Manager and F1 to show/hide mod version information.
7. Note: I am not certain about these install instructions. In the worst case, just copy _TheWiseWolf.dll_ into the _BepInEx\plugins_ folder, and the _The Steel Spartan_ folder (the one with the subfolders containing the json files) into _BepInEx\config\Obeliskial\_importing_

## Installation (automatic)

1. Download and install [Thunderstore Mod Manager](https://www.overwolf.com/app/Thunderstore-Thunderstore_Mod_Manager) or [r2modman](https://across-the-obelisk.thunderstore.io/package/ebkr/r2modman/).
2. Click **Install with Mod Manager** button on top of the page.
3. Run the game via the mod manager.

## Support

This has been updated for version 1.5.0.

Hope you enjoy it and if have any issues, ping me in Discord or make a post in the **modding #support-and-requests** channel of the [official Across the Obelisk Discord](https://discord.gg/across-the-obelisk-679706811108163701).

## Donation

Please do not donate to me. If you wish to support me, I would prefer it if you just gave me feedback. 