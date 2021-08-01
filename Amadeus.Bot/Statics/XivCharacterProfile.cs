namespace Amadeus.Bot.Statics
{
    public static class XivCharacterProfile
    {
        private static readonly int YOffset = 12;

        public static readonly int TextSizeName = 48;
        public static readonly int TextSizeTitle = 28;
        public static readonly int TextSizeJobs = 26;
        public static readonly int TextSizeItemLevel = 34;
        public static readonly int TextSizeServer = 28;

        public static readonly (int, int) TotalDimensions = (1200, 873);

        public static readonly (int, int) NameTitleNone = (830, 88 + YOffset);
        public static readonly (int, int) NameTitleTop = (830, 105 + YOffset);
        public static readonly (int, int) NameTitleBottom = (830, 70 + YOffset);
        public static readonly (int, int) TitleTitleTop = (830, 60 + YOffset);
        public static readonly (int, int) TitleTitleBottom = (830, 115 + YOffset);

        public static readonly (int, int) PortraitDimensions = (564, 769);
        public static readonly int PortraitCropX = 60;
        public static readonly (int, int) PortraitLocation = (26, 68);
        public static readonly (int, int) CharacterFrameLocation = (18, 22);
        public static readonly (int, int) JobIconLocation = (225, 39);

        public static readonly (int, int) ItemLevel = (585, 356);
        public static readonly (int, int) Server = (1100, 340);
    }
}