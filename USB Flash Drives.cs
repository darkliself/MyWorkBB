//548610392096142211 "USB Flash Drives" "Alex K." 

USBFlashDriveCapacityAndType(); 
USBDlashDriveDesignColorMaterial(); 
USBFlashDriveInterface(); 
MaximumReadAndWriteSpeed(); 
CompatibleWithOS(); 
FlashDrivesEncryptionAESSHA(); 
USBFlashDriveSecurityFeatures(); 
AdditionalKeyRingLoop(); 
AdditionalLEDAccessIndicator(); 
AdditionalOpertaingTemperature(); 
// Warranty 

// --[FEATURE #1] 
// --USB flash drive capacity & type 
void USBFlashDriveCapacityAndType() { 
var result = ""; 
var capacity = R("SP-18334").HasValue() ? R("SP-18334").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18334").HasValue() ? R("cnet_common_SP-18334").Replace("<NULL>", "").Text : ""; 
//Flash Drive|Encrypted Secure|Wireless|Key Ring|Fashion|Rugged|Water Resistant|Micro|Compact|Portable|Nano|Other|High Speed|External|Basic|Mini 
var flashDriveType = R("SP-14923").HasValue() ? R("SP-14923").Replace("<NULL>", "").Text : 
R("cnet_common_SP-14923").HasValue() ? R("cnet_common_SP-14923").Replace("<NULL>", "").Text : ""; 


if (!String.IsNullOrEmpty(capacity) && !String.IsNullOrEmpty(flashDriveType)) { 
if (flashDriveType.ToLower().Equals("flash drive") || flashDriveType.ToLower().Equals("Other")) { 
if (Coalesce(capacity).In("2GB", "4GB", "8GB", "16GB")) { 
result = $"{capacity} flash drive offers reliable data storage"; 
} else { 
result = $"{capacity} USB flash drive for convenient storage"; 
} 
} 
else { 
if (Coalesce(capacity).In("2GB", "4GB", "8GB", "16GB")) { 
result = $"{capacity} {flashDriveType.ToLower()} flash drive offers reliable data storage"; 
} 
else { 
result = $"{capacity} {flashDriveType.ToLower()} USB flash drive for convenient storage"; 
} 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"USBFlashDriveCapacityAndType⸮{result}"); 
} 
} 

// --[FEATURE #2] 
// --USB flash drive design, True color& USB flash drive material 
void USBDlashDriveDesignColorMaterial() { 
    var result = ""; 
    var design = R("SP-22140").HasValue() ? R("SP-22140").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22140").HasValue() ? R("cnet_common_SP-22140").Replace("<NULL>", "").Text : ""; 
    var color = R("SP-22967").HasValue() ? R("SP-22967").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22967").HasValue() ? R("cnet_common_SP-22967").Replace("<NULL>", "").Text : ""; 
    var material = R("SP-14924").HasValue() ? R("SP-14924").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-14924").HasValue() ? R("cnet_common_SP-14924").Replace("<NULL>", "").Text : ""; 
    if (!String.IsNullOrEmpty(design) 
    && !String.IsNullOrEmpty(color) 
    && !String.IsNullOrEmpty(material)) { 
        if (!color.ToLower().Equals("assorted")) { 
            result = $"{design.ToLower().ToUpperFirstChar()} design, comes in {color.ToLower()}, made of {material.ToLower(true)}"; 
        } else { 
            result = $"{design.ToLower().ToUpperFirstChar()} design, comes in assorted colors, made of {material.ToLower(true)}"; 
        } 
    } 
    //No material 
    else if (!String.IsNullOrEmpty(design) 
    && !String.IsNullOrEmpty(color)) { 
        if (design.ToLower().Equals("swivel")) { 
            if (color.ToLower().Equals("assorted")) { 
                result = "Capless design with a rotating case, comes in assorted colors"; 
            } else { 
                result = $"Capless design with a rotating case, comes in {color.ToLower()}"; 
            } 
        } else { 
            if (color.ToLower().Equals("assoted")) { 
                result = $"{design.ToLower().ToUpperFirstChar()} design, comes in assorted colors"; 
            } else { 
                result = $"{design.ToLower().ToUpperFirstChar()} design, comes in {color.ToLower()}"; 
            } 
        } 
    } 
    //no design 
    else if (!String.IsNullOrEmpty(color) 
    && !String.IsNullOrEmpty(material)) { 
        if (color.ToLower().Equals("assorted")) { 
            result = $"Comes in assorted colors, made of {material.ToLower(true)}"; 
        } else { 
            result = $"Comes in {color.ToLower()}, made of {material.ToLower(true)}"; 
        } 
    } 
    // no color 
    else if (!String.IsNullOrEmpty(design) 
    && !String.IsNullOrEmpty(material)) { 
        result = $"{design.ToLower().ToUpperFirstChar()} design, made of {material.ToLower(true)}"; 
    } 
    else if (!String.IsNullOrEmpty(design)) { 
        result = $"{design.ToLower().ToUpperFirstChar()} design"; 
    } 
    else if (!String.IsNullOrEmpty(color)) { 
        if (color.ToLower().Equals("assorted")) { 
            result = "Comes in assorted colors"; 
        } else { 
            result = $"Comes in {color.ToLower()}"; 
        } 
    } 
    else if (!String.IsNullOrEmpty(material)) { 
        result = $"Made of {material.ToLower(true)}"; 
    } 
    if (!String.IsNullOrEmpty(result)) { 
        Add($"USBDlashDriveDesignColorMaterial⸮{result}"); 
    } 
}

// --[FEATURE #3] 
// --USB flash drive interface 
void USBFlashDriveInterface() { 
var result = ""; 
//Type C|USB|USB 3.0|USB 2.0|USB 3.1 
var flashDriveInterface = "";//"SP-18335"; 
var interfaces = !String.IsNullOrEmpty(flashDriveInterface) ? flashDriveInterface : A[2448].HasValue() ? A[2448].FirstValue().ToString() : ""; 
if (!String.IsNullOrEmpty(interfaces)) { 
if (interfaces.ToLower().Equals("usb 2.0")) { 
result = "USB 2.0 interface is compatible with various USB-ready devices"; 
} 
else if (interfaces.ToLower().Equals("usb 3.0")) { 
result = $"USB 3.0 interface makes transporting and sharing files simple and convenient"; 
} 
else if (interfaces.ToLower().Equals("usb 3.1")) { 
result = $"USB 3.1 interface enables blazing-fast data transferring ability"; 
} 
else if (interfaces.ToLower().Equals("usb c")) { 
result = $"USB Type-C flash drive provides a seamless way to move content between your Type-C devices"; 
} 
else { 
result = $"{interfaces} interface makes transporting and sharing files simple and convenient"; 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"USBFlashDriveInterface⸮{result}"); 
} 
} 
// --[FEATURE #4] 
// --Maximum read & write speed (Mb per seconds) 
void MaximumReadAndWriteSpeed() { 
var result = ""; 
var maximumReadSpeed = R("SP-22260").HasValue() ? R("SP-22260").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22260").HasValue() ? R("cnet_common_SP-22260").Replace("<NULL>", "").Text : ""; 

var maximumWriteSpeed = R("SP-22261").HasValue() ? R("SP-22261").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22261").HasValue() ? R("cnet_common_SP-22261").Replace("<NULL>", "").Text : ""; 
if (!String.IsNullOrEmpty(maximumReadSpeed) 
&& !String.IsNullOrEmpty(maximumWriteSpeed) 
&& Coalesce(maximumReadSpeed).ExtractNumbers().Any()) { 
if (Coalesce(maximumReadSpeed).ExtractNumbers().First() < 45) { 
result = $"{maximumReadSpeed}MB/s read speed and {maximumWriteSpeed}MB/s write speed for quick data access and transfer"; 
} else { 
result = $"{maximumReadSpeed}MB/s read and {maximumWriteSpeed}MB/s write speeds provide swift performance"; 
} 
} 
else if (!String.IsNullOrEmpty(maximumReadSpeed)) { 
if (Coalesce(maximumReadSpeed).ExtractNumbers().First() < 45) { 
result = $"{maximumReadSpeed}MB/s read speed for quick data access and transfer"; 
} else { 
result = $"Up to {maximumReadSpeed}MB/s read speed provides swift performance"; 
} 
} 
else if (!String.IsNullOrEmpty(maximumWriteSpeed)) { 
if (Coalesce(maximumWriteSpeed).ExtractNumbers().First() < 45) { 
result = $"{maximumWriteSpeed}MB/s write speed for quick data access and transfer"; 
} else { 
result = $"Up to {maximumWriteSpeed}MB/s write speed provides swift performance"; 
} 
} 
if(!String.IsNullOrEmpty(result)) { 
Add($"MaximumReadAndWriteSpeed⸮{result}"); 
} 
} 
// --[FEATURE #5] 
// --Compatible With 
void CompatibleWithOS() { 
//var test = new List<string>(){"some pc", "some windows", "some2 android", "some2 android2"}; 
var ckeckedList = new List<string>(){"Windows", "MacOS", "Linux", "Android", "Symbian"}; 
var result = ""; 
var requiredOS = A[429]; 
var compatibility = A[603]; //Deactiveted 
var compWith = R("SP-382").HasValue() ? R("SP-382").Replace("<NULL>", "").Text : 
R("cnet_common_SP-382").HasValue() ? R("cnet_common_SP-382").Replace("<NULL>", "").Text : ""; 

if (requiredOS.HasValue()) { 
var formatted = requiredOS.Values.Select(s => s.Value().ToString()); 
foreach (var item in ckeckedList) { 
formatted = formatted.Select(s => s.Contains(item) ? item : s).Distinct(); 
} 
result = $"Compatible with: {formatted.FlattenWithAnd()}"; 
} 
else if (compatibility.HasValue()) { 
result = $"Compatible with: {compatibility.Values.Select(o => o.Value()).FlattenWithAnd()}"; 
} 
else if (!String.IsNullOrEmpty(compWith)) { 
result = $"Compatible with: {compWith.Split(", ").Distinct().FlattenWithAnd()}"; 
} 
if(!String.IsNullOrEmpty(result)) { 
Add($"CompatibleWithOS⸮{result}"); 
} 
} 

// --[FEATURE #6] 
// --Encryption 
void FlashDrivesEncryptionAESSHA(){ 
    if (A[1997].HasValue("%-bit AES%")) { 
       Add($"FlashDrivesEncryptionAESSHA⸮{A[1997].Where("%-bit AES%", "%SHA%").Select(o => o.Value()).FlattenWithAnd()} encryption protects your data from unauthorized access"); 
    } 
} 

// --[FEATURE #7] 
// --Security features 
void USBFlashDriveSecurityFeatures() { 
    var result = ""; 
    if (A[1997].HasValue("FIPS 140-2 Level 3")) { 
        result = result + "FIPS 140-2 Level ##3, "; 
    } 
    if (A[2909].HasValue("auto-locking")) { 
        result = result + "auto-lock, "; 
    } 
    if (A[344].HasValue("SanDisk SecureAccess")) { 
        result = result + "SanDisk SecureAccess software, "; 
    } 
    if (A[2909].HasValue("password protection")) { 
        result = result + "password security, "; 
    } 
    if (A[344].HasValue("MerlinSafe")) { 
        result = result + "MerlinSafe encryption software, "; 
    } 
    if (A[344].HasValue("EncryptStick Lite")) { 
        result = result + "EncryptStick software"; 
    } 
    if(!String.IsNullOrEmpty(result)) { 
        Add($"USBFlashDriveSecurityFeatures⸮Security features: {Coalesce(result.ToUpperFirstChar()).RegexReplace("(, $)", "")}"); 
    } 
} 

// --[FEATURE #8] 
// --Additional Key Loop 
void AdditionalKeyRingLoop() { 
    if (A[2909].HasValue("key ring loop")) { 
        Add("AdditionalKeyRingLoop⸮The included key loop easily attaches to key chains, so important files are never out of reach"); 
    } 
}  

// --[FEATURE #9] 
// --Additional LED light 

void AdditionalLEDAccessIndicator() { 
    if (A[2909].HasValue("LED access indicator")) { 
       Add("AdditionalLEDAccessIndicator⸮LED activity light that blinks during file transfer, so you know when the job is done"); 
    }
}

// --[FEATURE #10] 
// --Additional Opertaing temperature 
void AdditionalOpertaingTemperature() { 
    if (A[358].HasValue() && A[359].HasValue()) { 
        Add($"AdditionalOpertaingTemperature⸮Temperature: {A[358].FirstValueUsm()} to {A[359].FirstValueUsm()} degrees F (operating)");
    } 
} 

// --[FEATURE #12] 
// --Languages Supported Warranty 

//548610392096142211 end of "USB Flash Drives" §§ 