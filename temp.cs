/*
--Packing tape type & Use    
        --Acrylic
        IF $SP-21708$ LIKE "Acrylic" 
            THEN "Acrylic tape designed for moving and storage needs" 

        --Carton Sealing
        ELSE IF $SP-21708$ LIKE "Carton Sealing" 
            THEN "Carton sealing tape adheres instantly to most surfaces including cartons" 

        --Fashion
        ELSE IF $SP-21708$ LIKE "Cellophane" 
            THEN "Cellophane tape offers excellent adhesion, dispensing and handling properties" 

        --Flatback
        ELSE IF $SP-21708$ LIKE "Flatback" 
            THEN "Flatback tape is designed to minimize carton-sealing failures and prevent pilferage" 

        --Hot Melt
        ELSE IF $SP-21708$ LIKE "Hot Melt" 
            THEN "Hot melt adhesive tape holds strong for your shipping needs" 

        --Label Protection
        ELSE IF $SP-21708$ LIKE "Label Protection" 
            THEN "Provides excellent label protection" 

        --Paper Tape
        ELSE IF $SP-21708$ LIKE "Paper Tape" 
            THEN "Paper tape provides secure closure for inner packing and lightweight packaging" 

        --Pre-Printed Message
        ELSE IF $SP-21708$ LIKE "Pre-Printed Message" 
            THEN "Pre-Printed Message" 

        --PVC/Bag Sealing
        ELSE IF $SP-21708$ LIKE "PVC/Bag Sealing" 
            THEN "PVC/Bag Sealing tape offers excellent adhesion, dispensing and handling properties" 

        --Reinforced
        ELSE IF $SP-21708$ LIKE "Reinforced" 
            THEN "Reinforced strapping tape is ideal for heavy tasks and bundling" 

        --Standard
        ELSE IF $SP-21708$ LIKE "Standard" 
            THEN "Standard tape offers excellent adhesion, dispensing and handling properties" 

        --Strapping/Filament
        ELSE IF $SP-21708$ LIKE "Strapping/Filament" 
            THEN "Strapping/Filament tape offers excellent adhesion, dispensing and handling properties" 

        --Water Activated
        ELSE IF $SP-21708$ LIKE "Water Activated" 
            THEN "Water activated adhesive bonds to corrugated even in dusty conditions" 

        --Moving
        ELSE IF $SP-21708$ LIKE "Moving" 
            THEN "Moving tape offers excellent adhesion, dispensing and handling properties" 

        --Cellophane
        ELSE IF $SP-21708$ LIKE "Cellophane" 
            THEN "Cellophane tape offers excellent adhesion, dispensing and handling properties" 
                ELSE "@@";
*/


void  PackingTapeTypeAndUse(){
    // Pre-Printed Message|Paper Tape|Reinforced|PVC/Bag Sealing|Acrylic|Standard|Cellophane|Water Activated|Carton Sealing|Strapping/Filament|Flatback|Fashion|Moving|Label Protection|Hot Melt
    var type = GetReferenceBase("SP-21708");
    if (type.HasValue("Acrylic")) {
        Add($"PackingTapeTypeAndUse⸮Acrylic tape designed for moving and storage needs");
    }
    else if (type.HasValue("Carton Sealing")) {
        Add($"PackingTapeTypeAndUse⸮Carton sealing tape adheres instantly to most surfaces including cartons");
    }
    else if (type.HasValue("Cellophane")) {
        Add($"PackingTapeTypeAndUse⸮Cellophane tape offers excellent adhesion, dispensing and handling properties");
    }
    else if (type.HasValue("Flatback")) {
        Add($"PackingTapeTypeAndUse⸮Flatback tape is designed to minimize carton-sealing failures and prevent pilferage");
    }
    else if (type.HasValue("Hot Melt")) {
        Add($"PackingTapeTypeAndUse⸮Hot melt adhesive tape holds strong for your shipping needs");
    }
    else if (type.HasValue("Label Protection")) {
        Add($"PackingTapeTypeAndUse⸮Provides excellent label protection");
    }
    else if (type.HasValue("Paper Tape")) {
        Add($"PackingTapeTypeAndUse⸮Paper tape provides secure closure for inner packing and lightweight packaging");
    }
    else if (type.HasValue("Pre-Printed Message")) {
        Add($"PackingTapeTypeAndUse⸮Pre-printed message");
    }
    else if (type.HasValue("PVC/Bag Sealing")) {
        Add($"PackingTapeTypeAndUse⸮PVC/Bag Sealing tape offers excellent adhesion, dispensing and handling properties");
    }
    else if (type.HasValue("Reinforced")) {
        Add($"PackingTapeTypeAndUse⸮Reinforced strapping tape is ideal for heavy tasks and bundling");
    }
    else if (type.HasValue("Standard")) {
        Add($"PackingTapeTypeAndUse⸮Standard tape offers excellent adhesion, dispensing and handling properties");
    }
    else if (type.HasValue("Strapping/Filament")) {
        Add($"PackingTapeTypeAndUse⸮Strapping/Filament tape offers excellent adhesion, dispensing and handling properties");
    }
    else if (type.HasValue("Water Activated")) {
        Add($"PackingTapeTypeAndUse⸮Water activated adhesive bonds to corrugated even in dusty conditions");
    }
    else if (type.HasValue("Moving")) {
        Add($"PackingTapeTypeAndUse⸮Moving tape offers excellent adhesion, dispensing and handling properties");
    }
    else if (type.HasValue("Cellophane")) {
        Add($"PackingTapeTypeAndUse⸮Cellophane tape offers excellent adhesion, dispensing and handling properties");
    }
}