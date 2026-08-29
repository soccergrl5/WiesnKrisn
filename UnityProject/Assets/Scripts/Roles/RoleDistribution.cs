using System;
using System.Collections.Generic;
using UnityEngine;
using WiesnKrisn.Roles.Traits;
using Random = System.Random;

namespace WiesnKrisn.Roles
{
    public class RoleDistribution
    {
        public static RoleDistribution Instance = new RoleDistribution();
        private RoleDistribution(){}

        private const int SuspectAmount = 10;
        
        private readonly Dictionary<Suspects, Trait> _suspectDescriptions = new Dictionary<Suspects, Trait>()
        {
            { Suspects.Saufi1, new Trait(Clothes.Casual, HairColor.Dark, ClotheColor.Green, Shoes.Sneaker, Hairstyle.Kurz, EyeColor.Brown, Headpiece.Cap, Rose.Nothing, Lebkuchenherz.Nothing, Glasses.Sun)},
            { Suspects.Saufi2, new Trait(Clothes.Lederhosn, HairColor.Colorful, ClotheColor.Red, Shoes.Haferl, Hairstyle.Open, EyeColor.Brown, Headpiece.Hendl, Rose.Yellow, Lebkuchenherz.Small, Glasses.Nothing)},
            { Suspects.Aperoli1, new Trait(Clothes.Dirndl, HairColor.Dark, ClotheColor.Red, Shoes.Sneaker, Hairstyle.Zopf, EyeColor.Green, Headpiece.Nothing, Rose.Red, Lebkuchenherz.Big, Glasses.Nothing)},
            { Suspects.Aperoli2, new Trait(Clothes.Dirndl, HairColor.Colorful, ClotheColor.Blue, Shoes.Heels, Hairstyle.Open, EyeColor.Green, Headpiece.Hendl, Rose.Yellow, Lebkuchenherz.Nothing, Glasses.Normal)},
            { Suspects.Infoluenci, new Trait(Clothes.Dirndl, HairColor.Light, ClotheColor.Red, Shoes.Heels, Hairstyle.Zopf, EyeColor.Brown, Headpiece.Nothing, Rose.Red, Lebkuchenherz.Big, Glasses.Sun)},
            { Suspects.Suessigkeiti, new Trait(Clothes.Casual, HairColor.Light, ClotheColor.Blue, Shoes.Sneaker, Hairstyle.Zopf, EyeColor.Blue, Headpiece.Cap, Rose.Nothing, Lebkuchenherz.Nothing, Glasses.Nothing)},
            { Suspects.GreifiTypi, new Trait(Clothes.Dirndl, HairColor.Colorful, ClotheColor.Green, Shoes.Heels, Hairstyle.Open, EyeColor.Green, Headpiece.Hendl, Rose.Nothing, Lebkuchenherz.Big, Glasses.Sun)},
            { Suspects.Achterbahni, new Trait(Clothes.Lederhosn, HairColor.Light, ClotheColor.Blue, Shoes.Sneaker, Hairstyle.Kurz, EyeColor.Blue, Headpiece.Cap, Rose.Nothing, Lebkuchenherz.Small, Glasses.Normal)},
            { Suspects.Geisterbahni, new Trait(Clothes.Lederhosn, HairColor.Dark, ClotheColor.Blue, Shoes.Haferl, Hairstyle.Kurz, EyeColor.Blue, Headpiece.Nothing, Rose.Red, Lebkuchenherz.Big, Glasses.Nothing)},
            { Suspects.DosiWerfi, new Trait(Clothes.Casual, HairColor.Light, ClotheColor.Green, Shoes.Haferl, Hairstyle.Kurz, EyeColor.Brown, Headpiece.Nothing, Rose.Yellow, Lebkuchenherz.Small, Glasses.Normal)}
        };

        private readonly Dictionary<Suspects, Trait>[] _liesHardMode = new Dictionary<Suspects, Trait>[2]
        {
            new Dictionary<Suspects, Trait>()
            {
                { Suspects.Saufi1, new Trait(Clothes.Lederhosn, HairColor.Light, ClotheColor.Red, Shoes.Heels, Hairstyle.Open, EyeColor.Green, Headpiece.Nothing, Rose.Yellow, Lebkuchenherz.Small, Glasses.Normal)},
                { Suspects.Saufi2, new Trait(Clothes.Casual, HairColor.Light, ClotheColor.Green, Shoes.Heels, Hairstyle.Zopf, EyeColor.Green, Headpiece.Cap, Rose.Nothing, Lebkuchenherz.Big, Glasses.Normal)},
                { Suspects.Aperoli1, new Trait(Clothes.Lederhosn, HairColor.Light, ClotheColor.Green, Shoes.Heels, Hairstyle.Open, EyeColor.Blue, Headpiece.Cap, Rose.Yellow, Lebkuchenherz.Small, Glasses.Sun)},
                { Suspects.Aperoli2, new Trait(Clothes.Lederhosn, HairColor.Light, ClotheColor.Red, Shoes.Haferl, Hairstyle.Zopf, EyeColor.Blue, Headpiece.Cap, Rose.Red, Lebkuchenherz.Small, Glasses.Sun)},
                { Suspects.Infoluenci, new Trait(Clothes.Lederhosn, HairColor.Dark, ClotheColor.Blue, Shoes.Sneaker, Hairstyle.Open, EyeColor.Blue, Headpiece.Cap, Rose.Yellow, Lebkuchenherz.Small, Glasses.Normal)},
                { Suspects.Suessigkeiti, new Trait(Clothes.Lederhosn, HairColor.Dark, ClotheColor.Green, Shoes.Haferl, Hairstyle.Open, EyeColor.Green, Headpiece.Hendl, Rose.Red, Lebkuchenherz.Small, Glasses.Normal)},
                { Suspects.GreifiTypi, new Trait(Clothes.Casual, HairColor.Dark, ClotheColor.Blue, Shoes.Haferl, Hairstyle.Zopf, EyeColor.Brown, Headpiece.Cap, Rose.Red, Lebkuchenherz.Small, Glasses.Normal)},
                { Suspects.Achterbahni, new Trait(Clothes.Casual, HairColor.Dark, ClotheColor.Red, Shoes.Haferl, Hairstyle.Open, EyeColor.Green, Headpiece.Hendl, Rose.Yellow, Lebkuchenherz.Nothing, Glasses.Sun)},
                { Suspects.Geisterbahni, new Trait(Clothes.Casual, HairColor.Light, ClotheColor.Green, Shoes.Heels, Hairstyle.Open, EyeColor.Green, Headpiece.Hendl, Rose.Yellow, Lebkuchenherz.Nothing, Glasses.Sun)},
                { Suspects.DosiWerfi, new Trait(Clothes.Lederhosn, HairColor.Colorful, ClotheColor.Red, Shoes.Heels, Hairstyle.Open, EyeColor.Green, Headpiece.Cap, Rose.Red, Lebkuchenherz.Nothing, Glasses.Sun)}
            },
            new Dictionary<Suspects, Trait>()
            {
                { Suspects.Saufi1, new Trait(Clothes.Lederhosn, HairColor.Colorful, ClotheColor.Blue, Shoes.Heels, Hairstyle.Zopf, EyeColor.Green, Headpiece.Hendl, Rose.Red, Lebkuchenherz.Big, Glasses.Nothing)},
                { Suspects.Saufi2, new Trait(Clothes.Dirndl, HairColor.Dark, ClotheColor.Green, Shoes.Sneaker, Hairstyle.Zopf, EyeColor.Blue, Headpiece.Nothing, Rose.Nothing, Lebkuchenherz.Nothing, Glasses.Normal)},
                { Suspects.Aperoli1, new Trait(Clothes.Casual, HairColor.Colorful, ClotheColor.Green, Shoes.Heels, Hairstyle.Kurz, EyeColor.Brown, Headpiece.Hendl, Rose.Nothing, Lebkuchenherz.Small, Glasses.Normal)},
                { Suspects.Aperoli2, new Trait(Clothes.Casual, HairColor.Dark, ClotheColor.Green, Shoes.Sneaker, Hairstyle.Kurz, EyeColor.Blue, Headpiece.Nothing, Rose.Red, Lebkuchenherz.Small, Glasses.Nothing)},
                { Suspects.Infoluenci, new Trait(Clothes.Casual, HairColor.Colorful, ClotheColor.Green, Shoes.Haferl, Hairstyle.Open, EyeColor.Green, Headpiece.Cap, Rose.Nothing, Lebkuchenherz.Nothing, Glasses.Normal)},
                { Suspects.Suessigkeiti, new Trait(Clothes.Dirndl, HairColor.Dark, ClotheColor.Red, Shoes.Heels, Hairstyle.Open, EyeColor.Brown, Headpiece.Nothing, Rose.Yellow, Lebkuchenherz.Small, Glasses.Sun)},
                { Suspects.GreifiTypi, new Trait(Clothes.Casual, HairColor.Dark, ClotheColor.Red, Shoes.Sneaker, Hairstyle.Kurz, EyeColor.Blue, Headpiece.Cap, Rose.Yellow, Lebkuchenherz.Small, Glasses.Nothing)},
                { Suspects.Achterbahni, new Trait(Clothes.Casual, HairColor.Colorful, ClotheColor.Green, Shoes.Heels, Hairstyle.Zopf, EyeColor.Green, Headpiece.Nothing, Rose.Red, Lebkuchenherz.Big, Glasses.Sun)},
                { Suspects.Geisterbahni, new Trait(Clothes.Casual, HairColor.Colorful, ClotheColor.Red, Shoes.Sneaker, Hairstyle.Zopf, EyeColor.Green, Headpiece.Cap, Rose.Yellow, Lebkuchenherz.Small, Glasses.Normal)},
                { Suspects.DosiWerfi, new Trait(Clothes.Dirndl, HairColor.Colorful, ClotheColor.Red, Shoes.Sneaker, Hairstyle.Open, EyeColor.Blue, Headpiece.Cap, Rose.Nothing, Lebkuchenherz.Big, Glasses.Nothing)}
            }
        };

        private Dictionary<Witnesses, WitnessTestimony> _testimonies = new Dictionary<Witnesses, WitnessTestimony>();
        
        private readonly List<Witnesses> _extraWitnesses = new List<Witnesses>()
        {
            Witnesses.SaufiGroup2,
            Witnesses.AperoliGroup2,
            Witnesses.KarussellParents
        };
        
        private Suspects _mainSuspect;
        private Suspects _lover;


        /// <summary>
        /// Select a Main Suspect and the corresponding Lover
        /// </summary>
        public void SelectMainSuspectAndLover()
        {
            Random random = new Random();
            
            int mainSuspectIndex  = random.Next(SuspectAmount);
            int loverIndex        = random.Next(SuspectAmount);
            
            while (loverIndex == mainSuspectIndex)
                loverIndex = random.Next(SuspectAmount);
            
            _mainSuspect = (Suspects)mainSuspectIndex;
            _lover       = (Suspects)loverIndex;
            
            Debug.Log(_mainSuspect + " + " + _lover);
        }

        public void DistributionEasyMode()
        {
            Trait lies = _suspectDescriptions[_mainSuspect].CreateLiesForAll();
            
            Dictionary<Witnesses, int[]> distribution = TypeDistribution(Witnesses.AutoscooterKid, Witnesses.KarussellKid);
            
            foreach (Witnesses witness in distribution.Keys)
            {
                WitnessTestimony testimony = new WitnessTestimony();

                if (witness == Witnesses.SaufiGroup || witness == Witnesses.AperoliGroup)
                {
                    testimony.AddToTestimonies(lies.CreateTraitDescription(distribution[witness][0]), 0);
                    testimony.AddToTestimonyTypes(distribution[witness][0], 0);
                    
                    testimony.AddToTestimonies(lies.CreateTraitDescription(distribution[witness][1]), 1);
                    testimony.AddToTestimonyTypes(distribution[witness][1], 1);
                    
                    testimony.AddToTestimonies(lies.CreateTraitDescription(distribution[witness][2]), 2);
                    testimony.AddToTestimonyTypes(distribution[witness][2], 2);
                }
                else if (witness == Witnesses.AutoscooterKid || witness == Witnesses.KarussellKid)
                {
                    testimony.AddToTestimonies(_suspectDescriptions[_mainSuspect].CreateTraitDescription(distribution[witness][0]), 0);
                    testimony.AddToTestimonyTypes(distribution[witness][0], 0);
                    
                    testimony.AddToTestimonies(_suspectDescriptions[_mainSuspect].CreateTraitDescription(distribution[witness][1]), 1);
                    testimony.AddToTestimonyTypes(distribution[witness][1], 1);
                    
                    testimony.AddToTestimonies(lies.CreateTraitDescription(distribution[witness][2]), 2);
                    testimony.AddToTestimonyTypes(distribution[witness][2], 2);
                }
                else
                {
                    testimony.AddToTestimonies(_suspectDescriptions[_mainSuspect].CreateTraitDescription(distribution[witness][0]), 0);
                    testimony.AddToTestimonyTypes(distribution[witness][0], 0);
                    
                    testimony.AddToTestimonies(lies.CreateTraitDescription(distribution[witness][1]), 1);
                    testimony.AddToTestimonyTypes(distribution[witness][1], 1);
                    
                    testimony.AddToTestimonies(lies.CreateTraitDescription(distribution[witness][2]), 2);
                    testimony.AddToTestimonyTypes(distribution[witness][2], 2);
                }
                
                testimony.ShuffleTestimonies();
                _testimonies.Add(witness, testimony);
            }
        }

        public void DistributionHardMode()
        {
            // Select two Randoms to get two Truths
            Witnesses twoTruths1 = Witnesses.SaufiGroup;
            Witnesses twoTruths2 = Witnesses.SaufiGroup;
            Random random = new Random();
            
            while (twoTruths1 == Witnesses.SaufiGroup
                   || twoTruths1 == Witnesses.AperoliGroup
                   || _extraWitnesses.Contains(twoTruths1))
                twoTruths1 = (Witnesses)random.Next(Enum.GetValues(typeof(Witnesses)).Length);
            
            while (twoTruths2 == Witnesses.SaufiGroup
                   || twoTruths2 == Witnesses.AperoliGroup
                   || _extraWitnesses.Contains(twoTruths2)
                   || twoTruths2 == twoTruths1)
                twoTruths2 = (Witnesses)random.Next(Enum.GetValues(typeof(Witnesses)).Length);
            
            Dictionary<Witnesses, int[]> distribution = TypeDistribution(twoTruths1, twoTruths2);

            foreach (Witnesses witness in distribution.Keys)
            {
                WitnessTestimony testimony = new WitnessTestimony();
                
                if (witness == Witnesses.SaufiGroup || witness == Witnesses.AperoliGroup)
                {
                    testimony.AddToTestimonies(_liesHardMode[0][_mainSuspect].CreateTraitDescription(distribution[witness][0]), 0);
                    testimony.AddToTestimonyTypes(distribution[witness][0], 0);
                    
                    testimony.AddToTestimonies(_liesHardMode[1][_mainSuspect].CreateTraitDescription(distribution[witness][1]), 1);
                    testimony.AddToTestimonyTypes(distribution[witness][1], 1);
                    
                    testimony.AddToTestimonies(_liesHardMode[1][_mainSuspect].CreateTraitDescription(distribution[witness][2]), 2);
                    testimony.AddToTestimonyTypes(distribution[witness][2], 2);
                }
                else if (witness == twoTruths1 || witness == twoTruths2)
                {
                    testimony.AddToTestimonies(_suspectDescriptions[_mainSuspect].CreateTraitDescription(distribution[witness][0]), 0);
                    testimony.AddToTestimonyTypes(distribution[witness][0], 0);
                    
                    testimony.AddToTestimonies(_suspectDescriptions[_mainSuspect].CreateTraitDescription(distribution[witness][1]), 1);
                    testimony.AddToTestimonyTypes(distribution[witness][1], 1);
                    
                    testimony.AddToTestimonies(_liesHardMode[0][_mainSuspect].CreateTraitDescription(distribution[witness][2]), 2);
                    testimony.AddToTestimonyTypes(distribution[witness][2], 2);
                }
                else
                {
                    testimony.AddToTestimonies(_suspectDescriptions[_mainSuspect].CreateTraitDescription(distribution[witness][0]), 0);
                    testimony.AddToTestimonyTypes(distribution[witness][0], 0);
                    
                    testimony.AddToTestimonies(_liesHardMode[0][_mainSuspect].CreateTraitDescription(distribution[witness][1]), 1);
                    testimony.AddToTestimonyTypes(distribution[witness][1], 1);
                    
                    testimony.AddToTestimonies(_liesHardMode[1][_mainSuspect].CreateTraitDescription(distribution[witness][2]), 2);
                    testimony.AddToTestimonyTypes(distribution[witness][2], 2);
                }
                
                testimony.ShuffleTestimonies();
                _testimonies.Add(witness, testimony);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="twoTruths1">First Witness for 2 Truths</param>
        /// <param name="twoTruths2">Second Witness for 2 Truths</param>
        private Dictionary<Witnesses, int[]> TypeDistribution(Witnesses twoTruths1, Witnesses twoTruths2)
        {
            _testimonies = new Dictionary<Witnesses, WitnessTestimony>();
            
            Dictionary<Witnesses, int[]> distribution = new Dictionary<Witnesses, int[]>();
            
            Random random = new Random();
            
            List<int> truthIndexes = new List<int>(){0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            List<int> liesIndexes1 = new List<int>(){0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            List<int> liesIndexes2 = new List<int>(){0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            
            HashSet<int> numberCalled = new HashSet<int>();
            
            int[] testimonies;

            // Groups with only lies
            foreach (Witnesses witnesses in new List<Witnesses>() { Witnesses.SaufiGroup , Witnesses.AperoliGroup})
            {
                testimonies = new int[] { -1, -1, -1 };
                
                testimonies = SelectFromLists(liesIndexes1, liesIndexes2, liesIndexes2, testimonies);

                liesIndexes1.Remove(testimonies[0]);
                liesIndexes2.Remove(testimonies[1]);
                liesIndexes2.Remove(testimonies[2]);

                numberCalled.Add(testimonies[0]);
                numberCalled.Add(testimonies[1]);
                numberCalled.Add(testimonies[2]);

                distribution.Add(witnesses, testimonies);
            }

            // Persons with 2 Truths
            foreach (Witnesses witnesses in new List<Witnesses>() { twoTruths1, twoTruths2 })
            {
                testimonies = new int[] { -1, -1, -1 };
                
                testimonies = SelectFromLists(truthIndexes, truthIndexes, liesIndexes1, testimonies);

                truthIndexes.Remove(testimonies[0]);
                truthIndexes.Remove(testimonies[1]);
                liesIndexes1.Remove(testimonies[2]);

                numberCalled.Add(testimonies[0]);
                numberCalled.Add(testimonies[1]);
                numberCalled.Add(testimonies[2]);

                distribution.Add(witnesses, testimonies);

            }

            int usedWitnesses = 4;
            foreach (Witnesses witnesses in Enum.GetValues(typeof(Witnesses)))
            {
                testimonies = new int[] { -1, -1, -1 };
                
                if (witnesses == Witnesses.SaufiGroup
                    || witnesses == Witnesses.AperoliGroup
                    || witnesses == twoTruths1
                    || witnesses == twoTruths2
                    || _extraWitnesses.Contains(witnesses))
                    continue;
                
                usedWitnesses++;
                
                // Special Cases for End of List
                if (usedWitnesses == SuspectAmount - 2)
                {
                    List<int> missingNumbers = new List<int>();
                    for (int i = 0; i < SuspectAmount; i++)
                    {
                        if (numberCalled.Contains(i)) continue;
                            
                        missingNumbers.Add(i);
                    }
                    
                    if (numberCalled.Count == 8)
                    {
                        switch (random.Next(6))
                        {
                            case 0:
                                testimonies[0] = missingNumbers[0];
                                testimonies[1] = missingNumbers[1];
                                break;
                            
                            case 1:
                                testimonies[0] = missingNumbers[0];
                                testimonies[2] = missingNumbers[1];
                                break;
                            
                            case 2:
                                testimonies[1] = missingNumbers[0];
                                testimonies[0] = missingNumbers[1];
                                break;
                            
                            case 3:
                                testimonies[1] = missingNumbers[0];
                                testimonies[2] = missingNumbers[1];
                                break;
                            
                            case 4:
                                testimonies[2] = missingNumbers[0];
                                testimonies[0] = missingNumbers[1];
                                break;
                            
                            case 5:
                                testimonies[2] = missingNumbers[0];
                                testimonies[1] = missingNumbers[1];
                                break;
                        }
                    }
                    else if (numberCalled.Count == 9)
                    {
                        switch (random.Next(3))
                        {
                            case 0:
                                testimonies[0] = missingNumbers[0];
                                break;
                            
                            case 1:
                                testimonies[1] = missingNumbers[0];
                                break;
                            
                            case 2:
                                testimonies[2] = missingNumbers[0];
                                break;
                        }
                    }
                }

                if (usedWitnesses == SuspectAmount - 1)
                {
                    List<int> twoTimes = new List<int>();
                    foreach (int truthIndex in truthIndexes)
                    {
                        foreach (int liesIndex1 in liesIndexes1)
                        {
                            if (truthIndex != liesIndex1) continue;
                            
                            twoTimes.Add(truthIndex);
                        }

                        foreach (int liesIndex2 in liesIndexes2)
                        {
                            if (truthIndex != liesIndex2) continue;
                            
                            twoTimes.Add(truthIndex);
                        }
                    }
                    
                    foreach (int liesIndex1 in liesIndexes1)
                    {
                        foreach (int liesIndex2 in liesIndexes2)
                        {
                            if (liesIndex1 != liesIndex2) continue;
                            
                            twoTimes.Add(liesIndex1);
                        }
                    }

                    if (twoTimes.Count == 2)
                    {
                        if (!truthIndexes.Contains(twoTimes[0]))
                        {
                            if (!truthIndexes.Contains(twoTimes[1]))
                            {
                                if (random.Next(2) == 0)
                                {
                                    testimonies[1] = twoTimes[0];
                                    testimonies[2] = twoTimes[1];
                                }
                                else
                                {
                                    testimonies[1] = twoTimes[1];
                                    testimonies[2] = twoTimes[0];
                                }
                            }
                            else if (!liesIndexes1.Contains(twoTimes[1]))
                            {
                                if (random.Next(2) == 0)
                                {
                                    testimonies[2] = twoTimes[0];
                                    testimonies[0] = twoTimes[1];
                                }
                                else
                                {
                                    testimonies[1] = twoTimes[0];
                                    testimonies[2] = twoTimes[1];
                                }
                            }
                            else
                            {
                                if (random.Next(2) == 0)
                                {
                                    testimonies[1] = twoTimes[0];
                                    testimonies[0] = twoTimes[1];
                                }
                                else
                                {
                                    testimonies[2] = twoTimes[0];
                                    testimonies[1] = twoTimes[1];
                                }
                            }
                            
                        }
                        else if (!liesIndexes1.Contains(twoTimes[0]))
                        {
                            if (!liesIndexes1.Contains(twoTimes[1]))
                            {
                                if (random.Next(2) == 0)
                                {
                                    testimonies[0] = twoTimes[0];
                                    testimonies[2] = twoTimes[1];
                                }
                                else
                                {
                                    testimonies[0] = twoTimes[1];
                                    testimonies[2] = twoTimes[0];
                                }
                            }
                            else if (!truthIndexes.Contains(twoTimes[1]))
                            {
                                if (random.Next(2) == 0)
                                {
                                    testimonies[2] = twoTimes[0];
                                    testimonies[1] = twoTimes[1];
                                }
                                else
                                {
                                    testimonies[0] = twoTimes[0];
                                    testimonies[2] = twoTimes[1];
                                }
                            }
                            else
                            {
                                if (random.Next(2) == 0)
                                {
                                    testimonies[0] = twoTimes[0];
                                    testimonies[1] = twoTimes[1];
                                }
                                else
                                {
                                    testimonies[2] = twoTimes[0];
                                    testimonies[0] = twoTimes[1];
                                }
                            }
                            
                        }
                        else
                        {
                            if (!liesIndexes2.Contains(twoTimes[1]))
                            {
                                if (random.Next(2) == 0)
                                {
                                    testimonies[0] = twoTimes[0];
                                    testimonies[1] = twoTimes[1];
                                }
                                else
                                {
                                    testimonies[0] = twoTimes[1];
                                    testimonies[1] = twoTimes[0];
                                }
                            }
                            else if (!truthIndexes.Contains(twoTimes[1]))
                            {
                                if (random.Next(2) == 0)
                                {
                                    testimonies[1] = twoTimes[0];
                                    testimonies[2] = twoTimes[1];
                                }
                                else
                                {
                                    testimonies[0] = twoTimes[0];
                                    testimonies[1] = twoTimes[1];
                                }
                            }
                            else
                            {
                                if (random.Next(2) == 0)
                                {
                                    testimonies[0] = twoTimes[0];
                                    testimonies[2] = twoTimes[1];
                                }
                                else
                                {
                                    testimonies[1] = twoTimes[0];
                                    testimonies[0] = twoTimes[1];
                                }
                            }
                            
                        }
                    }
                    else if (twoTimes.Count == 1)
                    {
                        if (!truthIndexes.Contains(twoTimes[0]))
                        {
                            testimonies[random.Next(2) + 1] = twoTimes[0];
                        }
                        else if (!liesIndexes1.Contains(twoTimes[0]))
                        {
                            testimonies[random.Next(2) == 0 ? 0 : 2] = twoTimes[0];
                        }
                        else
                        {
                            testimonies[random.Next(2)] = twoTimes[0];
                        }
                    }
                }

                testimonies = SelectFromLists(truthIndexes, liesIndexes1, liesIndexes2, testimonies);

                truthIndexes.Remove(testimonies[0]);
                liesIndexes1.Remove(testimonies[1]);
                liesIndexes2.Remove(testimonies[2]);

                numberCalled.Add(testimonies[0]);
                numberCalled.Add(testimonies[1]);
                numberCalled.Add(testimonies[2]);

                distribution.Add(witnesses, testimonies);
            }

            return distribution;
        }

        /// <summary>
        /// Select Random Elements from the Lists
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <param name="list3"></param>
        /// <param name="testimonies"></param>
        /// <returns></returns>
        private int[] SelectFromLists(List<int> list1, List<int> list2, List<int> list3, int[] testimonies)
        {
            Random random = new Random();
            List<int> tmp = new List<int>();
            
            if (testimonies[0] == -1)
            {
                tmp.Clear();
                
                testimonies[0] = list1[random.Next(list1.Count)];
                while (testimonies[0] == testimonies[1] ||
                       testimonies[0] == testimonies[2])
                {
                    tmp.Add(testimonies[0]);
                    list1.Remove(testimonies[0]);
                    
                    int random1    = random.Next(list1.Count);
                    testimonies[0] = list1[random1];
                }

                foreach (int tp in tmp)
                    list1.Add(tp);
            }

            if (testimonies[1] == -1)
            {
                tmp.Clear();

                testimonies[1] = list2[random.Next(list2.Count)];
                while (testimonies[1] == testimonies[0] ||
                       testimonies[1] == testimonies[2])
                {
                    tmp.Add(testimonies[1]);
                    list2.Remove(testimonies[1]);

                    int random2    = random.Next(list2.Count);
                    testimonies[1] = list2[random2];
                }

                foreach (int tp in tmp)
                    list2.Add(tp);
            }

            if (testimonies[2] == -1)
            {
                tmp.Clear();

                testimonies[2] = list3[random.Next(list3.Count)];
                while (testimonies[2] == testimonies[0] ||
                       testimonies[2] == testimonies[1])
                {
                    tmp.Add(testimonies[2]);
                    list3.Remove(testimonies[2]);
                    
                    int random3    = random.Next(list3.Count);
                    testimonies[2] = list3[random3];
                }

                foreach (int tp in tmp)
                    list3.Add(tp);
            }
            
            return testimonies;
        }

        public string GetTestimonyForWitness(Witnesses witnesses, int index)
        {
            if (witnesses == Witnesses.KarussellParents)
                witnesses = Witnesses.KarussellKid;
            
            if (witnesses == Witnesses.SaufiGroup2)
                witnesses = Witnesses.SaufiGroup;
            
            if (witnesses == Witnesses.AperoliGroup2)
                witnesses = Witnesses.AperoliGroup;
            
            return _testimonies[witnesses].GetTestimonies()[index];
        }

        public int GetTestimonyTypeForWitness(Witnesses witnesses, int index)
        {
            if (witnesses == Witnesses.KarussellParents)
                witnesses = Witnesses.KarussellKid;
            
            if (witnesses == Witnesses.SaufiGroup2)
                witnesses = Witnesses.SaufiGroup;
            
            if (witnesses == Witnesses.AperoliGroup2)
                witnesses = Witnesses.AperoliGroup;
            
            return _testimonies[witnesses].GetTestimonyType(index);
        }
        
        public Suspects GetMainSuspect() => _mainSuspect;

        public Suspects GetLover() => _lover;
    }
}