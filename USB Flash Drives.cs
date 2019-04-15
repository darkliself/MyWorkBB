//§§548610392096142211 "USB Flash Drives" "Alex K."

USBFlashDriveCapacityAndType();
USBDlashDriveDesignColorMaterial();
USBFlashDriveInterface();
MaximumReadAndWriteSpeed();
CompatibleWithOS();

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
void USBDlashDriveDesignColorMaterial () {
    var result = "";
    var design = R("SP-22140").HasValue() ? R("SP-22140").Replace("<NULL>", "").Text :
		R("cnet_common_SP-22140").HasValue() ? R("cnet_common_SP-22140").Replace("<NULL>", "").Text : "";
    var color = R("SP-22967").HasValue() ? R("SP-22967").Replace("<NULL>", "").Text :
		R("cnet_common_SP-22967").HasValue() ? R("cnet_common_SP-22967").Replace("<NULL>", "").Text : "";
    var material = R("SP-14924").HasValue() ? R("SP-14924").Replace("<NULL>", "").Text :
		R("cnet_common_SP-14924").HasValue() ? R("cnet_common_SP-14924").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(design) 
    && !String.IsNullOrEmpty(color) 
    && !String.IsNullOrEmpty(material))  {
        if (!color.ToLower().Equals("assorted")) {
            result = $"{design.ToLower().ToUpperFirstChar()} designed {color.ToLower()} flash drive is made of {material.ToLower()}";
        } else {
            result = $"{design.ToLower().ToUpperFirstChar()} designed flash drives in assorted colors are made of {material.ToLower()}";
        }
    }  
    //No material
    else if (!String.IsNullOrEmpty(design)
    && !String.IsNullOrEmpty(color)) {
        if (design.ToLower().Equals("swivel")) {
            if (color.ToLower().Equals("assorted")) {
                result = "Flash drives in assorted colors have a capless design with a rotating case";
            } else {
                result = $"Flash drive in {color.ToLower()} has a capless design with a rotating case";
            }
        } else {
            if (color.ToLower().Equals("assoted")) {
                result = $"{design.ToLower().ToUpperFirstChar()} designed assorted colors flash drives";
            } else {
                result = $"{design.ToLower().ToUpperFirstChar()} designed {color.ToLower()} flash drive";
            }
        }
    } 
    //no design
    else if (!String.IsNullOrEmpty(color) 
    && !String.IsNullOrEmpty(material))  {
        if (color.ToLower().Equals("assorted")) {
            result = $"Comes in assorted colors and made of {material.ToLower()}";
        } else {
            result = $"Comes in {color.ToLower()} and made of {material.ToLower()}";
        }
    }
    // no color
    else if (!String.IsNullOrEmpty(design) 
    && !String.IsNullOrEmpty(material))  {
        result = $"{design.ToLower().ToUpperFirstChar()} designed flash drive is made of {material.ToLower()}";    
    }
    else if (!String.IsNullOrEmpty(design)) {
        result = $"{design.ToLower().ToUpperFirstChar()} designed flash drive";
    }
    else if (!String.IsNullOrEmpty(color)) {
        if (color.ToLower().Equals("assorted")) {
            result = "Comes in assorted colors";
        } else {
            result = $"Comes in {color.ToLower()}";
        }
    }
    else if (!String.IsNullOrEmpty(material)) {
        result = $"Made of {material.ToLower()}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"USBDlashDriveDesignColorMaterial⸮{result}");
    }
}

// --[FEATURE #3]
// --USB flash drive interface
void USBFlashDriveInterface () {
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
    var compWith = "SP-382";
    Add(requiredOS.Values.Select(o => o.Value()).FlattenWithAnd());
    
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

// --[FEATURE #7]
// --Security features

// --[FEATURE #8]
// --

// --[FEATURE #9]
// --

// --[FEATURE #10]
// --

// --[FEATURE #11]
// --

// --[FEATURE #12]
// --Languages Supported	Warranty

//§§548610392096142211 end of "USB Flash Drives"

						
https://www.staples.com/SanDisk-Ultra-Type-C-USB-3-1-Flash-Drive-16GB-Black-Silver-SDCZ450-016G-A46/product_IM17T4312
USB Type C connector is completely reversible, there’s no wrong way to plug it in

https://www.staples.com/SanDisk-Ultra-Dual-USB-Type-C-USB-3-1-Flash-Drive-32GB-Black-Gray-SDDDC2-032G-A46/product_IM18A5705
Interface: USB type-C, USB 3.1

//128-bit AES encryption software to protect your files
https://www.staples.com/sandisk-ultra-usb-3-0-flash-drive-128gb-black-sdcz48-128g-a46/product_1568885?akamai-feo=off

It provides 256-bit AES encryption to protect your data from unauthorized access
https://www.staples.com/Lexar-S45-64GB-LJDS45-64GABNL/product_2071237?akamai-feo=off


https://www.staples.com/SanDisk-iXpand-128GB-Lightning-USB-3-0-Flash-Drive-Silver-Black-SDIX30C-128G-AN6NE/product_IM17M9765
Encryption: AES 128-bit