﻿using System.Collections.Generic;
using System.Linq;

namespace Common.Utilities
{
    public static class Definitions
    {
        private static readonly Dictionary<string, string> definitionsDictionary = new()
        {
            { "2591", "Potion of Attack" },
            { "2592", "Potion of Defense"},
            { "2593", "Potion of Speed" },
            { "2636", "Potion of Dexterity" },
            { "2613", "Potion of Wisdom" },
            { "2612", "Potion of Vitality" },
            { "2794", "Potion of Mana" },
            { "2793", "Potion of Life" },
            { "116", "Any stat pot" },
            { "9065", "Greater Potion of Defense" },
            { "9064", "Greater Potion of Attack" },
            { "9066", "Greater Potion of Speed" },
            { "9069", "Greater Potion of Dexterity" },
            { "9068", "Greater Potion of Wisdom" },
            { "9067", "Greater Potion of Vitality" },
            { "9071", "Greater Potion of Mana" },
            { "9070", "Greater Potion of Life" },
            { "1826", "Wine Cellar Incantation" },
            { "10024", "Helmet Rune" },
            { "10023", "Shield Rune" },
            { "10022", "Sword Rune" },
            { "1808", "Tomb of the Ancients Key" },
            { "3089", "Ocean Trench Key" },
            { "1802", "Abyss of Demons Key" },
            { "3107", "Manor Key" },
            { "3133", "Beachzone Key" },
            { "3170", "Candy Key" },
            { "3278", "Deadwater Docks Key" },
            { "3311", "Battle Nexus Key" },
            { "3293", "Shatters Key" },
            { "8852", "Shaitan's Key" },
            { "29804", "Puppet Master's Encore Key" },
            { "303", "Mountain Temple Key" },
            { "4252", "The Nest Key" },
            { "2991", "Lost Halls Key" },
            { "2555", "Reef Key" },
            { "3872", "Mad God Mayhem Key" },
            { "13984", "Secluded Thicket Key" },
            { "1404", "Cursed Library Key" },
            { "9326", "Heroic Undead Lair Key" },
            { "9325", "Heroic Abyss of Demons Key" },
            { "1413", "Malogia Key" },
            { "1415", "Forax Key" },
            { "1412", "Fungal Cavern Key" },
            { "19301", "The Third Dimension Key" },
            { "1799", "Healing Ichor" },
            { "3105", "Holy Water" },
            { "2868", "Fire Water" },
            { "2875", "Muscat" },
            { "2871", "Melon Liqueur" },
            { "3090", "Coral Juice" },
            { "2736", "Minor Health Potion" },
            { "2781", "Minor Magic Potion" },
            { "3109", "Ghost Pirate Rum" },
            { "3137", "Transformation Potion" },
            { "3267", "Saint Paddy's Brew" },
            { "3129", "Bahama Sunrise" },
            { "3130", "Blue Paradise" },
            { "3131", "Pink Passion Breeze" },
            { "3132", "Lime Jungle Bay" },
            { "1409", "Server Heart" },
            { "2835", "Effusion of Defense" },
            { "3436", "Effusion of Attack" },
            { "3437", "Effusion of Speed" },
            { "2832", "Effusion of Dexterity" },
            { "3439 ", "Effusion of Wisdom" },
            { "3438", "Effusion of Vitality" },
            { "2833", "Effusion of Life" },
            { "3432", "Tincture of Attack" },
            { "3433", "Tincture of Speed" },
            { "2828", "Tincture of Dexterity" },
            { "19080", "Amaranth Nildrop" },
            { "19083", "Scarlet Nildrop" },
            { "19085", "Azure Nildrop" },
            { "19084", "Crimson Nildrop" },
            { "19086", "Hazel Nildrop" },
            { "19087", "Teal Nildrop" },
            { "19079", "Force Nildrop" },
            { "19078", "Cinder Nildrop" },
            { "9077", "Blood Nildrop" },
            { "2799", "White Drake Egg" },
            { "2800", "Blue Drake Egg" },
            { "2801", "Orange Drake Egg" },
            { "2802", "Green Drake Egg" },
            { "2641", "Magic Mushroom" },
            { "5407", "Valentine" },
            { "8779", "Potion of Health1" },
            { "8798", "Potion of Health2" },
            { "8814", "Slime Archer Skin" },
            { "8815", "Brigand Skin" },
            { "8827", "Agent Skin" },
            { "8828", "Slime Knight Skin" },
            { "8829", "Slime Priest Skin" },
            { "8830", "Bashing Bride Skin" },
            { "8831", "Eligible Bachelor Skin" },
            { "8858", "Nun Skin" },
            { "8859", "Witch Doctor Skin" },
            { "8860", "Sorceress Skin" },
            { "8861 ", "Artemis Skin" },
            { "8781", "Shoveguy Skin" },
            { "8812", "Scarlett Skin" },
            { "9615", "Slime Ninja Skin" },
            { "8850", "Slime Wizard Skin" },
            { "8851", "Deadly Vixen Skin" },
            { "8856", "Stanley the Spring Bunny Skin" },
            { "8993", "Nexus no Miko Skin" },
            { "8995", "Drow Trickster Skin" },
            { "8983", "B.B. Wolf Skin" },
            { "8984", "Lil Red Skin" },
            { "8783", "Holy Avenger Skin" },
            { "8796", "Ranger Skin" },
            { "8985", "King Knifeula Skin" },
            { "9011", "Witch Skin" },
            { "8980", "Platinum Knight Skin" },
            { "8981", "Platinum Warrior Skin" },
            { "8982", "Platinum Rogue Skin" },
            { "9033", "Slime Sorcerer Skin" },
            { "9034", "Slime Trickster Skin" },
            { "9035", "Slime Mystic Skin" },
            { "9036", "Slime Huntress Skin" },
            { "9037", "Slime Necromancer Skin" },
            { "9038", "Slime Assassin Skin" },
            { "9039", "Slime Paladin Skin" },
            { "9040", "Slime Rogue Skin" },
            { "9041", "Slime Warrior Skin" },
            { "29769", "Puppet Master Skin" },
            { "29772,", "Jester Skin" },
            { "9074", "Ghost Huntress Skin" },
            { "9075", "Skeleton Warrior Skin" },
            { "29819", "Huntsman Skin" },
            { "29820", "Demon Spawn Skin" },
            { "29821", "Hunchback Skin" },
            { "29822", "Vampiress Skin" },
            { "29823", "Frankensteins Monster Skin" },
            { "29824", "Jack the Ripper Skin" },
            { "29825", "Death Skin" },
            { "29826", "Tiny Avatar Skin" },
            { "20827", "Zombie Nurse Skin" },
            { "29828", "Ascended Sorcerer Skin" },
            { "29829", "Mischievous Imp Skin" },
            { "29830", "Vampire Hunter Skin" },
            { "29831", "Poltergeist Skin" },
            { "29832", "Dark Elf Huntress Skin" },
            { "9076", "Infected Skin" },
            { "3312", "Santa Skin" },
            { "3313", "Little Helper Skin" },
            { "3320", "Iceman Skin" },
            { "6874", "Santa Trickster Skin" },
            { "8319", "Oryxmas Rogue Skin" },
            { "8320", "Oryxmas Archer Skin" },
            { "8321", "Oryxmas Wizard Skin" },
            { "8322", "Oryxmas Priest Skin" },
            { "8323", "Oryxmas Warrior Skin" },
            { "8324 ", "Oryxmas Knight Skin" },
            { "8325", "Oryxmas Paladin Skin" },
            { "8326", "Oryxmas Assassin Skin" },
            { "8327", "Oryxmas Necromancer Skin" },
            { "8328", "Oryxmas Huntress Skin" },
            { "8329", "Oryxmas Mystic Skin" },
            { "8330", "Oryxmas Trickster Skin" },
            { "8331", "Oryxmas Sorcerer Skin" },
            { "8332", "Oryxmas Ninja Skin" },
            { "19660", "Oryxmas Samurai Skin" },
            { "9382", "Slime Bard Skin" },
            { "9390", "Oryxmas Bard Skin" },
            { "30720", "Slime Summoner Skin" },
            { "30804", "Oryxmas Summoner Skin" },
            { "31233", "Oryxmas Kensei Skin" },
            { "2721", "Staff of Diabolic Secrets T10" },
            { "2722", "Staff of Astral Knowledge T11" },
            { "2824", "Staff of the Cosmic Whole T12" },
            { "2513", "Staff of the Vital Unity T13" },
            { "3075", "Staff of Extreme Prejudice UT" },
            { "5847 ", "Staff of Iceblast UT" },
            { "3108", "Anatis Staff UT" },
            { "3306", "KoalaPOW UT" },
            { "8843", "Staff of Adoration UT" },
            { "8857", "Staff of the Rising Sun UT" },
            { "8998", "Barely Attuned Magic Thingy UT" },
            { "3859", "Staff of Horrific Knowledge UT" },
            { "9059", "The Phylactery UT" },
            { "9086", "Staff of Yuletide Carols UT" },
            { "8615", "Legacy Sentient Staff UT" },
            { "14493", "Staff of Silver Wings UT" },
            { "2112", "Spirit Staff UT" },
            { "25688", "Foramite Staff UT" },
            { "6860", "Frosty's Walking Stick UT" },
            { "1894", "Staff of Esben UT" },
            { "12069", "Screwdriver Staff UT" },
            { "20977", "Staff of the Saint UT" },
            { "1394", "K.I.D.D. Force UT" },
            { "2694", "Wand of Shadow T10" },
            { "2695", "Wand of Ancient Warning T11" },
            { "2806", "Wand of Recompense T12" },
            { "2506", "Wand of Evocation T13" },
            { "3076", "Wand of the Bulwark UT" },
            { "5848", "Eternal Snowflake Wand UT" },
            { "3101", "St. Abraham's Wand UT" },
            { "3123", "Conducting Wand UT" },
            { "3307", "Spicy Wand of Spice UT" },
            { "8999", "Lethargic Sentience UT" },
            { "8854", "Wand of Egg" },
            { "3856", "Wand of Ancient Terror UT" },
            { "9055", "Wand of Geb UT" },
            { "9084", "Present Dispensing Wand UT" },
            { "14494", "Wand of the Summoned One UT" },
            { "6861", "Winter's Breath Wand UT" },
            { "24834", "Soul's Guidance UT" },
            { "5138,", "Frozen Wand UT" },
            { "1398", "Caduceus of Current Craziness UT" },
            { "2635", "Sprite Wand UT" },
            { "2700", "Bow of Fey Magic T10" },
            { "2701", "Bow of Innocent Blood T11" },
            { "2818", "Bow of Covert Havens T12" },
            { "2508", "Bow of Mystical Energy T13" },
            { "6858", "Icicle Launcher UT" },
            { "3074", "Doom Bow UT" },
            { "8961", "Bow of the Morning Star UT" },
            { "25696", "Sun's Judgement UT" },
            { "3088", "Coral Bow UT" },
            { "5849", "Arctic Bow UT" },
            { "3308", "Robobow UT" },
            { "8997", "Precisely Calibrated Stringstick UT" },
            { "9610", "Bow of Eternal Frost UT" },
            { "14492", "Bow of the Sky's Glory UT" },
            { "12067", "Nail Gun Bow UT" },
            { "5706", "Clover Bow UT" },
            { "1397", "Butter Bow UT" },
            { "2697", "Emeraldshard Dagger T10" },
            { "2698", "Agateclaw Dagger T11" },
            { "2815", "Dagger of Foul Malevolence T12" },
            { "2502", "Dagger of Sinister Deeds T13" },
            { "2849", "Chicken Leg of Doom UT" },
            { "3106", "Bone Dagger UT" },
            { "3113", "Spirit Dagger UT" },
            { "1801", "Poison Fang Dagger UT" },
            { "3309", "Sunshine Shiv UT" },
            { "8845", "Heartfind Dagger UT" },
            { "9002", "Dagger of the Amethyst Prism UT" },
            { "9000", "Toy Knife UT" },
            { "3857", "Dagger of the Terrible Talon UT" },
            { "8608", "Legacy Etherite Dagger UT" },
            { "9085", "An Icicle UT" },
            { "14491", "Dagger of Eternal Chaos UT" },
            { "12068", "Spatula Dagger UT" },
            { "2201", "Dagger of the Hasteful Rabbit UT" },
            { "20983", "Prismatic Slasher UT" },
            { "1408", "Corruption Cutter UT" },
            { "25698", "Dueling Daggers UT" },
            { "6859", "Frost Lich's Finger UT" },
            { "1396", "Mister Mango UT" },
            { "2692", "Archon Sword T10" },
            { "2631", "Skysplitter Sword T11" },
            { "2827", "Sword of Acclaim T12" },
            { "2504", "Sword of Splendor T13" },
            { "3073", "Demon Blade UT" },
            { "8963", "Sword of Illumination UT" },
            { "3291", "Pirate King's Cutlass UT" },
            { "3295", "Doctor Swordsworth UT" },
            { "8732", "Skull" },
            { "8846", "Vinesword UT" },
            { "9659", "Arcane Rapier UT" },
            { "8962", "Sword of the Mad God UT" },
            { "8996", "Unstable Anomaly UT" },
            { "9063", "Legacy Pixie" },
            { "9612", "Frostbite UT" },
            { "19212", "Keychain Cutlass UT" },
            { "14495", "Sword of the Unending Chaos UT" },
            { "25691", "Gaseous Glaive UT" },
            { "5846", "Enchanted Ice Blade UT" },
            { "6862", "Saint Nicolas' Blade UT" },
            { "12071", "Hammer Sword UT" },
            { "1705", "Apocalypse Feather UT" },
            { "5707", "Sword of the Rainbow's End UT" },
            { "23334", "Silex's Hammer UT" },
            { "1395", "MMace MMurderer UT" },
            { "3150", "Ichimonji T10" },
            { "3151", "Muramasa T11" },
            { "3152", "Masamune T12" },
            { "586", "Sadamune T13" },
            { "3292", "Doku No Ken UT" },
            { "3310", "Arbiter's Wrath UT" },
            { "8842", "Diamond" },
            { "3860", "Corrupted Cleaver UT" },
            { "9087", "Salju UT" },
            { "14497", "Skybound Blade UT" },
            { "25693", "Fire Blade UT" },
            { "12072", "Sawblade Katana UT" },
            { "1399", "Blade of Ages UT" },
            { "29037", "Void Blade UT" },
            { "6863", "Yuki UT" },
            { "23332", "Krathana UT" },
            { "2709", "Robe of the Moon Wizard T11" },
            { "2710", "Robe of the Elder Warlock T12" },
            { "2821", "Robe of the Grand Sorcerer T13" },
            { "2511", "Robe of the Star Mother T14" },
            { "8863", "Robe of the Summer Solstice UT" },
            { "3103", "Chasuble of Holy Light UT" },
            { "3122", "Robe of the Mad Scientist UT" },
            { "3203", "Cheater Robe UT" },
            { "3092", "Robe of the Tlatoani UT" },
            { "9052", "Shendyt of Geb UT" },
            { "9056", "Soulless Robe UT" },
            { "8616", "Legacy Robe of Twilight UT" },
            { "14499", "Robe of Dark Summoning UT" },
            { "-19465", "Esben's Shaman Attire UT" },
            { "5851", "Frost Elementalist Robe UT" },
            { "12073", "Magic Construction Vest UT" },
            { "14074", "Tlatoani's Shroud UT" },
            { "2706", "Vengeance Armor T11" },
            { "2707", "Abyssal Armor T12" },
            { "2812", "Acropolis Armor T13" },
            { "2500", "Dominion Armor T14" },
            { "9015", "Almandine Armor of Anger UT" },
            { "3169", "Candy" },
            { "3182", "Resurrected Warrior's Armor UT" },
            { "3204", "Cheater Heavy Armor UT" },
            { "9060", "Legacy Fairy Plate UT" },
            { "14500", "Armor of Heavenly Light UT" },
            { "5853", "Frost Citadel Armor UT" },
            { "12075", "Heavy Construction Vest UT" },
            { "2703", "Hippogriff Hide Armor T11" },
            { "2704", "Griffon Hide Armor T12" },
            { "2809", "Hydra Skin Armor T13" },
            { "2497", "Wyrmhide Armor T14" },
            { "3096", "Coral Silk Armor UT" },
            { "3112", "Spectral Cloth Armor UT" },
            { "3202", "Cheater Light Armor UT" },
            { "8609", "Legacy Mantle of Skuld UT" },
            { "14498", "Shadowbeast Hide UT" },
            { "4308", "Beehemoth Armor UT" },
            { "12074", "Fitted Construction Vest UT" },
            { "5850", "Frost Drake Hide Armor UT" },
            { "15717", "Greaterhosen UT" },
            { "29768", "Harlequin Armor UT" },
            { "2785", "Cloak of Endless Twilight T5" },
            { "2855", "Cloak of Ghostly Concealment T6" },
            { "2650", "Cloak of the Planewalker UT" },
            { "8610", "Legacy Ghastly Drape UT" },
            { "8333", "Cloak of Winter UT" },
            { "2661", "Golden Quiver T5" },
            { "2856", "Quiver of Elvish Mastery T6" },
            { "8336", "Coalbearing Quiver UT" },
            { "23337", "Archerang UT" },
            { "2608", "Magic Nova Spell T5" },
            { "2852", "Elemental Detonation Spell T6" },
            { "8862", "Thousand Suns Spell UT" },
            { "8617", "Legacy Ancient Spell" },
            { "8338", "Vigil Spell UT" },
            { "23330", "Random Spell Extraction Device UT" },
            { "23339", "Spelling Spell UT" },
            { "2651", "Tome of Divine Favor T5" },
            { "2853", "Tome of Holy Guidance T6" },
            { "3081", "Tome of Purification UT" },
            { "9054", "Book of Geb UT" },
            { "8342", "Nativity Tome UT" },
            { "23341", "Tome of Moral Support UT" },
            { "1405", "Necronomicon UT" },
            { "2667", "Golden Helm T5" },
            { "2857", "Helm of the Great General T6" },
            { "8335", "Pathfinder's Helm UT"},
            { "9661", "Amber Encrusted Helmet UT" },
            { "2572", "Mithril Shield T5" },
            { "2850", "Colossus Shield T6" },
            { "9017", "Onyx Shield of the Mad God UT" },
            { "2624", "Snakeskin Shield UT" },
            { "8340", "Resounding Shield UT" },
            { "23344", "Shield of Pogmur UT" },
            { "2645", "Seal of the Holy Warrior T5" },
            { "2854", "Seal of the Blessed Champion T6" },
            { "9062", "Legacy Seal of the Enchanted Forest UT" },
            { "8344", "Advent Seal UT" },
            { "1406", "Scholar's Seal UT" },
            { "9660", "Sandstone Seal UT" },
            { "2728", "Nightwing Venom T5" },
            { "2858", "Baneserpent Poison T6" },
            { "3181", "Plague Poison UT" },
            { "8343", "Holly Poison UT" },
            { "15718", "Mighty Stein UT" },
            { "23347", "Lightning in a Bottle UT" },
            { "29040", "Murky Toxin UT" },
            { "2735", "Lifedrinker Skull T5" },
            { "2859", "Bloodsucker Skull T6" },
            { "3094", "Cracked Crystal Skull UT" },
            { "8337", "Skull of Krampus UT" },
            { "23349", "Epiphany Skull UT" },
            { "1895", "Skullish Remains of Esben UT" },
            { "13987", "Sealed Crystal Skull UT" },
            { "2742", "Dragonstalker Trap T5" },
            { "2860", "Giantcatcher Trap T6" },
            { "8339", "Greedsnatcher Trap UT" },
            { "25109", "Painbow UT" },
            { "19252", "Mimicry Trap UT" },
            { "23351", "Helium Trap UT" },
            { "2630", "Banishment Orb T5" },
            { "2861", "Planefetter Orb T6" },
            { "9058", "Soul of the Bearer UT" },
            { "8334", "Snowbound Orb UT" },
            { "2848", "Prism of Phantoms T5" },
            { "2851", "Prism of Apparitions T6" },
            { "3114", "Ghostly Prism UT" },
            { "8341", "Ornamental Prism UT" },
            { "1904", "Heart of Gold Prism UT" },
            { "2322", "Prism of Dancing Swords UT" },
            { "23353", "Fool's Prism UT" },
            { "19254", "Prismimic UT" },
            { "2866", "Scepter of Skybolts T5" },
            { "2867", "Scepter of Storms T6" },
            { "3120", "Scepter of Fulmination UT" },
            { "8346", "Scepter of Sainthood UT" },
            { "23354", "Honey Scepter Supreme UT" },
            { "310", "Honey Scepter UT" },
            { "3160", "Ice Star T5" },
            { "3161", "Doom Circle T6" },
            { "8345", "Ilex Star UT" },
            { "23355", "Unshuriken UT" },
            { "29718", "Clover Star UT" },
            { "6831", "Jade" },
            { "6832", "Royal Wakizashi T6" },
            { "613", "Peppermint Wakizashi UT" },
            { "23357", "NSFWakizashi UT" },
            { "1407", "Ronin's Wakizashi UT" },
            { "-12298", "Regal Lute T5" },
            { "-12297", "Skyward Lute T6" },
            { "12167", "Oryxmas Carol UT" },
            { "19950", "Snake Charmer Pungi UT" },
            { "14408", "Monarch Mace T5" },
            { "14409", "Sovereign Mace T6" },
            { "14341", "Mace of the North Pole UT" },
            { "14613", "Serpentine Sheath T5" },
            { "14614", "Great Shinobi Sheath T6" },
            { "14662", "Snowman's Sheath UT" },
            { "14661", "Paper Machete UT" },
            { "2752", "Ring of Paramount Defense T4" },
            { "2757", "Ring of Paramount Health T4" },
            { "2759", "Ring of Exalted Attack T5" },
            { "2760", "Ring of Exalted Defense T5" },
            { "2761", "Ring of Exalted Speed T5" },
            { "2762", "Ring of Exalted Vitality T5" },
            { "2763", "Ring of Exalted Wisdom T5" },
            { "2764", "Ring of Exalted Dexterity T5" },
            { "2765", "Ring of Exalted Health T5" },
            { "2766", "Ring of Exalted Magic T5" },
            { "9016", "Almandine Ring of Wrath UT" },
            { "8960", "Ring of the Burning Sun UT" },
            { "2979", "Ring of Unbound Attack T6" },
            { "2980", "Ring of Unbound Defense T6" },
            { "2981", "Ring of Unbound Speed T6" },
            { "2982", "Ring of Unbound Vitality T6" },
            { "2983", "Ring of Unbound Wisdom T6" },
            { "2984", "Ring of Unbound Dexterity T6" },
            { "2985", "Ring of Unbound Health T6" },
            { "2986", "Ring of Unbound Magic T6" },
            { "2990", "Ring of Decades UT" },
            { "2976", "Ring of the Sphinx UT" },
            { "3121", "Experimental Ring UT" },
            { "3167", "Candy Ring UT" },
            { "2878", "Amulet of Dispersion UT" },
            { "2625", "Snake Eye Ring UT" },
            { "25788", "Oryxmas Ornament" },
            { "4058", "Mask of Anubis UT" },
            { "4059", "Mask of Lightning UT" },
            { "4061", "Mask of Mucus UT" },
            { "4060", "Mask of Cnidaria UT" },
            { "19250", "Crystal Key UT" },
            { "32722", "Enchanted Ice Shard UT" },
            { "-19464", "Esben's Wedding Ring UT" },
            { "9053", "Geb's Ring of Wisdom UT" },
            { "9057", "Ring of the Covetous Heart UT" },
            { "9061", "Legacy Ring of Pure Wishes UT" },
            { "8611", "Legacy Spectral Ring of Horrors UT" },
            { "8618", "Legacy Forgotten Ring UT" },
            { "9669", "The Forgotten Ring UT" },
            { "3091", "Coral Ring UT" },
            { "3104", "Ring of Divine Faith UT" },
            { "3111", "Captain's Ring UT" },
            { "1800", "Spider's Eye Ring UT" },
            { "2109", "Fairy Ring UT" },
            { "3184", "Golden Ankh" },
            { "3185", "Eye of Osiris" },
            { "3186", "Pharaoh's Mask" },
            { "3187", "Golden Cockle" },
            { "3188", "Golden Conch" },
            { "3189", "Golden Horn Conch" },
            { "3190", "Golden Nut" },
            { "3191", "Golden Bolt" },
            { "3192", "Golden Femur" },
            { "3193", "Golden Ribcage" },
            { "3194", "Golden Skull" },
            { "3195", "Golden Candelabra" },
            { "3196", "Holy Cross" },
            { "3197", "Pearl Necklace" },
            { "3198", "Golden Chalice" },
            { "3199", "Ruby Gemstone" },
            { "9018", "The Devil Tarot Card" },
            { "9019", "The Sun Tarot Card" },
            { "9020", "Death Tarot Card" },
            { "9021", "The Tower Tarot Card" },
            { "9022", "The Magician Tarot Card" },
            { "9023", "The World Tarot Card" },
            { "9024", "The Chariot Tarot Card" },
            { "3861", "The Fool Tarot Card" },
            { "3205", "Common Feline Egg" },
            { "3209", "Common Canine Egg" },
            { "3213", "Common Avian Egg" },
            { "3217", "Common Exotic Egg" },
            { "3221", "Common Farm Egg" },
            { "3225", "Common Woodland Egg" },
            { "3229", "Common Reptile Egg" },
            { "3233", "Common Insect Egg" },
            { "3237", "Common Penguin Egg" },
            { "3241", "Common Aquatic Egg" },
            { "3245", "Common Spooky Egg" },
            { "3249", "Common Humanoid Egg" },
            { "3253", "Common ???? Egg" },
            { "3257", "Common Automaton Egg" },
            { "3261", "Common Mystery Egg" },
            { "-117", "Any common pet egg" },
            { "3206", "Uncommon Feline Egg" },
            { "3210", "Uncommon Canine Egg" },
            { "3214", "Uncommon Avian Egg" },
            { "3218", "Uncommon Exotic Egg" },
            { "3222", "Uncommon Farm Egg" },
            { "3226", "Uncommon Woodland Egg" },
            { "3230", "Uncommon Reptile Egg" },
            { "3234", "Uncommon Insect Egg" },
            { "3238", "Uncommon Penguin Egg" },
            { "3242", "Uncommon Aquatic Egg" },
            { "3246", "Uncommon Spooky Egg" },
            { "3250", "Uncommon Humanoid Egg" },
            { "3254", "Uncommon ???? Egg" },
            { "3258", "Uncommon Automaton Egg" },
            { "3262", "Uncommon Mystery Egg" },
            { "-118", "Any uncommon pet egg" },
            { "3207", "Rare Feline Egg" },
            { "3215", "Rare Avian Egg" },
            { "3219", "Rare Exotic Egg" },
            { "3223", "Rare Farm Egg" },
            { "3227", "Rare Woodland Egg" },
            { "3231", "Rare Reptile Egg" },
            { "3239", "Rare Penguin Egg" },
            { "3247", "Rare Spooky Egg" },
            { "3251", "Rare Humanoid Egg" },
            { "3255", "Rare ???? Egg" },
            { "3259", "Rare Automaton Egg" },
            { "3263", "Rare Mystery Egg" },
            { "-119", "Any rare pet egg" },
            { "-101", "Any item with 200 or more feed power" },
            { "-102", "Any item with 300 or more feed power" },
            { "-103", "Any item with 400 or more feed power" },
            { "-104", "Any item with 450 or more feed power"},
            { "-105", "Any item with 500 or more feed power" },
            { "-106", "Any item with 550 or more feed power" },
            { "-107", "Any item with 600 or more feed power" },
            { "-108", "Any item with 650 or more feed power" },
            { "-109", "Any item with 700 or more feed power" },
            { "-110", "Any item with 800 or more feed power" },
            { "-111", "Any item with 900 or more feed power" },
            { "-112", "Any item with 1000 or more feed power" },
            { "-115", "Any item with 1300 or more feed power" },
            { "-120", "Any untiered item providing 15 common forge material when dismantled" },
            { "-121", "Any untiered item providing 5 rare forge material when dismantled" },
            { "-122", "Any untiered item providing 15 rare forge material when dismantled" },
        };

        public static string GetItemNameFromItemId(string itemId)
        {
            if (!definitionsDictionary.ContainsKey(itemId)) return "Could not find item id in the dictionary";
            return definitionsDictionary[itemId];
        }


        //// Used at all? e.g. {1234, ringofdecadesut}
        //public static string GetItemNameNoSpacesFromItemId(string itemId)
        //{
        //    var idToItemNameDictionary = definitionsDictionary;
        //    foreach (var (key, val) in idToItemNameDictionary)
        //    {
        //        idToItemNameDictionary.Add(key, $"{val.ToLower().Replace(" ", "")}");
        //    }

        //    if (!idToItemNameDictionary.ContainsKey(idToItemNameDictionary[itemId])) return "Could not find item id in the dictionary";

        //    return idToItemNameDictionary[itemId];
        //}

        public static string GetItemIdFromItemNameNoSpaces(string itemNameNoSpaces)
        {
            var itemNameToIdDictionary = ConvertToItemNameToIdNoSpacesDictionary();
            if (!itemNameToIdDictionary.ContainsKey(itemNameNoSpaces)) return "Could not find item name in the dictionary";
            return itemNameToIdDictionary[itemNameNoSpaces];
        }

        public static List<string> GetAllItemNames()
        {
            return definitionsDictionary.Values.ToList();
        }

        private static Dictionary<string, string> ConvertToItemNameToIdNoSpacesDictionary()
        {
            var itemNameToIdDictionary = new Dictionary<string, string>();

            foreach (var (key, val) in definitionsDictionary)
            {
                itemNameToIdDictionary.Add($"{val.ToLower().Replace(" ", "")}", key);
            }

            return itemNameToIdDictionary;
        }
    }
}
