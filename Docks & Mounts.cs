//§§530868304206054 "Docks & Mounts" "Alex K." "Serhii.O"

DocksMountsTypeUse();
DocksAndMountsCompatibility();
ConnectionOrHoldingDesign();
// Dimentions
DocksAndMountsPowerSource();
DocksAndMountsAdjustments();
DocksAndMountsDegreeViewingAngles();
DocksAndMountsScreenSizeCompatibility();
DocksAndMountsPackageContent();
// true color material
// Warranty


// --[FEATURE #1]
// --Type of Piece and UseType of Mount/Dock
void DocksMountsTypeUse(){
    //Sticky Mat/Pad|Mount|Dock
    var type = R("SP-18044").HasValue() ? R("SP-18044") : R("cnet_common_SP-18044");
    var miscellaneous_PlacingMounting = A[1031];
    if (type.HasValue("Mount") && A[7522].HasValue()) {
        Add($"DocksMountsTypeUse⸮This mount is a stable mounting option for your {A[7522].Values.Select(o => o.Value()).FlattenWithAnd()}");
    }
    else if (type.HasValue("Mount")) {
        Add($"DocksMountsTypeUse⸮Secure your devices with this mount");
    }
    else if (type.HasValue("Dock")) {
        Add($"DocksMountsTypeUse⸮This dock is the ideal solution when you want to charge your devices");
    }
    else if (type.HasValue("Sticky Mat/Pad")) {
        Add($"DocksMountsTypeUse⸮Secure your devices with this sticky mat/pad");
    }
}

// --[FEATURE #2]
// --Product Compatibility (if it exceeds bullet size list in Extended Description)
void DocksAndMountsCompatibility(){ 
    // Universal|Nokia|Motorola|LG|Amazon Fire|Samsung Galaxy S2|iPhone 6 Plus/6s Plus/7 Plus/8 Plus|iPhone 6/6S|Blackberry|Google Nexus|iPhone 7/8|Google Pixel 2|iPhone 6/6S/7/8|Samsung Galaxy Note 8|iPhone 5/5s/SE|iPhone 8 Plus|iPhone X|Samsung Galaxy Note 4|iPhone 6 Plus/6s Plus|Galaxy S8|ZTE|Mini Mobile|Most Smartphones|Samsung Galaxy Note 3|Samsung Galaxy Note 2|HTC M9|Samsung Galaxy S6|iPhone 6s Plus|6" Cell Phone|iPhone 6s|5" Cell Phone|iPhone 7 Plus /iPhone 8 Plus|iPhone 7/ iPhone 8|iPhone 3G/3GS|HTC Desire 526|HTC Desire 510|HTC 8Xt|Electrify/Mb855/Photon 4G|iPhone 5/5S/5C/6/6 Plus|HTC Evo 4G Lte|HTC Desire 626/626S|HTC Desire 626|HTC Desire 601/Zara|HTC One M9|iPhone/iPad/iPod Touch|HTC One M8|Utstarcom|Samsung Galaxy S 8+|Cisco|Samsung Galaxy S 8|Sanyo E4100Taho|Samsung Galaxy 8|HTC M7|Huawei|Coolpad|HTC Windows Phone 8X/Htc Zenith|HTC One/M7|PCD|iPhone 4/4S/5 & iPod Touch|iPhone Se|HTC One Vx|iPhone 7 Plus|iPhone XS|iPhone 8|iPhone XS Max|myTouch|HTC One X|Samsung Galaxy S9+|iPhone 6 Plus|Microsoft|Apple iPhone X, Xs|iPhone 7/6s/6|Samsung D710, R760, Galaxy S II 4G|Samsung D700|Microsoft Lumia 640|Microsoft Lumia|iPhone 5 & Newer|Samsung Galaxy S9|Samsung Galaxy Note|Pantech|Samsung (Other)|HP|BLU|iPhone 3G/3GS|Dell|Palm|Kyocera|Sony|Alcatel|Samsung Galaxy S5|Samsung Galaxy S4|Samsung Galaxy S3|iPhone 6|Samsung R360|iPhone 4/4s|CASIOC811|iPhone 7|Samsung Galaxy S6 Edge|Samsung M580|iPhone 5/5s|Alcatel 7024W|Samsung Intensity III|iPhone 5c|Samsung i9260|Alcatel 5020T|Samsung S390G/T189N|Samsung R830 Galaxy Axiom|HTC|Samsung R480|Samsung Galaxy S7|Samsung Galaxy S4 Mini|Samsung Galaxy Note 5|Google Pixel 2 XL|All iPhones|Multiple Brands 
    var CellPhoneCompatibilityRef = REQ.GetVariable("SP-18043").HasValue() ? REQ.GetVariable("SP-18043") : R("SP-18043").HasValue() ? R("SP-18043") : R("cnet_common_SP-18043");
 
    var CompatibleProducts = Coalesce(SPEC["MS"].GetLine("Compatible with").Body, SPEC["ES"].GetLine("Compatible with").Body, SPEC["MS"].GetLine("Designed For").Body, SPEC["ES"].GetLine("Designed For").Body); 
    
    if(CellPhoneCompatibilityRef.HasValue("iPhone/iPad/iPod Touch")){ 
        Add($"DocksAndMountsCompatibility⸮Compatible with iPod/iPhone/iPad"); 
    } 
    else if(CellPhoneCompatibilityRef.HasValue()){ 
        Add($"DocksAndMountsCompatibility⸮Compatible with {CellPhoneCompatibilityRef.Replace("All iPhones", "all iPhones", "Multiple Brands", "multiple brands")}"); 
    } 
    else if(CompatibleProducts.HasValue()){ 
        Add($"DocksAndMountsCompatibility⸮{Shorten($"Charges {CompatibleProducts.Text.Replace(";", ",").Split(", ").Select(d => Coalesce(d).ExtractNumbers().Any() ? $"##{d}" : d).FlattenWithAnd().RegexReplace(@"(-in\w+)", @"""").RegexReplace(@"\(.*?\)", "").Replace("lightning", "Lightning")}", 250, ',')}"); 
    } 
} 

// --[FEATURE #3]
// --Connection or Holding Design
void ConnectionOrHoldingDesign() {
    // Securely attaches to dash or window
    if (A[1031].HasValue()) {
        Add($"ConnectionOrHoldingDesign⸮Securely attaches to {A[1031].Values.Select(o => o.Value()).FlattenWithAnd()}");
    }
}

// --[FEATURE #4]
// --Dimensions as HxWxD

// --[FEATURE #5]
// --Power Source
void DocksAndMountsPowerSource() {
    // https://www.staples.com/Lenovo-Proprietary-Interface-Docking-Station-for-Notebook-Tablet-PC-230-W-40A50230US/product_IM11X6392
    // Power: Power adapter 230 Watt AC 120/230 V ( 50/60 Hz )
    var type = REQ.GetVariable("SP-98").HasValue() ? REQ.GetVariable("SP-98") : R("SP-98").HasValue() ? R("SP-98") : R("cnet_common_SP-98");
    if (type.HasValue()) {
        Add($"DocksAndMountsPowerSource⸮Power: {type.ToLower(true)}");
    }
}

// --[FEATURE #6]
// --Additional adjustments
void DocksAndMountsAdjustments() {
    // Features tilt, vertically adjustable by 90 degrees, adjustable height
    if (A[9724].HasValue() && A[9724].Values.Count() == 1) {
        Add($"DocksAndMountsAdjustments⸮Features {A[9724].FirstValue()} adjustment");
    }
    else if (A[9724].HasValue()) {
        Add($"DocksAndMountsAdjustments⸮Features {A[9724].Values.Select(o => o.Value()).FlattenWithAnd()} adjustments");
    }
}

// --[FEATURE #7]
// --Additional Degree
void DocksAndMountsDegreeViewingAngles() {
    //  360-degree free rotation for optimal viewing angles
    if (A[9726].HasValue()) {
        Add($"DocksAndMountsDegreeViewingAngles⸮{A[9726].FirstValue().Replace("°", "")}-degree free rotation for optimal viewing angles");
    }
}

// --[FEATURE #8]
// --Additional Screen Size Compatibility
void DocksAndMountsScreenSizeCompatibility() {
    //   Fits most smartphones, up to 7"
    // screen size compatible 7522
    if ((A[10095].HasValue("up to%") || A[10095].HasValue("from %")) && A[7522].HasValue("cellular phone")) {
        Add($"DocksAndMountsScreenSizeCompatibility⸮Fits most smartphones, {A[10095].FirstValue()}");
    } 
    else if ((A[10095].HasValue("up to%") || A[10095].HasValue("from %")) && A[7522].HasValue("tablet")) {
        Add($"DocksAndMountsScreenSizeCompatibility⸮Fits most tablets, {A[10095].FirstValue()}");
    }
    else if (A[10095].HasValue() && A[7522].HasValue("cellular phone")) {
        Add($"DocksAndMountsScreenSizeCompatibility⸮Fits {A[10095].FirstValue()} smartphones");
    }
    else if (A[10095].HasValue() && A[7522].HasValue("tablet")) {
        Add($"DocksAndMountsScreenSizeCompatibility⸮Fits {A[10095].FirstValue()} tablets");
    }
    else if (A[10095].HasValue()) {
         Add($"DocksAndMountsScreenSizeCompatibility⸮Compatible with {A[10095].FirstValue()} screens");
    }
}

// --[FEATURE #9] 
// --Additional Package Content
void DocksAndMountsPackageContent() {
    //  Includes AC power adapter, power cord and publications
    if (A[7521].HasValue()) {
        Add($"DocksAndMountsPackageContent⸮Includes {A[7521].Values.Select(o => o.Value()).FlattenWithAnd()}");
    }
}

// --[FEATURE #10]
// -- Additional true color material

// --[FEATURE #11]
// --Additional Warranty

//§§530868304206054 end of "Docks & Mounts"