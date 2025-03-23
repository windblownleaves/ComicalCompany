using HarmonyLib;
using System;
using UnityEngine;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class ItemNamePatch
    {
        [HarmonyPatch(typeof(GrabbableObject), "Start")]
        [HarmonyPostfix]
        public static void ItemStartPatch(GrabbableObject __instance)
        {
            if (!ComicalCompany.BoundConfig.enableItemRenaming.Value)
            {
                return;
            }

            switch (__instance.name)
            {
                case "Airhorn(Clone)":
                    __instance.itemProperties.itemName = "Loud Tube";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Loud Tube";
                    break;
                case "LungApparatus(Clone)":
                    __instance.itemProperties.itemName = "Battery";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Battery";
                    break;
                case "RedLocustHive(Clone)":
                    __instance.itemProperties.itemName = "Bee ball";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Bee ball";
                    break;
                case "BigBolt(Clone)":
                    __instance.itemProperties.itemName = "Small metal thing";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Small metal thing";
                    break;
                case "BinFullOfBottles(Clone)":
                    __instance.itemProperties.itemName = "Liquid holder holder";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Liquid holder holder";
                    break;
                case "HandBell(Clone)":
                    __instance.itemProperties.itemName = "Church";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Church";
                    break;
                case "Candy(Clone)":
                    __instance.itemProperties.itemName = "Free candy";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Free candy";
                    break;
                case "CashRegisterItem(Clone)":
                    __instance.itemProperties.itemName = "Typewriter";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Typewriter";
                    break;
                case "ChemicalJug(Clone)":
                    __instance.itemProperties.itemName = "3,4-dichloro-4-ethyl-5-methylheptane";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "3,4-dichloro-4-ethyl-5-methylheptane";
                    break;
                case "Clock(Clone)":
                    __instance.itemProperties.itemName = "Wheel of time";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Wheel of time";
                    break;
                case "Clownhorn(Clone)":
                    __instance.itemProperties.itemName = "Long loud tube";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Long loud tube";
                    break;
                case "Mug(Clone)":
                    __instance.itemProperties.itemName = "Office theft";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Office theft";
                    break;
                case "ComedyMask(Clone)":
                    __instance.itemProperties.itemName = "PVP mode";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "PVP mode";
                    break;
                case "ControlPad(Clone)":
                    __instance.itemProperties.itemName = "Keyboard";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Keyboard";
                    break;
                case "CookieMoldPan(Clone)":
                    __instance.itemProperties.itemName = "Dust pan";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Dust pan";
                    break;
                case "ShotgunItem(Clone)":
                    __instance.itemProperties.itemName = "Desert eagle";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Desert eagle";
                    break;
                case "Dustpan(Clone)":
                    __instance.itemProperties.itemName = "Cookie mold pan";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Cookie mold pan";
                    break;
                case "EasterEgg(Clone)":
                    __instance.itemProperties.itemName = "Celebratory spheroid";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Celebratory spheroid";
                    break;
                case "EggBeater(Clone)":
                    __instance.itemProperties.itemName = "I don't want to say";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "I don't want to say";
                    break;
                case "FancyLamp(Clone)":
                    __instance.itemProperties.itemName = "ÅRSTID";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "ÅRSTID";
                    break;
                case "Flask(Clone)":
                    __instance.itemProperties.itemName = "Christmas bulb";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Christmas bulb";
                    break;
                case "GarbageLid(Clone)":
                    __instance.itemProperties.itemName = "Hat";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Hat";
                    break;
                case "GiftBox(Clone)":
                    int i = UnityEngine.Random.Range(0, 2);
                    __instance.itemProperties.itemName = i == 0 ? "Loot box" : "Gambling addiction";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = i == 0 ? "Loot box" : "Gambling addiction";
                    break;
                case "GoldBar(Clone)":
                    __instance.itemProperties.itemName = "Brass bar";
                    __instance.scrapValue = __instance.scrapValue / 4;
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Brass bar";
                    break;
                case "FancyGlass(Clone)":
                    __instance.itemProperties.itemName = "Copper cup";
                    __instance.scrapValue = __instance.scrapValue / 3;
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Copper cup";
                    break;
                case "Hairbrush(Clone)":
                    __instance.itemProperties.itemName = "Ouch";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Ouch";
                    break;
                case "Hairdryer(Clone)":
                    __instance.itemProperties.itemName = "Wind gun";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Wind gun";
                    break;
                case "PickleJar(Clone)":
                    __instance.itemProperties.itemName = "TV show reference";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "TV show reference";
                    break;
                case "KnifeItem(Clone)":
                    __instance.itemProperties.itemName = "Murder";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Murder";
                    break;
                case "Cog(Clone)":
                    __instance.itemProperties.itemName = "Heavy metal thing";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Heavy metal thing";
                    break;
                case "Magic7Ball(Clone)":
                    __instance.itemProperties.itemName = "Regular 7 ball";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Regular 7 ball";
                    break;
                case "MagnifyingGlass(Clone)":
                    __instance.itemProperties.itemName = "Magnificent glass";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Magnificent glass";
                    break;
                case "OldPhone(Clone)":
                    __instance.itemProperties.itemName = "Walkie-talkie";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Walkie-talkie";
                    break;
                case "Painting(Clone)":
                    __instance.itemProperties.itemName = "Stained canvas";
                    __instance.scrapValue = __instance.scrapValue / 3;
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Stained canvas";
                    break;
                case "PerfumeBottle(Clone)":
                    __instance.itemProperties.itemName = "Weird soda";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Weird soda";
                    break;
                case "PillBottle(Clone)":
                    __instance.itemProperties.itemName = "Mystery gummies";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Mystery gummies";
                    break;
                case "PlasticCup(Clone)":
                    __instance.itemProperties.itemName = "Small bucket";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Small bucket";
                    break;
                case "FishTestProp(Clone)":
                    __instance.itemProperties.itemName = "Fish action figure";
                    __instance.scrapValue = __instance.scrapValue * 2;
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Fish action figure";
                    break;
                case "RedSodaCan(Clone)":
                    __instance.itemProperties.itemName = "Off-brand Dr Pepper";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Off-brand Dr Pepper";
                    break;
                case "Remote(Clone)":
                    __instance.itemProperties.itemName = "Telephone";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Telephone";
                    break;
                case "FancyRing(Clone)":
                    __instance.itemProperties.itemName = "Regret";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Regret";
                    break;
                case "RobotToy(Clone)":
                    __instance.itemProperties.itemName = "Old bird action figure";
                    __instance.scrapValue = __instance.scrapValue * 2;
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Old bird action figure";
                    break;
                case "RubberDucky(Clone)":
                    __instance.itemProperties.itemName = "Duck action figure";
                    __instance.scrapValue = __instance.scrapValue * 2;
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Duck action figure";
                    break;
                case "ShotgunShell(Clone)":
                    __instance.itemProperties.itemName = ".50 cal AE 12.7x33mmRB";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = ".50 cal AE 12.7x33mmRB";
                    break;
                case "SoccerBall(Clone)":
                    __instance.itemProperties.itemName = "Basketball";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Basketball";
                    break;
                case "SteeringWheel(Clone)":
                    __instance.itemProperties.itemName = "Fidget spinner";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Fidget spinner";
                    break;
                case "StopSign(Clone)":
                    __instance.itemProperties.itemName = "Government property";
                    __instance.scrapValue = __instance.scrapValue * -1;
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Government property";
                    break;
                case "MetalSheet(Clone)":
                    __instance.itemProperties.itemName = "Music genre";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Music genre";
                    break;
                case "TeaKettle(Clone)":
                    __instance.itemProperties.itemName = "Anglo-saxon phase transition device";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Anglo-saxon phase transition device";
                    break;
                case "Dentures(Clone)":
                    __instance.itemProperties.itemName = "T̵HA̶T̵ ̸̋W̵̒̋H̴̉I̵C̴H̶ ̴C̵O̵N̸S̷̈U̷M̶E̸Š̶";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "T̵HA̶T̵ ̸̋W̵̒̋H̴̉I̵C̴H̶ ̴C̵O̵N̸S̷̈U̷M̶E̸Š̶";
                    break;
                case "ToiletPaperRolls(Clone)":
                    __instance.itemProperties.itemName = "Pandemic currency";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Pandemic currency";
                    break;
                case "Toothpaste(Clone)":
                    __instance.itemProperties.itemName = "T̵HA̶T̵ ̸̋W̵̒̋H̴̉I̵C̴H̶ ̴C̵O̵N̸S̷̈U̷M̶E̸Š̶ paste";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "T̵HA̶T̵ ̸̋W̵̒̋H̴̉I̵C̴H̶ ̴C̵O̵N̸S̷̈U̷M̶E̸Š̶ paste";
                    break;
                case "ToyCube(Clone)":
                    __instance.itemProperties.itemName = "(R'U'R)y'x'(RU')(R'F)(RUR')(RUR'U')R'FRUR'U'F'";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "(R'U'R)y'x'(RU')(R'F)(RUR')(RUR'U')R'FRUR'U'F'";
                    break;
                case "ToyTrain(Clone)":
                    __instance.itemProperties.itemName = "Autism contraption";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Autism Contraption";
                    break;
                case "TragedyMask(Clone)":
                    __instance.itemProperties.itemName = "You know you want to";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "You know you want to";
                    break;
                case "EnginePart(Clone)":
                    __instance.itemProperties.itemName = "2.39 MW (3,200 hp) EMD 12-710";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "2.39 MW (3,200 hp) EMD 12-710";
                    break;
                case "WhoopieCushion(Clone)":
                    __instance.itemProperties.itemName = "Weird gold bar";
                    __instance.scrapValue = __instance.scrapValue * 8;
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Weird gold bar";
                    break;
                case "YieldSign(Clone)":
                    __instance.itemProperties.itemName = "Government property";
                    __instance.scrapValue = __instance.scrapValue * -1;
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Government property";
                    break;
                case "ZeddogPlushie(Clone)":
                    __instance.itemProperties.itemName = "Dog action figure";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Dog action figure";
                    break;
                case "Boombox(Clone)":
                    __instance.itemProperties.itemName = "Bombox";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Bombox";
                    break;
                case "BBFlashlight(Clone)":
                    __instance.itemProperties.itemName = "Flashlghti";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Flashlghti";
                    break;
                case "CaveDwellerEnemy(Clone)":
                    int j = UnityEngine.Random.Range(0, 2);
                    __instance.itemProperties.itemName = j == 0 ? "Your son, Jeremy" : "Your daughter, Megan";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = j == 0 ? "Your son, Jeremy" : "Your daughter, Megan";
                    break;
                case "DiyFlashbang(Clone)":
                    __instance.itemProperties.itemName = "Vodka";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Vodka";
                    break;
                case "ExtensionLadder(Clone)":
                    __instance.itemProperties.itemName = "Extnesion ladder";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Vodka";
                    break;
                case "FlashlightItem(Clone)":
                    __instance.itemProperties.itemName = "Pro-flasglight";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Vodka";
                    break;
                case "JetpackItem(Clone)":
                    __instance.itemProperties.itemName = "Jtepacl";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Jtepacl";
                    break;
                case "Key(Clone)":
                    __instance.itemProperties.itemName = "Hey";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Hey";
                    break;
                case "LaserPointer(Clone)":
                    __instance.itemProperties.itemName = "Laser gun";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Laser gun";
                    break;
                case "MappingDevice(Clone)":
                    __instance.itemProperties.itemName = "Maper";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Maper";
                    break;
                case "PatcherGunItem(Clone)":
                    __instance.itemProperties.itemName = "Zap ugn";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Maper";
                    break;
                case "RadarBooster(Clone)":
                    __instance.itemProperties.itemName = "Radar bboster";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Radar bboster";
                    break;
                case "ShovelItem(Clone)":
                    __instance.itemProperties.itemName = "Shover";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Shover";
                    break;
                case "StunGrenade(Clone)":
                    __instance.itemProperties.itemName = "Stujn grenad";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Stujn grenad";
                    break;
                case "TZPChemical(Clone)":
                    __instance.itemProperties.itemName = "Skooma";
                    __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Skooma";
                    break;
            }
        }
    }
}
