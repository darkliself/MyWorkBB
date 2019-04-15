//§§168136191010890400 "Label Makers" "Alex K."

LabelMakerTypeAndUse();
LabelMakerMaxPrintSpeed();
LabelMakerMaximumResolution();
LabelMakerPrintingTechnology();
MediaSupportedTypeOfLabels();
InterfaceOrPortType();
ScreenSizeInInches();
OutputTypeFontSizeStyles();
LabelMakerConnectionType();
// Dimentions
LabelMakerBatterySize();
// Warranty

// --[FEATURE #1]
// --Label maker type & Use
void LabelMakerTypeAndUse() {
    var result = "";
    //Printhead|Cleaning Card|Label Maker|Charger|Label Printer
    var type = R("SP-21843").HasValue() ? R("SP-21843").Replace("<NULL>", "").Text :
		R("cnet_common_SP-21843").HasValue() ? R("cnet_common_SP-21843").Replace("<NULL>", "").Text : "";

    
    if (!String.IsNullOrEmpty(type)) {
        if (type.ToLower().Equals("label maker")) {
            result = "Label maker for quick organization of items and documents";
        }
        else if (type.ToLower().Equals("label printer")) {
            result = "Label printer for customized creation and printing of labels";
        }
        //https://www.staples.com/Brother-AD24-Label-Maker-Power-Adapter/product_407288
        //https://www.staples.com/rollo-shipping-label-printer-direct-thermal-4-6-printer-compare-to-dymo-4xl/product_24307174
        else {
            result = $"{type.ToLower().ToUpperFirstChar()} for label makers";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"LabelMakerTypeAndUse⸮{result}");    
    }
}

// --[FEATURE #2]
// --Max print speed
void  LabelMakerMaxPrintSpeed() {
    var result = "";
    var printSpeed = R("SP-21839").HasValue() ? R("SP-21839").Replace("<NULL>", "").Text :
		R("cnet_common_SP-21839").HasValue() ? R("cnet_common_SP-21839").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(printSpeed)) {
        result = $"Maximum print speed up to {printSpeed} ips for enhanced productivity";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"LabelMakerMaxPrintSpeed⸮{result}");    
    }
}

// --[FEATURE #3]
// --Maximum resolution
void  LabelMakerMaximumResolution() {
    var result = "";
    var maximumResolution = R("SP-25").HasValue() ? R("SP-25").Replace("<NULL>", "").Text :	R("cnet_common_SP-25").HasValue() ? R("cnet_common_SP-25").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(maximumResolution)) {
        result = $"Offers printing with {maximumResolution.ToLower().Replace(" dpi", "dpi")} maximum resolution";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"LabelMakerMaximumResolution⸮{result}");    
    }
}


// --[FEATURE #4]
// --Printing technology
void  LabelMakerPrintingTechnology() {
    var result = "";
    var printingTechnology = R("SP-22121").HasValue() ? R("SP-22121").Replace("<NULL>", "").Text :	R("cnet_common_SP-22121").HasValue() ? R("cnet_common_SP-22121").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(printingTechnology)) {
        if (printingTechnology.ToLower().Equals("thermal transfer")) {
            result = "Thermal transfer printing technology eliminates the need for ink";
        }
        else if (printingTechnology.ToLower().Equals("direct thermal")) {
            result = "Direct thermal print is not affected by sunlight or friction";
        }
        else {
            result = $"{printingTechnology.ToLower().ToUpperFirstChar()} technology prints professional-quality labels";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"LabelMakerPrintingTechnology⸮{result}");    
    }
}
// --[FEATURE #5]
// --Media supported/Type of labels (Type & Size)
void  MediaSupportedTypeOfLabels() {
    var result = "";
    var compatibleTapes = A[2942];
    var maximumLabelWidth = R("SP-22123").HasValue() ? R("SP-22123").Replace("<NULL>", "").Text :
		R("cnet_common_SP-22123").HasValue() ? R("cnet_common_SP-22123").Replace("<NULL>", "").Text : "";

    var maximumLabelLength = R("SP-22122").HasValue() ? R("SP-22122").Replace("<NULL>", "").Text :
		R("cnet_common_SP-22122").HasValue() ? R("cnet_common_SP-22122").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(maximumLabelWidth) && !String.IsNullOrEmpty(maximumLabelLength)  && compatibleTapes.HasValue()) {
        if (compatibleTapes.Values.Count() > 1) {
            result = $"Uses {compatibleTapes.Values.Select(o => o.Value().Replace(" tape", "")).FlattenWithAnd()} tapes up to {maximumLabelWidth}\" wide and {maximumLabelLength}\" long";
        }
        else if (compatibleTapes.Values.Count() == 1) {
             result = $"Uses {compatibleTapes.FirstValue().Replace(" tape", "")} tape up to {maximumLabelWidth}\" wide and {maximumLabelLength}\" long";
        }
    } 
    else if (!String.IsNullOrEmpty(maximumLabelWidth) && compatibleTapes.HasValue())  {
        if (compatibleTapes.Values.Count() > 1) {
            result = $"Uses {compatibleTapes.Values.Select(o => o.Value().Replace(" tape", "")).FlattenWithAnd()} tapes up to {maximumLabelWidth}\" wide";    
        }
        else if (compatibleTapes.Values.Count() == 1) {
             result = $"Uses {compatibleTapes.FirstValue().Replace(" tape", "")} tape up to {maximumLabelWidth}\" wide";
        }
    }
    else if (!String.IsNullOrEmpty(maximumLabelLength) && compatibleTapes.HasValue()) {
        if (compatibleTapes.Values.Count() > 1) {
            result = $"Uses {compatibleTapes.Values.Select(o => o.Value().Replace(" tape", "")).FlattenWithAnd()} tapes up to {maximumLabelLength}\" long";
        }
        else if (compatibleTapes.Values.Count() == 1) {
             result = $"Uses {compatibleTapes.FirstValue().Replace(" tape", "")} tape up to {maximumLabelLength}\" long";
        }
    }
    else if (compatibleTapes.HasValue()) {
         if (compatibleTapes.Values.Count() > 1) {
            result = $"Uses {compatibleTapes.Values.Select(o => o.Value().Replace(" tape", "")).FlattenWithAnd()} tapes";
        }
        else if (compatibleTapes.Values.Count() == 1) {
            result = $"Uses {compatibleTapes.FirstValue().Replace(" tape", "")} tape";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"MediaSupportedTypeOfLabels⸮{result}");    
    }
}

// --[FEATURE #6]
// --Interface or port type (If Applicable)
void  InterfaceOrPortType() {
    var result = "";
    var interfaceOrPortType = R("SP-21791").HasValue() ? R("SP-21791").Replace("<NULL>", "").Text :
		R("cnet_common_SP-21791").HasValue() ? R("cnet_common_SP-21791").Replace("<NULL>", "").Text : "";

    if (!String.IsNullOrEmpty(interfaceOrPortType) && !interfaceOrPortType.ToLower().Contains("none")) {
        result = $"{interfaceOrPortType.Split(", ").Select(o => "##"+ o.Replace("Wi-Fi(n)", "Wi-Fi (n)")).FlattenWithAnd(10, ", ")} for easy ##and flexible connectivity";
    }
    if (!String.IsNullOrEmpty(result)) {
        var tmp = $"InterfaceOrPortType⸮{result}";
        result = tmp.Split("⸮##").Last()[0].ToString().ToUpper() + tmp.Split("⸮##").Last().Substring(1);
        Add($"InterfaceOrPortType⸮{result}");
    }
}

// --[FEATURE #7]
// --Screen size in Inches
void  ScreenSizeInInches() {
    var result = "";
    var vortexScreenLines = A[2950];
    var screenSize = R("SP-21574").HasValue() ? R("SP-21574").Replace("<NULL>", "").Text :
		R("cnet_common_SP-21574").HasValue() ? R("cnet_common_SP-21574").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(screenSize)) {
        result = $"Features {screenSize}\" display helps to view the images on screen";
    }
    else if (vortexScreenLines.HasValue()) {
        if (vortexScreenLines.HasValue("1 line x % characters")) {
            result = "Screen displays up to 12 characters to view text before printing";
        }
        else if (vortexScreenLines.HasValue("% line x % characters")) {
            var tmpLast = vortexScreenLines.FirstValue().ToString().Split(" x ").Last().Replace(" ", "-").Replace("characters", "character");
            var tmpFirst = vortexScreenLines.FirstValue().ToString().Split(" x ").First().Replace(" ", "-").Replace("lines", "line");
            result = $"{tmpLast}, {tmpFirst} graphical display to view text before printing";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"ScreenSizeInInches⸮{result}");
    }
}
// --[FEATURE #8]
// --Output Type (font sizes/styles, templates/columns, symbols and clip-art);
void  OutputTypeFontSizeStyles() {
    var result = "";
    var fonts = A[2944];
    var characterSizes = A[2945];
    var stylesEffects = A[2946];
    if (fonts.HasValue() || characterSizes.HasValue() || stylesEffects.HasValue()) {
        //_A[2944].Values.Count.Postfix(" fonts").Prefix(" ").Replace(" 1 fonts", " one font")
        result = "Choose from";
        if (fonts.HasValue()) {
            var numOfFonts = "";
            if (fonts.Values.ExtractNumbers().Any() 
            && !fonts.HasValue("Arial 65") 
            && !fonts.HasValue("Monospace 821") 
            && !fonts.HasValue("Chinese Simplified (SimSun GB18030)") 
            && !fonts.HasValue("PT Dingbats 2") && !fonts.HasValue("Swiss 721%") 
            && !fonts.HasValue("Traditional Chinese (Big5)")) {
                numOfFonts = fonts.Values.ExtractNumbers().First().ToString();
            }
            else {
                numOfFonts = fonts.Values.Count().ToString();
            }
             result = result + " " + (numOfFonts.Equals("1") ? "one font" : (numOfFonts + " fonts"));
        }
        if (characterSizes.HasValue()) {
            //A[2945].Values.Count.Prefix(", ").Postfix(" font sizes").Erase(", 0 font sizes").Replace(" 1 font sizes", " one font size")
            var separator = result.Length > 11 ? ", " : " ";
            result =(result + separator + characterSizes.Values.Count() + " font sizes").Replace(" 1 font sizes",  " one font size");
        }
        if (stylesEffects.HasValue()) {
            //_A[2946].Values.Count.Prefix(" and ").Postfix(" font styles").Erase("and 0 font styles").Replace(" 1 font styles", " one font style")
            var separator = result.Length > 11 ? ", " : " ";
            result =(result + separator + stylesEffects.Values.Count() + " font styles").Replace(" 1 font styles", " one font style");
        }
        result = result + " for user convenience";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"OutputTypeFontSizeStyles⸮{result}");
    }
}

// --[FEATURE #9]
// --Label maker connection type
void  LabelMakerConnectionType() {
    var result = "";
    //None|Bluetooth|Wireless|USB, Wireless, Ethernet|USB, Ethernet, Serial|USB & Ethernet|USB, Serial|USB & Wireless|USB, Wireless, Ethernet, Serial|USB, Serial, Parallel|USB|USB, Wireless, Ethernet, Serial, Bluetooth
    var connectionType = R("SP-18625").HasValue() ? R("SP-18625").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18625").HasValue() ? R("cnet_common_SP-18625").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(connectionType) && !connectionType.ToLower().Equals("none")) {
        if (connectionType.ToLower().Equals("usb")) {
            result = "Connects easily with a USB cable";
        } else {
            var isPluralInteface = "";
            if (connectionType.Split(", ").Count() > 1) {
                isPluralInteface = "intefaces";
            } else {
                isPluralInteface = "inteface";
            }
            result = $"Connects easily with {connectionType} {isPluralInteface}";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"LabelMakerConnectionType⸮{result}");
    }
}

// --[FEATURE #10]
// --Dimensions

// --[FEATURE #11]
// --Battery size
void  LabelMakerBatterySize() {
    var result = "";
    //Lithium Ion|AAAA|AAA|AA|C|223|D|123|9V|CR2|3V|6LF22|6V|1.5V|12V|3.7V|CR17345|2CR5|CRV3|N|CR2016|AAA/AA|CR2032
    var  batterySize = R("SP-18114").HasValue() ? R("SP-18114").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18114").HasValue() ? R("cnet_common_SP-18114").Replace("<NULL>", "").Text : "";
		
	var count = A[861];
    if (!String.IsNullOrEmpty(batterySize) && !batterySize.ToLower().Equals("none")) {
        if (count.HasValue() && count.Values.ExtractNumbers().Any() && count.Values.ExtractNumbers().First() > 1) {
           result = $"Runs on ##{count.FirstValue()} x {batterySize} batteries";
        }
        else {
            result = $"Runs on {batterySize} battery";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"LabelMakerBatterySize⸮{result}");
    }
}
// --[FEATURE #12]
// --Warranty

//§§168136191010890400 end of "Label Makers"
