// §§1676385310823140679 -- "Colored paper" "Alex K."

ColoredPaperTypeAndUse();
NumberOfHoles();
ColoredPaperSheetDimentions();
PaperWeight();
PaperColorTypeAndBrightness();

// "Bullet 1" "Paper type & Use"

void ColoredPaperTypeAndUse() {
    var result = "";
    var mediaType = A[4759];
    var printingTechnology = A[4763];
    var paperType = R("SP-21737").HasValue() ? R("SP-21737").Text : R("cnet_common_SP-21737").HasValue() ? R("cnet_common_SP-21737").Text : Coalesce("");
    if (mediaType.HasValue("%ply%collated%paper%")) {
        result = "{mediaType.FirstValue()} is ideal for every day use and special occasions";
    }
    else if (String.IsNullOrEmpty(paperType)) {
        result = $"Reliable {paperType.ToLower()} paper is perfect for everyday use";
    }
    else if (printingTechnology.HasValue()) {
        result = $"Works great in your {printingTechnology.Values.Select(s => s.Value()).FlattenWithAnd().Replace("and", "or")} printer";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"ColoredPaperTypeAndUse⸮{result}");
    }
}

// "Bullet 2" "Number of holes"	

void NumberOfHoles() {
    var holePunnched = A[4783];
    var arr = new List<string>();
    if (DC.KSP.GetLines().Flatten(" ").HasValue()) {
        arr.Add(DC.KSP.GetLines().Flatten(" ").ToString());
    }
    if (DC.MKT.GetLines().Flatten(" ").HasValue()) {
        arr.Add(DC.MKT.GetLines().Flatten(" ").ToString());
    }
    if (DC.WIB.GetLines().Flatten(" ").HasValue()) {
        arr.Add(DC.WIB.GetLines().Flatten(" ").ToString());
    }
    if (DC.FEAT.GetLines().Flatten(" ").HasValue()) {
        arr.Add(DC.FEAT.GetLines().Flatten(" ").ToString());
    }
    if (REQ.GetVariable("CNET_SD").HasValue()) {
        arr.Add(REQ.GetVariable("CNET_SD").ToString());
    }
    if (REQ.GetVariable("CNET_HL").HasValue()) {
        arr.Add(REQ.GetVariable("CNET_HL").ToString());
    }
    if (!(SKU.ProductLineName is null)) {
       arr.Add(SKU.ProductLineName.ToString());
    }
    if (!(SKU.ModelName is null)) {
       arr.Add(SKU.ModelName.ToString());
    }
    if (!(SKU.Description is null)) {
        arr.Add(SKU.Description.ToString());
    }
    // parcing every element in arr
    var result = new List<string>();

    foreach (var item in arr) {
        var checkAWG = item.Replace(" holes", "_hole")
            .Replace("-holes", "_hole")
            .Replace(" hole", "_hole")
            .Replace("-hole", "_hole");
        if (checkAWG.Contains("_hole")) {
            var splited = checkAWG.Split(" ");
            foreach (var subStr in splited) {
                if (subStr.Contains("_hole")) {
                    var checkNum = Coalesce(subStr);
                    if (checkNum.Replace("_hole", "").ExtractNumbers().Any()) {
                        result.Add(checkNum.Replace("_hole", ""));
                    }
                }
            }
        }
    }
    if (holePunnched.HasValue("%-hole punched%")) {
        var tmp = holePunnched.Where("%-hole punched%").First().Value().ExtractNumbers().First();
        Add($"NumberOfHoles⸮{tmp} holes paper");
    }
    else if (result.Count() > 0 && result.First().Equals("1")) {
        Add($"NumberOfHoles⸮{result.First()} hole paper");
    }
    else if (result.Count() > 0) {
        Add($"NumberOfHoles⸮{result.First()} holes paper");
    }
}

// "Bullet 3" "Sheet dimension"	

// Cnet Size for colored paper
string ColoredPaperCnetSize() {
    var result = "";
    var vortexSize = A[4765];
    var itemSize = R("SP-18229").HasValue() ? R("SP-18229").Text : R("cnet_common_SP-18229").HasValue() ? R("cnet_common_SP-18229").Text : "";
    if (!String.IsNullOrEmpty(itemSize) && !itemSize.ToLower().Equals("other")) {
        result = itemSize;
    }
    else if (vortexSize.HasValue()) {
        result = vortexSize.FirstValueUsm().Replace(" in", "\"");
    }
    if (!String.IsNullOrEmpty(result)) {
        return result;
    } else {
        return "";
    }
}

void ColoredPaperSheetDimentions() {
    var result = "";
    var size = Coalesce(ColoredPaperCnetSize());
    if (size.HasValue()) {
        result = $"Dimensions: {size.RegexReplace(@"(\d*"")(\sx\s)(\d*"")", "$1W$2$3L" )}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"ColoredPaperSheetDimentions⸮{result}");
    }
}

// "Bullet 4" "Paper Weight (lbs.)"	

void PaperWeight() {
    var result = "";
    var weight = R("SP-18379").HasValue() ? R("SP-18379").Text : R("cnet_common_SP-18379").HasValue() ? R("cnet_common_SP-18379").Text : "";
    if (!String.IsNullOrEmpty(weight)) {
       var units = "lbs.";
       if (weight.Equals("1")) {
           units = "lb."; 
       } else {
           result = $"Paper weight: {weight} {units}";
       }
    }
    if (!String.IsNullOrEmpty(result)) {
       Add($"PaperWeight⸮{result}");
    }
}


// "Bullet 5" "Paper color, type & brightness"	

void PaperColorTypeAndBrightness() {
    var result = "";
    var paperColorType = R("SP-350854").HasValue() ? R("SP-350854").Text : R("cnet_common_SP-350854").HasValue() ? R("cnet_common_SP-350854").Text : "";
    var paperBrightness = R("SP-18379").HasValue() ? R("SP-18379").Text : R("cnet_common_SP-18379").HasValue() ? R("cnet_common_SP-18379").Text : "";
    
    if (!String.IsNullOrEmpty(paperColorType) && paperColorType.ToLower().Equals("brights")
        && !String.IsNullOrEmpty(paperBrightness)) {
            result = $"Bright color with {paperBrightness} brightness";
    }
    else if (!String.IsNullOrEmpty(paperColorType) && paperColorType.ToLower().Equals("pastels")
        && !String.IsNullOrEmpty(paperBrightness)) {
            result = $"Pastel color with {paperBrightness} brightness";
    }
    else if (!String.IsNullOrEmpty(paperColorType) && paperColorType.ToLower().Equals("brights")) {
        result = "Bright-colored paper is ideal for direct mail, flyers and office or school projects";
    } 
    else if (!String.IsNullOrEmpty(paperColorType) && paperColorType.ToLower().Equals("brights")) {
        result = $"This pastel paper is ideal for prints with moderate solid areas, graphics and saturated colors";
    }
    else if (!String.IsNullOrEmpty(paperBrightness)) {
        result = "Brightness rating of {paperBrightness} for sharp, clear print results";
    }
    if (!String.IsNullOrEmpty(result)) {
       Add($"PaperColorTypeAndBrightness⸮{result}");
    }
}

// "Bullet 6" "True Color"	

// "Bullet 7" "Pack Size"	

// "Bullet 8" "Certifications & Standards"	

// "Bullet 9" "Recycled Content (%)"

// additional acid-free

// 