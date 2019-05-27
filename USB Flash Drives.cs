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
    var capacity = REQ.GetVariable("SP-18334").HasValue() ? REQ.GetVariable("SP-18334") : R("SP-18334").HasValue() ? R("SP-18334") : R("cnet_common_SP-18334");
    //Flash Drive|Encrypted Secure|Wireless|Key Ring|Fashion|Rugged|Water Resistant|Micro|Compact|Portable|Nano|Other|High Speed|External|Basic|Mini 
    var flashDriveType = REQ.GetVariable("SP-14923").HasValue() ? REQ.GetVariable("SP-14923") : R("SP-14923").HasValue() ? R("SP-14923") : R("cnet_common_SP-14923");
    
    if (capacity.HasValue() && flashDriveType.HasValue()) { 
        if (flashDriveType.HasValue("flash drive") || flashDriveType.HasValue("Other")) { 
            if (Coalesce(capacity).In("2GB", "4GB", "8GB", "16GB")) { 
                Add($"USBFlashDriveCapacityAndType⸮{capacity} flash drive offers reliable data storage"); 
            } else { 
                Add($"USBFlashDriveCapacityAndType⸮{capacity} USB flash drive for convenient storage"); 
            } 
        } 
        else { 
            if (Coalesce(capacity).In("2GB", "4GB", "8GB", "16GB")) { 
                Add($"USBFlashDriveCapacityAndType⸮{capacity} {flashDriveType.ToLower(true)} flash drive offers reliable data storage"); 
            } 
            else { 
               Add($"USBFlashDriveCapacityAndType⸮{capacity} {flashDriveType.ToLower(true)} USB flash drive for convenient storage"); 
            } 
        } 
    } 
}

// --[FEATURE #2] 
// --USB flash drive design, True color& USB flash drive material 
void USBDlashDriveDesignColorMaterial() { 
    var design = REQ.GetVariable("SP-22140").HasValue() ? REQ.GetVariable("SP-22140") : R("SP-22140").HasValue() ? R("SP-22140") : R("cnet_common_SP-22140");
    var color = REQ.GetVariable("SP-22967").HasValue() ? REQ.GetVariable("SP-22967") : R("SP-22967").HasValue() ? R("SP-22967") : R("cnet_common_SP-22967");
    var material = REQ.GetVariable("SP-14924").HasValue() ? REQ.GetVariable("SP-14924") : R("SP-14924").HasValue() ? R("SP-14924") : R("cnet_common_SP-14924");
    if (design.HasValue() 
    && color.HasValue() 
    && material.HasValue()) { 
        if (!color.HasValue("assorted")) { 
            Add($"USBDlashDriveDesignColorMaterial⸮{design.ToLower().ToUpperFirstChar()} design, comes in {color.ToLower()}, made of {material.ToLower(true)}"); 
        } else { 
            Add($"USBDlashDriveDesignColorMaterial⸮{design.ToLower().ToUpperFirstChar()} design, comes in assorted colors, made of {material.ToLower(true)}"); 
        } 
    } 
    //No material 
    else if (design.HasValue() 
    && color.HasValue()) { 
        if (design.HasValue("swivel")) { 
            if (color.HasValue("assorted")) { 
                Add($"USBDlashDriveDesignColorMaterial⸮Capless design with a rotating case, comes in assorted colors"); 
            } else { 
                Add($"USBDlashDriveDesignColorMaterial⸮Capless design with a rotating case, comes in {color.ToLower()}"); 
            } 
        } else { 
            if (color.HasValue()) { 
                Add($"USBDlashDriveDesignColorMaterial⸮{design.ToLower().ToUpperFirstChar()} design, comes in assorted colors"); 
            } else { 
                Add($"USBDlashDriveDesignColorMaterial⸮{design.ToLower().ToUpperFirstChar()} design, comes in {color.ToLower()}"); 
            } 
        } 
    } 
    //no design 
    else if (color.HasValue() 
    && material.HasValue()) { 
        if (color.HasValue("assorted")) { 
            Add($"USBDlashDriveDesignColorMaterial⸮Comes in assorted colors, made of {material.ToLower(true)}"); 
        } else { 
            Add($"USBDlashDriveDesignColorMaterial⸮Comes in {color.ToLower()}, made of {material.ToLower(true)}"); 
        } 
    } 
    // no color 
    else if (design.HasValue() 
    && material.HasValue()) { 
        Add($"USBDlashDriveDesignColorMaterial⸮{design.ToLower().ToUpperFirstChar()} design, made of {material.ToLower(true)}");
    } 
    else if (design.HasValue()) { 
        Add($"USBDlashDriveDesignColorMaterial⸮{design.ToLower().ToUpperFirstChar()} design"); 
    } 
    else if (color.HasValue()) { 
        if (color.HasValue("assorted")) { 
            Add($"USBDlashDriveDesignColorMaterial⸮Comes in assorted colors"); 
        } else { 
            Add($"USBDlashDriveDesignColorMaterial⸮Comes in {color.ToLower()}"); 
        } 
    } 
    else if (material.HasValue()) { 
        Add($"USBDlashDriveDesignColorMaterial⸮Made of {material.ToLower(true)}"); 
    } 
}

// --[FEATURE #3] 
// --USB flash drive interface 
void USBFlashDriveInterface() { 
    //Type C|USB|USB 3.0|USB 2.0|USB 3.1 
    var interfaces = REQ.GetVariable("SP-18335").HasValue() ? REQ.GetVariable("SP-18335") : R("SP-18335").HasValue() ? R("SP-18335") : R("cnet_common_SP-18335");
    if (interfaces.HasValue("usb 2.0") || A[2448].HasValue("usb 2.0")) { 
        Add($"USBFlashDriveInterfaceUSB 2.0 interface is compatible with various USB-ready devices"); 
    }
    else if (interfaces.HasValue("usb 3.0") || A[2448].HasValue("usb 3.0")) { 
        Add($"USBFlashDriveInterfaceUSB 3.0 interface makes transporting and sharing files simple and convenient"); 
    } 
    else if (interfaces.HasValue("usb 3.1") || A[2448].HasValue("usb 3.1")) { 
        Add($"USBFlashDriveInterfaceUSB 3.1 interface enables blazing-fast data transferring ability"); 
    } 
    else if (interfaces.HasValue("usb c") || A[2448].HasValue("usb c")) { 
        Add($"USBFlashDriveInterfaceUSB Type-C flash drive provides a seamless way to move content between your Type-C devices"); 
    } 
    else if (interfaces.HasValue()) { 
        Add($"USBFlashDriveInterface{interfaces.ToUpperFirstChar()} interface makes transporting and sharing files simple and convenient"); 
    }
    else if (A[2448].HasValue()) {
        Add($"USBFlashDriveInterface{A[2448].FirstValue()} interface makes transporting and sharing files simple and convenient");
    }
} 


// --[FEATURE #4] 
// --Maximum read & write speed (Mb per seconds) 
void MaximumReadAndWriteSpeed() { 
    var maximumReadSpeed = REQ.GetVariable("SP-22260").HasValue() ? REQ.GetVariable("SP-22260") : R("SP-22260").HasValue() ? R("SP-22260") : R("cnet_common_SP-22260");
    var maximumWriteSpeed = REQ.GetVariable("SP-22261").HasValue() ? REQ.GetVariable("SP-22261") : R("SP-22261").HasValue() ? R("SP-22261") : R("cnet_common_SP-22261");

    if (maximumReadSpeed.HasValue() 
    && maximumWriteSpeed.HasValue() 
    && maximumReadSpeed.ExtractNumbers().Any()) { 
        if (maximumReadSpeed.ExtractNumbers().First() < 45) { 
            Add($"MaximumReadAndWriteSpeed⸮{maximumReadSpeed}MB/s read speed and {maximumWriteSpeed}MB/s write speed for quick data access and transfer"); 
        } else { 
            Add($"MaximumReadAndWriteSpeed⸮{maximumReadSpeed}MB/s read and {maximumWriteSpeed}MB/s write speeds provide swift performance"); 
        } 
    } 
    else if (maximumReadSpeed.HasValue()) { 
        if (maximumReadSpeed.ExtractNumbers().First() < 45) { 
            Add($"MaximumReadAndWriteSpeed⸮{maximumReadSpeed}MB/s read speed for quick data access and transfer"); 
        } else { 
            Add($"MaximumReadAndWriteSpeed⸮Up to {maximumReadSpeed}MB/s read speed provides swift performance"); 
        } 
    } 
    else if (maximumWriteSpeed.HasValue()) { 
        if (maximumWriteSpeed.ExtractNumbers().First() < 45) { 
            Add($"MaximumReadAndWriteSpeed⸮{maximumWriteSpeed}MB/s write speed for quick data access and transfer"); 
        } else { 
            Add($"MaximumReadAndWriteSpeed⸮Up to {maximumWriteSpeed}MB/s write speed provides swift performance"); 
        } 
    } 
}

// --[FEATURE #5] 
// --Compatible With 
void CompatibleWithOS() { 
    //var test = new List<string>(){"some pc", "some windows", "some2 android", "some2 android2"}; 
    var compWith = REQ.GetVariable("SP-382").HasValue() ? REQ.GetVariable("SP-382") : R("SP-382").HasValue() ? R("SP-382") : R("cnet_common_SP-382");
    if (A[429].HasValue()) { 
        var ckeckedList = new List<string>(){"Windows", "MacOS", "Linux", "Android", "Symbian"};
        var formatted = A[429].Values.Select(s => s.Value().ToString()); 
        foreach (var item in ckeckedList) { 
            formatted = formatted.Select(s => s.Contains(item) ? item : s).Distinct(); 
        }    
        Add($"CompatibleWithOS⸮Compatible with: {formatted.FlattenWithAnd()}"); 
    } 
    else if (A[603].HasValue()) { 
        Add($"CompatibleWithOS⸮Compatible with: {A[603].Values.Select(o => o.Value()).FlattenWithAnd()}"); 
    } 
    else if (compWith.HasValue()) { 
        
        Add($"CompatibleWithOS⸮Compatible with: {compWith.ToString().Split(", ").Distinct().FlattenWithAnd()}"); 
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