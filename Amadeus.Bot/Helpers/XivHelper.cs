using System;
using System.Linq;
using xivapi_cs.Models.CharacterProfile;

namespace Amadeus.Bot.Helpers
{
    public static class XivHelper
    {
        public static int CalcItemLevel(CharacterExtended c)
        {
            var g = c.GearSet.Gear;

            var iLvlTotal = g.GetType().GetProperties().Where(x =>
                    !new[] {"SoulCrystal", "OffHand"}.Contains(x.Name) &&
                    x.GetValue(g) != null)
                .Sum(x => ((GearPieceExtended) x.GetValue(g)).Item.LevelItem);

            iLvlTotal += g.OffHand?.Item?.LevelItem ?? g.MainHand.Item.LevelItem;

            return Convert.ToInt32(Math.Floor(decimal.Divide(iLvlTotal, 13)));
        }
    }
}