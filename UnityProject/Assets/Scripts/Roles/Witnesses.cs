using System.Collections.Generic;

namespace WiesnKrisn.Roles
{
    public enum Witnesses
    {
        SaufiGroup,
        SaufiGroup2,
        AperoliGroup,
        AperoliGroup2,
        AutoscooterKid,
        KarussellParents,
        KarussellKid,
        Influenci,
        SuessigkeitenFan,
        Achterbahni,
        Geisterbahni,
        DosiWerfi,
        GreifiTypi,
    }

    public class WitnessNames
    {
        public static readonly Dictionary<Witnesses, string> Names = new Dictionary<Witnesses, string>()
        {
            { Witnesses.SaufiGroup , "Goley"},
            { Witnesses.SaufiGroup2 , "Leon"},
            { Witnesses.AperoliGroup , "Penelope"},
            { Witnesses.AperoliGroup2 , "Kira"},
            { Witnesses.AutoscooterKid , "Nova"},
            { Witnesses.KarussellParents , "Timmys Parents"},
            { Witnesses.KarussellKid , "Timmy"},
            { Witnesses.Influenci , "Miss Cascade"},
            { Witnesses.SuessigkeitenFan , "Josy"},
            { Witnesses.Achterbahni , "Rory"},
            { Witnesses.Geisterbahni , "Cryss Angel"},
            { Witnesses.DosiWerfi , "Dr. Hopstone"},
            { Witnesses.GreifiTypi , "Jesse"},
        };

        public static readonly Dictionary<Suspects, string> SuspectNames = new Dictionary<Suspects, string>()
        {
            { Suspects.Saufi1 , "Goley"},
            { Suspects.Saufi2 , "Leon"},
            { Suspects.Aperoli1 , "Penelope"},
            { Suspects.Aperoli2 , "Kira"},
            { Suspects.Infoluenci , "Miss Cascade"},
            { Suspects.Suessigkeiti , "Josy"},
            { Suspects.Achterbahni , "Rory"},
            { Suspects.Geisterbahni , "Cryss Angel"},
            { Suspects.GreifiTypi , "Jesse"},
            { Suspects.DosiWerfi , "Dr. Hopstone"},
        };
    }
}