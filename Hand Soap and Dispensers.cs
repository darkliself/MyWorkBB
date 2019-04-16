//§§1683393110991141836 -- "Hand Soap & Dispensers" "Alex K."

CleanserFormFactorAndUse();
SoapDispensersScent();
CapacityAndContainerTypes();
//Pack Qty (If more than 1)
SoapDispensersAntibacterial();
Moisturizer();
Hypoallergenic();
StainRemoval();
pHBalaced();
SoapDispensersVitamins();
AloeGlycerin();
Pumice();
LockingMechanism();
ContainerFeatures();
DispenserMaterial();
AutomaticManualDispensers();
// Standards
// Warranty

// --[FEATURE #1]
// --Cleanser form factor & use
void CleanserFormFactorAndUse() {
    var result = "";
    //Push-Style Dispenser|Automatic Dispensers|Touch-Free Dispenser|Manual Dispensers|Liquid Sanitizers|Foaming Sanitizers|--Liquid Soap|Dispenser & Refill Kit|Wipes|Refills|--Foaming Soap|Gel Sanitizers
    var cleanserFormFactor  = R("SP-18148").HasValue() ? R("SP-18148").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18148").HasValue() ? R("cnet_common_SP-18148").Replace("<NULL>", "").Text : "";

    if (!String.IsNullOrEmpty(cleanserFormFactor)) {
        if (cleanserFormFactor.ToLower().EndsWith("soap")) {
            result = $"{cleanserFormFactor.ToLower().ToUpperFirstChar()} provides an efficient way to maintain hygiene levels";
        }
        else if (cleanserFormFactor.ToLower().Equals("automatic dispensers")) {
            result = "Automatically dispenses the perfect amount of foam soap needed for hand washing in one shot";
        }
        else if (cleanserFormFactor.ToLower().Equals("manual dispensers")) {
            result = "Manual soap dispenser for reliable, hygienic soap dispensing at all your handwashing stations";
        } 
        else if (cleanserFormFactor.ToLower().Equals("foaming sanitizers")) {
            result = "Specially designed foaming formula provides gentle cleansing";
        } 
        else if (cleanserFormFactor.ToLower().Equals("gel sanitizers")) {
            result = "Advanced gel sanitizer formulation designed for healthcare environments";
        } 
        else if (cleanserFormFactor.ToLower().Equals("liquid sanitizers")) {
            result = "Liquid sanitizer is designed to keep you germ-free without sacrificing everyday personal comfort";
        } 
        else if (cleanserFormFactor.ToLower().Equals("wipes")) {
            result = "Wipes are ideal for use anytime your employees do not have easy access to running water";
        }
        else if (cleanserFormFactor.ToLower().Equals("refills")) {
            result = "Refills are an economical alternative to bar soaps";
        }
        else {
            result = $"{cleanserFormFactor.ToLower().ToUpperFirstChar()} provides gentle cleansing";
        }
    }
    
    if(!String.IsNullOrEmpty(result)) {
        Add($"CleanserFormFactorAndUse⸮{result}");
    }
}

// --[FEATURE #2]
// --Scent (If Applicable)
void SoapDispensersScent() {
    var result = "";
    var fragrance = A[7356];
	//Push-Style Dispenser|Automatic Dispensers|Touch-Free Dispenser|Manual Dispensers|Liquid Sanitizers|Foaming Sanitizers|Liquid Soap|Dispenser & Refill Kit|Wipes|Refills|Foaming Soap|Gel Sanitizers
    var cleanserFormFactor = R("SP-18148").HasValue() ? R("SP-18148").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18148").HasValue() ? R("cnet_common_SP-18148").Replace("<NULL>", "").Text : "";
	//Woodsy|Lavender|Tea|Original|Seasons|Strawberry|Clean|Floral|Masculine|Bakery|Fruity|No Scent|Nature|Spicy|Oceanic|Fresh Citrus|Lemon|Unscented|Other|Gain|Low odor|Almond|Vanilla|Herbal
	var scent = R("SP-18147").HasValue() ? R("SP-18147").Replace("<NULL>", "").Text :
	    R("cnet_common_SP-18147").HasValue() ? R("cnet_common_SP-18147").Replace("<NULL>", "").Text : "";
		
    if (!String.IsNullOrEmpty(cleanserFormFactor) && fragrance.HasValue()) {
        result = $"{cleanserFormFactor.ToLower().ToUpperFirstChar()} leaves a pleasant {fragrance.Values.Select(o => o.Value().ToUpperFirstChar()).FlattenWithAnd()}";
    }
    else if (fragrance.HasValue()) {
        result = $"{fragrance.Values.Select(o => o.Value().ToUpperFirstChar().Replace("Floral fresh", "Fresh floral")).FlattenWithAnd()} scent leaves a pleasant aroma";
    }
    else if (!String.IsNullOrEmpty(scent) && !scent.ToLower().Equals("no scent")) {
        result = $"{scent} scent leaves a pleasant aroma";
    }
    else if (!String.IsNullOrEmpty(cleanserFormFactor) && !cleanserFormFactor.ToLower().Contains("dispenser")) {
        result = $"Unscented formula for those sensitive to fragrances";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"SoapDispensersScent⸮{result}");
    }
}

// --[FEATURE #3]
// --Capacity (oz.) & container types
void CapacityAndContainerTypes() {
    var result = "";
    var capacityOz = R("SP-21132").HasValue() ? R("SP-21132").Replace("<NULL>", "").Text :
		R("cnet_common_SP-21132").HasValue() ? R("cnet_common_SP-21132").Replace("<NULL>", "").Text : "";
	//Push-Style Dispenser|Automatic Dispensers|Touch-Free Dispenser|Manual Dispensers|Liquid Sanitizers|Foaming Sanitizers|Liquid Soap|Dispenser & Refill Kit|Wipes|Refills|Foaming Soap|Gel Sanitizers
    var cleanserFormFactor = R("SP-18148").HasValue() ? R("SP-18148").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18148").HasValue() ? R("cnet_common_SP-18148").Replace("<NULL>", "").Text : "";
    var containerTypes = R("SP-24477").HasValue() ? R("SP-24477").Replace("<NULL>", "").Text :
		R("cnet_common_SP-24477").HasValue() ? R("cnet_common_SP-24477").Replace("<NULL>", "").Text : "";

    if (!String.IsNullOrEmpty(capacityOz)) {
        if (!String.IsNullOrEmpty(cleanserFormFactor) && cleanserFormFactor.ToLower().EndsWith("dispensers")) {
            result = $"Comes in {capacityOz} fl. oz. dispenser";
        }
        else if (!String.IsNullOrEmpty(containerTypes)) {
            result = $"{capacityOz} oz. {containerTypes.ToLower()}";
        } else {
            result = $"Comes in capacity of {capacityOz} oz.";
        }
    }
    
    if(!String.IsNullOrEmpty(result)) {
        Add($"CapacityAndContainerTypes⸮{result}");
    }
}
// --[FEATURE #4]
// --Pack Qty (If more than 1)

// --[FEATURE #5]
// --Additional Antibacterial
void SoapDispensersAntibacterial() {
    var result = "";
    var antibacterial = A[7363];
    var features = A[7379];
    var components = A[7355];
    
    if (antibacterial.HasValue()) {
        result = "Provides a high level of germ-killing action to stop spread of infection";
    }
    else if (features.HasValue("antibacterial")) {
        result = "Antibacterial for a healthier workplace";
    }
    else if (components.HasValue("triclosan")) {
        result = "Contains triclosan to kill bacteria and microbes";
    } 
    if(!String.IsNullOrEmpty(result)) {
        Add($"SoapDispensersAntibacterial⸮{result}");
    }
}
// --[FEATURE #6]
// --Additional Moisturizer
void Moisturizer() {
    var result = "";
    var moisturizer = A[7361];
   
    if (moisturizer.HasValue()) {
        result = "Contains moisturizers that help prevent your hands from drying";
    }
 
    if(!String.IsNullOrEmpty(result)) {
        Add($"Moisturizer⸮{result}");
    }
}

// --[FEATURE #7]
// --Additional Hypoallergenic
void Hypoallergenic() {
    var result = "";
    var features = A[7367];
   
    if (features.HasValue("hypoallergenic")) {
        result = "Hypoallergenic, so it has less of a chance of causing flare-ups of skin allergies";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"Hypoallergenic⸮{result}");
    }
}

// --[FEATURE #8]
// --Additional Stain Removal
void StainRemoval() {
    var result = "";
    var stainRemoval = A[7358];
   
    if (stainRemoval.HasValue()) {
        result = $"Effectively removes {stainRemoval.Values.Select(o => o.Value().ToLower()).FlattenWithAnd()} stains";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"StainRemoval⸮{result}");
    }
}

// --[FEATURE #9]
// --Additional pH Balaced
void pHBalaced() {
    var result = "";
    var features = A[7367];
   
    if (features.HasValue()) {
        result = "pH balanced to promote skin comfort";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"pHBalaced⸮{result}");
    }
}

// --[FEATURE #10]
// --Additional Vitamins
void SoapDispensersVitamins() {
    var result = "";
    var components = A[7355];
    if (Coalesce(components).HasValue("%vitamin%")) {
        result = $"Enriches your skin with vitamin {components.Where("%vitamin%").Select(o => o.Value().Replace("vitamin ", "")).FlattenWithAnd()}";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"SoapDispensersVitamins⸮{result}");
    }
}

// --[FEATURE #11]
// --Additional Aloe Glycerin
void AloeGlycerin() {
    var result = "";
    var components = A[7355];
    if (Coalesce(components).HasValue("aloe%", "glycerin%")) {
        result = $"Includes {components.Where("aloe%","glycerin%").Select(o => o.Value()).FlattenWithAnd()} to leave skin soft, smooth and refreshed";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"AloeGlycerin⸮{result}");
    }
}

// --[FEATURE #12]
// --Additional Pumice
void Pumice() {
    var result = "";
    var components = A[7355];
    if (Coalesce(components).HasValue("%pumice%")) {
        result = $"{components.Where("%pumice%").Select(o => o.Value()).FlattenWithAnd().ToUpperFirstChar()} for an effective hand cleanup";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"Pumice⸮{result}");
    }
}

// --[FEATURE #13]
// --Additional Locking Mechanism
void LockingMechanism() {
    var result = "";
    var lockable = A[7372];
    var features = A[7379];
    if (lockable.HasValue() || features.HasValue("key lock")) {
        result = "Locking mechanism helps prevent tampering and vandalism";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"LockingMechanism⸮{result}");
    }
}

// --[FEATURE #14]
// --Additional
void ContainerFeatures() {
    var result = "";
    var transparentCover = A[7371];
    var features = A[7379];
    if (features.HasValue("sight window")) {
        result = "Sight window makes it easy to check fill status";
    }
    else if (transparentCover.HasValue()) {
        result = "Transparent container allows for quick monitoring of soap level";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"ContainerFeatures⸮{result}");
    }
}

// --[FEATURE #15]
// --Additional
void DispenserMaterial() {
    var result = "";
    var type = R("SP-18148").HasValue() ? R("SP-18148").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18148").HasValue() ? R("cnet_common_SP-18148").Replace("<NULL>", "").Text : "";
    
    var material = A[372];
    if (!String.IsNullOrEmpty(type) 
    && type.ToLower().EndsWith("dispensers")
    && material.HasValue()) {
        result =  $"{material.Values.Select(o => o.Value()).FlattenWithAnd().ToUpperFirstChar()} construction withstands everyday use in a wide variety of settings";
    }

    if(!String.IsNullOrEmpty(result)) {
        Add($"DispenserMaterial⸮{result}");
    }
}

// --[FEATURE #16]
// --Additional Automatic Manual Dispensers
void AutomaticManualDispensers() {
    var result = "";
    var type = R("SP-18148").HasValue() ? R("SP-18148").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18148").HasValue() ? R("cnet_common_SP-18148").Replace("<NULL>", "").Text : "";
    
    if (!String.IsNullOrEmpty(type)) {
        if (type.ToLower().Equals("automatic dispensers")) {
            result = "Touchless technology helps reduce mess and spread of germs";
        }
        else if (type.ToLower().Equals("manual dispensers")) {
            result = "Features an improved design that eliminates soap waste";
        }
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"AutomaticManualDispensers⸮{result}");
    }
}

// --[FEATURE #17]
// --Additional standards

// --[FEATURE #18]
// --Warranty Information

//1683393110991141836 end of "Hand Soap & Dispensers" §§