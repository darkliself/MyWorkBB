--[Bullet 1] USB flash drive capacity & type
--[Bullet 2] USB flash drive design, color family& USB flash drive material
--[Bullet 3] USB flash drive interface
--[Bullet 4] Maximum read & write speed (Mb per seconds)
--[Bullet 5] Compatible With
--[Bullet 6] Encryption
--[Bullet 7] Security features
--[Bullet 8] Languages Supported
--[Last Bullet] Warranty

--[Bullet 1] USB flash drive capacity & type
--        $SP-18334$ - Capacity
--        $SP-14923$ - Type

    --4GB Flash drive offers reliable data storage (https://www.staples.com/Kanguru-Defender-Elite300-4GB-260-120-Mbps-SuperSpeed-USB-3-0-Secure-Flash-Drive-Black-KDFE300-4G/product_IM11P3185)
    IF $SP-18334$ IN ("2GB", "4GB", "8GB", "16GB")
    AND $SP-14923$ LIKE "Flash Drive" 
        THEN $SP-18334$
            _" flash drive offers reliable data storage" 

    ELSE IF $SP-18334$ IN ("2GB", "4GB", "8GB", "16GB")
    AND $SP-14923$ IS NOT NULL
        THEN $SP-18334$
             _" "_$SP-14923$_" flash drive offers reliable data storage" 

    --128GB USB flash drive for convenient storage (https://www.staples.com/SanDisk-128GB-130-Mbps-USB-Flash-Drive/product_IM11A7133)
    ELSE IF $SP-18334$ IS NOT NULL
    AND $SP-14923$ LIKE "Flash Drive" 
        THEN $SP-18334$
            _" USB flash drive for convenient storage" 

    ELSE IF $SP-18334$ IS NOT NULL
    AND $SP-14923$ IS NOT NULL
        THEN $SP-18334$
            _" "_$SP-14923$_" USB flash drive for convenient storage" 
ELSE "@@";

--[Bullet 2] USB flash drive design, color family& USB flash drive material 

--    $SP-22140$ - Design
--    $SP-22967$ - Color
--    $SP-14924$ - Material

    --All three is not null and colors aren't assorted
    IF $SP-22140$ IS NOT NULL
        AND $SP-22967$ NOT LIKE "Assorted" 
        AND $SP-14924$ IS NOT NULL 
        THEN $SP-22140$_" designed "_$SP-22967$_" flash drive is made of "_$SP-14924$

    --All three is not null and colors are assorted
    ELSE IF $SP-22140$ IS NOT NULL
        AND $SP-22967$ LIKE "Assorted" 
        AND $SP-14924$ IS NOT NULL 
        THEN $SP-22140$_" designed flash drives in "_$SP-22967$_" colors are made of "_$SP-14924$

    --EMPTY MATERIAL

    --Swivel design
    --The drive has a capless design with a rotating case to protect the drive and its contents (https://www.staples.com/lexar-32gb-2-0-usb-drive-teal-ljdtt32gamodt/product_SS7923457)
    ELSE IF $SP-22140$ LIKE "Swivel" 
    AND $SP-22967$ LIKE "Assorted" 
        THEN "Flash drives in assorted colors have a capless design with a rotating case" 
    ELSE IF $SP-22140$ LIKE "Swivel" 
    AND $SP-22967$ IS NOT NULL
        THEN "Flash drive in "_$SP-22967$_" color has a capless design with a rotating case" 
    ELSE IF $SP-22140$ LIKE "Swivel" 
        THEN "Flash drive has a capless design with a rotating case to protect the drive and its contents" 

    --Black color with retractable design for enhanced portability (https://www.staples.com/SanDisk-Ultra-Dual-USB-3-0-Flash-Drive/product_SS4150993)
    ELSE IF $SP-22967$ LIKE "Assorted" 
       THEN $SP-22140$_" flash drive in assorted colors" 
    ELSE IF $SP-22967$ IS NOT NULL 
        THEN $SP-22967$_" "_$SP-22140$_" flash drive" 

    --EMPTY COLOR

    ELSE IF $SP-14924$ IS NOT NULL
        THEN $SP-22140$_" designed flash drive is made of "_$SP-14924$

    ELSE "Flash drive has "_$SP-22140$_" design";

--[Bullet 3] USB flash drive interface

    --USB 2.0 interface is compatible with various USB-ready devices (https://www.staples.com/Gigastone-8GB-USB-2-0-Black-and-Silver/product_2721783)
    IF $SP-18335$ LIKE "USB 2.0" 
        THEN "USB 2.0 interface is compatible with various USB-ready devices" 

    --USB 3.0 drive makes transporting and sharing files simple and convenient (https://www.staples.com/verbatim-32gb-pinstripe-usb-3-0-flash-drive-blue-red-2pk-70056/product_24337384) 
    ELSE IF $SP-18335$ LIKE "USB 3.0" 
        THEN "USB 3.0 interface makes transporting and sharing files simple and convenient" 

    --USB 3.1 interface enables blazing-fast data transferring ability (https://www.staples.com/SanDisk-Extreme-PRO-USB-3-1-Solid-State-Flash-Drive-SDCZ880-256G-A46/product_IM14U1074)
    ELSE IF A[2448].Value LIKE "USB 3.1" 
        THEN "USB 3.1 interface enables blazing-fast data transferring ability" 

    --USB Type-C flash drive provides a seamless way to move content between your Type-C devices (https://www.staples.com/lexar-jumpdrive-32gb-c20c-usb-type-c-flash-drive/product_24319519)
    ELSE IF $SP-18335$ LIKE "Type C" 
        THEN "USB Type-C flash drive provides a seamless way to move content between your Type-C devices" 

    --USB 3.0 and micro-USB connectors interface with multiple devices for ease of use (https://www.staples.com/SanDisk-Ultra-Dual-32GB-130MB-s-USB-3-0-Flash-Drive-SDDD2-032G-A46/product_1623787)
    ELSE IF A[2448].Value LIKE "%/ micro USB" 
        THEN A[2448].Value.Split("/").First()
            _" and micro-USB connectors interface with multiple devices for ease of use" 

    --Dual USB 3.0 and Apple Lightning flash drive (https://www.staples.com/Verbatim-32GB-iStore-n-Go-Dual-USB-3-0-Flash-Drive-for-Apple-Lightning-Devices/product_2290456)    
    ELSE IF A[2448].Value LIKE "%/ Apple Lightning" 
    AND A[2448].Value NOT LIKE "%micro USB%" 
        THEN "Dual " 
            _A[2448].Value.Split("/").First()
            _" and Apple Lightning flash drive" 
ELSE "@@";

--[Bullet 4] Maximum read & write speed (Mb per seconds)

    --10 Mbps read speed and 3 Mbps write speed for quick data access and transfer (https://www.staples.com/Gigastone-8GB-USB-2-0-Black-and-Silver/product_2721783)
    IF $SP-22260$ < 45
    AND $SP-22261$ IS NOT NULL
        THEN $SP-22260$
            _"MB/s read speed and " 
            _$SP-22261$
            _"MB/s write speed for quick data access and transfer" 

    --Up to 185MB/s read and 135MB/s write speeds provide swift performance (https://www.staples.com/PNY-Turbo-USB-3-0-Flash-Drive-Silver-Black/product_SS2196523)
    ELSE IF $SP-22260$ IS NOT NULL
    AND $SP-22261$ IS NOT NULL
        THEN "Up to "_$SP-22260$
            _"MB/s read and " 
            _$SP-22261$
            _"MB/s write speeds provide swift performance" 

    ELSE IF $SP-22260$ < 45
        THEN $SP-22260$
             _"MB/s read speed for quick data access and transfer" 
    ELSE IF $SP-22260$ IS NOT NULL
        THEN "Up to "_$SP-22260$
             _"MB/s read speed provides swift performance" 

    ELSE IF $SP-22261$ < 45
        THEN $SP-22260$
             _"MB/s write speed for quick data access and transfer" 
    ELSE IF $SP-22261$ IS NOT NULL
        THEN "Up to "_$SP-22260$
             _"MB/s write speed provides swift performance" 
ELSE "@@";

--[Bullet 5] Compatible With

    --Compatible with Windows ME/2000/XP/Vista/7/8/10, Mac OS 9.0 or later, Linux kernel V2.4 or later (https://www.staples.com/Gigastone-8GB-USB-2-0-Black-and-Silver/product_2721783)

IF A[429].Values IS NOT NULL AND A[429].Values.Count >2 THEN
         "Compatible with: "_          
         A[429].Values
         .IfLike("%Windows%", "PC")
         .IfLike("%MacOS%", "Mac")
         .IfLike("%Linux%", "Linux")
         .IfLike("%Android%", "Android")
         .IfLike("%Symbian%", "Symbian")
         .Distinct
         .FlattenWithAnd()
         .Prefix("##").Replace(" "," ##").Replace("##and ", ", and ")   

ELSE IF A[429].Values IS NOT NULL THEN
         "Compatible with: "_          
         A[429].Values
         .IfLike("%Windows%", "PC")
         .IfLike("%MacOS%", "Mac")
         .IfLike("%Linux%", "Linux")
         .IfLike("%Android%", "Android")
         .IfLike("%Symbian%", "Symbian")
         .Distinct
         .FlattenWithAnd()
         .Prefix("##").Replace(" "," ##")    

 ELSE IF A[603].Values IS NOT NULL THEN
    "Compatible with: ##"_A[603].Values.FlattenWithAnd().Replace(" "," ##")

ELSE IF $SP-382$ IS NOT NULL
        THEN "Compatible with: " 
            _$SP-382$
ELSE "@@";

--[Bullet 6] Encryption
    --It provides 256-bit AES encryption to protect your data from unauthorized access (https://www.staples.com/SanDisk-Ultra-Flair-64GB-150-Mbps-Read-USB-3-0-Flash-Drive-Silver-SDCZ73-064G-A46/product_IM12R5635)
    IF A[1997].Where("%-bit AES%").Values IS NOT NULL
        THEN A[1997].Where("%-bit AES%").Values
            _" encryption protects your data from unauthorized access" 
ELSE "@@";

    --Crypto-parameters protected with SHA-256 hashing (https://www.staples.com/Apricorn-Aegis-Secure-Key-3-0-30GB-195-162-Mbps-SuperSpeed-USB-3-0-Flash-Drive-Black-ASK3-30GB/product_IM1YB4286)         
    IF A[1997].Where("%SHA%").Values IS NOT NULL
        THEN "Crypto parameters: protected with SHA-" 
            _A[1997].Where("%SHA%").Values.First().ExtractDecimals()
            _" hashing" 
ELSE "@@";

--[Bullet 7] Security features

    --FIPS 140-2 Level 3 certified to meet the highest security and performance needs of government agencies, military, healthcare, financial services (https://www.staples.com/DataLocker-500GB-5-Gbps-External-Hard-Drive-Black-Silver-MXKB1B500G5001FIPS-E/product_IM11V4740)
    IF A[1997].Where("FIPS 140-2 Level 3").Values IS NOT NULL
        THEN 
            "FIPS 140-2 Level ##3 certified to meet the highest security and performance" 
ELSE "@@";

    --Auto-lock on USB port removal or after predetermined period of inactivity (https://www.staples.com/Apricorn-Aegis-Secure-Key-3-0-480GB-195-Mbps-162-Mbps-USB-3-0-Flash-Drive-Black-ASK3-480GB/product_IM13E7250)
    IF A[2909].Where("auto-locking").Values IS NOT NULL
        THEN "Auto-lock on USB port removal or after predetermined period of inactivity" 
ELSE "@@";

    --SanDisk SecureAccess software allows you to create a password-protected folder on your drive to store your private data (https://www.staples.com/product_IM1QY5993)
    IF A[344].Where("SanDisk SecureAccess").Values IS NOT NULL
        THEN "SanDisk SecureAccess software allows you to create a password-protected folder on your drive to store your private data" 

    --Password security software to protect sensitive data available for download (https://www.staples.com/Verbatim-Clip-It-8GB-USB-2-0-Flash-Drive-2-Pack-99156/product_1958211)
   ELSE IF A[2909].Where("password protection").Values IS NOT NULL
        THEN "Password security software to protect sensitive data" 
ELSE "@@";

    --Featuring MerlinSafe encryption software (https://www.staples.com/product_2455155)
    IF A[344].Where("MerlinSafe").Values IS NOT NULL
        THEN "Featuring MerlinSafe encryption software" 
ELSE "@@";

    --The drive comes with EncryptStick Lite software (https://www.staples.com/product_SS4829038)
    IF A[344].Where("EncryptStick Lite").Values IS NOT NULL
        THEN "Flash drive comes with ##EncryptStick ##Lite ##Software" 
ELSE "@@";

--Additional Bullet (if relevant) (Key Loop)

    --The included key loop easily attaches to key chains, so important files are never out of reach (https://www.staples.com/product_2503510)
    IF A[2909].Where("key ring loop").Values IS NOT NULL
        THEN "The included key loop easily attaches to key chains, so important files are never out of reach";

--Additional Bullet (if relevant) (LED light)

    --LED activity light that blinks during file transfer, so you know when the job is done (https://www.staples.com/lexar-jumpdrive-twist-turn-16gb-5-pack/product_24323499)
    IF A[2909].Where("LED access indicator").Values IS NOT NULL
        THEN "LED activity light that blinks during file transfer, so you know when the job is done";

--Additional Bullet (if relevant) (Opertaing temperature)

    --Temperature: 0 to 45 deg C (operating) (https://www.staples.com/product_IM12R5634)
IF A[358].UnitUSM LIKE "%F" 
    AND A[359].UnitUSM LIKE "%F" 
        THEN "Temperature: " 
             _A[358].ValueUSM
             _" to " 
             _A[359].ValueUSM
             _" degrees F (operating)";

--[Bullet 8] Languages Supported
--[Last Bullet] Warranty

IF A[4850].Value LIKE "United States%year%" THEN A[4850].Value.ExtractDecimals().Max().Postfix("-year manufacturer limited warranty")
ELSE IF A[4850].Value LIKE "United States%month%" THEN A[4850].Value.ExtractDecimals().Max().Postfix("-month manufacturer limited warranty")
ELSE IF A[430].Value IS NOT NULL THEN 
            A[430].Value
                .Replace("limited lifetime", "Lifetime")
                .Replace(" year","-year")
                .Replace(" days","-day")
                .Replace(" years", "-year")
                .Replace("-years", "-year")
                .Replace(" months", "-month")
                .Erase("limited")
                .Erase("manufacturer")
                .Replace("warranty", "manufacturer limited warranty")
                .ToUpperFirstChar()
ELSE "@@";
