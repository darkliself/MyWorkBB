//§§1114953110001 "Pens" "Alex K." 

PenType();
PenInkTrueColor();
PenPointTypeAndSize();
PenBarrelColorAndBarrelMaterial();
//Pack size
PenGrip();
PenPocketClip();
PenRefillable();
BarrelShape();
InkLevelViewingWindow();
ArchivalAcidFree();
UniSuperInk();
UniFlowSystem();
LiquidInkSystem();
// "Pen Type" "Bullet 1" 
void PenType() {
    var result = "";
    var penTypeVal = R("SP-18609").HasValue() ? R("SP-18609").Replace("<NULL>", "").Text : R("cnet_common_SP-18609").HasValue() ? R("cnet_common_SP-18609").Replace("<NULL>", "").Text : "";
    var penPackSize = R("SP-18666").HasValue() ? R("SP-18666").Replace("<NULL>", "").Text : R("cnet_common_SP-18666").HasValue() ? R("cnet_common_SP-18666").Replace("<NULL>", "").Text : "";
    var isPluralPen = "pens";
    var isAre = "are";
    if (!String.IsNullOrEmpty(penTypeVal)) {

        if (!String.IsNullOrEmpty(penPackSize) && penPackSize.Equals("1")) {
            isPluralPen = "pen";
            isAre = "is";
        }
        if (penTypeVal.ToLower().Contains("erasable")) {
            result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen} allows you to write, erase and rewrite with no wear-and-tear";
        }
        else if (penTypeVal.ToLower().Equals("retractable gel") ) {
            result = $"Retractable gel {isPluralPen} for everyday writing tasks";
        }
        else if (penTypeVal.ToLower().Contains("retractable")) {
            result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen} {isAre} ready to write with just a click";
        }
        else if (penTypeVal.ToLower().Contains("ballpoint")) {
            result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen} for clear, consistent writing";
        }
        else if (penTypeVal.ToLower().Contains("ballpoint")) {
            result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen} for clear, consistent writing";
        }
        else if (penTypeVal.ToLower().Contains("fountain")) {
            result = $"The everyday {penTypeVal} {isPluralPen} for smooth, expressive writing";
        }
        else if (penTypeVal.ToLower().Contains("gel")) {
            result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen} for any task";
        }
        else if (penTypeVal.ToLower().Contains("erasable")) {
            result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen} allow you to write, erase and rewrite with no wear-and-tear";
        }
        else if (penTypeVal.ToLower().Equals("retractable ballpoint")) {
            result = $"Retractable ballpoint {isPluralPen} for ease of use";
        }
        else if (penTypeVal.ToLower().Contains("retractable")) {
            result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen} {isAre} ready to write with just a click";
        }
        else if (penTypeVal.ToLower().Equals("felt")) {
            result = $"Felt tip draws bold and expressive lines";
        }
        else if (penTypeVal.ToLower().Contains("ballpoint")) {
            result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen} for clear, consistent writing";
        }
        else if (penTypeVal.ToLower().Contains("fountain")) {
            result = $"The everyday {penTypeVal.ToLower()} {isPluralPen} for smooth, expressive writing";
        }
        else if (penTypeVal.ToLower().Contains("rollerball")) {
            result = $"Rollerball design delivers a bold ink laydown";
        }
        else if (penTypeVal.ToLower().Contains("gel")) {
            result = $"{penTypeVal.ToLower().ToUpperFirstChar()} ink {isPluralPen} for any task";
        }
        else if (penTypeVal.ToLower().Contains("counter top")) {
            result = $"Counter top {isPluralPen} {isAre} always handy for your customers";
        }
        else if (!String.IsNullOrEmpty(penTypeVal)) {
            result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen}";
        }
    }
    if (!String.IsNullOrEmpty(result)) { 
        Add($"PenType⸮{result}"); 
    }
}

// "Bullet 2"  "Pen Ink True color" 
void PenInkTrueColor() {
    var penInkTrueColor = "";
    var trueColor = R("SP-22967").HasValue() ? R("SP-22967").Replace("<NULL>", "").Text :
		R("cnet_common_SP-22967").HasValue() ? R("cnet_common_SP-22967").Replace("<NULL>", "").Text : "";

    if (!String.IsNullOrEmpty(trueColor)) {
        switch (trueColor) {
            case var a when a.ToLower().Contains("assorted"):
                penInkTrueColor = ($"{trueColor} colors for a variety of tasks");
                break;
            case var a when a.ToLower().Contains("black"):
                penInkTrueColor = ($"Black ink has a rich color for excellent readability");
                break;
            case var a when a.ToLower().StartsWith("blue"):
            case var b when b.ToLower().Equals("navy"):
                penInkTrueColor = ($"{trueColor[0].ToString().ToUpper()}{trueColor.Substring(1).ToLower()} ink is easy to read");
                break;
            case var a when a.ToLower().EndsWith("brown"):
                penInkTrueColor = ($"The color {trueColor.ToLower()} brings a sense of stability and comfort");
                break;
            case var a when a.ToLower().EndsWith("clear"):
                break;
            case var a when a.ToLower().StartsWith("silver"):
            case var b when b.ToLower().Contains("grey"):
            case var c when c.ToLower().Contains("gray"):
                penInkTrueColor = ($"The {trueColor.ToLower()} color is great for a professional-looking writing utensil that gets the job done");
                break;
            case var a when a.ToLower().EndsWith("clear"):
                break;
            case var a when !(String.IsNullOrEmpty(a)):
                penInkTrueColor = ($"{trueColor[0].ToString().ToUpper()}{trueColor.Substring(1).ToLower()} ink for bright and clear writing");
                break;
        }
    if (!String.IsNullOrEmpty(penInkTrueColor)) { Add($"PenInkTrueColor⸮{penInkTrueColor}");} 
    }
}

//"Bullet 3" "Pen Point Type & Size" 
void PenPointTypeAndSize() {
    var result = "";
    var penPointType = R("SP-18608").HasValue() ? R("SP-18608").Replace("<NULL>", "").Text :
        R("cnet_common_SP-18608").HasValue() ? R("cnet_common_SP-18608").Replace("<NULL>", "").Text : "";

    var penPointSize = R("SP-16585").HasValue() ? R("SP-16585").Replace("<NULL>", "").Text :
        R("cnet_common_SP-16585").HasValue() ? R("cnet_common_SP-16585").Replace("<NULL>", "").Text : "";

    var pointTypeIsNotNull = !String.IsNullOrEmpty(penPointType);
    var pointSizeIsNotNull = !String.IsNullOrEmpty(penPointSize);

    if (pointTypeIsNotNull 
        && penPointType.ToLower().Equals("micro point")
        && pointSizeIsNotNull) {
            result = $"{penPointSize} microo-point tip delivers crisp, precise lines";
    }
        else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("ultra micro")
        && pointSizeIsNotNull) {
            result = $"{penPointType} {penPointSize} is highly precise for crisp writing";
    }
        else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("micro")
        && pointSizeIsNotNull) {
            result = $"{penPointSize} {penPointType} tip delivers crisp, precise line";
    }
    else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("micro")) {
            result = "Micro-point tip delivers crisp, precise lines";
    }
    else if (pointTypeIsNotNull
        && (
            penPointType.ToLower().Contains("medium") 
            || penPointType.ToLower().Contains("standard"))
        ) {
            result = $"{penPointType} tip";
    }
    else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("fine")
        && pointSizeIsNotNull) {
            var tmp = penPointType.ToLower().Equals("ultra fine") ? "Ultra-fine" : penPointType.ToLower().ToUpperFirstChar();
            result = $"{tmp} {penPointSize.ToLower().ToUpperFirstChar()} tip ensures crisp lines with every use";
    }
        else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("broad")
        && pointSizeIsNotNull) {
            result = $"{penPointType.ToLower().ToUpperFirstChar()} {penPointSize} point provides easy readability";
    }
        else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("broad")) {
            result = $"{penPointType.ToLower().ToUpperFirstChar()} point provides easy readability";
    }
    else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("bold")
        && pointSizeIsNotNull) {
            result = $"{penPointSize} {penPointType.ToLower()} point provides easy readability";
    }
        else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("tip")
        && pointSizeIsNotNull) {
            result = $"{penPointSize} {penPointType.ToLower()} delivers crisp, precise lines";
    }
    else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("tip")) {
            result = $"{penPointType.ToLower().ToUpperFirstChar()} delivers crisp, precise lines";
    }
    else if (pointTypeIsNotNull && pointSizeIsNotNull) {
        result = $"{penPointSize} {penPointType.ToLower()} tip delivers crisp, precise lines";
    }
    else if (pointTypeIsNotNull || pointSizeIsNotNull) {
        var tmp = pointTypeIsNotNull ? penPointType : penPointSize;
        result =$"{tmp.ToLower().ToUpperFirstChar()} tip delivers crisp, precise lines";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"PenPointTypeAndSize⸮{result}");
    }
}

    // "Bullet 4" "Pen Barrel Color & Barrel Material" 
void PenBarrelColorAndBarrelMaterial () {
    var penBarrelColorAndBarrelMaterial = "";
    var penBarrelColor = R("SP-18606").HasValue() ? R("SP-18606").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18606").HasValue() ? R("cnet_common_SP-18606").Replace("<NULL>", "").Text : "";
    var barrelMaterial = R("SP-4882").HasValue() ? R("SP-4882").Replace("<NULL>", "").Text :
		R("cnet_common_SP-4882").HasValue() ? R("cnet_common_SP-4882").Replace("<NULL>", "").Text : "";
    var colorIsNotNull = !String.IsNullOrEmpty(penBarrelColor);
    var materialIsNotNull = !String.IsNullOrEmpty(barrelMaterial);
    if (colorIsNotNull && (penBarrelColor.ToLower().Contains("translucent")
            || penBarrelColor.ToLower().Contains("transparent")
            || penBarrelColor.ToLower().Equals("clear"))
        ) {
            var tmp = materialIsNotNull ? $"{penBarrelColor} {barrelMaterial}" : penBarrelColor;
            penBarrelColorAndBarrelMaterial = $"{tmp} barrels";
    }
    else if (colorIsNotNull && penBarrelColor.ToLower().Equals("assorted")) {
        var tmp = materialIsNotNull ? $"{penBarrelColor} {barrelMaterial}" : penBarrelColor;
        penBarrelColorAndBarrelMaterial = $"{tmp} barrels";
    } else if (colorIsNotNull) {
        var tmp = materialIsNotNull ? $"{penBarrelColor} {barrelMaterial}" : penBarrelColor;
        penBarrelColorAndBarrelMaterial =  $"{tmp} barrel";
    }
    if (!String.IsNullOrEmpty(penBarrelColorAndBarrelMaterial)) {
            Add($"PenBarrelColorAndBarrelMaterial⸮{penBarrelColorAndBarrelMaterial}");
    }
 }
 // Bullet 5 "Pack size" (if more then one)

void PenGrip() {
    var penGrip = R("SP-350752").HasValue() ? R("SP-350752").Replace("<NULL>", "").Text :
		R("cnet_common_SP-350752").HasValue() ? R("cnet_common_SP-350752").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(penGrip) && penGrip.ToLower().Equals("yes")) {
        Add("AdditionalPenGrip⸮Comfort grip for superior control");
    }
}

void PenPocketClip() {
    var penClip = R("SP-4878").HasValue() ? R("SP-4878").Replace("<NULL>", "").Text :
		R("cnet_common_SP-4878").HasValue() ? R("cnet_common_SP-4878").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(penClip) && penClip.ToLower().Equals("yes")) {
        Add("AdditionalPenClip⸮Features pocket clip for convenient carrying");
    }
}

void PenRefillable() {
    var penRefillable = R("SP-12520").HasValue() ? R("SP-12520").Replace("<NULL>", "").Text :
		R("cnet_common_SP-12520").HasValue() ? R("cnet_common_SP-12520").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(penRefillable) && penRefillable.ToLower().Equals("yes")) {
        Add("AdditionalPenRefillable⸮Refillable ink allows pens to be reused instead of being replaced to save money");
    }
}

void BarrelShape() {
    var resBarrelShape = "";
    var barrelShape = R("SP-350746").HasValue() ? R("SP-350746").Replace("<NULL>", "").Text :
		R("cnet_common_SP-350746").HasValue() ? R("cnet_common_SP-350746").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(barrelShape) && barrelShape.ToLower().Equals("hexagonal")) {
        resBarrelShape = "Features roll-proof, hexagon-shaped barrel, so it would not roll off surfaces";
    }
    else if (!String.IsNullOrEmpty(barrelShape) && barrelShape.ToLower().Equals("triangular")) {
        resBarrelShape = "Ergonomic triangular shape";
    }
    else if (!String.IsNullOrEmpty(barrelShape) && barrelShape.ToLower().Equals("hourglass")) {
        resBarrelShape = "Unique hourglass shape features a comfortable grip for a natural fit in your hand";
    }
    else if (!String.IsNullOrEmpty(barrelShape) && barrelShape.ToLower().Equals("round")) {
        resBarrelShape = "Comfortable, round barrel design";
    }
    else if (!String.IsNullOrEmpty(barrelShape)) {
        resBarrelShape = $"{barrelShape} barrel design";
    }
    if (!String.IsNullOrEmpty(resBarrelShape)) {
        Add($"AdditionalPenBarrelShape⸮{resBarrelShape}");
    }
}

void InkLevelViewingWindow() {
    var window =  Coalesce(A[5996].HasValue("ink level viewing window"));  
    var barrelColor = R("SP-18606").HasValue() ? R("SP-18606").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18606").HasValue() ? R("cnet_common_SP-18606").Replace("<NULL>", "").Text : "";
    var clearColor = Coalesce(barrelColor).ToLower().In("%translucent%", "%transparent%");
    if (window) {
        var tmp = clearColor ? "Barrel windows reveal the amount of remaining ink" : "Visible ink supply, so you never run out unexpectedly";
        Add($"AdditionalPenInkLevelViewingWindow⸮{tmp}");
    }
}

void ArchivalAcidFree() {
    var tmp = Coalesce(A[5996].Values);
    if (!String.IsNullOrEmpty(tmp.ToString()) && Coalesce(A[5996].Values.Flatten()).In("archival quality") && Coalesce(A[5996].Values.Flatten()).In("acid-free")) {
        Add("AdditionalArchivalAcidFree⸮Archival quality, acid-free ink");
    }
}

void UniSuperInk() {
    var superInk = Coalesce(A[5996].HasValue("uni Super Ink"));
    if (superInk) {
        Add("AdditionalUniSuperInk⸮Features uni-Super Ink that helps prevent against check fraud and document alteration");
    }
}

void UniFlowSystem() {
    var superInk = Coalesce(A[5996].HasValue("UNI-FLOW SYSTEM"));
    if (superInk) {
        Add("AdditionalUniFlowSystem⸮Uni-flow ink system provides a consistent ink flow and clean lines");
    }
}

void LiquidInkSystem() {
    var superInk = Coalesce(A[5996].HasValue("liquid ink system"));
    if (superInk) {
        Add("AdditionalLiquidInkSystem⸮Unique liquid ink system provides even flow and smooth marking");
    }
}

//1114953110001 end of  "Pens" §§