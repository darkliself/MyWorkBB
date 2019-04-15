
// Regular expression BEGIN

var regexes = new Dictionary<string, string>(){
    { @"([,].+[^,])(\sand\s)", @"$1, $2" },
    { @"(?<=\d)\s(?=(yd\(s\)|mm|cm|m|L|mL|gram|cup\(s\)|qt|MB\/s)(\s|$|\|))", @"" },
    { @"\s\s+", @"\s" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.)\s1\s(x\s)?(?!Plus|\d|x\s\d|year|oz.|mil.|lb.|lbs.|gal.|fl. oz.|cu. ft.)", @"\sone\s" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.)\s2\s(x\s)?(?!Plus|\d|x\s\d|years|oz.|mil.|lb.|lbs.|gal.|fl. oz.|cu. ft.)", @"\stwo\s" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.)\s3\s(x\s)?(?!Plus|\d|x\s\d|years|oz.|mil.|lb.|lbs.|gal.|fl. oz.|cu. ft.)", @"\sthree\s" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.)\s4\s(x\s)?(?!Plus|\d|x\s\d|years|oz.|mil.|lb.|lbs.|gal.|fl. oz.|cu. ft.)", @"\sfour\s" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.)\s5$", @"\sfive" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.)\s6$", @"\ssix" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.)\s7$", @"\sseven" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.)\s8$", @"\seight" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.)\s9$", @"\snine" },
    { @"^1\s(x\s)?(?!Plus|x\s\d|mil.|gal.|oz.|lb.)", @"One\s" },
    { @"^2\s(x\s)?(?!Plus|x\s\d|mil.|gal.|oz.|lb.)", @"Two\s" },
    { @"^3\s(x\s)?(?!Plus|x\s\d|mil.|gal.|oz.|lb.)", @"Three\s" },
    { @"^4\s(x\s)?(?!Plus|x\s\d|mil.|gal.|oz.|lb.)", @"Four\s" },
    { @"^5\s(x\s)?(?!Plus|x\s\d|mil.|gal.|oz.|lb.)", @"Five\s" },
    { @"^6-button", @"Six-button" },
    { @"^7-button", @"Seven-button" },
    { @"^8-button", @"Eight-button" },
    { @"^9-button", @"Nine-button" },
    { @"(?<=water|dirt|snow|drop|fire|splash|sand|weather|rain|tear|Tear)(?<!-)\s?proof", @"-proof"},
    { @"Gray\/Silver", @"Gray/silver" },
    { @"(?<=\s)tyvek", @"Tyvek" },
    { @"(?<=[0-9])\spercent(?=\s|$|.)", @"%" },
    { @"®|™|©", @""},
    { @"one touch ring", @"one-touch ring" },
    { @"\srip-Proof", @"\sRip-Proof" },
    { @"(l|L)eather(s|S)oft", @"LeatherSoft" },
    { @"SanDisk secureAccess", @"SanDisk SecureAccess" },
    { @"big sur color", @"Big Sur color" },
    { @"post-consumer", @"postconsumer" },
    { @"Bluetrack", @"BlueTrack" },
    { @"-Strip", @"-strip" },
    { @"(\[\[.+)(\sx\s)(.+\]\])", @"$1W$2$3L" },
    { @"\[\[", @""},
    { @"-Tilt", @"-tilt"},
    { @"H.K. anderson", @"H.K. Anderson" },
    { @"\sExtra Bold", @"\sextra\sbold" },
    { @"double wall construction", @"double-wall construction" },
    { @"Male to male", @"Male\sto\sMale" },
    { @"multicolor", @"multi\scolors" },
    { @"[Aa]ssorted(?!\scolor)", @"$&\scolors" },
    { @"(?<=\s)PLA(?=\s|$)", @"plastic" },
    { @"arabica flavor", @"Arabica\sflavor" },
    { @"black and white images", @"black/white images" },
    { @"ActiveAire Passive Whole-Room Freshener Dispenser", @"ActiveAire\spassive\swhole-room\sfreshener\sdispenser" },
    { @"chisel tip marker", @"chisel-tip marker" },
    { @"NFC\/wireless direct", @"NFC\/Wireless\sDirect" },
    { @"Audio Line In", @"audio\sline-in" },
    { @"VGA In", @"VGA\sinput" },
    { @"(?<=\d\s)lbs(?=[^.]|$)", @"$&." },
    { @"jalapeno", @"jalapeño" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\\/|\d x|\d,|No.)\s5\s(x\s)?(?!Plus|\d|x\s\d|years|oz.|mil.|lb.|lbs.|gal.|cu. ft.)", @"\sfive\s"},
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.)\s6\s(x\s)?(?!Plus|\d|x\s\d|years|oz.|mil.|lb.|lbs.|gal.|fl. oz.|cu. ft.)", @"\ssix\s" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.)\s7\s(x\s)?(?!Plus|\d|x\s\d|years|oz.|mil.|lb.|lbs.|gal.|fl. oz.|cu. ft.)", @"\sseven\s" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.)\s8\s(x\s)?(?!Plus|\d|x\s\d|years|oz.|mil.|lb.|lbs.|gal.|fl. oz.|cu. ft.)", @"\seight\s" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.)\s9\s(x\s)?(?!Plus|\d|x\s\d|years|oz.|mil.|lb.|lbs.|gal.|fl. oz.|cu. ft.)", @"\snine\s" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.)\s1$", @"\sone" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.)\s2$", @"\stwo" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.)\s3$", @"\sthree" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.)\s4$", @"\sfour" },
    { @"^6\s(x\s)?(?!Plus|x\s\d|mil.|gal.|oz.|lb.)", @"Six\s" },
    { @"^7\s(x\s)?(?!Plus|x\s\d|mil.|gal.|oz.)", @"Seven\s" },
    { @"^8\s(x\s)?(?!Plus|x\s\d|mil.|gal.|oz.|lb.)", @"Eight\s" },
    { @"^9\s(x\s)?(?!Plus|x\s\d|mil.|gal.|oz.|lb.)", @"Nine\s" },
    { @"^3-button", @"Three-button" },
    { @"^4-button", @"Four-button" },
    { @"^5-button", @"Five-button" },
    { @"\(4-way\)", @"(four-way)" },
    { @"\s\s+", @"\s" },
    { @"(?<=(^A)|(\sa))cid\sfree", @"cid-free" },
    { @"(?<=(^R)|(\sr))esidue\sfree", @"esidue-free" },
    { @"One touch ring", @"One-touch ring" },
    { @"non-View", @"non-view" },
    { @"caribbean blue", @"Caribbean blue" },
    { @"encryptStick lite", @"EncryptStick Lite" },
    { @"\spunk color", @" Punk color" },
    { @"multi-pack", @"multipack" },
    { @"C-fold", @"C-Fold" },
    { @"postconsumer", @"post-consumer" },
};

// Regular expression END

void AddBullet(Dictionary<string, string> dictionaryBulletsPool2, string bulletKey, bool addBlank){
    if (dictionaryBulletsPool2.ContainsKey(bulletKey)){
        Add(dictionaryBulletsPool2[bulletKey]);
    }
    else if (addBlank){
        Add("@@");
    }
}

var dictionaryBulletsPool = new Dictionary<string, string>();
var bulletsPool = R("cnet_common_feature_bullets_pool").Text;
var RegexString = "";

foreach(var bulletPoint in bulletsPool.Split("|")){
    foreach(var regex in regexes){
        RegexString = Coalesce(bulletPoint.Split("⸮").Last()).RegexReplace(regex.Key, regex.Value).Text;
        
    }
    dictionaryBulletsPool.Add(
        bulletPoint.Split("⸮").First(),
        RegexString
    );
     if(RegexString.Contains(regex.Value)){
            break;
    }
}

if (CAT.Alt.Count() > 0)
{
    switch (CAT.MainAlt.Key){
        // "Desktop Computers" "Serhii.O" 
        case var a when a.In("3364110222167288"):
        AddBullet(dictionaryBulletsPool, "ProcessorTypeOfDesktopLaptop", true);
        AddBullet(dictionaryBulletsPool, "RAMTypeOfDesktop", true);
        AddBullet(dictionaryBulletsPool, "HardDriveMemoryTypeCapacityOfDesktop", true);
        AddBullet(dictionaryBulletsPool, "OperatingSystem", true);
        AddBullet(dictionaryBulletsPool, "Graphics", true);
        AddBullet(dictionaryBulletsPool, "ConnectivityOfDesktop", true);
        AddBullet(dictionaryBulletsPool, "OpticalDriveOfDesktop", true);
        AddBullet(dictionaryBulletsPool, "USBPortsOfDesktop", true);
        AddBullet(dictionaryBulletsPool, "AdditionalPortsInterface", false);
        AddBullet(dictionaryBulletsPool, "Dimensions", true);
        AddBullet(dictionaryBulletsPool, "BoxContentsOfDesktop", false);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "Remanufactured Laser Printer Ink, Toner & Drum Units, InkJet Printer Ink, Toner & Drum Units" "Serhii.O" 
        case var a when a.In("5226058", "5226054", "5226006", "12393811005220973", "5226157", "12375510567216950", "12366910285216553", "1212164913166147", "12378610641217427"):
        AddBullet(dictionaryBulletsPool, "CartridgeYieldType", true);
        AddBullet(dictionaryBulletsPool, "CartridgePackageContents", true);
        AddBullet(dictionaryBulletsPool, "VendorSpecificInformation", true);
        AddBullet(dictionaryBulletsPool, "CartridgeSavingsMessage", true);
        AddBullet(dictionaryBulletsPool, "CartridgeCompatible", true);
        AddBullet(dictionaryBulletsPool, "RecycledPostConsumerContent", true);
        AddBullet(dictionaryBulletsPool, "CartridgeMediaIncluded", true);
        AddBullet(dictionaryBulletsPool, "CartridgeFeatures", true);
        AddBullet(dictionaryBulletsPool, "CartridgeRemanufactured", true);
        break;
        // "Heaters" "Serhii.O" 
        case var a when a.In("168339291098380001"):
        AddBullet(dictionaryBulletsPool, "TypeOfHeater", true);
        AddBullet(dictionaryBulletsPool, "SpeedSettingsOfHeater", true);
        AddBullet(dictionaryBulletsPool, "MaximumWattageHeaterBTUOfHeater", true);
        AddBullet(dictionaryBulletsPool, "EstimatedCoverageOfHeater", true);
        AddBullet(dictionaryBulletsPool, "FeaturesOfHeater", true);
        AddBullet(dictionaryBulletsPool, "SpecialButtonsAndControlsOfHeater", true);
        AddBullet(dictionaryBulletsPool, "AdjustableOfHeater", true);
        AddBullet(dictionaryBulletsPool, "Dimensions", true);
        AddBullet(dictionaryBulletsPool, "CompliantStandards", true);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "Classification Folders" "Serhii.O" 
        case var a when a.In("135391203142831"):
        AddBullet(dictionaryBulletsPool, "TrueColorMaterialOfClassificationFolders", true);
        AddBullet(dictionaryBulletsPool, "FastenersOfClassificationFolders", true);
        AddBullet(dictionaryBulletsPool, "SizeLetterAndlegalOfClassificationFolders", true);
        AddBullet(dictionaryBulletsPool, "TabStyleAndLocationOfClassificationFolders", true);
        AddBullet(dictionaryBulletsPool, "RecycledPostConsumerContent", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "AdditionalSafeSHIELDOfClassificationFolders", false);
        AddBullet(dictionaryBulletsPool, "AdditionalTyvekGussetOfClassificationFolders", false);
        AddBullet(dictionaryBulletsPool, "AdditionalReinforcedTabsOfClassificationFolders", false);
        AddBullet(dictionaryBulletsPool, "AdditionalAntimicrobialOfClassificationFolders", false);
        AddBullet(dictionaryBulletsPool, "AdditionalDividersOfClassificationFolders", false);
        AddBullet(dictionaryBulletsPool, "AdditionalHeavyDutyOfClassificationFolders", false);
        AddBullet(dictionaryBulletsPool, "AdditionalMulticolorOfClassificationFolders", false);
        break;
        // "Pens" "Alex K." 
        case var a when a.In("1114953110001"):
        AddBullet(dictionaryBulletsPool, "PenType", true);
        AddBullet(dictionaryBulletsPool, "PenInkTrueColor", true);
        AddBullet(dictionaryBulletsPool, "PenPointTypeAndSize", true);
        AddBullet(dictionaryBulletsPool, "PenBarrelColorAndBarrelMaterial", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "AdditionalPenGrip", false);
        AddBullet(dictionaryBulletsPool, "AdditionalPenClip", false);
        AddBullet(dictionaryBulletsPool, "AdditionalPenRefillable", false);
        AddBullet(dictionaryBulletsPool, "AdditionalPenBarrelShape", false);
        AddBullet(dictionaryBulletsPool, "AdditionalPenInkLevelViewingWindow", false);
        AddBullet(dictionaryBulletsPool, "AdditionalArchivalAcidFree", false);
        AddBullet(dictionaryBulletsPool, "AdditionalUniSuperInk", false);
        AddBullet(dictionaryBulletsPool, "AdditionalUniFlowSystem", false);
        AddBullet(dictionaryBulletsPool, "AdditionalLiquidInkSystem", false);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "File Folders" "Serhii.O" 
        case var a when a.In("135391203141669"):
        AddBullet(dictionaryBulletsPool, "TrueColorMaterialOfFileFolder", true);
        AddBullet(dictionaryBulletsPool, "NumberOfTabs", true);
        AddBullet(dictionaryBulletsPool, "FileFolderSize", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "RecycledPostConsumerContent", true);
        AddBullet(dictionaryBulletsPool, "CapacityOfFileFolders", true);
        AddBullet(dictionaryBulletsPool, "AdditionalResistsOfFileFolders", false);
        AddBullet(dictionaryBulletsPool, "AdditonalFeaturesOfFileFolders", false);
        AddBullet(dictionaryBulletsPool, "AdditionalRoundedCornersOfFileFolders", false);
        AddBullet(dictionaryBulletsPool, "AdditionalAcidFreeOfFileFolders", false);
        AddBullet(dictionaryBulletsPool, "AdditionalReinforcedShelfMasterOfFileFolders", false);
        AddBullet(dictionaryBulletsPool, "AdditionalProtectCutLessOfFileFolders", false);
        AddBullet(dictionaryBulletsPool, "AdditionalStraightCutOfFileFolders", false);
        break;
        // "Labels" "Serhii.O" 
        case var a when a.In("1385810842142725"):
        AddBullet(dictionaryBulletsPool, "LabelTypeOfLabels", true);
        AddBullet(dictionaryBulletsPool, "LayFlatLabelDimensions", true);
        AddBullet(dictionaryBulletsPool, "TrueColorMaterial", true);
        AddBullet(dictionaryBulletsPool, "AdhesiveType", true);
        AddBullet(dictionaryBulletsPool, "PackageContentsLabels", true);
        AddBullet(dictionaryBulletsPool, "AdditionalPopUpOfLabels", false);
        AddBullet(dictionaryBulletsPool, "AdditionalTrueBlockOfLabels", false);
        AddBullet(dictionaryBulletsPool, "AdditionalLabels8", false);
        AddBullet(dictionaryBulletsPool, "AdditionalLabels9", false);
        AddBullet(dictionaryBulletsPool, "AdditionalLabels10", false);
        AddBullet(dictionaryBulletsPool, "AdditionalLabels11", false);
        AddBullet(dictionaryBulletsPool, "AdditionalDesignProOfLabels", false);
        AddBullet(dictionaryBulletsPool, "PrinterCompatibility", true);
        break;
        // "Post-it&reg; & Sticky Notes" "Alex K." 
        case var a when a.In("167638614736165755"): 
        AddBullet(dictionaryBulletsPool, "TypeOfStickyNotes", true);
        AddBullet(dictionaryBulletsPool, "PostItSizeColorCollection", true);
        AddBullet(dictionaryBulletsPool, "SheetCountPadsPack", true);
        AddBullet(dictionaryBulletsPool, "PopUpOrFlat", true);
        AddBullet(dictionaryBulletsPool, "LineType", true);
        AddBullet(dictionaryBulletsPool, "DispenserIncluded", true);
        AddBullet(dictionaryBulletsPool, "RecycledContent", true);
        AddBullet(dictionaryBulletsPool, "PostConsumerContent", true);
        break;
        // "Fans" "Alex K." 
        case var a when a.In("168339291098380002"):
        AddBullet(dictionaryBulletsPool, "FanTypeAndUse", true);
        AddBullet(dictionaryBulletsPool, "SpecialButtonsAndControls", true);
        AddBullet(dictionaryBulletsPool, "Dimensions", true);
        AddBullet(dictionaryBulletsPool, "OscillatingFanPosition", true);
        AddBullet(dictionaryBulletsPool, "ColorFamily", true); // can be for all categories
        AddBullet(dictionaryBulletsPool, "FanCordLength", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "AdditionalMetalHousing", false);
        AddBullet(dictionaryBulletsPool, "AdditionalTimer", false);
        AddBullet(dictionaryBulletsPool, "AdditionalCarryHandle", false);
        AddBullet(dictionaryBulletsPool, "CompliantStandards", false); // waiting for rename in heaters
        AddBullet(dictionaryBulletsPool, "Warranty", false);
        break;
        // "Juice" "Alex K." 
        case var a when a.In("168412514983216257"):
        AddBullet(dictionaryBulletsPool, "JuiceType", true);
        AddBullet(dictionaryBulletsPool, "CapacityOz", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "Ingredients", true);
        AddBullet(dictionaryBulletsPool, "SugarFree", true);
        AddBullet(dictionaryBulletsPool, "ArtificialFlavorsOrPreservatives", true);
        AddBullet(dictionaryBulletsPool, "Kosher", false);
        AddBullet(dictionaryBulletsPool, "GlutenFree", false);
        AddBullet(dictionaryBulletsPool, "AdditionalVitamins", false);
        AddBullet(dictionaryBulletsPool, "AdditionalProductEnergy", false);
        AddBullet(dictionaryBulletsPool, "AdditionalContainerType", false);
        AddBullet(dictionaryBulletsPool, "AdditionalCapriSunVaietyPack", false);
        break;
        // "Markers" "Alex K." 
        case var a when a.In("1114953140896"):
        AddBullet(dictionaryBulletsPool, "MarkerTypeAndUse", true);
        AddBullet(dictionaryBulletsPool, "MarkerInkColor", true);
        AddBullet(dictionaryBulletsPool, "MarkerPointType", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "NonToxic", true);
        AddBullet(dictionaryBulletsPool, "CompliantStandards", true);
        AddBullet(dictionaryBulletsPool, "AdditionalOdor", false);
        break;
        // "Binder Accessories" "Alex K."  
        case var a when a.In("13529397020006"):
        AddBullet(dictionaryBulletsPool, "BinderAccessoryTypeAndUse", true);
        AddBullet(dictionaryBulletsPool, "TrueColorMaterial", true);
        AddBullet(dictionaryBulletsPool, "Dimensions", true);
        AddBullet(dictionaryBulletsPool, "TabIndexPunchInformation", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "RecycledPostConsumerContent", false);
        AddBullet(dictionaryBulletsPool, "AdditionalArchivalSafe", false);
        break;
    }
}