using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MallBuddyApi2.Models.existing
{
    public class Category : IEquatable<Category>
    {
        public Category(string cat)
        {
            // TODO: Complete member initialization
            StoreCategory toParse;
            if (Enum.TryParse(cat, out toParse))
                this.CategoryType = toParse;
        }

        public Category(int cat)
        {
            // TODO: Complete member initialization
            //StoreCategory toParse;
            //if (Enum.TryParse(cat, out toParse))
                this.CategoryType = (StoreCategory)cat ;
        }
        public Category() { }
        //public String ID { get; set; }
        [EnumDataType(typeof(StoreCategory))]
        public StoreCategory CategoryType { get; set; }
        [Key]
        public int Id { get; set; }

        public enum StoreCategory
        {
            GATE = 1, TIOLET, MEN_FASHION, WOMEN_FASHION, PARKING, RECREATON, SHOES, KIDS,
            UNDERWEAR, LIFESTYLE, HEALTH, FOOD_COFFEE,
            OPTICS, CELLULAR, ELECTRIC, GIFTS
               , MISC, SPORTS, ACCESSORIES, TATOO
        }
        public static Dictionary<string, string> HebrewMappings;
        public static string[] HebrewCategories = {"אופנת נשים", "אופנת גברים",  "שירותים","שערים",
"חניון", "פנאי", "הנעלה", "ילדים",
"מסעדות וקפה", "בריאות", "לייפסטייל","הלבשה תחתונה",
"מתנות ובית", "מוצרי חשמל", "מחשבים וסלולר", "אופטיקה ומשקפיים",
"תיקים ואקססוריז","קעקועים", "ספורט","פנאי", "שונות"};

        public static string[] HebrewCategoriesCenter = {  "שערים", "שירותים","אופנת גברים","אופנת נשים",
"חניון", "פנאי", "הנעלה", "ילדים",
"הלבשה תחתונה",  "לייפסטייל וקוסמטיקה","בריאות","מסעדות ובתי קפה",
"אופטיקה ומשקפיים","מחשבים תקשורת ומוצרי חשמל", "מוצרי חשמל",  "מתנות פנאי והום סטיילינג",
 "שונות", "ספורט ומחנאות", "תיקים ואקססוריז","קעקועים"};

        public bool Equals(Category other)
        {
            return this.CategoryType.Equals(other.CategoryType);
        }

        public override int GetHashCode()
        {
            return ((int)CategoryType).GetHashCode();
        }
    }

    public class CategoryIntComparer : IEqualityComparer
    {

        public bool Equals(object x, object y)
        {
            if (x is Category)
                if (y is int)
                    return (int)((Category)x).CategoryType == (int)y;
            if (y is Category)
                if (x is int)
                    return (int)((Category)y).CategoryType == (int)x;
            return false;
        }

        public int GetHashCode(object obj)
        {
            if (obj is Category)
                return ((int)((Category)obj).CategoryType).GetHashCode();
            return obj.GetHashCode();

        }
    }
}

