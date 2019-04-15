// Regular expression BEGIN

var regexes = new Dictionary<string, string>(){
    { @"([,].+[^,])( and )", @"$1, $2" },
    { @"(?<=\d) (?=(yd\(s\)|mm|cm|m|L|mL|gram|cup\(s\)|qt|MB\/s)( |$|\|))", @"" },
    { @" +", @" " },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.) 1 (x )?(?!Plus|\d|x \d|year|oz.|mil.|lb.|lbs.|gal.|fl. oz.|cu. ft.)", @" one " },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.) 2 (x )?(?!Plus|\d|x \d|years|oz.|mil.|lb.|lbs.|gal.|fl. oz.|cu. ft.)", @" two " },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.) 3 (x )?(?!Plus|\d|x \d|years|oz.|mil.|lb.|lbs.|gal.|fl. oz.|cu. ft.)", @" three " },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.) 4 (x )?(?!Plus|\d|x \d|years|oz.|mil.|lb.|lbs.|gal.|fl. oz.|cu. ft.)", @" four " },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\\/|\d x|\d,|No.) 5 (x )?(?!Plus|\d|x \d|years|oz.|mil.|lb.|lbs.|gal.|cu. ft.)", @" five "},
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.) 6 (x )?(?!Plus|\d|x \d|years|oz.|mil.|lb.|lbs.|gal.|fl. oz.|cu. ft.)", @" six " },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.) 7 (x )?(?!Plus|\d|x \d|years|oz.|mil.|lb.|lbs.|gal.|fl. oz.|cu. ft.)", @" seven " },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.) 8 (x )?(?!Plus|\d|x \d|years|oz.|mil.|lb.|lbs.|gal.|fl. oz.|cu. ft.)", @" eight " },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.) 9 (x )?(?!Plus|\d|x \d|years|oz.|mil.|lb.|lbs.|gal.|fl. oz.|cu. ft.)", @" nine " },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.) 1$", @" one" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.) 2$", @" two" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.) 3$", @" three" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.) 4$", @" four" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.) 5$", @" five" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.) 6$", @" six" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.) 7$", @" seven" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.) 8$", @" eight" },
    { @"(?<!Windows|Gen|Windows Embedded Standard|iOS|Android|iPhone|Pixel|Samsung Galaxy Tab|Storm|\/|\d x|\d,|No.) 9$", @" nine" },
    { @"^1 (x )?(?!Plus|x \d|mil.|gal.|oz.|lb.)", @"One " },
    { @"^2 (x )?(?!Plus|x \d|mil.|gal.|oz.|lb.)", @"Two " },
    { @"^3 (x )?(?!Plus|x \d|mil.|gal.|oz.|lb.)", @"Three " },
    { @"^4 (x )?(?!Plus|x \d|mil.|gal.|oz.|lb.)", @"Four " },
    { @"^5 (x )?(?!Plus|x \d|mil.|gal.|oz.|lb.)", @"Five " },
    { @"^6 (x )?(?!Plus|x \d|mil.|gal.|oz.|lb.)", @"Six " },
    { @"^7 (x )?(?!Plus|x \d|mil.|gal.|oz.)", @"Seven " },
    { @"^8 (x )?(?!Plus|x \d|mil.|gal.|oz.|lb.)", @"Eight " },
    { @"^9 (x )?(?!Plus|x \d|mil.|gal.|oz.|lb.)", @"Nine " },
    { @"^3-button", @"Three-button" },
    { @"^4-button", @"Four-button" },
    { @"^5-button", @"Five-button" },
    { @"^6-button", @"Six-button" },
    { @"^7-button", @"Seven-button" },
    { @"^8-button", @"Eight-button" },
    { @"^9-button", @"Nine-button" },
    { @"\(4-way\)", @"(four-way)" },
    { @"(?<=[Ww]ater|[Dd]irt|[Ss]now|[Dd]rop|[Ff]ire|[Ss]plash|[Ss]and|[Ww]eather|[Rr]ain|[Tt]ear)(?<!-) ?proof", @"-proof"},
    { @"Gray\/Silver", @"Gray/silver" },
    { @"(?<=(^A)|( a))cid free", @"cid-free" },
    { @"(?<=(^R)|( r))esidue free", @"esidue-free" },
    { @"(?<= )tyvek", @"Tyvek" },
    { @"(?<=[0-9]) percent(?= |$|.)", @"%" },
    { @"®|™|©", @""},
    { @"one touch ring", @"one-touch ring" },
    { @"One touch ring", @"One-touch ring" },
    { @" rip-Proof", @" Rip-Proof" },
    { @"non-View", @"non-view" },
    { @"(l|L)eather(s|S)oft", @"LeatherSoft" },
    { @"caribbean blue", @"Caribbean blue" },
    { @"SanDisk secureAccess", @"SanDisk SecureAccess" },
    { @"encryptStick lite", @"EncryptStick Lite" },
    { @"big sur color", @"Big Sur color" },
    { @" punk color", @" Punk color" },
    { @"post-consumer", @"postconsumer" },
    { @"multi-pack", @"multipack" },
    { @"Bluetrack", @"BlueTrack" },
    { @"C-fold", @"C-Fold" },
    { @"-Strip", @"-strip" },
    { @"postconsumer", @"post-consumer" },
    { @"(\[\[.+)( x )(.+\]\])", @"$1W$2$3L" },
    { @"\[\[", @""},
    { @"-Tilt", @"-tilt"},
    { @"H.K. anderson", @"H.K. Anderson" },
    { @" Extra Bold", @" extra bold" },
    { @"double wall construction", @"double-wall construction" },
    { @"Male to male", @"Male to Male" },
    { @"multicolor", @"multi colors" },
    { @"[Aa]ssorted(?! color)", @"$& colors" },
    { @"(?<= )PLA(?= |$)", @"plastic" },
    { @"arabica flavor", @"Arabica flavor" },
    { @"black and white images", @"black/white images" },
    { @"ActiveAire Passive Whole-Room Freshener Dispenser", @"ActiveAire passive whole-room freshener dispenser" },
    { @"chisel tip marker", @"chisel-tip marker" },
    { @"NFC\/wireless direct", @"NFC\/Wireless Direct" },
    { @"Audio Line In", @"audio line-in" },
    { @"VGA In", @"VGA input" },
    { @"(?<=\d )lbs(?=[^.]|$)", @"$&." },
    { @"jalapeno", @"jalapeño" },
    { @"##", @"" }
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
    RegexString = bulletPoint.Split("⸮").Last(); 
    foreach(var regex in regexes){
        RegexString = Coalesce(RegexString).RegexReplace(regex.Key, regex.Value).Text;
    }
    dictionaryBulletsPool.Add(
        bulletPoint.Split("⸮").First(),
        RegexString
    );  
}

if (CAT.Alt.Count() > 0)
{
    switch (CAT.MainAlt.Key){
        // "Office Chairs" "Serhii.O" 
        case var a when a.In("2324927166253"): 
        AddBullet(dictionaryBulletsPool, "TypeofChairUse", true); 
        AddBullet(dictionaryBulletsPool, "OfficeChairsTrueColorUpholsteryMaterial", true); 
        AddBullet(dictionaryBulletsPool, "TypeOfSupportAndErgonomic", true); 
        AddBullet(dictionaryBulletsPool, "OfficeChairsOverallDimensions", true); 
        AddBullet(dictionaryBulletsPool, "SeatDimension", true); 
        AddBullet(dictionaryBulletsPool, "BackDimension", true); 
        AddBullet(dictionaryBulletsPool, "ArmType", true); 
        AddBullet(dictionaryBulletsPool, "TiltMechanismType", true); 
        AddBullet(dictionaryBulletsPool, "OfficeChairsCapacity", true); 
        AddBullet(dictionaryBulletsPool, "OfficeChairsAssemblyRequired", true); 
        AddBullet(dictionaryBulletsPool, "CertificationsStandardsANSIBIFMA", true); 
        AddBullet(dictionaryBulletsPool, "Warranty", true); 
        break; 
        // "Monitor Mounts & Stands" "Serhii.O" 
        case var a when a.In("590434034623141470"):
        AddBullet(dictionaryBulletsPool, "MonitorMountStandTypeUse", true);
        AddBullet(dictionaryBulletsPool, "MonitorSizeSupported", true);
        AddBullet(dictionaryBulletsPool, "AdjustableMinimumMaximumHeight", true);
        AddBullet(dictionaryBulletsPool, "Material", true);
        AddBullet(dictionaryBulletsPool, "MonitorMountStandArticulation", true);
        AddBullet(dictionaryBulletsPool, "MonitorMountStandRotation", true);
        AddBullet(dictionaryBulletsPool, "MonitorMountStandStorageOptions", true);
        AddBullet(dictionaryBulletsPool, "WeightAll", true);
        AddBullet(dictionaryBulletsPool, "Dimensions", true);
        AddBullet(dictionaryBulletsPool, "MountBracketOrVESAPattern", true);
        AddBullet(dictionaryBulletsPool, "CompliantStandards", true);
        AddBullet(dictionaryBulletsPool, "TrueColor", false);
        AddBullet(dictionaryBulletsPool, "RubberFeet", false);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "Filing Accessories" "Serhii.O" 
        case var a when a.In("135391203158855"):
        AddBullet(dictionaryBulletsPool, "FilingAccessoryType", true);
        AddBullet(dictionaryBulletsPool, "TrueColorMaterial", true);
        AddBullet(dictionaryBulletsPool, "Dimensions", true);
        AddBullet(dictionaryBulletsPool, "PaperSizeFilingAccessory", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "FolderExpands", false);
        AddBullet(dictionaryBulletsPool, "TypeOfFilingAccessory", false);
        AddBullet(dictionaryBulletsPool, "AdhesiveFilingAccessory", false);
        AddBullet(dictionaryBulletsPool, "HeavyDutyFilingAccessory", false);
        AddBullet(dictionaryBulletsPool, "LaminatedSurfaceFilingAccessory", false);
        AddBullet(dictionaryBulletsPool, "CutTabFilingAccessory", false);
        AddBullet(dictionaryBulletsPool, "TearResistantFilingAccessory⸮", false);
        AddBullet(dictionaryBulletsPool, "BlankInsertsFilingAccessory", false);
        AddBullet(dictionaryBulletsPool, "PrintingTechnologyFilingAccessory", false);
        AddBullet(dictionaryBulletsPool, "HeavyWeightFilingAccessory", false);
        AddBullet(dictionaryBulletsPool, "SelfAdhesivenessFilingAccessory", false);
        AddBullet(dictionaryBulletsPool, "AlphabeticalOrderFilingAccessory", false);
        break;
        // "Headsets & Microphones" "Serhii.O" 
        case var a when a.In("168130738704140478"):
        AddBullet(dictionaryBulletsPool, "HeadsetMicrophoneTypeUse", true);
        AddBullet(dictionaryBulletsPool, "CordHeadsetMicrophone", true);
        AddBullet(dictionaryBulletsPool, "HeadsetDesignColor", true);
        AddBullet(dictionaryBulletsPool, "MaterialOfItemCoating", true);
        AddBullet(dictionaryBulletsPool, "MicrophoneAndHeadsetTechnology", true);
        AddBullet(dictionaryBulletsPool, "InterfaceHeadsetMicrophone", true);
        AddBullet(dictionaryBulletsPool, "SensitivityHeadsetMicrophone", false);
        AddBullet(dictionaryBulletsPool, "MicrophonesFrequency", false);
        AddBullet(dictionaryBulletsPool, "ImpedanceHeadsetMicrophone", false);
        AddBullet(dictionaryBulletsPool, "PowerSource", false);
        AddBullet(dictionaryBulletsPool, "PushToTalk", false);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "Computer Mice" "Serhii.O" 
        case var a when a.In("5904340310149141983"):
        AddBullet(dictionaryBulletsPool, "MouseTrackingMethod", true);
        AddBullet(dictionaryBulletsPool, "WirelessOrWired", true);
        AddBullet(dictionaryBulletsPool, "MouseTrackingMethodOperation", true);
        AddBullet(dictionaryBulletsPool, "TypeOfBatteryNumber", true);
        AddBullet(dictionaryBulletsPool, "MultiDevice", true);
        AddBullet(dictionaryBulletsPool, "CompliantStandards", true);
        AddBullet(dictionaryBulletsPool, "NumberOfButtons", false);
        AddBullet(dictionaryBulletsPool, "Ergonomic", false);
        AddBullet(dictionaryBulletsPool, "BatteryIndicator", false);
        AddBullet(dictionaryBulletsPool, "AdjustableTrackball", false);
        AddBullet(dictionaryBulletsPool, "BlueTrackTechnology", false);
        AddBullet(dictionaryBulletsPool, "GestureFunction", false);
        AddBullet(dictionaryBulletsPool, "OpticalSensor", false);
        AddBullet(dictionaryBulletsPool, "USBCompatibility", false);
        AddBullet(dictionaryBulletsPool, "ControlCursor", false);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "Computer Monitors" "Serhii.O" 
        case var a when a.In("590442000142219"):
        AddBullet(dictionaryBulletsPool, "MonitorScreenSize", true);
        AddBullet(dictionaryBulletsPool, "DisplayTechnology", true);
        AddBullet(dictionaryBulletsPool, "MonitorResolution", true);
        AddBullet(dictionaryBulletsPool, "InterfaceOfMonitor", true);
        AddBullet(dictionaryBulletsPool, "MonitorAspectRatio", true);
        AddBullet(dictionaryBulletsPool, "MonitorColor", true);
        AddBullet(dictionaryBulletsPool, "MaxViewingAngle", true);
        AddBullet(dictionaryBulletsPool, "OverallDimensions", true);
        AddBullet(dictionaryBulletsPool, "SpeakersIncluded", true);
        AddBullet(dictionaryBulletsPool, "WeightAll", true);
        AddBullet(dictionaryBulletsPool, "CompliantStandards", false);
        AddBullet(dictionaryBulletsPool, "TrueColor", false);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "Audio/Video Cables" "Serhii.O" 
        case var a when a.In("590434031426140452"):
        AddBullet(dictionaryBulletsPool, "TypeOfCableInterfaceUse", true);
        AddBullet(dictionaryBulletsPool, "DimensionsAudioVideoCables", true);
        AddBullet(dictionaryBulletsPool, "ConnectorsPlating", true);
        AddBullet(dictionaryBulletsPool, "CableJacketMaterial", true);
        AddBullet(dictionaryBulletsPool, "SpecificAudioVisualFeature", true);
        AddBullet(dictionaryBulletsPool, "ConnectorFinishAndOrConnectorMaterial", true);
        AddBullet(dictionaryBulletsPool, "CompliantStandards", true);
        AddBullet(dictionaryBulletsPool, "SerialATA", true);
        AddBullet(dictionaryBulletsPool, "ProvidesProtection", false);
        AddBullet(dictionaryBulletsPool, "HDCPCompliant", false);
        AddBullet(dictionaryBulletsPool, "LatchedConnectors", false);
        AddBullet(dictionaryBulletsPool, "StrainRelief", false);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "TVs" "Serhii.O" 
        case var a when a.In("591714025317164870"):
        AddBullet(dictionaryBulletsPool, "TVSizeUse", true);
        AddBullet(dictionaryBulletsPool, "TVHDQualityTVType", true);
        AddBullet(dictionaryBulletsPool, "Enabled3D", true);
        AddBullet(dictionaryBulletsPool, "TVDisplayResolution", true);
        AddBullet(dictionaryBulletsPool, "IntergratedIOFeatures", true);
        AddBullet(dictionaryBulletsPool, "AudioVideoEnhancementTechnology", true);
        AddBullet(dictionaryBulletsPool, "PortsInterfaces", true);
        AddBullet(dictionaryBulletsPool, "Weight", true);
        AddBullet(dictionaryBulletsPool, "VESAMountingStandard", true);
        AddBullet(dictionaryBulletsPool, "CompliantStandards", true);
        AddBullet(dictionaryBulletsPool, "RemoteControlDetails", true);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "Laptops" "Serhii.O" 
        case var a when a.In("3364010221167289"):
        AddBullet(dictionaryBulletsPool, "ProcessorType", true);
        AddBullet(dictionaryBulletsPool, "HardDriveType", true);
        AddBullet(dictionaryBulletsPool, "OperatingSystem", true);
        AddBullet(dictionaryBulletsPool, "RAMType", true);
        AddBullet(dictionaryBulletsPool, "LaptopMemoryType", true);
        AddBullet(dictionaryBulletsPool, "ScreenResolution", true);
        AddBullet(dictionaryBulletsPool, "ScreenSize", true);
        AddBullet(dictionaryBulletsPool, "Graphics", true);
        AddBullet(dictionaryBulletsPool, "BacklitKeyboard", true);
        AddBullet(dictionaryBulletsPool, "WirelessConnectivity", true);
        AddBullet(dictionaryBulletsPool, "BatteryLife", true);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "Desktop Computers" "Serhii.O" 
        case var a when a.In("3364110222167288"):
        AddBullet(dictionaryBulletsPool, "ProcessorType", true);
        AddBullet(dictionaryBulletsPool, "RAMType", true);
        AddBullet(dictionaryBulletsPool, "HardDriveMemoryTypeCapacityOfDesktop", true);
        AddBullet(dictionaryBulletsPool, "OperatingSystem", true);
        AddBullet(dictionaryBulletsPool, "Graphics", true);
        AddBullet(dictionaryBulletsPool, "WirelessConnectivity", true);
        AddBullet(dictionaryBulletsPool, "OpticalDriveOfDesktop", true);
        AddBullet(dictionaryBulletsPool, "USBPortsOfDesktop", true);
        AddBullet(dictionaryBulletsPool, "AdditionalPortsInterface", false);
        AddBullet(dictionaryBulletsPool, "Dimensions", true);
        AddBullet(dictionaryBulletsPool, "BoxContentsOfDesktop", false);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "MICR Ink for All Units" "Serhii.O" 
        case var a when a.In("12375510567216950"): 
        AddBullet(dictionaryBulletsPool, "TypeOfCartridges", true); 
        AddBullet(dictionaryBulletsPool, "ColorOfCartridges", true); 
        AddBullet(dictionaryBulletsPool, "CartridgeYieldType", true); 
        AddBullet(dictionaryBulletsPool, "PageYieldPerPackage", true); 
        AddBullet(dictionaryBulletsPool, "InkOrTonerSpecialType", true); 
        AddBullet(dictionaryBulletsPool, "CartridgeCompatible", true); 
        AddBullet(dictionaryBulletsPool, "InkOrTonerPackSize", true); 
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
        AddBullet(dictionaryBulletsPool, "AcidFree", false);
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
        AddBullet(dictionaryBulletsPool, "PopUpEdge", false);
        AddBullet(dictionaryBulletsPool, "TrueBlockTechnology", false);
        AddBullet(dictionaryBulletsPool, "AdditionalLabels8", false);
        AddBullet(dictionaryBulletsPool, "AdditionalLabels9", false);
        AddBullet(dictionaryBulletsPool, "AdditionalLabels10", false);
        AddBullet(dictionaryBulletsPool, "AdditionalLabels11", false);
        AddBullet(dictionaryBulletsPool, "DesignProSoftware", false);
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
        AddBullet(dictionaryBulletsPool, "DrinkPackSize", true);
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
        // "Notebooks" "Alex K." 
        case var a when a.In("1676378310636165557"):
        AddBullet(dictionaryBulletsPool, "NotebookTypeAndUse", true);
        AddBullet(dictionaryBulletsPool, "NumSheetDimension", true);
        AddBullet(dictionaryBulletsPool, "TrueColor", true);
        AddBullet(dictionaryBulletsPool, "NotebookCoverMaterial", true);
        AddBullet(dictionaryBulletsPool, "Perforation", true);
        AddBullet(dictionaryBulletsPool, "NotebookBinding", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "RecycledContent", true);
        AddBullet(dictionaryBulletsPool, "AcidFree", false);
        AddBullet(dictionaryBulletsPool, "AdditionalRemovableDividers", false);
        AddBullet(dictionaryBulletsPool, "AdditionalPenHolder", false);
        AddBullet(dictionaryBulletsPool, "PostConsumerContent", false);
        break;
        // "Notepads" "Alex K." 
        case var a when a.In("1676393410999220835"):
        AddBullet(dictionaryBulletsPool, "NotepadTypeAndUse", true);
        AddBullet(dictionaryBulletsPool, "NotepadsSheetDimension", true);
        AddBullet(dictionaryBulletsPool, "NumberSheetsPerPadAndPerforation", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "BindingPosition", true);
        AddBullet(dictionaryBulletsPool, "TrueColorAndCoverMateral", true);
        AddBullet(dictionaryBulletsPool, "RecycledContent", true);
        AddBullet(dictionaryBulletsPool, "AcidFree", false);
        AddBullet(dictionaryBulletsPool, "HolePunchedNotepad", false);
        AddBullet(dictionaryBulletsPool, "FormsPerBook", false);
        AddBullet(dictionaryBulletsPool, "PostConsumerContent", false);
        break;
        // "Colored Paper" "Alex K." 
        case var a when a.In("1676385310823140679"):
        AddBullet(dictionaryBulletsPool, "ColoredPaperTypeAndUse", true);
        AddBullet(dictionaryBulletsPool, "NumberOfHoles", true);
        AddBullet(dictionaryBulletsPool, "ColoredPaperSheetDimentions", true);
        AddBullet(dictionaryBulletsPool, "PaperWeight", true);
        AddBullet(dictionaryBulletsPool, "PaperColorTypeAndBrightness", true);
        AddBullet(dictionaryBulletsPool, "TrueColor", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "CompliantStandards", true);
        AddBullet(dictionaryBulletsPool, "RecycledContent", true);
        AddBullet(dictionaryBulletsPool, "AcidFree", false);
        AddBullet(dictionaryBulletsPool, "PostConsumerContent", false);
        break;
        // "Post-it&reg; / Sticky Flags & Tabs" "Alex K." 
        case var a when a.In("167638614736165756"):
        AddBullet(dictionaryBulletsPool, "FlagTabTypeAndUse", true);
        AddBullet(dictionaryBulletsPool, "ColorFamily", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "Messaging", true);
        AddBullet(dictionaryBulletsPool, "FlagOrTabWidthInInches", true);
        AddBullet(dictionaryBulletsPool, "RecycledContent", true);
        AddBullet(dictionaryBulletsPool, "PostConsumerContent", false);
        AddBullet(dictionaryBulletsPool, "CompliantStandards", false);
        AddBullet(dictionaryBulletsPool, "AcidFree", false);
        break;
        // "Label Maker Tapes & Printer Labels" "Alex K." 
        case var a when a.In("1681361910108140557"):
        AddBullet(dictionaryBulletsPool, "LabelMakerTapeWidth", true);
        AddBullet(dictionaryBulletsPool, "PrintTrueColorOnLabelTrueColor", true);
        AddBullet(dictionaryBulletsPool, "CompatibleDevices", true);
        AddBullet(dictionaryBulletsPool, "UseSurfacesApplicationsAdhesiveEtc", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "WaterResistant", false);
        AddBullet(dictionaryBulletsPool, "TearResistant", false);
        AddBullet(dictionaryBulletsPool, "ColdEnvironment", false);
        AddBullet(dictionaryBulletsPool, "OilResistance", false);
        AddBullet(dictionaryBulletsPool, "AcidFree", false);
        AddBullet(dictionaryBulletsPool, "RecycledContent", false);
        AddBullet(dictionaryBulletsPool, "PostConsumerContent", false);
        AddBullet(dictionaryBulletsPool, "CompliantStandards", false);
        break;
            // "Post-it&reg; / Sticky Flags & Tabs" "Alex K." 
        case var a when a.In("167638614736165756"):
        AddBullet(dictionaryBulletsPool, "BinderTypeDurabilityColor", true);
        AddBullet(dictionaryBulletsPool, "Dimensions", true);
        AddBullet(dictionaryBulletsPool, "RingTypeClosure", true);
        AddBullet(dictionaryBulletsPool, "BinderCapacity", true);
        AddBullet(dictionaryBulletsPool, "InputSize", true);
        AddBullet(dictionaryBulletsPool, "MaterialOfItemFinish", true);
        AddBullet(dictionaryBulletsPool, "DesignIncludesPockets", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "RecycledContent", false);
        AddBullet(dictionaryBulletsPool, "IncludedLabels", false);
        AddBullet(dictionaryBulletsPool, "DuraHinge", false);
        AddBullet(dictionaryBulletsPool, "HandleStrap", false);
        AddBullet(dictionaryBulletsPool, "PostConsumerContent", false);
        AddBullet(dictionaryBulletsPool, "AcidFree", false);
        break;
    }
}
