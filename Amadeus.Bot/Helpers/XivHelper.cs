using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amadeus.Bot.Statics;
using SkiaSharp;
using xivapi_cs.Models;
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

        public static async Task<SKImage> GetFreeCompanyIcon(FreeCompany fc)
        {
            using var client = new WebClient();
            var bytearrays = new List<byte[]>();
            foreach (var c in fc.Crest) bytearrays.Add(await client.DownloadDataTaskAsync(c));

            if (bytearrays.Count <= 0) return null;
            var firstLayer = SKBitmap.Decode(bytearrays.First());

            using var canvas = new SKCanvas(firstLayer);
            foreach (var bytearray in bytearrays.Skip(1))
                canvas.DrawBitmap(SKBitmap.Decode(bytearray), SKRect.Create(0, 0, firstLayer.Width, firstLayer.Height));

            var (x, y) = XivCharacterProfile.LogoTopDimensions;
            firstLayer = firstLayer.Resize(new SKSizeI(x, y), SKFilterQuality.High);
            return SKImage.FromBitmap(firstLayer);
        }

        public static SKImage GetGrandCompanyIcon(GrandCompanyExtended g)
        {
            var gcIcon = g?.Company?.ID switch
            {
                1 => "GcM.png",
                2 => "GcI.png",
                3 => "GcI.png",
                _ => throw new ArgumentException("GrandCompanyId not found")
            };

            var stream = ResourceHelper.GetResource(gcIcon);
            var (x, y) = XivCharacterProfile.LogoBottomDimensions;
            using var bitmap = SKBitmap.Decode(stream, new SKImageInfo(x, y));
            return SKImage.FromBitmap(bitmap);
        }
    }
}