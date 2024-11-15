using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI.Events;
using StardewModdingAPI;
using StardewValley;
using Microsoft.Xna.Framework.Graphics;
using System.Security;
using StardewValley.Objects;
using StardewValley.Extensions;
using StardewValley.Quests;
using StardewValley.Buildings;
using Microsoft.Xna.Framework.Input;
using StardewValley.ItemTypeDefinitions;
using StardewValley.GameData;
using StardewValley.Monsters;
using StardewValley.Locations;
using xTile;
using Microsoft.Xna.Framework;
using static System.Net.WebRequestMethods;
using StardewValley.Internal;

namespace StardewValleyDataManagement
{
    internal class ModEntry : Mod
    {

        private Dictionary<string, string> categorySprites = new Dictionary<string, string>()
        {
            { "shipped", "https://stardewvalleywiki.com/mediawiki/images/1/1c/Mini-Shipping_Bin.png" },
            { "level-up", "https://stardewvalleywiki.com/mediawiki/images/d/df/Mastery_Icon.png" },
            { "fish", "https://stardewvalleywiki.com/mediawiki/images/e/e7/Fishing_Skill_Icon.png" },
            { "milestone", "https://stardewvalleywiki.com/mediawiki/images/b/b8/Golden_Scroll.png" },
            { "crafting", "https://stardewvalleywiki.com/mediawiki/images/e/ea/Workbench.png" },
            { "friendship", "https://stardewvalleywiki.com/mediawiki/images/6/63/DialogueBubbleLove.png" },
            { "quest", "https://stardewvalleywiki.com/mediawiki/images/d/de/Bulletin_Board.png" },
            { "artifacts", "https://stardewvalleywiki.com/mediawiki/images/8/82/Artifact_Trove.png" },
            { "minerals", "https://stardewvalleywiki.com/mediawiki/images/0/09/Omni_Geode.png" },
            { "cooking", "https://stardewvalleywiki.com/mediawiki/images/4/4d/Mini-Fridge.png" },
            { "achievement", "https://stardewvalleywiki.com/mediawiki/images/5/5b/Achievement_Star_04.png" },
            { "bundle-green", "https://stardewvalleywiki.com/mediawiki/images/thumb/b/b3/Bundle_Green.png/32px-Bundle_Green.png" },
            { "bundle-yellow", "https://stardewvalleywiki.com/mediawiki/images/thumb/2/2b/Bundle_Yellow.png/32px-Bundle_Yellow.png" },
            { "bundle-orange", "https://stardewvalleywiki.com/mediawiki/images/thumb/9/95/Bundle_Orange.png/32px-Bundle_Orange.png" },
            { "bundle-teal", "https://stardewvalleywiki.com/mediawiki/images/thumb/c/ce/Bundle_Teal.png/32px-Bundle_Teal.png" },
            { "bundle-red", "https://stardewvalleywiki.com/mediawiki/images/thumb/3/3f/Bundle_Red.png/32px-Bundle_Red.png" },
            { "bundle-purple", "https://stardewvalleywiki.com/mediawiki/images/thumb/a/a1/Bundle_Purple.png/32px-Bundle_Purple.png" },
            { "bundle-blue", "https://stardewvalleywiki.com/mediawiki/images/thumb/e/e4/Bundle_Blue.png/32px-Bundle_Blue.png" },
            { "monster-slayer", "https://stardewvalleywiki.com/mediawiki/images/3/37/Marlon.png" },
            { "wallet", "https://stardewvalleywiki.com/mediawiki/images/3/36/36_Backpack.png" },
            { "island-field-office", "https://stardewvalleywiki.com/mediawiki/images/6/68/Professor_Snail_Happy.png" },
            { "books", "https://stardewvalleywiki.com/mediawiki/images/f/f3/Lost_Book.png" },
            { "powers", "https://stardewvalleywiki.com/mediawiki/images/5/51/Grandpa_Speaking.png" }
        };

        private Dictionary<string, string> miscSprites = new Dictionary<string, string>()
        {
            { "stardrop", "https://stardewvalleywiki.com/mediawiki/images/a/a5/Stardrop.png" },
            { "combat", "https://stardewvalleywiki.com/mediawiki/images/c/cf/Combat_Skill_Icon.png" },
            { "mining", "https://stardewvalleywiki.com/mediawiki/images/2/2f/Mining_Skill_Icon.png" },
            { "fishing", "https://stardewvalleywiki.com/mediawiki/images/e/e7/Fishing_Skill_Icon.png" },
            { "farming", "https://stardewvalleywiki.com/mediawiki/images/8/82/Farming_Skill_Icon.png" },
            { "foraging", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Foraging_Skill_Icon.png" },
            { "large-pack", "https://stardewvalleywiki.com/mediawiki/images/b/b1/Backpack.png" },
            { "deluxe-pack", "https://stardewvalleywiki.com/mediawiki/images/3/36/36_Backpack.png" },
            { "copper-pickaxe", "https://stardewvalleywiki.com/mediawiki/images/d/d7/Copper_Pickaxe.png" },
            { "steel-pickaxe", "https://stardewvalleywiki.com/mediawiki/images/d/d1/Steel_Pickaxe.png" },
            { "gold-pickaxe", "https://stardewvalleywiki.com/mediawiki/images/d/d3/Gold_Pickaxe.png" },
            { "iridium-pickaxe", "https://stardewvalleywiki.com/mediawiki/images/c/ca/Iridium_Pickaxe.png" },
            { "copper-axe", "https://stardewvalleywiki.com/mediawiki/images/2/21/Copper_Axe.png" },
            { "steel-axe", "https://stardewvalleywiki.com/mediawiki/images/c/ce/Steel_Axe.png" },
            { "gold-axe", "https://stardewvalleywiki.com/mediawiki/images/2/29/Gold_Axe.png" },
            { "iridium-axe", "https://stardewvalleywiki.com/mediawiki/images/8/8b/Iridium_Axe.png" },
            { "copper-watering-can", "https://stardewvalleywiki.com/mediawiki/images/0/0d/Copper_Watering_Can.png" },
            { "steel-watering-can", "https://stardewvalleywiki.com/mediawiki/images/6/66/Steel_Watering_Can.png" },
            { "gold-watering-can", "https://stardewvalleywiki.com/mediawiki/images/7/74/Gold_Watering_Can.png" },
            { "iridium-watering-can", "https://stardewvalleywiki.com/mediawiki/images/5/5e/Iridium_Watering_Can.png" },
            { "copper-hoe", "https://stardewvalleywiki.com/mediawiki/images/8/8f/Copper_Hoe.png" },
            { "steel-hoe", "https://stardewvalleywiki.com/mediawiki/images/f/f5/Steel_Hoe.png" },
            { "gold-hoe", "https://stardewvalleywiki.com/mediawiki/images/d/d3/Gold_Hoe.png" },
            { "iridium-hoe", "https://stardewvalleywiki.com/mediawiki/images/5/5f/Iridium_Hoe.png" },
            { "golden-scythe", "https://stardewvalleywiki.com/mediawiki/images/3/33/Golden_Scythe.png" },
            { "fiberglass-rod", "https://stardewvalleywiki.com/mediawiki/images/5/5e/Fiberglass_Rod.png" },
            { "iridium-rod", "https://stardewvalleywiki.com/mediawiki/images/0/05/Iridium_Rod.png" },
            { "copper-trash-can", "https://stardewvalleywiki.com/mediawiki/images/6/6f/Trash_Can_Copper.png" },
            { "steel-trash-can", "https://stardewvalleywiki.com/mediawiki/images/b/b9/Trash_Can_Steel.png" },
            { "gold-trash-can", "https://stardewvalleywiki.com/mediawiki/images/e/e0/Trash_Can_Gold.png" },
            { "iridium-trash-can", "https://stardewvalleywiki.com/mediawiki/images/a/ad/Trash_Can_Iridium.png" },
            { "rarecrow-1", "https://stardewvalleywiki.com/mediawiki/images/6/62/Rarecrow_1.png" },
            { "rarecrow-2", "https://stardewvalleywiki.com/mediawiki/images/2/28/Rarecrow_2.png" },
            { "rarecrow-3", "https://stardewvalleywiki.com/mediawiki/images/e/ea/Rarecrow_3.png" },
            { "rarecrow-4", "https://stardewvalleywiki.com/mediawiki/images/e/ef/Rarecrow_4.png" },
            { "rarecrow-5", "https://stardewvalleywiki.com/mediawiki/images/9/9f/Rarecrow_5.png" },
            { "rarecrow-6", "https://stardewvalleywiki.com/mediawiki/images/2/29/Rarecrow_6.png" },
            { "rarecrow-7", "https://stardewvalleywiki.com/mediawiki/images/5/52/Rarecrow_7.png" },
            { "rarecrow-8", "https://stardewvalleywiki.com/mediawiki/images/b/bb/Rarecrow_8.png" },
            { "gold", "https://stardewvalleywiki.com/mediawiki/images/1/10/Gold.png" },
            { "hay", "https://stardewvalleywiki.com/mediawiki/images/a/aa/Hay.png" },
            { "frozen-geode", "https://stardewvalleywiki.com/mediawiki/images/b/bf/Frozen_Geode.png" },
            { "house-upgrade-1", "https://stardewvalleywiki.com/mediawiki/images/5/5d/House_%28tier_2%29.png" },
            { "house-upgrade-2", "https://stardewvalleywiki.com/mediawiki/images/5/51/House_%28tier_3%29.png" },
            { "house-upgrade-3", "https://stardewvalleywiki.com/mediawiki/images/5/51/House_%28tier_3%29.png" },
            { "wood-1", "https://stardewvalleywiki.com/mediawiki/images/d/df/Wood.png" },
            { "wood-2", "https://stardewvalleywiki.com/mediawiki/images/d/df/Wood.png" },
            { "galaxy-sword", "https://stardewvalleywiki.com/mediawiki/images/4/44/Galaxy_Sword.png" },
            { "infinity-blade", "https://stardewvalleywiki.com/mediawiki/images/4/40/Infinity_Blade.png" },
            { "iridium-snake-milk", "https://stardewvalleywiki.com/mediawiki/images/b/b4/Mr._Qi.png" },
            { "willys-boat", "https://stardewvalleywiki.com/mediawiki/images/0/0c/Willy_Smoking.png" },
            { "water-obelisk", "https://stardewvalleywiki.com/mediawiki/images/b/b7/Water_Obelisk.png" },
            { "earth-obelisk", "https://stardewvalleywiki.com/mediawiki/images/3/3c/Earth_Obelisk.png" },
            { "desert-obelisk", "https://stardewvalleywiki.com/mediawiki/images/6/60/Desert_Obelisk.png" },
            { "island-obelisk", "https://stardewvalleywiki.com/mediawiki/images/1/18/Island_Obelisk.png" },
            { "gold-clock", "https://stardewvalleywiki.com/mediawiki/images/b/b5/Gold_Clock.png" },
            { "statue-of-perfection", "https://stardewvalleywiki.com/mediawiki/images/b/b2/Statue_Of_Perfection.png" },
            { "statue-of-true-perfection", "https://stardewvalleywiki.com/mediawiki/images/d/db/Statue_Of_True_Perfection.png" },
            { "steel-pan", "https://stardewvalleywiki.com/mediawiki/images/4/49/Steel_Pan.png" },
            { "gold-pan", "https://stardewvalleywiki.com/mediawiki/images/6/6e/Gold_Pan.png" },
            { "iridium-pan", "https://stardewvalleywiki.com/mediawiki/images/6/66/Iridium_Pan.png" },
            { "advanced-iridium-rod", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Advanced_Iridium_Rod.png" },
        };

        private Dictionary<string, string> shippedSprites = new Dictionary<string, string>()
        {
            { "wild-horseradish", "https://stardewvalleywiki.com/mediawiki/images/9/90/Wild_Horseradish.png" },
            { "daffodil", "https://stardewvalleywiki.com/mediawiki/images/4/4b/Daffodil.png" },
            { "leek", "https://stardewvalleywiki.com/mediawiki/images/5/57/Leek.png" },
            { "dandelion", "https://stardewvalleywiki.com/mediawiki/images/b/b1/Dandelion.png" },
            { "parsnip", "https://stardewvalleywiki.com/mediawiki/images/d/db/Parsnip.png" },
            { "cave-carrot", "https://stardewvalleywiki.com/mediawiki/images/3/34/Cave_Carrot.png" },
            { "coconut", "https://stardewvalleywiki.com/mediawiki/images/2/2f/Coconut.png" },
            { "cactus-fruit", "https://stardewvalleywiki.com/mediawiki/images/3/32/Cactus_Fruit.png" },
            { "banana", "https://stardewvalleywiki.com/mediawiki/images/6/69/Banana.png" },
            { "sap", "https://stardewvalleywiki.com/mediawiki/images/7/73/Sap.png" },
            { "large-egg-white", "https://stardewvalleywiki.com/mediawiki/images/5/5d/Large_Egg.png" },
            { "egg-white", "https://stardewvalleywiki.com/mediawiki/images/2/26/Egg.png" },
            { "egg-brown", "https://stardewvalleywiki.com/mediawiki/images/0/01/Brown_Egg.png" },
            { "large-egg-brown", "https://stardewvalleywiki.com/mediawiki/images/9/91/Large_Brown_Egg.png" },
            { "milk", "https://stardewvalleywiki.com/mediawiki/images/9/92/Milk.png" },
            { "large-milk", "https://stardewvalleywiki.com/mediawiki/images/6/67/Large_Milk.png" },
            { "green-bean", "https://stardewvalleywiki.com/mediawiki/images/5/5c/Green_Bean.png" },
            { "cauliflower", "https://stardewvalleywiki.com/mediawiki/images/a/aa/Cauliflower.png" },
            { "potato", "https://stardewvalleywiki.com/mediawiki/images/c/c2/Potato.png" },
            { "garlic", "https://stardewvalleywiki.com/mediawiki/images/c/cc/Garlic.png" },
            { "kale", "https://stardewvalleywiki.com/mediawiki/images/d/d1/Kale.png" },
            { "rhubarb", "https://stardewvalleywiki.com/mediawiki/images/6/6e/Rhubarb.png" },
            { "melon", "https://stardewvalleywiki.com/mediawiki/images/1/19/Melon.png" },
            { "tomato", "https://stardewvalleywiki.com/mediawiki/images/9/9d/Tomato.png" },
            { "morel", "https://stardewvalleywiki.com/mediawiki/images/b/b1/Morel.png" },
            { "blueberry", "https://stardewvalleywiki.com/mediawiki/images/9/9e/Blueberry.png" },
            { "fiddlehead-fern", "https://stardewvalleywiki.com/mediawiki/images/4/48/Fiddlehead_Fern.png" },
            { "hot-pepper", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Hot_Pepper.png" },
            { "wheat", "https://stardewvalleywiki.com/mediawiki/images/e/e2/Wheat.png" },
            { "radish", "https://stardewvalleywiki.com/mediawiki/images/d/d5/Radish.png" },
            { "red-cabbage", "https://stardewvalleywiki.com/mediawiki/images/2/2d/Red_Cabbage.png" },
            { "starfruit", "https://stardewvalleywiki.com/mediawiki/images/d/db/Starfruit.png" },
            { "corn", "https://stardewvalleywiki.com/mediawiki/images/f/f8/Corn.png" },
            { "unmilled-rice", "https://stardewvalleywiki.com/mediawiki/images/f/fe/Unmilled_Rice.png" },
            { "eggplant", "https://stardewvalleywiki.com/mediawiki/images/8/8f/Eggplant.png" },
            { "artichoke", "https://stardewvalleywiki.com/mediawiki/images/d/dd/Artichoke.png" },
            { "pumpkin", "https://stardewvalleywiki.com/mediawiki/images/6/64/Pumpkin.png" },
            { "bok-choy", "https://stardewvalleywiki.com/mediawiki/images/4/40/Bok_Choy.png" },
            { "yam", "https://stardewvalleywiki.com/mediawiki/images/5/52/Yam.png" },
            { "chanterelle", "https://stardewvalleywiki.com/mediawiki/images/1/1d/Chanterelle.png" },
            { "cranberries", "https://stardewvalleywiki.com/mediawiki/images/6/6e/Cranberries.png" },
            { "holly", "https://stardewvalleywiki.com/mediawiki/images/b/b8/Holly.png" },
            { "beet", "https://stardewvalleywiki.com/mediawiki/images/a/a4/Beet.png" },
            { "ostrich-egg", "https://stardewvalleywiki.com/mediawiki/images/c/c3/Ostrich_Egg.png" },
            { "salmonberry", "https://stardewvalleywiki.com/mediawiki/images/5/59/Salmonberry.png" },
            { "amaranth", "https://stardewvalleywiki.com/mediawiki/images/f/f6/Amaranth.png" },
            { "pale-ale", "https://stardewvalleywiki.com/mediawiki/images/7/78/Pale_Ale.png" },
            { "hops", "https://stardewvalleywiki.com/mediawiki/images/5/59/Hops.png" },
            { "void-egg", "https://stardewvalleywiki.com/mediawiki/images/5/58/Void_Egg.png" },
            { "mayonnaise", "https://stardewvalleywiki.com/mediawiki/images/4/4e/Mayonnaise.png" },
            { "duck-mayonnaise", "https://stardewvalleywiki.com/mediawiki/images/2/23/Duck_Mayonnaise.png" },
            { "void-mayonnaise", "https://stardewvalleywiki.com/mediawiki/images/f/f3/Void_Mayonnaise.png" },
            { "clay", "https://stardewvalleywiki.com/mediawiki/images/a/a2/Clay.png" },
            { "copper-bar", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Copper_Bar.png" },
            { "iron-bar", "https://stardewvalleywiki.com/mediawiki/images/6/6c/Iron_Bar.png" },
            { "gold-bar", "https://stardewvalleywiki.com/mediawiki/images/4/4e/Gold_Bar.png" },
            { "iridium-bar", "https://stardewvalleywiki.com/mediawiki/images/c/c4/Iridium_Bar.png" },
            { "refined-quartz", "https://stardewvalleywiki.com/mediawiki/images/9/98/Refined_Quartz.png" },
            { "honey", "https://stardewvalleywiki.com/mediawiki/images/c/c6/Honey.png" },
            { "pickles", "https://stardewvalleywiki.com/mediawiki/images/c/c7/Pickles.png" },
            { "jelly", "https://stardewvalleywiki.com/mediawiki/images/0/05/Jelly.png" },
            { "beer", "https://stardewvalleywiki.com/mediawiki/images/b/b3/Beer.png" },
            { "wine", "https://stardewvalleywiki.com/mediawiki/images/6/69/Wine.png" },
            { "juice", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Juice.png" },
            { "poppy", "https://stardewvalleywiki.com/mediawiki/images/3/37/Poppy.png" },
            { "copper-ore", "https://stardewvalleywiki.com/mediawiki/images/7/78/Copper_Ore.png" },
            { "iron-ore", "https://stardewvalleywiki.com/mediawiki/images/8/87/Iron_Ore.png" },
            { "coal", "https://stardewvalleywiki.com/mediawiki/images/a/a7/Coal.png" },
            { "gold-ore", "https://stardewvalleywiki.com/mediawiki/images/f/f7/Gold_Ore.png" },
            { "iridium-ore", "https://stardewvalleywiki.com/mediawiki/images/e/e9/Iridium_Ore.png" },
            { "wood", "https://stardewvalleywiki.com/mediawiki/images/d/df/Wood.png" },
            { "stone", "https://stardewvalleywiki.com/mediawiki/images/d/d4/Stone.png" },
            { "nautilus-shell", "https://stardewvalleywiki.com/mediawiki/images/a/a4/Nautilus_Shell.png" },
            { "coral", "https://stardewvalleywiki.com/mediawiki/images/b/b1/Coral.png" },
            { "rainbow-shell", "https://stardewvalleywiki.com/mediawiki/images/3/3d/Rainbow_Shell.png" },
            { "spice-berry", "https://stardewvalleywiki.com/mediawiki/images/c/c6/Spice_Berry.png" },
            { "sea-urchin", "https://stardewvalleywiki.com/mediawiki/images/e/e7/Sea_Urchin.png" },
            { "grape", "https://stardewvalleywiki.com/mediawiki/images/c/c2/Grape.png" },
            { "spring-onion", "https://stardewvalleywiki.com/mediawiki/images/0/0c/Spring_Onion.png" },
            { "strawberry", "https://stardewvalleywiki.com/mediawiki/images/6/6d/Strawberry.png" },
            { "sweet-pea", "https://stardewvalleywiki.com/mediawiki/images/d/d9/Sweet_Pea.png" },
            { "common-mushroom", "https://stardewvalleywiki.com/mediawiki/images/2/2e/Common_Mushroom.png" },
            { "wild-plum", "https://stardewvalleywiki.com/mediawiki/images/3/3b/Wild_Plum.png" },
            { "hazelnut", "https://stardewvalleywiki.com/mediawiki/images/3/31/Hazelnut.png" },
            { "blackberry", "https://stardewvalleywiki.com/mediawiki/images/2/25/Blackberry.png" },
            { "winter-root", "https://stardewvalleywiki.com/mediawiki/images/1/11/Winter_Root.png" },
            { "crystal-fruit", "https://stardewvalleywiki.com/mediawiki/images/1/16/Crystal_Fruit.png" },
            { "snow-yam", "https://stardewvalleywiki.com/mediawiki/images/3/3f/Snow_Yam.png" },
            { "sweet-gem-berry", "https://stardewvalleywiki.com/mediawiki/images/8/88/Sweet_Gem_Berry.png" },
            { "crocus", "https://stardewvalleywiki.com/mediawiki/images/2/2f/Crocus.png" },
            { "red-mushroom", "https://stardewvalleywiki.com/mediawiki/images/e/e1/Red_Mushroom.png" },
            { "sunflower", "https://stardewvalleywiki.com/mediawiki/images/8/81/Sunflower.png" },
            { "purple-mushroom", "https://stardewvalleywiki.com/mediawiki/images/4/4b/Purple_Mushroom.png" },
            { "cheese", "https://stardewvalleywiki.com/mediawiki/images/a/a5/Cheese.png" },
            { "goat-cheese", "https://stardewvalleywiki.com/mediawiki/images/c/c8/Goat_Cheese.png" },
            { "cloth", "https://stardewvalleywiki.com/mediawiki/images/5/51/Cloth.png" },
            { "truffle", "https://stardewvalleywiki.com/mediawiki/images/f/f2/Truffle.png" },
            { "truffle-oil", "https://stardewvalleywiki.com/mediawiki/images/3/3d/Truffle_Oil.png" },
            { "coffee-bean", "https://stardewvalleywiki.com/mediawiki/images/3/33/Coffee_Bean.png" },
            { "goat-milk", "https://stardewvalleywiki.com/mediawiki/images/4/45/Goat_Milk.png" },
            { "l.-goat-milk", "https://stardewvalleywiki.com/mediawiki/images/f/f2/Large_Goat_Milk.png" },
            { "wool", "https://stardewvalleywiki.com/mediawiki/images/3/34/Wool.png" },
            { "duck-egg", "https://stardewvalleywiki.com/mediawiki/images/3/31/Duck_Egg.png" },
            { "duck-feather", "https://stardewvalleywiki.com/mediawiki/images/f/f9/Duck_Feather.png" },
            { "caviar", "https://stardewvalleywiki.com/mediawiki/images/8/89/Caviar.png" },
            { "rabbit's-foot", "https://stardewvalleywiki.com/mediawiki/images/c/ca/Rabbit%27s_Foot.png" },
            { "aged-roe", "https://stardewvalleywiki.com/mediawiki/images/0/0e/Aged_Roe.png" },
            { "ancient-fruit", "https://stardewvalleywiki.com/mediawiki/images/0/01/Ancient_Fruit.png" },
            { "mead", "https://stardewvalleywiki.com/mediawiki/images/8/84/Mead.png" },
            { "tulip", "https://stardewvalleywiki.com/mediawiki/images/c/cf/Tulip.png" },
            { "summer-spangle", "https://stardewvalleywiki.com/mediawiki/images/9/9f/Summer_Spangle.png" },
            { "fairy-rose", "https://stardewvalleywiki.com/mediawiki/images/5/5c/Fairy_Rose.png" },
            { "blue-jazz", "https://stardewvalleywiki.com/mediawiki/images/2/2f/Blue_Jazz.png" },
            { "apple", "https://stardewvalleywiki.com/mediawiki/images/7/7d/Apple.png" },
            { "green-tea", "https://stardewvalleywiki.com/mediawiki/images/8/8f/Green_Tea.png" },
            { "apricot", "https://stardewvalleywiki.com/mediawiki/images/f/fc/Apricot.png" },
            { "orange", "https://stardewvalleywiki.com/mediawiki/images/4/43/Orange.png" },
            { "peach", "https://stardewvalleywiki.com/mediawiki/images/e/e2/Peach.png" },
            { "pomegranate", "https://stardewvalleywiki.com/mediawiki/images/1/1b/Pomegranate.png" },
            { "cherry", "https://stardewvalleywiki.com/mediawiki/images/2/20/Cherry.png" },
            { "bug-meat", "https://stardewvalleywiki.com/mediawiki/images/b/b6/Bug_Meat.png" },
            { "hardwood", "https://stardewvalleywiki.com/mediawiki/images/e/ed/Hardwood.png" },
            { "maple-syrup", "https://stardewvalleywiki.com/mediawiki/images/6/6a/Maple_Syrup.png" },
            { "oak-resin", "https://stardewvalleywiki.com/mediawiki/images/4/40/Oak_Resin.png" },
            { "pine-tar", "https://stardewvalleywiki.com/mediawiki/images/c/ce/Pine_Tar.png" },
            { "slime", "https://stardewvalleywiki.com/mediawiki/images/3/38/Slime.png" },
            { "bat-wing", "https://stardewvalleywiki.com/mediawiki/images/3/35/Bat_Wing.png" },
            { "solar-essence", "https://stardewvalleywiki.com/mediawiki/images/f/f4/Solar_Essence.png" },
            { "void-essence", "https://stardewvalleywiki.com/mediawiki/images/1/1f/Void_Essence.png" },
            { "fiber", "https://stardewvalleywiki.com/mediawiki/images/4/45/Fiber.png" },
            { "battery-pack", "https://stardewvalleywiki.com/mediawiki/images/2/25/Battery_Pack.png" },
            { "dinosaur-mayonnaise", "https://stardewvalleywiki.com/mediawiki/images/c/c3/Dinosaur_Mayonnaise.png" },
            { "roe", "https://stardewvalleywiki.com/mediawiki/images/5/56/Roe.png" },
            { "squid-ink", "https://stardewvalleywiki.com/mediawiki/images/a/ac/Squid_Ink.png" },
            { "tea-leaves", "https://stardewvalleywiki.com/mediawiki/images/5/5b/Tea_Leaves.png" },
            { "ginger", "https://stardewvalleywiki.com/mediawiki/images/8/85/Ginger.png" },
            { "taro-root", "https://stardewvalleywiki.com/mediawiki/images/0/01/Taro_Root.png" },
            { "pineapple", "https://stardewvalleywiki.com/mediawiki/images/f/fb/Pineapple.png" },
            { "mango", "https://stardewvalleywiki.com/mediawiki/images/3/38/Mango.png" },
            { "cinder-shard", "https://stardewvalleywiki.com/mediawiki/images/f/fd/Cinder_Shard.png" },
            { "magma-cap", "https://stardewvalleywiki.com/mediawiki/images/7/77/Magma_Cap.png" },
            { "bone-fragment", "https://stardewvalleywiki.com/mediawiki/images/9/97/Bone_Fragment.png" },
            { "radioactive-ore", "https://stardewvalleywiki.com/mediawiki/images/9/9f/Radioactive_Ore.png" },
            { "radioactive-bar", "https://stardewvalleywiki.com/mediawiki/images/7/7e/Radioactive_Bar.png" },
            { "smoked-fish", "https://stardewvalleywiki.com/mediawiki/images/4/4c/Smoked_Fish.png" },
            { "moss", "https://stardewvalleywiki.com/mediawiki/images/6/64/Moss.png" },
            { "mystic-syrup", "https://stardewvalleywiki.com/mediawiki/images/5/5a/Mystic_Syrup.png" },
            { "raisins", "https://stardewvalleywiki.com/mediawiki/images/0/06/Raisins.png" },
            { "dried-fruit", "https://stardewvalleywiki.com/mediawiki/images/6/66/Dried_Fruit.png" },
            { "dried-mushrooms", "https://stardewvalleywiki.com/mediawiki/images/1/1a/Dried_Mushrooms.png" },
            { "carrot", "https://stardewvalleywiki.com/mediawiki/images/c/c3/Carrot.png" },
            { "summer-squash", "https://stardewvalleywiki.com/mediawiki/images/4/43/Summer_Squash.png" },
            { "broccoli", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Broccoli.png" },
            { "powdermelon", "https://stardewvalleywiki.com/mediawiki/images/a/aa/Powdermelon.png" },
        };

        private Dictionary<string, string> fishSprites = new Dictionary<string, string>()
        {
            { "pufferfish", "https://stardewvalleywiki.com/mediawiki/images/b/ba/Pufferfish.png" },
            { "anchovy", "https://stardewvalleywiki.com/mediawiki/images/7/79/Anchovy.png" },
            { "tuna", "https://stardewvalleywiki.com/mediawiki/images/c/c5/Tuna.png" },
            { "sardine", "https://stardewvalleywiki.com/mediawiki/images/0/04/Sardine.png" },
            { "bream", "https://stardewvalleywiki.com/mediawiki/images/8/82/Bream.png" },
            { "largemouth-bass", "https://stardewvalleywiki.com/mediawiki/images/1/11/Largemouth_Bass.png" },
            { "smallmouth-bass", "https://stardewvalleywiki.com/mediawiki/images/a/a5/Smallmouth_Bass.png" },
            { "rainbow-trout", "https://stardewvalleywiki.com/mediawiki/images/1/14/Rainbow_Trout.png" },
            { "salmon", "https://stardewvalleywiki.com/mediawiki/images/e/e0/Salmon.png" },
            { "walleye", "https://stardewvalleywiki.com/mediawiki/images/e/e0/Salmon.png" },
            { "perch", "https://stardewvalleywiki.com/mediawiki/images/4/43/Perch.png" },
            { "carp", "https://stardewvalleywiki.com/mediawiki/images/a/a8/Carp.png" },
            { "catfish", "https://stardewvalleywiki.com/mediawiki/images/9/99/Catfish.png" },
            { "pike", "https://stardewvalleywiki.com/mediawiki/images/3/31/Pike.png" },
            { "sunfish", "https://stardewvalleywiki.com/mediawiki/images/5/56/Sunfish.png" },
            { "red-mullet", "https://stardewvalleywiki.com/mediawiki/images/f/f2/Red_Mullet.png" },
            { "herring", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Herring.png" },
            { "eel", "https://stardewvalleywiki.com/mediawiki/images/9/91/Eel.png" },
            { "octopus", "https://stardewvalleywiki.com/mediawiki/images/5/5a/Octopus.png" },
            { "red-snapper", "https://stardewvalleywiki.com/mediawiki/images/d/d3/Red_Snapper.png" },
            { "squid", "https://stardewvalleywiki.com/mediawiki/images/8/81/Squid.png" },
            { "seaweed", "https://stardewvalleywiki.com/mediawiki/images/1/13/Seaweed.png" },
            { "green-algae", "https://stardewvalleywiki.com/mediawiki/images/6/6d/Green_Algae.png" },
            { "sea-cucumber", "https://stardewvalleywiki.com/mediawiki/images/a/a9/Sea_Cucumber.png" },
            { "super-cucumber", "https://stardewvalleywiki.com/mediawiki/images/d/d5/Super_Cucumber.png" },
            { "ghostfish", "https://stardewvalleywiki.com/mediawiki/images/7/72/Ghostfish.png" },
            { "white-algae", "https://stardewvalleywiki.com/mediawiki/images/f/f7/White_Algae.png" },
            { "stonefish", "https://stardewvalleywiki.com/mediawiki/images/0/03/Stonefish.png" },
            { "crimsonfish", "https://stardewvalleywiki.com/mediawiki/images/d/dc/Crimsonfish.png" },
            { "angler", "https://stardewvalleywiki.com/mediawiki/images/b/bf/Angler.png" },
            { "ice-pip", "https://stardewvalleywiki.com/mediawiki/images/6/63/Ice_Pip.png" },
            { "lava-eel", "https://stardewvalleywiki.com/mediawiki/images/1/12/Lava_Eel.png" },
            { "legend", "https://stardewvalleywiki.com/mediawiki/images/1/10/Legend.png" },
            { "sandfish", "https://stardewvalleywiki.com/mediawiki/images/b/bb/Sandfish.png" },
            { "scorpion-carp", "https://stardewvalleywiki.com/mediawiki/images/7/76/Scorpion_Carp.png" },
            { "flounder", "https://stardewvalleywiki.com/mediawiki/images/8/85/Flounder.png" },
            { "midnight-carp", "https://stardewvalleywiki.com/mediawiki/images/3/33/Midnight_Carp.png" },
            { "mutant-carp", "https://stardewvalleywiki.com/mediawiki/images/c/cb/Mutant_Carp.png" },
            { "sturgeon", "https://stardewvalleywiki.com/mediawiki/images/4/42/Sturgeon.png" },
            { "tiger-trout", "https://stardewvalleywiki.com/mediawiki/images/0/01/Tiger_Trout.png" },
            { "bullhead", "https://stardewvalleywiki.com/mediawiki/images/d/db/Bullhead.png" },
            { "tilapia", "https://stardewvalleywiki.com/mediawiki/images/7/73/Tilapia.png" },
            { "chub", "https://stardewvalleywiki.com/mediawiki/images/b/bd/Chub.png" },
            { "dorado", "https://stardewvalleywiki.com/mediawiki/images/1/18/Dorado.png" },
            { "albacore", "https://stardewvalleywiki.com/mediawiki/images/e/e1/Albacore.png" },
            { "shad", "https://stardewvalleywiki.com/mediawiki/images/e/ef/Shad.png" },
            { "lingcod", "https://stardewvalleywiki.com/mediawiki/images/8/87/Lingcod.png" },
            { "halibut", "https://stardewvalleywiki.com/mediawiki/images/0/02/Halibut.png" },
            { "lobster", "https://stardewvalleywiki.com/mediawiki/images/9/9f/Lobster.png" },
            { "crayfish", "https://stardewvalleywiki.com/mediawiki/images/5/53/Crayfish.png" },
            { "crab", "https://stardewvalleywiki.com/mediawiki/images/6/63/Crab.png" },
            { "cockle", "https://stardewvalleywiki.com/mediawiki/images/a/ad/Cockle.png" },
            { "mussel", "https://stardewvalleywiki.com/mediawiki/images/a/aa/Mussel.png" },
            { "shrimp", "https://stardewvalleywiki.com/mediawiki/images/9/91/Shrimp.png" },
            { "snail", "https://stardewvalleywiki.com/mediawiki/images/d/d2/Snail.png" },
            { "periwinkle", "https://stardewvalleywiki.com/mediawiki/images/1/1d/Periwinkle.png" },
            { "oyster", "https://stardewvalleywiki.com/mediawiki/images/5/54/Oyster.png" },
            { "woodskip", "https://stardewvalleywiki.com/mediawiki/images/9/97/Woodskip.png" },
            { "glacierfish", "https://stardewvalleywiki.com/mediawiki/images/f/fd/Glacierfish.png" },
            { "void-salmon", "https://stardewvalleywiki.com/mediawiki/images/a/ad/Void_Salmon.png" },
            { "slimejack", "https://stardewvalleywiki.com/mediawiki/images/3/34/Slimejack.png" },
            { "midnight-squid", "https://stardewvalleywiki.com/mediawiki/images/8/83/Midnight_Squid.png" },
            { "spook-fish", "https://stardewvalleywiki.com/mediawiki/images/8/8c/Spook_Fish.png" },
            { "blobfish", "https://stardewvalleywiki.com/mediawiki/images/7/7f/Blobfish.png" },
            { "stingray", "https://stardewvalleywiki.com/mediawiki/images/3/3a/Stingray.png" },
            { "lionfish", "https://stardewvalleywiki.com/mediawiki/images/b/bb/Lionfish.png" },
            { "blue-discus", "https://stardewvalleywiki.com/mediawiki/images/e/ee/Blue_Discus.png" },
            { "clam", "https://stardewvalleywiki.com/mediawiki/images/e/ed/Clam.png" },
            { "river-jelly", "https://stardewvalleywiki.com/mediawiki/images/8/80/River_Jelly.png" },
            { "cave-jelly", "https://stardewvalleywiki.com/mediawiki/images/0/0a/Cave_Jelly.png" },
            { "sea-jelly", "https://stardewvalleywiki.com/mediawiki/images/d/d5/Sea_Jelly.png" },
            { "goby", "https://stardewvalleywiki.com/mediawiki/images/6/67/Goby.png" },
        };

        private Dictionary<string, string> artifactSprites = new Dictionary<string, string>()
        {
            { "dwarf-scroll-I", "https://stardewvalleywiki.com/mediawiki/images/b/b9/Dwarf_Scroll_I.png" },
            { "dwarf-scroll-II", "https://stardewvalleywiki.com/mediawiki/images/c/ca/Dwarf_Scroll_II.png" },
            { "dwarf-scroll-III", "https://stardewvalleywiki.com/mediawiki/images/e/ec/Dwarf_Scroll_III.png" },
            { "dwarf-scroll-IV", "https://stardewvalleywiki.com/mediawiki/images/8/85/Dwarf_Scroll_IV.png" },
            { "chipped-amphora", "https://stardewvalleywiki.com/mediawiki/images/9/9e/Chipped_Amphora.png" },
            { "arrowhead", "https://stardewvalleywiki.com/mediawiki/images/d/d1/Arrowhead.png" },
            { "ancient-doll", "https://stardewvalleywiki.com/mediawiki/images/c/c0/Ancient_Doll.png" },
            { "elvish-jewelry", "https://stardewvalleywiki.com/mediawiki/images/9/9d/Elvish_Jewelry.png" },
            { "chewing-stick", "https://stardewvalleywiki.com/mediawiki/images/d/d2/Chewing_Stick.png" },
            { "ornamental-fan", "https://stardewvalleywiki.com/mediawiki/images/a/ab/Ornamental_Fan.png" },
            { "dinosaur-egg", "https://stardewvalleywiki.com/mediawiki/images/a/a1/Dinosaur_Egg.png" },
            { "rare-disc", "https://stardewvalleywiki.com/mediawiki/images/e/e0/Rare_Disc.png" },
            { "ancient-sword", "https://stardewvalleywiki.com/mediawiki/images/7/76/Ancient_Sword.png" },
            { "rusty-spoon", "https://stardewvalleywiki.com/mediawiki/images/4/4e/Rusty_Spoon.png" },
            { "rusty-spur", "https://stardewvalleywiki.com/mediawiki/images/c/cd/Rusty_Spur.png" },
            { "rusty-cog", "https://stardewvalleywiki.com/mediawiki/images/7/74/Rusty_Cog.png" },
            { "chicken-statue", "https://stardewvalleywiki.com/mediawiki/images/a/af/Chicken_Statue.png" },
            { "ancient-seed", "https://stardewvalleywiki.com/mediawiki/images/a/af/Ancient_Seed.png" },
            { "prehistoric-tool", "https://stardewvalleywiki.com/mediawiki/images/2/26/Prehistoric_Tool.png" },
            { "dried-starfish", "https://stardewvalleywiki.com/mediawiki/images/d/df/Dried_Starfish.png" },
            { "anchor", "https://stardewvalleywiki.com/mediawiki/images/c/c0/Anchor.png" },
            { "glass-shards", "https://stardewvalleywiki.com/mediawiki/images/b/b9/Glass_Shards.png" },
            { "bone-flute", "https://stardewvalleywiki.com/mediawiki/images/f/fc/Bone_Flute.png" },
            { "prehistoric-handaxe", "https://stardewvalleywiki.com/mediawiki/images/0/07/Prehistoric_Handaxe.png" },
            { "dwarvish-helm", "https://stardewvalleywiki.com/mediawiki/images/8/8b/Dwarvish_Helm.png" },
            { "dwarf-gadget", "https://stardewvalleywiki.com/mediawiki/images/5/58/Dwarf_Gadget.png" },
            { "ancient-drum", "https://stardewvalleywiki.com/mediawiki/images/1/1e/Ancient_Drum.png" },
            { "golden-mask", "https://stardewvalleywiki.com/mediawiki/images/7/7e/Golden_Mask.png" },
            { "golden-relic", "https://stardewvalleywiki.com/mediawiki/images/3/37/Golden_Relic.png" },
            { "strange-doll-green", "https://stardewvalleywiki.com/mediawiki/images/3/39/Strange_Doll_%28green%29.png" },
            { "strange-doll-yellow", "https://stardewvalleywiki.com/mediawiki/images/e/ef/Strange_Doll_%28yellow%29.png" },
            { "prehistoric-scapula", "https://stardewvalleywiki.com/mediawiki/images/e/ee/Prehistoric_Scapula.png" },
            { "prehistoric-tibia", "https://stardewvalleywiki.com/mediawiki/images/c/ce/Prehistoric_Tibia.png" },
            { "prehistoric-skull", "https://stardewvalleywiki.com/mediawiki/images/5/58/Prehistoric_Skull.png" },
            { "skeletal-hand", "https://stardewvalleywiki.com/mediawiki/images/5/58/Prehistoric_Skull.png" },
            { "prehistoric-rib", "https://stardewvalleywiki.com/mediawiki/images/6/62/Prehistoric_Rib.png" },
            { "prehistoric-vertebra", "https://stardewvalleywiki.com/mediawiki/images/e/e9/Prehistoric_Vertebra.png" },
            { "skeletal-tail", "https://stardewvalleywiki.com/mediawiki/images/2/29/Skeletal_Tail.png" },
            { "nautilus-fossil", "https://stardewvalleywiki.com/mediawiki/images/3/3b/Nautilus_Fossil.png" },
            { "amphibian-fossil", "https://stardewvalleywiki.com/mediawiki/images/c/cf/Amphibian_Fossil.png" },
            { "palm-fossil", "https://stardewvalleywiki.com/mediawiki/images/e/e1/Palm_Fossil.png" },
            { "trilobite", "https://stardewvalleywiki.com/mediawiki/images/2/2b/Trilobite.png" },
        };

        private Dictionary<string, string> mineralSprites = new Dictionary<string, string>()
        {
            { "emerald", "https://stardewvalleywiki.com/mediawiki/images/6/6a/Emerald.png" },
            { "aquamarine", "https://stardewvalleywiki.com/mediawiki/images/a/a2/Aquamarine.png" },
            { "ruby", "https://stardewvalleywiki.com/mediawiki/images/a/a9/Ruby.png" },
            { "amethyst", "https://stardewvalleywiki.com/mediawiki/images/2/2e/Amethyst.png" },
            { "topaz", "https://stardewvalleywiki.com/mediawiki/images/a/a5/Topaz.png" },
            { "jade", "https://stardewvalleywiki.com/mediawiki/images/7/7e/Jade.png" },
            { "diamond", "https://stardewvalleywiki.com/mediawiki/images/e/ea/Diamond.png" },
            { "prismatic-shard", "https://stardewvalleywiki.com/mediawiki/images/5/56/Prismatic_Shard.png" },
            { "quartz", "https://stardewvalleywiki.com/mediawiki/images/c/cf/Quartz.png" },
            { "fire-quartz", "https://stardewvalleywiki.com/mediawiki/images/5/5b/Fire_Quartz.png" },
            { "frozen-tear", "https://stardewvalleywiki.com/mediawiki/images/e/ec/Frozen_Tear.png" },
            { "earth-crystal", "https://stardewvalleywiki.com/mediawiki/images/7/74/Earth_Crystal.png" },
            { "alamite", "https://stardewvalleywiki.com/mediawiki/images/7/7c/Alamite.png" },
            { "bixite", "https://stardewvalleywiki.com/mediawiki/images/9/98/Bixite.png" },
            { "baryte", "https://stardewvalleywiki.com/mediawiki/images/a/aa/Baryte.png" },
            { "aerinite", "https://stardewvalleywiki.com/mediawiki/images/6/6b/Aerinite.png" },
            { "calcite", "https://stardewvalleywiki.com/mediawiki/images/9/97/Calcite.png" },
            { "dolomite", "https://stardewvalleywiki.com/mediawiki/images/d/d4/Dolomite.png" },
            { "esperite", "https://stardewvalleywiki.com/mediawiki/images/a/aa/Esperite.png" },
            { "fluorapatite", "https://stardewvalleywiki.com/mediawiki/images/4/4a/Fluorapatite.png" },
            { "geminite", "https://stardewvalleywiki.com/mediawiki/images/5/54/Geminite.png" },
            { "helvite", "https://stardewvalleywiki.com/mediawiki/images/3/3f/Helvite.png" },
            { "jamborite", "https://stardewvalleywiki.com/mediawiki/images/4/4b/Jamborite.png" },
            { "jagoite", "https://stardewvalleywiki.com/mediawiki/images/c/c3/Jagoite.png" },
            { "kyanite", "https://stardewvalleywiki.com/mediawiki/images/e/e4/Kyanite.png" },
            { "lunarite", "https://stardewvalleywiki.com/mediawiki/images/0/06/Lunarite.png" },
            { "malachite", "https://stardewvalleywiki.com/mediawiki/images/a/ad/Malachite.png" },
            { "neptunite", "https://stardewvalleywiki.com/mediawiki/images/0/05/Neptunite.png" },
            { "lemon-stone", "https://stardewvalleywiki.com/mediawiki/images/3/31/Lemon_Stone.png" },
            { "nekoite", "https://stardewvalleywiki.com/mediawiki/images/5/53/Nekoite.png" },
            { "orpiment", "https://stardewvalleywiki.com/mediawiki/images/4/41/Orpiment.png" },
            { "petrified-slime", "https://stardewvalleywiki.com/mediawiki/images/2/24/Petrified_Slime.png" },
            { "thunder-egg", "https://stardewvalleywiki.com/mediawiki/images/1/14/Thunder_Egg.png" },
            { "pyrite", "https://stardewvalleywiki.com/mediawiki/images/6/64/Pyrite.png" },
            { "ocean-stone", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Ocean_Stone.png" },
            { "ghost-crystal", "https://stardewvalleywiki.com/mediawiki/images/d/d0/Ghost_Crystal.png" },
            { "tigerseye", "https://stardewvalleywiki.com/mediawiki/images/6/6e/Tigerseye.png" },
            { "jasper", "https://stardewvalleywiki.com/mediawiki/images/9/9b/Jasper.png" },
            { "opal", "https://stardewvalleywiki.com/mediawiki/images/3/3c/Opal.png" },
            { "fire-opal", "https://stardewvalleywiki.com/mediawiki/images/6/60/Fire_Opal.png" },
            { "celestine", "https://stardewvalleywiki.com/mediawiki/images/1/19/Celestine.png" },
            { "marble", "https://stardewvalleywiki.com/mediawiki/images/8/82/Marble.png" },
            { "sandstone", "https://stardewvalleywiki.com/mediawiki/images/d/d6/Sandstone.png" },
            { "granite", "https://stardewvalleywiki.com/mediawiki/images/4/4a/Granite.png" },
            { "basalt", "https://stardewvalleywiki.com/mediawiki/images/2/22/Basalt.png" },
            { "limestone", "https://stardewvalleywiki.com/mediawiki/images/4/4e/Limestone.png" },
            { "soapstone", "https://stardewvalleywiki.com/mediawiki/images/8/81/Soapstone.png" },
            { "hematite", "https://stardewvalleywiki.com/mediawiki/images/b/b1/Hematite.png" },
            { "mudstone", "https://stardewvalleywiki.com/mediawiki/images/5/52/Mudstone.png" },
            { "obsidian", "https://stardewvalleywiki.com/mediawiki/images/2/23/Obsidian.png" },
            { "slate", "https://stardewvalleywiki.com/mediawiki/images/9/97/Slate.png" },
            { "fairy-stone", "https://stardewvalleywiki.com/mediawiki/images/d/d9/Fairy_Stone.png" },
            { "star-shards", "https://stardewvalleywiki.com/mediawiki/images/3/3f/Star_Shards.png" },
        };

        private Dictionary<string, string> cookingSprites = new Dictionary<string, string>()
        {
            { "fried-egg", "https://stardewvalleywiki.com/mediawiki/images/1/18/Fried_Egg.png" },
            { "omelet", "https://stardewvalleywiki.com/mediawiki/images/1/12/Omelet.png" },
            { "salad", "https://stardewvalleywiki.com/mediawiki/images/7/7e/Salad.png" },
            { "cheese-cauliflower", "https://stardewvalleywiki.com/mediawiki/images/6/6e/Cheese_Cauliflower.png" },
            { "baked-fish", "https://stardewvalleywiki.com/mediawiki/images/9/94/Baked_Fish.png" },
            { "parsnip-soup", "https://stardewvalleywiki.com/mediawiki/images/7/76/Parsnip_Soup.png" },
            { "vegetable-medley", "https://stardewvalleywiki.com/mediawiki/images/0/0a/Vegetable_Medley.png" },
            { "complete-breakfast", "https://stardewvalleywiki.com/mediawiki/images/3/3d/Complete_Breakfast.png" },
            { "fried-calamari", "https://stardewvalleywiki.com/mediawiki/images/2/25/Fried_Calamari.png" },
            { "strange-bun", "https://stardewvalleywiki.com/mediawiki/images/5/5e/Strange_Bun.png" },
            { "lucky-lunch", "https://stardewvalleywiki.com/mediawiki/images/5/5d/Lucky_Lunch.png" },
            { "fried-mushroom", "https://stardewvalleywiki.com/mediawiki/images/4/4a/Fried_Mushroom.png" },
            { "pizza", "https://stardewvalleywiki.com/mediawiki/images/f/f4/Pizza.png" },
            { "bean-hotpot", "https://stardewvalleywiki.com/mediawiki/images/2/24/Bean_Hotpot.png" },
            { "glazed-yams", "https://stardewvalleywiki.com/mediawiki/images/3/30/Glazed_Yams.png" },
            { "carp-suprise", "https://stardewvalleywiki.com/mediawiki/images/c/cc/Carp_Surprise.png" },
            { "hashbrowns", "https://stardewvalleywiki.com/mediawiki/images/8/8f/Hashbrowns.png" },
            { "pancakes", "https://stardewvalleywiki.com/mediawiki/images/6/6b/Pancakes.png" },
            { "salmon-dinner", "https://stardewvalleywiki.com/mediawiki/images/8/8b/Salmon_Dinner.png" },
            { "fish-taco", "https://stardewvalleywiki.com/mediawiki/images/d/d5/Fish_Taco.png" },
            { "crispy-bass", "https://stardewvalleywiki.com/mediawiki/images/5/53/Crispy_Bass.png" },
            { "pepper-poppers", "https://stardewvalleywiki.com/mediawiki/images/0/08/Pepper_Poppers.png" },
            { "bread", "https://stardewvalleywiki.com/mediawiki/images/e/e1/Bread.png" },
            { "tom-kha-soup", "https://stardewvalleywiki.com/mediawiki/images/3/3b/Tom_Kha_Soup.png" },
            { "trout-soup", "https://stardewvalleywiki.com/mediawiki/images/4/48/Trout_Soup.png" },
            { "chocolate-cake", "https://stardewvalleywiki.com/mediawiki/images/8/87/Chocolate_Cake.png" },
            { "pink-cake", "https://stardewvalleywiki.com/mediawiki/images/3/32/Pink_Cake.png" },
            { "rhubarb-pie", "https://stardewvalleywiki.com/mediawiki/images/2/21/Rhubarb_Pie.png" },
            { "cookies", "https://stardewvalleywiki.com/mediawiki/images/7/70/Cookie.png" },
            { "spaghetti", "https://stardewvalleywiki.com/mediawiki/images/0/08/Spaghetti.png" },
            { "fried-eel", "https://stardewvalleywiki.com/mediawiki/images/8/84/Fried_Eel.png" },
            { "spicy-eel", "https://stardewvalleywiki.com/mediawiki/images/f/f2/Spicy_Eel.png" },
            { "sashimi", "https://stardewvalleywiki.com/mediawiki/images/4/41/Sashimi.png" },
            { "maki-roll", "https://stardewvalleywiki.com/mediawiki/images/b/b6/Maki_Roll.png" },
            { "tortilla", "https://stardewvalleywiki.com/mediawiki/images/d/d7/Tortilla.png" },
            { "red-plate", "https://stardewvalleywiki.com/mediawiki/images/4/45/Red_Plate.png" },
            { "eggplant-parmesan", "https://stardewvalleywiki.com/mediawiki/images/7/73/Eggplant_Parmesan.png" },
            { "rice-pudding", "https://stardewvalleywiki.com/mediawiki/images/e/ec/Rice_Pudding.png" },
            { "ice-cream", "https://stardewvalleywiki.com/mediawiki/images/5/5d/Ice_Cream.png" },
            { "blueberry-tart", "https://stardewvalleywiki.com/mediawiki/images/9/9b/Blueberry_Tart.png" },
            { "autumns-bounty", "https://stardewvalleywiki.com/mediawiki/images/f/f4/Autumn%27s_Bounty.png" },
            { "pumpkin-soup", "https://stardewvalleywiki.com/mediawiki/images/5/59/Pumpkin_Soup.png" },
            { "super-meal", "https://stardewvalleywiki.com/mediawiki/images/d/d2/Super_Meal.png" },
            { "cranberry-sauce", "https://stardewvalleywiki.com/mediawiki/images/0/0b/Cranberry_Sauce.png" },
            { "stuffing", "https://stardewvalleywiki.com/mediawiki/images/9/9a/Stuffing.png" },
            { "farmers-lunch", "https://stardewvalleywiki.com/mediawiki/images/7/79/Farmer%27s_Lunch.png" },
            { "survival-burger", "https://stardewvalleywiki.com/mediawiki/images/8/87/Survival_Burger.png" },
            { "dish-o-the-sea", "https://stardewvalleywiki.com/mediawiki/images/f/ff/Dish_O%27_The_Sea.png" },
            { "miners-treat", "https://stardewvalleywiki.com/mediawiki/images/1/12/Miner%27s_Treat.png" },
            { "roots-platter", "https://stardewvalleywiki.com/mediawiki/images/e/e0/Roots_Platter.png" },
            { "triple-shot-espresso", "https://stardewvalleywiki.com/mediawiki/images/3/36/Triple_Shot_Espresso.png" },
            { "seafoam-pudding", "https://stardewvalleywiki.com/mediawiki/images/3/33/Seafoam_Pudding.png" },
            { "algae-soup", "https://stardewvalleywiki.com/mediawiki/images/5/53/Algae_Soup.png" },
            { "pale-broth", "https://stardewvalleywiki.com/mediawiki/images/7/7e/Pale_Broth.png" },
            { "plum-pudding", "https://stardewvalleywiki.com/mediawiki/images/a/a0/Plum_Pudding.png" },
            { "artichoke-dip", "https://stardewvalleywiki.com/mediawiki/images/7/77/Artichoke_Dip.png" },
            { "stir-fry", "https://stardewvalleywiki.com/mediawiki/images/e/ed/Stir_Fry.png" },
            { "roasted-hazelnuts", "https://stardewvalleywiki.com/mediawiki/images/1/18/Roasted_Hazelnuts.png" },
            { "pumpkin-pie", "https://stardewvalleywiki.com/mediawiki/images/7/7d/Pumpkin_Pie.png" },
            { "radish-salad", "https://stardewvalleywiki.com/mediawiki/images/b/b9/Radish_Salad.png" },
            { "fruit-salad", "https://stardewvalleywiki.com/mediawiki/images/9/9e/Fruit_Salad.png" },
            { "blackberry-cobbler", "https://stardewvalleywiki.com/mediawiki/images/7/70/Blackberry_Cobbler.png" },
            { "cranberry-candy", "https://stardewvalleywiki.com/mediawiki/images/9/9d/Cranberry_Candy.png" },
            { "bruschetta", "https://stardewvalleywiki.com/mediawiki/images/c/ca/Bruschetta.png" },
            { "coleslaw", "https://stardewvalleywiki.com/mediawiki/images/e/e1/Coleslaw.png" },
            { "fiddlehead-risotto", "https://stardewvalleywiki.com/mediawiki/images/2/2d/Fiddlehead_Risotto.png" },
            { "poppyseed-muffin", "https://stardewvalleywiki.com/mediawiki/images/8/8e/Poppyseed_Muffin.png" },
            { "chowder", "https://stardewvalleywiki.com/mediawiki/images/9/95/Chowder.png" },
            { "fish-stew", "https://stardewvalleywiki.com/mediawiki/images/6/6f/Fish_Stew.png" },
            { "escargot", "https://stardewvalleywiki.com/mediawiki/images/7/78/Escargot.png" },
            { "lobster-bisque", "https://stardewvalleywiki.com/mediawiki/images/0/0a/Lobster_Bisque.png" },
            { "maple-bar", "https://stardewvalleywiki.com/mediawiki/images/1/18/Maple_Bar.png" },
            { "crab-cakes", "https://stardewvalleywiki.com/mediawiki/images/7/70/Crab_Cakes.png" },
            { "shrimp-cocktail", "https://stardewvalleywiki.com/mediawiki/images/8/8e/Shrimp_Cocktail.png" },
            { "ginger-ale", "https://stardewvalleywiki.com/mediawiki/images/1/1a/Ginger_Ale.png" },
            { "banana-pudding", "https://stardewvalleywiki.com/mediawiki/images/4/40/Banana_Pudding.png" },
            { "mango-sticky-rice", "https://stardewvalleywiki.com/mediawiki/images/6/6e/Mango_Sticky_Rice.png" },
            { "poi", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Poi.png" },
            { "tropical-curry", "https://stardewvalleywiki.com/mediawiki/images/3/32/Tropical_Curry.png" },
            { "squid-ink-ravioli", "https://stardewvalleywiki.com/mediawiki/images/8/86/Squid_Ink_Ravioli.png" },
            { "moss-soup", "https://stardewvalleywiki.com/mediawiki/images/d/df/Moss_Soup.png" },
        };

        private Dictionary<string, string> craftingSprites = new Dictionary<string, string>()
        {
            { "wood-fence", "https://stardewvalleywiki.com/mediawiki/images/1/1e/Wood_Fence.png" },
            { "stone-fence", "https://stardewvalleywiki.com/mediawiki/images/2/2d/Stone_Fence.png" },
            { "iron-fence", "https://stardewvalleywiki.com/mediawiki/images/9/9d/Iron_Fence.png" },
            { "hardwood-fence", "https://stardewvalleywiki.com/mediawiki/images/5/5c/Hardwood_Fence.png" },
            { "grass-starter", "https://stardewvalleywiki.com/mediawiki/images/1/14/Grass_Starter.png" },
            { "gate", "https://stardewvalleywiki.com/mediawiki/images/9/94/Gate.png" },
            { "chest", "https://stardewvalleywiki.com/mediawiki/images/b/b3/Chest.png" },
            { "torch", "https://stardewvalleywiki.com/mediawiki/images/b/b2/Torch.png" },
            { "scarecrow", "https://stardewvalleywiki.com/mediawiki/images/7/75/Scarecrow.png" },
            { "deluxe-scarecrow", "https://stardewvalleywiki.com/mediawiki/images/5/59/Deluxe_Scarecrow.png" },
            { "bee-house", "https://stardewvalleywiki.com/mediawiki/images/2/23/Bee_House_Full.png" },
            { "keg", "https://stardewvalleywiki.com/mediawiki/images/7/7c/Keg.png" },
            { "cask", "https://stardewvalleywiki.com/mediawiki/images/7/7c/Cask.png" },
            { "furnace", "https://stardewvalleywiki.com/mediawiki/images/d/de/Furnace_On.png" },
            { "garden-pot", "https://stardewvalleywiki.com/mediawiki/images/2/2c/Garden_Pot.png" },
            { "wood-sign", "https://stardewvalleywiki.com/mediawiki/images/c/ce/Wood_Sign.png" },
            { "stone-sign", "https://stardewvalleywiki.com/mediawiki/images/f/f3/Stone_Sign.png" },
            { "cheese-press", "https://stardewvalleywiki.com/mediawiki/images/7/79/Cheese_Press.png" },
            { "mayonnaise-machine", "https://stardewvalleywiki.com/mediawiki/images/e/ef/Mayonnaise_Machine.png" },
            { "seed-maker", "https://stardewvalleywiki.com/mediawiki/images/1/19/Seed_Maker.png" },
            { "loom", "https://stardewvalleywiki.com/mediawiki/images/3/3b/Loom.png" },
            { "oil-maker", "https://stardewvalleywiki.com/mediawiki/images/c/c5/Oil_Maker.png" },
            { "recycling-machine", "https://stardewvalleywiki.com/mediawiki/images/2/26/Recycling_Machine.png" },
            { "worm-bin", "https://stardewvalleywiki.com/mediawiki/images/7/71/Worm_Bin.png" },
            { "preserves-jar", "https://stardewvalleywiki.com/mediawiki/images/1/1e/Preserves_Jar.png" },
            { "charcoal-kiln", "https://stardewvalleywiki.com/mediawiki/images/9/9a/Charcoal_Kiln_Off.png" },
            { "tapper", "https://stardewvalleywiki.com/mediawiki/images/d/da/Tapper.png" },
            { "lightning-rod", "https://stardewvalleywiki.com/mediawiki/images/6/62/Lightning_Rod.png" },
            { "slime-incubator", "https://stardewvalleywiki.com/mediawiki/images/b/ba/Slime_Incubator_On.png" },
            { "slime-egg-press", "https://stardewvalleywiki.com/mediawiki/images/7/79/Slime_Egg-Press.png" },
            { "crystalarium", "https://stardewvalleywiki.com/mediawiki/images/6/63/Crystalarium.png" },
            { "mini-jukebox", "https://stardewvalleywiki.com/mediawiki/images/0/02/Mini-Jukebox.png" },
            { "sprinkler", "https://stardewvalleywiki.com/mediawiki/images/0/08/Sprinkler.png" },
            { "quality-sprinkler", "https://stardewvalleywiki.com/mediawiki/images/a/af/Quality_Sprinkler.png" },
            { "iridium-sprinkler", "https://stardewvalleywiki.com/mediawiki/images/9/90/Iridium_Sprinkler.png" },
            { "flute-block", "https://stardewvalleywiki.com/mediawiki/images/3/31/Flute_Block.png" },
            { "drum-block", "https://stardewvalleywiki.com/mediawiki/images/d/dc/Drum_Block.png" },
            { "basic-fertilizer", "https://stardewvalleywiki.com/mediawiki/images/9/9b/Basic_Fertilizer.png" },
            { "tree-fertilizer", "https://stardewvalleywiki.com/mediawiki/images/5/5d/Tree_Fertilizer.png" },
            { "quality-fertilizer", "https://stardewvalleywiki.com/mediawiki/images/a/a0/Quality_Fertilizer.png" },
            { "basic-retaining-soil", "https://stardewvalleywiki.com/mediawiki/images/c/c7/Basic_Retaining_Soil.png" },
            { "quality-retaining-soil", "https://stardewvalleywiki.com/mediawiki/images/0/0a/Quality_Retaining_Soil.png" },
            { "staircase", "https://stardewvalleywiki.com/mediawiki/images/8/8c/Staircase.png" },
            { "speed-gro", "https://stardewvalleywiki.com/mediawiki/images/9/94/Speed-Gro.png" },
            { "deluxe-speed-gro", "https://stardewvalleywiki.com/mediawiki/images/6/6d/Deluxe_Speed-Gro.png" },
            { "hyper-speed-gro", "https://stardewvalleywiki.com/mediawiki/images/5/53/Hyper_Speed-Gro.png" },
            { "deluxe-fertilizer", "https://stardewvalleywiki.com/mediawiki/images/1/1c/Deluxe_Fertilizer.png" },
            { "deluxe-retaining-soil", "https://stardewvalleywiki.com/mediawiki/images/c/c3/Deluxe_Retaining_Soil.png" },
            { "cherry-bomb", "https://stardewvalleywiki.com/mediawiki/images/1/1b/Cherry_Bomb.png" },
            { "bomb", "https://stardewvalleywiki.com/mediawiki/images/3/3b/Bomb.png" },
            { "mega-bomb", "https://stardewvalleywiki.com/mediawiki/images/4/4f/Mega_Bomb.png" },
            { "explosive-ammo", "https://stardewvalleywiki.com/mediawiki/images/f/f3/Explosive_Ammo.png" },
            { "transmute-fe", "https://stardewvalleywiki.com/mediawiki/images/6/6c/Iron_Bar.png" },
            { "transmute-au", "https://stardewvalleywiki.com/mediawiki/images/4/4e/Gold_Bar.png" },
            { "ancient-seeds", "https://stardewvalleywiki.com/mediawiki/images/e/ec/Ancient_Seeds.png" },
            { "wild-seeds-sp", "https://stardewvalleywiki.com/mediawiki/images/3/39/Spring_Seeds.png" },
            { "wild-seeds-su", "https://stardewvalleywiki.com/mediawiki/images/c/c4/Summer_Seeds.png" },
            { "wild-seeds-fa", "https://stardewvalleywiki.com/mediawiki/images/5/55/Fall_Seeds.png" },
            { "wild-seeds-wi", "https://stardewvalleywiki.com/mediawiki/images/d/dd/Winter_Seeds.png" },
            { "fiber-seeds", "https://stardewvalleywiki.com/mediawiki/images/0/05/Fiber_Seeds.png" },
            { "tea-sapling", "https://stardewvalleywiki.com/mediawiki/images/1/12/Tea_Sapling.png" },
            { "warp-totem-farm", "https://stardewvalleywiki.com/mediawiki/images/e/e4/Warp_Totem_Farm.png" },
            { "warp-totem-mountains", "https://stardewvalleywiki.com/mediawiki/images/d/d8/Warp_Totem_Mountains.png" },
            { "warp-totem-beach", "https://stardewvalleywiki.com/mediawiki/images/2/2f/Warp_Totem_Beach.png" },
            { "warp-totem-desert", "https://stardewvalleywiki.com/mediawiki/images/8/83/Warp_Totem_Desert.png" },
            { "warp-totem-island", "https://stardewvalleywiki.com/mediawiki/images/b/b9/Warp_Totem_Island.png" },
            { "rain-totem", "https://stardewvalleywiki.com/mediawiki/images/f/f7/Rain_Totem.png" },
            { "cookout-kit", "https://stardewvalleywiki.com/mediawiki/images/9/91/Cookout_Kit.png" },
            { "field-snack", "https://stardewvalleywiki.com/mediawiki/images/1/1b/Field_Snack.png" },
            { "jack-o-lantern", "https://stardewvalleywiki.com/mediawiki/images/1/17/Jack-O-Lantern.png" },
            { "wood-floor", "https://stardewvalleywiki.com/mediawiki/images/5/58/Wood_Floor.png" },
            { "straw-floor", "https://stardewvalleywiki.com/mediawiki/images/8/85/Straw_Floor.png" },
            { "weathered-floor", "https://stardewvalleywiki.com/mediawiki/images/2/2a/Weathered_Floor.png" },
            { "rustic-plank-floor", "https://stardewvalleywiki.com/mediawiki/images/9/9e/Rustic_Plank_Floor.png" },
            { "crystal-floor", "https://stardewvalleywiki.com/mediawiki/images/a/a4/Crystal_Floor.png" },
            { "stone-floor", "https://stardewvalleywiki.com/mediawiki/images/4/4e/Stone_Floor.png" },
            { "stone-walkway-floor", "https://stardewvalleywiki.com/mediawiki/images/5/51/Stone_Walkway_Floor.png" },
            { "brick-floor", "https://stardewvalleywiki.com/mediawiki/images/2/29/Brick_Floor.png" },
            { "wood-path", "https://stardewvalleywiki.com/mediawiki/images/e/e3/Wood_Path.png" },
            { "gravel-path", "https://stardewvalleywiki.com/mediawiki/images/1/15/Gravel_Path.png" },
            { "cobblestone-path", "https://stardewvalleywiki.com/mediawiki/images/9/90/Cobblestone_Path.png" },
            { "stepping-stone-path", "https://stardewvalleywiki.com/mediawiki/images/1/1b/Stepping_Stone_Path.png" },
            { "crystal-path", "https://stardewvalleywiki.com/mediawiki/images/9/9b/Crystal_Path.png" },
            { "wild-bait", "https://stardewvalleywiki.com/mediawiki/images/d/da/Wild_Bait.png" },
            { "bait", "https://stardewvalleywiki.com/mediawiki/images/f/ff/Bait.png" },
            { "spinner", "https://stardewvalleywiki.com/mediawiki/images/7/7b/Spinner.png" },
            { "magnet", "https://stardewvalleywiki.com/mediawiki/images/8/8c/Magnet.png" },
            { "trap-bobber", "https://stardewvalleywiki.com/mediawiki/images/d/da/Trap_Bobber.png" },
            { "cork-bobber", "https://stardewvalleywiki.com/mediawiki/images/0/0b/Cork_Bobber.png" },
            { "dressed-spinner", "https://stardewvalleywiki.com/mediawiki/images/e/e8/Dressed_Spinner.png" },
            { "treasure-hunter", "https://stardewvalleywiki.com/mediawiki/images/7/79/Treasure_Hunter.png" },
            { "barbed-hook", "https://stardewvalleywiki.com/mediawiki/images/4/4f/Barbed_Hook.png" },
            { "oil-of-garlic", "https://stardewvalleywiki.com/mediawiki/images/4/4b/Oil_of_Garlic.png" },
            { "life-elixer", "https://stardewvalleywiki.com/mediawiki/images/6/6e/Life_Elixir.png" },
            { "crab-pot", "https://stardewvalleywiki.com/mediawiki/images/9/92/Crab_Pot.png" },
            { "iridium-band", "https://stardewvalleywiki.com/mediawiki/images/7/77/Iridium_Band.png" },
            { "ring-of-yoba", "https://stardewvalleywiki.com/mediawiki/images/2/21/Ring_of_Yoba.png" },
            { "sturdy-ring", "https://stardewvalleywiki.com/mediawiki/images/7/7e/Sturdy_Ring.png" },
            { "warrior-ring", "https://stardewvalleywiki.com/mediawiki/images/1/13/Warrior_Ring.png" },
            { "tub-o-flowers", "https://stardewvalleywiki.com/mediawiki/images/d/d9/Tub_o%27_Flowers.png" },
            { "wooden-brazier", "https://stardewvalleywiki.com/mediawiki/images/3/34/Wooden_Brazier.png" },
            { "wicked-statue", "https://stardewvalleywiki.com/mediawiki/images/4/4a/Wicked_Statue.png" },
            { "stone-brazier", "https://stardewvalleywiki.com/mediawiki/images/1/1a/Stone_Brazier.png" },
            { "gold-brazier", "https://stardewvalleywiki.com/mediawiki/images/b/b7/Gold_Brazier.png" },
            { "campfire", "https://stardewvalleywiki.com/mediawiki/images/0/01/Campfire.png" },
            { "stump-brazier", "https://stardewvalleywiki.com/mediawiki/images/f/f2/Stump_Brazier.png" },
            { "carved-brazier", "https://stardewvalleywiki.com/mediawiki/images/3/3c/Carved_Brazier.png" },
            { "skull-brazier", "https://stardewvalleywiki.com/mediawiki/images/e/e6/Skull_Brazier.png" },
            { "barrel-brazier", "https://stardewvalleywiki.com/mediawiki/images/7/78/Barrel_Brazier.png" },
            { "marble-brazier", "https://stardewvalleywiki.com/mediawiki/images/e/e0/Marble_Brazier.png" },
            { "wood-lamp-post", "https://stardewvalleywiki.com/mediawiki/images/c/c8/Wood_Lamp-post.png" },
            { "iron-lamp-post", "https://stardewvalleywiki.com/mediawiki/images/b/b0/Iron_Lamp-post.png" },
            { "fairy-dust", "https://stardewvalleywiki.com/mediawiki/images/f/fc/Fairy_Dust.png" },
            { "bug-steak", "https://stardewvalleywiki.com/mediawiki/images/b/b7/Bug_Steak.png" },
            { "dark-sign", "https://stardewvalleywiki.com/mediawiki/images/7/72/Dark_Sign.png" },
            { "quality-bobber", "https://stardewvalleywiki.com/mediawiki/images/0/01/Quality_Bobber.png" },
            { "stone-chest", "https://stardewvalleywiki.com/mediawiki/images/1/1b/Stone_Chest.png" },
            { "monster-musk", "https://stardewvalleywiki.com/mediawiki/images/a/af/Monster_Musk.png" },
            { "mini-obelisk", "https://stardewvalleywiki.com/mediawiki/images/c/c9/Mini-Obelisk.png" },
            { "bone-mill", "https://stardewvalleywiki.com/mediawiki/images/c/cc/Bone_Mill.png" },
            { "thorns-ring", "https://stardewvalleywiki.com/mediawiki/images/5/5b/Thorns_Ring.png" },
            { "glowstone-ring", "https://stardewvalleywiki.com/mediawiki/images/6/65/Glowstone_Ring.png" },
            { "farm-computer", "https://stardewvalleywiki.com/mediawiki/images/f/f8/Farm_Computer.png" },
            { "ostrich-incubator", "https://stardewvalleywiki.com/mediawiki/images/7/72/Ostrich_Incubator_Full.png" },
            { "heavy-tapper", "https://stardewvalleywiki.com/mediawiki/images/0/0c/Heavy_Tapper.png" },
            { "geode-crusher", "https://stardewvalleywiki.com/mediawiki/images/1/13/Geode_Crusher_On.png" },
            { "hopper", "https://stardewvalleywiki.com/mediawiki/images/8/81/Hopper.png" },
            { "solar-panel", "https://stardewvalleywiki.com/mediawiki/images/5/5d/Solar_Panel.png" },
            { "magic-bait", "https://stardewvalleywiki.com/mediawiki/images/5/58/Magic_Bait.png" },
            { "blue-grass-starter", "https://stardewvalleywiki.com/mediawiki/images/1/18/Blue_Grass_Starter.png" },
            { "big-chest", "https://stardewvalleywiki.com/mediawiki/images/8/89/Big_Chest.png" },
            { "big-stone-chest", "https://stardewvalleywiki.com/mediawiki/images/a/a6/Big_Stone_Chest.png" },
            { "dehydrator", "https://stardewvalleywiki.com/mediawiki/images/0/02/Dehydrator.png" },
            { "heavy-furnace", "https://stardewvalleywiki.com/mediawiki/images/c/c1/Heavy_Furnace_On.png" },
            { "anvil", "https://stardewvalleywiki.com/mediawiki/images/d/dd/Anvil.png" },
            { "mini-forge", "https://stardewvalleywiki.com/mediawiki/images/6/6c/Mini-Forge.png" },
            { "text-sign", "https://stardewvalleywiki.com/mediawiki/images/9/93/Text_Sign.png" },
            { "deluxe-worm-bin", "https://stardewvalleywiki.com/mediawiki/images/e/ea/Deluxe_Worm_Bin.png" },
            { "bait-maker", "https://stardewvalleywiki.com/mediawiki/images/c/c0/Bait_Maker.png" },
            { "fish-smoker", "https://stardewvalleywiki.com/mediawiki/images/c/c0/Fish_Smoker_On.png" },
            { "mushroom-log", "https://stardewvalleywiki.com/mediawiki/images/8/83/Mushroom_Log_Ready.png" },
            { "mystic-tree-seed", "https://stardewvalleywiki.com/mediawiki/images/f/ff/Mystic_Tree_Seed.png" },
            { "treasure-totem", "https://stardewvalleywiki.com/mediawiki/images/0/06/Treasure_Totem.png" },
            { "tent-kit", "https://stardewvalleywiki.com/mediawiki/images/0/05/Tent_Kit.png" },
            { "statue-of-blessings", "https://stardewvalleywiki.com/mediawiki/images/e/e9/Statue_Of_Blessings.png" },
            { "statue-of-the-dwarf-king", "https://stardewvalleywiki.com/mediawiki/images/e/e4/Statue_Of_The_Dwarf_King.png" },
            { "deluxe-bait", "https://stardewvalleywiki.com/mediawiki/images/4/43/Deluxe_Bait.png" },
            { "challenge-bait", "https://stardewvalleywiki.com/mediawiki/images/9/96/Challenge_Bait.png" },
            { "sonar-bobber", "https://stardewvalleywiki.com/mediawiki/images/3/33/Sonar_Bobber.png" },
        };

        private Dictionary<string, string> monsterSprites = new Dictionary<string, string>()
        {
            { "slimes", "https://stardewvalleywiki.com/mediawiki/images/b/bc/Green_Slime_Anim.gif" },
            { "void-spirits", "https://stardewvalleywiki.com/mediawiki/images/b/ba/Shadow_Shaman_Anim.gif" },
            { "bats", "https://stardewvalleywiki.com/mediawiki/images/8/8c/Bat_Anim.gif" },
            { "skeletons", "https://stardewvalleywiki.com/mediawiki/images/b/bf/Skeleton_Anim.gif" },
            { "cave-insects", "https://stardewvalleywiki.com/mediawiki/images/b/b0/Grub_Anim.gif" },
            { "duggies", "https://stardewvalleywiki.com/mediawiki/images/3/3a/Duggy.png" },
            { "dust-sprites", "https://stardewvalleywiki.com/mediawiki/images/9/9a/Dust_Sprite.png" },
            { "rock-crabs", "https://stardewvalleywiki.com/mediawiki/images/3/30/Rock_Crab_Anim.gif" },
            { "mummies", "https://stardewvalleywiki.com/mediawiki/images/7/76/Mummy_Anim.gif" },
            { "pepper-rexes", "https://stardewvalleywiki.com/mediawiki/images/1/1a/Pepper_Rex.gif" },
            { "serpents", "https://stardewvalleywiki.com/mediawiki/images/c/cf/Serpent_Anim.gif" },
            { "magma-sprites", "https://stardewvalleywiki.com/mediawiki/images/a/a5/Magma_Sprite_Anim.gif" },
        };

        private Dictionary<string, string> questSprites = new Dictionary<string, string>() {
            { "introductions", "https://stardewvalleywiki.com/mediawiki/images/6/63/DialogueBubbleLove.png" },
            { "how-to-win-friends", "https://stardewvalleywiki.com/mediawiki/images/b/bb/Fishing_Treasure_Chest.png" },
            { "getting-started", "https://stardewvalleywiki.com/mediawiki/images/d/db/Parsnip.png" },
            { "to-the-beach", "https://stardewvalleywiki.com/mediawiki/images/5/5e/Fiberglass_Rod.png" },
            { "raising-animals", "https://stardewvalleywiki.com/mediawiki/images/5/5d/Large_Egg.png" },
            { "advancement", "https://stardewvalleywiki.com/mediawiki/images/7/75/Scarecrow.png" },
            { "explore-the-mine", "https://stardewvalleywiki.com/mediawiki/images/d/d1/Steel_Pickaxe.png" },
            { "deeper-in-the-mine", "https://stardewvalleywiki.com/mediawiki/images/d/d1/Steel_Pickaxe.png" },
            { "to-the-bottom?", "https://stardewvalleywiki.com/mediawiki/images/d/d1/Steel_Pickaxe.png" },
            { "archaeology", "https://stardewvalleywiki.com/mediawiki/images/8/87/Rusty_Key.png" },
            { "rat-problem", "https://stardewvalleywiki.com/mediawiki/images/b/b8/Golden_Scroll.png" },
            { "meet-the-wizard", "https://stardewvalleywiki.com/mediawiki/images/0/04/Wizard_Surprised.png" },
            { "forging-ahead", "https://stardewvalleywiki.com/mediawiki/images/d/de/Furnace_On.png" },
            { "smelting", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Copper_Bar.png" },
            { "initiation", "https://stardewvalleywiki.com/mediawiki/images/b/bc/Green_Slime_Anim.gif" },
            { "robin's-lost-axe", "https://stardewvalleywiki.com/mediawiki/images/4/4f/Springobjects788.png" },
            { "jodi's-request", "https://stardewvalleywiki.com/mediawiki/images/a/aa/Cauliflower.png" },
            { "mayor's-shorts", "https://stardewvalleywiki.com/mediawiki/images/0/04/Springobjects789.png" },
            { "blackberry-basket", "https://stardewvalleywiki.com/mediawiki/images/5/5a/Springobjects790.png" },
            { "marnie's-request", "https://stardewvalleywiki.com/mediawiki/images/3/34/Cave_Carrot.png" },
            { "pam-is-thirsty", "https://stardewvalleywiki.com/mediawiki/images/7/78/Pale_Ale.png" },
            { "a-dark-reagent", "https://stardewvalleywiki.com/mediawiki/images/1/1f/Void_Essence.png" },
            { "cow's-delight", "https://stardewvalleywiki.com/mediawiki/images/f/f6/Amaranth.png" },
            { "the-skull-key", "https://stardewvalleywiki.com/mediawiki/images/d/d3/Skull_Key.png" },
            { "crop-research", "https://stardewvalleywiki.com/mediawiki/images/1/19/Melon.png" },
            { "knee-therapy", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Hot_Pepper.png" },
            { "robin's-request", "https://stardewvalleywiki.com/mediawiki/images/e/ed/Hardwood.png" },
            { "qi's-challenge", "https://stardewvalleywiki.com/mediawiki/images/b/b4/Mr._Qi.png" },
            { "the-mysterious-qi", "https://stardewvalleywiki.com/mediawiki/images/b/b4/Mr._Qi.png" },
            { "carving-pumpkins", "https://stardewvalleywiki.com/mediawiki/images/6/64/Pumpkin.png" },
            { "a-winter-mystery", "https://stardewvalleywiki.com/mediawiki/images/5/5f/Magnifying_Glass.png" },
            { "strange-note", "https://stardewvalleywiki.com/mediawiki/images/6/6a/Maple_Syrup.png" },
            { "cryptic-note", "https://stardewvalleywiki.com/mediawiki/images/e/ec/Secret_Note.png" },
            { "fresh-fruit", "https://stardewvalleywiki.com/mediawiki/images/f/fc/Apricot.png" },
            { "aquatic-research", "https://stardewvalleywiki.com/mediawiki/images/b/ba/Pufferfish.png" },
            { "a-soldier's-star", "https://stardewvalleywiki.com/mediawiki/images/d/db/Starfruit.png" },
            { "mayor's-need", "https://stardewvalleywiki.com/mediawiki/images/3/3d/Truffle_Oil.png" },
            { "wanted:-lobster", "https://stardewvalleywiki.com/mediawiki/images/9/9f/Lobster.png" },
            { "pam-needs-juice", "https://stardewvalleywiki.com/mediawiki/images/2/25/Battery_Pack.png" },
            { "fish-casserole", "https://stardewvalleywiki.com/mediawiki/images/1/11/Largemouth_Bass.png" },
            { "catch-a-squid", "https://stardewvalleywiki.com/mediawiki/images/8/81/Squid.png" },
            { "fish-stew", "https://stardewvalleywiki.com/mediawiki/images/e/e1/Albacore.png" },
            { "pierre's-notice", "https://stardewvalleywiki.com/mediawiki/images/4/41/Sashimi.png" },
            { "clint's-attempt", "https://stardewvalleywiki.com/mediawiki/images/2/2e/Amethyst.png" },
            { "a-favor-for-clint", "https://stardewvalleywiki.com/mediawiki/images/6/6c/Iron_Bar.png" },
            { "staff-of-power", "https://stardewvalleywiki.com/mediawiki/images/c/c4/Iridium_Bar.png" },
            { "granny's-gift", "https://stardewvalleywiki.com/mediawiki/images/5/57/Leek.png" },
            { "exotic-spirits", "https://stardewvalleywiki.com/mediawiki/images/2/2f/Coconut.png" },
            { "catch-a-lingcod", "https://stardewvalleywiki.com/mediawiki/images/8/87/Lingcod.png" },
            { "dark-talisman", "https://stardewvalleywiki.com/mediawiki/images/e/e1/Dark_Talisman.png" },
            { "goblin-problem", "https://stardewvalleywiki.com/mediawiki/images/f/f3/Void_Mayonnaise.png" },
            { "the-pirate's-wife", "https://stardewvalleywiki.com/mediawiki/images/1/18/Pirate%27s_Locket.png" },
            { "island-ingredients", "https://stardewvalleywiki.com/mediawiki/images/0/01/Taro_Root.png" },
            { "cave-patrol", "https://stardewvalleywiki.com/mediawiki/images/8/8c/Bat_Anim.gif" },
            { "aquatic-overpopulation", "https://stardewvalleywiki.com/mediawiki/images/1/11/Largemouth_Bass.png" },
            { "biome-balance", "https://stardewvalleywiki.com/mediawiki/images/1/11/Largemouth_Bass.png" },
            { "rock-rejuvenation", "https://stardewvalleywiki.com/mediawiki/images/6/6a/Emerald.png" },
            { "gifts-for-george", "https://stardewvalleywiki.com/mediawiki/images/5/57/Leek.png" },
            { "fragments-of-the-past", "https://stardewvalleywiki.com/mediawiki/images/9/97/Bone_Fragment.png" },
            { "community-cleanup", "https://stardewvalleywiki.com/mediawiki/images/7/7c/Trash_%28item%29.png" },
            { "robin's-resource-rush", "https://stardewvalleywiki.com/mediawiki/images/d/df/Wood.png" },
            { "juicy-bugs-wanted!", "https://stardewvalleywiki.com/mediawiki/images/b/b6/Bug_Meat.png" },
            { "a-curious-substance", "https://stardewvalleywiki.com/mediawiki/images/3/36/Ectoplasm.png" },
            { "prismatic-jelly", "https://stardewvalleywiki.com/mediawiki/images/4/45/Prismatic_Jelly.png" },
            { "the-giant-stump", "https://stardewvalleywiki.com/mediawiki/images/8/83/Mushroom_Log_Ready.png" },
        };

        private Dictionary<string, string> characterSprites = new Dictionary<string, string>()
        {
            { "alex", "https://stardewvalleywiki.com/mediawiki/images/0/04/Alex.png" },
            { "elliott", "https://stardewvalleywiki.com/mediawiki/images/b/bd/Elliott.png" },
            { "harvey", "https://stardewvalleywiki.com/mediawiki/images/9/95/Harvey.png" },
            { "sam", "https://stardewvalleywiki.com/mediawiki/images/9/94/Sam.png" },
            { "sebastian", "https://stardewvalleywiki.com/mediawiki/images/a/a8/Sebastian.png" },
            { "shane", "https://stardewvalleywiki.com/mediawiki/images/8/8b/Shane.png" },
            { "abigail", "https://stardewvalleywiki.com/mediawiki/images/8/88/Abigail.png" },
            { "emily", "https://stardewvalleywiki.com/mediawiki/images/2/28/Emily.png" },
            { "haley", "https://stardewvalleywiki.com/mediawiki/images/1/1b/Haley.png" },
            { "leah", "https://stardewvalleywiki.com/mediawiki/images/e/e6/Leah.png" },
            { "maru", "https://stardewvalleywiki.com/mediawiki/images/f/f8/Maru.png" },
            { "penny", "https://stardewvalleywiki.com/mediawiki/images/a/ab/Penny.png" },
            { "caroline", "https://stardewvalleywiki.com/mediawiki/images/8/87/Caroline.png" },
            { "clint", "https://stardewvalleywiki.com/mediawiki/images/3/31/Clint.png" },
            { "demetrius", "https://stardewvalleywiki.com/mediawiki/images/f/f9/Demetrius.png" },
            { "dwarf", "https://stardewvalleywiki.com/mediawiki/images/e/ed/Dwarf.png" },
            { "evelyn", "https://stardewvalleywiki.com/mediawiki/images/8/8e/Evelyn.png" },
            { "george", "https://stardewvalleywiki.com/mediawiki/images/7/78/George.png" },
            { "gus", "https://stardewvalleywiki.com/mediawiki/images/5/52/Gus.png" },
            { "jas", "https://stardewvalleywiki.com/mediawiki/images/5/55/Jas.png" },
            { "jodi", "https://stardewvalleywiki.com/mediawiki/images/4/41/Jodi.png" },
            { "kent", "https://stardewvalleywiki.com/mediawiki/images/9/99/Kent.png" },
            { "krobus", "https://stardewvalleywiki.com/mediawiki/images/b/bc/Krobus_Happy.png" },
            { "leo", "https://stardewvalleywiki.com/mediawiki/images/1/1d/Leo.png" },
            { "lewis", "https://stardewvalleywiki.com/mediawiki/images/2/2b/Lewis.png" },
            { "linus", "https://stardewvalleywiki.com/mediawiki/images/3/31/Linus.png" },
            { "marnie", "https://stardewvalleywiki.com/mediawiki/images/5/52/Marnie.png" },
            { "pam", "https://stardewvalleywiki.com/mediawiki/images/d/da/Pam.png" },
            { "pierre", "https://stardewvalleywiki.com/mediawiki/images/7/7e/Pierre.png" },
            { "robin", "https://stardewvalleywiki.com/mediawiki/images/1/1b/Robin.png" },
            { "sandy", "https://stardewvalleywiki.com/mediawiki/images/4/4e/Sandy.png" },
            { "vincent", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Vincent.png" },
            { "willy", "https://stardewvalleywiki.com/mediawiki/images/8/82/Willy.png" },
            { "wizard", "https://stardewvalleywiki.com/mediawiki/images/c/c7/Wizard.png" },
        };

        private Dictionary<string, string> achievementSprites = new Dictionary<string, string>()
        {
            { "greenhorn", "https://stardewvalleywiki.com/mediawiki/images/1/1d/Achievement_Greenhorn.jpg" },
            { "cowpoke", "https://stardewvalleywiki.com/mediawiki/images/2/24/Achievement_Cowpoke.jpg" },
            { "homesteader", "https://stardewvalleywiki.com/mediawiki/images/f/fb/Achievement_Homesteader.jpg" },
            { "millionaire", "https://stardewvalleywiki.com/mediawiki/images/9/91/Achievement_Millionaire.jpg" },
            { "legend-achievement", "https://stardewvalleywiki.com/mediawiki/images/b/bb/Achievement_Legend.jpg" },
            { "a-complete-collection", "https://stardewvalleywiki.com/mediawiki/images/c/c6/Achievement_A_Complete_Collection.jpg" },
            { "a-new-friend", "https://stardewvalleywiki.com/mediawiki/images/c/cd/Achievement_A_New_Friend.jpg" },
            { "best-friends", "https://stardewvalleywiki.com/mediawiki/images/e/e8/Achievement_Best_Friends.jpg" },
            { "the-beloved-farmer", "https://stardewvalleywiki.com/mediawiki/images/5/5d/Achievement_The_Beloved_Farmer.jpg" },
            { "cliques", "https://stardewvalleywiki.com/mediawiki/images/2/26/Achievement_Cliques.jpg" },
            { "networking", "https://stardewvalleywiki.com/mediawiki/images/5/5e/Achievement_Networking.jpg" },
            { "popular", "https://stardewvalleywiki.com/mediawiki/images/a/a4/Achievement_Popular.jpg" },
            { "cook", "https://stardewvalleywiki.com/mediawiki/images/c/c6/Achievement_Cook.jpg" },
            { "sous-chef", "https://stardewvalleywiki.com/mediawiki/images/4/44/Achievement_Sous_Chef.jpg" },
            { "gourmet-chef", "https://stardewvalleywiki.com/mediawiki/images/d/dd/Achievement_Gourmet_Chef.jpg" },
            { "moving-up", "https://stardewvalleywiki.com/mediawiki/images/b/bd/Achievement_Moving_Up.jpg" },
            { "living-large", "https://stardewvalleywiki.com/mediawiki/images/6/65/Achievement_Living_Large.jpg" },
            { "do-it-yourself", "https://stardewvalleywiki.com/mediawiki/images/2/2e/Achievement_DIY.jpg" },
            { "artisan", "https://stardewvalleywiki.com/mediawiki/images/2/20/Achievement_Artisan.jpg" },
            { "craft-master", "https://stardewvalleywiki.com/mediawiki/images/6/60/Achievement_Master_Craft.jpg" },
            { "fisherman", "https://stardewvalleywiki.com/mediawiki/images/0/00/Achievement_Fisherman.jpg" },
            { "ol-mariner", "https://stardewvalleywiki.com/mediawiki/images/a/af/Achievement_Ol_Mariner.jpg" },
            { "master-angler", "https://stardewvalleywiki.com/mediawiki/images/6/65/Achievement_Master_Angler.jpg" },
            { "mother-catch", "https://stardewvalleywiki.com/mediawiki/images/0/0f/Achievement_Mother_Catch.jpg" },
            { "treasure-trove", "https://stardewvalleywiki.com/mediawiki/images/5/55/Achievement_Treasure_Trove.jpg" },
            { "gofer", "https://stardewvalleywiki.com/mediawiki/images/2/27/Achievement_Gofer.jpg" },
            { "a-big-help", "https://stardewvalleywiki.com/mediawiki/images/3/37/Achievement_A_Big_Help.jpg" },
            { "polyculture", "https://stardewvalleywiki.com/mediawiki/images/7/7f/Achievement_Polyculture.jpg" },
            { "monoculture", "https://stardewvalleywiki.com/mediawiki/images/2/2c/Achievement_Monoculture.jpg" },
            { "full-shipment", "https://stardewvalleywiki.com/mediawiki/images/b/b8/Achievement_Full_Shipment.jpg" },
            { "a-distant-shore", "https://stardewvalleywiki.com/mediawiki/images/5/5b/Achievement_A_Distant_Shore.jpg" },
            { "well-read", "https://stardewvalleywiki.com/mediawiki/images/1/1b/Achievement_Well-Read.jpg" },
            { "two-thumbs-up", "https://stardewvalleywiki.com/mediawiki/images/c/ca/Achievement_Two_Thumbs_Up.jpg" },
            { "blue-ribbon", "https://stardewvalleywiki.com/mediawiki/images/7/7a/Achievement_Blue_Ribbon.jpg" },
            { "an-unforgettable-soup", "https://stardewvalleywiki.com/mediawiki/images/c/c8/Achievement_An_Unforgettable_Soup.jpg" },
            { "good-neighbors", "https://stardewvalleywiki.com/mediawiki/images/0/00/Achievement_Good_Neighbors.jpg" },
            { "danger-in-the-deep", "https://stardewvalleywiki.com/mediawiki/images/9/99/Achievement_Danger_In_The_Deep.jpg" },
            { "infinite-power", "https://stardewvalleywiki.com/mediawiki/images/0/0c/Achievement_Infinite_Power.jpg" },
            { "perfection", "https://stardewvalleywiki.com/mediawiki/images/8/8b/Achievement_Perfection.jpg" },
        };

        private Dictionary<string, string> walletSprites = new Dictionary<string, string>()
        {
            { "dwarvish-translation-guide", "https://stardewvalleywiki.com/mediawiki/images/9/9c/Dwarvish_Translation_Guide.png" },
            { "rusty-key", "https://stardewvalleywiki.com/mediawiki/images/8/87/Rusty_Key.png" },
            { "club-card", "https://stardewvalleywiki.com/mediawiki/images/0/0d/Club_Card.png" },
            { "special-charm", "https://stardewvalleywiki.com/mediawiki/images/5/50/Special_Charm.png" },
            { "skull-key", "https://stardewvalleywiki.com/mediawiki/images/d/d3/Skull_Key.png" },
            { "magnifying-glass", "https://stardewvalleywiki.com/mediawiki/images/5/5f/Magnifying_Glass.png" },
            { "dark-talisman", "https://stardewvalleywiki.com/mediawiki/images/e/e1/Dark_Talisman.png" },
            { "magic-ink", "https://stardewvalleywiki.com/mediawiki/images/4/4b/Magic_Ink.png" },
            { "bears-knowledge", "https://stardewvalleywiki.com/mediawiki/images/7/7d/Bear%27s_Knowledge.png" },
            { "spring-onion-mastery", "https://stardewvalleywiki.com/mediawiki/images/3/3e/Spring_Onion_Mastery.png" },
            { "key-to-the-town", "https://stardewvalleywiki.com/mediawiki/images/a/a7/Key_To_The_Town.png" },
            { "forest-magic", "https://stardewvalleywiki.com/mediawiki/images/2/24/Forest_Magic.png" },
        };

        private Dictionary<string, string> fieldOfficeSprites = new Dictionary<string, string>() {
            { "fossilized-leg-1", "https://stardewvalleywiki.com/mediawiki/images/0/09/Fossilized_Leg.png" },
            { "fossilized-leg-2", "https://stardewvalleywiki.com/mediawiki/images/0/09/Fossilized_Leg.png" },
            { "fossilized-ribs", "https://stardewvalleywiki.com/mediawiki/images/f/fc/Fossilized_Ribs.png" },
            { "fossilized-skull", "https://stardewvalleywiki.com/mediawiki/images/7/72/Fossilized_Skull.png" },
            { "fossilized-spine", "https://stardewvalleywiki.com/mediawiki/images/5/5c/Fossilized_Spine.png" },
            { "fossilized-tail", "https://stardewvalleywiki.com/mediawiki/images/3/3a/Fossilized_Tail.png" },
            { "mummified-frog", "https://stardewvalleywiki.com/mediawiki/images/e/e9/Mummified_Frog.png" },
            { "mummified-bat", "https://stardewvalleywiki.com/mediawiki/images/6/6f/Mummified_Bat.png" },
            { "snake-skull", "https://stardewvalleywiki.com/mediawiki/images/2/25/Snake_Skull.png" },
            { "snake-vertebrae-1", "https://stardewvalleywiki.com/mediawiki/images/7/7e/Snake_Vertebrae.png" },
            { "snake-vertebrae-2", "https://stardewvalleywiki.com/mediawiki/images/7/7e/Snake_Vertebrae.png" },
        };

        private Dictionary<string, string> bookSprites = new Dictionary<string, string>() {
            { "price-catalogue", "https://stardewvalleywiki.com/mediawiki/images/d/d5/Price_Catalogue.png" },
            { "mapping-cave-systems", "https://stardewvalleywiki.com/mediawiki/images/6/6e/Mapping_Cave_Systems.png" },
            { "way-of-the-wind-pt.-1", "https://stardewvalleywiki.com/mediawiki/images/8/87/Way_Of_The_Wind_pt._1.png" },
            { "way-of-the-wind-pt.-2", "https://stardewvalleywiki.com/mediawiki/images/c/c7/Way_Of_The_Wind_pt._2.png" },
            { "monster-compendium", "https://stardewvalleywiki.com/mediawiki/images/1/15/Monster_Compendium.png" },
            { "friendship-101", "https://stardewvalleywiki.com/mediawiki/images/b/b7/Friendship_101.png" },
            { "jack-be-nimble,-jack-be-thick", "https://stardewvalleywiki.com/mediawiki/images/c/c2/Jack_Be_Nimble%2C_Jack_Be_Thick.png" },
            { "woody's-secret", "https://stardewvalleywiki.com/mediawiki/images/8/8b/Woody%27s_Secret.png" },
            { "raccoon-journal", "https://stardewvalleywiki.com/mediawiki/images/1/1b/Ways_Of_The_Wild.png" },
            { "jewels-of-the-sea", "https://stardewvalleywiki.com/mediawiki/images/7/7d/Jewels_Of_The_Sea.png" },
            { "dwarvish-safety-manual", "https://stardewvalleywiki.com/mediawiki/images/a/a7/Dwarvish_Safety_Manual.png" },
            { "the-art-o'-crabbing", "https://stardewvalleywiki.com/mediawiki/images/c/c6/The_Art_O%27_Crabbing.png" },
            { "the-alleyway-buffet", "https://stardewvalleywiki.com/mediawiki/images/3/3a/The_Alleyway_Buffet.png" },
            { "the-diamond-hunter", "https://stardewvalleywiki.com/mediawiki/images/a/a7/The_Diamond_Hunter.png" },
            { "book-of-mysteries", "https://stardewvalleywiki.com/mediawiki/images/d/df/Book_of_Mysteries.png" },
            { "horse-the-book", "https://stardewvalleywiki.com/mediawiki/images/4/45/Horse_The_Book.png" },
            { "treasure-appraisal-guide", "https://stardewvalleywiki.com/mediawiki/images/0/02/Treasure_Appraisal_Guide.png" },
            { "ol'-slitherlegs", "https://stardewvalleywiki.com/mediawiki/images/0/02/Ol%27_Slitherlegs.png" },
            { "animal-catalogue", "https://stardewvalleywiki.com/mediawiki/images/d/df/Animal_Catalogue.png" },
        };

        private Dictionary<string, string> powerSprites = new Dictionary<string, string>() {
            { "farming-mastery", "https://stardewvalleywiki.com/mediawiki/images/8/82/Farming_Skill_Icon.png" },
            { "fishing-mastery", "https://stardewvalleywiki.com/mediawiki/images/e/e7/Fishing_Skill_Icon.png" },
            { "foraging-mastery", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Foraging_Skill_Icon.png" },
            { "mining-mastery", "https://stardewvalleywiki.com/mediawiki/images/2/2f/Mining_Skill_Icon.png" },
            { "combat-mastery", "https://stardewvalleywiki.com/mediawiki/images/c/cf/Combat_Skill_Icon.png" },
        };

        private Dictionary<string, string> bundleSprites = new Dictionary<string, string>() {
            { "wild-horseradish", "https://stardewvalleywiki.com/mediawiki/images/9/90/Wild_Horseradish.png" },
            { "daffodil", "https://stardewvalleywiki.com/mediawiki/images/4/4b/Daffodil.png" },
            { "leek", "https://stardewvalleywiki.com/mediawiki/images/5/57/Leek.png" },
            { "dandelion", "https://stardewvalleywiki.com/mediawiki/images/b/b1/Dandelion.png" },
            { "spring-onion", "https://stardewvalleywiki.com/mediawiki/images/0/0c/Spring_Onion.png" },
            { "grape", "https://stardewvalleywiki.com/mediawiki/images/c/c2/Grape.png" },
            { "spice-berry", "https://stardewvalleywiki.com/mediawiki/images/c/c6/Spice_Berry.png" },
            { "sweet-pea", "https://stardewvalleywiki.com/mediawiki/images/d/d9/Sweet_Pea.png" },
            { "common-mushroom", "https://stardewvalleywiki.com/mediawiki/images/2/2e/Common_Mushroom.png" },
            { "wild-plum", "https://stardewvalleywiki.com/mediawiki/images/3/3b/Wild_Plum.png" },
            { "hazelnut", "https://stardewvalleywiki.com/mediawiki/images/3/31/Hazelnut.png" },
            { "blackberry", "https://stardewvalleywiki.com/mediawiki/images/2/25/Blackberry.png" },
            { "winter-root", "https://stardewvalleywiki.com/mediawiki/images/1/11/Winter_Root.png" },
            { "crystal-fruit", "https://stardewvalleywiki.com/mediawiki/images/1/16/Crystal_Fruit.png" },
            { "snow-yam", "https://stardewvalleywiki.com/mediawiki/images/3/3f/Snow_Yam.png" },
            { "crocus", "https://stardewvalleywiki.com/mediawiki/images/2/2f/Crocus.png" },
            { "holly", "https://stardewvalleywiki.com/mediawiki/images/b/b8/Holly.png" },
            { "wood-1", "https://stardewvalleywiki.com/mediawiki/images/d/df/Wood.png" },
            { "wood-2", "https://stardewvalleywiki.com/mediawiki/images/d/df/Wood.png" },
            { "stone", "https://stardewvalleywiki.com/mediawiki/images/d/d4/Stone.png" },
            { "hardwood", "https://stardewvalleywiki.com/mediawiki/images/e/ed/Hardwood.png" },
            { "sap", "https://stardewvalleywiki.com/mediawiki/images/7/73/Sap.png" },
            { "moss", "https://stardewvalleywiki.com/mediawiki/images/6/64/Moss.png" },
            { "fiber", "https://stardewvalleywiki.com/mediawiki/images/4/45/Fiber.png" },
            { "acorn", "https://stardewvalleywiki.com/mediawiki/images/c/cd/Acorn.png" },
            { "maple-seed", "https://stardewvalleywiki.com/mediawiki/images/3/36/Maple_Seed.png" },
            { "coconut", "https://stardewvalleywiki.com/mediawiki/images/2/2f/Coconut.png" },
            { "cactus-fruit", "https://stardewvalleywiki.com/mediawiki/images/3/32/Cactus_Fruit.png" },
            { "cave-carrot", "https://stardewvalleywiki.com/mediawiki/images/3/34/Cave_Carrot.png" },
            { "red-mushroom", "https://stardewvalleywiki.com/mediawiki/images/e/e1/Red_Mushroom.png" },
            { "purple-mushroom", "https://stardewvalleywiki.com/mediawiki/images/4/4b/Purple_Mushroom.png" },
            { "maple-syrup", "https://stardewvalleywiki.com/mediawiki/images/6/6a/Maple_Syrup.png" },
            { "oak-resin", "https://stardewvalleywiki.com/mediawiki/images/4/40/Oak_Resin.png" },
            { "pine-tar", "https://stardewvalleywiki.com/mediawiki/images/c/ce/Pine_Tar.png" },
            { "morel", "https://stardewvalleywiki.com/mediawiki/images/b/b1/Morel.png" },
            { "fiddlehead-fern", "https://stardewvalleywiki.com/mediawiki/images/4/48/Fiddlehead_Fern.png" },
            { "white-algae", "https://stardewvalleywiki.com/mediawiki/images/f/f7/White_Algae.png" },
            { "hops", "https://stardewvalleywiki.com/mediawiki/images/5/59/Hops.png" },
            { "parsnip", "https://stardewvalleywiki.com/mediawiki/images/d/db/Parsnip.png" },
            { "green-bean", "https://stardewvalleywiki.com/mediawiki/images/5/5c/Green_Bean.png" },
            { "cauliflower", "https://stardewvalleywiki.com/mediawiki/images/a/aa/Cauliflower.png" },
            { "potato", "https://stardewvalleywiki.com/mediawiki/images/c/c2/Potato.png" },
            { "kale", "https://stardewvalleywiki.com/mediawiki/images/d/d1/Kale.png" },
            { "carrot", "https://stardewvalleywiki.com/mediawiki/images/c/c3/Carrot.png" },
            { "tomato", "https://stardewvalleywiki.com/mediawiki/images/9/9d/Tomato.png" },
            { "hot-pepper", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Hot_Pepper.png" },
            { "blueberry", "https://stardewvalleywiki.com/mediawiki/images/9/9e/Blueberry.png" },
            { "melon", "https://stardewvalleywiki.com/mediawiki/images/1/19/Melon.png" },
            { "summer-squash", "https://stardewvalleywiki.com/mediawiki/images/4/43/Summer_Squash.png" },
            { "corn", "https://stardewvalleywiki.com/mediawiki/images/f/f8/Corn.png" },
            { "eggplant", "https://stardewvalleywiki.com/mediawiki/images/8/8f/Eggplant.png" },
            { "pumpkin", "https://stardewvalleywiki.com/mediawiki/images/6/64/Pumpkin.png" },
            { "yam", "https://stardewvalleywiki.com/mediawiki/images/5/52/Yam.png" },
            { "broccoli", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Broccoli.png" },
            { "ancient-fruit", "https://stardewvalleywiki.com/mediawiki/images/0/01/Ancient_Fruit.png" },
            { "sweet-gem-berry", "https://stardewvalleywiki.com/mediawiki/images/8/88/Sweet_Gem_Berry.png" },
            { "large-milk", "https://stardewvalleywiki.com/mediawiki/images/6/67/Large_Milk.png" },
            { "large-egg-brown", "https://stardewvalleywiki.com/mediawiki/images/9/91/Large_Brown_Egg.png" },
            { "large-egg-white", "https://stardewvalleywiki.com/mediawiki/images/5/5d/Large_Egg.png" },
            { "l.-goat-milk", "https://stardewvalleywiki.com/mediawiki/images/f/f2/Large_Goat_Milk.png" },
            { "wool", "https://stardewvalleywiki.com/mediawiki/images/3/34/Wool.png" },
            { "duck-egg", "https://stardewvalleywiki.com/mediawiki/images/3/31/Duck_Egg.png" },
            { "roe", "https://stardewvalleywiki.com/mediawiki/images/5/56/Roe.png" },
            { "aged-roe", "https://stardewvalleywiki.com/mediawiki/images/0/0e/Aged_Roe.png" },
            { "squid-ink", "https://stardewvalleywiki.com/mediawiki/images/a/ac/Squid_Ink.png" },
            { "tulip", "https://stardewvalleywiki.com/mediawiki/images/c/cf/Tulip.png" },
            { "blue-jazz", "https://stardewvalleywiki.com/mediawiki/images/2/2f/Blue_Jazz.png" },
            { "summer-spangle", "https://stardewvalleywiki.com/mediawiki/images/9/9f/Summer_Spangle.png" },
            { "sunflower", "https://stardewvalleywiki.com/mediawiki/images/8/81/Sunflower.png" },
            { "fairy-rose", "https://stardewvalleywiki.com/mediawiki/images/5/5c/Fairy_Rose.png" },
            { "truffle-oil", "https://stardewvalleywiki.com/mediawiki/images/3/3d/Truffle_Oil.png" },
            { "cloth", "https://stardewvalleywiki.com/mediawiki/images/5/51/Cloth.png" },
            { "goat-cheese", "https://stardewvalleywiki.com/mediawiki/images/c/c8/Goat_Cheese.png" },
            { "cheese", "https://stardewvalleywiki.com/mediawiki/images/a/a5/Cheese.png" },
            { "honey", "https://stardewvalleywiki.com/mediawiki/images/c/c6/Honey.png" },
            { "jelly", "https://stardewvalleywiki.com/mediawiki/images/0/05/Jelly.png" },
            { "apple", "https://stardewvalleywiki.com/mediawiki/images/7/7d/Apple.png" },
            { "apricot", "https://stardewvalleywiki.com/mediawiki/images/f/fc/Apricot.png" },
            { "orange", "https://stardewvalleywiki.com/mediawiki/images/4/43/Orange.png" },
            { "peach", "https://stardewvalleywiki.com/mediawiki/images/e/e2/Peach.png" },
            { "pomegranate", "https://stardewvalleywiki.com/mediawiki/images/1/1b/Pomegranate.png" },
            { "cherry", "https://stardewvalleywiki.com/mediawiki/images/2/20/Cherry.png" },
            { "mead", "https://stardewvalleywiki.com/mediawiki/images/8/84/Mead.png" },
            { "pale-ale", "https://stardewvalleywiki.com/mediawiki/images/7/78/Pale_Ale.png" },
            { "wine", "https://stardewvalleywiki.com/mediawiki/images/6/69/Wine.png" },
            { "juice", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Juice.png" },
            { "green-tea", "https://stardewvalleywiki.com/mediawiki/images/8/8f/Green_Tea.png" },
            { "sunfish", "https://stardewvalleywiki.com/mediawiki/images/5/56/Sunfish.png" },
            { "catfish", "https://stardewvalleywiki.com/mediawiki/images/9/99/Catfish.png" },
            { "shad", "https://stardewvalleywiki.com/mediawiki/images/e/ef/Shad.png" },
            { "tiger-trout", "https://stardewvalleywiki.com/mediawiki/images/0/01/Tiger_Trout.png" },
            { "largemouth-bass", "https://stardewvalleywiki.com/mediawiki/images/1/11/Largemouth_Bass.png" },
            { "carp", "https://stardewvalleywiki.com/mediawiki/images/a/a8/Carp.png" },
            { "bullhead", "https://stardewvalleywiki.com/mediawiki/images/d/db/Bullhead.png" },
            { "sturgeon", "https://stardewvalleywiki.com/mediawiki/images/4/42/Sturgeon.png" },
            { "sardine", "https://stardewvalleywiki.com/mediawiki/images/0/04/Sardine.png" },
            { "tuna", "https://stardewvalleywiki.com/mediawiki/images/c/c5/Tuna.png" },
            { "red-snapper", "https://stardewvalleywiki.com/mediawiki/images/d/d3/Red_Snapper.png" },
            { "tilapia", "https://stardewvalleywiki.com/mediawiki/images/7/73/Tilapia.png" },
            { "walleye", "https://stardewvalleywiki.com/mediawiki/images/0/05/Walleye.png" },
            { "bream", "https://stardewvalleywiki.com/mediawiki/images/8/82/Bream.png" },
            { "eel", "https://stardewvalleywiki.com/mediawiki/images/9/91/Eel.png" },
            { "lobster", "https://stardewvalleywiki.com/mediawiki/images/9/9f/Lobster.png" },
            { "crayfish", "https://stardewvalleywiki.com/mediawiki/images/5/53/Crayfish.png" },
            { "crab", "https://stardewvalleywiki.com/mediawiki/images/6/63/Crab.png" },
            { "cockle", "https://stardewvalleywiki.com/mediawiki/images/a/ad/Cockle.png" },
            { "mussel", "https://stardewvalleywiki.com/mediawiki/images/a/aa/Mussel.png" },
            { "shrimp", "https://stardewvalleywiki.com/mediawiki/images/9/91/Shrimp.png" },
            { "snail", "https://stardewvalleywiki.com/mediawiki/images/d/d2/Snail.png" },
            { "periwinkle", "https://stardewvalleywiki.com/mediawiki/images/1/1d/Periwinkle.png" },
            { "oyster", "https://stardewvalleywiki.com/mediawiki/images/5/54/Oyster.png" },
            { "clam", "https://stardewvalleywiki.com/mediawiki/images/e/ed/Clam.png" },
            { "pufferfish", "https://stardewvalleywiki.com/mediawiki/images/b/ba/Pufferfish.png" },
            { "ghostfish", "https://stardewvalleywiki.com/mediawiki/images/7/72/Ghostfish.png" },
            { "sandfish", "https://stardewvalleywiki.com/mediawiki/images/b/bb/Sandfish.png" },
            { "woodskip", "https://stardewvalleywiki.com/mediawiki/images/9/97/Woodskip.png" },
            { "lava-eel", "https://stardewvalleywiki.com/mediawiki/images/1/12/Lava_Eel.png" },
            { "scorpion-carp", "https://stardewvalleywiki.com/mediawiki/images/7/76/Scorpion_Carp.png" },
            { "octopus", "https://stardewvalleywiki.com/mediawiki/images/5/5a/Octopus.png" },
            { "blobfish", "https://stardewvalleywiki.com/mediawiki/images/7/7f/Blobfish.png" },
            { "copper-bar", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Copper_Bar.png" },
            { "iron-bar", "https://stardewvalleywiki.com/mediawiki/images/6/6c/Iron_Bar.png" },
            { "gold-bar", "https://stardewvalleywiki.com/mediawiki/images/4/4e/Gold_Bar.png" },
            { "quartz", "https://stardewvalleywiki.com/mediawiki/images/c/cf/Quartz.png" },
            { "earth-crystal", "https://stardewvalleywiki.com/mediawiki/images/7/74/Earth_Crystal.png" },
            { "frozen-tear", "https://stardewvalleywiki.com/mediawiki/images/e/ec/Frozen_Tear.png" },
            { "fire-quartz", "https://stardewvalleywiki.com/mediawiki/images/5/5b/Fire_Quartz.png" },
            { "slime", "https://stardewvalleywiki.com/mediawiki/images/3/38/Slime.png" },
            { "bat-wing", "https://stardewvalleywiki.com/mediawiki/images/3/35/Bat_Wing.png" },
            { "solar-essence", "https://stardewvalleywiki.com/mediawiki/images/f/f4/Solar_Essence.png" },
            { "void-essence", "https://stardewvalleywiki.com/mediawiki/images/1/1f/Void_Essence.png" },
            { "bone-fragment", "https://stardewvalleywiki.com/mediawiki/images/9/97/Bone_Fragment.png" },
            { "amethyst", "https://stardewvalleywiki.com/mediawiki/images/2/2e/Amethyst.png" },
            { "aquamarine", "https://stardewvalleywiki.com/mediawiki/images/a/a2/Aquamarine.png" },
            { "diamond", "https://stardewvalleywiki.com/mediawiki/images/e/ea/Diamond.png" },
            { "emerald", "https://stardewvalleywiki.com/mediawiki/images/6/6a/Emerald.png" },
            { "ruby", "https://stardewvalleywiki.com/mediawiki/images/a/a9/Ruby.png" },
            { "topaz", "https://stardewvalleywiki.com/mediawiki/images/a/a5/Topaz.png" },
            { "iridium-ore", "https://stardewvalleywiki.com/mediawiki/images/e/e9/Iridium_Ore.png" },
            { "battery-pack", "https://stardewvalleywiki.com/mediawiki/images/2/25/Battery_Pack.png" },
            { "refined-quartz", "https://stardewvalleywiki.com/mediawiki/images/9/98/Refined_Quartz.png" },
            { "truffle", "https://stardewvalleywiki.com/mediawiki/images/f/f2/Truffle.png" },
            { "poppy", "https://stardewvalleywiki.com/mediawiki/images/3/37/Poppy.png" },
            { "maki-roll", "https://stardewvalleywiki.com/mediawiki/images/b/b6/Maki_Roll.png" },
            { "fried-egg", "https://stardewvalleywiki.com/mediawiki/images/1/18/Fried_Egg.png" },
            { "beet", "https://stardewvalleywiki.com/mediawiki/images/a/a4/Beet.png" },
            { "sea-urchin", "https://stardewvalleywiki.com/mediawiki/images/e/e7/Sea_Urchin.png" },
            { "amaranth", "https://stardewvalleywiki.com/mediawiki/images/f/f6/Amaranth.png" },
            { "starfruit", "https://stardewvalleywiki.com/mediawiki/images/d/db/Starfruit.png" },
            { "duck-feather", "https://stardewvalleywiki.com/mediawiki/images/f/f9/Duck_Feather.png" },
            { "red-cabbage", "https://stardewvalleywiki.com/mediawiki/images/2/2d/Red_Cabbage.png" },
            { "iridium-bar", "https://stardewvalleywiki.com/mediawiki/images/c/c4/Iridium_Bar.png" },
            { "nautilus-shell", "https://stardewvalleywiki.com/mediawiki/images/a/a4/Nautilus_Shell.png" },
            { "chub", "https://stardewvalleywiki.com/mediawiki/images/b/bd/Chub.png" },
            { "frozen-geode", "https://stardewvalleywiki.com/mediawiki/images/b/bf/Frozen_Geode.png" },
            { "wheat", "https://stardewvalleywiki.com/mediawiki/images/e/e2/Wheat.png" },
            { "hay", "https://stardewvalleywiki.com/mediawiki/images/a/aa/Hay.png" },
            { "rabbit's-foot", "https://stardewvalleywiki.com/mediawiki/images/c/ca/Rabbit%27s_Foot.png" },
            { "salmonberry", "https://stardewvalleywiki.com/mediawiki/images/5/59/Salmonberry.png" },
            { "cookie", "https://stardewvalleywiki.com/mediawiki/images/7/70/Cookie.png" },
            { "ancient-doll", "https://stardewvalleywiki.com/mediawiki/images/c/c0/Ancient_Doll.png" },
            { "ice-cream", "https://stardewvalleywiki.com/mediawiki/images/5/5d/Ice_Cream.png" },
            { "egg", "https://stardewvalleywiki.com/mediawiki/images/2/26/Egg.png" },
            { "milk", "https://stardewvalleywiki.com/mediawiki/images/9/92/Milk.png" },
            { "wheat-flour", "https://stardewvalleywiki.com/mediawiki/images/2/2c/Wheat_Flour.png" },
            { "prize-ticket", "https://stardewvalleywiki.com/mediawiki/images/e/ec/Prize_Ticket.png" },
            { "mystery-box", "https://stardewvalleywiki.com/mediawiki/images/8/87/Mystery_Box.png" },
            { "jack-o-lantern", "https://stardewvalleywiki.com/mediawiki/images/1/17/Jack-O-Lantern.png" },
            { "plum-pudding", "https://stardewvalleywiki.com/mediawiki/images/a/a0/Plum_Pudding.png" },
            { "stuffing", "https://stardewvalleywiki.com/mediawiki/images/9/9a/Stuffing.png" },
            { "powdermelon", "https://stardewvalleywiki.com/mediawiki/images/a/aa/Powdermelon.png" },
            { "gold", "https://stardewvalleywiki.com/mediawiki/images/1/10/Gold.png" },
            { "dinosaur-mayonnaise", "https://stardewvalleywiki.com/mediawiki/images/c/c3/Dinosaur_Mayonnaise.png" },
            { "prismatic-shard", "https://stardewvalleywiki.com/mediawiki/images/5/56/Prismatic_Shard.png" },
            { "void-salmon", "https://stardewvalleywiki.com/mediawiki/images/a/ad/Void_Salmon.png" },
            { "caviar", "https://stardewvalleywiki.com/mediawiki/images/8/89/Caviar.png" },
            { "coffee-bean", "https://stardewvalleywiki.com/mediawiki/images/3/33/Coffee_Bean.png" },
            { "garlic", "https://stardewvalleywiki.com/mediawiki/images/c/cc/Garlic.png"},
            { "rhubarb", "https://stardewvalleywiki.com/mediawiki/images/6/6e/Rhubarb.png" },
            { "strawberry", "https://stardewvalleywiki.com/mediawiki/images/6/6d/Strawberry.png" },
            { "unmilled-rice", "https://stardewvalleywiki.com/mediawiki/images/f/fe/Unmilled_Rice.png" },
            { "radish", "https://stardewvalleywiki.com/mediawiki/images/d/d5/Radish.png"},
            { "artichoke", "https://stardewvalleywiki.com/mediawiki/images/d/dd/Artichoke.png" },
            { "bok-choy", "https://stardewvalleywiki.com/mediawiki/images/4/40/Bok_Choy.png" },
            { "cranberries", "https://stardewvalleywiki.com/mediawiki/images/6/6e/Cranberries.png" },
            { "dinosaur-egg", "https://stardewvalleywiki.com/mediawiki/images/a/a1/Dinosaur_Egg.png" },
            { "void-egg", "https://stardewvalleywiki.com/mediawiki/images/5/58/Void_Egg.png" },
            { "goat-milk", "https://stardewvalleywiki.com/mediawiki/images/4/45/Goat_Milk.png" },
            { "beer", "https://stardewvalleywiki.com/mediawiki/images/b/b3/Beer.png" },
            { "vinegar", "https://stardewvalleywiki.com/mediawiki/images/f/fe/Vinegar.png" },
            { "coffee", "https://stardewvalleywiki.com/mediawiki/images/e/e9/Coffee.png" },
            { "mayonnaise", "https://stardewvalleywiki.com/mediawiki/images/4/4e/Mayonnaise.png" },
            { "duck-mayonnaise", "https://stardewvalleywiki.com/mediawiki/images/2/23/Duck_Mayonnaise.png" },
            { "void-mayonnaise", "https://stardewvalleywiki.com/mediawiki/images/f/f3/Void_Mayonnaise.png" },
            { "oil", "https://stardewvalleywiki.com/mediawiki/images/0/06/Oil.png" },
            { "pickles", "https://stardewvalleywiki.com/mediawiki/images/c/c7/Pickles.png" },
            { "dried-fruit", "https://stardewvalleywiki.com/mediawiki/images/6/66/Dried_Fruit.png" },
            { "dried-mushrooms", "https://stardewvalleywiki.com/mediawiki/images/1/1a/Dried_Mushrooms.png" },
            { "raisins", "https://stardewvalleywiki.com/mediawiki/images/0/06/Raisins.png" },
            { "smoked-fish", "https://stardewvalleywiki.com/mediawiki/images/4/4c/Smoked_Fish.png" },
            { "chanterelle", "https://stardewvalleywiki.com/mediawiki/images/1/1d/Chanterelle.png" },
            { "clay", "https://stardewvalleywiki.com/mediawiki/images/a/a2/Clay.png" },
            { "rainbow-shell", "https://stardewvalleywiki.com/mediawiki/images/3/3d/Rainbow_Shell.png" },
            { "coral", "https://stardewvalleywiki.com/mediawiki/images/b/b1/Coral.png" },
            { "smallmouth-bass", "https://stardewvalleywiki.com/mediawiki/images/a/a5/Smallmouth_Bass.png" },
            { "rainbow-trout", "https://stardewvalleywiki.com/mediawiki/images/1/14/Rainbow_Trout.png" },
            { "salmon", "https://stardewvalleywiki.com/mediawiki/images/e/e0/Salmon.png" },
            { "perch", "https://stardewvalleywiki.com/mediawiki/images/4/43/Perch.png" },
            { "pike", "https://stardewvalleywiki.com/mediawiki/images/3/31/Pike.png" },
            { "dorado", "https://stardewvalleywiki.com/mediawiki/images/1/18/Dorado.png" },
            { "lingcod", "https://stardewvalleywiki.com/mediawiki/images/8/87/Lingcod.png" },
            { "anchovy", "https://stardewvalleywiki.com/mediawiki/images/7/79/Anchovy.png" },
            { "red-mullet", "https://stardewvalleywiki.com/mediawiki/images/f/f2/Red_Mullet.png" },
            { "herring", "https://stardewvalleywiki.com/mediawiki/images/f/f1/Herring.png" },
            { "squid", "https://stardewvalleywiki.com/mediawiki/images/8/81/Squid.png" },
            { "sea-cucumber", "https://stardewvalleywiki.com/mediawiki/images/a/a9/Sea_Cucumber.png" },
            { "super-cucumber", "https://stardewvalleywiki.com/mediawiki/images/d/d5/Super_Cucumber.png" },
            { "flounder", "https://stardewvalleywiki.com/mediawiki/images/8/85/Flounder.png" },
            { "albacore", "https://stardewvalleywiki.com/mediawiki/images/e/e1/Albacore.png" },
            { "halibut", "https://stardewvalleywiki.com/mediawiki/images/0/02/Halibut.png" },
            { "midnight-squid", "https://stardewvalleywiki.com/mediawiki/images/8/83/Midnight_Squid.png" },
            { "spook-fish", "https://stardewvalleywiki.com/mediawiki/images/8/8c/Spook_Fish.png" },
            { "midnight-carp", "https://stardewvalleywiki.com/mediawiki/images/3/33/Midnight_Carp.png" },
            { "stonefish", "https://stardewvalleywiki.com/mediawiki/images/0/03/Stonefish.png" },
            { "ice-pip", "https://stardewvalleywiki.com/mediawiki/images/6/63/Ice_Pip.png" },
            { "goby", "https://stardewvalleywiki.com/mediawiki/images/6/67/Goby.png" },
            { "jade", "https://stardewvalleywiki.com/mediawiki/images/7/7e/Jade.png" },
            { "bug-meat", "https://stardewvalleywiki.com/mediawiki/images/b/b6/Bug_Meat.png" },
            { "complete-breakfast", "https://stardewvalleywiki.com/mediawiki/images/3/3d/Complete_Breakfast.png" },
            { "sashimi", "https://stardewvalleywiki.com/mediawiki/images/4/41/Sashimi.png" },
            { "blueberry-tart", "https://stardewvalleywiki.com/mediawiki/images/9/9b/Blueberry_Tart.png" },
            { "salmon-dinner", "https://stardewvalleywiki.com/mediawiki/images/8/8b/Salmon_Dinner.png" },
            { "tea-leaves", "https://stardewvalleywiki.com/mediawiki/images/5/5b/Tea_Leaves.png" },
            { "stingray", "https://stardewvalleywiki.com/mediawiki/images/3/3a/Stingray.png" },
            { "radioactive-bar", "https://stardewvalleywiki.com/mediawiki/images/7/7e/Radioactive_Bar.png" },
            { "iridium-band", "https://stardewvalleywiki.com/mediawiki/images/7/77/Iridium_Band.png" },
            { "mango", "https://stardewvalleywiki.com/mediawiki/images/3/38/Mango.png" },
            { "banana", "https://stardewvalleywiki.com/mediawiki/images/6/69/Banana.png" },
        };

        private Dictionary<string, string> translateMonsters = new Dictionary<string, string>()
        {
            {"Slimes", "Slimes" },
            {"Shadows", "Void Spirits" },
            {"Bats", "Bats" },
            {"Skeletons", "Skeletons" },
            {"Insects", "Cave Insects" },
            {"Duggy", "Duggies" },
            {"DustSpirits", "Dust Sprites" },
            {"Crabs", "Rock Crabs" },
            {"Mummies", "Mummies" },
            {"Dinos", "Pepper Rexes" },
            {"Serpents", "Serpents" },
            {"FlameSpirits","Flame Spirits" }
        };

        private string[] fishExceptions = {"Seaweed","Green Algae","White Algae","Sea Jelly","River Jelly","Cave Jelly" };

        private string[] milestoneExceptions = { "Gold", "Hay", "Frozen Geode" };

        private string runName = "";
        private string date = "";
        private BundleData bundleData = new BundleData();
        private List<string> bundleQueue = new List<string>();
        private List<string> quests = new List<string>();
        private List<string> specialOrders = new List<string>();
        private List<int> friendshipLevel = new List<int>();
        private List<int> friendshipHearts = new List<int>();
        private List<string> cachedCooking = new List<string>();
        private List<string> cachedCrafting = new List<string>();
        private List<int> achievements = new List<int>();
        private bool saveLaucnhedFlag = false;
        private bool bundleFlag = false;
        private bool shippedFlag = false;
        private bool chestFlag = false;
        private bool farmingMastery = false;
        private bool miningMastery = false;
        private bool foragingMastery = false;
        private bool fishingMastery = false;
        private bool combatMastery = false;
        private bool reverseFlags = false;
        private int houseUpdate = 0;
        private int trashUpdade = 0;
        private int bagSize = 0;
        private int money = 0;

        private EventData eventData = new EventData();
        private MetaData meta = new MetaData();
        private History history = new History();
        private OutsideInfo outsideInfo = new OutsideInfo();

        public override void Entry(IModHelper helper)
        {
            var model = this.Helper.Data.ReadJsonFile<Approved>("Approved.json") ?? new Approved();
            if(model.mode == "Enabled")
            {
                helper.Events.GameLoop.SaveLoaded += this.SaveLoaded;
                helper.Events.GameLoop.DayEnding += this.DayEnding;
                helper.Events.World.ChestInventoryChanged += this.ChestInventoryChanged;
                helper.Events.Player.InventoryChanged += this.InventoryChanged;
                helper.Events.GameLoop.DayStarted += this.DayStarted;
                helper.Events.Player.LevelChanged += this.LevelChanged;
                helper.Events.GameLoop.OneSecondUpdateTicked += this.OneSecondUpdateTicked;
                helper.Events.Display.MenuChanged += this.MenuChanged;
            }
        }

        private void DayStarted(object? sender, DayStartedEventArgs e) {
            date = getStardewDate();

            var newHouse = StardewValley.Game1.player.HouseUpgradeLevel;
            var newTrash = StardewValley.Game1.player.trashCanLevel;

            if (houseUpdate != newHouse)
            {
                outsideInfo.indexes[1] += 1;
                var category = "milestone";
                var item = "House Upgrade " + newHouse.ToString();
                var reverseItem = reverseFormat(item);

                eventData.id.Add(generateID());
                eventData.runIndex.Add(outsideInfo.indexes[1]);
                eventData.category.Add(category);
                eventData.categorySprite.Add(categorySprites[category]);
                eventData.lastEvent.Add(reverseItem);
                eventData.itemSprite.Add(miscSprites[reverseItem]);
                eventData.description.Add(FormatDescription(category, item));
                eventData.date.Add(date);
                history.categories.Add(category);
                history.events.Add(reverseItem);

                StardewValley.Game1.chatBox.addMessage("[SVDM]: Milestone " + item + " has been added to your collection!", new Microsoft.Xna.Framework.Color(255, 255, 255));
                Monitor.Log("Milestone " + item + " has been added to your collection!");

                houseUpdate = newHouse;
            }

            if(trashUpdade != newTrash)
            {
                outsideInfo.indexes[1] += 1;
                var category = "milestone";
                var item = "";

                if (newTrash == 1) item = "Copper Trah Can";
                else if (newTrash == 2) item = "Silver Trash Can";
                else if (newTrash == 3) item = "Gold Trash Can";
                else item = "Iridium Trash Can";

                var reverseItem = reverseFormat(item);

                eventData.id.Add(generateID());
                eventData.runIndex.Add(outsideInfo.indexes[1]);
                eventData.category.Add(category);
                eventData.categorySprite.Add(categorySprites[category]);
                eventData.lastEvent.Add(reverseItem);
                eventData.itemSprite.Add(miscSprites[reverseItem]);
                eventData.description.Add(FormatDescription(category, item));
                eventData.date.Add(date);
                history.categories.Add(category);
                history.events.Add(reverseItem);

                StardewValley.Game1.chatBox.addMessage("[SVDM]: Milestone " + item + " has been added to your collection!", new Microsoft.Xna.Framework.Color(255, 255, 255));
                Monitor.Log("Milestone " + item + " has been added to your collection!");

                trashUpdade = newTrash;
            }

            checkObelisks();
        }

        private void SaveLoaded(object? sender, SaveLoadedEventArgs e)
        {
            this.outsideInfo = this.Helper.Data.ReadJsonFile<OutsideInfo>("OutsideInfo.json") ?? new OutsideInfo();

            initQuests();
            initSpecialOrders();
            initFriendship();
            initBundles();
            initPowers();
            initAchievements();

            houseUpdate = StardewValley.Game1.player.HouseUpgradeLevel;
            trashUpdade = StardewValley.Game1.player.trashCanLevel;
            bagSize = StardewValley.Game1.player.MaxItems;
            money = Game1.player.team.money.Value;

            this.Helper.ConsoleCommands.Add("addRunName", "Change current run's name.", ccRunName);
            this.Helper.ConsoleCommands.Add("addEvent", "Add an event manually to your run.", ccManualAddition);
            this.Helper.ConsoleCommands.Add("runFinished", "Register this run as complete.", ccFinished);
            this.Helper.ConsoleCommands.Add("helpMe", "Get info about this mod.", ccHelp);

            var model = this.Helper.Data.ReadJsonFile<Approved>("Approved.json") ?? new Approved();
            if (!model.farmerNames.Contains(StardewValley.Game1.player.Name) || !model.farmNames.Contains(StardewValley.Game1.player.farmName.Value))
            {

                model.farmerNames.Add(StardewValley.Game1.player.Name);
                model.farmNames.Add(StardewValley.Game1.player.farmName.Value);
                this.Helper.Data.WriteJsonFile("Approved.json", model);

                StardewValley.Game1.chatBox.addMessage("[SVDM]: Welcome to the valley, " + StardewValley.Game1.player.Name + "! Use addRunName 'name' in the SMAPI console to begin tracking your run :3", new Microsoft.Xna.Framework.Color(255, 255, 255));

                //robin
                outsideInfo.indexes[1] += 1;
                var reverseEvent = "robin";

                eventData.id.Add(generateID());
                eventData.runIndex.Add(outsideInfo.indexes[1]);
                eventData.category.Add("friendship-0");
                eventData.lastEvent.Add(reverseEvent);
                eventData.categorySprite.Add(categorySprites["friendship"]);
                eventData.itemSprite.Add(characterSprites[reverseEvent]);
                eventData.description.Add(FormatDescription("friendship-0", "Robin"));
                eventData.date.Add("Spring 1 Year 1");

                history.categories.Add("friendship-0");
                history.events.Add(reverseEvent);

                StardewValley.Game1.chatBox.addMessage("[SVDM]: Friendship 0 Robin has been achieved!", new Microsoft.Xna.Framework.Color(255, 255, 255));
                Monitor.Log("Friendship 0 Robin has been achieved!");

                //lewis
                outsideInfo.indexes[1] += 1;
                reverseEvent = "lewis";

                eventData.id.Add(generateID());
                eventData.runIndex.Add(outsideInfo.indexes[1]);
                eventData.category.Add("friendship-0");
                eventData.lastEvent.Add(reverseEvent);
                eventData.categorySprite.Add(categorySprites["friendship"]);
                eventData.itemSprite.Add(characterSprites[reverseEvent]);
                eventData.description.Add(FormatDescription("friendship-0", "Lewis"));
                eventData.date.Add("Spring 1 Year 1");

                history.categories.Add("friendship-0");
                history.events.Add(reverseEvent);

                StardewValley.Game1.chatBox.addMessage("[SVDM]: Friendship 0 Lewis has been achieved!", new Microsoft.Xna.Framework.Color(255, 255, 255));
                Monitor.Log("Friendship 0 Lewis has been achieved!");

                StardewValley.Game1.chatBox.addMessage("[SVDM]: Quest Introductions has been added to your quest log!", new Microsoft.Xna.Framework.Color(255, 255, 255));
                Monitor.Log("Quest Introductions has been added to your quest log!");
            }
            else
            {
                StardewValley.Game1.chatBox.addMessage("[SVDM]: Welcome back, " + StardewValley.Game1.player.Name + "! :3", new Microsoft.Xna.Framework.Color(255, 255, 255));
                this.meta = this.Helper.Data.ReadJsonFile<MetaData>("MetaData.json") ?? new MetaData();
                this.eventData = new EventData();
                this.eventData.runName = this.meta.runName;
                this.history = this.Helper.Data.ReadJsonFile<History>("History.json") ?? new History();
            }

            saveLaucnhedFlag = true;
        }

        private void DayEnding(object? sender, DayEndingEventArgs e)
        {

            var mLength = eventData.lastEvent.Count - 1;

            if (meta.farmerName != StardewValley.Game1.player.Name || meta.farmName != StardewValley.Game1.player.farmName.Value)
            {
                this.outsideInfo.indexes[0] += 1;
                eventData.runName = runName;

                meta.runName = runName;
                meta.index = outsideInfo.indexes[0];
                meta.farmName = StardewValley.Game1.player.farmName.Value;
                meta.farmerName = StardewValley.Game1.player.Name;
                if (StardewValley.Game1.player.hasPet())
                {
                    meta.petName = StardewValley.Game1.player.getPetDisplayName();
                }
                else
                {
                    meta.petName = "";
                }
                meta.version = StardewValley.Game1.version;
                meta.status = "Current";
            }else if(StardewValley.Game1.player.hasPet() && meta.petName == "")
            {
                meta.petName = StardewValley.Game1.player.getPetName();
            }
            
            if(mLength >= 0)
            {
                meta.lastCategory = eventData.category[mLength];
                meta.lastEvent = eventData.lastEvent[mLength];
                meta.itemSprite = eventData.itemSprite[mLength];
                meta.date = eventData.date[mLength];
                meta.description = eventData.description[mLength];
            }

            this.Helper.Data.WriteJsonFile<History>("History.json", history);
            this.Helper.Data.WriteJsonFile<OutsideInfo>("OutsideInfo.json", outsideInfo);
            this.Helper.Data.WriteJsonFile<MetaData>("MetaData.json", meta);
            this.Helper.Data.WriteJsonFile<EventData>("EventData.json", eventData);
        }

        private void InventoryChanged(object? sender, InventoryChangedEventArgs e)
        {
            foreach (StardewValley.Item item in e.Added)
            {
                var reverseItem = reverseFormat(item.Name);
                var category = item.getCategoryName();

                if (item.Name.Contains("Dwarf Scroll"))
                {
                    var tempSplit = item.Name.Split(' ');
                    reverseItem = "dwarf-scroll-" + tempSplit[2];
                }
                else if (item.ItemId == "126")
                {
                    reverseItem = "strange-doll-green";
                }
                else if (item.ItemId == "127")
                {
                    reverseItem = "strange-doll-yellow";
                }
                else if (item.Name == "Rarecrow")
                {
                    var tempDescription = item.getDescription().Split("(");
                    var numChar = tempDescription[1][0];
                    var newItem = "Rarecrow " + numChar;
                    reverseItem = reverseFormat(newItem);
                }
                else if (item.Name == "Spring Seeds") reverseItem = "wild-seeds-sp";
                else if (item.Name == "Summer Seeds") reverseItem = "wild-seeds-su";
                else if (item.Name == "Fall Seeds") reverseItem = "wild-seeds-fa";
                else if (item.Name == "Winter Seeds") reverseItem = "wild-seeds-wi";
                else if (item.Name.Contains("Smoked")) reverseItem = "smoked-fish";
                else if (item.ItemId == "174") reverseItem = "large-egg-white";
                else if (item.ItemId == "176") reverseItem = "egg-white";
                else if (item.ItemId == "180") reverseItem = "egg-brown";
                else if (item.ItemId == "182") reverseItem = "large-egg-brown";
                else if (item.Name.Contains("Honey")) reverseItem = "honey";
                else if (category == "Book") reverseItem = reverseFormat(item.DisplayName);

                var mineralCheck = false;
                var artifactCheck = false;
                var fishCheck = false;
                var bookCheck = false;

                var milestoneCheck = false;
                var fieldOfficeCheck = false;
                var historyCheck = false;

                if (category == "Mineral")
                {
                    mineralCheck = true;
                    category = "minerals";
                }
                else if (category == "Artifact")
                {
                    artifactCheck = true;
                    category = "artifacts";
                }
                else if (category == "Fish" || fishExceptions.Contains(item.Name))
                {
                    fishCheck = true;
                    category = "fish";
                }
                else if (cookingSprites.ContainsKey(reverseItem))
                {
                    category = "cooking";
                    cachedCooking.Add(item.Name);
                }
                else if(category == "Book" && bookSprites.ContainsKey(reverseItem))
                {
                    category = "books";
                    bookCheck = true;
                    
                }
                else if (fieldOfficeSprites.ContainsKey(reverseItem))
                {
                    fieldOfficeCheck = true;
                    category = "island-field-office";
                }
                else if ((miscSprites.ContainsKey(reverseItem) || item.Name == "Rarecrow") && !milestoneExceptions.Contains(item.Name))
                {
                    milestoneCheck = true;
                    category = "milestone";
                }
                else if (craftingSprites.ContainsKey(reverseItem))
                {
                    category = "crafting";
                    if (item.Name == "Spring Seeds") cachedCrafting.Add("Wild Seeds Sp");
                    else if (item.Name == "Summer Seeds") cachedCrafting.Add("Wild Seeds Su");
                    else if (item.Name == "Fall Seeds") cachedCrafting.Add("Wild Seeds Fa");
                    else if (item.Name == "Winter Seeds") cachedCrafting.Add("Wild Seeds Wi");
                    else cachedCrafting.Add(item.Name);
                }


                if (mineralCheck || artifactCheck || fishCheck || milestoneCheck || fieldOfficeCheck || bookCheck)
                {
                        var tempIndexes = Enumerable.Range(0, history.events.Count)
                        .Where(i => history.events[i] == reverseItem)
                        .ToList();
                        var tempIt = 0;
                        var tempCheck = false;
                        while (tempIt < tempIndexes.Count)
                        {
                            if (history.categories[tempIndexes[tempIt]] == category) tempCheck = true;
                            tempIt++;
                        }
                        if (!tempCheck) historyCheck = true;
                }


                if (mineralCheck && historyCheck)
                {
                    outsideInfo.indexes[1] += 1;

                    eventData.id.Add(generateID());
                    eventData.runIndex.Add(outsideInfo.indexes[1]);
                    eventData.category.Add(category);
                    eventData.categorySprite.Add(categorySprites[category]);
                    eventData.lastEvent.Add(reverseItem);
                    eventData.itemSprite.Add(mineralSprites[reverseItem]);
                    eventData.description.Add(FormatDescription(category, item.Name));
                    eventData.date.Add(date);
                    history.categories.Add(category);
                    history.events.Add(reverseItem);

                    StardewValley.Game1.chatBox.addMessage("[SVDM]: Mineral " + item.Name + " has been added to your collection!", new Microsoft.Xna.Framework.Color(255, 255, 255));
                    Monitor.Log("Mineral " + item.Name + " has been added to your collection!");
                }
                else if (artifactCheck && historyCheck)
                {
                    outsideInfo.indexes[1] += 1;

                    eventData.id.Add(generateID());
                    eventData.runIndex.Add(outsideInfo.indexes[1]);
                    eventData.category.Add(category);
                    eventData.categorySprite.Add(categorySprites[category]);
                    eventData.lastEvent.Add(reverseItem);
                    eventData.itemSprite.Add(artifactSprites[reverseItem]);
                    eventData.description.Add(FormatDescription(category, item.Name));
                    eventData.date.Add(date);
                    history.categories.Add(category);
                    history.events.Add(reverseItem);

                    StardewValley.Game1.chatBox.addMessage("[SVDM]: Artifact " + item.Name + " has been added to your collection!", new Microsoft.Xna.Framework.Color(255, 255, 255));
                    Monitor.Log("Artifact " + item.Name + " has been added to your collection!");
                }
                else if (fishCheck && historyCheck && checkCrabPot(item.Name))
                {
                    outsideInfo.indexes[1] += 1;

                    eventData.id.Add(generateID());
                    eventData.runIndex.Add(outsideInfo.indexes[1]);
                    eventData.category.Add(category);
                    eventData.categorySprite.Add(categorySprites[category]);
                    eventData.lastEvent.Add(reverseItem);
                    eventData.itemSprite.Add(fishSprites[reverseItem]);
                    eventData.description.Add(FormatDescription(category, item.Name));
                    eventData.date.Add(date);
                    history.categories.Add(category);
                    history.events.Add(reverseItem);

                    StardewValley.Game1.chatBox.addMessage("[SVDM]: Fish " + item.Name + " has been added to your collection!", new Microsoft.Xna.Framework.Color(255, 255, 255));
                    Monitor.Log("Fish " + item.Name + " has been added to your collection!");
                }
                else if (milestoneCheck && historyCheck)
                {
                    outsideInfo.indexes[1] += 1;
                    var newItem = item.Name;

                    if(item.Name == "Rarecrow")
                    {
                        var tempDescription = item.getDescription().Split("(");
                        var numChar = tempDescription[1][0];
                        newItem = "Rarecrow " + numChar;
                    }


                    eventData.id.Add(generateID());
                    eventData.runIndex.Add(outsideInfo.indexes[1]);
                    eventData.category.Add(category);
                    eventData.categorySprite.Add(categorySprites[category]);
                    eventData.lastEvent.Add(reverseItem);
                    eventData.itemSprite.Add(miscSprites[reverseItem]);
                    eventData.description.Add(FormatDescription(category, newItem));
                    eventData.date.Add(date);
                    history.categories.Add(category);
                    history.events.Add(reverseItem);

                    StardewValley.Game1.chatBox.addMessage("[SVDM]: Milestone " + newItem + " has been added completed!", new Microsoft.Xna.Framework.Color(255, 255, 255));
                    Monitor.Log("Milestone " + newItem + " has been completed!");
                }
                else if (fieldOfficeCheck && historyCheck)
                {
                    outsideInfo.indexes[1] += 1;

                    eventData.id.Add(generateID());
                    eventData.runIndex.Add(outsideInfo.indexes[1]);
                    eventData.category.Add(category);
                    eventData.categorySprite.Add(categorySprites[category]);
                    eventData.lastEvent.Add(reverseItem);
                    eventData.itemSprite.Add(fieldOfficeSprites[reverseItem]);
                    eventData.description.Add(FormatDescription(category, item.Name));
                    eventData.date.Add(date);
                    history.categories.Add(category);
                    history.events.Add(reverseItem);

                    StardewValley.Game1.chatBox.addMessage("[SVDM]: Island Field Office Artifact " + item.Name + " has been added to your collection!", new Microsoft.Xna.Framework.Color(255, 255, 255));
                    Monitor.Log("Island Field Office Artifact " + item.Name + " has been added to your collection!");
                }
                else if (bookCheck && historyCheck)
                {
                    var newItem = item.DisplayName;

                    outsideInfo.indexes[1] += 1;

                    eventData.id.Add(generateID());
                    eventData.runIndex.Add(outsideInfo.indexes[1]);
                    eventData.category.Add(category);
                    eventData.categorySprite.Add(categorySprites[category]);
                    eventData.lastEvent.Add(reverseItem);
                    eventData.itemSprite.Add(bookSprites[reverseItem]);
                    eventData.description.Add(FormatDescription(category, newItem));
                    eventData.date.Add(date);
                    history.categories.Add(category);
                    history.events.Add(reverseItem);

                    StardewValley.Game1.chatBox.addMessage("[SVDM]: Book " + newItem + " has been added to your collection!", new Microsoft.Xna.Framework.Color(255, 255, 255));
                    Monitor.Log("Book " + newItem + " has been added to your collection!");
                }
            }

            // quantity changed and removed are mutually exclusive
            foreach(StardewModdingAPI.Events.ItemStackSizeChange itemStackSizeChange in e.QuantityChanged)
            {
                var item = itemStackSizeChange.Item.Name;
                var reverseItem = reverseFormat(itemStackSizeChange.Item.Name);

                if (itemStackSizeChange.Item.ItemId == "DriedFruit")
                {
                    item = "Dried Fruit";
                    reverseItem = "dried-fruit";
                }
                else if (itemStackSizeChange.Item.ItemId == "DriedMushrooms")
                {
                    item = "Dried Mushrooms";
                    reverseItem = "dried-mushrooms";
                }else if(itemStackSizeChange.Item.ItemId == "SmokedFish")
                {
                    item = "Smoked Fish";
                    reverseItem = "smoked-fish";
                }
                else if (itemStackSizeChange.Item.Name.Contains("Roe") && !itemStackSizeChange.Item.Name.Contains("Aged"))
                {
                    item = "Roe";
                    reverseItem = "roe";
                }
                else if (itemStackSizeChange.Item.Name.Contains("Roe") && itemStackSizeChange.Item.Name.Contains("Aged"))
                {
                    item = "Aged Roe";
                    reverseItem = "aged-roe";
                }
                else if (itemStackSizeChange.Item.Name.Contains("Wine"))
                {
                    item = "Wine";
                    reverseItem = "wine";
                }
                else if (itemStackSizeChange.Item.Name.Contains("Juice"))
                {
                    item = "Juice";
                    reverseItem = "juice";
                }
                else if (itemStackSizeChange.Item.Name.Contains("Jelly"))
                {
                    item = "Jelly";
                    reverseItem = "jelly";
                }
                else if (itemStackSizeChange.Item.Name.Contains("Pickles"))
                {
                    item = "Pickles";
                    reverseItem = "pickles";
                }
                else if (itemStackSizeChange.Item.Name.Contains("Honey"))
                {
                    item = "Honey";
                    reverseItem = "honey";
                }
                else if (itemStackSizeChange.Item.ItemId == "174")
                {
                    item = "Large Egg White";
                    reverseItem = "large-egg-white";
                }
                else if (itemStackSizeChange.Item.ItemId == "176")
                {
                    item = "Egg White";
                    reverseItem = "egg-white";
                }
                else if (itemStackSizeChange.Item.ItemId == "180")
                {
                    item = "Egg Brown";
                    reverseItem = "egg-brown";
                }
                else if (itemStackSizeChange.Item.ItemId == "182")
                {
                    item = "Large Egg Brown";
                    reverseItem = "large-egg-brown";
                }


                if (!chestFlag && shippedFlag && shippedSprites.ContainsKey(reverseItem))
                {
                    var historyCheck = false;
                    var category = "shipped";
                    var tempIndexes = Enumerable.Range(0, history.events.Count)
                        .Where(i => history.events[i] == reverseItem)
                        .ToList();
                    var tempIt = 0;
                    var tempCheck = false;
                    while (tempIt < tempIndexes.Count)
                    {
                        if (history.categories[tempIndexes[tempIt]] == category) tempCheck = true;
                        tempIt++;
                    }
                    if (!tempCheck) historyCheck = true;

                    if (historyCheck)
                    {
                        outsideInfo.indexes[1] += 1;

                        eventData.id.Add(generateID());
                        eventData.runIndex.Add(outsideInfo.indexes[1]);
                        eventData.category.Add(category);
                        eventData.categorySprite.Add(categorySprites[category]);
                        eventData.lastEvent.Add(reverseItem);
                        eventData.itemSprite.Add(shippedSprites[reverseItem]);
                        eventData.description.Add(FormatDescription(category, item));
                        eventData.date.Add(date);
                        history.categories.Add(category);
                        history.events.Add(reverseItem);

                        StardewValley.Game1.chatBox.addMessage("[SVDM]: Shipped " + item + " has been added to your collection!", new Microsoft.Xna.Framework.Color(255, 255, 255));
                        Monitor.Log("Shipped " + item + " has been added to your collection!");
                    }
                }
                else if (bundleFlag && !bundleQueue.Contains(item))
                {
                    bundleQueue.Add(item);
                }
            }

            // same applies
            foreach(StardewValley.Item item in e.Removed)
            {
                var newItem = item.Name;
                var reverseItem = reverseFormat(item.Name);

                if (item.ItemId == "DriedFruit")
                {
                    newItem = "Dried Fruit";
                    reverseItem = "dried-fruit";
                }
                else if (item.ItemId == "DriedMushrooms")
                {
                    newItem = "Dried Mushrooms";
                    reverseItem = "dried-mushrooms";
                }
                else if (item.ItemId == "SmokedFish")
                {
                    newItem = "Smoked Fish";
                    reverseItem = "smoked-fish";
                }
                else if (item.Name.Contains("Roe") && !item.Name.Contains("Aged"))
                {
                    newItem = "Roe";
                    reverseItem = "roe";
                }
                else if (item.Name.Contains("Roe") && item.Name.Contains("Aged"))
                {
                    newItem = "Aged Roe";
                    reverseItem = "aged-roe";
                }
                else if (item.Name.Contains("Wine"))
                {
                    newItem = "Wine";
                    reverseItem = "wine";
                }
                else if (item.Name.Contains("Juice"))
                {
                    newItem = "Juice";
                    reverseItem = "juice";
                }
                else if (item.Name.Contains("Jelly"))
                {
                    newItem = "Jelly";
                    reverseItem = "jelly";
                }
                else if (item.Name.Contains("Pickles"))
                {
                    newItem = "Pickles";
                    reverseItem = "pickles";
                }
                else if (item.Name.Contains("Honey"))
                {
                    newItem = "Honey";
                    reverseItem = "honey";
                }
                else if (item.ItemId == "174")
                {
                    newItem = "Large Egg White";
                    reverseItem = "large-egg-white";
                }
                else if (item.ItemId == "176")
                {
                    newItem = "Egg White";
                    reverseItem = "egg-white";
                }
                else if (item.ItemId == "180")
                {
                    newItem = "Egg Brown";
                    reverseItem = "egg-brown";
                }
                else if (item.ItemId == "182")
                {
                    newItem = "Large Egg Brown";
                    reverseItem = "large-egg-brown";
                }

                if (!chestFlag && shippedFlag && shippedSprites.ContainsKey(reverseItem))
                {
                    var historyCheck = false;
                    var category = "shipped";
                    var tempIndexes = Enumerable.Range(0, history.events.Count)
                        .Where(i => history.events[i] == reverseItem)
                        .ToList();
                    var tempIt = 0;
                    var tempCheck = false;
                    while (tempIt < tempIndexes.Count)
                    {
                        if (history.categories[tempIndexes[tempIt]] == category) tempCheck = true;
                        tempIt++;
                    }
                    if (!tempCheck) historyCheck = true;

                    if (historyCheck)
                    {
                        outsideInfo.indexes[1] += 1;

                        eventData.id.Add(generateID());
                        eventData.runIndex.Add(outsideInfo.indexes[1]);
                        eventData.category.Add(category);
                        eventData.categorySprite.Add(categorySprites[category]);
                        eventData.lastEvent.Add(reverseItem);
                        eventData.itemSprite.Add(shippedSprites[reverseItem]);
                        eventData.description.Add(FormatDescription(category, newItem));
                        eventData.date.Add(date);
                        history.categories.Add(category);
                        history.events.Add(reverseItem);

                        StardewValley.Game1.chatBox.addMessage("[SVDM]: Shipped " + newItem + " has been added to your collection!", new Microsoft.Xna.Framework.Color(255, 255, 255));
                        Monitor.Log("Shipped " + newItem + " has been added to your collection!");
                    }
                }
                else if (bundleFlag && !bundleQueue.Contains(newItem))
                {
                    bundleQueue.Add(newItem);
                }
            }
        }

        private void ChestInventoryChanged(object? sender, ChestInventoryChangedEventArgs e)
        {
            chestFlag = true;
        }

        private void LevelChanged(object? sender, LevelChangedEventArgs e)
        {
            outsideInfo.indexes[1] += 1;
            var lastEvent = "";
            var category = "level-up-" + e.NewLevel.ToString();

            eventData.id.Add(generateID());
            eventData.runIndex.Add(outsideInfo.indexes[1]);
            eventData.category.Add(category);

            if (e.Skill == StardewModdingAPI.Enums.SkillType.Mining) lastEvent = "mining";
            else if(e.Skill == StardewModdingAPI.Enums.SkillType.Fishing) lastEvent = "fishing";
            else if (e.Skill == StardewModdingAPI.Enums.SkillType.Farming) lastEvent = "farming";
            else if (e.Skill == StardewModdingAPI.Enums.SkillType.Foraging) lastEvent = "foraging";
            else if (e.Skill == StardewModdingAPI.Enums.SkillType.Combat) lastEvent = "combat";

            eventData.lastEvent.Add(lastEvent);
            eventData.categorySprite.Add("https://stardewvalleywiki.com/mediawiki/images/d/df/Mastery_Icon.png");
            eventData.itemSprite.Add(miscSprites[lastEvent]);
            eventData.description.Add(FormatDescription(category, customToUpper(lastEvent)));
            eventData.date.Add(date);

            Monitor.Log(customToUpper(lastEvent) + " has leveled up to " + e.NewLevel.ToString());
            StardewValley.Game1.chatBox.addMessage("[SVDM]: Level Up " + e.NewLevel + " " + customToUpper(lastEvent) + " has been achieved!", new Microsoft.Xna.Framework.Color(255, 255, 255));

        }

        private void OneSecondUpdateTicked(object? sender, OneSecondUpdateTickedEventArgs e) {
            if (e.IsMultipleOf(600) && saveLaucnhedFlag)
            {
                computeFriendship();
                computeQuestLog();
                computeSpecialOrders();
                checkWallet();
                checkCachedRecipes();
                checkBundles();
                checkPowers();
                checkAchievements();
                checkBagSize();
                checkStardrops();
                checkMonsterSlayer();
                if (money > Game1.player.team.money.Value) checkVaultBundles();
                money = Game1.player.team.money.Value;
            }
        }

        private void MenuChanged(object? sender, MenuChangedEventArgs e)
        {
            string menu = e.NewMenu?.ToString() ?? "Null Menu";
            if(menu == "StardewValley.Menus.ItemGrabMenu")
            {
                shippedFlag = true;
            }else if(menu == "StardewValley.Menus.JunimoNoteMenu")
            {
                bundleFlag = true;
            }
            else
            {
                shippedFlag = false;
                bundleFlag = false;
                chestFlag = false;
            }
        }

        private string FormatDescription(string category, string lastEvent) {
            var tempCategory = category.Split("-");
            var finalCategory = "";
            var ii = 0;
            while (ii < tempCategory.Length)
            {
                if (ii + 1 == tempCategory.Length)
                {
                    finalCategory += customToUpper(tempCategory[ii]);
                }
                else
                {
                    finalCategory += customToUpper(tempCategory[ii]) + " ";
                }
                ii++;
            }
            return finalCategory + " · " + lastEvent;
        }

        private string reverseFormat(string subject)
        {
            var tempSubject = subject.Split(" ");
            var finalSubject = "";
            var ii = 0;
            while (ii < tempSubject.Length)
            {
                if (ii + 1 == tempSubject.Length)
                {
                    finalSubject += customToLower(tempSubject[ii]);
                }
                else
                {
                    finalSubject += customToLower(tempSubject[ii]) + "-";
                }
                ii++;
            }
            return finalSubject;
        }

        private string customToUpper(string subject)
        {
            if (subject == null) return null;
            if(subject.Length > 1 && subject[0] != '\"') return char.ToUpper(subject[0]) + subject.Substring(1);
            else if (subject.Length > 1 && subject[0] == '\"') return '\"' + char.ToUpper(subject[1]) + subject.Substring(2);
            return subject.ToUpper();
        }

        private string customToLower(string subject)
        {
            if (subject == null) return null;
            if (subject.Length > 1 && subject[0] != '\"') return char.ToLower(subject[0]) + subject.Substring(1);
            else if(subject.Length > 1 && subject[0] == '\"') return '\"' + char.ToLower(subject[1]) + subject.Substring(2);
            return subject.ToLower();
        }

        private string getStardewDate()
        {
            return customToUpper(StardewValley.Game1.CurrentSeasonDisplayName) + " " + StardewValley.Game1.dayOfMonth + " Year " + StardewValley.Game1.year;
        }

        private int generateID()
        {
            var check = true;
            var id = 0;
            Random rnd = new Random();
            while (check)
            {
                id = rnd.Next(0, 1000000000);
                if (!outsideInfo.ids.Contains(id))
                {
                    outsideInfo.ids.Add(id);
                    check = false;
                }
            }
            return id;
        }

        private void ccRunName(string command, string[] args)
        {
            var ii = 0;
            var result = "";
            while (ii < args.Length)
            {
                if (ii + 1 == args.Length)
                {
                    result += args[ii];
                }
                else
                {
                    result += args[ii] + " ";
                }
                ii++;
            }
            runName = result;
            meta.runName = runName;
            eventData.runName = runName;
            Monitor.Log("This run's name is now: " + runName);
        }

        private void ccManualAddition(string command, string[] args)
        {
            var category = args[0];
            var reverseItem = args[1];
            var item = "";
            outsideInfo.indexes[1]++;

            var ii = 0;
            var tempItem = reverseItem.Split("-");
            while(ii < tempItem.Length)
            {
                if(ii + 1 == tempItem.Length) item += customToUpper(tempItem[ii]);
                else item += customToUpper(tempItem[ii]) + " ";
                ii++;
            }

            eventData.id.Add(generateID());
            eventData.runIndex.Add(outsideInfo.indexes[1]);
            eventData.category.Add(category);
            if (category.Contains("bundle")) eventData.categorySprite.Add(bundleCheck(category));
            else eventData.categorySprite.Add(categorySprites[category]);
            eventData.lastEvent.Add(reverseItem);
            eventData.itemSprite.Add(getManualEventSprite(category,reverseItem));
            eventData.description.Add(FormatDescription(category, item));
            eventData.date.Add(date);
            history.categories.Add(category);
            history.events.Add(reverseItem);

            StardewValley.Game1.chatBox.addMessage("[SVDM]: Manual Add " + item + " has been added to your collection!", new Microsoft.Xna.Framework.Color(255, 255, 255));
            Monitor.Log("Manual Add " + item + " has been added to your collection!");
        }

        private void ccHelp(string command, string[] args)
        {
            if (args.Length == 0) Monitor.Log("The commands for SVDM are addRunName, addEvent, and runFinished. You can get information about these commands with helpMe 'command'.");
            else if (args[0] == "addEvent") Monitor.Log("The command struc for addEvent is: addEvent category event season day year.");
            else if (args[0] == "addRunName") Monitor.Log("The command structure for addRunName is: addRunName 'name'. All characters (including spaces) are allowed.");
            else if (args[0] == "runFinished") Monitor.Log("Simply type runFinished to change the MetaData status to Completed.");
            else Monitor.Log("Please input a valid argument after the command.");
        }

        private void ccFinished(string command, string[] args)
        {
            meta.status = "Completed";
            Monitor.Log("This run has been marked as complete");
        }

        private void testingMethod()
        {

        }

        private void initQuests()
        {
            foreach (var val in StardewValley.Game1.player.questLog)
            {
                if (questSprites.Keys.Contains(reverseFormat(val.GetName())) && val.GetName() != "The Mysterious Qi")
                {
                    quests.Add(val.id.Value);
                }
            }
        }

        private void initSpecialOrders()
        {
            foreach (var val in StardewValley.Game1.player.team.specialOrders)
            {
                if (questSprites.Keys.Contains(reverseFormat(val.GetName())))
                {
                    specialOrders.Add(val.GetName());
                }
            }
        }

        private void computeQuestLog()
        {
            List<string> tempQuests = new List<string>();

            foreach (var val in StardewValley.Game1.player.questLog)
            {
                if (questSprites.Keys.Contains(reverseFormat(val.GetName().Replace("\"",""))) && val.GetName() != "The Mysterious Qi")
                {
                    tempQuests.Add(val.id.Value);
                }
            }

            var ii = 0;
            while (ii < quests.Count)
            {
                var quest = StardewValley.Quests.Quest.getQuestFromId(quests[ii]).GetName().Replace("\"", "");
                if (!tempQuests.Contains(quests[ii]) && (!history.events.Contains(reverseFormat(quest))))
                {
                    //add quests to event
                    outsideInfo.indexes[1] += 1;

                    eventData.id.Add(generateID());
                    eventData.runIndex.Add(outsideInfo.indexes[1]);
                    eventData.category.Add("quest");
                    eventData.lastEvent.Add(reverseFormat(quest));
                    eventData.itemSprite.Add(questSprites[reverseFormat(quest)]);
                    eventData.categorySprite.Add(categorySprites["quest"]);
                    eventData.description.Add(FormatDescription("Quest",quest));
                    eventData.date.Add(date);

                    history.events.Add(reverseFormat(quest));
                    history.categories.Add("quest");
                    quests.Remove(quests[ii]);

                    StardewValley.Game1.chatBox.addMessage("[SVDM]: Quest " + quest + " has been marked as complete! (10 Second Update Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                    Monitor.Log("Quest " + quest + " has been marked as complete! (10 Second Update Tick)");

                    if(quest == "Cryptic Note")
                    {
                        outsideInfo.indexes[1] += 1;
                        var item = "Iridium Snake Milk";
                        var reverseItem = "iridium-snake-milk";

                        eventData.id.Add(generateID());
                        eventData.runIndex.Add(outsideInfo.indexes[1]);
                        eventData.category.Add("milestone");
                        eventData.lastEvent.Add(reverseItem);
                        eventData.itemSprite.Add(miscSprites[reverseItem]);
                        eventData.categorySprite.Add(categorySprites["milestone"]);
                        eventData.description.Add(FormatDescription("Milestone", item));
                        eventData.date.Add(date);

                        history.events.Add(reverseItem);
                        history.categories.Add("milestone");

                        StardewValley.Game1.chatBox.addMessage("[SVDM]: Milestone " + item + " has been marked as complete! (10 Second Update Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                        Monitor.Log("Milestone " + item + " has been marked as complete! (10 Second Update Tick)");
                    }
                }
                ii++;
            }

            var jj = 0;
            while(jj < tempQuests.Count)
            {
                var quest = StardewValley.Quests.Quest.getQuestFromId(tempQuests[jj]).GetName().Replace("\"", "");

                if (!quests.Contains(tempQuests[jj]) && questSprites.ContainsKey(reverseFormat(quest)))
                {
                    quests.Add(tempQuests[jj]);

                    StardewValley.Game1.chatBox.addMessage("[SVDM]: Quest " + quest + " has been added to your quest log! (10 Second Update Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                    Monitor.Log("Quest " + quest + " has been added to your quest log! (10 Second Update Tick)");
                }
                jj++;
            }

        }

        private void computeSpecialOrders()
        {
            List<string> tempQuests = new List<string>();

            foreach (var val in StardewValley.Game1.player.team.specialOrders)
            {
                if (questSprites.Keys.Contains(reverseFormat(val.GetName())))
                {
                    tempQuests.Add(val.GetName());
                }
            }

            var ii = 0;
            while (ii < specialOrders.Count)
            {
                var quest = specialOrders[ii];
                if (!tempQuests.Contains(specialOrders[ii]) && (!history.events.Contains(reverseFormat(quest))))
                {
                    //add quests to event
                    outsideInfo.indexes[1] += 1;

                    eventData.id.Add(generateID());
                    eventData.runIndex.Add(outsideInfo.indexes[1]);
                    eventData.category.Add("quest");
                    eventData.lastEvent.Add(reverseFormat(quest));
                    eventData.itemSprite.Add(questSprites[reverseFormat(quest)]);
                    eventData.categorySprite.Add(categorySprites["quest"]);
                    eventData.description.Add(FormatDescription("Quest",quest));
                    eventData.date.Add(date);

                    history.events.Add(reverseFormat(quest));
                    history.categories.Add("quest");
                    specialOrders.Remove(specialOrders[ii]);

                    StardewValley.Game1.chatBox.addMessage("[SVDM]: Quest " + quest + " has been marked as complete! (10 Second Update Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                    Monitor.Log("Quest " + quest + " has been marked as complete! (10 Second Update Tick)");
                }
                ii++;
            }

            var jj = 0;
            while (jj < tempQuests.Count)
            {
                var quest = tempQuests[jj];

                if (!specialOrders.Contains(tempQuests[jj]) && questSprites.ContainsKey(reverseFormat(quest)))
                {
                    specialOrders.Add(tempQuests[jj]);

                    StardewValley.Game1.chatBox.addMessage("[SVDM]: Quest " + quest + " has been added to your quest log! (10 Second Update Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                    Monitor.Log("Quest " + quest + " has been added to your quest log! (10 Second Update Tick)");
                }
                jj++;
            }
        }

        private void checkWallet()
        {
            List<bool> flags = new List<bool>();

            flags.Add(StardewValley.Game1.player.canUnderstandDwarves); //dwarvish translation guide
            flags.Add(StardewValley.Game1.player.hasRustyKey); //rusty key
            flags.Add(StardewValley.Game1.player.hasClubCard); //club card
            flags.Add(StardewValley.Game1.player.hasSpecialCharm); //special charm
            flags.Add(StardewValley.Game1.player.hasSkullKey); //skull key
            flags.Add(StardewValley.Game1.player.hasMagnifyingGlass); //magnifying glass
            flags.Add(StardewValley.Game1.player.hasDarkTalisman); //dark talisman
            flags.Add(StardewValley.Game1.player.hasMagicInk); //magic ink
            flags.Add(history.events.Contains("strange-note")); //bears knowledge
            flags.Add(StardewValley.Game1.player.getFriendshipHeartLevelForNPC("Jas") >= 8 && StardewValley.Game1.player.getFriendshipHeartLevelForNPC("Vincent") >= 8); //spring onion mastery
            flags.Add(StardewValley.Game1.player.HasTownKey); //town key
            flags.Add(history.events.Contains("meet-the-wizard")); //forest magic

            var ii = 0;
            List<string> keys = new List<string>(walletSprites.Keys);

            while(ii < walletSprites.Count)
            {
                var historyCheck = false;
                var tempIndexes = Enumerable.Range(0, history.events.Count)
                .Where(i => history.events[i] == keys[ii])
                .ToList();
                var tempIt = 0;
                var tempCheck = false;
                while (tempIt < tempIndexes.Count)
                {
                    if (history.categories[tempIndexes[tempIt]] == "wallet") tempCheck = true;
                    tempIt++;
                }
                if (!tempCheck) historyCheck = true;
                if (flags[ii] && historyCheck)
                {
                    outsideInfo.indexes[1] += 1;

                    eventData.id.Add(generateID());
                    eventData.runIndex.Add(outsideInfo.indexes[1]);
                    eventData.category.Add("wallet");
                    eventData.lastEvent.Add(keys[ii]);
                    eventData.categorySprite.Add(categorySprites["wallet"]);
                    eventData.itemSprite.Add(walletSprites[keys[ii]]);

                    var tempEvent = keys[ii].Split("-");
                    var finalEvent = "";
                    var jj = 0;
                    while (jj < tempEvent.Length)
                    {
                        if (jj + 1 == tempEvent.Length)
                        {
                            finalEvent += customToUpper(tempEvent[jj]);
                        }
                        else
                        {
                            finalEvent += customToUpper(tempEvent[jj]) + " ";
                        }
                        jj++;
                    }

                    eventData.description.Add(FormatDescription("wallet", finalEvent));
                    eventData.date.Add(date);

                    history.categories.Add("wallet");
                    history.events.Add(keys[ii]);

                    StardewValley.Game1.chatBox.addMessage("[SVDM]: Wallet " + finalEvent + " has been added to your collection! (10 Second Update Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                    Monitor.Log("Wallet " + finalEvent + " has been added to your collection! (10 Second Update Tick");

                    if(ii == 2)
                    {
                        outsideInfo.indexes[1] += 1;

                        eventData.id.Add(generateID());
                        eventData.runIndex.Add(outsideInfo.indexes[1]);
                        eventData.category.Add("quests");
                        eventData.lastEvent.Add("the-mysterious-qi");
                        eventData.categorySprite.Add(categorySprites["quests"]);
                        eventData.itemSprite.Add(questSprites["the-mysterious-qi"]);

                        finalEvent = "The Mysterious Qi";

                        eventData.description.Add(FormatDescription("quest", finalEvent));
                        eventData.date.Add(date);

                        history.categories.Add("quests");
                        history.events.Add(keys[ii]);

                        StardewValley.Game1.chatBox.addMessage("[SVDM]: Quest " + finalEvent + " has been added to your collection! (10 Second Update Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                        Monitor.Log("Quest " + finalEvent + " has been added to your collection! (10 Second Update Tick");
                    }
                }
                ii++;
            }
        }

        private void initFriendship()
        {
            List<string> keys = new List<string>(characterSprites.Keys);

            foreach(string key in keys)
            {
                friendshipLevel.Add(StardewValley.Game1.player.getFriendshipLevelForNPC(customToUpper(key)));
                friendshipHearts.Add(StardewValley.Game1.player.getFriendshipHeartLevelForNPC(customToUpper(key)));
            }
        }

        private void computeFriendship()
        {
            List<string> keys = new List<string>(characterSprites.Keys);

            var ii = 0;
            foreach (string key in keys)
            {
                var upperKey = customToUpper(key);
                if (friendshipLevel[ii] == 0 && StardewValley.Game1.player.getFriendshipLevelForNPC(upperKey) != 0 && upperKey != "Robin" && upperKey != "Lewis")
                {
                    //friendship-0 for key and update
                    outsideInfo.indexes[1] += 1;
                    var reverseEvent = reverseFormat(key);

                    eventData.id.Add(generateID());
                    eventData.runIndex.Add(outsideInfo.indexes[1]);
                    eventData.category.Add("friendship-0");
                    eventData.lastEvent.Add(reverseEvent);
                    eventData.categorySprite.Add(categorySprites["friendship"]);
                    eventData.itemSprite.Add(characterSprites[reverseEvent]);
                    eventData.description.Add(FormatDescription("friendship-0", upperKey));
                    eventData.date.Add(date);

                    history.categories.Add("friendship-0");
                    history.events.Add(reverseEvent);
                    friendshipLevel[ii] = StardewValley.Game1.player.getFriendshipLevelForNPC(upperKey);

                    StardewValley.Game1.chatBox.addMessage("[SVDM]: Friendship 0 " + upperKey + " has been achieved! (10 Second Update Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                    Monitor.Log("Friendship 0 " + upperKey + " has been achieved! (10 Second Update Tick)");

                }
                while(friendshipHearts[ii] != StardewValley.Game1.player.getFriendshipHeartLevelForNPC(upperKey))
                {
                    //add event for that level of hearts and update
                    outsideInfo.indexes[1] += 1;
                    var reverseEvent = reverseFormat(key);

                    eventData.id.Add(generateID());
                    eventData.runIndex.Add(outsideInfo.indexes[1]);
                    eventData.category.Add("friendship-" + (friendshipHearts[ii] + 1).ToString());
                    eventData.lastEvent.Add(reverseEvent);
                    eventData.categorySprite.Add(categorySprites["friendship"]);
                    eventData.itemSprite.Add(characterSprites[reverseEvent]);
                    eventData.description.Add(FormatDescription("friendship-" + (friendshipHearts[ii] + 1).ToString(), upperKey));
                    eventData.date.Add(date);

                    history.categories.Add("friendship-" + (friendshipHearts[ii] + 1).ToString());
                    history.events.Add(reverseEvent);
                    friendshipHearts[ii] ++;

                    StardewValley.Game1.chatBox.addMessage("[SVDM]: Friendship " + (friendshipHearts[ii]).ToString() + " " + upperKey + " has been achieved! (10 Second Update Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                    Monitor.Log("Friendship " + (friendshipHearts[ii]).ToString() + " " + upperKey + " has been achieved! (10 Second Update Tick)");
                }
                ii += 1;
            }

        }

        private void checkCachedRecipes()
        {
            if(cachedCooking.Count > 0)
            {
                foreach(var cook in cachedCooking)
                {
                    var reverseItem = reverseFormat(cook);
                    foreach (var val in StardewValley.Game1.player.cookingRecipes)
                    {
                        foreach (var val2 in val)
                        {
                            var tempIndex = history.events.IndexOf(reverseItem);
                            var historyCheck = false;
                            var tempIndexes = Enumerable.Range(0, history.events.Count)
                                .Where(i => history.events[i] == reverseItem)
                                .ToList();
                            var tempIt = 0;
                            var tempCheck = false;
                            while (tempIt < tempIndexes.Count)
                            {
                                if (history.categories[tempIndexes[tempIt]] == "cooking") tempCheck = true;
                                tempIt++;
                            }
                            if (!tempCheck) historyCheck = true;
                            if (val2.Key == cook && historyCheck)
                            {
                                outsideInfo.indexes[1] += 1;
                                var category = "cooking";

                                eventData.id.Add(generateID());
                                eventData.runIndex.Add(outsideInfo.indexes[1]);
                                eventData.category.Add(category);
                                eventData.categorySprite.Add(categorySprites[category]);
                                eventData.lastEvent.Add(reverseItem);
                                eventData.itemSprite.Add(cookingSprites[reverseItem]);
                                eventData.description.Add(FormatDescription(category, cook));
                                eventData.date.Add(date);
                                history.categories.Add(category);
                                history.events.Add(reverseItem);

                                StardewValley.Game1.chatBox.addMessage("[SVDM]: Dish " + cook + " has been added to your collection! (10 Second Update Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                                Monitor.Log("Dish " + cook + " has been added to your collection! (10 Second Update Tick)");
                            }
                        }
                    }
                }
                cachedCooking.Clear();
            }

            if(cachedCrafting.Count > 0)
            {
                foreach (var craft in cachedCrafting)
                {
                    var reverseItem = reverseFormat(craft);
                    foreach (var val in StardewValley.Game1.player.craftingRecipes)
                    {
                        foreach (var val2 in val)
                        {
                            var tempIndex = history.events.IndexOf(reverseItem);
                            var historyCheck = false;
                            var tempIndexes = Enumerable.Range(0, history.events.Count)
                                .Where(i => history.events[i] == reverseItem)
                                   .ToList();
                            var tempIt = 0;
                            var tempCheck = false;
                            while (tempIt < tempIndexes.Count)
                            {
                                if (history.categories[tempIndexes[tempIt]] == "crafting") tempCheck = true;
                                tempIt++;
                            }
                            if (!tempCheck) historyCheck = true;
                            if (val2.Key == craft && val2.Value > 0 && historyCheck)
                            {
                                outsideInfo.indexes[1] += 1;
                                var category = "crafting";

                                eventData.id.Add(generateID());
                                eventData.runIndex.Add(outsideInfo.indexes[1]);
                                eventData.category.Add(category);
                                eventData.categorySprite.Add(categorySprites[category]);
                                eventData.lastEvent.Add(reverseItem);
                                eventData.itemSprite.Add(craftingSprites[reverseItem]);
                                eventData.description.Add(FormatDescription(category, craft));
                                eventData.date.Add(date);
                                history.categories.Add(category);
                                history.events.Add(reverseItem);

                                StardewValley.Game1.chatBox.addMessage("[SVDM]: Crafting " + craft + " has been added to your collection! (10 Second Update Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                                Monitor.Log("Crafting " + craft + " has been added to your collection! (10 Second Update Tick)");
                            }
                        }
                    }
                }
                cachedCrafting.Clear();
            }
        }

        private void initBundles()
        {
            var bb = 0;
            var cc = 0;

            foreach (var val in StardewValley.Game1.netWorldState.Value.BundleData)
            {
                bundleData.items.Add(new List<string>());

                bundleData.location.Add(val.Key);
                bundleData.bundleString.Add(val.Value);

                var tempBundleString = val.Value.Split("/");
                bundleData.name.Add(tempBundleString[0]);

                var tempItems = tempBundleString[2].Split(" ");
                var ii = 0;

                while (ii < tempItems.Length)
                {
                    if (tempItems[0] != "-1")
                    {
                        StardewValley.ItemTypeDefinitions.ParsedItemData info = ItemRegistry.GetData(tempItems[ii]);
                        bundleData.items[bb].Add(reverseFormat(info.DisplayName));
                    }
                    else
                    {
                        bundleData.items[bb].Add("gold");
                    }
                    ii += 3;
                }
                bb++;
            }

            foreach (var val in StardewValley.Game1.netWorldState.Value.Bundles.Values)
            {
                bundleData.flags.Add(new List<bool>());
                foreach (var item in val)
                {
                    bundleData.flags[cc].Add(item);
                }
                cc++;
            }

            if (bundleData.flags[0].Count / 3 != bundleData.items[0].Count)
            {
                bundleData.flags.Reverse();
                reverseFlags = true;
            }
        }

        private string bundleCheck(string bundle)
        {
            string[] bundleGreen = { "spring-foraging-bundle", "spring-crops-bundle", "lake-fish-bundle", "wild-medicine-bundle", "childrens-bundle", "forest-bundle" };
            string[] bundleYellow = { "summer-foraging-bundle", "summer-crops-bundle", "fodder-bundle", "10000-bundle", "sticky-bundle", "treasure-hunters-bundle", "home-cooks-bundle" };
            string[] bundleOrange = { "fall-foraging-bundle", "fall-crops-bundle", "blacksmiths-bundle", "5000-bundle", "brewers-bundle", "foragers-bundle" };
            string[] bundleTeal = { "winter-foraging-bundle", "quality-crops-bundle", "river-fish-bundle", "dye-bundle", "rare-crops-bundle" };
            string[] bundleRed = { "construction-bundle", "animal-bundle", "specialty-fish-bundle", "chefs-bundle", "2500-bundle", "garden-bundle", "quality-fish-bundle", "master-fishers-bundle", "helpers-bundle", "winter-star-bundle" };
            string[] bundlePurple = { "exotic-foraging-bundle", "artisan-bundle", "night-fishing-bundle", "crab-pot-bundle", "geologists-bundle", "adventurers-bundle", "enchanters-bundle", "25000-bundle", "the-missing-bundle", "engineers-bundle", "spirits-eve-bundle" };
            string[] bundleBlue = { "ocean-fish-bundle", "field-research-bundle", "fish-farmers-bundle" };

            if (bundleGreen.Contains(bundle)) return categorySprites["bundle-green"];
            else if (bundleYellow.Contains(bundle)) return categorySprites["bundle-yellow"];
            else if (bundleOrange.Contains(bundle)) return categorySprites["bundle-orange"];
            else if (bundleTeal.Contains(bundle)) return categorySprites["bundle-teal"];
            else if (bundleRed.Contains(bundle)) return categorySprites["bundle-red"];
            else if (bundlePurple.Contains(bundle)) return categorySprites["bundle-purple"];
            else if (bundleBlue.Contains(bundle)) return categorySprites["bundle-blue"];
            else return "";
        }

        private void checkBundles()
        {
            while(bundleQueue.Count > 0)
            {

                var bb = 0;
                var cc = 0;
                var bundleIndex = 0;
                List<List<bool>> test = new List<List<bool>>();
                var checkFlag = false;
                var item = bundleQueue[0];
                var reverseItem = reverseFormat(item);

                var zz = 0;
                foreach (var val in StardewValley.Game1.netWorldState.Value.Bundles.Values)
                {
                    test.Add(new List<bool>());
                    foreach (var item2 in val)
                    {
                        test[zz].Add(item2);
                    }
                    zz++;
                }

                if (reverseFlags) test.Reverse();

                while (bb < test.Count && bb < bundleData.flags.Count && !checkFlag)
                {
                    while (cc < test[bb].Count && cc < bundleData.flags[bb].Count && !checkFlag)
                    {
                        if ((test[bb][cc] != bundleData.flags[bb][cc]) && bundleData.items[bb].Contains(reverseItem))
                        {
                            checkFlag = true;
                            bundleIndex = bb;
                            bundleData.flags[bb][cc] = test[bb][cc];
                        }
                        cc++;
                    }
                    cc = 0;
                    bb++;
                }

                if (checkFlag)
                {
                    outsideInfo.indexes[1] += 1;
                    var category = reverseFormat(bundleData.name[bundleIndex]).Replace("'", "") + "-bundle";

                    if(category == "construction-bundle" && reverseItem == "wood")
                    {
                        var historyCheck = false;
                        var tempIndex = history.events.IndexOf("wood-1");
                        if (tempIndex == -1 || history.categories[tempIndex] != category) historyCheck = true;

                        if (historyCheck)
                        {
                            item = "Wood 1";
                            reverseItem = "wood-1";
                        }
                        else
                        {
                            item = "Wood 2";
                            reverseItem = "wood-2";
                        }
                    }

                    eventData.id.Add(generateID());
                    eventData.runIndex.Add(outsideInfo.indexes[1]);
                    eventData.category.Add(category);
                    eventData.categorySprite.Add(bundleCheck(category));
                    eventData.lastEvent.Add(reverseItem);
                    eventData.itemSprite.Add(bundleSprites[reverseItem]);
                    eventData.description.Add(FormatDescription(category, item));
                    eventData.date.Add(date);
                    history.categories.Add(category);
                    history.events.Add(reverseItem);

                    StardewValley.Game1.chatBox.addMessage("[SVDM]: " + bundleData.name[bundleIndex] + " Bundle " + item + " has been added to your collection! (10 Second Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                    Monitor.Log(bundleData.name[bundleIndex] + " Bundle " + item + " has been added to your collection! (10 Second Tick)");
                }

                bundleQueue.Remove(item);
                if (bundleQueue.Count == 0) bundleData.flags = test;
            }

        }

        private void initPowers()
        {
            //string[] powers = {"farming-mastery", "mining-mastery", "foraging-mastery", "fishing-mastery", "combat-mastery" };

            var tempIndex = history.events.IndexOf("farming-mastery");
            if (tempIndex != -1) farmingMastery = true;

            var tempIndex2 = history.events.IndexOf("mining-mastery");
            if (tempIndex2 != -1) miningMastery = true;

            var tempIndex3 = history.events.IndexOf("foraging-mastery");
            if (tempIndex3 != -1) foragingMastery = true;

            var tempIndex4 = history.events.IndexOf("fishing-mastery");
            if (tempIndex4 != -1) fishingMastery = true;

            var tempIndex5 = history.events.IndexOf("combat-mastery");
            if (tempIndex5 != -1) combatMastery = true;
        }

        private void checkPowers()
        {
            List<string> recipes = new List<string>();
            var category = "powers";

            foreach(var craft in StardewValley.Game1.player.craftingRecipes)
            {
                foreach(var craft2 in craft)
                {
                    recipes.Add(craft2.Key);
                }
            }

            if(!farmingMastery && recipes.Contains("Statue of Blessings"))
            {
                outsideInfo.indexes[1] += 1;
                var item = "Farming Mastery";
                var reverseItem = "farming-mastery";

                eventData.id.Add(generateID());
                eventData.runIndex.Add(outsideInfo.indexes[1]);
                eventData.category.Add(category);
                eventData.categorySprite.Add(categorySprites[category]);
                eventData.lastEvent.Add(reverseItem);
                eventData.itemSprite.Add(powerSprites[reverseItem]);
                eventData.description.Add(FormatDescription(category, item));
                eventData.date.Add(date);
                history.categories.Add(category);
                history.events.Add(reverseItem);

                StardewValley.Game1.chatBox.addMessage("[SVDM]: Powers " + item + " has been added to your collection! (10 Second Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                Monitor.Log("Powers " + item + " has been added to your collection! (10 Second Tick)");

                farmingMastery = true;
            }
            else if(!miningMastery && recipes.Contains("Heavy Furnace"))
            {
                outsideInfo.indexes[1] += 1;
                var item = "Mining Mastery";
                var reverseItem = "mining-mastery";

                eventData.id.Add(generateID());
                eventData.runIndex.Add(outsideInfo.indexes[1]);
                eventData.category.Add(category);
                eventData.categorySprite.Add(categorySprites[category]);
                eventData.lastEvent.Add(reverseItem);
                eventData.itemSprite.Add(powerSprites[reverseItem]);
                eventData.description.Add(FormatDescription(category, item));
                eventData.date.Add(date);
                history.categories.Add(category);
                history.events.Add(reverseItem);

                StardewValley.Game1.chatBox.addMessage("[SVDM]: Powers " + item + " has been added to your collection! (10 Second Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                Monitor.Log("Powers " + item + " has been added to your collection! (10 Second Tick)");

                miningMastery = true;
            }
            else if(!foragingMastery && recipes.Contains("Mystic Tree Seed"))
            {
                outsideInfo.indexes[1] += 1;
                var item = "Foraging Mastery";
                var reverseItem = "foraging-mastery";

                eventData.id.Add(generateID());
                eventData.runIndex.Add(outsideInfo.indexes[1]);
                eventData.category.Add(category);
                eventData.categorySprite.Add(categorySprites[category]);
                eventData.lastEvent.Add(reverseItem);
                eventData.itemSprite.Add(powerSprites[reverseItem]);
                eventData.description.Add(FormatDescription(category, item));
                eventData.date.Add(date);
                history.categories.Add(category);
                history.events.Add(reverseItem);

                StardewValley.Game1.chatBox.addMessage("[SVDM]: Powers " + item + " has been added to your collection! (10 Second Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                Monitor.Log("Powers " + item + " has been added to your collection! (10 Second Tick)");

                foragingMastery = true;
            }
            else if(!fishingMastery && recipes.Contains("Challenge Bait"))
            {
                outsideInfo.indexes[1] += 1;
                var item = "Fishing Mastery";
                var reverseItem = "fishing-mastery";

                eventData.id.Add(generateID());
                eventData.runIndex.Add(outsideInfo.indexes[1]);
                eventData.category.Add(category);
                eventData.categorySprite.Add(categorySprites[category]);
                eventData.lastEvent.Add(reverseItem);
                eventData.itemSprite.Add(powerSprites[reverseItem]);
                eventData.description.Add(FormatDescription(category, item));
                eventData.date.Add(date);
                history.categories.Add(category);
                history.events.Add(reverseItem);

                StardewValley.Game1.chatBox.addMessage("[SVDM]: Powers " + item + " has been added to your collection! (10 Second Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                Monitor.Log("Powers " + item + " has been added to your collection! (10 Second Tick)");

                fishingMastery = true;
            }
            else if(!combatMastery && recipes.Contains("Anvil"))
            {
                outsideInfo.indexes[1] += 1;
                var item = "Combat Mastery";
                var reverseItem = "combat-mastery";

                eventData.id.Add(generateID());
                eventData.runIndex.Add(outsideInfo.indexes[1]);
                eventData.category.Add(category);
                eventData.categorySprite.Add(categorySprites[category]);
                eventData.lastEvent.Add(reverseItem);
                eventData.itemSprite.Add(powerSprites[reverseItem]);
                eventData.description.Add(FormatDescription(category, item));
                eventData.date.Add(date);
                history.categories.Add(category);
                history.events.Add(reverseItem);

                StardewValley.Game1.chatBox.addMessage("[SVDM]: Powers " + item + " has been added to your collection! (10 Second Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                Monitor.Log("Powers " + item + " has been added to your collection! (10 Second Tick)");

                combatMastery = true;
            }
        }

        private void initAchievements()
        {
            foreach (var val in StardewValley.Game1.player.achievements)
            {
                achievements.Add(val);
            }
        }

        private void checkAchievements()
        {
            foreach (var val in StardewValley.Game1.player.achievements)
            {
                if (!achievements.Contains(val))
                {
                    outsideInfo.indexes[1] += 1;

                    var tempAchs = Game1.achievements;
                    var reverseItem = reverseFormat(tempAchs[val].Split("^")[0]).Replace("(","").Replace(")","").Replace("'","");

                    if (reverseItem == "greenhorn-15k") reverseItem = "greenhorn";
                    else if (reverseItem == "cowpoke-50k") reverseItem = "cowpoke";
                    else if (reverseItem == "homesteader-250k") reverseItem = "homesteader";
                    else if (reverseItem == "millionaire-1mil") reverseItem = "millionaire";
                    else if (reverseItem == "legend-10mil") reverseItem = "legend-achievement";
                    else if (reverseItem == "d.I.Y.") reverseItem = "do-it-yourself";

                    var category = "achievement";
                    var item = "";

                    var tempItem = reverseItem.Split("-");
                    var ii = 0;
                    while (ii < tempItem.Length)
                    {
                        if (ii + 1 == tempItem.Length) item += customToUpper(tempItem[ii]);
                        else item += customToUpper(tempItem[ii]) + " ";
                        ii++;
                    }

                    eventData.id.Add(generateID());
                    eventData.runIndex.Add(outsideInfo.indexes[1]);
                    eventData.category.Add(category);
                    eventData.categorySprite.Add(categorySprites[category]);
                    eventData.lastEvent.Add(reverseItem);
                    eventData.itemSprite.Add(achievementSprites[reverseItem]);
                    eventData.description.Add(FormatDescription(category, item));
                    eventData.date.Add(date);
                    history.categories.Add(category);
                    history.events.Add(reverseItem);

                    StardewValley.Game1.chatBox.addMessage("[SVDM]: Achievement " + item + " has been added to your collection! (10 Second Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                    Monitor.Log("Achievement " + item + " has been added to your collection! (10 Second Tick)");

                    achievements.Add(val);

                    if(reverseItem == "a-distant-shore")
                    {
                        outsideInfo.indexes[1] += 1;
                        item = "Willy's Boat";
                        reverseItem = "willys-boat";
                        category = "milestone";

                        eventData.id.Add(generateID());
                        eventData.runIndex.Add(outsideInfo.indexes[1]);
                        eventData.category.Add(category);
                        eventData.categorySprite.Add(categorySprites[category]);
                        eventData.lastEvent.Add(reverseItem);
                        eventData.itemSprite.Add(miscSprites[reverseItem]);
                        eventData.description.Add(FormatDescription(category, item));
                        eventData.date.Add(date);
                        history.categories.Add(category);
                        history.events.Add(reverseItem);

                        StardewValley.Game1.chatBox.addMessage("[SVDM]: Milestones " + item + " has been added to your collection! (10 Second Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                        Monitor.Log("Milestones " + item + " has been added to your collection! (10 Second Tick)");
                    }
                }
            }
        }

        private void checkBagSize()
        {
            int newSize = StardewValley.Game1.player.MaxItems;
            if(newSize > bagSize && newSize == 24)
            {
                outsideInfo.indexes[1] += 1;
                var item = "Large Pack";
                var reverseItem = "large-pack";
                var category = "milestone";

                eventData.id.Add(generateID());
                eventData.runIndex.Add(outsideInfo.indexes[1]);
                eventData.category.Add(category);
                eventData.categorySprite.Add(categorySprites[category]);
                eventData.lastEvent.Add(reverseItem);
                eventData.itemSprite.Add(miscSprites[reverseItem]);
                eventData.description.Add(FormatDescription(category, item));
                eventData.date.Add(date);
                history.categories.Add(category);
                history.events.Add(reverseItem);

                StardewValley.Game1.chatBox.addMessage("[SVDM]: Milestones " + item + " has been added to your collection! (10 Second Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                Monitor.Log("Milestones " + item + " has been added to your collection! (10 Second Tick)");

                bagSize = newSize;
            }
            else if(newSize > bagSize && newSize == 36)
            {
                outsideInfo.indexes[1] += 1;
                var item = "Deluxe Pack";
                var reverseItem = "deluxe-pack";
                var category = "milestone";

                eventData.id.Add(generateID());
                eventData.runIndex.Add(outsideInfo.indexes[1]);
                eventData.category.Add(category);
                eventData.categorySprite.Add(categorySprites[category]);
                eventData.lastEvent.Add(reverseItem);
                eventData.itemSprite.Add(miscSprites[reverseItem]);
                eventData.description.Add(FormatDescription(category, item));
                eventData.date.Add(date);
                history.categories.Add(category);
                history.events.Add(reverseItem);

                StardewValley.Game1.chatBox.addMessage("[SVDM]: Milestones " + item + " has been added to your collection! (10 Second Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                Monitor.Log("Milestones " + item + " has been added to your collection! (10 Second Tick)");

                bagSize = newSize;
            }
        }

        private bool checkCrabPot(string fish)
        {
            string[] checkFish = {"Lobster","Clam","Crayfish","Crab","Cockle","Mussel","Shrimp","Snail","Periwinkle","Oyster"};

            if (!checkFish.Contains(fish))
            {
                return true;
            }else{
                Microsoft.Xna.Framework.Vector2 vector = StardewValley.Game1.currentCursorTile;
                bool check = StardewValley.Game1.player.currentLocation.isTileFishable((int)vector.X, (int)vector.Y);

                if (check) {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private string getManualEventSprite(string category, string item){
            if ((category == "shipped") && (shippedSprites.ContainsKey(item))) return shippedSprites[item];
            else if((category == "crafting") && (craftingSprites.ContainsKey(item))) return craftingSprites[item];
            else if((category == "artifacts") && (artifactSprites.ContainsKey(item))) return artifactSprites[item];
            else if((category == "minerals") && (mineralSprites.ContainsKey(item))) return mineralSprites[item];
            else if((category == "fish") && (fishSprites.ContainsKey(item))) return fishSprites[item];
            else if((category == "monster-slayer") && (monsterSprites.ContainsKey(item))) return monsterSprites[item];
            else if((category.Contains("friendship")) && (characterSprites.ContainsKey(item))) return  characterSprites[item];
            else if((category == "quest") && (questSprites.ContainsKey(item))) return questSprites[item];
            else if((category == "achievement") && (achievementSprites.ContainsKey(item))) return achievementSprites[item];
            else if((category == "wallet") && (walletSprites.ContainsKey(item))) return walletSprites[item];
            else if((category == "island-field-office") && (fieldOfficeSprites.ContainsKey(item))) return fieldOfficeSprites[item];
            else if((category == "cooking") && (cookingSprites.ContainsKey(item))) return cookingSprites[item];
            else if((category == "books") && (bookSprites.ContainsKey(item))) return bookSprites[item];
            else if((category == "powers") && (powerSprites.ContainsKey(item))) return powerSprites[item];
            else if((category == "milestone") && (miscSprites.ContainsKey(item))) return miscSprites[item];
            else if((category.Contains("level-up")) && (miscSprites.ContainsKey(item))) return miscSprites[item];
            else if((item.Contains("stardrop")) &&  (category == "milestone")) return miscSprites["stardrop"];
            else if((category.Contains("bundle")) && (bundleSprites.ContainsKey(item))) return bundleSprites[item];
            else return "invalid";
        }

        private void checkVaultBundles()
        {
            List<List<bool>> test = new List<List<bool>>();
            var zz = 0;
            foreach (var val in StardewValley.Game1.netWorldState.Value.Bundles.Values)
            {
                test.Add(new List<bool>());
                foreach (var item2 in val)
                {
                    test[zz].Add(item2);
                }
                zz++;
            }
            if (reverseFlags) test.Reverse();

            var result = new List<int>();

            if (!bundleData.flags[21].SequenceEqual(test[21]))
            {
                result.Add(21);
                bundleData.flags[21] = test[21];
            }
            else if (!bundleData.flags[22].SequenceEqual(test[22]))
            {
                result.Add(22);
                bundleData.flags[22] = test[22];
            }
            else if (!bundleData.flags[23].SequenceEqual(test[23]))
            {
                result.Add(23);
                bundleData.flags[23] = test[23];
            }
            else if (!bundleData.flags[24].SequenceEqual(test[24]))
            {
                result.Add(24);
                bundleData.flags[24] = test[24];
            }

            var ii = 0;
            while (ii < result.Count)
            {
                outsideInfo.indexes[1] += 1;
                var category = bundleData.name[result[ii]].Replace("'", "") + "-bundle";
                var item = "Gold";
                var reverseItem = "gold";

                eventData.id.Add(generateID());
                eventData.runIndex.Add(outsideInfo.indexes[1]);
                eventData.category.Add(category);
                eventData.categorySprite.Add(bundleCheck(category));
                eventData.lastEvent.Add(reverseItem);
                eventData.itemSprite.Add(bundleSprites[reverseItem]);
                eventData.description.Add(FormatDescription(category, item));
                eventData.date.Add(date);
                history.categories.Add(category);
                history.events.Add(reverseItem);

                StardewValley.Game1.chatBox.addMessage("[SVDM]: " + bundleData.name[result[ii]] + " Bundle " + item + " has been added to your collection! (10 Second Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                Monitor.Log(bundleData.name[result[ii]] + " Bundle " + item + " has been added to your collection! (10 Second Tick)");

                ii++;
            }
        }

        private void checkObelisks()
        {
            var buildings = new List<String>();
            foreach (var val in Game1.getFarm().buildings)
            {
                var building = reverseFormat(val.textureName().Split("\\")[1]);
                if (miscSprites.ContainsKey(building))
                {
                    var historyCheck = false;
                    var tempIndexes = Enumerable.Range(0, history.events.Count)
                    .Where(i => history.events[i] == building)
                    .ToList();
                    var tempIt = 0;
                    var tempCheck = false;
                    while (tempIt < tempIndexes.Count)
                    {
                        if (history.categories[tempIndexes[tempIt]] == "milestone") tempCheck = true;
                        tempIt++;
                    }
                    if (!tempCheck) historyCheck = true;
                    if (historyCheck) buildings.Add(val.textureName().Split("\\")[1]);
                }
            }

            var ii = 0;
            while (ii < buildings.Count)
            {
                outsideInfo.indexes[1] += 1;
                var reverseItem = reverseFormat(buildings[ii]);

                eventData.id.Add(generateID());
                eventData.runIndex.Add(outsideInfo.indexes[1]);
                eventData.category.Add("milestone");
                eventData.lastEvent.Add(reverseItem);
                eventData.categorySprite.Add(categorySprites["milestone"]);
                eventData.itemSprite.Add(miscSprites[reverseItem]);

                eventData.description.Add(FormatDescription("milestone", buildings[ii]));
                eventData.date.Add(date);

                history.categories.Add("milestone");
                history.events.Add(reverseItem);

                StardewValley.Game1.chatBox.addMessage("[SVDM]: Milestone " + buildings[ii] + " has been added to your collection! (10 Second Update Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                Monitor.Log("Milestone " + buildings[ii] + " has been added to your collection! (10 Second Update Tick");

                ii++;
            }
        }

        private void checkStardrops()
        {
            var stardrops = new List<string>();
            foreach(var val in Game1.player.mailReceived)
            {
                if (val.Contains("CF_")){
                    var stardrop = val.Replace("CF_", "Stardrop ");
                    var historyCheck = false;
                    var tempIndexes = Enumerable.Range(0, history.events.Count)
                    .Where(i => history.events[i] == reverseFormat(stardrop))
                    .ToList();
                    var tempIt = 0;
                    var tempCheck = false;
                    while (tempIt < tempIndexes.Count)
                    {
                        if (history.categories[tempIndexes[tempIt]] == "milestone") tempCheck = true;
                        tempIt++;
                    }
                    if (!tempCheck) historyCheck = true;
                    if (historyCheck) stardrops.Add(stardrop);
                }
            }

            var ii = 0;
            while (ii < stardrops.Count)
            {
                outsideInfo.indexes[1] += 1;
                var reverseItem = reverseFormat(stardrops[ii]);

                eventData.id.Add(generateID());
                eventData.runIndex.Add(outsideInfo.indexes[1]);
                eventData.category.Add("milestone");
                eventData.lastEvent.Add(reverseItem);
                eventData.categorySprite.Add(categorySprites["milestone"]);
                eventData.itemSprite.Add(miscSprites["stardrop"]);

                eventData.description.Add(FormatDescription("milestone", stardrops[ii]));
                eventData.date.Add(date);

                history.categories.Add("milestone");
                history.events.Add(reverseItem);

                StardewValley.Game1.chatBox.addMessage("[SVDM]: Milestone " + stardrops[ii] + " has been added to your collection! (10 Second Update Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                Monitor.Log("Milestone " + stardrops[ii] + " has been added to your collection! (10 Second Update Tick");

                ii++;
            }
        }

        private void checkMonsterSlayer()
        {
            var monsters = new List<string>();
            foreach(var val in Game1.player.mailReceived)
            {
                if (val.Contains("Gil_"))
                {
                    var monster = translateMonsters[val.Replace("Gil_", "")];
                    var historyCheck = false;
                    var tempIndexes = Enumerable.Range(0, history.events.Count)
                    .Where(i => history.events[i] == reverseFormat(monster))
                    .ToList();
                    var tempIt = 0;
                    var tempCheck = false;
                    while (tempIt < tempIndexes.Count)
                    {
                        if (history.categories[tempIndexes[tempIt]] == "monster-slayer") tempCheck = true;
                        tempIt++;
                    }
                    if (!tempCheck) historyCheck = true;
                    if (historyCheck) monsters.Add(monster);
                }
            }

            var ii = 0;
            while (ii < monsters.Count)
            {
                outsideInfo.indexes[1] += 1;
                var reverseItem = reverseFormat(monsters[ii]);

                eventData.id.Add(generateID());
                eventData.runIndex.Add(outsideInfo.indexes[1]);
                eventData.category.Add("monster-slayer");
                eventData.lastEvent.Add(reverseItem);
                eventData.categorySprite.Add(categorySprites["monster-slayer"]);
                eventData.itemSprite.Add(monsterSprites[reverseItem]);

                eventData.description.Add(FormatDescription("monster-slayer", monsters[ii]));
                eventData.date.Add(date);

                history.categories.Add("monster-slayer");
                history.events.Add(reverseItem);

                StardewValley.Game1.chatBox.addMessage("[SVDM]: Monster Slayer " + monsters[ii] + " has been added to your collection! (10 Second Update Tick)", new Microsoft.Xna.Framework.Color(255, 255, 255));
                Monitor.Log("Monster Slayer " + monsters[ii] + " has been added to your collection! (10 Second Update Tick");

                ii++;
            }
        }
    }
}
