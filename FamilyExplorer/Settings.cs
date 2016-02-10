using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyExplorer
{
    public static class Settings
    {

        public static string[] GenderList = new string[]
        {
            "Female", "Male", "Other", "Not Specified"
        };

        public static string ColorBackgroundFemale = "LightPink";
        public static string ColorBorderBrushFemale = "Red";
        public static string ColorTextFemale = "Black";

        public static string ColorBackgroundMale = "LightBlue";
        public static string ColorBorderBrushMale = "Gray";
        public static string ColorTextMale = "Black";

        public static string ColorBackgroundOther = "White";
        public static string ColorBorderBrushOther = "Black";
        public static string ColorTextOther = "Black";

        public static string ColorBackgroundNotSpecified = "White";
        public static string ColorBorderBrushNotSpecified = "Black";
        public static string ColorTextNotSpecified = "Black";

        public static double PersonWidth = 100;
        public static double PersonHeight = 80;

        public static double HorizontalSpace = 50;
        public static double VerticalSpace = 80;


    }
}
