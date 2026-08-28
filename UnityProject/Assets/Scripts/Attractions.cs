using System.Collections.Generic;

namespace WiesnKrisn
{
    public enum Attractions
    {
        RollerCoaster,
        GhostTrain,
        FerrisWheel,
        CandyBar,
        Karussell,
        Autoscooter,
        Dosenwerfen,
        Greifautomat,
    }

    public class AttractionNames
    {
        public static readonly Dictionary<Attractions, string> Names = new Dictionary<Attractions, string>
        {
            { Attractions.RollerCoaster , "Roller Coaster" },
            { Attractions.GhostTrain , "Ghost Ride" },
            { Attractions.FerrisWheel , "Ferris Wheel" },
            { Attractions.CandyBar , "Candy Shop" },
            { Attractions.Karussell , "Carousel" },
            { Attractions.Autoscooter , "Autoscooter" },
            { Attractions.Dosenwerfen , "Can Knockdown" },
            { Attractions.Greifautomat , "Plushie-Grapple" },
        };
    }
}