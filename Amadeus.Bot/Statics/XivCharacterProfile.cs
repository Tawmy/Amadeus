namespace Amadeus.Bot.Statics
{
    public static class XivCharacterProfile
    {
        public static readonly int TextSizeName = 48;
        public static readonly int TextSizeTitle = 28;
        public static readonly int TextSizeJobs = 26;
        public static readonly int TextSizeItemLevel = 34;
        public static readonly int TextSizeServer = 28;
        public static readonly int TextTopSize = 28;

        public static readonly (int, int) TotalDimensions = (1200, 873);

        public static readonly (int, int) NameTitleNone = (838, 104);
        public static readonly (int, int) NameTitleTop = (838, 117);
        public static readonly (int, int) NameTitleBottom = (838, 82);
        public static readonly (int, int) TitleTitleTop = (838, 72);
        public static readonly (int, int) TitleTitleBottom = (838, 127);

        public static readonly (int, int) PortraitDimensions = (564, 769);
        public static readonly int PortraitCropX = 60;
        public static readonly (int, int) PortraitLocation = (26, 68);
        public static readonly (int, int) CharacterFrameLocation = (18, 22);
        public static readonly (int, int) JobIconLocation = (225, 39);

        public static readonly (int, int) TextTop = (838, 238);
        public static readonly (int, int) LogoTopDimensions = (52, 52);
        public static readonly (int, int) LogoBottomDimensions = (50, 50);
        public static readonly (int, int) LogoTopOffset = (70, 34);

        public static readonly (int, int) LogoTop = (TextTop.Item1 - LogoTopOffset.Item1,
            TextTop.Item2 - LogoTopOffset.Item2); // subtract half of text width of x axis

        public static readonly (int, int) LogoBottom = (518, 306);
        public static readonly (int, int) ItemLevel = (585, 356);
        public static readonly (int, int) Server = (1100, 340);
    }
}