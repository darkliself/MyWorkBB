
--Product Type, Integrated Functionality, Modem & Transmission Speed

---EXAMPLES: 
--Fax machine with print/copy functionality and a 33.6 kbps fax modem for quick transmission of files

--BULLET 1. Product Type, Integrated Functionality, Modem & Transmission Speed

--only fax machine

IF A[4386].Values.Where("fax") IS NOT NULL
    AND A[4386].Values.Where("copier", "printer", "scanner") IS NULL
    AND $SP-23948$ IS NOT NULL
    THEN REF["SP-18058"].Postfix(" ")_"Fax machine"_" and a "_$SP-23948$_"Kbps fax modem for quick transmission of files" 
    
ELSE IF A[4386].Values.Where("fax") IS NOT NULL
        AND A[4386].Values.Where("copier", "printer", "scanner") IS NULL
        AND $SP-23948$ IS NULL
    THEN REF["SP-18058"].Postfix(" ")_"Fax machine and fax modem for quick transmission of files" 

--fax copier

ELSE IF A[4386].Values.Where("fax") IS NOT NULL
        AND A[4386].Values.Where("copier") IS NOT NULL
        AND A[4386].Values.Where("printer", "scanner") IS NULL
        AND $SP-23948$ IS NOT NULL
    THEN REF["SP-18058"].Postfix(" ")_"Fax machine with copy functionality"_" and a "_$SP-23948$_"Kbps fax modem for quick transmission of files" 
ELSE IF A[4386].Values.Where("fax") IS NOT NULL
        AND A[4386].Values.Where("copier") IS NOT NULL
        AND A[4386].Values.Where("printer", "scanner") IS NULL
        AND $SP-23948$ IS NULL
    THEN REF["SP-18058"].Postfix(" ")_"Fax machine with copy functionality and fax modem for quick transmission of files" 

--fax printer

ELSE IF A[4386].Values.Where("fax") IS NOT NULL
        AND A[4386].Values.Where("printer") IS NOT NULL
        AND A[4386].Values.Where("copier", "scanner") IS NULL
        AND $SP-23948$ IS NULL
    THEN REF["SP-18058"].Postfix(" ")_"Fax machine with print functionality and fax modem for quick transmission of files" 
ELSE IF A[4386].Values.Where("fax") IS NOT NULL
        AND A[4386].Values.Where("printer") IS NOT NULL
        AND A[4386].Values.Where("copier", "scanner") IS NULL
        AND $SP-23948$ IS NOT NULL
    THEN REF["SP-18058"].Postfix(" ")_"Fax machine with print functionality"_" and a "_$SP-23948$_"Kbps fax modem for quick transmission of files" 

--fax scanner

ELSE IF A[4386].Values.Where("fax") IS NOT NULL
        AND A[4386].Values.Where("scanner") IS NOT NULL
        AND A[4386].Values.Where("copier", "printer") IS NULL
        AND $SP-23948$ IS NULL
    THEN REF["SP-18058"].Postfix(" ")_"Fax machine with scan functionality and fax modem for quick transmission of files" 
ELSE IF A[4386].Values.Where("fax") IS NOT NULL
        AND A[4386].Values.Where("scanner") IS NOT NULL
        AND A[4386].Values.Where("copier", "printer") IS NULL
        AND $SP-23948$ IS NOT NULL
    THEN REF["SP-18058"].Postfix(" ")_"Fax machine with scan functionality"_" and a "_$SP-23948$_"Kbps fax modem for quick transmission of files" 

--fax copier printer

ELSE IF A[4386].Values.Where("fax") IS NOT NULL
        AND A[4386].Values.Where("copier") IS NOT NULL
        AND A[4386].Values.Where("printer") IS NOT NULL
        AND A[4386].Values.Where("scanner") IS NULL
        AND $SP-23948$ IS NULL
    THEN REF["SP-18058"].Postfix(" ")_"Fax machine with copy/print functionality and fax modem for quick transmission of files" 
ELSE IF A[4386].Values.Where("fax") IS NOT NULL
        AND A[4386].Values.Where("copier") IS NOT NULL
        AND A[4386].Values.Where("printer") IS NOT NULL
        AND A[4386].Values.Where("scanner") IS NULL
        AND $SP-23948$ IS NOT NULL
    THEN REF["SP-18058"].Postfix(" ")_"Fax machine with copy/print functionality"_" and a "_$SP-23948$_"Kbps fax modem for quick transmission of files" 

--fax copier scanner

ELSE IF A[4386].Values.Where("fax") IS NOT NULL
        AND A[4386].Values.Where("copier") IS NOT NULL
        AND A[4386].Values.Where("scanner") IS NOT NULL
        AND A[4386].Values.Where("printer") IS NULL
        AND $SP-23948$ IS NULL
    THEN REF["SP-18058"].Postfix(" ")_"Fax machine with copy/scan functionality and fax modem for quick transmission of files" 
ELSE IF A[4386].Values.Where("fax") IS NOT NULL
        AND A[4386].Values.Where("copier") IS NOT NULL
        AND A[4386].Values.Where("scanner") IS NOT NULL
        AND A[4386].Values.Where("printer") IS NULL
        AND $SP-23948$ IS NOT NULL
    THEN REF["SP-18058"].Postfix(" ")_"Fax machine with copy/scan functionality"_" and a "_$SP-23948$_"Kbps fax modem for quick transmission of files" 

--fax printer scanner

ELSE IF A[4386].Values.Where("fax") IS NOT NULL
        AND A[4386].Values.Where("printer") IS NOT NULL
        AND A[4386].Values.Where("scanner") IS NOT NULL
        AND A[4386].Values.Where("copier") IS NULL
        AND $SP-23948$ IS NULL
    THEN REF["SP-18058"].Postfix(" ")_"Fax machine with print/scan functionality and fax modem for quick transmission of files" 
ELSE IF A[4386].Values.Where("fax") IS NOT NULL
        AND A[4386].Values.Where("printer") IS NOT NULL
        AND A[4386].Values.Where("scanner") IS NOT NULL
        AND A[4386].Values.Where("copier") IS NULL
        AND $SP-23948$ IS NOT NULL
    THEN REF["SP-18058"].Postfix(" ")_"Fax machine with print/scan functionality"_" and a "_$SP-23948$_"Kbps fax modem for quick transmission of files" 

--fax copier printer scanner

ELSE IF A[4386].Values.Where("fax") IS NOT NULL
        AND A[4386].Values.Where("printer") IS NOT NULL
        AND A[4386].Values.Where("scanner") IS NOT NULL
        AND A[4386].Values.Where("copier") IS NOT NULL
        AND $SP-23948$ IS NULL
    THEN REF["SP-18058"].Postfix(" ")_"Fax machine with print/scan/copy functionality and fax modem for quick transmission of files" 
ELSE IF A[4386].Values.Where("fax") IS NOT NULL
        AND A[4386].Values.Where("printer") IS NOT NULL
        AND A[4386].Values.Where("scanner") IS NOT NULL
        AND A[4386].Values.Where("copier") IS NOT NULL
        AND $SP-23948$ IS NOT NULL
    THEN REF["SP-18058"].Postfix(" ")_"Fax machine with print/scan/copy functionality"_" and a "_$SP-23948$_"Kbps fax modem for quick transmission of files" 
ELSE "@@";



--Memory (Transmission & Reception)    
--EXAMPLES: 
--Faxing memory stores up to 512 pages

--2747, 2748

IF COALESCE (A[2747].Value, A[2748].Value) IS NOT NULL
    THEN "Faxing memory stores up to "_COALESCE (A[2747].Value, A[2748].Value)_" pages" 
ELSE "@@";

--Output (Print Speed, Resolution, Color & Duplexing Capability)    
--Outputs in black ink up to 19 pages per minute single sided and 16 pages per minute double sided at up to 600 x 400 dpi (119 symblos)

--Features a printing speed of [] pages per minute in black and [] pages per minute in color 

IF A[2760].Value IS NOT NULL
AND A[2761].Value IS NOT NULL 
    THEN "Features a printing speed of ##"_A[2760].Value_" pages per minute in black and ##"_A[2761].Value_" pages per minute in color" 

ELSE IF A[2760].Value IS NULL
AND A[2761].Value IS NOT NULL 
AND A[5425].Value LIKE "yes" 
    THEN "Features a printing speed of ##"_A[2761].Value_" pages per minute in color with automatic duplexing" 

ELSE IF A[2760].Value IS NULL
AND A[2761].Value IS NOT NULL 
    THEN "Features a printing speed of ##"_A[2761].Value_" pages per minute in color" 

ELSE IF A[2760].Value IS NOT NULL
AND A[2761].Value IS NULL 
AND A[5425].Value LIKE "yes" 
    THEN "Features a printing speed of ##"_A[2760].Value_" pages per minute in black with automatic duplexing" 

ELSE IF A[2760].Value IS NOT NULL
AND A[2761].Value IS NULL
    THEN "Features a printing speed of ##"_A[2760].Value_" pages per minute in black" 
ELSE IF A[3172].Unit LIKE "ppm" THEN
"Features a printing speed of ##"_A[3172].Value_" pages per minute" 

ELSE "@@";

--Input loading feature, Input Capacity (standard & ADF) & Size

--https://www.staples.com/product_711376
--Standard input paper capacity (sheets): 250 
--2731  -- standart capacity
--8601 feeder type (ADF)
--document feeder capacity
--media class size

--250-sheet paper capacity, 30-page auto document feeder 
--Accommodates A4/legal-size pages in the 250-sheet input tray and 50-sheet auto document feeder

IF A[2731].Value IS NOT NULL
AND A[2725].Value IS NOT NULL
AND A[6838].Value IS NOT NULL
    THEN "Accomodates " 
        _A[6838].Value.Postfix("-size pages")
        _" in the " 
        _A[2731].Value_A[2731].Unit.Replace("sheets", "sheet").Prefix("-")
        _" input tray and " 
        _A[2725].Value_A[2725].Unit.Replace("sheets", "sheet").Prefix("-")
        _" auto document feeder" 

ELSE IF A[2731].Value IS NOT NULL
AND A[6838].Value IS NOT NULL
    THEN "Accomodates " 
        _A[6838].Value.Postfix("-size pages")
        _" in the " 
        _A[2731].Value_A[2731].Unit.Replace("sheets", "sheet").Prefix("-")
        _" input tray" 

ELSE IF A[2725].Value IS NOT NULL
AND A[6838].Value IS NOT NULL
    THEN "Accomodates " 
        _A[6838].Value.Postfix("-size pages")
        _" in the " 
        _A[2725].Value_A[2725].Unit.Replace("sheets", "sheet").Prefix("-")
         _" auto document feeder" 

ELSE "@@";

--Dimensions    

--SP-21044 width
--SP-20654 height
--SP-20657 depth

IF $SP-21044$ IS NOT NULL
AND $SP-20654$ IS NOT NULL
AND $SP-20657$ IS NOT NULL
    THEN  "Dimensions: "_$SP-20654$_"""H x "_$SP-21044$_"""W x "_$SP-20657$_"""D" 

ELSE IF  $SP-21044$ IS NOT NULL
AND $SP-20657$ IS NOT NULL
    THEN "Dimensions: "_$SP-21044$_"""W x "_$SP-20657$_"""L" 
ELSE "@@";

--Display (Type & Size) & Indicators (includes Caller ID, telephone directory/capacity)    
-- LCD|LED display supports caller ID and up to 30 speed dial numbers for efficient faxing and calls 

--Easy to read [] 16-digit LCD display

IF A[2780].Value IS NOT NULL AND A[2779].Value IS NOT NULL
    THEN "Easy to read "_A[2780].Value.ExtractDecimals().First().Postfix("-character")_A[2779].Value.Replace(" lines", "-line").Prefix("/")_A[5299].Values.Where("LCD", "LED").Prefix(" ")_" display" 
ELSE IF A[2779].Value IS NOT NULL
    THEN "Easy to read "_A[2779].Value.Replace(" lines", "-line")_A[5299].Values.Where("LCD", "LED").Prefix(" ")_" display" 
ELSE "@@";

--Broadcast Capacity (if applicable)
--Up to 222 station auto dialing 

IF $SP-64$ IS NOT NULL
    THEN "Up to "_$SP-64$_" auto dialing" 
ELSE "@@";

--Connectivity (No. of Ports & Interface) (includes wireless)    

--1 Hi-Speed USB 20 Device; 2 Hi-Speed USB 20 Host; 1 Gigabit Ethernet 10/100/1000T network 

--USB 20, 10/100/1000 Base-T Ethernet and Wi-Fi 80211b/g/n ports enable easy connectivity; mobile-ready through printing apps 
--https://www.staples.com/product_2656790

--USB 20, Base-T Ethernet and Wi-Fi connectivity enhance productivity 
--https://www.staples.com/product_2656788

IF A[2703].Where("%Wi-Fi%").Values IS NOT NULL
AND  A[2703].Where("%Bluetooth%").Values IS NULL
AND A[6836].Values IS NOT NULL
    THEN A[6836].Match(23, 6836).Values.Prefix("##").Flatten(", ").Replace("¦¬", " x ").Replace("<>", "##1")_" and ##Wi-Fi connectivity enhance productivity" 

ELSE IF A[2703].Where("%Wi-Fi%").Values IS NOT NULL
AND  A[2703].Where("%Bluetooth%").Values IS NULL
AND A[2703].Values.Count > 1
    THEN A[2703].WhereNot("%Wi-Fi%").Values.Flatten(", ")_" and ##Wi-Fi connectivity enhance productivity" 

--Network-ready wired 10/100/1000Base-T Ethernet and USB 30 connectivity with Apple and Google mobile print options 
--https://www.staples.com/product_2580332

ELSE IF A[2703].Where("%LAN%").Values IS NOT NULL
AND  A[2703].Where("%Bluetooth%").Values IS NULL
AND A[6836].Values IS NOT NULL
    THEN "Network-ready wired " 
    _A[6836].Where("%lan%").Match(23).Values.First().Prefix("##")
    _" x " 
    _A[2703].Where("%LAN%").Values.First()
    _" ##Ethernet and " 
    _A[6836].Match(23, 6836).Values.WhereNot("%LAN%").Prefix("##").Flatten(", ").Replace("¦¬", " x ").Replace("<>", "##1")
    _" connectivity" 

ELSE IF A[2703].Where("%LAN%").Values IS NOT NULL
AND  A[2703].Where("%Bluetooth%").Values IS NULL
    THEN "Network-ready wired "_A[2703].Where("%LAN%").Values.First()_" ##Ethernet"_A[2703].WhereNot("%LAN%").Values.FlattenWithAnd().Prefix(", ")_" connectivity" 

--Printer features USB port, Ethernet port, phone line port and wireless connectivity for added convenience 

ELSE IF A[6836].Values IS NOT NULL
    THEN "Features "_A[6836].Match(23, 6836).Values.Prefix("##").Flatten(", ").Replace("¦¬", " x ").Replace("<>", "##1")_COALESCE(A[2703].Where("%Bluetooth%").Values.Prefix(" and ").Postfix(" interface "), " interface")
ELSE "@@";

--Copy Functionality (if applicable)
--Copies documents at up to 12 copies per minute so you need one less machine for your office 
--2763, 2764  

IF COALESCE (A[2763].Value, A[2764].Value) IS NOT NULL
    THEN "Copies documents at up to "_COALESCE (A[2763].Value, A[2764].Value)_" copies per minute so you need one less machine for your office" 
ELSE "@@";

--Scanning Capabilities (includes technology/software, resolution & output color) if applicable

--Color flatbed scanner captures up to 19,200 x 19,200 dpi to capture every detail
--https://www.staples.com/product_153935

--Color scanner captures resolutions up to 4,800 x 4,800 dpi at speeds up to 24 ipm for efficient archival and copying
--https://www.staples.com/product_2502596

IF $SP-183$ IS NOT NULL
AND A[2756].Value IS NOT NULL
    THEN "This fax machine captures up "_$SP-183$_" resolution in color for efficient archiving and copying" 

ELSE IF $SP-183$ IS NOT NULL
AND A[2757].Value IS NOT NULL
    THEN "This fax machine captures up "_$SP-183$_" resolution in grayscale for efficient archiving and copying" 
ELSE IF $SP-183$ IS NOT NULL
    THEN "This fax machine captures up  "_$SP-183$_" resolution for efficient archiving and copying" 
ELSE "@@";

--Calling Capabilities (includes answering machine feature) if applicable    

--Supports fax forward, paging, caller ID, and distinctive ring for added convenience 
--https://www.staples.com/product_586026

--2791 - Error Correction Mode (ECM)
--2735 - One-Touch Dialing
--2736 - Speed Dialing
--2737 - Caller ID
--2738 - Telephone / Fax Switch
--2740 - Broadcast Transmission
--2741 - Delayed Transmission

IF SKU.ProductId LIKE "10903324" 
    THEN "Supports one-touch dialing, speed dialing" 
ELSE IF SKU.ProductId LIKE "1268015" 
    THEN "Supports caller ##ID, speed dialing, telephone/fax switch, fax forwarding, broadcast transmission, group dialing, and distinctive ring" 
ELSE IF SKU.ProductId LIKE "10462012" 
    THEN "Supports one-touch dialing, speed dialing, fax forwarding, and broadcast transmission" 
ELSE IF SKU.ProductId LIKE "11134298" 
    THEN "Supports one-touch dialing, speed dialing, telephone/fax switch, fax forwarding, and broadcast transmission" 
ELSE IF SKU.ProductId LIKE "5824318" 
    THEN "Supports caller ##ID, broadcast transmission, distinctive ring" 
ELSE IF SKU.ProductId LIKE "1268016" 
    THEN "Supports caller ##ID, one-touch dialing, speed dialing, telephone/fax switch, fax forwarding, and broadcast transmission" 
ELSE IF SKU.ProductId LIKE "11098412" 
    THEN "Supports one-touch dialing, speed dialing, fax forwarding, broadcast transmission, and group dialing" 
ELSE IF SKU.ProductId LIKE "2549299" 
    THEN "Supports caller ##ID, one-touch dialing, speed dialing, telephone/fax switch, fax forwarding, and broadcast transmission" 

ELSE IF A[2737].Value IS NOT NULL
OR A[2736].Value IS NOT NULL
OR A[2735].Value IS NOT NULL
OR A[2738].Value IS NOT NULL
    THEN "Supports" 
        _COALESCE(A[2737].Value
            .HasText
            .IfLike("Yes", " caller ##ID,")
            .IfLike("no", ""))
        _COALESCE(A[2735].Value
            .HasText
            .IfLike("Yes", " one-touch dialing,")
            .IfLike("no", ""))
        _COALESCE(A[2736].Value
            .HasText
            .IfLike("Yes", " speed dialing,")
            .IfLike("no", ""))
        _COALESCE(A[2738].Value
            .HasText
            .IfLike("Yes", " telephone/fax switch,")
            .IfLike("no", ""))
        _COALESCE(A[2743].Where("fax forwarding").Values.First()
            .HasText
            .IfLike("Yes", " fax forwarding,")
            .IfLike("no", ""))
        _COALESCE(A[2740].Value
            .HasText
            .IfLike("Yes", " Broadcast Transmission,")
            .IfLike("no", ""))
        _COALESCE(A[2743].Where("group dialing").Values.First()
            .HasText
            .IfLike("Yes", " group dialing,")
            .IfLike("no", ""))
        _COALESCE(A[2743].Where("Distinctive Ring").Values.First()
            .HasText
            .IfLike("Yes", " distinctive ring")
            .IfLike("no", ""))
ELSE "@@";

--Warranty
IF A[430].Value IS NOT NULL
    THEN A[430].Value.Replace(" days", "##-day").Replace(" year", "##-year").Replace(" years", "##-year").Replace(" months", "##-month").Erase("limited").Erase("manufacturer").Erase("warranty").Erase("with product registration").Postfix(" manufacturer limited warranty").ToUpperFirstChar().Prefix("##")
ELSE "@@";