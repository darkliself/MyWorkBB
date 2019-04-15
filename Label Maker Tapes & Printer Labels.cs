// §§1681361910108140557 "Label Maker Tapes & Printer Labels" "Alex K."

// "Bullet 1" "Label maker tape width (Inches) & overall cartridge yield"
LabelMakerTapeWidth();
PrintTrueColorOnLabelTrueColor();
CompatibleDevices();
UseSurfacesApplicationsAdhesiveEtc();
// Pack Size
WaterResistant();
TearResistant();
ColdEnvironment();
OilResistance();

// --[FEATURE #1]
// --Flags or Tab type & Use
void LabelMakerTapeWidth() {
    var result = "";
    var size = A[4765];
    var isTape = A[4759];
    if (size.HasValue("%Roll%") 
        && size.HasValue("%m)%") 
        && isTape.HasValue("%tape%")
        && size.FirstValue().ExtractNumbers().Any()) {
            var tmp1 = Math.Round((double)size.FirstValue().ExtractNumbers().First() * 0.393701, 2);
            var tmp2 = Math.Round((double)size.FirstValue().ExtractNumbers().First() * 3.28084, 2);
            result = $"Create labels for home, work, or school with this {tmp1}\" wide and {tmp2}' yield label tape";
        }
    if(!String.IsNullOrEmpty(result)) {
        Add($"LabelMakerTapeWidth⸮{result}");
    }
}
// --[FEATURE #2]
// --True color (print True color on label True color) 

void  PrintTrueColorOnLabelTrueColor() {
    var result = "";
    // Label Printer Labels|Labeler Accessories|Label Maker Tapes
    var type =R("SP-21844").HasValue() ? R("SP-21844").Replace("<NULL>", "").Text :
		R("cnet_common_SP-21844").HasValue() ? R("cnet_common_SP-21844").Replace("<NULL>", "").Text : "";
    var trueColor = A[373];
    var colorFamily = R("SP-17441").HasValue() ? R("SP-17441").Replace("<NULL>", "").Text :
		R("cnet_common_SP-17441").HasValue() ? R("cnet_common_SP-17441").Replace("<NULL>", "").Text : ""; // Assorted

    if (!String.IsNullOrEmpty(type) && type.ToLower().Contains("labels") && trueColor.HasValue()) {
        if (trueColor.HasValue("black%on%white")) {
            result = "Black on white labels make text easy to read";
        }
        else if (trueColor.HasValue("white") && trueColor.HasValue("blue")) {
            result = "White labels with blue print make text easy to read";
        }
        else if ((trueColor.HasValue("%clear%") || trueColor.HasValue("%transparent%"))
        && trueColor.Values.Count() > 1) {
            var tmp = trueColor.WhereNot("%transparent%", "%clear%").First().Value().ToLower().ToUpperFirstChar();
            result = $"{tmp} print on clear label for legibility";
        }
         else if (trueColor.HasValue("% on %") && !String.IsNullOrEmpty(colorFamily) && !colorFamily.ToLower().Equals("assorted")) {
            var tmp = trueColor.Where("% on %").First().Value().ToLower().ToUpperFirstChar();
            result = $"{tmp} print label for legibility";
        }
        else if (trueColor.HasValue() && !String.IsNullOrEmpty(colorFamily) && !colorFamily.ToLower().Equals("assorted")) {
            result = $"Comes in {trueColor.Values.Flatten("/")}";
        }
        else if (trueColor.HasValue() && String.IsNullOrEmpty(colorFamily)) {
            result = $"Comes in {trueColor.Values.Flatten("/")}";
        }
    }
    else if (!String.IsNullOrEmpty(type) && type.ToLower().Contains("tape") && trueColor.HasValue()) {
        if (trueColor.HasValue("black%on%white")) {
            result = "Black print on a white tape creates an easy-to-read text";
        }
        else if (trueColor.HasValue("black") && trueColor.HasValue("silver")) {
            result = "Black print on a silver tape creates an easy-to-read text";
        }
        else if (trueColor.HasValue("black%on%blue")) {
            result = "Black print on a blue tape ensures high visibility on light-colored surfaces";
        }
        else if (trueColor.HasValue("black%on%yellow")) {
            result = "Black print on yellow tape for easy identification";
        }
        else if ((trueColor.HasValue("%clear%") || trueColor.HasValue("%transparent%"))
        && trueColor.Values.Count() > 1) {
            var tmp = trueColor.WhereNot("%transparent%", "%clear%").First().Value().ToLower().ToUpperFirstChar();
            result = $"{tmp} print on clear tape for legibility";
        }
        else if (trueColor.HasValue("% on %") && !String.IsNullOrEmpty(colorFamily) && !colorFamily.ToLower().Equals("assorted")) {
            var tmp = trueColor.Where("% on %").First().Value().ToLower().ToUpperFirstChar();
            result = $"{tmp} tape for legibility";
        }
        else if (trueColor.HasValue() && !String.IsNullOrEmpty(colorFamily) && !colorFamily.ToLower().Equals("assorted")) {
            result = $"Comes in {trueColor.Values.Flatten("/")}";
        }
        else if (trueColor.HasValue() && String.IsNullOrEmpty(colorFamily)) {
            result = $"Comes in {trueColor.Values.Flatten("/")}";
        }
    }
    else if (!String.IsNullOrEmpty(type) && trueColor.HasValue()) {
        if (trueColor.HasValue() && !String.IsNullOrEmpty(colorFamily) && !colorFamily.ToLower().Equals("assorted")) {
            result = $"Comes in {trueColor.Values.Flatten("/")}";
        }
        else if (trueColor.HasValue() && String.IsNullOrEmpty(colorFamily)) {
            result = $"Comes in {trueColor.Values.Flatten("/")}";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"PrintTrueColorOnLabelTrueColor⸮{result}");
    }
}
 
// --[FEATURE #3]
// --Compatible devices; if more than 3 lines: list top 5 and refer to Extended description for more  

void CompatibleDevices() {
    var result = "";
    var cp = Coalesce(RP.GetCiEs(200), RP.GetCiMs(200));
    if (cp.HasValue()) {
        result = $"Compatible with: {cp.ToString().Split(", ").Take(5).Flatten(", ")}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"CompatibleDevices⸮{result}");
    }
}

// --[FEATURE #4]
// --Use; surfaces, applications, adhesive, etc.   

void UseSurfacesApplicationsAdhesiveEtc() {
    var result = "";
    var termal = A[4763];
    var laminated = A[6073];
    var adhesive = A[10619];
 
    if (termal.HasValue("direct thermal")) {
        result = "Uses direct thermal printing technology for mess-free printing without toner or ink";
    }
    else if (termal.HasValue("%thermal%")) {
        result = "Thermal print technology requires no ink or toner";
    }
    else if (laminated.HasValue("%laminated%")) {
        result = "Features exclusive lamination for added protection";
    }
    else if (adhesive.HasValue()) {
        result = "Adhesive backing for quick application";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"UseSurfacesApplicationsAdhesiveEtc⸮{result}");
    }
}

// --[FEATURE #5]
// --Label maker pack size

// --[FEATURE #6]
// --additional water

void WaterResistant() {
    var result = "";
    var waterКesistant = A[4783];
    if (waterКesistant.HasValue("%water%")) {
        result = "Water-resistant for durability";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"UseSurfacesApplicationsAdhesiveEtc⸮{result}");
    }
}

// --[FEATURE #7]
// --additional tear resistant

void TearResistant() {
    var result = "";
    var waterКesistant = A[4783];
    if (waterКesistant.HasValue("%tear%")) {
        result = "Tear-resistant for absolute safe keeping";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"TearResistant⸮{result}");
    }
}

// --[FEATURE #8]
// --additional cold environment

void ColdEnvironment() {
    var result = "";
    var waterКesistant = A[4783];
    if (waterКesistant.HasValue("%cold%")) {
        result = "Can withstand cold environments";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"ColdEnvironment⸮{result}");
    }
}

// --[FEATURE #9]
// --additional oil resistance

void OilResistance() {
    var result = "";
    var waterКesistant = A[4783];
    if (waterКesistant.HasValue("%oil%")) {
        result = "Has great resistance against oil";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"OilResistance⸮{result}");
    }
}

// §§1681361910108140557 end of "Label Maker Tapes & Printer Labels"