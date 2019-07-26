--Type of business envelope & use    
IF A[6481].Value LIKE "envelope" 
AND A[6514].Value IS NOT NULL 
    THEN A[6514].Value.Replace("No. ","#").Erase("size ")_" business envelopes are suitable for many mailing projects" 
ELSE IF A[6481].Value LIKE "envelope" 
AND A[6485].Value LIKE "% (%)" 
    THEN A[6485].Value.Split(" (").First().Replace("No ","#")_" business envelopes are suitable for many mailing projects" 
ELSE IF A[6481].Value LIKE "envelope" 
    THEN "Business envelopes are suitable for many mailing projects" 
ELSE "@@";

--True color & Material of item; including paper weight (If Applicable)
IF $cnet_true_color$ NOT IN ("Assorted", "Multicolor", NULL)
AND $SP-21543$ IS NOT NULL 
AND $SP-21408$ IS NOT NULL 
    THEN "Made from "_$cnet_true_color$_" "_$SP-21543$_" lb. "_$SP-21408$_" for durability" 
ELSE IF $cnet_true_color$ IS NOT NULL 
AND $cnet_true_color$ NOT IN ("Assorted", "Multicolor")
AND $SP-21543$ IS NOT NULL 
    THEN "Made from "_$cnet_true_color$_" "_$SP-21543$_" lb. paper for durability" 
ELSE IF $cnet_true_color$ IS NOT NULL 
AND $cnet_true_color$ NOT IN ("Assorted", "Multicolor")
AND $SP-21408$ IS NOT NULL 
    THEN "Made from "_$cnet_true_color$_" "_$SP-21408$_" for durability" 
ELSE IF $cnet_true_color$ IN ("white", "manila")
    THEN "Envelope is "_$cnet_true_color$_"-colored and ideal for business or personal correspondance" 
ELSE IF $cnet_true_color$ IS NOT NULL 
AND $cnet_true_color$ NOT IN ("Assorted", "Multicolor")
    THEN "Available in eye-pleasing "_$cnet_true_color$_" color" 
ELSE IF $cnet_true_color$ IN ("assorted") 
        THEN "The assortment of colors allows you to color-code or simply add some color to mailings" 
ELSE IF $SP-21408$ IS NOT NULL 
    THEN "Made from "_$SP-21408$_" for durability" 
ELSE "@@";

--Envelope size
IF $SP-21019$ NOT IN (NULL, "Other")
    THEN "These envelopes measure ]]"_$SP-21019$_"[[ in size, providing enough space for your mail" 
ELSE "@@";

----Closure type & flap style
IF $SP-18916$ NOT IN (NULL, "V-Flap") 
AND $SP-18915$ LIKE "Gummed" 
    THEN $SP-18915$_" closure with "_$SP-18916$_" flap keeps contents in place" 

ELSE IF $SP-18915$ IN ("self seal") 
AND $SP-18916$ NOT IN (NULL, "V-Flap")  
    THEN "Self-seal closure with "_$SP-18916$_" flap seals securely without moistening" 

ELSE IF $SP-18915$ IN ("%seal", "EasyClose") 
AND $SP-18916$ NOT IN (NULL, "V-Flap")  
    THEN $SP-18915$_" closure with "_$SP-18916$_" flap seals securely without moistening" 
ELSE IF $SP-18915$ IS NOT NULL
AND $SP-18916$ NOT IN (NULL, "V-Flap")  
    THEN $SP-18915$_" closure with "_$SP-18916$_" flap for a secure seal" 

ELSE IF $SP-18916$ IN ("V-Flap") 
AND $SP-18915$ LIKE "Gummed" 
    THEN $SP-18915$_" closure with ##"_$SP-18916$_" keeps contents in place" 

ELSE IF $SP-18915$ IN ("self seal") 
AND $SP-18916$ NOT IN (NULL, "V-Flap")  
    THEN "Self-seal closure with ##"_$SP-18916$_" flap seals securely without moistening"     

ELSE IF $SP-18915$ IN ("%seal", "EasyClose") 
AND $SP-18916$ IN ("V-Flap") 
    THEN $SP-18915$_" closure with ##"_$SP-18916$_" seals securely without moistening" 
ELSE IF $SP-18915$ IS NOT NULL
AND $SP-18916$ IN ("V-Flap")  
    THEN $SP-18915$_" closure with ##"_$SP-18916$_" for a secure seal" 

ELSE IF $SP-18915$ IN ("self seal") 
THEN "Self-seal closure for a secure seal" 

ELSE IF $SP-18915$ IS NOT NULL 
THEN $SP-18915$_" closure for a secure seal" 

ELSE IF $SP-18916$ IN ("V-Flap") 
THEN $SP-18916$_" for a crisp, professional look" 

ELSE IF $SP-18916$ NOT IN (NULL, "V-Flap")  
THEN $SP-18916$_" flap for a crisp, professional look" 

ELSE "@@";

--Interior cushioning (if any)
IF A[6497].Value LIKE "%bubble wrap%" 
    THEN "Interior bubble wrap keeps mail cushioned during bumpy rides" 
ELSE "@@";
--Security tinted
IF $SP-18919$ LIKE "Yes" 
    THEN "Security-tinted pattern helps to discourage from reading sensitive information through the outside of the envelope" 
ELSE "@@";
--Envelope pack size
IF Request.Data["TX_UOM"] LIKE "%/%" 
     THEN Request.Data["TX_UOM"].Split("/").First()_" envelopes per " 
    _Request.Data["TX_UOM"].Split("/").Last().ToLower()
ELSE "@@";

--Post Consumer Content
IF $SP-21624$ NOT IN (NULL, "0") 
    THEN "Contains minimum "_$SP-21624$_"% post-consumer content" 
ELSE "@@";

--Recycled Content
IF $SP-21623$ NOT IN (NULL, "0")  
    THEN "Support the environment with envelopes made with "_$SP-21623$_"% recycled material" 
ELSE "@@";

--protection
IF A[6498].Values IS NOT NULL
AND A[6500].Values.Where("%resistant" , "%proof", "%repellent").Count > 1
    THEN A[6498].Values.Erase("-resistant").Flatten(", ").ToLower(true).ToUpperFirstChar()_", " 
    _A[6500].Values.Where("%resistant" , "%proof", "%repellent").FlattenWithAnd().Erase("resistant").Erase("proof").Erase("repellent").ToLower(true)_
    "resistant envelope provides extra security for item being shipped" 

ELSE IF A[6498].Values IS NOT NULL 
AND A[6500].Values.Where("%resistant" , "%proof", "%repellent").Count = 1 
    THEN A[6500].Values.Where("%resistant" , "%proof", "%repellent").Flatten().Erase("resistant").Erase("proof").Erase("repellent").ToUpperFirstChar()_" and "_A[6498].Values.Erase("resistant").Flatten(", ").ToLower(true)_"resistant envelope provides extra security for item being shipped" 

ELSE IF A[6498].Values.Where("%resistant") IS NOT NULL THEN
A[6498].Values.Erase("resistant").FlattenWithAnd().ToUpperFirstChar()_"-resistant envelope provides extra security for item being shipped" 

ELSE IF A[6500].Values.Where("%resistant", "%proof", "%repellent") IS NOT NULL 
    THEN A[6500].Values.Where("%resistant", "%proof", "%repellent").FlattenWithAnd().Erase("resistant").Erase("proof").Erase("repellent").ToLower(true).ToUpperFirstChar()
    _"resistant envelope provides extra security for item being shipped" 
ELSE "@@"; 

--Use for additional product and/or manufacturer information relevant to the customer buying decision
IF A[6500].Where("%address window%").Values IS NOT NULL AND A[6495].Value LIKE "%left%" 
    THEN "Simply position the address to show through the single-left window" 
        ELSE IF A[6493].Value = 1 AND A[6495].Value LIKE "%left%" 
            THEN "Simply position the address to show through the single-left window" 
                ELSE IF A[6493].Value = 2 
                    THEN "Double-window envelopes conveniently display addresses" 

ELSE IF A[6500].Where("%address window%").Values IS NOT NULL AND A[6495].Value LIKE "%right%" 
    THEN "Simply position the address to show through the single-right window" 
        ELSE IF A[6493].Value = 1 AND A[6495].Value LIKE "%right%" 
            THEN "Simply position the address to show through the single-right window" 
                ELSE IF A[6493].Value = 2 
                    THEN "Double-window envelopes conveniently display addresses";

--opening style
IF A[6488].Value LIKE "open side" 
    THEN "The flap is on the long side for easier filling" 
ELSE IF A[6488].Value LIKE "open end" OR $SP-18916$ LIKE "Open End" 
    THEN "Flap is on the short side, helping to keep things from falling out after opening";