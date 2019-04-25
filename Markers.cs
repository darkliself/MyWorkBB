//§§1114953140896 -- "Markers" "Alex K." 

MarkerTypeAndUse();
MarkerInkColor();
// 3 and 4 bullet in MarkerPointType()
MarkerPointType();
// pack size
NonToxic();
Odor();

// "Bullet 1" "Marker type & Use" 
void MarkerTypeAndUse() {
    var result = "";
    var markerType = R("SP-22698").HasValue() ? R("SP-22698").Replace("<NULL>", "").Text :
		R("cnet_common_SP-22698").HasValue() ? R("cnet_common_SP-22698").Replace("<NULL>", "").Text : "";
    var quickDry = Coalesce(A[6282]);
    if (!String.IsNullOrEmpty(markerType)) {
        if (quickDry.HasValue() && markerType.ToLower().Equals("alcohol")) {
            result = "These alcohol based markers dry fast and do not bleed";
        }
        else {
            var tmp  = markerType.ToLower();
            switch(tmp) {
                case "alcohol":
                    result = "These alcohol based markers make a great addition to any craft room or design studio";
                    break;
                case "brush tip":
                    result = "Brush tip creates fluid lines and the marker tips are perfect for consistency when coloring";
                    break;
                case "calligraphy":
                    result = "Makes lettering for Holiday, Wedding, Anniversary or other festive events easy";
                    break;
                case "chalk":
                    result = "A wonderful way to create chalkboard art to attract attention and increase sales";
                    break;
                case "dry erase":
                    result = "Dry-erase markers for writing and drawing";
                    break;
                case "wet erase":
                    result = "Washes from most fabrics and can be removed from skin with soap and water";
                    break;
                case "fabric":
                    result = "Fabric marker set for customized designs on clothing, paper, lamps, jewelry, frames and more";
                    break;
                case "art":
                    result = "Perfect for customizing your arts and crafts, accessories and more. Ideal for artists of all ages";
                    break;
                case "paint":
                    result = "These paint markers are perfect for adding a bold touch of color to any project";
                    break;
                case "permanent":
                    result = "Permanent ink formula is designed to last";
                    break;
                case "sketch":
                    result = "Sketch markers are used by manga artists, designers, and architects worldwide";
                    break;
                case "water based":
                    result = "Water-based paint markers for writing and drawing";
                    break;
                case "china":
                    result = "China marker peels for continuous use";
                    break;
                case "kid's markers":
                    result = "Kid's markers are great for arts, crafts, homework, school projects, journal entries, scrapbooks, and more";
                    break;
                case "window":
                    result = "Window markers are suitable for indoor and outdoor use on windows around the house or car";
                    break;
                default: 
                    result = $"{markerType} markers for writing and drawing";
                    break;
            }
        }  
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"MarkerTypeAndUse⸮{result}");
    }
}

// "Bullet 2" "Marker ink color";
void MarkerInkColor() {
    var result = "";
    var trueColor = R("SP-23267").HasValue() ? R("SP-23267").Replace("<NULL>", "").Text :
		R("cnet_common_SP-23267").HasValue() ? R("cnet_common_SP-23267").Replace("<NULL>", "").Text : "";

    if (!String.IsNullOrEmpty(trueColor) && trueColor.ToLower().Equals("assorted")) {
        result = "Comes in assorted colors";
    }
    else if (!String.IsNullOrEmpty(trueColor) && !trueColor.ToLower().Equals("<null>")) {
        result = $"Ink comes in {trueColor.ToLower()}";
    }
    if (!String.IsNullOrEmpty(trueColor)) {
        Add($"MarkerInkColor⸮{result}");
    }
}


// "Bullet 3" "Marker point type" 
void MarkerPointType() {
    var result = "";
    var pointType = R("SP-16612").HasValue() ? R("SP-16612").Replace("<NULL>", "").Text :
		R("cnet_common_SP-16612").HasValue() ? R("cnet_common_SP-16612").Replace("<NULL>", "").Text : "";
    var firstLineType = Coalesce(A[5981]);
    var secondLineType = Coalesce(A[5984]);

    if (pointType.ToLower().Equals("bold")) {
        result = "Bold point creates thick, strong and detailed lines. Provides high visibility for your needs";
    }
    else if (pointType.ToLower().Equals("bullet")) {
        result = "Bullet tip writes with uniform and defined lines";
    }
    else if (pointType.ToLower().Equals("extra fine")) {
        result = "Extra fine point for detailed writing applications";
    }
    else if (pointType.ToLower().Equals("fine")) {
        result  = "Fine-point tip creates thin, accurate lines";
    }
    else if (pointType.ToLower().Equals("ultra fine")) {
        result = "Ultra-fine point tip creates thin, accurate lines";
    }
    else if (pointType.ToLower().Equals("medium")) {
        result = "Medium point provides accuracy and detail";
    }
    else if (pointType.ToLower().Equals("broad")) {
        result = "Creates broad and fine lines";
    }
    else if (pointType.ToLower().Equals("super fine")) {
        result = "Write with precision and accuracy with this super-fine tip";
    }
    else if (pointType.ToLower().Equals("conical")) {
        result = "Conical tips write broad and narrow";
    }
    // "Bullet 3 and 4" "2nd marker tip type (If applicable)" 
    else if (pointType.ToLower().Equals("twin tip") 
        && firstLineType.HasValue("extra fine") 
        && secondLineType.HasValue("fine")) {
            result = "Features a fine point for thin, detailed lines on one end and an ultra-fine point for even more precise writing on the other end";
    }
    else if (pointType.ToLower().Equals("twin tip") 
        && secondLineType.HasValue("fine")) {
            result = "Dual-tip multi-purpose marker";
    }
    else if (pointType.ToLower().Equals("twin tip")
        || (firstLineType.HasValue("fine") && secondLineType.HasValue("brush"))
        || (firstLineType.HasValue("brush") && secondLineType.HasValue("fine"))) {
            result = "While fine tip allows thin and consistent lines, the brush tip can be used to draw medium to bold strokes by altering the pressure on the tip";
    }
    else if (pointType.ToLower().Equals("twin tip")
        && firstLineType.HasValue() && secondLineType.HasValue()) {
            result = $"Features both {firstLineType.FirstValue().RegexReplace("(.+)", "a $1").RegexReplace("a ultra fine", "an ultra-fine")} tip and {secondLineType.FirstValue().RegexReplace("a ultra fine", "an ultra-fine")} tip, perfect for creating a variety of looks";
    }
     else if (!String.IsNullOrEmpty(pointType)) {
        result = $"{pointType} point provides accuracy and detail";
    }   
    if (!String.IsNullOrEmpty(result)) {
        Add($"MarkerPointType⸮{result}");
    }  
}

// BUllet 4 "Pack size" 

// "Bullet 5" "NON-TOXIC" 
void NonToxic() {
    var result = "";
    var nonToxic = R("SP-21018").HasValue() ? R("SP-21018").Replace("<NULL>", "").Text :
		R("cnet_common_SP-21018").HasValue() ? R("cnet_common_SP-21018").Replace("<NULL>", "").Text : "";
    if (nonToxic.ToLower().Equals("yes")) {
        result = "As these markers are non-toxic, they are extremely safe to use";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"NonToxic⸮{result}");
    }
}


// "Bullet 7" "Certification & Standards" 

// "Additional Low Odor" 
void Odor() {
    var result = "";
    var odor = R("SP-21015").HasValue() ? R("SP-21015").Replace("<NULL>", "").Text :
		R("cnet_common_SP-21015").HasValue() ? R("cnet_common_SP-21015").Replace("<NULL>", "").Text : "";

    if (odor.ToLower().Equals("low odor")) {
        result = "Low-odor ink for your comfort";
    }
    else if (odor.ToLower().Equals("yes")) {
        result = "Bring this odorless markers with you to work or school for smear-free applications";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalOdor⸮{result}");
    }
}


//1114953140896 end of "Markers" §§