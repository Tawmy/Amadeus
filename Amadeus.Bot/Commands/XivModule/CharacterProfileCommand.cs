using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amadeus.Bot.Helpers;
using Amadeus.Bot.Statics;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using SkiaSharp;
using xivapi_cs;
using xivapi_cs.Models.CharacterProfile;

namespace Amadeus.Bot.Commands.XivModule
{
    public static class CharacterProfileCommand
    {
        public static async Task Run(CommandContext ctx, string firstName, string lastName, string server)
        {
            var fullName = $"{firstName} {lastName}";
            var client = new XivApiClient();
            var searchResults = await client.CharacterSearch(fullName, server);
            if (searchResults?.Results == null || searchResults.Results.Length == 0)
            {
                await ctx.RespondAsync("Character not found");
                return;
            }

            await GetAndPostCharacterSheet(ctx, searchResults.Results.First().ID, client);
        }

        public static async Task Run(CommandContext ctx, string id)
        {
            await GetAndPostCharacterSheet(ctx, Convert.ToInt32(id), null);
        }

        private static async Task GetAndPostCharacterSheet(CommandContext ctx, int id, XivApiClient c)
        {
            c ??= new XivApiClient();

            var character = await c.CharacterProfileExtended(id);

            var opensans = await GetFont("OpenSans-Regular");
            var vollkorn = await GetFont("Vollkorn-Regular");

            var info = new SKImageInfo(1200, 873);
            await using (var stream = ResourceHelper.GetResource("XivCharacterTemplate.png"))
            using (var bitmap = SKBitmap.Decode(stream, info))
            {
                var canvas = new SKCanvas(bitmap);
                await AddCharacterPortrait(canvas, character.Character);
                await AddCharacterFrame(canvas, character.Character);
                AddActiveJobIcon(canvas, character.Character);
                AddCharacterName(canvas, vollkorn, character.Character);
                AddJobLevels(canvas, opensans, character.Character);
                canvas.DrawBitmap(bitmap, 0, 0);
                var imgStream = SKImage.FromPixels(bitmap.PeekPixels()).Encode().AsStream();
                var msg = new DiscordMessageBuilder();
                msg.WithFile($"{character.Character.Name}.png", imgStream);
                await ctx.RespondAsync(msg);
            }
        }

        private static async Task<SKTypeface> GetFont(string name)
        {
            await using var stream = ResourceHelper.GetResource($"{name}.ttf");
            return SKTypeface.FromStream(stream);
        }

        private static async Task AddCharacterPortrait(SKCanvas c, CharacterBase ch)
        {
            // get portrait
            var client = new WebClient();
            var portraitArray = await client.DownloadDataTaskAsync(ch.Portrait);
            var portrait = SKBitmap.Decode(new MemoryStream(portraitArray));

            // resize portrait
            portrait = portrait.Resize(new SKSizeI(564, 769), SKFilterQuality.High);

            // crop portrait
            var image = SKImage.FromBitmap(portrait);
            var portraitCropped = image.Subset(SKRectI.Create(60, 0, image.Width - 60 * 2, image.Height));

            // draw portrait on canvas
            c.DrawImage(portraitCropped, 26, 68);
        }

        private static async Task AddCharacterFrame(SKCanvas c, CharacterExtended ch)
        {
            await using var stream = ResourceHelper.GetResource("XivCharacterFrame.png");
            var b2 = SKBitmap.Decode(stream);
            c.DrawBitmap(b2, 18, 22);
        }

        private static void AddActiveJobIcon(SKCanvas c, CharacterExtended ch)
        {
            var abbr = ch.ActiveClassJob.Job?.Abbreviation ?? ch.ActiveClassJob.Class?.Abbreviation;
            if (abbr == null) return;

            var icon = ResourceHelper.GetResource($"Jobs.{abbr.ToUpper()}.png");
            var b = SKBitmap.Decode(icon);
            c.DrawBitmap(b, 225, 39);
        }

        private static void AddCharacterName(SKCanvas c, SKTypeface t, CharacterExtended ch)
        {
            var paintName = new SKPaint
            {
                IsAntialias = true,
                TextSize = XivCharacterProfile.TextSizeName,
                Typeface = t
            };

            var coorName = XivCharacterProfile.NameTitleNone;

            if (!string.IsNullOrWhiteSpace(ch.Title?.Name))
            {
                // Character has title

                var paintTitle = new SKPaint
                {
                    IsAntialias = paintName.IsAntialias,
                    Typeface = paintName.Typeface,
                    TextSize = XivCharacterProfile.TextSizeTitle
                };

                var coorTitle = ch.TitleTop ? XivCharacterProfile.TitleTitleTop : XivCharacterProfile.TitleTitleBottom;
                coorName = ch.TitleTop ? XivCharacterProfile.NameTitleTop : XivCharacterProfile.NameTitleBottom;

                // draw title if character has one, coordinates for name also changed
                DrawTextCentered(ch.Title.Name, coorTitle, c, paintTitle);
            }

            // always draw name
            DrawTextCentered(ch.Name, coorName, c, paintName);
        }

        private static void AddJobLevels(SKCanvas c, SKTypeface t, CharacterExtended ch)
        {
            var paint = new SKPaint
            {
                IsAntialias = true,
                TextSize = XivCharacterProfile.TextSizeJobs,
                Typeface = t,
                Color = SKColors.White
            };

            foreach (var job in ch.ClassJobs)
            {
                if (job.Level <= 0) continue;
                DrawTextCentered(job.Level.ToString(), XivCharacterProfile.GetJobCoordinates(job.Job.Abbreviation), c,
                    paint);
            }
        }

        private static void DrawTextCentered(string text, XivCharacterProfile.Coordinates coor, SKCanvas c, SKPaint p)
        {
            DrawTextCentered(text, coor.X, coor.Y, c, p);
        }

        private static void DrawTextCentered(string text, int x, int y, SKCanvas c, SKPaint p)
        {
            c.DrawText(text, x - p.MeasureText(text) / 2, y, p);
        }
    }
}