--Dimensions: Width (in Inches) x Length (in Yards); Width is how wide is the tape and Length is the total length on the roll    
        IF $SP-22586$ IS NOT NULL AND $SP-22585$ IS NOT NULL
            THEN "Dimensions: "_$SP-22586$_"""W x "_$SP-22585$_" yds. ##L" 
        ELSE IF $SP-22586$ IS NOT NULL 
            THEN "Dimensions: "_$SP-22586$_"""W" 
        ELSE IF $SP-22585$ IS NOT NULL 
            THEN "Dimensions: "_$SP-22585$_" yds. ##L" 
                ELSE "@@";

--Thickness (millimeters)    

        IF $SP-22042$ <2.7  THEN $SP-22042$_"mm thick tape stands up to rough handling" 
        ELSE IF $SP-22042$ >2.6  THEN $SP-22042$_"mm thickness for durable strength" 
                ELSE "@@";

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

--True color    
        IF $SP-22967$ LIKE "Clear" 
        THEN "High-clarity clear tape" 

        ELSE IF $SP-22967$ IS NOT NULL
            THEN "Comes in "_$SP-22967$
                ELSE "@@";

--Label adhesive    
        IF $SP-2779$ LIKE "Non-Adhesive" THEN "Non-Adhesive" 
        ELSE IF $SP-2779$ LIKE "Permanent" THEN A[6449].Value.Erase("paper").IfLike("%kraft%","Kraft tape is made for permanent adhesion").IfLike("", "Tape is made for permanent adhesion")
        ELSE IF $SP-2779$ LIKE "Permanent Self-Adhesive" THEN "Permanent Self-Adhesive" 
        ELSE IF $SP-2779$ LIKE "Removable" THEN "Removable" 
                ELSE "@@";

--Pack Size (If more than 1)    
IF Request.Data["TX_UOM"] NOT IN ("each","roll")
    THEN "Comes in "_Request.Data["TX_UOM"].RegexReplace("(\S+)[\/](\S+)", "$2 of $1")
ELSE "@@";

--Tensile strength rating (If Applicable)    

IF $SP-22050$ > 0 THEN
$SP-22050$_"  lbs. of tensile strength per inch of width" 
     ELSE "@@";

    --OOS

--Certifications (If Applicable)    
        IF A[380].Values IS NOT NULL
            THEN A[380].Values.Prefix("##").FlattenWithAnd().Prefix("Meets or exceeds ").Postfix(" standards")
                ELSE "@@";

--Use for additional product and/or manufacturer information relevant to the customer buying decision

IF A[6457].Where("heavy-duty").Values IS NOT NULL 
    THEN "Heavy-duty tape for tough jobs";

--Use for additional product and/or manufacturer information relevant to the customer buying decision
IF A[6457].Where("tear-resistant").Values IS NOT NULL 
    AND  A[6457].Where("break-resistant").Values IS NOT NULL 
        THEN "Resists breaking or tearing even if nicked or punctured during handling";

--Use for additional product and/or manufacturer information relevant to the customer buying decision
IF A[6457].Where("Frustration Free").Values IS NOT NULL 
    THEN "Frustration-Free technology";

--Use for additional product and/or manufacturer information relevant to the customer buying decision