//§§16831375514580302 "Batteries" "Alex K."

BatterySize();
BatteryType();
Rechargeable();
BatteryCapacity();
BatteryUse();
AdditionalHiDensity();
AdditionalDuralockPowerPreserve();
AdditionalM3Technology();

// --[FEATURE #1]
// --Battery size
void BatterySize() {
    var result = "";
    //Lithium Ion|AAAA|AAA|AA|C|223|D|123|9V|CR2|3V|6LF22|6V|1.5V|12V|3.7V|CR17345|2CR5|CRV3|N|CR2016|AAA/AA|CR2032
    var batterySize = R("SP-18114").HasValue() ? R("SP-18114").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18114").HasValue() ? R("cnet_common_SP-18114").Replace("<NULL>", "").Text : "";

    if (!String.IsNullOrEmpty(batterySize)) {
        if (batterySize.ToLower().Equals("aaa")) {
            result = "AAA batteries power a variety of handheld electronics, gadgets and alarms";
        }

        else if (batterySize.ToLower().Equals("aa")) {
            result = "AA batteries give your devices the consistent power they need to work at their best";
        }
        else if (batterySize.ToLower().Equals("c")) {
            result = "C batteries provide reliable, convenient and long-lasting power for your electronic devices";
        }
        else if (batterySize.ToLower().Equals("d")) {
            result = "D batteries easily power your small electronic devices";
        }
        else if (batterySize.ToLower().Equals("9v")) {
            result = "9V batteries are the perfect choice when you need to operate with essential electronic items";
        }
        else if (batterySize.ToLower().Equals("9v")) {
            result = "9V batteries are the perfect choice when you need to operate with essential electronic items";
        }
        else {
            result = $"{batterySize} batteries for everyday use in devices";
        }
    }
  
    if (!String.IsNullOrEmpty(result)) {
        Add($"BatterySize⸮{result}");
    }
}

// --[FEATURE #2]
// --Battery Type
void BatteryType() {
    var result = "";
    //Lithium|NiMH|Lithium Polymer|Alkaline|Silver Oxide|Charger
    var type = R("SP-18115").HasValue() ? R("SP-18115").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18115").HasValue() ? R("cnet_common_SP-18115").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(type)) {
        if (type.ToLower().Equals("alkaline")) {
            result = "The alkaline battery technology is designed to release the right amount of power when needed";
        }
        else if (type.ToLower().Contains("lithium")) {
            result = $"{type.ToLower().ToUpperFirstChar()} technology for ultimate power";
        }
        else if (type.ToLower().Equals("silver oxide")) {
            result = "Silver oxide batteries provide reliable, long-lasting power for your electronic devices";
        }
        else if (type.ToLower().Equals("nimh")) {
            result = "NiMH batteries provide a reusable power source for frequently used and high-drain devices";
        }
        else if (type.ToLower().Equals("charger")) {
            result =  $"{type.ToLower().ToUpperFirstChar()} technology for ultimate power";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"BatteryType⸮{result}");
    }
}
// --[FEATURE #3]
// --Rechargeable
void Rechargeable() {
    var result = "";
    var type = R("SP-22066").HasValue() ? R("SP-22066").Replace("<NULL>", "").Text :
		R("cnet_common_SP-22066").HasValue() ? R("cnet_common_SP-22066").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(type)) {
        if (type.ToLower().Equals("rechargeable")) {
            result = "Battery can be charged again and again to keep your device powered while you're on-the-go";
        }
        else if (type.ToLower().Equals("not rechargeable")) {
            result = $"Not rechargeable";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"Rechargeable⸮{result}");
    }
}

// --[FEATURE #4]
// --Capacity (mAh)
void BatteryCapacity() {
    var result = "";
    var capacity = R("SP-21541").HasValue() ? R("SP-21541").Replace("<NULL>", "").Text :
		R("cnet_common_SP-21541").HasValue() ? R("cnet_common_SP-21541").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(capacity)) {
        result = $"{capacity} capacity";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"BatteryCapacity⸮{result}");
    }
}

// --[FEATURE #5]
// --Battery general use
void BatteryUse() {
    var result = "";
    //General-Purpose Use|Car Security, Organizers and glucometers|Toys, Flashlights, remote controls and fire alarms|Portable CD players, Shavers, Radios and Smoke alarms|Headset
    var use = R("SP-24312").HasValue() ? R("SP-24312").Replace("<NULL>", "").Text :
		R("cnet_common_SP-24312").HasValue() ? R("cnet_common_SP-24312").Replace("<NULL>", "").Text : "";
	//
    if (!String.IsNullOrEmpty(use)) {
        if (use.ToLower().Equals("general-purpose use")) {
            result = "These general-purpose batteries are perfect for cameras and other small devices";
        }
        else {
            result = $"Battery is ideal to use in {use.ToLower()}";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"BatteryUse⸮{result}");
    }
}
// --[FEATURE #6]
// --additional Hi-Density
void AdditionalHiDensity() {
    var result = "";
    //General-Purpose Use|Car Security, Organizers and glucometers|Toys, Flashlights, remote controls and fire alarms|Portable CD players, Shavers, Radios and Smoke alarms|Headset
    var features = A[1505];
    if (features.HasValue("Hi-Density Core")) {
        result = "Features ##Hi-Density ##Core technology for unbeatable long-lasting power";
    } 
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalHiDensity⸮{result}");
    }
}

// --[FEATURE #7]
// --additional Power Check
AdditionalPowerCheck();
void AdditionalPowerCheck() {
    var result = "";
    //General-Purpose Use|Car Security, Organizers and glucometers|Toys, Flashlights, remote controls and fire alarms|Portable CD players, Shavers, Radios and Smoke alarms|Headset
    var features = A[1505];
    if (features.HasValue("Duracell Power Check")) {
        result = "Power check lets you see how much energy is left with the push of a button";
    } 
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalPowerCheck⸮{result}");
    }
}

// --[FEATURE #8]
// --additional
void AdditionalDuralockPowerPreserve() {
    var result = "";
    //General-Purpose Use|Car Security, Organizers and glucometers|Toys, Flashlights, remote controls and fire alarms|Portable CD players, Shavers, Radios and Smoke alarms|Headset
    var features = A[1505];
    if (features.HasValue("Duralock Power Preserve Technology")) {
        result = "Duralock Power Preserve™ Technology is guaranteed for 10 years in storage";
    } 
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalDuralockPowerPreserve⸮{result}");
    }
}

// --[FEATURE #9]
// --additional

void AdditionalM3Technology() {
    var result = "";
    //General-Purpose Use|Car Security, Organizers and glucometers|Toys, Flashlights, remote controls and fire alarms|Portable CD players, Shavers, Radios and Smoke alarms|Headset
    var features = A[1505];
    if (features.HasValue("Duracell M3 technology")) {
        result = "Features Duracell M3 technology";
    } 
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalM3Technology⸮{result}");
    }
}

//16831375514580302 end of "Batteries" §§