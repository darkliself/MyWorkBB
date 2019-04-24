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
        // "Laptop Bags & Cases" "Serhii.O" 
        case var a when a.In("5884389110931140978"):
        AddBullet(dictionaryBulletsPool, "LaptopBagCasestyleUse", true);
        AddBullet(dictionaryBulletsPool, "TrueColorMaterial", true);
        AddBullet(dictionaryBulletsPool, "LaptopBagsCasesStrapHandleConfiguration", true);
        AddBullet(dictionaryBulletsPool, "Dimensions", true);
        AddBullet(dictionaryBulletsPool, "LaptopBagsCasesInnerDimensions", true);
        AddBullet(dictionaryBulletsPool, "LaptopBagsCasesBottleHolder", true);
        AddBullet(dictionaryBulletsPool, "LaptopBagsCasesDetailCompartments", true);
        AddBullet(dictionaryBulletsPool, "LaptopBagsCasesWheeled", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "LaptopBagsCasesCheckPointFriendly", false);
        AddBullet(dictionaryBulletsPool, "LaptopBagsCasesWaterResistant", false);
        AddBullet(dictionaryBulletsPool, "LaptopBagsCasesAntiScratch", false);
        AddBullet(dictionaryBulletsPool, "Warranty", false);
        break;
        // "Headphones" "Serhii.O" 
        case var a when a.In("5829362510113210844"):
        AddBullet(dictionaryBulletsPool, "HeadphonesTypeUse", true);
        AddBullet(dictionaryBulletsPool, "HeadphonesConnectivityOperationalRange", true);
        AddBullet(dictionaryBulletsPool, "HeadphonesInterfaceBluetoothCompatibility", true);
        AddBullet(dictionaryBulletsPool, "HeadphonesConstruction", true);
        AddBullet(dictionaryBulletsPool, "HeadphonesBatteryType", true);
        AddBullet(dictionaryBulletsPool, "HeadphonesRechargeBattery", true);
        AddBullet(dictionaryBulletsPool, "HeadphonesPowerSourceRechargeTimeBatteryLife", true);
        AddBullet(dictionaryBulletsPool, "HeadphonesBuiltRemote", false);
        AddBullet(dictionaryBulletsPool, "HeadphonesBuiltMicAndControls", false);
        AddBullet(dictionaryBulletsPool, "HeadphonesTechnologyFeature", false);
        AddBullet(dictionaryBulletsPool, "HeadphonesEarParts", false);
        AddBullet(dictionaryBulletsPool, "HeadphonesPushToTalk", false);
        AddBullet(dictionaryBulletsPool, "HeadphonesBuiltInMicrophone", false);
        AddBullet(dictionaryBulletsPool, "HeadphonesSoundIsolating", false);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "Cell Phone Cases" "Serhii.O" 
        case var a when a.In("530868304206053"):
        AddBullet(dictionaryBulletsPool, "CellPhoneCaseStyleCompatibility", true);
        AddBullet(dictionaryBulletsPool, "CellPhoneCaseMaterialtrueColor", true);
        AddBullet(dictionaryBulletsPool, "CellPhoneCaseGripType", true);
        AddBullet(dictionaryBulletsPool, "CellPhoneCaseFashionCase", false);
        AddBullet(dictionaryBulletsPool, "CellPhoneCasesScreenProtector", false);
        AddBullet(dictionaryBulletsPool, "CellPhoneCasesProtections", false);
        AddBullet(dictionaryBulletsPool, "CellPhoneCasesRaisedEdge", false);
        AddBullet(dictionaryBulletsPool, "CellPhoneCasesMagnetic", false);
        AddBullet(dictionaryBulletsPool, "CellPhoneCasesLayerDefense", false);
        AddBullet(dictionaryBulletsPool, "CellPhoneCasesUnderwater", false);
        AddBullet(dictionaryBulletsPool, "CellPhoneCasesMicrofiber", false);
        AddBullet(dictionaryBulletsPool, "CellPhoneCasesUltraSlim", false);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "Projectors" "Serhii.O" 
        case var a when a.In("168134749830140542"):
        AddBullet(dictionaryBulletsPool, "ProjectorsTypeUse", true);
        AddBullet(dictionaryBulletsPool, "ProjectorsSystem", true);
        AddBullet(dictionaryBulletsPool, "ProjectorsColorBrightness", true);
        AddBullet(dictionaryBulletsPool, "ProjectorsResolution", true);
        AddBullet(dictionaryBulletsPool, "ProjectorsLampTypeLife", true);
        AddBullet(dictionaryBulletsPool, "ProjectorsLensFocusType", true);
        AddBullet(dictionaryBulletsPool, "ProjectorsProjectionDistance", true);
        AddBullet(dictionaryBulletsPool, "ProjectorsThrowRatio", true);
        AddBullet(dictionaryBulletsPool, "ProjectorsAudioSupport", true);
        AddBullet(dictionaryBulletsPool, "ProjectorsConnectivity", true);
        AddBullet(dictionaryBulletsPool, "ProjectorsCompatibility", true);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "Vacuums" "Serhii.O" 
        case var a when a.In("1683392410972140733"):
        AddBullet(dictionaryBulletsPool, "VacuumsTypeUse", true);
        AddBullet(dictionaryBulletsPool, "VacuumsBagless", true);
        AddBullet(dictionaryBulletsPool, "VacuumsFiltrationSystem", true);
        AddBullet(dictionaryBulletsPool, "VacuumsLightingPowerType", true);
        AddBullet(dictionaryBulletsPool, "VacuumsPowerVoltage", true);
        AddBullet(dictionaryBulletsPool, "VacuumsCordLength", true);
        AddBullet(dictionaryBulletsPool, "CompliantStandards", true);
        AddBullet(dictionaryBulletsPool, "Dimensions", false);
        AddBullet(dictionaryBulletsPool, "AdditionalVacuumsMotorDetails", false);
        AddBullet(dictionaryBulletsPool, "AdditionalVacuumsBatteries", false);
        AddBullet(dictionaryBulletsPool, "AdditionalVacuumsCordlessDesign", false);
        AddBullet(dictionaryBulletsPool, "AdditionalVacuumsFingertipControls", false);
        AddBullet(dictionaryBulletsPool, "AdditionalVacuumsIndicators", false);
        AddBullet(dictionaryBulletsPool, "AdditionalVacuumsSwivelSteering", false);
        AddBullet(dictionaryBulletsPool, "AdditionalVacuumsBrushRoll", false);
        AddBullet(dictionaryBulletsPool, "AdditionalVacuumsEdgeCreviceTool", false);
        AddBullet(dictionaryBulletsPool, "AdditionalVacuumsCyclonicFiltration", false);
        AddBullet(dictionaryBulletsPool, "AdditionalVacuumsCarpetHeightAdjustments", false);
        AddBullet(dictionaryBulletsPool, "AdditionalVacuumsEdgeCleaning", false);
        AddBullet(dictionaryBulletsPool, "AdditionalVacuumsPackageContent", false);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "Chargers & Connectors" "Serhii.O" 
        case var a when a.In("530868304162051"):
        AddBullet(dictionaryBulletsPool, "ChargerTypeUse", true);
        AddBullet(dictionaryBulletsPool, "Dimensions", true);
        AddBullet(dictionaryBulletsPool, "ChargersConnectorsConnections", true);
        AddBullet(dictionaryBulletsPool, "ChargersConnectorsCompatibility", true);
        AddBullet(dictionaryBulletsPool, "ChargersConnectorsAudioFeatureOrVoltage", true);
        AddBullet(dictionaryBulletsPool, "ChargersConnectorsVideoFeatureOrAdditionalChargerData", true);
        AddBullet(dictionaryBulletsPool, "ChargersConnectorsDataFeatureOrAdditionalChargerData", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "ChargersConnectorsKitContent", false);
        AddBullet(dictionaryBulletsPool, "ChargersConnectorsFastCharging", false);
        AddBullet(dictionaryBulletsPool, "ChargersConnectorsBraided", false);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "Dry Erase Whiteboards" "Serhii.O" 
        case var a when a.In("11012078166382"):
        AddBullet(dictionaryBulletsPool, "DryEraseWhiteboardTypeUse", true);
        AddBullet(dictionaryBulletsPool, "WhiteboardsOverallDimensions", true);
        AddBullet(dictionaryBulletsPool, "WhiteboardsSurfaceMaterial", true);
        AddBullet(dictionaryBulletsPool, "WhiteboardsFrameConstruction", true);
        AddBullet(dictionaryBulletsPool, "WhiteboardsDesign", true);
        AddBullet(dictionaryBulletsPool, "WhiteboardsAssemblyInformation", true);
        AddBullet(dictionaryBulletsPool, "WhiteboardsPackageContents", true);
        AddBullet(dictionaryBulletsPool, "AdditionalWhiteboardsStainResistant", false);
        AddBullet(dictionaryBulletsPool, "AdditionalWhiteboardsBoardDesign", false);
        AddBullet(dictionaryBulletsPool, "AdditionalWhiteboardsMarkerTrayHolds", false);
        AddBullet(dictionaryBulletsPool, "AdditionalWhiteboardsMarkerGhostingResistant", false);
        AddBullet(dictionaryBulletsPool, "AdditionalWhiteboardsMagnetic", false);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "Printers Laser and Other" "Serhii.O" 
        case var a when a.In("5435445243142044", "5435445243167883"):
        AddBullet(dictionaryBulletsPool, "PrintersTypeOfPrinterUse", true);
        AddBullet(dictionaryBulletsPool, "PrintersPrintTechnologyResolution", true);
        AddBullet(dictionaryBulletsPool, "PrintersPrintSpeedMode", true);
        AddBullet(dictionaryBulletsPool, "PrintersConnectivity", true);
        AddBullet(dictionaryBulletsPool, "Dimensions", true);
        AddBullet(dictionaryBulletsPool, "PrintersInputLoadingFeature", true);
        AddBullet(dictionaryBulletsPool, "PrintersProcessorMemory", true);
        AddBullet(dictionaryBulletsPool, "AutomaticPrintingFeatures", true);
        AddBullet(dictionaryBulletsPool, "PrintersDisplay", true);
        AddBullet(dictionaryBulletsPool, "PrintersScanningCapabilities", true);
        AddBullet(dictionaryBulletsPool, "CompliantStandards", true);
        AddBullet(dictionaryBulletsPool, "LargeFormatPrinter", false);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "Printers All-in-One, Inkjet" "Serhii.O" 
        case var a when a.In("5435445243161518", "543544524340300"):
        AddBullet(dictionaryBulletsPool, "PrintersTypeOfPrinterUse", true);
        AddBullet(dictionaryBulletsPool, "PrintersPrintTechnologyResolution", true);
        AddBullet(dictionaryBulletsPool, "PrintersPrintSpeedMode", true);
        AddBullet(dictionaryBulletsPool, "PrintersConnectivity", true);
        AddBullet(dictionaryBulletsPool, "Dimensions", true);
        AddBullet(dictionaryBulletsPool, "PrintersInputLoadingFeature", true);
        AddBullet(dictionaryBulletsPool, "PrintersProcessorMemory", true);
        AddBullet(dictionaryBulletsPool, "CompliantStandards", true);
        AddBullet(dictionaryBulletsPool, "AutomaticPrintingFeatures", true);
        AddBullet(dictionaryBulletsPool, "PrintersDisplay", true);
        AddBullet(dictionaryBulletsPool, "PrintersScanningCapabilities", true);
        AddBullet(dictionaryBulletsPool, "LargeFormatPrinter", false);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        // "Tablets & iPads" "Serhii.O" 
        case var a when a.In("532628898138204449"): 
        AddBullet(dictionaryBulletsPool, "TabletsSizeAndUse", true); 
        AddBullet(dictionaryBulletsPool, "ScreenSizeDisplayTypeAndScreenResolution", true); 
        AddBullet(dictionaryBulletsPool, "TabletsProcessor", true); 
        AddBullet(dictionaryBulletsPool, "TabletsOperatingSystem", true); 
        AddBullet(dictionaryBulletsPool, "TabletsMemory", true); 
        AddBullet(dictionaryBulletsPool, "TabletsBatteryLife", false); 
        AddBullet(dictionaryBulletsPool, "TabletsWirelessConnectivity", false); 
        AddBullet(dictionaryBulletsPool, "TabletsCameraDetails", false); 
        AddBullet(dictionaryBulletsPool, "TabletsAudioSpeakerInfo", false); 
        AddBullet(dictionaryBulletsPool, "TabletsImputsAndOutputs", false); 
        AddBullet(dictionaryBulletsPool, "Dimensions", false); 
        AddBullet(dictionaryBulletsPool, "Warranty", true); 
        break; 
        // "Printer Parts" "Serhii.O" 
        case var a when a.In("54353597404140828"): 
        AddBullet(dictionaryBulletsPool, "PrinterPartTypeUse", true); 
        AddBullet(dictionaryBulletsPool, "ShippingDimensions", true); 
        AddBullet(dictionaryBulletsPool, "PrinterPartCompatibility", true); 
        AddBullet(dictionaryBulletsPool, "TrueColorMaterial", true); 
        AddBullet(dictionaryBulletsPool, "PackSize", true); 
        AddBullet(dictionaryBulletsPool, "OffersDutyCycle", false); 
        AddBullet(dictionaryBulletsPool, "RecycledPostConsumerContent", false);
        AddBullet(dictionaryBulletsPool, "CompliantStandards", false);
        AddBullet(dictionaryBulletsPool, "Warranty", true); 
        break; 
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
        AddBullet(dictionaryBulletsPool, "TearResistantFilingAccessory", false);
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
        AddBullet(dictionaryBulletsPool, "SensitivityHeadsetMicrophone", true);
        AddBullet(dictionaryBulletsPool, "MicrophonesFrequency", true);
        AddBullet(dictionaryBulletsPool, "ImpedanceHeadsetMicrophone", true);
        AddBullet(dictionaryBulletsPool, "PowerSource", true);
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
        AddBullet(dictionaryBulletsPool, "AdditionalPortsInterface", true);
        AddBullet(dictionaryBulletsPool, "Dimensions", true);
        AddBullet(dictionaryBulletsPool, "BoxContentsOfDesktop", true);
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
        case var a when a.In("5226058", "5226054", "5226006", "12393811005220973", "5226157", "12366910285216553", "1212164913166147", "12378610641217427"):
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
        // "Markers", "Art & School Markers" "Alex K." 
        case var a when a.In("1114953140896", "5787384610799221591"):
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
        // "Binders" "Alex K." 
        case var a when a.In("135293970167205"):
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
        // "Label Makers" "Alex K." 
        case var a when a.In("168136191010890400"):
        AddBullet(dictionaryBulletsPool, "LabelMakerTypeAndUse", true);
        AddBullet(dictionaryBulletsPool, "LabelMakerMaxPrintSpeed", true);
        AddBullet(dictionaryBulletsPool, "LabelMakerMaximumResolution", true);
        AddBullet(dictionaryBulletsPool, "LabelMakerPrintingTechnology", true);
        AddBullet(dictionaryBulletsPool, "MediaSupportedTypeOfLabels", true);
        AddBullet(dictionaryBulletsPool, "InterfaceOrPortType", true);
        AddBullet(dictionaryBulletsPool, "ScreenSizeInInches", true);
        AddBullet(dictionaryBulletsPool, "OutputTypeFontSizeStyles", true);
        AddBullet(dictionaryBulletsPool, "LabelMakerConnectionType", true);
        AddBullet(dictionaryBulletsPool, "Dimensions", true);
        AddBullet(dictionaryBulletsPool, "LabelMakerBatterySize", true);
        AddBullet(dictionaryBulletsPool, "Warranty", false);
        break;
         // "Batteries" "Alex K." 
        case var a when a.In("16831375514580302"):
        AddBullet(dictionaryBulletsPool, "BatterySize", true);
        AddBullet(dictionaryBulletsPool, "BatteryType", true);
        AddBullet(dictionaryBulletsPool, "Rechargeable", true);
        AddBullet(dictionaryBulletsPool, "BatteryCapacity", true);
        AddBullet(dictionaryBulletsPool, "BatteryUse", true);
        AddBullet(dictionaryBulletsPool, "AdditionalHiDensity", false);
        AddBullet(dictionaryBulletsPool, "AdditionalPowerCheck", false);
        AddBullet(dictionaryBulletsPool, "AdditionalDuralockPowerPreserve", false);
        AddBullet(dictionaryBulletsPool, "AdditionalM3Technology", false);
        AddBullet(dictionaryBulletsPool, "Warranty", false);
        break;
        // "Hand Soap & Dispensers" "Alex K." "Alex K." 
        case var a when a.In("1683393110991141836"):
        AddBullet(dictionaryBulletsPool, "CleanserFormFactorAndUse", true);
        AddBullet(dictionaryBulletsPool, "SoapDispensersScent", true);
        AddBullet(dictionaryBulletsPool, "CapacityAndContainerTypes", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "SoapDispensersAntibacterial", false);
        AddBullet(dictionaryBulletsPool, "Moisturizer", false);
        AddBullet(dictionaryBulletsPool, "Hypoallergenic", false);
        AddBullet(dictionaryBulletsPool, "StainRemoval", false);
        AddBullet(dictionaryBulletsPool, "pHBalaced", false);
        AddBullet(dictionaryBulletsPool, "SoapDispensersVitamins", false);
        AddBullet(dictionaryBulletsPool, "AloeGlycerin", false);
        AddBullet(dictionaryBulletsPool, "Pumice", false);
        AddBullet(dictionaryBulletsPool, "LockingMechanism", false);
        AddBullet(dictionaryBulletsPool, "ContainerFeatures", false);
        AddBullet(dictionaryBulletsPool, "DispenserMaterial", false);
        AddBullet(dictionaryBulletsPool, "AutomaticManualDispensers", false);
        AddBullet(dictionaryBulletsPool, "CompliantStandards", false);
        AddBullet(dictionaryBulletsPool, "Warranty", false);
        break;
        // "Card Files, Cases & Holders" "Alex K." 
        case var a when a.In("1221973163092"):
        AddBullet(dictionaryBulletsPool, "CardFileCaseAndHolderTypeAndUse", true);
        AddBullet(dictionaryBulletsPool, "TrueColorMaterial", true);
        AddBullet(dictionaryBulletsPool, "CardSizeAndCapacity", true);
        AddBullet(dictionaryBulletsPool, "Dimensions", true);
        AddBullet(dictionaryBulletsPool, "IndexTypes", true);
        AddBullet(dictionaryBulletsPool, "PackSize", true);
        AddBullet(dictionaryBulletsPool, "RecycledContent", false);
        AddBullet(dictionaryBulletsPool, "RoundShapeCorners", false);
        AddBullet(dictionaryBulletsPool, "FlexibleDividers", false);
        AddBullet(dictionaryBulletsPool, "SecureClosure", false);
        AddBullet(dictionaryBulletsPool, "LabelHolder", false);
        AddBullet(dictionaryBulletsPool, "TransparentDesign", false);
        AddBullet(dictionaryBulletsPool, "PostConsumerContent", false);
        break;
        // "Trash Cans & Waste Receptacles" "Alex K." 
        case var a when a.In("168328278181141928"):
        AddBullet(dictionaryBulletsPool, "TypeOfTrashCanAndUse", true);
        AddBullet(dictionaryBulletsPool, "TrashCanCapacity", true);
        AddBullet(dictionaryBulletsPool, "TrueColorMaterial", true);
        AddBullet(dictionaryBulletsPool, "Dimensions", true);
        AddBullet(dictionaryBulletsPool, "LidType", true);
        AddBullet(dictionaryBulletsPool, "RimBorderDetails", true);
        AddBullet(dictionaryBulletsPool, "HandleDetails", true);
        AddBullet(dictionaryBulletsPool, "SafetyFeatures", true);
        AddBullet(dictionaryBulletsPool, "CompliantStandards", true);
        AddBullet(dictionaryBulletsPool, "SlideLockSecurity", false);
        AddBullet(dictionaryBulletsPool, "HandsFreeOperation", false);
        AddBullet(dictionaryBulletsPool, "TrashCansPedal", false);
        AddBullet(dictionaryBulletsPool, "FingerPrintResistantCoating", false);
        AddBullet(dictionaryBulletsPool, "CarbonFilterGate", false);
        AddBullet(dictionaryBulletsPool, "TrashCanBase", false);
        AddBullet(dictionaryBulletsPool, "StayOpenFunction", false);
        AddBullet(dictionaryBulletsPool, "LinerPocket", false);
        AddBullet(dictionaryBulletsPool, "RemovableInnerBucket", false);
        AddBullet(dictionaryBulletsPool, "ShoxSilentLid", false);
        AddBullet(dictionaryBulletsPool, "VentingChannels", false);
        AddBullet(dictionaryBulletsPool, "InternalHinge", false);
        AddBullet(dictionaryBulletsPool, "TrashCanWheels", false);
        AddBullet(dictionaryBulletsPool, "TrashCanPowered", false);
        break;
        ////////// "Backpacks" "Alex K." "Serhii.O" "have some tmp features for SKU 21863890" 
        case var a when a.In("5884203981142745"):
        AddBullet(dictionaryBulletsPool, "BackpackStyleAndUse", true);
        AddBullet(dictionaryBulletsPool, "TrueColorMaterial", true);
        AddBullet(dictionaryBulletsPool, "LaptopCompatibleAndSize", false);
        AddBullet(dictionaryBulletsPool, "BackpackSize", true);
        AddBullet(dictionaryBulletsPool, "NumberOfCompartments", true);
        AddBullet(dictionaryBulletsPool, "BackpacksWeightCapacity", true);
        AddBullet(dictionaryBulletsPool, "IsWheeled", true);
        AddBullet(dictionaryBulletsPool, "ShockAbsorbingShoulderStraps", false);
        AddBullet(dictionaryBulletsPool, "AirFlowBackPadding", false);
        AddBullet(dictionaryBulletsPool, "QuikPocket", false);
        AddBullet(dictionaryBulletsPool, "BackpackFrontPocket", false);
        AddBullet(dictionaryBulletsPool, "BackpackOrganizer", false);
        AddBullet(dictionaryBulletsPool, "BackpacSidePocket", false);
        AddBullet(dictionaryBulletsPool, "CaseBaseStabilizingPlatform", false);
        AddBullet(dictionaryBulletsPool, "PaddedShoulderStrap", false);
        AddBullet(dictionaryBulletsPool, "AdjustableShoulderStrap", false);
        AddBullet(dictionaryBulletsPool, "TopCarryHandle", false);
        AddBullet(dictionaryBulletsPool, "BackpackHandGrip", false);
        AddBullet(dictionaryBulletsPool, "AdditionalStraps", false);
        AddBullet(dictionaryBulletsPool, "Warranty", true);
        break;
        ///////// "END OF TEMP BACKPACKS" 
    }
}