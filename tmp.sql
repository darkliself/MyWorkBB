--2        Post-it size & Post-it color collection    
    IF $SP-18403$ LIKE "other" 
                AND $SP-21044$ IS NOT NULL AND $SP-20400$ IS NOT NULL
                THEN "##"_$SP-21044$_"""W x "_$SP-20400$_"""L sticky notes" 

ELSE IF $SP-350290$ LIKE "Assorted" 
AND A[6022].Values.Where("canary yellow") IS NOT NULL
                AND $SP-18403$ NOT IN ("Other", NULL)
            THEN $SP-21044$_"""W x "_$SP-20400$_"""L"_", Canary Yellow + Assorted Color Notes" 
        ELSE IF A[6022].Where("%Canary Yellow%").Values IS NOT NULL
            THEN $SP-21044$_"""W x "_$SP-20400$_"""L"_" notes in the canary yellow attach without staples, paperclips, or tape" 
        ELSE IF $SP-350290$ LIKE "Bora Bora" 
            THEN $SP-21044$_"""W x "_$SP-20400$_"""L"_" notes in the ##Bora ##Bora ##Collection hold stronger and longer than other notes" 
            ELSE IF $SP-350290$ LIKE "Bali" 
            THEN $SP-21044$_"""W x "_$SP-20400$_"""L"_" notes in the ##Bali ##Collection hold stronger and longer than other notes" 
        ELSE IF $SP-350290$ LIKE "Helsinki" 
            THEN "These "_$SP-21044$_"""W x "_$SP-20400$_"""L"_" ##Helsinki ##Collection notes are just the right size to carry around or keep on your desk" 
        ELSE IF $SP-350290$ LIKE "Jaipur" 
            THEN $SP-21044$_"""W x "_$SP-20400$_"""L"_" ##Jaipur ##Collection notes stand out from the crowd to give you peace of mind" 
        ELSE IF $SP-350290$ LIKE "Miami" 
            THEN $SP-21044$_"""W x "_$SP-20400$_"""L"_" Notes in the ##Miami ##Collection are a simple tool to keep your everyday organized" 
                ELSE IF $SP-350290$ LIKE "Rio de Janeiro" 
                AND $SP-18403$ NOT IN ("Other", NULL)
            THEN "Eye-catching "_$SP-21044$_"""W x "_$SP-20400$_"""L"_" ##Rio de ##Janeiro notes" 

        ELSE IF $SP-350290$ LIKE "Rio de Janeiro" 
            THEN "Grab even the busiest person's attention with the ##Rio de ##Janeiro ##Collection" 
        ELSE IF $SP-350290$ LIKE "Cape Town" 
            THEN $SP-21044$_"""W x "_$SP-20400$_"""L"_" Notes in the ##Cape ##Town ##Collection are a simple tool to keep your everyday organized" 

ELSE IF $SP-350290$ LIKE "Marrakesh" AND $SP-18403$ NOT IN (NULL, "Other") 
            THEN $SP-21044$_"""W x "_$SP-20400$_"""L"_" notes in the ##Marrakesh ##Collection keep your messages visible" 

                ELSE IF $SP-350290$ LIKE "New York" AND $SP-18403$ NOT IN (NULL, "Other") 
            THEN $SP-21044$_"""W x "_$SP-20400$_"""L"_" ##New ##York notes include colors inspired by the skyline, stone and steel" 

        ELSE IF $SP-350290$ LIKE "New York" 
            THEN "New York collection includes colors inspired by the skyline, stone and steel" 
        ELSE IF $SP-350290$ LIKE "Marseille" AND $SP-18403$ NOT IN (NULL, "Other")
            THEN $SP-21044$_"""W x "_$SP-20400$_"""L"_" Notes in the ##Marseille ##Collection are a simple tool to keep you organized" 

ELSE IF $SP-350290$ LIKE "Assorted" 
AND A[6022].Values.Where("canary yellow") IS NOT NULL
                AND $SP-18403$ NOT IN ("Other", NULL)
            THEN $SP-18403$_", Canary Yellow + Assorted Color Notes" 
        ELSE IF A[6022].Where("%Canary Yellow%").Values IS NOT NULL
            THEN $SP-18403$_" notes in the canary yellow attach without staples, paperclips, or tape" 
        ELSE IF $SP-350290$ LIKE "Bora Bora" 
            THEN $SP-18403$_" notes in the ##Bora ##Bora ##Collection hold stronger and longer than other notes" 
            ELSE IF $SP-350290$ LIKE "Bali" 
            THEN $SP-18403$_" notes in the ##Bali ##Collection hold stronger and longer than other notes" 
        ELSE IF $SP-350290$ LIKE "Helsinki" 
            THEN "These "_$SP-18403$_" ##Helsinki ##Collection notes are just the right size to carry around or keep on your desk" 
        ELSE IF $SP-350290$ LIKE "Jaipur" 
            THEN $SP-18403$_" ##Jaipur ##Collection notes stand out from the crowd to give you peace of mind" 
        ELSE IF $SP-350290$ LIKE "Miami" 
            THEN $SP-18403$_" Notes in the ##Miami ##Collection are a simple tool to keep your everyday organized" 
                ELSE IF $SP-350290$ LIKE "Rio de Janeiro" 
                AND $SP-18403$ NOT IN ("Other", NULL)
            THEN "Eye-catching "_$SP-18403$_" ##Rio de ##Janeiro notes" 

        ELSE IF $SP-350290$ LIKE "Rio de Janeiro" 
            THEN "Grab even the busiest person's attention with the ##Rio de ##Janeiro ##Collection" 
        ELSE IF $SP-350290$ LIKE "Cape Town" 
            THEN $SP-18403$_" Notes in the ##Cape ##Town ##Collection are a simple tool to keep your everyday organized" 

ELSE IF $SP-350290$ LIKE "Marrakesh" AND $SP-18403$ NOT IN (NULL, "Other") 
            THEN $SP-18403$ _" notes in the ##Marrakesh ##Collection keep your messages visible" 

                ELSE IF $SP-350290$ LIKE "New York" AND $SP-18403$ NOT IN (NULL, "Other") 
            THEN $SP-18403$_" ##New ##York notes include colors inspired by the skyline, stone and steel" 

        ELSE IF $SP-350290$ LIKE "New York" 
            THEN "New York collection includes colors inspired by the skyline, stone and steel" 
        ELSE IF $SP-350290$ LIKE "Marseille" AND $SP-18403$ NOT IN (NULL, "Other")
            THEN $SP-18403$_" Notes in the ##Marseille ##Collection are a simple tool to keep you organized" 
        ELSE $SP-21044$_"""W x "_$SP-20400$_"""L"_" Notes are just the right size to leave reminders on your computer monitor";