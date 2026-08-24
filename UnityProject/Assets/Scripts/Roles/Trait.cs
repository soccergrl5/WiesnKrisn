using System;
using WiesnKrisn.Roles.Traits;

namespace WiesnKrisn.Roles
{
    public class Trait
    {
        private Clothes _clothes;
        private HairColor _hairColor;
        private ClotheColor _clotheColor;
        private Shoes _shoes;
        private Hairstyle _hairstyle;
        private EyeColor _eyeColor;
        private Headpiece _headpiece;
        private Rose _rose;
        private Lebkuchenherz _lebkuchenherz;
        private Glasses _glasses;

        public Trait(Clothes clothes, HairColor hairColor, ClotheColor clotheColor, Shoes shoes, Hairstyle hairstyle, EyeColor eyeColor, Headpiece headpiece, Rose rose, Lebkuchenherz lebkuchenherz, Glasses glasses)
        {
            _clothes       = clothes;
            _hairColor     = hairColor;
            _clotheColor   = clotheColor;
            _shoes         = shoes;
            _hairstyle     = hairstyle;
            _eyeColor      = eyeColor;
            _headpiece     = headpiece;
            _rose          = rose;
            _lebkuchenherz = lebkuchenherz;
            _glasses       = glasses;
        }

        public string CreateTraitDescription(int type)
        {
            switch (type)
            {
                case 0:
                    switch (_clothes)
                    {
                        case Clothes.Casual:
                            return "wore casual clothing";
                        
                        case Clothes.Lederhosn:
                            return "wore leather pants";
                        
                        case Clothes.Dirndl:
                            return "wore a dirndl";
                    }
                    break;
                
                case 1:
                    switch (_hairColor)
                    {
                        case HairColor.Dark:
                            return "had dark hair";
                        
                        case HairColor.Light:
                            return "had light hair";
                        
                        case HairColor.Colorful:
                            return "had coloured hair";
                    }
                    break;
                
                case 2:
                    switch (_clotheColor)
                    {
                        case ClotheColor.Red:
                            return "had blue clothing";
                        
                        case ClotheColor.Green:
                            return "had green clothing";
                        
                        case ClotheColor.Blue:
                            return "had blue clothing";
                    }
                    break;
                
                case 3:
                    switch (_shoes)
                    {
                        case Shoes.Sneaker:
                            return "wore sneakers";
                        
                        case Shoes.Heels:
                            return "wore heels";
                        
                        case Shoes.Haferl:
                            return "wore haferl-shoes";
                    }
                    break;
                
                case 4:
                    switch (_hairstyle)
                    {
                        case Hairstyle.Kurz:
                            return "had short hair";
                        
                        case Hairstyle.Open:
                            return "had open hair";
                        
                        case Hairstyle.Zopf:
                            return "had a ponytail";
                    }
                    break;
                
                case 5:
                    switch (_eyeColor)
                    {
                        case EyeColor.Brown:
                            return "had brown eyes";
                        
                        case EyeColor.Green:
                            return "had green eyes";
                        
                        case EyeColor.Blue:
                            return "had blue eyes";
                    }
                    break;
                
                case 6:
                    switch (_headpiece)
                    {
                        case Headpiece.Nothing:
                            return "wore no head gear";
                        
                        case Headpiece.Cap:
                            return "wore a cap";
                        
                        case Headpiece.Hendl:
                            return "wore a hendl-hat";
                    }
                    break;
                
                case 7:
                    switch (_rose)
                    {
                        case Rose.Nothing:
                            return "had no rose";
                        
                        case Rose.Red:
                            return "had a red rose";
                        
                        case Rose.Yellow:
                            return "had a yellow rose";
                    }
                    break;
                
                case 8:
                    switch (_lebkuchenherz)
                    {
                        case Lebkuchenherz.Nothing:
                            return "wore no gingerbread heart";
                        
                        case Lebkuchenherz.Small:
                            return "wore a small gingerbread heart";
                        
                        case Lebkuchenherz.Big:
                            return "wore a big gingerbread heart";
                    }
                    break;
                
                case 9:
                    switch (_glasses)
                    {
                        case Glasses.Nothing:
                            return "wore no glasses";
                        
                        case Glasses.Normal:
                            return "wore regular glasses";
                        
                        case Glasses.Sun:
                            return "wore sunglasses";
                    }
                    break;
            }

            return "";
        }

        public Trait CreateLiesForAll()
        {
            Random random = new Random();
            
            return new Trait(
                (Clothes)((random.Next(2) + 1 + (int) _clothes) % 3),
                (HairColor)((random.Next(2) + 1 + (int) _hairColor) % 3),
                (ClotheColor)((random.Next(2) + 1 + (int) _clotheColor) % 3),
                (Shoes)((random.Next(2) + 1 + (int) _shoes) % 3),
                (Hairstyle)((random.Next(2) + 1 + (int) _hairstyle) % 3),
                (EyeColor)((random.Next(2) + 1 + (int) _eyeColor) % 3),
                (Headpiece)((random.Next(2) + 1 + (int) _headpiece) % 3),
                (Rose)((random.Next(2) + 1 + (int) _rose) % 3),
                (Lebkuchenherz)((random.Next(2) + 1 + (int) _lebkuchenherz) % 3),
                (Glasses)((random.Next(2) + 1 + (int) _glasses) % 3)
            );
        }
    }
}