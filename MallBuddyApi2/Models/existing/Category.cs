using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MallBuddyApi2.Models.existing
{
    public class Category
    {
        public Category(string cat)
        {
            // TODO: Complete member initialization
            StoreCategory toParse;
            if (Enum.TryParse(cat, out toParse))
                this.CategoryType = toParse;
        }
        public Category() { }
        //public String ID { get; set; }
        [EnumDataType(typeof(StoreCategory))]
        public StoreCategory CategoryType { get; set; }
        [Key]
        public int Id { get; set; }

        public enum StoreCategory
        {
            WOMEN_FASHION=1, MEN_FASHION, GATE, TIOLET, KIDS, SHOES, ATM, PARKING, FOOD_COFFEE, HEALTH, LIFESTYLE,
            UNDERWEAR
                , GIFTS, ELECTRIC, CELLULAR, OPTICS, ACCESSORIES,TATOO, SPORTS, RECREATON, MISC
        }
        public static Dictionary<string, string> HebrewMappings;
        public static string[] HebrewCategories = {"אופנת נשים", "אופנת גברים", "שערים", "שירותים",
"ילדים", "הנעלה", "כספומט", "חניון",
"מסעדות וקפה", "בריאות", "לייפסטייל","הלבשה תחתונה",
"מתנות ובית", "מוצרי חשמל", "מחשבים וסלולר", "אופטיקה ומשקפיים",
"תיקים ואקססוריז","קעקועים", "ספורט","פנאי", "שונות"};

        public static string[] HebrewCategoriesCenter = {"אופנת נשים", "אופנת גברים", "שערים", "שירותים",
"ילדים", "הנעלה", "כספומט", "חניון",
"מסעדות ובתי קפה", "בריאות", "לייפסטייל וקוסמטיקה","הלבשה תחתונה",
"מתנות פנאי והום סטיילינג", "מוצרי חשמל", "מחשבים תקשורת ומוצרי חשמל", "אופטיקה ומשקפיים",
"תיקים ואקססוריז","קעקועים", "ספורט ומחנאות","פנאי", "שונות"};
    }
}
