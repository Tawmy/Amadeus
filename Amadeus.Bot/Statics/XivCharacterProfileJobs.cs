using System;

namespace Amadeus.Bot.Statics
{
    public static class XivCharacterProfileJobs
    {
        private static readonly int JobsLine1 = 670;
        private static readonly int JobsLine2 = 748;
        private static readonly int JobsLine3 = 826;

        private static readonly (int, int) Pld = (550, JobsLine1);
        private static readonly (int, int) War = (602, JobsLine1);
        private static readonly (int, int) Drk = (654, JobsLine1);
        private static readonly (int, int) Gnb = (706, JobsLine1);

        private static readonly (int, int) Whm = (786, JobsLine1);
        private static readonly (int, int) Sch = (838, JobsLine1);
        private static readonly (int, int) Ast = (890, JobsLine1);

        private static readonly (int, int) Mnk = (550, JobsLine2);
        private static readonly (int, int) Drg = (602, JobsLine2);
        private static readonly (int, int) Nin = (654, JobsLine2);
        private static readonly (int, int) Sam = (706, JobsLine2);

        private static readonly (int, int) Brd = (786, JobsLine2);
        private static readonly (int, int) Mch = (838, JobsLine2);
        private static readonly (int, int) Dnc = (890, JobsLine2);

        private static readonly (int, int) Blm = (972, JobsLine2);
        private static readonly (int, int) Smn = (1024, JobsLine2);
        private static readonly (int, int) Rdm = (1076, JobsLine2);
        private static readonly (int, int) Blu = (1128, JobsLine2);

        private static readonly (int, int) Crp = (550, JobsLine3);
        private static readonly (int, int) Bsm = (602, JobsLine3);
        private static readonly (int, int) Arm = (654, JobsLine3);
        private static readonly (int, int) Gsm = (706, JobsLine3);
        private static readonly (int, int) Ltw = (758, JobsLine3);
        private static readonly (int, int) Wvr = (810, JobsLine3);
        private static readonly (int, int) Alc = (862, JobsLine3);
        private static readonly (int, int) Cul = (914, JobsLine3);

        private static readonly (int, int) Min = (994, JobsLine3);
        private static readonly (int, int) Btn = (1046, JobsLine3);
        private static readonly (int, int) Fsh = (1098, JobsLine3);

        public static (int, int) GetJobCoordinates(string job)
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
    }
}