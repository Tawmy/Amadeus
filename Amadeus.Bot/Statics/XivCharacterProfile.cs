using System;

namespace Amadeus.Bot.Statics
{
    public static class XivCharacterProfile
    {
        private const int YOffset = 12;

        private const int JobsLine1 = 670;
        private const int JobsLine2 = 748;
        private const int JobsLine3 = 826;

        public const int TextSizeName = 48;
        public const int TextSizeTitle = 28;
        public const int TextSizeJobs = 26;

        public static readonly Coordinates NameTitleNone = new(830, 88 + YOffset);
        public static readonly Coordinates NameTitleTop = new(830, 105 + YOffset);
        public static readonly Coordinates NameTitleBottom = new(830, 70 + YOffset);
        public static readonly Coordinates TitleTitleTop = new(830, 60 + YOffset);
        public static readonly Coordinates TitleTitleBottom = new(830, 115 + YOffset);

        public static readonly Coordinates Pld = new(550, JobsLine1);
        public static readonly Coordinates War = new(602, JobsLine1);
        public static readonly Coordinates Drk = new(654, JobsLine1);
        public static readonly Coordinates Gnb = new(706, JobsLine1);

        public static readonly Coordinates Whm = new(786, JobsLine1);
        public static readonly Coordinates Sch = new(838, JobsLine1);
        public static readonly Coordinates Ast = new(890, JobsLine1);

        public static readonly Coordinates Mnk = new(550, JobsLine2);
        public static readonly Coordinates Drg = new(602, JobsLine2);
        public static readonly Coordinates Nin = new(654, JobsLine2);
        public static readonly Coordinates Sam = new(706, JobsLine2);

        public static readonly Coordinates Brd = new(786, JobsLine2);
        public static readonly Coordinates Mch = new(838, JobsLine2);
        public static readonly Coordinates Dnc = new(890, JobsLine2);

        public static readonly Coordinates Blm = new(972, JobsLine2);
        public static readonly Coordinates Smn = new(1024, JobsLine2);
        public static readonly Coordinates Rdm = new(1076, JobsLine2);
        public static readonly Coordinates Blu = new(1128, JobsLine2);

        public static readonly Coordinates Crp = new(550, JobsLine3);
        public static readonly Coordinates Bsm = new(602, JobsLine3);
        public static readonly Coordinates Arm = new(654, JobsLine3);
        public static readonly Coordinates Gsm = new(706, JobsLine3);
        public static readonly Coordinates Ltw = new(758, JobsLine3);
        public static readonly Coordinates Wvr = new(810, JobsLine3);
        public static readonly Coordinates Alc = new(862, JobsLine3);
        public static readonly Coordinates Cul = new(914, JobsLine3);

        public static readonly Coordinates Min = new(994, JobsLine3);
        public static readonly Coordinates Btn = new(1046, JobsLine3);
        public static readonly Coordinates Fsh = new(1098, JobsLine3);

        public static Coordinates GetJobCoordinates(string job)
        {
            return job.ToLower() switch
            {
                "pld" => Pld,
                "war" => War,
                "drk" => Drk,
                "gnb" => Gnb,
                "whm" => Whm,
                "sch" => Sch,
                "ast" => Ast,
                "mnk" => Mnk,
                "drg" => Drg,
                "nin" => Nin,
                "sam" => Sam,
                "brd" => Brd,
                "mch" => Mch,
                "dnc" => Dnc,
                "blm" => Blm,
                "smn" => Smn,
                "rdm" => Rdm,
                "blu" => Blu,
                "crp" => Crp,
                "bsm" => Bsm,
                "arm" => Arm,
                "gsm" => Gsm,
                "ltw" => Ltw,
                "wvr" => Wvr,
                "alc" => Alc,
                "cul" => Cul,
                "min" => Min,
                "btn" => Btn,
                "fsh" => Fsh,
                _ => throw new ArgumentException("Job not found")
            };
        }

        public class Coordinates
        {
            public readonly int X;
            public readonly int Y;

            public Coordinates(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}