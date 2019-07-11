--Battery backup or UPS device type & Use 
IF $SP-22341$ IN ("Battery Backup UPS","Mini Tower UPS", "UPS")
    THEN $SP-22341$_" protects electronic equipment and prevents data loss during a power outage"    
ELSE IF $SP-22341$ LIKE "Standby UPS" 
    THEN $SP-22341$_" protects your equipment in case of power outages or power fluctuations" 
ELSE IF $SP-22341$ IN ("%replacement battery%", "battery") 
    THEN $SP-22341$_" helps restore the runtime of UPS for better performance" 
ELSE IF $SP-22341$ IN ("Extended Battery Module", "UPS Battery Module")
    THEN $SP-22341$_" for expanded runtime and faster restoration of full backup power" 
ELSE IF $SP-22341$ IN ("Extended Battery Cabinet", "UPS Battery Cabinet")
    THEN $SP-22341$_" is used for additional run time" 
ELSE IF $SP-22341$ LIKE "Display Module" 
    THEN $SP-22341$_" allows the user to locally view information" 
ELSE IF    $SP-22341$ IN ("%power module") 
    THEN $SP-22341$_" ensures that systems continue to operate smoothly during power outages" 
ELSE IF $SP-22341$ LIKE "Power Conditioner" 
    THEN $SP-22341$_" extends the life of the equipment and keeps it in working state in all conditions" 
ELSE IF $SP-22341$ LIKE "Step-Down Transformer" 
    THEN "Step-down transformer avoids inrush current and saturation eliminating need to oversize transformer" 
ELSE IF $SP-22341$ LIKE "network management card" 
    THEN $SP-22341$_" offers remote monitoring and control of UPS" 
ELSE IF $SP-22341$ LIKE "UPS Management Adapter" 
    THEN $SP-22341$_" offers remote monitoring and control" 
ELSE IF $SP-22341$ LIKE "intelligence module" 
    THEN $SP-22341$_" is designed to proactively identify and correct problems before downtime occur" 
ELSE IF $SP-22341$ LIKE "DC Power Supply" 
    THEN $SP-22341$_" ensures that you always have a reliable source of electricity" 
ELSE IF $SP-22341$ LIKE "%cable" 
    THEN $SP-22341$_" ensures easy connectivity between devices" 
ELSE IF $SP-22341$ IN ("%kit", "%kits")
    THEN $SP-22341$_" aids for easy installation of equipment" 
ELSE "@@";

--Joule rating    
IF $SP-397$ IS NOT NULL 
    THEN "Surge energy rating: "_$SP-397$_" Joules" 
ELSE "@@";

--Outlet count 
IF $SP-22340$ IS NOT NULL 
    THEN "Equipped with "_$SP-22340$_" outlets for seamless connectivity" 
ELSE "@@"; 

--USB connectivity    
IF $SP-460$ IS NOT NULL 
    THEN "Provides USB connectivity for maximum flexibility" 
ELSE "@@";

--Cord length (ft.) 
IF $SP-21372$ IS NOT NULL 
    THEN $SP-21372$_"' cord allows you to set up your system at a comfortable distance" 
ELSE "@@";

--Battery recharge time  
IF $SP-22233$ IS NOT NULL 
    THEN "Recharge time: "_$SP-22233$_" hours" 
ELSE "@@";

--Output voltage capacity    
IF $SP-22337$ IS NOT NULL
    THEN "Output voltage of "_$SP-22337$_" to ensure adequate power supply" 
ELSE IF A[387].Value IS NOT NULL
    THEN "Output voltage of "_A[387].Value_A[387].Unit_" to ensure adequate power supply" 
ELSE "@@";

-- Port of Interface
IF $SP-22335$ IS NOT NULL
    THEN REF["SP-22335"].Split(", ").FlattenWithAnd().Replace(" "," ##").Replace("x ", "")
ELSE "@@";

--Battery backup or UPS form factor    
IF $SP-22339$ IS NOT NULL 
    THEN $SP-22339$_" form makes it convenient to install and use" 
ELSE "@@";

--Type of battery
IF $SP-22342$ LIKE "Lead Acid Battery" 
THEN "Lead acid battery ensures efficient performance" 
ELSE IF $SP-22342$ NOT IN (NULL, "Other", "%battery%") 
    THEN $SP-22342$_" ensures efficient performance" 
ELSE "@@";

--Safety Features (lightning protection) 
IF A[3469].Values.Where("lightning protection") IS NOT NULL 
OR DC.Ksp.Values.Where("%lightning protection%") IS NOT NULL
    THEN "Lightning protection ensures your investments are safe from potentially irreversible damage" 
ELSE "@@";

--Additional-charg USB
IF A[374].Where("%USB%charging%").Values IS NOT NULL
    THEN "USB charging port let you charge smartphones or tablets" 
ELSE "@@"; 

--remote    
IF $SP-22341$ IN ("network management card", "UPS Management Adapter")
    THEN "Allows you to reboot your equipment through remote access without any manual help" 
ELSE "@@";    

--Additional(Certifications & Standards)
IF $SP-21834$ IS NOT NULL 
    THEN "Meets or exceeds "_REF["SP-21834"].Split(", ").Erase(" Certified").Prefix("##").Replace(" "," ##").FlattenWithAnd()_" standards" 
ELSE "@@";

--Dimensions
IF $SP-20654$ IS NOT NULL --H
AND $SP-21044$ IS NOT NULL --W
AND $SP-20657$ IS NOT NULL --D
    THEN "Dimensions: "_$SP-20654$_"""H x "_$SP-21044$_"""W x "_$SP-20657$_"""D" 
ELSE "@@"; 

--Warranty
IF A[430].Value Like "%lifetime%" 
    THEN "Lifetime manufacturer limited warranty" 
ELSE IF A[430].Value Like "%year%" 
    THEN A[430].Value.Erase("Warranty").Erase("limited").ExtractDecimals().Postfix("-year manufacturer limited warranty")
ELSE "@@";